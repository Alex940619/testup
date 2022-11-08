using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class EducacionDetalleBE
    {
        public EducacionDetalleBE()
        { }

        #region "Métodos No Transaccionales"

        private Int32? _idEducacion = null;
        private Int32? _idDetalleEducacion = null;
        private Int32? _idMerito = null;
        private DateTime? _fechaInicio = null;
        private Int32? _anioInicio = null;
        private DateTime? _fechaFin = null;
        private Int32? _idGrado = null;
        private Int32? _idEspecialidad = null;

        private Int32? _idCarrera = null;
        private String _nombreCarrera = null;
        private String _nombreGrado = null;
        private Int32? _cantidadEstudiantes = null;
        private Int32? _cantidadCiclosCursados = null;
        private Int32? _cantidadCreditosAprobados = null;
        private Int32? _Calc = null;

        private Int32? _IdApplication = null;
        private Int32? _IdApplicationEducation = null;
        private Int32? _ApplicationEducationEnrollId = null;
        private Int32? _NotaMateTercero = null;
        private Int32? _NotaMateCuarto = null;
        private Int32? _NotaMateQuinto = null;
        private Int32? _NotaLengTercero = null;
        private Int32? _NotaLengCuarto = null;
        private Int32? _NotaLengQuinto = null;
        private Int32? _NotaPromTercero = null;
        private Int32? _NotaPromCuarto = null;
        private Int32? _NotaPromQuinto = null;
        private String _OtraNotaMateTercero = null;
        private String _OtraNotaMateCuarto = null;
        private String _OtraNotaMateQuinto = null;
        private String _OtraNotaLengTercero = null;
        private String _OtraNotaLengCuarto = null;
        private String _OtraNotaLengQuinto = null;
        private String _OtraNotaPromTercero = null;
        private String _OtraNotaPromCuarto = null;
        private String _OtraNotaPromQuinto = null;
        private Int32? _SituaAcademica = null;

        private String _revision_opid = null;

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        public Int32? IdEducacion
        {
            get { return this._idEducacion; }
            set { this._idEducacion = value; }
        }
        public Int32? IdDetalleEducacion
        {
            get { return this._idDetalleEducacion; }
            set { this._idDetalleEducacion = value; }
        }
        public Int32? IdMerito
        {
            get { return this._idMerito; }
            set { this._idMerito = value; }
        }
        public DateTime? FechaInicio
        {
            get { return this._fechaInicio; }
            set { this._fechaInicio = value; }
        }
        public Int32? anioInicio
        {
            get { return this._anioInicio; }
            set { this._anioInicio = value; }
        }
        public DateTime? FechaFin
        {
            get { return this._fechaFin; }
            set { this._fechaFin = value; }
        }
        public Int32? IdGrado
        {
            get { return this._idGrado; }
            set { this._idGrado = value; }
        }
        public Int32? IdEspecialidad
        {
            get { return this._idEspecialidad; }
            set { this._idEspecialidad = value; }
        }
        public Int32? IdCarrera
        {
            get { return this._idCarrera; }
            set { this._idCarrera = value; }
        }
        public String NombreCarrera
        {
            get { return this._nombreCarrera; }
            set { this._nombreCarrera = value; }
        }
        public Int32? CantidadEstudiantes
        {
            get { return this._cantidadEstudiantes; }
            set { this._cantidadEstudiantes = value; }
        }
        public Int32? CantidadCiclosCursados
        {
            get { return this._cantidadCiclosCursados; }
            set { this._cantidadCiclosCursados = value; }
        }
        public Int32? CantidadCreditosAprobados
        {
            get { return this._cantidadCreditosAprobados; }
            set { this._cantidadCreditosAprobados = value; }
        }
        public String NombreGrado
        {
            get { return this._nombreGrado; }
            set { this._nombreGrado = value; }
        }
        public Int32? Calc
        {
            get { return this._Calc; }
            set { this._Calc = value; }
        }
        public Int32? IdApplication
        {
            get { return this._IdApplication; }
            set { this._IdApplication = value; }
        }
        public Int32? IdApplicationEducation
        {
            get { return this._IdApplicationEducation; }
            set { this._IdApplicationEducation = value; }
        }
        public Int32? IdApplicationEducationEnroll
        {
            get { return this._ApplicationEducationEnrollId; }
            set { this._ApplicationEducationEnrollId = value; }
        }
        public Int32? NotaMateTercero
        {
            get { return this._NotaMateTercero; }
            set { this._NotaMateTercero = value; }
        }
        public Int32? NotaMateCuarto
        {
            get { return this._NotaMateCuarto; }
            set { this._NotaMateCuarto = value; }
        }
        public Int32? NotaMateQuinto
        {
            get { return this._NotaMateQuinto; }
            set { this._NotaMateQuinto = value; }
        }
        public Int32? NotaLengTercero
        {
            get { return this._NotaLengTercero; }
            set { this._NotaLengTercero = value; }
        }
        public Int32? NotaLengCuarto
        {
            get { return this._NotaLengCuarto; }
            set { this._NotaLengCuarto = value; }
        }
        public Int32? NotaLengQuinto
        {
            get { return this._NotaLengQuinto; }
            set { this._NotaLengQuinto = value; }
        }
        public Int32? NotaPromTercero
        {
            get { return this._NotaPromTercero; }
            set { this._NotaPromTercero = value; }
        }
        public Int32? NotaPromCuarto
        {
            get { return this._NotaPromCuarto; }
            set { this._NotaPromCuarto = value; }
        }
        public Int32? NotaPromQuinto
        {
            get { return this._NotaPromQuinto; }
            set { this._NotaPromQuinto = value; }
        }
        public String OtraNotaMateTercero
        {
            get { return this._OtraNotaMateTercero; }
            set { this._OtraNotaMateTercero = value; }
        }
        public String OtraNotaMateCuarto
        {
            get { return this._OtraNotaMateCuarto; }
            set { this._OtraNotaMateCuarto = value; }
        }
        public String OtraNotaMateQuinto
        {
            get { return this._OtraNotaMateQuinto; }
            set { this._OtraNotaMateQuinto = value; }
        }
        public String OtraNotaLengTercero
        {
            get { return this._OtraNotaLengTercero; }
            set { this._OtraNotaLengTercero = value; }
        }
        public String OtraNotaLengCuarto
        {
            get { return this._OtraNotaLengCuarto; }
            set { this._OtraNotaLengCuarto = value; }
        }
        public String OtraNotaLengQuinto
        {
            get { return this._OtraNotaLengQuinto; }
            set { this._OtraNotaLengQuinto = value; }
        }
        public String OtraNotaPromTercero
        {
            get { return this._OtraNotaPromTercero; }
            set { this._OtraNotaPromTercero = value; }
        }
        public String OtraNotaPromCuarto
        {
            get { return this._OtraNotaPromCuarto; }
            set { this._OtraNotaPromCuarto = value; }
        }
        public String OtraNotaPromQuinto
        {
            get { return this._OtraNotaPromQuinto; }
            set { this._OtraNotaPromQuinto = value; }
        }
        public Int32? SituaAcademica
        {
            get { return this._SituaAcademica; }
            set { this._SituaAcademica = value; }
        }
        public String Revision_Opid
        {
            get { return this._revision_opid; }
            set { this._revision_opid = value; }
        }

        /*Ini:Christian Ramirez - REQ91569*/
        public Int32? CodTipoCalificacion { get; set; }
        public string DescTipoCalificacion { get; set; }
        /*Fin:Christian Ramirez - REQ91569*/
        #endregion "Métodos Transaccionales"
    }
}
