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
    public class TelefonoDL : ConexionBD
    {
        #region "Constructores"

        public TelefonoDL() { }

        public TelefonoDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
        {
            this.connection = miConexion;
            this.miTransaccion = miTransaccion;
        }

        #endregion "Constructores"

        #region "Atributos"

        private SqlCommand cmd;

        #endregion

        #region "Métodos No Transaccionales"

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        public Boolean insertarTelefonoAplicante(AplicanteBE oAplicanteBE, TelefonoBE oTelefonoBE, Boolean transaccionIniciada)
        {
            bool respuesta = true;
            SqlConnection myCon = new SqlConnection();           
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }
                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_InsTelefono", myCon);   
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                if (oTelefonoBE.Pais.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryId", oTelefonoBE.Pais));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryId", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_PhoneNumber", oTelefonoBE.NroTelefono));
                cmd.Parameters.Add(new SqlParameter("@VC_PhoneType", oTelefonoBE.TipoTelefono.ToString()));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                respuesta = false;
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                if (!transaccionIniciada)
                {
                    myCon.Close();
                }
            }
            return respuesta;
        }

        //Inicio JC.DelgadoV [Preformalización]
        public Boolean ActualizarTelefonoAplicante(AplicanteBE oAplicanteBE, TelefonoBE oTelefonoBE, Boolean transaccionIniciada)
        {
            bool respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }
                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ActualizarTelefono", myCon);
                cmd.Transaction = this.miTransaccion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                if (oTelefonoBE.Pais.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryId", oTelefonoBE.Pais));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryId", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_PhoneNumber", oTelefonoBE.NroTelefono));
                cmd.Parameters.Add(new SqlParameter("@VC_PhoneType", oTelefonoBE.TipoTelefono.ToString()));

                if (oAplicanteBE.RedId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", DBNull.Value));
                }

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                respuesta = false;
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                if (!transaccionIniciada)
                {
                    myCon.Close();
                }
            }
            return respuesta;
        }
        //Fin JC.DelgadoV [Preformalización]

        #endregion "Métodos Transaccionales"
    }
}
