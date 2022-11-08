using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class RelacionBE
    {
        public RelacionBE()
        { }

        #region "Atributos"

        private Int32? _IdApplicationRelationship = null;
        private Int32? _idRelacion = UIConstantes.idValorNulo;
        private Int32? _idAplicacion = UIConstantes.idValorNulo;
        private Int32? _idTipoRelacion = null;
        private Int32? _prefijo = null;
        private String _nombres = null;
        private String _apellido = null;
        private Int32? _sufijo = null;
        private Int32? _asistenciaInstitucion = UIConstantes.idValorNulo;
        private DateTime? _fechaCreacion = null;
        private DateTime? _fechaRevision = null;
        private String _correoPersonal = null;
        private String _tipoDocumento = null;
        private String _documento = null;
        private String _numeroTelefono = null;
        private Int32? _estudioAntesUP = UIConstantes.idValorNulo;
        private Int32? _isdeleted = UIConstantes.idValorNulo;
        private Int32? _compartir = null;
        private Int32? _fallecido = null;
        private String _revision_opid = null;

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? IdApplicationRelationship
        {
            get { return this._IdApplicationRelationship; }
            set { this._IdApplicationRelationship = value; }
        }

        public Int32? IdRelacion
        {
            get { return this._idRelacion; }
            set { this._idRelacion = value; }
        }

        public Int32? IdAplicacion
        {
            get { return this._idAplicacion; }
            set { this._idAplicacion = value; }
        }

        public Int32? IdTipoRelacion
        {
            get { return this._idTipoRelacion; }
            set { this._idTipoRelacion = value; }
        }

        public Int32? Prefijo
        {
            get { return this._prefijo; }
            set { this._prefijo = value; }
        }

        public String Nombres
        {
            get { return this._nombres; }
            set { this._nombres = value; }
        }

        public String Apellido
        {
            get { return this._apellido; }
            set { this._apellido = value; }
        }

        public Int32? Sufijo
        {
            get { return this._sufijo; }
            set { this._sufijo = value; }
        }

        public Int32? AsistenciaInstitucion
        {
            get { return this._asistenciaInstitucion; }
            set { this._asistenciaInstitucion = value; }
        }

        public DateTime? FechaCreacion
        {
            get { return this._fechaCreacion; }
            set { this._fechaCreacion = value; }
        }

        public DateTime? FechaRevision
        {
            get { return this._fechaRevision; }
            set { this._fechaRevision = value; }
        }

        public String CorreoPersonal
        {
            get { return this._correoPersonal; }
            set { this._correoPersonal = value; }
        }

        public String TipoDocumento
        {
            get { return this._tipoDocumento; }
            set { this._tipoDocumento = value; }
        }

        public String Documento
        {
            get { return this._documento; }
            set { this._documento = value; }
        }

        public String NumeroTelefono
        {
            get { return this._numeroTelefono; }
            set { this._numeroTelefono = value; }
        }

        public Int32? EstudioAntesUP
        {
            get { return this._estudioAntesUP; }
            set { this._estudioAntesUP = value; }
        }

        public Int32? IsDeleted
        {
            get { return this._isdeleted; }
            set { this._isdeleted = value; }
        }

        public Int32? Compartir
        {
            get { return _compartir; }
            set { _compartir = value; }
        }

        public Int32? Fallecido
        {
            get { return _fallecido; }
            set { _fallecido = value; }
        }

        public String Revision_Opid
        {
            get { return this._revision_opid; }
            set { this._revision_opid = value; }
        }

        #endregion "Propiedades"
    }
}
