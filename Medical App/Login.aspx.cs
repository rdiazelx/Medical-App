using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Medical_App
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        Autenticacion();
        }

        private void Autenticacion()
        {
            try
            {

                var listaUsuarios = (List<oUsuarios>)Session["listaUsuarios"];

                //configurar el comportamiento del FileUpload
                string folder = Server.MapPath("~/Uploads/");
                string nombreArchivo = "BaseDeDatos.xlsx";
                string filePath = Path.Combine(folder, nombreArchivo);
                string fileContent = File.ReadAllText(filePath);


                //leer el archivo de excel
                string conectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

                conectionString = string.Format(conectionString, filePath, "Yes");

                OleDbConnection connExcel = new OleDbConnection(conectionString);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter adapterExcel = new OleDbDataAdapter();

                cmdExcel.Connection = connExcel;

                //abrir el archivo
                connExcel.Open();

                //si no se conoce el nombre de las hojas, se puede obtener dinamicamente
                DataTable dtExcel = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //string hojaExcel = dtExcel.Rows[0]["TABLE_NAME"].ToString();

                string hoja1 = "Usuarios$";

                //lectura de los datos
                DataTable dt = new DataTable();

                cmdExcel.CommandText = "Select * from [" + hoja1 + "]";
                adapterExcel.SelectCommand = cmdExcel;
                adapterExcel.Fill(dt);

                //cerramos la conexion
                connExcel.Close();

                //puede mostrar en el gridView
                //gridListaMaterias.DataSource = dt;
                //gridListaMaterias.DataBind();

              
                //recorrer la tabla para generar la lista
                var listaUsuario = new List<oUsuarios>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                    var objUsuario = new oUsuarios();

                        int idUsuarios;
                        if (int.TryParse(dt.Rows[i]["ID"].ToString(), out idUsuarios))
                        {
                            objUsuario.Id = idUsuarios;
                        }
                        else
                        {
                     
                        }
                        objUsuario.usuario = dt.Rows[i]["Usuario"].ToString();
                        objUsuario.password = dt.Rows[i]["Password"].ToString();
                        objUsuario.rol = dt.Rows[i]["Rol"].ToString();


                        listaUsuario.Add(objUsuario);

                    }

                }

                //agregarlo a la session             
                Session["listaUsuarios"] = listaUsuario;
           }
            catch (Exception ex)
            {
                // Establecer el texto del mensaje
                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                // Mostrar el cuadro de mensaje
                divMensaje.Style["display"] = "block";
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Validar login
            var listaUsuarios = (List<oUsuarios>)Session["listaUsuarios"];

            string user = txtLogin.Text;
            string password = txtPassword.Text;

            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password))
            {
                bool validCredentials = false;

                foreach (var usuario in listaUsuarios)
                {
                    if (usuario.usuario == user && usuario.password == password)
                    {
                        validCredentials = true;
                        break;
                    }
                }

                if (validCredentials)
                {
                    mensajeTexto.InnerText = "Bienvenido";
                    divMensaje.Style["display"] = "block";
                }
                else
                {
                    mensajeTexto.InnerText = "Usuario o contraseña incorrectos. Intente de nuevo.";
                    divMensaje.Style["display"] = "block";
                }
            }
            else
            {
             
                mensajeTexto.InnerText = "El usuario o la contraseña no pueden estar vacios.";
                divMensaje.Style["display"] = "block";
            }
        }







    }
}