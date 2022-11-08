using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.DL;

namespace UPAdmPre.BL
{
    public class CertificadoBL
    {
        private CertificadoBL oCertificadoBL;

        public CertificadoBL()
        { }

        #region "Métodos No Transaccionales"

        public DataTable ListarCarrerasPorModalidad()
        {
            CertificadoDL oCertificadoDL = null;
            try
            {
                oCertificadoDL = new CertificadoDL();
                return oCertificadoDL.ListarCertificadosIngles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oCertificadoDL = null;
            }
        }

        #endregion
    }
}
