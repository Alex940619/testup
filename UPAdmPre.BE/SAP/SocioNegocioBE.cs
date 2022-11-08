using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class SocioNegocioBE
    {
        public String CodigoSocioNegocio { get; set; }
        public String NombreSocioNegocio { get; set; }
        public String ClasificacionSocioNegocio { get; set; }
        public Int32 GrupoSocioNegocio { get; set; }
        public String Moneda { get; set; }
        public String TipoSocioNegocio { get; set; }
        public String TipoDocumento { get; set; }
        public String NroDocumento { get; set; }
        public String ApellidoPaterno { get; set; }
        public String ApellidoMaterno { get; set; }
        public String SegundoNombre { get; set; }
        public String PrimerNombre { get; set; }
        public DateTime FecNacimiento { get; set; }
        public String CodPowerCampus { get; set; }
        public String UsuarioRed { get; set; }
        public String CodSpring { get; set; }
        public String Origen { get; set; }
        public int AsesorVentas { get; set; }
        public String numerotelefono1 { get; set; }
        public String numerotelefono2 { get; set; }
        public String numerocelular1 { get; set; }
        public String email1 { get; set; }
        public CamposAdicionalesListaBE CamposAdicionales { get; set; }
        public SocioNegocioContactoListaBE SocioNegocioContactoLista { get; set; }
        public SocioNegocioDireccionListaBE SocioNegocioDireccionLista { get; set; }
    }
}
