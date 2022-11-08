using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class ReferenciaBE
    {
        public ReferenciaBE()
        { }

        #region "Atributos"

        private Int32? _idReferencia = UIConstantes.idValorNulo;
        private Int32? _idAplicacion = UIConstantes.idValorNulo;
        private String _nombrePersona = null;
        private String _nombreOrganizacion = null;
        private String _cargo = null;
        private String _telefono = null;
        private String _revision_opid = null;
        private String _email = null;
        private String _firstname = null;
        private String _lastname = null;
        private Int32? _status = UIConstantes.idValorNulo;
        private String _idAplicacionEncrypt = null;
        private Int32? _errorenvioemail = UIConstantes.idValorNulo;
        private Int32? _refNum = null;
        private OrganizacionBE _organizacion = null;

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? IdReferencia
        {
            get { return this._idReferencia; }
            set { this._idReferencia = value; }
        }

        public Int32? IdAplicacion
        {
            get { return this._idAplicacion; }
            set { this._idAplicacion = value; }
        }

        public String NombrePersona
        {
            get { return this._nombrePersona; }
            set { this._nombrePersona = value; }
        }

        public String NombreOrganizacion
        {
            get { return this._nombreOrganizacion; }
            set { this._nombreOrganizacion = value; }
        }

        public String Cargo
        {
            get { return this._cargo; }
            set { this._cargo = value; }
        }

        public String Telefono
        {
            get { return this._telefono; }
            set { this._telefono = value; }
        }

        public String Revision_Opid
        {
            get { return this._revision_opid; }
            set { this._revision_opid = value; }
        }

        public String Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        public String Firstname
        {
            get { return this._firstname; }
            set { this._firstname = value; }
        }

        public String LastName
        {
            get { return this._lastname; }
            set { this._lastname = value; }
        }

        public Int32? Status
        {
            get { return this._status; }
            set { this._status = value; }
        }

        public String IdAplicacionEncrypt
        {
            get { return this._idAplicacionEncrypt; }
            set { this._idAplicacionEncrypt = value; }
        }

        public Int32? ErrorEnvioEmail
        {
            get { return this._errorenvioemail; }
            set { this._errorenvioemail = value; }
        }

        public Int32? NumReferencia
        {
            get { return this._refNum; }
            set { this._refNum = value; }
        }
        public OrganizacionBE Organizacion
        {
            get { return this._organizacion; }
            set { this._organizacion = value; }
        }

        #endregion "Propiedades"
    }
}
