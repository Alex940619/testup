using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class CursoConvalidacionFormalizacionBE
    {

        public CursoConvalidacionFormalizacionBE() { }

        #region "Atributos"

        private String _NomCursoConvalidacion = null;
        private Boolean _Estado = false;

        #endregion "Atributos"

        #region "Propiedades"

        public String NomCursoConvalidacion
        {
            get { return this._NomCursoConvalidacion; }
            set { this._NomCursoConvalidacion = value; }
        }

        public Boolean Estado
        {
            get { return this._Estado; }
            set { this._Estado = value; }
        }

        #endregion "Propiedades"
    }
}
