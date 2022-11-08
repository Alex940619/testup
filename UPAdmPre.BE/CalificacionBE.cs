using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class CalificacionBE
    {
        public CalificacionBE()
        { }

        #region "Atributos"

        private String _Tipo = null;
        private Decimal _Puntaje = UIConstantes.idValorNulo;
        private DateTime? _Fecha = null;
        private String _Comentario = null;
        private String _People = null;

        #endregion "Atributos"

        #region "Propiedades"

        public String Tipo
        {
            get { return this._Tipo; }
            set { this._Tipo = value; }
        }
        public Decimal Puntaje
        {
            get { return this._Puntaje; }
            set { this._Puntaje = value; }
        }
        public DateTime? Fecha
        {
            get { return this._Fecha; }
            set { this._Fecha = value; }
        }
        public String Comentario
        {
            get { return this._Comentario; }
            set { this._Comentario = value; }
        }
        public String People
        {
            get { return this._People; }
            set { this._People = value; }
        }

        #endregion "Propiedades"
    }
}
