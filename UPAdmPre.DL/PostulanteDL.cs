using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using UPAdmPre.BE;
using UPAdmPre.SL;

namespace UPAdmPre.DL
{
    public class PostulanteDL : ConexionBD
    {
        #region "Constructores"

        public PostulanteDL()
        { }

        #endregion "Constructores"

        #region "Atributos"

        private SqlCommand cmd;

        #endregion "Atributos"

        #region "Métodos No Transaccionales"

        public DataTable buscarPostulante(PostulanteBE oPostulanteBE, Int32? ApplicationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelResultado", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_UserName", oPostulanteBE.UserName));
                cmd.Parameters.Add(new SqlParameter("@VC_Proceso", oPostulanteBE.Proceso));
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", ApplicationId));

                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
                throw ex;
            }
            finally
            {
                if (da != null)
                {
                    da.Dispose();
                }
                cmd.Dispose();
                myCon.Close();
            }
            return dt;

        }

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        #endregion "Métodos Transaccionales"
    }
}
