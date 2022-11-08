using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class ActividadBE
    {
        public ActividadBE()
        { }

        #region "Atributos"

        private Int32? _ApplicationActivityId = null;
        private Int32? _idActividad = UIConstantes.idValorNulo;
        private Int32? _idAplicacion = UIConstantes.idValorNulo;
        private Int32? _idTipoActividad = null;
        //private Int32? _posicion = null;
        private String _posicion = null;
        private Double _horasPorSemana = UIConstantes.idValorNulo;
        private Int32? _semanasPorAnho = UIConstantes.idValorNulo;
        private Int32? _numeroAnhos = UIConstantes.idValorNulo;
        private Int32? _gradoParticipacion09 = UIConstantes.idValorNulo;
        private Int32? _gradoParticipacion10 = UIConstantes.idValorNulo;
        private Int32? _gradoParticipacion11 = UIConstantes.idValorNulo;
        private Int32? _gradoParticipacion12 = UIConstantes.idValorNulo;
        private Int32? _participacionSecundaria = UIConstantes.idValorNulo;
        private String _nombreActividad = null;
        private DateTime? _fechaInicio = null;
        private DateTime? _fechaFin = null;
        private Int32? _EsPromovidoPorColegio = UIConstantes.idValorNulo;
        private Int32? _isdeleted = UIConstantes.idValorNulo;
        private String _descripcion = null;
        private String _revision_opid = null;

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? IdApplicationActivity
        {
            get { return this._ApplicationActivityId; }
            set { this._ApplicationActivityId = value; }
        }

        public Int32? IdActividad
        {
            get { return this._idActividad; }
            set { this._idActividad = value; }
        }

        public Int32? IdAplicacion
        {
            get { return this._idAplicacion; }
            set { this._idAplicacion = value; }
        }

        public Int32? IdTipoActividad
        {
            get { return this._idTipoActividad; }
            set { this._idTipoActividad = value; }
        }
        
        public String Posicion
        {
            get { return this._posicion; }
            set { this._posicion = value; }
        }
        
        public Double HorasPorSemana
        {
            get { return this._horasPorSemana; }
            set { this._horasPorSemana = value; }
        }

        public Int32? SemanasPorAnho
        {
            get { return this._semanasPorAnho; }
            set { this._semanasPorAnho = value; }
        }

        public Int32? NumeroAnhos
        {
            get { return this._numeroAnhos; }
            set { this._numeroAnhos = value; }
        }

        public Int32? GradoParticipacion09
        {
            get { return this._gradoParticipacion09; }
            set { this._gradoParticipacion09 = value; }
        }

        public Int32? GradoParticipacion10
        {
            get { return this._gradoParticipacion10; }
            set { this._gradoParticipacion10 = value; }
        }

        public Int32? GradoParticipacion11
        {
            get { return this._gradoParticipacion11; }
            set { this._gradoParticipacion11 = value; }
        }

        public Int32? GradoParticipacion12
        {
            get { return this._gradoParticipacion12; }
            set { this._gradoParticipacion12 = value; }
        }

        public Int32? ParticipacionSecundaria
        {
            get { return this._participacionSecundaria; }
            set { this._participacionSecundaria = value; }
        }

        public String NombreActividad
        {
            get { return this._nombreActividad; }
            set { this._nombreActividad = value; }
        }

        public DateTime? FechaInicio
        {
            get { return this._fechaInicio; }
            set { this._fechaInicio = value; }
        }

        public DateTime? FechaFin
        {
            get { return this._fechaFin; }
            set { this._fechaFin = value; }
        }

        public Int32? EsPromovidoPorColegio
        {
            get { return this._EsPromovidoPorColegio; }
            set { this._EsPromovidoPorColegio = value; }
        }

        public Int32? IsDeleted
        {
            get { return this._isdeleted; }
            set { this._isdeleted = value; }
        }

        public String descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        public String Revision_Opid
        {
            get { return this._revision_opid; }
            set { this._revision_opid = value; }
        }

        #endregion "Propiedades"
    }
}
