using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using UPAdmPre.BE;
using UPAdmPre.SL;

namespace UPAdmPre.DL
{
    public class ActividadDL : ConexionBD
    {
        #region "Constructores"

        public ActividadDL()
            : base()
        { }

        public ActividadDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
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

        public Boolean insertarActividadAplicante(ActividadBE oActividadBE, Boolean transaccionIniciada)
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
                cmd = new SqlCommand("UPAdmPre_InsActExtracurricular", myCon);    
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oActividadBE.IdApplicationActivity.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationActivityId", oActividadBE.IdApplicationActivity));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationActivityId", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oActividadBE.IdAplicacion));

                if (oActividadBE.IdTipoActividad.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Activity", oActividadBE.IdTipoActividad));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Activity", DBNull.Value));
                }

                if (oActividadBE.Posicion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Position", oActividadBE.Posicion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Position", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_HoursPerWeek", oActividadBE.HorasPorSemana));
                cmd.Parameters.Add(new SqlParameter("@VC_WeeksPerYear", oActividadBE.SemanasPorAnho));
                cmd.Parameters.Add(new SqlParameter("@VC_NumberOfYears", oActividadBE.NumeroAnhos));
                cmd.Parameters.Add(new SqlParameter("@VC_ParticipatedGrade09", oActividadBE.GradoParticipacion09));
                cmd.Parameters.Add(new SqlParameter("@VC_ParticipatedGrade10", oActividadBE.GradoParticipacion10));
                cmd.Parameters.Add(new SqlParameter("@VC_ParticipatedGrade11", oActividadBE.GradoParticipacion11));
                cmd.Parameters.Add(new SqlParameter("@VC_ParticipatedGrade12", oActividadBE.GradoParticipacion12));
                cmd.Parameters.Add(new SqlParameter("@VC_ParticipatedPostSecondary", oActividadBE.ParticipacionSecundaria));
                cmd.Parameters.Add(new SqlParameter("@VC_ActivityName", oActividadBE.NombreActividad));

                if (oActividadBE.FechaInicio.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", oActividadBE.FechaInicio));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", DBNull.Value));
                }
                if (oActividadBE.FechaFin.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", oActividadBE.FechaFin));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", DBNull.Value));
                }
                if (oActividadBE.EsPromovidoPorColegio.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_isPromotedSchool", oActividadBE.EsPromovidoPorColegio));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_isPromotedSchool", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oActividadBE.Revision_Opid));

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

        public Boolean EliminaActividad(ActividadBE oActividadBE)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();
                cmd = new SqlCommand("UPAdmPre_EliActExtracurricular", myCon);    
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationActivityId", oActividadBE.IdApplicationActivity));
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oActividadBE.IdAplicacion));
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
