using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class InteresBE
    {
        public InteresBE()
        { }

        #region "Atributos"

        private int _idInteres = UIConstantes.idValorNulo;
        private int _idAplicacion = UIConstantes.idValorNulo;

        #endregion "Atributos"

        #region "Propiedades"

        public int IdInteres
        {
            get { return this._idInteres; }
            set { this._idInteres = value; }
        }
        public int IdAplicacion
        {
            get { return this._idAplicacion; }
            set { this._idAplicacion = value; }
        }

        #endregion "Propiedades"
    }
}
