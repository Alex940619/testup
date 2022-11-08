using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using UPAdmPre.DL;

namespace UPAdmPre.BL
{
    public class ReporteBL
    {
        public ReporteBL()
        { }

        #region "Métodos No Transaccionales"

        public List<DataSet> ImprimirVoucherPago(Int32? AplicanteId)
        {
            ReporteDL oReporteDL = null;
            DataSet dsTempReporte = null;
            List<DataSet> ldsReporte = null;
            try
            {
                oReporteDL = new ReporteDL();
                //oReporteDL.Conexion();
                //oReporteDL.AbrirConexion();
                //oReporteDL.BeginTransaction();
                dsTempReporte = oReporteDL.ImprimirVoucherPago(AplicanteId, false);
                if (dsTempReporte != null)
                {
                    if (ldsReporte == null)
                    {
                        ldsReporte = new List<DataSet>();
                    }
                    ldsReporte.Add(dsTempReporte.Copy());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                oReporteDL = null;
            }
            return ldsReporte;
        }

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        #endregion "Métodos Transaccionales"
    }
}
