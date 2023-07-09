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
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;


namespace Medical_App
{
    public partial class Pacientes : System.Web.UI.Page
    {
        DataTable dtNacionalidad = new DataTable();
        DataTable dtProvincia = new DataTable();
        DataTable dtEstadoCivil = new DataTable();
        List<oTipoIdentificacion> listaTipoIdentifiacion = new List<oTipoIdentificacion>();
        List<oGenero> listaGenero = new List<oGenero>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Asignamos los valores en Nacionalidad
                dtNacionalidad.Columns.Add("id");
                dtNacionalidad.Columns.Add("nacionalidad");
                dtNacionalidad.Rows.Add(1, "Nacional");
                dtNacionalidad.Rows.Add(2, "Extranjero");

                dpNacionalidad.DataTextField = "nacionalidad";
                dpNacionalidad.DataValueField = "id";
                dpNacionalidad.DataSource = dtNacionalidad;
                dpNacionalidad.DataBind();

                // Asignamos los valores en Provincia
                dtProvincia.Columns.Add("id");
                dtProvincia.Columns.Add("provincia");
                dtProvincia.Rows.Add(1, "Cartago");
                dtProvincia.Rows.Add(2, "San José");
                dtProvincia.Rows.Add(3, "Heredia");
                dtProvincia.Rows.Add(4, "Alajuela");
                dtProvincia.Rows.Add(5, "Puntarenas");
                dtProvincia.Rows.Add(6, "Guanacaste");
                dtProvincia.Rows.Add(7, "Limón");
                dpProvincia.DataTextField = "provincia";
                dpProvincia.DataValueField = "id";
                dpProvincia.DataSource = dtProvincia;
                dpProvincia.DataBind();

                // Asignamos los valores  EstadoCivil
                dtEstadoCivil.Columns.Add("id");
                dtEstadoCivil.Columns.Add("estadoCivil");
                dtEstadoCivil.Rows.Add(1, "Soltero");
                dtEstadoCivil.Rows.Add(2, "Casado");
                dtEstadoCivil.Rows.Add(3, "Viudo");
                dpEstadoCivil.DataTextField = "estadoCivil";
                dpEstadoCivil.DataValueField = "id";
                dpEstadoCivil.DataSource = dtEstadoCivil;
                dpEstadoCivil.DataBind();

                // Llena la lista de TipoIdentificacion
                listaTipoIdentifiacion.Add(new oTipoIdentificacion { id = 1, tipoIdentificacion = "Cédula nacional" });
                listaTipoIdentifiacion.Add(new oTipoIdentificacion { id = 2, tipoIdentificacion = "DIMEX" });
                listaTipoIdentifiacion.Add(new oTipoIdentificacion { id = 3, tipoIdentificacion = "Pasaporte" });
                foreach (var item in listaTipoIdentifiacion)
                {
                    dptipoIdentificacion.Items.Add(new ListItem(item.tipoIdentificacion, item.id.ToString()));
                }

                // Llena la lista de Genero
                listaGenero.Add(new oGenero { id = 1, genero = "Hombre" });
                listaGenero.Add(new oGenero { id = 2, genero = "Mujer" });
                listaGenero.Add(new oGenero { id = 3, genero = "Otro" });
                foreach (var item in listaGenero)
                {
                    dpGenero.Items.Add(new ListItem(item.genero, item.id.ToString()));
                }

