using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class ResultadoBE
    {
        public ResultadoBE()
        { }

        #region "Atributos"

        private String _anio = null;
        private String _periodo = null;
        private Int32? _activacionWeb = null;
        private String _msjPrevio = null;
        private String _msjSeleccionado = null;
        private String _msjNoSeleccionado = null;
        private String _proceso = null;

        #endregion "Atributos"

        #region "Propiedades"

        public string Anio
        {
            get { return _anio; }
            set { _anio = value; }
        }

        public string Periodo
        {
            get { return _periodo; }
            set { _periodo = value; }
        }

        public Int32? ActivacionWeb
        {
            get { return _activacionWeb; }
            set { _activacionWeb = value; }
        }

        public string MsjPrevio
        {
            get { return _msjPrevio; }
            set { _msjPrevio = value; }
        }

        public string MsjSeleccionado
        {
            get { return _msjSeleccionado; }
            set { _msjSeleccionado = value; }
        }

        public string MsjNoSeleccionado
        {
            get { return _msjNoSeleccionado; }
            set { _msjNoSeleccionado = value; }
        }

        public string Proceso
        {
            get { return _proceso; }
            set { _proceso = value; }
        }

        #endregion "Propiedades"
    }
}
