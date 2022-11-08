using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class EvaluacionBE
    {
        public EvaluacionBE()
        { }

        #region "Atributos"

        private Int32? _idAplicacion = UIConstantes.idValorNulo;
        private Int32? _idTipoPostulacion = UIConstantes.idValorNulo;
        private Double _cantidadPagada = UIConstantes.idValorNulo;
        private String _numeroBoletaPago = null;
        private DateTime? _fechaEntrevista = null;
        private DateTime? _fechaPresentacion = null;
        private String _nombreCiclo = null;
        private String _horarioCiclo = null;
        private String _observacion = null;
        private Int32? _porConfirmar = UIConstantes.idValorNulo;
        private String _revision_opid = null;

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? IdAplicacion
        {
            get { return this._idAplicacion; }
            set { this._idAplicacion = value; }
        }
        public Int32? IdTipoPostulacion
        {
            get { return this._idTipoPostulacion; }
            set { this._idTipoPostulacion = value; }
        }
        public DateTime? FechaEntrevista
        {
            get { return this._fechaEntrevista; }
            set { this._fechaEntrevista = value; }
        }
        public DateTime? FechaPresentacion
        {
            get { return this._fechaPresentacion; }
            set { this._fechaPresentacion = value; }
        }
        public String NombreCiclo
        {
            get { return this._nombreCiclo; }
            set { this._nombreCiclo = value; }
        }
        public String HorarioCiclo
        {
            get { return this._horarioCiclo; }
            set { this._horarioCiclo = value; }
        }
        public String Observacion
        {
            get { return this._observacion; }
            set { this._observacion = value; }
        }
        public double CantidadPagada
        {
            get { return this._cantidadPagada; }
            set { this._cantidadPagada = value; }
        }
        public String NumeroBoletaPago
        {
            get { return this._numeroBoletaPago; }
            set { this._numeroBoletaPago = value; }
        }
        public Int32? PorConfirmar
        {
            get { return this._porConfirmar; }
            set { this._porConfirmar = value; }
        }
        public String Revision_Opid
        {
            get { return this._revision_opid; }
            set { this._revision_opid = value; }
        }

        #endregion "Propiedades"
    }
}
