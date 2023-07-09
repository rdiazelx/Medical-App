using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using DocumentFormat.OpenXml.Spreadsheet;
using ClosedXML.Excel;
using System.Globalization;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Bibliography;

namespace Medical_App
{


    public partial class Citas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPacientes();
                CargarMedicos();
            }

        }







        protected void CargarPacientes()
        {
            try
            {
                string ruta = Server.MapPath("~/Uploads/BaseDeDatos.xlsx"); // Ruta del archivo en la carpeta "Uploads"

                string conec = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                conec = string.Format(conec, ruta, "Yes");

                OleDbConnection connExcel = new OleDbConnection(conec);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter adapterExcel = new OleDbDataAdapter();

                cmdExcel.Connection = connExcel;

                // Obtener el nombre de la hoja "Pacientes"
                connExcel.Open();
                DataTable dtExcel = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string hojaPacientes = dtExcel.Rows[4]["TABLE_NAME"].ToString();
                connExcel.Close();

                // Cargar los datos de la hoja "Pacientes" en un DataTable
                connExcel.Open();
                cmdExcel.CommandText = "SELECT [Id], [Nombre], [Apellido]   FROM [" + hojaPacientes + "]";
                adapterExcel.SelectCommand = cmdExcel;
                DataTable dtPacientes = new DataTable();
                adapterExcel.Fill(dtPacientes);
                connExcel.Close();

                // Crear la lista de pacientes
                var listaPacientes = new List<oPersonas>();

                if (dtPacientes.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPacientes.Rows.Count; i++)
                    {
                        var objPaciente = new oPersonas();
                        objPaciente.id = dtPacientes.Rows[i]["Id"].ToString();
                        objPaciente.nombre = dtPacientes.Rows[i]["Nombre"].ToString();
                        objPaciente.apellido = dtPacientes.Rows[i]["Apellido"].ToString();

                        // Agregar el paciente a la lista
                        listaPacientes.Add(objPaciente);
                    }
                }

                // Guardar la lista de pacientes en la sesión
                Session["listaPacientes"] = listaPacientes;

                // Cargar los pacientes en el DropDownList
                CargarDropDownListP(dpPacientes, listaPacientes);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al cargar los pacientes: " + ex.Message;
                lblError.Text = errorMessage;
                lblError.Visible = true;

            }
        }

        protected void CargarMedicos()
        {
            try
            {
                string ruta = Server.MapPath("~/Uploads/BaseDeDatos.xlsx");

                string conec = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                conec = string.Format(conec, ruta, "Yes");

                OleDbConnection connExcel = new OleDbConnection(conec);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter adapterExcel = new OleDbDataAdapter();

                cmdExcel.Connection = connExcel;

                // Obtener el nombre de la hoja "Medicos"
                connExcel.Open();
                DataTable dtExcel = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string hojaMedicos = dtExcel.Rows[3]["TABLE_NAME"].ToString();
                connExcel.Close();

                // Cargar los datos de la hoja "Medicos" en un DataTable
                connExcel.Open();
                cmdExcel.CommandText = "SELECT [Id], [Nombre], [Apellido], [Especialidad] FROM [" + hojaMedicos + "]";
                adapterExcel.SelectCommand = cmdExcel;
                DataTable dtMedicos = new DataTable();
                adapterExcel.Fill(dtMedicos);
                connExcel.Close();

                // Crear la lista de médicos
                var listaMedicos = new List<oMedicos>();

                if (dtMedicos.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMedicos.Rows.Count; i++)
                    {
                        var objMedico = new oMedicos();
                        objMedico.id = Int32.Parse(dtMedicos.Rows[i]["Id"].ToString());
                        objMedico.nombre = dtMedicos.Rows[i]["Nombre"].ToString();
                        objMedico.apellido = dtMedicos.Rows[i]["Apellido"].ToString();
                        objMedico.especialidad = dtMedicos.Rows[i]["Especialidad"].ToString();

                        // Agregar el médico a la lista
                        listaMedicos.Add(objMedico);
                    }
                }

                // Guardar la lista de médicos en la sesión
                Session["listaMedicos"] = listaMedicos;

                // Cargar los médicos en el DropDownList
                CargarDropDownListM(dpMedicos, listaMedicos);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al cargar los Medicos: " + ex.Message;
                lblError.Text = errorMessage;
                lblError.Visible = true;
            }
        }



        private void CargarDropDownListP(DropDownList dropDownList, List<oPersonas> items)
        {
            try
            {
                dropDownList.DataSource = items;
                dropDownList.DataTextField = "nombreCompleto";
                dropDownList.DataValueField = "nombreCompleto";
                dropDownList.DataBind();

                dropDownList.Items.Insert(0, new ListItem("Seleccione", ""));

            }
            catch (Exception ex)
            {
                string errorMessage = "Error al cargar los pacientes: " + ex.Message;
                lblError.Text = errorMessage;
                lblError.Visible = true;
            }

        }

        private void CargarDropDownListM(DropDownList dropDownList, List<oMedicos> items)
        {
            try
            {
                dropDownList.DataSource = items;
                dropDownList.DataTextField = "nombreCompleto";
                dropDownList.DataValueField = "nombreCompleto";
                dropDownList.DataBind();

                dropDownList.Items.Insert(0, new ListItem("Seleccione", ""));
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al cargar los Medicos: " + ex.Message;
                lblError.Text = errorMessage;
                lblError.Visible = true;
            }

        }

        protected void dpMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el médico seleccionado
            var listaMedicos = (List<oMedicos>)Session["listaMedicos"];
            var medicoSeleccionado = listaMedicos.FirstOrDefault(m => m.nombreCompleto == dpMedicos.SelectedItem.Text);

            // Verificar si se seleccionó un médico válido
            if (medicoSeleccionado != null)
            {
                txtEspecialidad.Text = medicoSeleccionado.especialidad;
            }
            else
            {
                txtEspecialidad.Text = string.Empty;
            }
        }




        protected void VerCitas(object sender, EventArgs e)
        {
            try
            {
                string ruta = Server.MapPath("~/Uploads/BaseDeDatos.xlsx");

                string conec = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

                conec = string.Format(conec, ruta, "Yes");

                OleDbConnection connExcel = new OleDbConnection(conec);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter adapterExcel = new OleDbDataAdapter();

                cmdExcel.Connection = connExcel;


                connExcel.Open();
                DataTable dtExcel = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string hojaExcel = dtExcel.Rows[0]["TABLE_NAME"].ToString();


                string hojaCitas = "Citas$";


                DataTable dtCitas = new DataTable();

                cmdExcel.CommandText = "Select * from [" + hojaCitas + "]";
                adapterExcel.SelectCommand = cmdExcel;
                adapterExcel.Fill(dtCitas);

                connExcel.Close();


                gridLista.DataSource = dtCitas;
                gridLista.DataBind();


                foreach (GridViewRow row in gridLista.Rows)
                {
                    row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridLista, "Select$" + row.RowIndex);
                }



                var listaCitas = new List<oCitas>();
                var objCitas = new oCitas();
                if (dtCitas.Rows.Count > 0)
                {
                    foreach (DataRow row in dtCitas.Rows)
                    {



                        objCitas.IdCita = Convert.IsDBNull(row["IdCita"]) ? 0 : Convert.ToInt32(row["IdCita"]);
                        objCitas.Paciente = row["Paciente"].ToString();
                        objCitas.Medico = row["Medico"].ToString();
                        objCitas.Especialidad = row["Especialidad"].ToString();
                        objCitas.Enfermedad = row["Enfermedad"].ToString();

                        objCitas.Sucursal = row["Sucursal"].ToString();
                        objCitas.Medicamentos = row["Medicamentos"].ToString();
                        objCitas.Indicaciones = row["Indicaciones"].ToString();

                        string fechaPString = row["fechaPrescripcion"].ToString();
                        DateTime fechaP;
                        if (DateTime.TryParseExact(fechaPString, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaP))
                        {
                            objCitas.fechaPrescripcion = fechaP;
                        }
                        else
                        {

                            objCitas.fechaPrescripcion = DateTime.MinValue;
                        }

                        string fechaString = row["Fecha"].ToString();
                        DateTime fecha;
                        if (DateTime.TryParseExact(fechaString, "d/M/yyyy H:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                        {
                            objCitas.Fecha = fecha;
                        }
                        else
                        {

                            objCitas.Fecha = DateTime.MinValue;
                        }


                    }



                    listaCitas.Add(objCitas);
                }


                Session["listaCitas"] = listaCitas;
            }

            catch (Exception ex)

            {

                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";

                divMensaje.Style["display"] = "block";
            }

        }


        protected void gridLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            try
            {

                if (e.CommandName == "Select")
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gridLista.Rows[rowIndex];
                    int id = Int32.Parse(selectedRow.Cells[0].Text);


                    var listaEnfermedades = (List<oEnfermedades>)Session["listaEnfermedades"];

                    if (listaEnfermedades.Count > 0)
                    {
                        listaEnfermedades = listaEnfermedades.FindAll(p => p.id == id);
                        DataTable dt = GeneraTablaDinamica<oEnfermedades>(listaEnfermedades);
                        gridLista.DataSource = dt;
                        gridLista.DataBind();
                    }

                }




            }
            catch (Exception ex)
            {
                // Establecer el texto del mensaje
                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                // Mostrar el cuadro de mensaje
                divMensaje.Style["display"] = "block";
            }
        }

        private DataTable GeneraTablaDinamica<T>(List<T> lista)
        {
            DataTable dt = new DataTable();
            PropertyDescriptorCollection listaProp = TypeDescriptor.GetProperties(typeof(T));

            for (int i = 0; i < listaProp.Count; i++)
            {
                PropertyDescriptor prop = listaProp[i];
                dt.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] valores = new object[listaProp.Count];
            foreach (T item in lista)
            {
                for (int i = 0; i < valores.Length; i++)
                {
                    valores[i] = listaProp[i].GetValue(item);
                }
                dt.Rows.Add(valores);
            }
            return dt;
        }

        protected void GuardarCita(object sender, EventArgs e)
        {
            try
            {
                string paciente = dpPacientes.SelectedValue;
                string medico = dpMedicos.SelectedValue;
                string sucursal = dpSucursal.SelectedItem.Text;
                DateTime fecha = DateTime.Now;
                DateTime fechaPrescripcion = DateTime.Parse(txtFecha.Text);
                string especialidad = txtEspecialidad.Text;
                string medicamentos = txtMedicamentos.Text;
                string indicaciones = txtIndicaciones.Text;
                string enfermedad = txtEnfermedad.Text;


                if (!string.IsNullOrEmpty(paciente) && !string.IsNullOrEmpty(medico))
                {
                    string excelFilePath = Server.MapPath("~/Uploads/BaseDeDatos.xlsx");

                    ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('¡Cita guardada con éxito!');", true);

                    dpPacientes.SelectedIndex = 0;
                    dpMedicos.SelectedIndex = 0;
                    dpSucursal.SelectedIndex = 0;
                    txtEspecialidad.Text = string.Empty;
                    txtMedicamentos.Text = string.Empty;
                    txtIndicaciones.Text = string.Empty;
                    txtFecha.Text = string.Empty;
                    txtEnfermedad.Text = string.Empty;

                    if (File.Exists(excelFilePath))
                    {
                        using (XLWorkbook workbook = new XLWorkbook(excelFilePath))
                        {
                            IXLWorksheet worksheet = workbook.Worksheets.FirstOrDefault(w => w.Name == "Citas") ?? workbook.Worksheets.Add("Citas");

                            int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 0;

                            worksheet.Cell(lastRow + 1, 1).Value = lastRow + 1; // IdCita
                            worksheet.Cell(lastRow + 1, 2).Value = paciente; // IdPaciente
                            worksheet.Cell(lastRow + 1, 3).Value = medico; // IdMedico
                            worksheet.Cell(lastRow + 1, 4).Value = sucursal; // Sucursal
                            worksheet.Cell(lastRow + 1, 5).Value = fecha; // Fecha
                            worksheet.Cell(lastRow + 1, 6).Value = especialidad;
                            worksheet.Cell(lastRow + 1, 7).Value = medicamentos;
                            worksheet.Cell(lastRow + 1, 8).Value = indicaciones;
                            worksheet.Cell(lastRow + 1, 9).Value = fechaPrescripcion;
                            worksheet.Cell(lastRow + 1, 10).Value = enfermedad;

                            // Guardar el archivo Excel
                            workbook.Save();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al guardar la cita: " + ex.Message;
                lblError.Text = errorMessage;
                lblError.Visible = true;
            }
        }


    }

}

