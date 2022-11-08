using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class RequisitoBE
    {
        public RequisitoBE()
        { }

        #region "Atributos"

        private Int32? _idTipoPostulacion = null;
        private String _idDocumento = null;
        private String _descripcion = null;
        private Boolean _aprobado = false;
        private String _observacion = null;
        private Int32? _observacionCode = null;
        private String _revision_opid = null;
        private Boolean _enviar = false;
        private String _estado = UIConvertNull.String(UIConstantes.idValorActivo);

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? IdTipoPostulacion
        {
            get { return this._idTipoPostulacion; }
            set { this._idTipoPostulacion = value; }
        }

        public String IdDocumento
        {
            get { return this._idDocumento; }
            set { this._idDocumento = value; }
        }

        public String Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        public Boolean Aprobado
        {
            get { return this._aprobado; }
            set { this._aprobado = value; }
        }

        public String Observacion
        {
            get { return this._observacion; }
            set { this._observacion = value; }
        }

        public String Revision_Opid
        {
            get { return this._revision_opid; }
            set { this._revision_opid = value; }
        }

        public bool Enviar
        {
            get { return this._enviar; }
            set { this._enviar = value; }
        }

        public String Estado
        {
            get { return this._estado; }
            set { this._estado = value; }
        }

        public Int32? ObservacionCode
        {
            get { return this._observacionCode; }
            set { this._observacionCode = value; }
        }

        #endregion "Propiedades"
    }
}
