using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class InstitucionBE : OrganizacionBE
    {
        public InstitucionBE()
            : base()
        {
            _tipoInstitucion = String.Empty;
        }

        #region "Atributos"

        private String _tipoInstitucion = null;

        #endregion "Atributos"

        #region "Propiedades"

        public String TipoInstitucion
        {
            get { return this._tipoInstitucion; }
            set { this._tipoInstitucion = value; }
        }

        #endregion "Propiedades"
    }
}
