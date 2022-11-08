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
    public class RelacionDL : ConexionBD
    {
        #region "Constructores"

        public RelacionDL()
            : base()
        {
        }

        public RelacionDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
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
        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        public Boolean insertarRelacionAplicante(AplicanteBE oAplicanteBE, RelacionBE oRelacionBE, Boolean transaccionIniciada)
        {
            Boolean respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }
                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_InsInfoPadres", myCon); 
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oRelacionBE.IdApplicationRelationship.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationRelationshipId", oRelacionBE.IdApplicationRelationship));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationRelationshipId", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                if (oRelacionBE.IdTipoRelacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RelationType", oRelacionBE.IdTipoRelacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RelationType", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_FirstName", oRelacionBE.Nombres));
                cmd.Parameters.Add(new SqlParameter("@VC_LastName", oRelacionBE.Apellido));
                cmd.Parameters.Add(new SqlParameter("@VC_AttendedInstitution", oRelacionBE.AsistenciaInstitucion));
                if (oRelacionBE.CorreoPersonal != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", oRelacionBE.CorreoPersonal));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", DBNull.Value));
                }
                if(oRelacionBE.TipoDocumento!=null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoDocIde", oRelacionBE.TipoDocumento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoDocIde", DBNull.Value));
                }
                if (oRelacionBE.Documento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovernmentId", oRelacionBE.Documento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovernmentId", DBNull.Value));
                }
                if (oRelacionBE.NumeroTelefono != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PhoneNumber", oRelacionBE.NumeroTelefono));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PhoneNumber", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_IsStudyUP", oRelacionBE.EstudioAntesUP));
                cmd.Parameters.Add(new SqlParameter("@VC_Compartir", oRelacionBE.Compartir));

                if (oRelacionBE.Fallecido != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Deceased", oRelacionBE.Fallecido));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Deceased", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oRelacionBE.Revision_Opid));
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
        public Boolean Preformalizacion_ActualizarRelacionAplicante(AplicanteBE oAplicanteBE, RelacionBE oRelacionBE, Boolean transaccionIniciada)
        {
            Boolean respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }
                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ActualizarInfoPadres", myCon);
                cmd.Transaction = this.miTransaccion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oRelacionBE.IdApplicationRelationship.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationRelationshipId", oRelacionBE.IdApplicationRelationship));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationRelationshipId", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                if (oRelacionBE.IdTipoRelacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RelationType", oRelacionBE.IdTipoRelacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RelationType", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_FirstName", oRelacionBE.Nombres));
                cmd.Parameters.Add(new SqlParameter("@VC_LastName", oRelacionBE.Apellido));
                cmd.Parameters.Add(new SqlParameter("@VC_AttendedInstitution", oRelacionBE.AsistenciaInstitucion));
                if (oRelacionBE.CorreoPersonal != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", oRelacionBE.CorreoPersonal));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", DBNull.Value));
                }
                if (oRelacionBE.TipoDocumento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoDocIde", oRelacionBE.TipoDocumento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoDocIde", DBNull.Value));
                }
                if (oRelacionBE.Documento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovernmentId", oRelacionBE.Documento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovernmentId", DBNull.Value));
                }
                if (oRelacionBE.NumeroTelefono != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PhoneNumber", oRelacionBE.NumeroTelefono));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PhoneNumber", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_IsStudyUP", oRelacionBE.EstudioAntesUP));
                cmd.Parameters.Add(new SqlParameter("@VC_Compartir", oRelacionBE.Compartir));

                if (oRelacionBE.Fallecido != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Deceased", oRelacionBE.Fallecido));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Deceased", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oRelacionBE.Revision_Opid));
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

        public Boolean EliminaInfoPadresRegistrada(Int32? IdRelacionFam, Int32? AplicanteId)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();
                cmd = new SqlCommand("UPAdmPre_EliInfoPadres", myCon); 
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationRelationshipId", IdRelacionFam));
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
