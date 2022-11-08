using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPAdmPre.BE
{
    public class OVentaDetailBE
    {
        #region ATRIBUTOS

        private string _CentroCosto;
        private string _TipoDocumento;
        private string _NumeroBV;
        private string _Usuario;
        private string _Proyecto;
        private string _a_nus_per_cod;
        private string _a_nus_per_nom;
        private string _ItemCodigo;
        private string _Descripcion;
        private decimal _PrecioUnitario;
        private decimal _PrecioUnitarioEmpresa;
        private string _Linea;
        private string _TipoDato;
        private string _ID;
        private string _CodigoSpring;
        #endregion

        #region CONSTRUCTOR
        public OVentaDetailBE() 
        {
            _CentroCosto = string.Empty;
            _TipoDocumento = string.Empty;
            _NumeroBV = string.Empty;
            _Usuario = string.Empty;
            _Proyecto = string.Empty;
            _a_nus_per_cod = string.Empty;
            _a_nus_per_nom = string.Empty;
            _ItemCodigo = string.Empty;
            _Descripcion = string.Empty;
            _PrecioUnitario = 0;
            _Linea = string.Empty;
            _TipoDato = "";
            _ID = "";
            _PrecioUnitarioEmpresa = 0;
            _CodigoSpring  = "";


        }

        #endregion

        #region PROPIEDADES
        public string CentroCosto
        {
            get { return _CentroCosto; }
            set { _CentroCosto = value; }
        }

        public string TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        public string NumeroBV
        {
            get { return _NumeroBV; }
            set { _NumeroBV = value; }
        }

        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        public string Proyecto
        {
            get { return _Proyecto; }
            set { _Proyecto = value; }
        }

        public string a_nus_per_cod
        {
            get { return _a_nus_per_cod; }
            set { _a_nus_per_cod = value; }
        }

        public string a_nus_per_nom
        {
            get { return _a_nus_per_nom; }
            set { _a_nus_per_nom = value; }
        }

        public string ItemCodigo
        {
            get { return _ItemCodigo; }
            set { _ItemCodigo = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public decimal PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set { _PrecioUnitario = value; }
        }

        public string Linea
        {
            get { return _Linea; }
            set { _Linea = value; }
        }

        public string TipoDato
        {
            get { return _TipoDato; }
            set { _TipoDato = value; }
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public decimal PrecioUnitarioEmpresa
        {
            get { return _PrecioUnitarioEmpresa; }
            set { _PrecioUnitarioEmpresa = value; }
        }

        public string CodigoSpring
        {
            get { return _CodigoSpring; }
            set { _CodigoSpring = value; }
        }
        #endregion

    }
}
