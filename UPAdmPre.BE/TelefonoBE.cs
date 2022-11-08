using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class TelefonoBE
    {
        public TelefonoBE()
        { }

        #region "Atributos"

        private Int32? _idTelefono = null;
        private Int32? _Pais = null;
        private String _nroTelefono = null;
        private String _nroCelular = null;
        private String _descripcion = null;
        private UIConstantes.TIPO_TELEFONO _tipoTelefono;
        private String _revision_opid = null;

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? IdTelefono
        {
            get { return this._idTelefono; }
            set { this._idTelefono = value; }
        }
        public Int32? Pais
        {
            get { return this._Pais; }
            set { this._Pais = value; }
        }
        public String NroTelefono
        {
            get { return this._nroTelefono; }
            set { this._nroTelefono = value; }
        }
        public String NroCelular
        {
            get { return this._nroCelular; }
            set { this._nroCelular = value; }
        }
        public String Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }
        public UIConstantes.TIPO_TELEFONO TipoTelefono
        {
            get { return this._tipoTelefono; }
            set { this._tipoTelefono = value; }
        }

        public String Revision_Opid
        {
            get { return this._revision_opid; }
            set { this._revision_opid = value; }
        }

        #endregion "Propiedades"
    }
}
