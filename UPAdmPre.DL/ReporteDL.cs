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
    public class ReporteDL : ConexionBD
    {
        #region "Constructores"

        public ReporteDL()
            : base()
        {
        }

        public ReporteDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
            : base()
        {
            this.connection = miConexion;
            this.miTransaccion = miTransaccion;
        }

        #endregion "Constructores"

        #region "Atributos"

        private SqlCommand cmd;

        #endregion "Atributos"

        #region "Métodos No Transaccionales"

        public DataSet ImprimirVoucherPago(Int32? AplicanteId, Boolean transaccionIniciada)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                //if (!transaccionIniciada)
                //{
                //    myCon = this.getConexion();
                //    myCon.Open();
                //}
                myCon = this.getConexion();
               // myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_ImprimirVoucher", myCon);
                //cmd.Transaction = this.miTransaccion;//cmcs se agrega por caida
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));

                myCon.Open();//cmcs Se agrega por caida
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                ds = null;
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
                //if (!transaccionIniciada)
                //{
                //    myCon.Close();
                //}
            }
            return ds;
        }

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"
        #endregion "Métodos Transaccionales"
    }
}
