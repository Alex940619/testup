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
    public class PostulanteBL
    {
        public PostulanteBL()
        { }

        #region "Métodos No Transaccionales"

        public DataTable buscarInfoPostulante(PostulanteBE oPostulanteBE, Int32? applicationid)
        {
            PostulanteDL oPostulanteDL = null;
            try
            {
                oPostulanteDL = new PostulanteDL();
                return oPostulanteDL.buscarPostulante(oPostulanteBE, applicationid);
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
