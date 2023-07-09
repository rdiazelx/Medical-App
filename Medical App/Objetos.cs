using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medical_App
{
    public class Objetos
    {
    }
    public class oTipoIdentificacion
    {
        public int id { get; set; }
        public string tipoIdentificacion { get; set; }

    }

    public class oGenero
    {
        public int id { get; set; }
        public string genero { get; set; }

    }
    public class oSucursales
    {
        public string lugar { get; set; }

        public string dirreccion { get; set; }

        public string telefono { get; set; }

        public string correo { get; set; }

    }

    public class oEnfermedades
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public string descripcion { get; set; }



    }

    public class oMedicamentos
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public string farmaceutica { get; set; }

        public int cantidad { get; set; }



    }
    public class oPersonas
    {
        public string id { get; set; }
        public string nombre { get; set; }

        public string apellido { get; set; }
        public string nombreCompleto => nombre + " " + apellido;

        public string genero { get; set; }
        public string estadoCivil { get; set; }

        public string nacionalidad { get; set; }

        public string provincia { get; set; }

        public string tipoIdentificacion { get; set; }

        public string numeroIdentificacion { get; set; }

        public DateTime fechaNacimiento { get; set; }

        public string especialidad { get; set; }

        public string identificacion { get; set; }
        
        public string telefono { get; set; }

        public string correo { get; set; }




    }


    public class oMedicos
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nombreCompleto => nombre + " " + apellido;
        public string tipoIdentificacion { get; set; }
        public string identificacion { get; set; }
        public string genero { get; set; }
        public string estadoCivil { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string especialidad { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }

    }

    public class oExpediente
    {

        public int idPaciente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public DateTime fechaCita { get; set; }

        public string medico { get; set; }

        public string especialidad { get; set; }
        public string enfermedad { get; set; }

        public string medicamentos { get; set; }

        public string indicaciones { get; set; }

        public DateTime fechaPrescripcion { get; set; }

        public string sucursal { get; set; }
    }

    public class oCitas
    {

        public int IdCita { get; set; }
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public string Sucursal { get; set; }
        public DateTime Fecha { get; set; }
        public string Especialidad { get; set; }
    
        public string Enfermedad { get; set; }
        public string Medicamentos { get; set; }

        public string Indicaciones { get; set; }

        public DateTime fechaPrescripcion { get; set; }

    }



}