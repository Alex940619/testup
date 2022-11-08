using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class DocumentoBE
    {
        public DocumentoBE()
        { }

        #region "Atributos"

        private Int32? _idDocumento = null;
        private Int32? _idAplicacion = null;
        private String _tituloDocumento = null;
        private Int32? idTipoMedio = null;
        private String _tipoMedio = null;
        private String _extension = null;
        private Byte[] _contenidoDocumento = null;
        private Decimal? _tamanhoDocumento = null;
        private String _usuarioActualiza = null;
        private Int32? _estado = null;
        private String _NombreApplicante = null;

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? Estado
        {
            get { return this._estado; }
            set { this._estado = value; }
        }
        public Int32? IdDocumento
        {
            get { return this._idDocumento; }
            set { this._idDocumento = value; }
        }
        public Int32? IdAplicacion
        {
            get { return this._idAplicacion; }
            set { this._idAplicacion = value; }
        }
        public String TituloDocumento
        {
            get { return this._tituloDocumento; }
            set { this._tituloDocumento = value; }
        }
        public Int32? IdTipoMedio
        {
            get { return idTipoMedio; }
            set { idTipoMedio = value; }
        }
        public String TipoMedio
        {
            get { return this._tipoMedio; }
            set { this._tipoMedio = value; }
        }
        public String Extension
        {
            get { return this._extension; }
            set { this._extension = value; }
        }
        public Byte[] ContenidoDocumento
        {
            get { return this._contenidoDocumento; }
            set { this._contenidoDocumento = value; }
        }
        public Decimal? TamanhoDocumento
        {
            get { return this._tamanhoDocumento; }
            set { this._tamanhoDocumento = value; }
        }

        public String UsuarioActualiza
        {
            get { return this._usuarioActualiza; }
            set { this._usuarioActualiza = value; }
        }
        public String NombreApplicante
        {
            get { return this._NombreApplicante; }
            set { this._NombreApplicante = value; }
        }

        #endregion "Propiedades"
    }
}