                var listaPacientes = (List<oPersonas>)Session["listaPacientes"];

               
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores del formulario
                Random random = new Random();
                int Id = random.Next(10000, 100000);
                string nombre = txtNombre.Text;
                string apellido = txtApellido1.Text;
                string numeroIdentificacion = txtNumeroIdentificacion.Text;
                string fechaNacimiento = txtFechaNacimiento.Text;
                string estadoCivil = dpEstadoCivil.SelectedItem.Text;
                string tipoIdentificacion = dptipoIdentificacion.SelectedItem.Text;
                string genero = dpGenero.SelectedItem.Text;
                string nacionalidad = dpNacionalidad.SelectedItem.Text;
                string provincia = dpProvincia.SelectedItem.Text;
                string telefono = txtTelefono.Text;
                string correo = txtCorreo.Text;

                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(numeroIdentificacion))
                {




                    // Obtener achivos de excel
                    string excelFilePath = Server.MapPath("~/Uploads/BaseDeDatos.xlsx"); // Ruta del excel guardado

                    using (XLWorkbook workbook = new XLWorkbook(excelFilePath))
                    {
                        IXLWorksheet worksheet = workbook.Worksheet("Pacientes");


                        int lastRow = worksheet.CellsUsed(c => c.Address.ColumnLetter == "A").LastOrDefault().Address.RowNumber;

                        // Agregar los nuevos a la fila disponible
                        worksheet.Cell(lastRow + 1, 1).Value = Id.ToString();
                        worksheet.Cell(lastRow + 1, 2).Value = nombre;
                        worksheet.Cell(lastRow + 1, 3).Value = apellido;
                        worksheet.Cell(lastRow + 1, 4).Value = tipoIdentificacion;
                        worksheet.Cell(lastRow + 1, 5).Value = numeroIdentificacion;
                        worksheet.Cell(lastRow + 1, 6).Value = genero;
                        worksheet.Cell(lastRow + 1, 7).Value = estadoCivil;
                        worksheet.Cell(lastRow + 1, 8).Value = DateTime.Parse(fechaNacimiento);
                        worksheet.Cell(lastRow + 1, 9).Value = nacionalidad;
                        worksheet.Cell(lastRow + 1, 10).Value = provincia;
                        worksheet.Cell(lastRow + 1, 11).Value = telefono;
                        worksheet.Cell(lastRow + 1, 12).Value = correo;

                        // Guardar los cambios de Excel
                        workbook.Save();

                        var listaUsuarios = (List<oUsuarios>)Session["listaUsuarios"];
                        string rol = "";

                        foreach (var usuario in listaUsuarios)
                        {
                            rol = usuario.rol;
                            if (rol == "administrador")
                            {
                                Response.Redirect("/admin.aspx");
                            }
                            else if (rol == "medico")
                            {
                                Response.Redirect("/medicos.aspx");
                            }


                        }
                            

                    }

                    // Borrar la información 
                    txtNombre.Text = "";
                    txtApellido1.Text = "";
                    txtNumeroIdentificacion.Text = "";
                    txtFechaNacimiento.Text = "";
                    dpEstadoCivil.SelectedIndex = 0;
                    dptipoIdentificacion.SelectedIndex = 0;
                    dpGenero.SelectedIndex = 0;
                    dpNacionalidad.SelectedIndex = 0;
                    dpProvincia.SelectedIndex = 0;
                    txtTelefono.Text = "";
                    txtCorreo.Text = "";

                  
                }
            }
            catch (Exception ex)
            {
                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                divMensaje.Style["display"] = "block";
            }
        }


        protected void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var listaPacientes = (List<oPersonas>)Session["listaPacientes"];
                if (listaPacientes != null)
                {
                    var dt = GeneraTablaDinamica<oPersonas>(listaPacientes);

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "ListaPacientes");

                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=Pacientes.xlsx");

                        using (MemoryStream ms = new MemoryStream())
                        {
                            wb.SaveAs(ms);
                            ms.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // La excepción
            }
        }

        public DataTable GeneraTablaDinamica<T>(List<T> items)
        {
            DataTable dt = new DataTable();
            if (items.Count > 0)
            {
                foreach (var prop in items[0].GetType().GetProperties())
                {
                    dt.Columns.Add(prop.Name);
                }
                foreach (var item in items)
                {
                    var values = new object[dt.Columns.Count];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        values[i] = item.GetType().GetProperty(dt.Columns[i].ColumnName).GetValue(item, null);
                    }
                    dt.Rows.Add(values);
                }
            }
            return dt;
        }
    }
}


