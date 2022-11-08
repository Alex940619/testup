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
    public class LogAplicanteDL : ConexionBD
    {
        #region "Constructor"

        public LogAplicanteDL() : base()
        { }

        public LogAplicanteDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
            : base()
        {
            this.connection = miConexion;
            this.miTransaccion = miTransaccion;
        }

        #endregion "Constructor"

        #region "Atributos"

        private SqlCommand cmd;

        #endregion

        #region "Metodos"

        public Int32 insertarLogAplicante(LogAplicanteBE oLogAplicanteBE, Boolean transaccionIniciada)
        {
            Int32 codInsertado = 0;
            SqlConnection myCon = new SqlConnection();           
            if (!transaccionIniciada)
            {
                myCon = this.getConexion();
                myCon.Open();
            }
            myCon = this.connection;
            cmd = new SqlCommand("UPAdmPreSPInsertaLogApp", myCon);   
            cmd.Transaction = this.miTransaccion; 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@pApplicationFormSettingId", oLogAplicanteBE.IdConfiguracionAplicacion));
            cmd.Parameters.Add(new SqlParameter("@pFirstName", oLogAplicanteBE.PrimerNombre));
            cmd.Parameters.Add(new SqlParameter("@pLastName", oLogAplicanteBE.Apellidos));
            cmd.Parameters.Add(new SqlParameter("@pBirthDate", oLogAplicanteBE.FechaNacimiento));
            cmd.Parameters.Add(new SqlParameter("@pEmail", oLogAplicanteBE.Email));
            cmd.Parameters.Add(new SqlParameter("@pExplorador", oLogAplicanteBE.Explorador));
            cmd.Parameters.Add(new SqlParameter("@pIdiomaExplorador", oLogAplicanteBE.IdiomaExplorador));
            cmd.Parameters.Add(new SqlParameter("@pError", oLogAplicanteBE.Error));
            cmd.Parameters.Add("@MiRegInsert", SqlDbType.Int).Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                codInsertado = (Int32)cmd.Parameters["@MiRegInsert"].Value;

            }
            catch (Exception ex)
            {
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
            return codInsertado;
        }

        public Int32 insertaLogXml(Int32 ApplicantId, string xmlaplicante)
        {
            //Int32 resultado = 0;
            Int32 codInsertado = 0;
            SqlConnection myCon = new SqlConnection();           
            myCon = this.getConexion();
            myCon.Open();
            cmd = new SqlCommand("UP_InsertaApplicantLogXml", myCon);   
            cmd.Transaction = this.miTransaccion; 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", ApplicantId));
            cmd.Parameters.Add(new SqlParameter("@VC_XMLText", xmlaplicante));
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                myCon.Close();
            }
            return codInsertado;
            //return resultado;
        }

        #endregion "Metodos"
    }
}
