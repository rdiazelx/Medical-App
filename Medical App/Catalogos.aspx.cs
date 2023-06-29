using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Medical_App
{
    public partial class Catalogos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
            protected void btnPacientes_Click(object sender, EventArgs e)
            {
                try
                {
                    string ruta = Server.MapPath("~/Uploads/Basedatos-Catalogos.xlsx"); // Ruta del archivo en la carpeta "Uploads"

                    //leer el archivo de excel
                    string conec = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

                    conec = string.Format(conec, ruta, "Yes");

                    OleDbConnection connExcel = new OleDbConnection(conec);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter adapterExcel = new OleDbDataAdapter();

                    cmdExcel.Connection = connExcel;

                    //abrir el archivo
                    //obtener el nombre de la primer hoja
                    connExcel.Open();
                    DataTable dtExcel = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string hojaExcel = dtExcel.Rows[0]["TABLE_NAME"].ToString();






                    string hojaPacientes = "Pacientes$";
                    string hojaEnfermedades = "Enfermedades$";

                    //crear un datatable
                    DataTable dtPacientes = new DataTable();
                    DataTable dtEnfermedades = new DataTable();

                    //obtiene la data del la hoja pacientes
                    cmdExcel.CommandText = "Select * from [" + hojaPacientes + "]";
                    adapterExcel.SelectCommand = cmdExcel;
                    adapterExcel.Fill(dtPacientes);

                    //obtiene la data del la hoja Enfermedades
                    cmdExcel.CommandText = "Select * from [" + hojaEnfermedades + "]";
                    adapterExcel.SelectCommand = cmdExcel;
                    adapterExcel.Fill(dtEnfermedades);


                    connExcel.Close();

                    //mis datos están en el dt

                    gridListaPacientes.DataSource = dtPacientes;
                    gridListaPacientes.DataBind();


                    foreach (GridViewRow row in gridListaPacientes.Rows)
                    {
                        row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridListaPacientes, "Select$" + row.RowIndex);
                    }


                    //listado de Pacientes
                    //Recorrer la tabla (dt) para cargar la lista de pacientes
                    var listaPacientes = new List<oPersonas>();

                    if (dtPacientes.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPacientes.Rows.Count; i++)
                        {
                            var objPacientes = new oPersonas();
                            objPacientes.id = Int32.Parse(dtPacientes.Rows[i]["Id"].ToString());
                            objPacientes.nombre = dtPacientes.Rows[i]["Nombre del paciente"].ToString();
                            objPacientes.apellido = dtPacientes.Rows[i]["Apellido del paciente"].ToString();
                            objPacientes.tipoIdentificacion = dtPacientes.Rows[i]["Tipo de Identificacion"].ToString();
                            objPacientes.numeroIdentificacion = Int32.Parse(dtPacientes.Rows[i]["Identificacion"].ToString());
                            objPacientes.genero = dtPacientes.Rows[i]["Genero"].ToString();
                            objPacientes.estadoCivil = dtPacientes.Rows[i]["Estado civil"].ToString();
                            objPacientes.fechaNacimiento = DateTime.Parse(dtPacientes.Rows[i]["fechaNacimiento"].ToString());
                            objPacientes.nacionalidad = dtPacientes.Rows[i]["Nacionalidad"].ToString();
                            objPacientes.provincia = dtPacientes.Rows[i]["Provincia"].ToString();
                            objPacientes.telefono = Int32.Parse(dtPacientes.Rows[i]["Telefono"].ToString());
                            objPacientes.correo = dtPacientes.Rows[i]["correo"].ToString();

                            listaPacientes.Add(objPacientes);
                        }

                        Session["listaPacientes"] = listaPacientes;
                    }
                    //Recorrer la tabla (dt) para cargar la lista de enfermedades
                    var listaEnfermedades = new List<oEnfermedades>();

                    if (dtEnfermedades.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtEnfermedades.Rows.Count; i++)
                        {
                            var objEnfermedades = new oEnfermedades();
                            objEnfermedades.id = Int32.Parse(dtEnfermedades.Rows[i]["Id"].ToString());
                            objEnfermedades.nombre = dtEnfermedades.Rows[i]["Nombre de enfermedad"].ToString();
                            objEnfermedades.descripcion = dtEnfermedades.Rows[i]["Descripcion"].ToString();


                            listaEnfermedades.Add(objEnfermedades);
                        }

                        Session["listaEnfermedades"] = listaEnfermedades;
                    }

                    labelInfoCargada.InnerText = "Información cargada.";

                }
                catch (Exception)
                {
                    // Establecer el texto del mensaje
                    mensajeTexto.InnerText = "No fue posible cargar la información requerida";
                    // Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
                }


            }





            protected void btnSucursales_Click(object sender, EventArgs e)
            {
                try
                {
                    string ruta = Server.MapPath("~/Uploads/Basedatos-Catalogos.xlsx"); // Ruta del archivo en la carpeta "Uploads"


                    //leer el archivo de excel
                    string conec = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

                    conec = string.Format(conec, ruta, "Yes");

                    OleDbConnection connExcel = new OleDbConnection(conec);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter adapterExcel = new OleDbDataAdapter();

                    cmdExcel.Connection = connExcel;

                    //abrir el archivo
                    //obtener el nombre de la primer hoja
                    connExcel.Open();
                    DataTable dtExcel = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string hojaExcel = dtExcel.Rows[0]["TABLE_NAME"].ToString();

                    string hojaSucursales = "Sucursales$";

                    //crear un datatable
                    DataTable dtSucursales = new DataTable();


                    //obtiene la data del la hoja Sucursales
                    cmdExcel.CommandText = "Select * from [" + hojaSucursales + "]";
                    adapterExcel.SelectCommand = cmdExcel;
                    adapterExcel.Fill(dtSucursales);


                    connExcel.Close();

                    //mis datos están en el dt

                    gridSucursales.DataSource = dtSucursales;
                    gridSucursales.DataBind();



                    //Recorrer la tabla (dt) para cargar la lista de personas
                    var listaSucursales = new List<oSucursales>();

                    if (dtSucursales.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtSucursales.Rows.Count; i++)
                        {
                            var objSucursales = new oSucursales();
                            objSucursales.lugar = dtSucursales.Rows[i]["Lugar"].ToString();
                            objSucursales.dirreccion = dtSucursales.Rows[i]["dirreccion"].ToString();
                            objSucursales.telefono = Int32.Parse(dtSucursales.Rows[i]["telefono"].ToString());
                            objSucursales.correo = dtSucursales.Rows[i]["correo"].ToString();


                            listaSucursales.Add(objSucursales);
                        }

                        Session["listaSucursales"] = listaSucursales;
                    }

                    labelInfoCargada.InnerText = "Información cargada.";

                }
                catch (Exception ex)
                {
                    // Establecer el texto del mensaje
                    mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                    // Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
                }

            }






            protected void btnEnfermedades_Click(object sender, EventArgs e)
            {


                try
                {
                    string ruta = Server.MapPath("~/Uploads/Basedatos-Catalogos.xlsx"); // Ruta del archivo en la carpeta "Uploads"

                    //leer el archivo de excel
                    string conec = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

                    conec = string.Format(conec, ruta, "Yes");

                    OleDbConnection connExcel = new OleDbConnection(conec);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter adapterExcel = new OleDbDataAdapter();

                    cmdExcel.Connection = connExcel;

                    //abrir el archivo
                    //obtener el nombre de la primer hoja
                    connExcel.Open();
                    DataTable dtExcel = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string hojaExcel = dtExcel.Rows[0]["TABLE_NAME"].ToString();





                    string hojaEnfermedades = "Enfermedades$";


                    //crear un datatable
                    DataTable dtEnfermedades = new DataTable();


                    //obtiene la data del la hoja Enfermedades
                    cmdExcel.CommandText = "Select * from [" + hojaEnfermedades + "]";
                    adapterExcel.SelectCommand = cmdExcel;
                    adapterExcel.Fill(dtEnfermedades);





                    connExcel.Close();

                    //mis datos están en el dt

                    gridListaEnfermedades.DataSource = dtEnfermedades;
                    gridListaEnfermedades.DataBind();


                    //Recorrer la tabla (dt) para cargar la lista de enfermedades
                    var listaEnfermedades = new List<oEnfermedades>();

                    if (dtEnfermedades.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtEnfermedades.Rows.Count; i++)
                        {
                            var objEnfermedades = new oEnfermedades();

                            objEnfermedades.id = Int32.Parse(dtEnfermedades.Rows[i]["Id"].ToString());
                            objEnfermedades.nombre = dtEnfermedades.Rows[i]["Nombre de enfermedad"].ToString();
                            objEnfermedades.descripcion = dtEnfermedades.Rows[i]["Descripcion"].ToString();


                            listaEnfermedades.Add(objEnfermedades);
                        }

                        Session["listaEnfermedades"] = listaEnfermedades;
                    }


                    labelInfoCargada.InnerText = "Información cargada.";

                }
                catch (Exception)
                {
                    // Establecer el texto del mensaje
                    mensajeTexto.InnerText = "No fue posible cargar la información requerida";
                    // Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
                }

                labelInfoCargada.InnerText = "Información cargada.";
            }

            protected void btnMedicamentos_Click(object sender, EventArgs e)
            {
                try
                {
                    string ruta = Server.MapPath("~/Uploads/Basedatos-Catalogos.xlsx"); // Ruta del archivo en la carpeta "Uploads"

                    //leer el archivo de excel
                    string conec = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

                    conec = string.Format(conec, ruta, "Yes");

                    OleDbConnection connExcel = new OleDbConnection(conec);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter adapterExcel = new OleDbDataAdapter();

                    cmdExcel.Connection = connExcel;

                    //abrir el archivo
                    //obtener el nombre de la primer hoja
                    connExcel.Open();
                    DataTable dtExcel = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string hojaExcel = dtExcel.Rows[0]["TABLE_NAME"].ToString();

                    string hojaMedicamentos = "Medicamentos$";

                    //crear un datatable
                    DataTable dtMedicamentos = new DataTable();

                    //obtiene la data del la hoja medicamentos
                    cmdExcel.CommandText = "Select * from [" + hojaMedicamentos + "]";
                    adapterExcel.SelectCommand = cmdExcel;
                    adapterExcel.Fill(dtMedicamentos);

                    connExcel.Close();

                    //mis datos están en el dt

                    gridMedicamentos.DataSource = dtMedicamentos;
                    gridMedicamentos.DataBind();


                    // Recorrer la tabla (dtMedicamentos) para cargar la lista de medicamentos
                    List<oMedicamentos> listaMedicamentos = new List<oMedicamentos>();
                    foreach (DataRow row in dtMedicamentos.Rows)
                    {
                        var objMedicamentos = new oMedicamentos();

                        objMedicamentos.id = Int32.Parse(row["Id"].ToString());
                        objMedicamentos.nombre = row["Nombre"].ToString();
                        objMedicamentos.farmaceutica = row["Farmaceutica"].ToString();
                        objMedicamentos.cantidad = Int32.Parse(row["Cantidad"].ToString());
                        listaMedicamentos.Add(objMedicamentos);
                    }

                    // Almacenar la lista en la sesión
                    Session["listaMedicamentos"] = listaMedicamentos;

                    labelInfoCargada.InnerText = "Información cargada.";
                }
                catch (Exception ex)
                {
                    // Establecer el texto del mensaje de error
                    mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                    // Mostrar el cuadro de mensaje de error
                    divMensaje.Style["display"] = "block";
                }
            }
            protected void gridListaPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
            {


                try
                {

                    if (e.CommandName == "Select")
                    {
                        int rowIndex = Convert.ToInt32(e.CommandArgument);
                        GridViewRow selectedRow = gridListaEnfermedades.Rows[rowIndex];
                        int id = Int32.Parse(selectedRow.Cells[0].Text);


                        var listaEnfermedades = (List<oEnfermedades>)Session["listaEnfermedades"];

                        if (listaEnfermedades.Count > 0)
                        {
                            listaEnfermedades = listaEnfermedades.FindAll(p => p.id == id);
                            DataTable dt = GeneraTablaDinamica<oEnfermedades>(listaEnfermedades);
                            gridListaEnfermedades.DataSource = dt;
                            gridListaEnfermedades.DataBind();
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




        }
















    }





  