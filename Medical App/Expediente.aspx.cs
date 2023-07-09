using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Medical_App
{
    public partial class Expediente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //cargarExpediente();

            if (!IsPostBack)
            {
                if (Request.QueryString["nombre"] != null)
                {
                    string nombre = Request.QueryString["nombre"];
                    string apellido = Request.QueryString["apellido"];
                    string fechaNacimiento = Request.QueryString["fechaNacimiento"];
                    string medico = Request.QueryString["medico"];
                    string especialidadMedico = Request.QueryString["especialidadMedico"];
                    string fechaCita = Request.QueryString["fechaCita"];
                    string medicamentos = Request.QueryString["medicamentos"];
                    string indicaciones = Request.QueryString["indicaciones"];
                    string fechaPrescripcion = Request.QueryString["fechaPrescripcion"];
                    string sucursal = Request.QueryString["sucursal"];

                    // Agregamos los valores a los lables
                    lblPaciente.Text = nombre + " " + apellido;
                    lblDOB.Text = fechaNacimiento;
                    lblNombreMedico.Text = medico;
                    lblEspecialidad.Text = especialidadMedico;
                    lblFechaConsulta.Text = fechaCita;
                    lblMedicamentos.Text = medicamentos;
                    lblIndicaciones.Text = indicaciones;
                    lblFechaPrescripcion.Text = fechaPrescripcion;
                    lblSucursal.Text = sucursal;


                    //Guardamos los valores en la Session

                    Session["NombreCompleto"] = nombre + " " + apellido;
                    Session["FechaNacimiento"] = fechaNacimiento.ToString();
                    Session["NombreMedico"] = medico;
                    Session["Especialidad"] = especialidadMedico;
                    Session["FechaConsulta"] = fechaCita;
                    Session["Medicamentos"] = medicamentos;
                    Session["Indicaciones"] = indicaciones;
                    Session["FechaPrescripcion"] = fechaPrescripcion;
                    Session["Sucursal"] = sucursal;



                }
            }
        }


        /*
        public void cargarExpediente()
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


                string hojaExpediente = "Expedientes$";

                //crear un datatable
                DataTable dtExpediente = new DataTable();
              

                //obtiene la data del la hoja pacientes
                cmdExcel.CommandText = "Select * from [" + hojaExpediente + "]";
                adapterExcel.SelectCommand = cmdExcel;
                adapterExcel.Fill(dtExpediente);

                connExcel.Close();


                // Set the values from the DataTable to the ASP labels
                string nombre = dtExpediente.Rows[0]["nombre"].ToString();
                string apellido = dtExpediente.Rows[0]["apellido"].ToString();
                lblPaciente.Text = nombre + " " + apellido;
                lblDOB.Text = dtExpediente.Rows[0]["fechaNacimiento"].ToString();


                lblNombreMedico.Text = dtExpediente.Rows[0]["medico"].ToString();
                lblEspecialidad.Text = dtExpediente.Rows[0]["especialidadMedico"].ToString();
                lblFechaConsulta.Text = dtExpediente.Rows[0]["fechaCita"].ToString();
                
                
                lblMedicamentos.Text = dtExpediente.Rows[0]["Medicamentos"].ToString();
                lblIndicaciones.Text = dtExpediente.Rows[0]["Indicaciones"].ToString();
                lblFechaPrescripcion.Text = dtExpediente.Rows[0]["fechaPrescripcion"].ToString();
                lblSucursal.Text = dtExpediente.Rows[0]["Sucursal"].ToString();


                // Store values in session variables
                Session["NombreCompleto"] = nombre + " " + apellido;
                Session["FechaNacimiento"] = dtExpediente.Rows[0]["fechaNacimiento"].ToString();
                Session["NombreMedico"] = dtExpediente.Rows[0]["medico"].ToString();
                Session["Especialidad"] = dtExpediente.Rows[0]["especialidadMedico"].ToString();
                Session["FechaConsulta"] = dtExpediente.Rows[0]["fechaCita"].ToString();
                Session["Medicamentos"] = dtExpediente.Rows[0]["Medicamentos"].ToString();
                Session["Indicaciones"] = dtExpediente.Rows[0]["Indicaciones"].ToString();
                Session["FechaPrescripcion"] = dtExpediente.Rows[0]["fechaPrescripcion"].ToString();
                Session["Sucursal"] = dtExpediente.Rows[0]["Sucursal"].ToString();



            }
            catch (Exception ex)
            {
               
                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
               divMensaje.Style["display"] = "block";

            }
            
            }

        */

         protected void btnDescargarXML_Click1(object sender, EventArgs e)
        {
            try
            {
                // Create a DataTable to hold the session values
                DataTable dt = new DataTable("Data");
                dt.Columns.Add("NombreCompleto");
                dt.Columns.Add("FechaNacimiento");
                dt.Columns.Add("NombreMedico");
                dt.Columns.Add("Especialidad");
                dt.Columns.Add("FechaConsulta");
                dt.Columns.Add("Medicamentos");
                dt.Columns.Add("Indicaciones");
                dt.Columns.Add("FechaPrescripcion");
                dt.Columns.Add("Sucursal");

                // Populate the DataTable with session values
                DataRow row = dt.NewRow();
                row["NombreCompleto"] = Session["NombreCompleto"];
                row["FechaNacimiento"] = Session["FechaNacimiento"];
                row["NombreMedico"] = Session["NombreMedico"];
                row["Especialidad"] = Session["Especialidad"];
                row["FechaConsulta"] = Session["FechaConsulta"];
                row["Medicamentos"] = Session["Medicamentos"];
                row["Indicaciones"] = Session["Indicaciones"];
                row["FechaPrescripcion"] = Session["FechaPrescripcion"];
                row["Sucursal"] = Session["Sucursal"];
                dt.Rows.Add(row);

                // Create a DataSet and add the DataTable
                DataSet ds = new DataSet("SessionData");
                ds.Tables.Add(dt);

                // Generate the XML file
                string random = DateTime.Now.ToFileTime().ToString();
                string folder = "~/Uploads/";
                string fileName = random + "archivo.xml";
                string ruta = Server.MapPath(folder + fileName);
                ds.WriteXml(ruta);

                // Download the XML file
                Response.Clear();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.ContentType = "application/xml";
                Response.WriteFile(ruta);
                Response.End();

            }
            catch (Exception ex)
            {
                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                divMensaje.Style["display"] = "block";

            }

        }
    }

}
