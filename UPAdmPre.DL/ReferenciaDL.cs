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
    public class ReferenciaDL : ConexionBD
    {
        #region "Constructores"

        public ReferenciaDL()
        { }

        public ReferenciaDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
            : base()
        {
            this.connection = miConexion;
            this.miTransaccion = miTransaccion;
        }

        #endregion "Constructores"

        #region "Atributos"

        private SqlCommand cmd;

        #endregion

        #region "Métodos No Transaccionales"

        //public ReferenciaBE ListarDatosReferenciaPorEmail(String strEmail, Int32? NumRefe)
        //{
        //    DataSet ds = null;
        //    ReferenciaBE oReferenciaBE = null;
        //    try
        //    {
        //        myCon = this.getConexion();
        //        cmd = new SqlCommand();
        //        cmd.Connection = this.connection;

        //        myCon.Open();

        //        cmd.CommandText = "UPAdmPre_SelReferentePorEmail";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.Add(new SqlParameter("@VC_Email", strEmail));
        //        cmd.Parameters.Add(new SqlParameter("@VC_NumRef", NumRefe));

        //        cmd.ExecuteNonQuery();
        //        ds = new DataSet();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            oReferenciaBE = this.poblarDatosReferencias(ds.Tables[(int)UIConstantes.ESTRUCTURA_TABLAS.REFERENCIAS], oReferenciaBE);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        ds = null;
        //        cmd.Dispose();
        //        myCon.Close();
        //    }
        //    return oReferenciaBE;
        //}

        private ReferenciaBE poblarDatosReferencias(DataTable dtReferencia, ReferenciaBE oReferenciaBE)
        {
            if (dtReferencia != null && dtReferencia.Rows.Count > 0)
            {
                foreach (DataRow drReferencia in dtReferencia.Rows)
                {
                    oReferenciaBE = new ReferenciaBE();
                    if (drReferencia["Email"] != DBNull.Value)
                    {
                        oReferenciaBE.Email = UIConvertNull.String(drReferencia["Email"].ToString());
                    }
                    if (drReferencia["FirstName"] != DBNull.Value)
                    {
                        oReferenciaBE.Firstname = UIConvertNull.String(drReferencia["FirstName"].ToString());
                    }
                    if (drReferencia["LastName"] != DBNull.Value)
                    {
                        oReferenciaBE.LastName = UIConvertNull.String(drReferencia["LastName"].ToString());
                    }
                    if (drReferencia["OrganizationName"] != DBNull.Value)
                    {
                        oReferenciaBE.NombreOrganizacion = UIConvertNull.String(drReferencia["OrganizationName"].ToString());
                    }
                    if (drReferencia["Charge"] != DBNull.Value)
                    {
                        oReferenciaBE.Cargo = UIConvertNull.String(drReferencia["Charge"].ToString());
                    }
                    if (drReferencia["PeopleName"] != DBNull.Value)
                    {
                        oReferenciaBE.NombrePersona = UIConvertNull.String(drReferencia["PeopleName"].ToString());
                    }
                    if (drReferencia["NumRef"] != DBNull.Value)
                    {
                        oReferenciaBE.NumReferencia = UIConvertNull.Int32(drReferencia["NumRef"].ToString());
                    }
                }
            }
            return oReferenciaBE;
        }

        public DataTable ObtenerApplicationReferencebyId(Int32? ReferenteID)
        {
            DataSet ds = null;
            SqlConnection myCon = new SqlConnection();
            myCon = this.getConexion();
            try
            {
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_ObtenerApplicationReferencebyId", myCon); 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationReferenceId", ReferenteID));
                cmd.ExecuteNonQuery();

                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
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
            if (ds != null) { return ds.Tables[0]; } else { return null; }
        }
        public string ReenvioenviaEmailReferenciaAplicante(Int32? ApplicantId, String ApplicantIdEncrypt, Int32? Reenvio, Boolean transaccionIniciada)
        {
            bool respuesta = true;
            SqlConnection myCon = new SqlConnection();

            if (!transaccionIniciada)
            {
                myCon = this.getConexion();
                myCon.Open();
            }
            try
            {
                DataSet ds = null;
                DataTable dt = null;

                myCon = this.connection; 
                cmd = new SqlCommand("UPAdmPre_EnviaCorreoReferente", myCon);
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@ApplicationReferenceId", ApplicantId));
                cmd.Parameters.Add(new SqlParameter("@AppIdEncrypt", ApplicantIdEncrypt));
                cmd.Parameters.Add(new SqlParameter("@ReenvioEmail", Reenvio));
                cmd.Parameters.Add(new SqlParameter("@ReenvioFormulario", 1));
                cmd.ExecuteNonQuery();

                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null) { dt = ds.Tables[0]; }
                if (dt.Rows.Count > 0) { return dt.Rows[0]["Msg"].ToString(); }
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
            return "";
        }

        public Boolean enviaEmailReferenciaAplicante(Int32? ApplicantId, String ApplicantIdEncrypt, Boolean transaccionIniciada)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }
                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_EnviaCorreoReferente", myCon);
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@ApplicationReferenceId", ApplicantId));
                cmd.Parameters.Add(new SqlParameter("@AppIdEncrypt", ApplicantIdEncrypt));
                cmd.Parameters.Add(new SqlParameter("@ReenvioEmail", null));
                cmd.Parameters.Add(new SqlParameter("@ReenvioFormulario", 1));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Respuesta = false;
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
            return Respuesta;
        }

        public Boolean enviaErrorEmailReferenciaAplicante(Int32? ApplicantId, String ApplicantIdEncrypt, String ApplicationIdEncrypt, Boolean transaccionIniciada)
        {
            bool respuesta = true;
            SqlConnection myCon = new SqlConnection();

            if (!transaccionIniciada)
            {
                myCon = this.getConexion();
                myCon.Open();
            }
            try
            {
                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_EnviaErrorCorreoRefAplic", myCon);
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationReferenceId", ApplicantId));
                cmd.Parameters.Add(new SqlParameter("@VC_AppIdEncrypt", ApplicantIdEncrypt));
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationIdEncrypt", ApplicationIdEncrypt));
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

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        public Boolean insertarReferenciaAplicante(AplicanteBE oAplicanteBE, ReferenciaBE oReferenciaBE, Boolean transaccionIniciada)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }
                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_InsReferencia", myCon);
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oReferenciaBE.IdReferencia.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ReferenceId", oReferenciaBE.IdReferencia));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ReferenceId", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                cmd.Parameters.Add(new SqlParameter("@VC_PeopleName", oReferenciaBE.NombrePersona));
                cmd.Parameters.Add(new SqlParameter("@VC_OrganizationName", oReferenciaBE.NombreOrganizacion));
                cmd.Parameters.Add(new SqlParameter("@VC_Charge", oReferenciaBE.Cargo));
                cmd.Parameters.Add(new SqlParameter("@VC_Email", oReferenciaBE.Email));
                cmd.Parameters.Add(new SqlParameter("@VC_FirstName", oReferenciaBE.Firstname));
                cmd.Parameters.Add(new SqlParameter("@VC_LastName", oReferenciaBE.LastName));
                cmd.Parameters.Add(new SqlParameter("@VC_Status", oReferenciaBE.Status));
                cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oReferenciaBE.Revision_Opid));
                cmd.Parameters.Add("@ApplicationReference", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                oReferenciaBE.IdReferencia = (int)cmd.Parameters["@ApplicationReference"].Value;
            }
            catch (Exception ex)
            {
                Respuesta = false;
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
            return Respuesta;
        }

        public bool modificarReferencia(ReferenciaBE oReferenciaBE)
        {
            bool respuesta = true;

            SqlConnection myCon = new SqlConnection();
            myCon = this.getConexion();
            
            try
            {
                myCon.Open();
                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_ModificaReferenciaAplicante", myCon);
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationReferenceId", oReferenciaBE.IdReferencia));
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oReferenciaBE.IdAplicacion));
                cmd.Parameters.Add(new SqlParameter("@VC_PeopleName", oReferenciaBE.NombrePersona));
                cmd.Parameters.Add(new SqlParameter("@VC_OrganizationName", oReferenciaBE.NombreOrganizacion));
                cmd.Parameters.Add(new SqlParameter("@VC_Charge", oReferenciaBE.Cargo));
                if (oReferenciaBE.Telefono != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Phone", oReferenciaBE.Telefono));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Phone", DBNull.Value));
                }

                if (oReferenciaBE.Organizacion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OrganizationId", oReferenciaBE.Organizacion.Codigo));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OrganizationId", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oReferenciaBE.Revision_Opid));
                cmd.Parameters.Add(new SqlParameter("@VC_FirstName", oReferenciaBE.Firstname));
                cmd.Parameters.Add(new SqlParameter("@VC_LastName", oReferenciaBE.LastName));
                cmd.Parameters.Add(new SqlParameter("@VC_Email", oReferenciaBE.Email));
                cmd.Parameters.Add(new SqlParameter("@VC_Status", oReferenciaBE.Status));
                cmd.Parameters.Add(new SqlParameter("@VC_ErrorEnvioEmail", oReferenciaBE.ErrorEnvioEmail));
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
                myCon.Close();
            }
            return respuesta;
        }

        public Boolean EliminaInfoReferenciaRegistrada(Int32? IdReferencia, Int32? AplicanteId)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open(); 
                cmd = new SqlCommand("UPAdmPre_EliInfoReferencia", myCon);
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationReferenceId", IdReferencia));
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Respuesta = false;
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                myCon.Close();
            }
            return Respuesta;
        }

        #endregion "Métodos Transaccionales"
    }
}
