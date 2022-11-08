using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.BE;
using UPAdmPre.SL;

namespace UPAdmPre.DL
{
    public class CertificadoDL: ConexionBD
    {
        public CertificadoDL()
        { }

        #region "Atributos"

        private SqlCommand cmd;

        #endregion "Atributos"

        #region "Metodos no transaccionales"

        public DataTable ListarCertificadosIngles()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();
                cmd = new SqlCommand("UP_ListarCertificadosIngles", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

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

        #endregion
    }
}
