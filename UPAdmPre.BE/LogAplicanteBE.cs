using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class LogAplicanteBE
    {
        public LogAplicanteBE()
        { }

        #region "Atributos"

        private Int32? _idLogAplicante = UIConstantes.idValorNulo;
        private String _idConfiguracionAplicacion = null;
        private String _primerNombre = null;
        private String _apellidos = null;
        private String _fechaNacimiento = null;
        private String _email = null;
        private String _explorador = null;
        private String _idiomaExplorador = null;
        private String _error = null;

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? IdLogAplicante
        {
            get { return this._idLogAplicante; }
            set { this._idLogAplicante = value; }
        }
        public String IdConfiguracionAplicacion
        {
            get { return this._idConfiguracionAplicacion; }
            set { this._idConfiguracionAplicacion = value; }
        }
        public String PrimerNombre
        {
            get { return this._primerNombre; }
            set { this._primerNombre = value; }
        }
        public String Apellidos
        {
            get { return this._apellidos; }
            set { this._apellidos = value; }
        }
        public String FechaNacimiento
        {
            get { return this._fechaNacimiento; }
            set { this._fechaNacimiento = value; }
        }
        public String Email
        {
            get { return this._email; }
            set { this._email = value; }
        }
        public String Explorador
        {
            get { return this._explorador; }
            set { this._explorador = value; }
        }
        public String IdiomaExplorador
        {
            get { return this._idiomaExplorador; }
            set { this._idiomaExplorador = value; }
        }
        public String Error
        {
            get { return this._error; }
            set { this._error = value; }
        }  

        #endregion "Propiedades"
    }
}
