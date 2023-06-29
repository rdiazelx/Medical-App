using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medical_App
{
    public class Objetos
    {
    }
    public class oSucursales
    {
        public string lugar { get; set; }

        public string dirreccion { get; set; }

        public int telefono { get; set; }

        public string correo { get; set; }

    }

    public class oEnfermedades
    {
        public int id { get; set; }
        public int IdPaciente { get; set; }
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
        public int id { get; set; }
        public string nombre { get; set; }

        public string apellido { get; set; }

        public string genero { get; set; }
        public string estadoCivil { get; set; }

        public string nacionalidad { get; set; }

        public string provincia { get; set; }

        public string tipoIdentificacion { get; set; }

        public int numeroIdentificacion { get; set; }

        public DateTime fechaNacimiento { get; set; }

        public string especialidad { get; set; }

        public int identificacion { get; set; }
        
        public int telefono { get; set; }

        public string correo { get; set; }




    }


    public class oMedicos
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string tipoIdentificacion { get; set; }
        public int identificacion { get; set; }
        public string genero { get; set; }
        public string estadoCivil { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string especialidad { get; set; }
        public int telefono { get; set; }
        public string correo { get; set; }




    }




}