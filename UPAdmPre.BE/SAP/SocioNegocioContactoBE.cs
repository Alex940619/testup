using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class SocioNegocioContactoBE
    {
        public String CodigoContacto { get; set; }
        public String NombreContacto { get; set; }
        public String ApellidoContacto { get; set; }
        public String DireccionContacto { get; set; }
        public String TelfContacto { get; set; }
        public String CorreoContacto { get; set; }
        public String FlgValido { get; set; }

    }
}
