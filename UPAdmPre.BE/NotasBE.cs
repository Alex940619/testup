using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class NotasBE
    {
        public NotasBE()
        { }

        #region "Atributos"

        private Int32? _applicationId = null;
        private Int32? _M3 = null;
        private Int32? _L3 = null;
        //private String _M3 = null;
        //private String _L3 = null;
        private Int32? _P3 = null;
        private Int32? _M4 = null;
        private Int32? _L4 = null;
        //private String _M4 = null;
        //private String _L4 = null;
        private Int32? _P4 = null;
        private Int32? _M5 = null;
        private Int32? _L5 = null;
        //private String _M5 = null;
        //private String _L5 = null;
        private Int32? _P5 = null;
        private String _ONM3 = null;
        private String _ONL3 = null;
        private String _ONP3 = null;
        private String _ONM4 = null;
        private String _ONL4 = null;
        private String _ONP4 = null;
        private String _ONM5 = null;
        private String _ONL5 = null;
        private String _ONP5 = null;
        private Int32? _status = null;
        private DateTime? _createDate = null;
        private String _createUser = null;

        #endregion "Atributos"

        #region "Propiedades"

        public Int32? ApplicationId
        {
            get { return this._applicationId; }
            set { this._applicationId = value; }
        }
        public Int32? M3
        {
            get { return this._M3; }
            set { this._M3 = value; }
        }
        public Int32? L3
        {
            get { return this._L3; }
            set { this._L3 = value; }
        }
        public Int32? P3
        {
            get { return this._P3; }
            set { this._P3 = value; }
        }
        public Int32? M4
        {
            get { return this._M4; }
            set { this._M4 = value; }
        }
        public Int32? L4
        {
            get { return this._L4; }
            set { this._L4 = value; }
        }
        public Int32? P4
        {
            get { return this._P4; }
            set { this._P4 = value; }
        }
        public Int32? M5
        {
            get { return this._M5; }
            set { this._M5 = value; }
        }
        public Int32? L5
        {
            get { return this._L5; }
            set { this._L5 = value; }
        }
        public Int32? P5
        {
            get { return this._P5; }
            set { this._P5 = value; }
        }
        public String ONM3
        {
            get { return this._ONM3; }
            set { this._ONM3 = value; }
        }
        public String ONL3
        {
            get { return this._ONL3; }
            set { this._ONL3 = value; }
        }
        public String ONP3
        {
            get { return this._ONP3; }
            set { this._ONP3 = value; }
        }
        public String ONM4
        {
            get { return this._ONM4; }
            set { this._ONM4 = value; }
        }
        public String ONL4
        {
            get { return this._ONL4; }
            set { this._ONL4 = value; }
        }
        public String ONP4
        {
            get { return this._ONP4; }
            set { this._ONP4 = value; }
        }
        public String ONM5
        {
            get { return this._ONM5; }
            set { this._ONM5 = value; }
        }
        public String ONL5
        {
            get { return this._ONL5; }
            set { this._ONL5 = value; }
        }
        public String ONP5
        {
            get { return this._ONP5; }
            set { this._ONP5 = value; }
        }
        public Int32? Status
        {
            get { return this._status; }
            set { this._status = value; }
        }
        public DateTime? CreateDate
        {
            get { return this._createDate; }
            set { this._createDate = value; }
        }
        public String CreateUser
        {
            get { return this._createUser; }
            set { this._createUser = value; }
        }

        #endregion "Propiedades"
    }
}
