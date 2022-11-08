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
    public class ResultadoBL
    {
        public ResultadoBL()
        { }

        #region "Métodos No Transaccionales"

        public DataTable ObtenerResultadoAdmision(String strProceso)
        {
            ResultadoDL oResultadoDL = null;
            try
            {
                oResultadoDL = new ResultadoDL();
                return oResultadoDL.ObtenerResultadoAdmision(strProceso);
            }
            catch (Exception ex)
            {                
                throw ex;
            }            
        }

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        #endregion "Métodos Transaccionales"
    }
}
