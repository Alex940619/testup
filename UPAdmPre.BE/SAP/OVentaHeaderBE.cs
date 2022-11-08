using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPAdmPre.BE
{
    public class OVentaHeaderBE
    {
        #region ATRIBUTOS

        private string _Periodo;
        private string _UnidadNegocio;
        private string _CentroCosto;
        private string _Establecimiento;
        private string _TipoDocumento;
        private string _Serie;
        private string _Cuota;
        private string _Usuario;
        private string _UnidadReplicacion;
        private string _CodigoSpring;
        private string _FechaDocumento;
        private string _Proyecto;
        private string _a_nus_per_cod;
        private string _a_nus_per_nom;
        private string _indAuspicio;
        private decimal _montoCuota;
        private string _EsCEUP;
        private string _Escala;
        private decimal _NroCreditos;
        private string _FecVencimiento;
        private string _MonedaDocumento;
        private string _Comentarios;
        private string _TipoDato;
        private OVentaDetailBE _BEOVentaDetail;
        private int _ApplicationId;
        private string _ID;
        private string _CodigoEmpresa;
        private decimal _montoCuotaEmpresa;
        private string _Identificador;
        private int _Propietario;

        #endregion

        #region CONSTRUCTOR
        public OVentaHeaderBE() 
        {
            _Periodo = "";
            _UnidadNegocio = "";
            _CentroCosto = "";
            _Establecimiento = "";
            _TipoDocumento = "";
            _Serie = "";
            _Cuota = "";
            _Usuario = "";
            _UnidadReplicacion = "";
            _CodigoSpring = "";
            _CodigoEmpresa = "";
            _FechaDocumento = "";
            _Proyecto = "";
            _a_nus_per_cod = "";
            _a_nus_per_nom = "";
            _indAuspicio = "";
            _montoCuota = 0;
            _EsCEUP = "";
            _Escala = "";
            _NroCreditos = 0;
            _FecVencimiento = "";
            _MonedaDocumento = "";
            _Comentarios = "";
            _TipoDato = "";
            _BEOVentaDetail = null;
            _ApplicationId = 0;
            _ID = "";
            _montoCuotaEmpresa = 0;
            _Propietario = 0;
            _Identificador = "";
        }

        #endregion

        #region PROPIEDADES
        public string Periodo
        {
            get { return _Periodo; }
            set { _Periodo = value; }
        }

        public string UnidadNegocio
        {
            get { return _UnidadNegocio; }
            set { _UnidadNegocio = value; }
        }

        public string CentroCosto
        {
            get { return _CentroCosto; }
            set { _CentroCosto = value; }
        }

        public string Establecimiento
        {
            get { return _Establecimiento; }
            set { _Establecimiento = value; }
        }

        public string Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        public string TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        public string Cuota
        {
            get { return _Cuota; }
            set { _Cuota = value; }
        }

        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        public string UnidadReplicacion
        {
            get { return _UnidadReplicacion; }
            set { _UnidadReplicacion = value; }
        }

        public string CodigoSpring
        {
            get { return _CodigoSpring; }
            set { _CodigoSpring = value; }
        }

        public string CodigoEmpresa
        {
            get { return _CodigoEmpresa; }
            set { _CodigoEmpresa = value; }
        }

        public string FechaDocumento
        {
            get { return _FechaDocumento; }
            set { _FechaDocumento = value; }
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

        public string indAuspicio
        {
            get { return _indAuspicio; }
            set { _indAuspicio = value; }
        }

        public decimal montoCuota
        {
            get { return _montoCuota; }
            set { _montoCuota = value; }
        }

        public string EsCEUP
        {
            get { return _EsCEUP; }
            set { _EsCEUP = value; }
        }

        public string Escala
        {
            get { return _Escala; }
            set { _Escala = value; }
        }

        public decimal NroCreditos
        {
            get { return _NroCreditos; }
            set { _NroCreditos = value; }
        }

        public string FecVencimiento
        {
            get { return _FecVencimiento; }
            set { _FecVencimiento = value; }
        }

        public string MonedaDocumento
        {
            get { return _MonedaDocumento; }
            set { _MonedaDocumento = value; }
        }

        public string Comentarios
        {
            get { return _Comentarios; }
            set { _Comentarios = value; }
        }

        public string TipoDato
        {
            get { return _TipoDato; }
            set { _TipoDato = value; }
        }

        public OVentaDetailBE OVentaDetailBE
        {
            get { return _BEOVentaDetail; }
            set { _BEOVentaDetail = value; }
        }

        public int ApplicationId
        {
            get { return _ApplicationId; }
            set { _ApplicationId = value; }
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Identificador
        {
            get { return _Identificador; }
            set { _Identificador = value; }
        }

        #endregion

        public decimal montoCuotaEmpresa
        {
            get { return _montoCuotaEmpresa; }
            set { _montoCuotaEmpresa = value; }
        }

        public int Propietario
        {
            get { return _Propietario; }
            set { _Propietario = value; }
        }
        

    }
}
