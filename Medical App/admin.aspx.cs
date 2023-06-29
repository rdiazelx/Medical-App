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

namespace Medical_App
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = Server.MapPath("~/Uploads/BaseDeDatos.xlsx"); // Ruta del archivo en la carpeta "Uploads"

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
                //string hojaEnfermedades = "Enfermedades$";

                //crear un datatable
                DataTable dtPacientes = new DataTable();
                DataTable dtEnfermedades = new DataTable();

                //obtiene la data del la hoja pacientes
                cmdExcel.CommandText = "Select * from [" + hojaPacientes + "]";
                adapterExcel.SelectCommand = cmdExcel;
                adapterExcel.Fill(dtPacientes);

                ////obtiene la data del la hoja Enfermedades
                //cmdExcel.CommandText = "Select * from [" + hojaEnfermedades + "]";
                //adapterExcel.SelectCommand = cmdExcel;
                //adapterExcel.Fill(dtEnfermedades);


                connExcel.Close();

                //mis datos están en el dt

                gridLista.DataSource = dtPacientes;
                gridLista.DataBind();


                foreach (GridViewRow row in gridLista.Rows)
                {
                    row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridLista, "Select$" + row.RowIndex);
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
                        objPacientes.nombre = dtPacientes.Rows[i]["Nombre"].ToString();
                        objPacientes.apellido = dtPacientes.Rows[i]["Apellido"].ToString();
                        objPacientes.tipoIdentificacion = dtPacientes.Rows[i]["Tipo de identificación"].ToString();

                        int identificacion;
                        if (Int32.TryParse(dtPacientes.Rows[i]["Identificación"].ToString(), out identificacion))
                        {
                            objPacientes.identificacion = identificacion;
                        }
                        else
                        {
                            // Manejar el caso cuando la conversión falla, por ejemplo, asignar un valor predeterminado o mostrar un mensaje de error.
                            // objPacientes.identificacion = valorPorDefecto;
                        }

                        objPacientes.genero = dtPacientes.Rows[i]["Género"].ToString();
                        objPacientes.estadoCivil = dtPacientes.Rows[i]["Estado civil"].ToString();
                        objPacientes.fechaNacimiento = DateTime.Parse(dtPacientes.Rows[i]["Fecha de nacimiento"].ToString());
                        objPacientes.nacionalidad = dtPacientes.Rows[i]["Nacionalidad"].ToString();
                        objPacientes.provincia = dtPacientes.Rows[i]["Provincia"].ToString();

                        int telefono;
                        if (Int32.TryParse(dtPacientes.Rows[i]["Teléfono"].ToString(), out telefono))
                        {
                            objPacientes.telefono = telefono;
                        }
                        else
                        {
                            // Manejar el caso cuando la conversión falla.
                            // objPacientes.telefono = valorPorDefecto;
                        }

                        objPacientes.correo = dtPacientes.Rows[i]["correo"].ToString();

                        listaPacientes.Add(objPacientes);
                    }


                    Session["listaPacientes"] = listaPacientes;
                }
                ////Recorrer la tabla (dt) para cargar la lista de enfermedades
                //var listaEnfermedades = new List<oEnfermedades>();

                //if (dtEnfermedades.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtEnfermedades.Rows.Count; i++)
                //    {
                //        var objEnfermedades = new oEnfermedades();
                //        objEnfermedades.id = Int32.Parse(dtEnfermedades.Rows[i]["Id"].ToString());
                //        objEnfermedades.nombre = dtEnfermedades.Rows[i]["Nombre de enfermedad"].ToString();
                //        objEnfermedades.descripcion = dtEnfermedades.Rows[i]["Descripcion"].ToString();


                //        listaEnfermedades.Add(objEnfermedades);
                //    }

                //    Session["listaEnfermedades"] = listaEnfermedades;
                //}


            }
            catch (Exception ex)
            {
                // Establecer el texto del mensaje
                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                // Mostrar el cuadro de mensaje
                divMensaje.Style["display"] = "block";
            }

        }
               
        protected void bSucursal_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = Server.MapPath("~/Uploads/BaseDeDatos.xlsx"); // Ruta del archivo en la carpeta "Uploads"


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

                gridLista.DataSource = dtSucursales;
                gridLista.DataBind();



                //Recorrer la tabla (dt) para cargar la lista de personas
                var listaSucursales = new List<oSucursales>();

                if (dtSucursales.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSucursales.Rows.Count; i++)
                    {
                        var objSucursales = new oSucursales();
                        objSucursales.lugar = dtSucursales.Rows[i]["Sucursal"].ToString();
                        objSucursales.dirreccion = dtSucursales.Rows[i]["Dirección"].ToString();

                        int telefono;
                        if (Int32.TryParse(dtSucursales.Rows[i]["Teléfono"].ToString(), out telefono))
                        {
                            objSucursales.telefono = telefono;
                        }
                        else
                        {
                            // Manejar el caso cuando la conversión falla.
                            // objSucursales.telefono = valorPorDefecto;
                        }

                        objSucursales.correo = dtSucursales.Rows[i]["Correo electrónico"].ToString();

                        listaSucursales.Add(objSucursales);
                    }

                    Session["listaSucursales"] = listaSucursales;
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
}
 
       