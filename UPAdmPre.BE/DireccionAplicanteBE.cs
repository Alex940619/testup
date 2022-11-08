using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class DireccionAplicanteBE
    {
        public DireccionAplicanteBE()
        { }

        #region "Atributos"

        private Int32? _idDireccion = null;
        private UIConstantes.TIPO_DIRECCION _idTipoDireccion;
        private String _direccion1 = null;
        private String _direccion2 = null;
        private String _direccion3 = null;
        private String _distrito = null;
        private Int32? _provincia = null;
        private String _codigoPostal = null;
        private Int32? _departamento = null;
        private Int32? _pais = null;
        private String _revision_opid = null;
        private String _number = null;
        private String _interior = null;
        private String _reference = null;
        private String _tipovia = null;

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? IdDireccion
        {
            get { return this._idDireccion; }
            set { this._idDireccion = value; }
        }
        public UIConstantes.TIPO_DIRECCION IdTipoDireccion
        {
            get { return this._idTipoDireccion; }
            set { this._idTipoDireccion = value; }
        }
        public string Direccion1
        {
            get { return this._direccion1; }
            set { this._direccion1 = value; }
        }
        public string Direccion2
        {
            get { return this._direccion2; }
            set { this._direccion2 = value; }
        }
        public string Direccion3
        {
            get { return this._direccion3; }
            set { this._direccion3 = value; }
        }
        public string Distrito
        {
            get { return this._distrito; }
            set { this._distrito = value; }
        }
        public int? Provincia
        {
            get { return this._provincia; }
            set { this._provincia = value; }
        }
        public string CodigoPostal
        {
            get { return this._codigoPostal; }
            set { this._codigoPostal = value; }
        }
        public int? Departamento
        {
            get { return this._departamento; }
            set { this._departamento = value; }
        }
        public Int32? Pais
        {
            get { return this._pais; }
            set { this._pais = value; }
        }
        public string Revision_Opid
        {
            get { return this._revision_opid; }
            set { this._revision_opid = value; }
        }

        public string Number
        {
            get { return this._number; }
            set { this._number = value; }
        }

        public string Interior
        {
            get { return this._interior; }
            set { this._interior = value; }
        }

        public string Reference
        {
            get { return this._reference; }
            set { this._reference = value; }
        }

        public string TipoVia
        {
            get { return this._tipovia; }
            set { this._tipovia = value; }
        }

        #endregion "Propiedades"
    }
}
