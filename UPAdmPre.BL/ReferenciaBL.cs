using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using UPAdmPre.BE;
using UPAdmPre.DL;

namespace UPAdmPre.BL
{
    public class ReferenciaBL
    {
        public ReferenciaBL()
        { }

        #region "Métodos No Transaccionales"

        public string ReenvioenviaEmailReferenciaAplicante(Int32? ApplicantId, String ApplicantIdEncrypt, Int32? Reenvio, Boolean transaccionIniciada)
        {
            ReferenciaDL oReferenciaDL = null;
            try
            {
                oReferenciaDL = new ReferenciaDL();
                return oReferenciaDL.ReenvioenviaEmailReferenciaAplicante(ApplicantId, ApplicantIdEncrypt, Reenvio, transaccionIniciada);
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        public DataTable ObtenerApplicationReferencebyId(Int32? ReferenteID)
        {
            ReferenciaDL oReferenciaDL = null;
            try
            {
                oReferenciaDL = new ReferenciaDL();
                return oReferenciaDL.ObtenerApplicationReferencebyId(ReferenteID);
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        public Boolean enviaErrorEmailReferenciaAplicante(Int32? ApplicantId, String ApplicantIdEncrypt, String ApplicationIdEncrypt, Boolean transaccionIniciada)
        {
            ReferenciaDL oReferenciaDL = null;
            try
            {
                oReferenciaDL = new ReferenciaDL();
                return oReferenciaDL.enviaErrorEmailReferenciaAplicante(ApplicantId, ApplicantIdEncrypt, ApplicationIdEncrypt, transaccionIniciada);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool enviaEmailReferenciaAplicante(Int32? ApplicantId, String ApplicantIdEncrypt, Boolean transaccionIniciada)
        {
            ReferenciaDL oReferenciaDL = null;
            try
            {
                oReferenciaDL = new ReferenciaDL();
                return oReferenciaDL.enviaEmailReferenciaAplicante(ApplicantId, ApplicantIdEncrypt, transaccionIniciada);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        public bool modificarReferencia(ReferenciaBE oReferenciaBE)
        {
            ReferenciaDL oReferenciaDL = null;
            try
            {
                oReferenciaDL = new ReferenciaDL();
                return oReferenciaDL.modificarReferencia(oReferenciaBE);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        #endregion "Métodos Transaccionales"
    }
}
