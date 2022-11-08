using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class EmpleadorBE
    {
        public EmpleadorBE()
        { }

        #region "Atributos"

        private Int32? _idEmpleador = null;
        private String _nombreEmpleador = null;
        private Int32? _categoriaEmpleado = null;
        private DateTime? _fechaIngreso = null;
        private DateTime? _fechaSalida = null;
        private String _RUCEmpleador = null;
        private String _areaFuncional = null;
        private String _cargo = null;
        private String _paginaWebEmpleador = null;
        private Double _salarioAnual = 0;
        private OrganizacionBE _organizacion = null;
        private TelefonoBE _telefono = null;
        private String _nombreComercial = null;
        private String _revision_opid = null;

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? IdEmpleador
        {
            get { return this._idEmpleador; }
            set { this._idEmpleador = value; }
        }
        public String NombreEmpleador
        {
            get { return this._nombreEmpleador; }
            set { this._nombreEmpleador = value; }
        }
        public Int32? CategoriaEmpleado
        {
            get { return this._categoriaEmpleado; }
            set { this._categoriaEmpleado = value; }
        }
        public DateTime? FechaIngreso
        {
            get { return this._fechaIngreso; }
            set { this._fechaIngreso = value; }
        }
        public DateTime? FechaSalida
        {
            get { return this._fechaSalida; }
            set { this._fechaSalida = value; }
        }
        public String RUCEmpleador
        {
            get { return this._RUCEmpleador; }
            set { this._RUCEmpleador = value; }
        }
        public String AreaFuncional
        {
            get { return this._areaFuncional; }
            set { this._areaFuncional = value; }
        }
        public String Cargo
        {
            get { return this._cargo; }
            set { this._cargo = value; }
        }
        public String PaginaWebEmpleador
        {
            get { return this._paginaWebEmpleador; }
            set { this._paginaWebEmpleador = value; }
        }
        public Double SalarioAnual
        {
            get { return this._salarioAnual; }
            set { this._salarioAnual = value; }
        }
        public OrganizacionBE Organizacion
        {
            get { return this._organizacion; }
            set { this._organizacion = value; }
        }
        public TelefonoBE Telefono
        {
            get { return this._telefono; }
            set { this._telefono = value; }
        }
        public String NombreComercial
        {
            get { return this._nombreComercial; }
            set { this._nombreComercial = value; }
        }
        public String Revision_Opid
        {
            get { return this._revision_opid; }
            set { this._revision_opid = value; }
        }

        #endregion "Propiedades"
    }
}
