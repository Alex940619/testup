using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class ExamenFormalizacionBE
    {
        public ExamenFormalizacionBE() { }

        #region "Atributos"

        private String _NomExamen = null;
        private Boolean _Estado = false;

        #endregion "Atributos"

        #region "Propiedades"

        public String NomExamen
        {
            get { return this._NomExamen; }
            set { this._NomExamen = value; }
        }

        public Boolean Estado
        {
            get { return this._Estado; }
            set { this._Estado = value; }
        }

        #endregion "Propiedades"
    }
}
