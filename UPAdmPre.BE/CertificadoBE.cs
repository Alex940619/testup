using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class CertificadoBE
    {
        public CertificadoBE()
        { }


        #region "Atributos"

        private String _EventId = null;
        private String _Description = null;
        private String _Name = null;

        #endregion

        #region "Propiedades"

        public String EventId
        {
            get { return _EventId; }
            set { _EventId = value; }
        }

        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        #endregion
    }
}
