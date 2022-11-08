using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class EducacionBE
    {
        public EducacionBE()
        { }

        #region "Atributos"

        private Int32? _ApplicationEducationId = null;
        private Int32? _idEducacion = UIConstantes.idValorNulo;
        private Int32? _idAplicacion = UIConstantes.idValorNulo;
        private String _nombreInstitucion = null;
        private String _direccion = null;
        private String _ciudadInstitucion = null;
        private Int32? _departamento = null;
        private String _departamentoDes = null;
        private Int32? _pais = null;
        private String _ficeCode = null;
        private String _etsCode = null;
        private String _modCode = null;

        private InstitucionBE _institucion = null;
        private List<EducacionDetalleBE> _lDetalleEducacion = null;
        private UIConstantes.SECCION_EN_FORMULARIO _seccionEnFormulario = UIConstantes.SECCION_EN_FORMULARIO.NINGUNO;
        private String _revision_opid = null;
        private String _sitAcademica = null;

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? IdApplicationEducation
        {
            get { return this._ApplicationEducationId; }
            set { this._ApplicationEducationId = value; }
        }
        public Int32? IdEducacion
        {
            get { return this._idEducacion; }
            set { this._idEducacion = value; }
        }
        public Int32? IdAplicacion
        {
            get { return this._idAplicacion; }
            set { this._idAplicacion = value; }
        }
        public String NombreInstitucion
        {
            get { return this._nombreInstitucion; }
            set { this._nombreInstitucion = value; }
        }
        public String Direccion
        {
            get { return this._direccion; }
            set { this._direccion = value; }
        }
        public String CiudadInstitucion
        {
            get { return this._ciudadInstitucion; }
            set { this._ciudadInstitucion = value; }
        }
        public Int32? Departamento
        {
            get { return this._departamento; }
            set { this._departamento = value; }
        }
        public String DepartamentoDes
        {
            get { return this._departamentoDes; }
            set { this._departamentoDes = value; }
        }
        public Int32? Pais
        {
            get { return this._pais; }
            set { this._pais = value; }
        }
        public String FiceCode
        {
            get { return this._ficeCode; }
            set { this._ficeCode = value; }
        }
        public String EtsCode
        {
            get { return this._etsCode; }
            set { this._etsCode = value; }
        }
        public InstitucionBE Institucion
        {
            get { return this._institucion; }
            set { this._institucion = value; }
        }
        public List<EducacionDetalleBE> LDetalleEducacion
        {
            get { return this._lDetalleEducacion; }
            set { this._lDetalleEducacion = value; }
        }
        public UIConstantes.SECCION_EN_FORMULARIO SeccionEnFormulario
        {
            get { return this._seccionEnFormulario; }
            set { this._seccionEnFormulario = value; }
        }
        public String Revision_Opid
        {
            get { return this._revision_opid; }
            set { this._revision_opid = value; }
        }
        public String ModCode
        {
            get { return this._modCode; }
            set { this._modCode = value; }
        }
        public String SituacionAcademica
        { 
            get { return this._sitAcademica; }
            set { this._sitAcademica = value; }
        }

        public string TipoEntrevista { get; set; } /*Se agrega:Christian Ramirez - GIIT[caso60747] - 20190521*/
        public String TipoColegioProcedencia { get; set; } /*Se agrega:Christian Ramirez - GIIT[caso 64015] - 20190619*/

        public String TipoEvaluacionECL { get; set; } /*Se agrega:Christian Ramirez - REQ110609*/
        #endregion "Propiedades"
    }
}
