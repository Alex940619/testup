using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class OrganizacionBE
    {
        public OrganizacionBE()
        { }

        #region "Atributos"

        private Int32? _codigo = null;
        private String _codigoAlternativo = null;
        private String _nombre = null;
        private String _telefono = null;
        private String _estado = null;

        private Int32? _idPais = null;
        private String _pais = null;

        private Int32? _idDepartamento = null;
        private String _departamento = null;
        private Int32? _idProvincia = null;
        private String _provincia = null;
        private String _distrito = null;
        private String _direccion = null;
        private String _ruc = null;
        private String _codigoModular = null;
        private String _Posicion = null;

        private UIConstantes.TIPO_ORGANIZACION _tipoOrganizacion = UIConstantes.TIPO_ORGANIZACION.NINGUNO;

        private String _order_by = UIConvertNull.String(UIConstantes.idValorActivo);
        private String _de_forma = UIConvertNull.String(UIConstantes.idValorActivo);

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? Codigo
        {
            get { return this._codigo; }
            set { this._codigo = value; }
        }
        public String CodigoAlternativo
        {
            get { return this._codigoAlternativo; }
            set { this._codigoAlternativo = value; }
        }
        public String Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }
        public String Telefono
        {
            get { return this._telefono; }
            set { this._telefono = value; }
        }
        public String Estado
        {
            get { return this._estado; }
            set { this._estado = value; }
        }
        public int? IdPais
        {
            get { return this._idPais; }
            set { this._idPais = value; }
        }
        public String Pais
        {
            get { return this._pais; }
            set { this._pais = value; }
        }
        public String Direccion
        {
            get { return this._direccion; }
            set { this._direccion = value; }
        }

        public String Order_by
        {
            get { return this._order_by; }
            set { this._order_by = value; }
        }
        public String De_forma
        {
            get { return this._de_forma; }
            set { this._de_forma = value; }
        }
        public int? IdDepartamento
        {
            get { return this._idDepartamento; }
            set { this._idDepartamento = value; }
        }

        public String Departamento
        {
            get { return this._departamento; }
            set { this._departamento = value; }
        }

        public int? IdProvincia
        {
            get { return this._idProvincia; }
            set { this._idProvincia = value; }
        }

        public String Provincia
        {
            get { return this._provincia; }
            set { this._provincia = value; }
        }


        public String Distrito
        {
            get { return this._distrito; }
            set { this._distrito = value; }
        }

        public String Ruc
        {
            get { return this._ruc; }
            set { this._ruc = value; }
        }

        public UIConstantes.TIPO_ORGANIZACION TipoOrganizacion
        {
            get { return this._tipoOrganizacion; }
            set { this._tipoOrganizacion = value; }
        }

        public String CodigoModular
        {
            get { return this._codigoModular; }
            set { this._codigoModular = value; }
        }

        public String Posicion
        {
            get { return this._Posicion; }
            set { this._Posicion = value; }
        }

        #endregion "Propiedades"
    }
}
