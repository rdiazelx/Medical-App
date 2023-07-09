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
using DocumentFormat.OpenXml.Drawing;

namespace Medical_App
{
    public partial class admin : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarExpediente();


        }



        private void cargarPacientes()
        {
            try
            {

                btnAñadirSucu.Style["display"] = "none";
                btnAñadirMeds.Style["display"] = "none";
                btnAñadirMedico.Style["display"] = "none";
                btnAñadirEnfe.Style["display"] = "none";
                btnAñadirUsu.Style["display"] = "none";
                btnNuevoPaciente.Style["display"] = "block";


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

                //crear un datatable
                DataTable dtPacientes = new DataTable();
                DataTable dtEnfermedades = new DataTable();

                //obtiene la data del la hoja pacientes
                cmdExcel.CommandText = "Select * from [" + hojaPacientes + "]";
                adapterExcel.SelectCommand = cmdExcel;
                adapterExcel.Fill(dtPacientes);

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
                        objPacientes.id = dtPacientes.Rows[i]["Id"].ToString();
                        objPacientes.nombre = dtPacientes.Rows[i]["Nombre"].ToString();
                        objPacientes.apellido = dtPacientes.Rows[i]["Apellido"].ToString();
                        objPacientes.tipoIdentificacion = dtPacientes.Rows[i]["Tipo de identificación"].ToString();
                        objPacientes.identificacion = dtPacientes.Rows[i]["Identificación"].ToString();
                        objPacientes.telefono = dtPacientes.Rows[i]["Teléfono"].ToString();
                        objPacientes.genero = dtPacientes.Rows[i]["Género"].ToString();
                        objPacientes.estadoCivil = dtPacientes.Rows[i]["Estado civil"].ToString();
                        DateTime fechaNacimiento;
                        if (DateTime.TryParse(dtPacientes.Rows[i]["Fecha de nacimiento"].ToString(), out fechaNacimiento))
                        {
                            objPacientes.fechaNacimiento = fechaNacimiento;
                        }
                        objPacientes.nacionalidad = dtPacientes.Rows[i]["Nacionalidad"].ToString();
                        objPacientes.provincia = dtPacientes.Rows[i]["Provincia"].ToString();
                        objPacientes.correo = dtPacientes.Rows[i]["correo"].ToString();

                        listaPacientes.Add(objPacientes);
                    }


                    Session["listaPacientes"] = listaPacientes;
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
        private void cargarSucursales()
        {
            try
            {
                btnAñadirSucu.Style["display"] = "block";
                btnAñadirMeds.Style["display"] = "none";
                btnAñadirMedico.Style["display"] = "none";
                btnAñadirEnfe.Style["display"] = "none";
                btnAñadirUsu.Style["display"] = "none";
                btnNuevoPaciente.Style["display"] = "none";

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
                        objSucursales.telefono = dtSucursales.Rows[i]["Teléfono"].ToString();
                        objSucursales.correo = dtSucursales.Rows[i]["Correo"].ToString();

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
  
        private void cargarMedicos()
        {
            try
            {
                btnAñadirMedico.Style["display"] = "block";
                btnAñadirMeds.Style["display"] = "none";
                btnAñadirEnfe.Style["display"] = "none";
                btnAñadirSucu.Style["display"] = "none";
                btnAñadirUsu.Style["display"] = "none";
                btnNuevoPaciente.Style["display"] = "none";

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


                string hojaMedicos = "Medicos$";

                //crear un datatable
                DataTable dtMedicos = new DataTable();
                DataTable dtEnfermedades = new DataTable();

                //obtiene la data del la hoja pacientes
                cmdExcel.CommandText = "Select * from [" + hojaMedicos + "]";
                adapterExcel.SelectCommand = cmdExcel;
                adapterExcel.Fill(dtMedicos);

                connExcel.Close();

                //mis datos están en el dt

                gridLista.DataSource = dtMedicos;
                gridLista.DataBind();


                /*foreach (GridViewRow row in gridLista.Rows)
                {
                    row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridLista, "Select$" + row.RowIndex);
                }
                */

                //listado de Pacientes
                //Recorrer la tabla (dt) para cargar la lista de pacientes
                var listaMedicos = new List<oMedicos>();

                if (dtMedicos.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMedicos.Rows.Count; i++)
                    {
                        var objMedicos = new oMedicos();
                        objMedicos.id = Int32.Parse(dtMedicos.Rows[i]["Id"].ToString());
                        objMedicos.nombre = dtMedicos.Rows[i]["Nombre"].ToString();
                        objMedicos.apellido = dtMedicos.Rows[i]["Apellido"].ToString();
                        objMedicos.tipoIdentificacion = dtMedicos.Rows[i]["Tipo de identificación"].ToString();
                        objMedicos.identificacion = dtMedicos.Rows[i]["Identificación"].ToString();
                        objMedicos.telefono = dtMedicos.Rows[i]["Teléfono"].ToString();
                        objMedicos.genero = dtMedicos.Rows[i]["Género"].ToString();
                        objMedicos.estadoCivil = dtMedicos.Rows[i]["Estado civil"].ToString();
                        objMedicos.fechaNacimiento = DateTime.Parse(dtMedicos.Rows[i]["Fecha de nacimiento"].ToString());
                        objMedicos.especialidad = dtMedicos.Rows[i]["Especialidad"].ToString();
                        objMedicos.correo = dtMedicos.Rows[i]["correo"].ToString();

                        listaMedicos.Add(objMedicos);
                    }


                    Session["listaMedicos"] = listaMedicos;
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

        private void cargarMedicamentos()
        {
            try
            {
                btnAñadirMeds.Style["display"] = "block";
                btnAñadirEnfe.Style["display"] = "none";
                btnAñadirMedico.Style["display"] = "none";
                btnAñadirSucu.Style["display"] = "none";
                btnAñadirUsu.Style["display"] = "none";
                btnNuevoPaciente.Style["display"] = "none";


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

                string hojaMedicamentos = "Medicamentos$";

                //crear un datatable
                DataTable dtMedicamentos = new DataTable();

                //obtiene la data del la hoja medicamentos
                cmdExcel.CommandText = "Select * from [" + hojaMedicamentos + "]";
                adapterExcel.SelectCommand = cmdExcel;
                adapterExcel.Fill(dtMedicamentos);

                connExcel.Close();

                //mis datos están en el dt

                gridLista.DataSource = dtMedicamentos;
                gridLista.DataBind();


                // Recorrer la tabla (dtMedicamentos) para cargar la lista de medicamentos
                List<oMedicamentos> listaMedicamentos = new List<oMedicamentos>();
                foreach (DataRow row in dtMedicamentos.Rows)
                {
                    var objMedicamentos = new oMedicamentos();

                    // Parsing the 'id' field
                    int id;
                    if (int.TryParse(row["Id"].ToString(), out id))
                    {
                        objMedicamentos.id = id;
                    }
                    else
                    {
                        // Handle the parsing error
                        // For example, you can assign a default value or log an error message
                        objMedicamentos.id = 0; // Default value or appropriate error handling
                    }

                    objMedicamentos.nombre = row["Nombre"].ToString();
                    objMedicamentos.farmaceutica = row["Farmacéutica"].ToString();

                    // Parsing the 'cantidad' field
                    int cantidad;
                    if (int.TryParse(row["Cantidad"].ToString(), out cantidad))
                    {
                        objMedicamentos.cantidad = cantidad;
                    }
                    else
                    {
                        // Handle the parsing error
                        // For example, you can assign a default value or log an error message
                        objMedicamentos.cantidad = 0; // Default value or appropriate error handling
                    }

                    // Almacenar la lista en la sesión
                    Session["listaMedicamentos"] = listaMedicamentos;


                }
            }
            catch (Exception ex)
            {
                // Establecer el texto del mensaje de error
                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                // Mostrar el cuadro de mensaje de error
                divMensaje.Style["display"] = "block";
            }

        }
        
        private void cargarEnfermedades()
        {
            try
            {
                btnAñadirEnfe.Style["display"] = "block";
                btnAñadirMeds.Style["display"] = "none";
                btnAñadirMedico.Style["display"] = "none";
                btnAñadirSucu.Style["display"] = "none";
                btnAñadirUsu.Style["display"] = "none";
                btnNuevoPaciente.Style["display"] = "none";


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





                string hojaEnfermedades = "Enfermedades$";


                //crear un datatable
                DataTable dtEnfermedades = new DataTable();


                //obtiene la data del la hoja Enfermedades
                cmdExcel.CommandText = "Select * from [" + hojaEnfermedades + "]";
                adapterExcel.SelectCommand = cmdExcel;
                adapterExcel.Fill(dtEnfermedades);





                connExcel.Close();

                //mis datos están en el dt

                gridLista.DataSource = dtEnfermedades;
                gridLista.DataBind();


                //Recorrer la tabla (dt) para cargar la lista de enfermedades
                var listaEnfermedades = new List<oEnfermedades>();

                if (dtEnfermedades.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEnfermedades.Rows.Count; i++)
                    {
                        var objEnfermedades = new oEnfermedades();

                        // Parsing the 'id' field
                        int Id;
                        if (int.TryParse(dtEnfermedades.Rows[i]["Id"].ToString(), out Id))
                        {
                            objEnfermedades.id = Id;
                        }
                        else
                        {
                            // Handle the parsing error
                            // For example, you can assign a default value or log an error message
                            objEnfermedades.id = 0; // Default value or appropriate error handling
                        }

                        objEnfermedades.nombre = dtEnfermedades.Rows[i]["Enfermedad"].ToString();
                        objEnfermedades.descripcion = dtEnfermedades.Rows[i]["Descripcion"].ToString();

                        listaEnfermedades.Add(objEnfermedades);
                    }


                    Session["listaEnfermedades"] = listaEnfermedades;
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
       
        private void cargarUsuarios()
        {
            try
            {
                btnAñadirUsu.Style["display"] = "block";
                btnAñadirMeds.Style["display"] = "none";
                btnAñadirMedico.Style["display"] = "none";
                btnAñadirSucu.Style["display"] = "none";
                btnAñadirEnfe.Style["display"] = "none";
                btnNuevoPaciente.Style["display"] = "none";


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


                string hojaUsuarios = "Usuarios$";

                //crear un datatable
                DataTable dtUsuarios = new DataTable();


                //obtiene la data del la hoja pacientes
                cmdExcel.CommandText = "SELECT Id, Usuario, Rol FROM [" + hojaUsuarios + "]";
                adapterExcel.SelectCommand = cmdExcel;
                adapterExcel.Fill(dtUsuarios);

                connExcel.Close();

                //mis datos están en el dt

                gridLista.DataSource = dtUsuarios;
                gridLista.DataBind();


                //listado de Pacientes
                //Recorrer la tabla (dt) para cargar la lista de pacientes
                var listaUsuarios = new List<oUsuarios>();

                if (dtUsuarios.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUsuarios.Rows.Count; i++)
                    {
                        var objUsuarios = new oUsuarios();

                        objUsuarios.Id = Int32.Parse(dtUsuarios.Rows[i]["Id"].ToString());
                        objUsuarios.usuario = dtUsuarios.Rows[i]["Usuario"].ToString();
                        objUsuarios.rol = dtUsuarios.Rows[i]["rol"].ToString();

                        listaUsuarios.Add(objUsuarios);
                    }


                    Session["listaUsuarios"] = listaUsuarios;
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

        public void cargarExpediente()
        {
            try
            {

                btnAñadirUsu.Style["display"] = "none";
                btnAñadirMeds.Style["display"] = "none";
                btnAñadirMedico.Style["display"] = "none";
                btnAñadirSucu.Style["display"] = "none";
                btnAñadirEnfe.Style["display"] = "none";
                btnNuevoPaciente.Style["display"] = "none";

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

                //mis datos están en el dt

                gridLista.DataSource = dtExpediente;
                //gridLista.DataBind();


                var listaExpediente = new List<oExpediente>();

                if (dtExpediente.Rows.Count > 0)
                {
                    for (int i = 0; i < dtExpediente.Rows.Count; i++)
                    {
                        var objExpediente = new oExpediente();

                        objExpediente.idPaciente = Int32.Parse(dtExpediente.Rows[i]["idPaciente"].ToString());
                        objExpediente.nombre = dtExpediente.Rows[i]["nombre"].ToString();
                        objExpediente.apellido = dtExpediente.Rows[i]["apellido"].ToString();
                        DateTime fechaNacimiento;
                        if (DateTime.TryParse(dtExpediente.Rows[i]["fechaNacimiento"].ToString(), out fechaNacimiento))
                        {
                            objExpediente.fechaNacimiento = fechaNacimiento;
                        }
                        DateTime fechaCita;
                        if (DateTime.TryParse(dtExpediente.Rows[i]["fechaCita"].ToString(), out fechaCita))
                        {
                            objExpediente.fechaNacimiento = fechaCita;
                        }
                        objExpediente.medico = dtExpediente.Rows[i]["medico"].ToString();
                        objExpediente.especialidad = dtExpediente.Rows[i]["especialidad"].ToString();
                        objExpediente.medicamentos = dtExpediente.Rows[i]["medicamentos"].ToString();
                        objExpediente.indicaciones = dtExpediente.Rows[i]["indicaciones"].ToString();
                        DateTime fechaPrescripcion;
                        if (DateTime.TryParse(dtExpediente.Rows[i]["fechaPrescripcion"].ToString(), out fechaPrescripcion))
                        {
                            objExpediente.fechaNacimiento = fechaPrescripcion;
                        }
                        objExpediente.sucursal = dtExpediente.Rows[i]["sucursal"].ToString();




                        listaExpediente.Add(objExpediente);
                    }


                    Session["listaExpediente"] = listaExpediente;
                }



            }catch (Exception ex)
            {

                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                divMensaje.Style["display"] = "block";

            }
        }




        //funciones click
        protected void bUsu_Click(object sender, EventArgs e)
        {
            cargarUsuarios();

        }
        protected void bEnfermedades_Click(object sender, EventArgs e)
        {
            cargarEnfermedades();
        }

        protected void bMedicamentos_Click(object sender, EventArgs e)
        {
            cargarMedicamentos();
        }

        protected void bMedicos_Click(object sender, EventArgs e)
        {
           cargarMedicos();
        }

        protected void bSucursal_Click(object sender, EventArgs e)
        {
            cargarSucursales();
        }

        protected void bPaciente_Click(object sender, EventArgs e)
        {
            cargarPacientes();

        }

        //funciones escribir en archivo


        protected void bAgregarUsu_Click(object sender, EventArgs e)
        {
            try
            {
                Random random = new Random();
                int ID = random.Next(10000, 100000);
                string Usuario = txtCorreo.Text;
                string Password = txtPass.Text;
                string Rol = txtRol.Text;
                /*string fechaNacimiento = txtFechaNacimiento.Text;
                string estadoCivil = dpEstadoCivil.SelectedItem.Text;
                string tipoIdentificacion = dptipoIdentificacion.SelectedItem.Text;
                string genero = dpGenero.SelectedItem.Text;*/

                txtCorreo.Text = string.Empty;
                txtPass.Text = string.Empty;
                txtRol.Text = string.Empty;

                if (!string.IsNullOrEmpty(Usuario) || !string.IsNullOrEmpty(Password))
                {


                    string ruta = Server.MapPath("~/Uploads/BaseDeDatos.xlsx");

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


                    string hojaUsu = "Usuarios$";
                    string consulta = "INSERT INTO [" + hojaUsu + "]  VALUES (@ID, @Usuario, @Password, @Rol)";

                    cmdExcel.Parameters.AddWithValue("@ID", ID);
                    cmdExcel.Parameters.AddWithValue("@Usuario", Usuario);
                    cmdExcel.Parameters.AddWithValue("@Password", Password);
                    cmdExcel.Parameters.AddWithValue("@Rol", Rol);

                    cmdExcel.CommandText = consulta;
                    cmdExcel.ExecuteNonQuery();
                    //adapterExcel.SelectCommand = cmdExcel;


                    connExcel.Close();
                    divAgregarUsu.Style["display"] = "none";
                    cargarUsuarios();

                }
                else
                {
                    // Establecer el texto del mensaje
                    //mensajeTexto.InnerText = "El nombre o la identificación no puede ser vacío.";
                    // Mostrar el cuadro de mensaje
                    //divMensaje.Style["display"] = "block";
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
        protected void bAgregarMedic_Click(object sender, EventArgs e)
        {
            try
            {

                Random random = new Random();
                int ID = random.Next(10000, 100000);
                string Nombre = txtNom_Medic.Text;
                string Apellido = txtApell_Medic.Text;
                string TipoIdent = txtTipoIdent.Text;
                string Identif = txtIdenti.Text;
                string Genero = txtGenero.Text;
                string EstadCivil = txtEstadoCivil.Text;
                string FechNac = txtFechaNacimiento.Text;
                string Espec = txtEspec.Text;
                string Telef = txtTelef.Text;
                string Correo_Med = txtCorreo_Med.Text;
                



                if ( ID != 0 || !string.IsNullOrEmpty(Nombre) || !string.IsNullOrEmpty(Apellido))
                {


                    string ruta = Server.MapPath("~/Uploads/BaseDeDatos.xlsx");

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


                    string hojaUsu = "Medicos$";
                    string consulta = "INSERT INTO [" + hojaUsu + "]  VALUES (@ID, @Nombre, @Apellido, @TipoIden, @Iden, @Genero, @EstadCiv, @FechaNac, @Espec, @Telef, @Correo)";

                    cmdExcel.Parameters.AddWithValue("@ID", ID);
                    cmdExcel.Parameters.AddWithValue("@Nombre", Nombre);
                    cmdExcel.Parameters.AddWithValue("@Apellido", Apellido);
                    cmdExcel.Parameters.AddWithValue("@TipoIden", TipoIdent);
                    cmdExcel.Parameters.AddWithValue("@Iden", Identif);
                    cmdExcel.Parameters.AddWithValue("@Genero", Genero);
                    cmdExcel.Parameters.AddWithValue("@EstadCiv", EstadCivil);
                    cmdExcel.Parameters.AddWithValue("@FechaNac", DateTime.Parse(FechNac));
                    cmdExcel.Parameters.AddWithValue("@Espec", Espec);
                    cmdExcel.Parameters.AddWithValue("@Telef", Telef);
                    cmdExcel.Parameters.AddWithValue("@Correo", Correo_Med);

                    cmdExcel.CommandText = consulta;
                    cmdExcel.ExecuteNonQuery();
                    //adapterExcel.SelectCommand = cmdExcel;
                    connExcel.Close();


                    divAgregarMedico.Style["display"] = "none";

                    mensajeTexto2.InnerText = "Medicamento Agregado";
                    divMensaje2.Style["display"] = "block";

                    string script = @"<script>
                    setTimeout(function(){
                        document.getElementById('" + divMensaje2.ClientID + @"').style.display = 'none';
                    }, 1000);
                </script>";

                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", script);


                    cargarMedicos();

                }
                else
                {
                    // Establecer el texto del mensaje
                    mensajeTexto.InnerText = "El nombre o la identificación no puede ser vacío.";
                    // Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
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

        protected void bAgregarMeds_Click(object sender, EventArgs e)
        {
            try
            {

                Random random = new Random();
                int ID = random.Next(10000, 100000);
                string Nombre = txtNombre_Meds.Text;
                string CasaFarma = txt_CasaFarma.Text;
                int Cantidad = Int32.Parse(txtCantidad.Text);


                if (ID != 0 || !string.IsNullOrEmpty(Nombre) || !string.IsNullOrEmpty(CasaFarma))
                {


                    string ruta = Server.MapPath("~/Uploads/BaseDeDatos.xlsx");

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


                    string hojaUsu = "Medicamentos$";
                    string consulta = "INSERT INTO [" + hojaUsu + "]  VALUES (@id, @Nombre, @Farmacéutica, @Cantidad)";

                    cmdExcel.Parameters.AddWithValue("@ID", ID);
                    cmdExcel.Parameters.AddWithValue("@Nombre", Nombre);
                    cmdExcel.Parameters.AddWithValue("@Farmacéutica", CasaFarma);
                    cmdExcel.Parameters.AddWithValue("@Cantidad", Cantidad);

                    cmdExcel.CommandText = consulta;
                    cmdExcel.ExecuteNonQuery();
                    //adapterExcel.SelectCommand = cmdExcel;
                    connExcel.Close();

                    divAgregarMeds.Style["display"] = "none";
                    cargarMedicamentos();


                }
                else
                {
                    // Establecer el texto del mensaje
                    mensajeTexto.InnerText = "El nombre o la identificación no puede ser vacío.";
                    // Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
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

        protected void bAgregarSucu_Click(object sender, EventArgs e)
        {
            try
            {

                string Sucursal = txtLugar_Sucu.Text;
                string Direccion = txtDireccion.Text;
                string Telefono = txtTelefono.Text;
                string Correo = txtCorreo_Sucu.Text;


                if (!string.IsNullOrEmpty(Sucursal) || !string.IsNullOrEmpty(Direccion))
                {


                    string ruta = Server.MapPath("~/Uploads/BaseDeDatos.xlsx");

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


                    string hojaUsu = "Sucursales$";
                    string consulta = "INSERT INTO [" + hojaUsu + "]  VALUES (@Sucursal, @Dirección, @Teléfono, @Correo)";

                    cmdExcel.Parameters.AddWithValue("@Sucursal", Sucursal);
                    cmdExcel.Parameters.AddWithValue("@Dirección", Direccion);
                    cmdExcel.Parameters.AddWithValue("@Teléfono", Telefono);
                    cmdExcel.Parameters.AddWithValue("@Correo", Correo);

                    cmdExcel.CommandText = consulta;
                    cmdExcel.ExecuteNonQuery();
                    //adapterExcel.SelectCommand = cmdExcel;
                    connExcel.Close();

                    divAgregarSucu.Style["display"] = "none";
                    cargarSucursales();

                }
                else
                {
                    // Establecer el texto del mensaje
                    mensajeTexto.InnerText = "El nombre o la identificación no puede ser vacío.";
                    // Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
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

        protected void bAgregarEnfe_Click(object sender, EventArgs e)
        {
            try
            {

                Random random = new Random();
                int ID = random.Next(10000, 100000);
                string Nom_Enfe = txtNom_Enfe.Text;
                string Des_Enfe = txtDescri_Enfe.Text;
                


                if (!string.IsNullOrEmpty(Nom_Enfe) || !string.IsNullOrEmpty(Des_Enfe))
                {


                    string ruta = Server.MapPath("~/Uploads/BaseDeDatos.xlsx");

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


                    string hojaUsu = "Enfermedades$";
                    string consulta = "INSERT INTO [" + hojaUsu + "]  VALUES (@IdPaciente, @Enfermedad, @Descripcion)";

                    cmdExcel.Parameters.AddWithValue("@IdPaciente", ID);
                    cmdExcel.Parameters.AddWithValue("@Enfermedad", Nom_Enfe);
                    cmdExcel.Parameters.AddWithValue("@Descripcion", Des_Enfe);
                    

                    cmdExcel.CommandText = consulta;
                    cmdExcel.ExecuteNonQuery();
                    //adapterExcel.SelectCommand = cmdExcel;
                    connExcel.Close();

                    divAgregarEnfe.Style["display"] = "none";

                    /*
                    mensajeTexto2.InnerText = "Enfermedad agregada";
                    divMensaje2.Style["display"] = "block";

                    string script = @"<script>
                    setTimeout(function(){
                        document.getElementById('" + divMensaje2.ClientID + @"').style.display = 'none';
                    }, 1000);
                </script>";

                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", script);
                    */

                    cargarEnfermedades();
                    

                }
                else
                {
                    // Establecer el texto del mensaje
                    mensajeTexto.InnerText = "El nombre o la identificación no puede ser vacío.";
                    // Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
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


        //funciones mostrar

        protected void btnMostrarUsu_Click(object sender, EventArgs e)
        {
            divAgregarUsu.Style["display"] = "block";
            
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            divAgregarUsu.Style["display"] = "none";

        }
        protected void btnMostrarMedic_Click(object sender, EventArgs e)
        {
            divAgregarMedico.Style["display"] = "block";
        }
        protected void btnCerrarMedic_Click(object sender, EventArgs e)
        {
            divAgregarMedico.Style["display"] = "none";
        }
        protected void btnMostrarMeds_Click(object sender, EventArgs e)
        {
            divAgregarMeds.Style["display"] = "block";
        }
        protected void btnCerrarMeds_Click(object sender, EventArgs e)
        {
            divAgregarMeds.Style["display"] = "none";
        }
        protected void btnMostrarSucu_Click(object sender, EventArgs e)
        {
            divAgregarSucu.Style["display"] = "block";
        }
        protected void btnCerrarSucu_Click(object sender, EventArgs e)
        {
            divAgregarSucu.Style["display"] = "none";
        }
        protected void btnMostrarEnfe_Click(object sender, EventArgs e)
        {
            divAgregarEnfe.Style["display"] = "block";
        }
        protected void btnCerrarEnfe_Click(object sender, EventArgs e)
        {
            divAgregarEnfe.Style["display"] = "none";
        }

        protected void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pacientes.aspx");
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


                    var listExpediente = (List<oExpediente>)Session["listaExpediente"];

                    /*
                    if (listExpediente.Count > 0)
                    {
                        listExpediente = listExpediente.FindAll(p => p.idPaciente == id);
                        DataTable dt = GeneraTablaDinamica<oExpediente>(listExpediente);
                        gridLista.DataSource = dt;
                        gridLista.DataBind();
                    }

                }*/

                    if (listExpediente.Count > 0)
                    {
                        listExpediente = listExpediente.FindAll(p => p.idPaciente == id);

                        // Get the values you want to pass to Expediente.aspx
                        string nombre = listExpediente[0].nombre;
                        string apellido = listExpediente[0].apellido;
                        DateTime fechaNacimiento = listExpediente[0].fechaNacimiento;

                        string medico = listExpediente[0].medico;
                        string especialidadMedico = listExpediente[0].especialidad;
                        DateTime fechaCita = listExpediente[0].fechaCita;
                        string medicamentos = listExpediente[0].medicamentos;
                        string indicaciones = listExpediente[0].indicaciones;
                        DateTime fechaPrescripcion = listExpediente[0].fechaPrescripcion;
                        string sucursal = listExpediente[0].sucursal;

                        // Construct the query string
                        string queryString = $"nombre={nombre}&apellido={apellido}&fechaNacimiento={fechaNacimiento}&medico={medico}&especialidadMedico={especialidadMedico}&fechaCita={fechaCita}&medicamentos={medicamentos}&indicaciones={indicaciones}&fechaPrescripcion={fechaPrescripcion}&sucursal={sucursal}";

                        // Redirect to Expediente.aspx with the query string
                        Response.Redirect("Expediente.aspx?" + queryString);
                    }

                }
            }
            catch (Exception ex)
            {

                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
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

 
       