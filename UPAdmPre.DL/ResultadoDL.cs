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
    public class ResultadoDL : ConexionBD
    {
        #region "Constructores"

        public ResultadoDL()
            //: base()
        {
        }

        //public ResultadoDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
        //    : base()
        //{
        //    this.connection = miConexion;
        //    this.miTransaccion = miTransaccion;
        //}

        #endregion "Constructores"

        #region "Atributos"

        private SqlCommand cmd;

        #endregion "Atributos"

        #region "Métodos No Transaccionales"

        public DataTable ObtenerResultadoAdmision(String strProceso)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelPublicacionResultados", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_Proceso", strProceso));
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
