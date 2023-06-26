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
    public partial class medicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargaDatos();
        }
        protected void BIngresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("tables.aspx");

        }
        private void CargaDatos()
        {
            try
            {
                string ruta = "C:\\HospitalABC\\Media\\Basedatos_Matricula.xlsx";


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

                string hojaMaterias = "Materias$";
                string hojaMatricula = "ListaEstudiantes$";
                //crear un datatable
                DataTable dtMaterias = new DataTable();
                DataTable dtMatricula = new DataTable();

                //obtiene la data del la hoja Materias
                cmdExcel.CommandText = "Select * from [" + hojaMaterias + "]";
                adapterExcel.SelectCommand = cmdExcel;
                adapterExcel.Fill(dtMaterias);

                //obtiene la data del la hoja
                cmdExcel.CommandText = "Select * from [" + hojaMatricula + "]";
                adapterExcel.SelectCommand = cmdExcel;
                adapterExcel.Fill(dtMatricula);

                connExcel.Close();

                //mis datos están en el dt

                gridListaMaterias.DataSource = dtMaterias;
                gridListaMaterias.DataBind();

                //asinga el evento a cada fila
                // Asigna el evento de selección a cada fila del GridView
                foreach (GridViewRow row in gridListaMaterias.Rows)
                {
                    row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridListaMaterias, "Select$" + row.RowIndex);
                }

                //Recorrer la tabla (dt) para cargar la lista de personas
                var listaMaterias = new List<oMateriasMed>();

                if (dtMaterias.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMaterias.Rows.Count; i++)
                    {
                        var obj = new oMateriasMed();
                        obj.IdGrupo = Int32.Parse(dtMaterias.Rows[i]["IdGrupo"].ToString());
                        obj.IdMateria = dtMaterias.Rows[i]["IdMateria"].ToString();
                        obj.NombreMateria = dtMaterias.Rows[i]["NombreMateria"].ToString();
                        obj.Grupo = Int32.Parse(dtMaterias.Rows[i]["Grupo"].ToString());
                        obj.Cantidad_Matriculados = Int32.Parse(dtMaterias.Rows[i]["Cantidad_Matriculados"].ToString());
                        obj.Horario = dtMaterias.Rows[i]["Horario"].ToString();

                        listaMaterias.Add(obj);
                    }

                    Session["listaMaterias"] = listaMaterias;
                }


                //listado de matricula
                //Recorrer la tabla (dt) para cargar la lista de personas
                var listaMatricula = new List<oMatriculaMed>();

                if (dtMatricula.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMatricula.Rows.Count; i++)
                    {
                        var obj = new oMatriculaMed();
                        obj.IdListado = Int32.Parse(dtMatricula.Rows[i]["IdListado"].ToString());
                        obj.IdGrupo = Int32.Parse(dtMatricula.Rows[i]["IdGrupo"].ToString());
                        obj.NombreEstudiante = dtMatricula.Rows[i]["NombreEstudiante"].ToString();
                        obj.Carrera = dtMatricula.Rows[i]["Carrera"].ToString();


                        listaMatricula.Add(obj);
                    }

                    Session["listaMatricula"] = listaMatricula;
                }
                labelInfoCargada.InnerText = "Información cargada.";

            }
            catch (Exception ex)
            {

                // Establecer el texto del mensaje
                // mensajeTexto.InnerText = "No fue posible cargar la información requerida.";
                // Mostrar el cuadro de mensaje
                // divMensaje.Style["display"] = "block";
            }

        }


        protected void gridListaMaterias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Select")
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gridListaMaterias.Rows[rowIndex];
                    int id = Int32.Parse(selectedRow.Cells[0].Text);


                    var listaMatricula = (List<oMatricula>)Session["listaMatricula"];

                    if (listaMatricula.Count > 0)
                    {
                        listaMatricula = listaMatricula.FindAll(p => p.IdGrupo == id);
                        DataTable dt = GeneraTablaDinamica<oMatricula>(listaMatricula);
                        gridMatricula.DataSource = dt;
                        gridMatricula.DataBind();
                    }

                }

            }
            catch (Exception ex)
            {
                // Establecer el texto del mensaje
                // mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                // Mostrar el cuadro de mensaje
                // divMensaje.Style["display"] = "block";
            }
        }



        //// Obtén el índice de la fila seleccionada
        //int indiceSeleccionado = gridListaMaterias.SelectedIndex;

        //// Accede a los valores de las celdas de la fila seleccionada
        //string valorColumna1 = gridListaMaterias.Rows[indiceSeleccionado].Cells[0].Text;

        //// Establecer el texto del mensaje
        //mensajeTexto.InnerText = "index" + indiceSeleccionado.ToString() + "; Datos:" + valorColumna1;
        //// Mostrar el cuadro de mensaje
        //divMensaje.Style["display"] = "block";

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


    public class oMateriasMed
    {

        public int IdGrupo { get; set; }
        public string IdMateria { get; set; }
        public string NombreMateria { get; set; }
        public int Grupo { get; set; }
        public int Cantidad_Matriculados { get; set; }
        public string Horario { get; set; }
    }

    public class oMatriculaMed
    {
        public int IdListado { get; set; }
        public int IdGrupo { get; set; }
        public string NombreEstudiante { get; set; }
        public string Carrera { get; set; }
    }
}
