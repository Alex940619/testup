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
    public class EmpleadorDL : ConexionBD
    {
        #region "Constructores"

        public EmpleadorDL()
            : base()
        { }

        public EmpleadorDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
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
        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        public Boolean insertarExperienciaLaboral(AplicanteBE oAplicanteBE, EmpleadorBE oEmpleadorBE, Boolean transaccionIniciada)
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
                cmd = new SqlCommand("UPAdmPre_InsExperienciaLaboral", myCon);   
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oEmpleadorBE.IdEmpleador.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EmpleadorId", oEmpleadorBE.IdEmpleador));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EmpleadorId", DBNull.Value));
                }
                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", DBNull.Value));
                }
                if (oEmpleadorBE.NombreEmpleador != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EmployerName", oEmpleadorBE.NombreEmpleador));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EmployerName", DBNull.Value));
                }
                if (oEmpleadorBE.Cargo != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EmployerCharge", oEmpleadorBE.Cargo));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EmployerCharge", DBNull.Value));
                }
                if (oEmpleadorBE.FechaIngreso.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", oEmpleadorBE.FechaIngreso));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", DBNull.Value));
                }
                if (oEmpleadorBE.FechaSalida.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", oEmpleadorBE.FechaSalida));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oEmpleadorBE.Revision_Opid));
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

        public Boolean EliminaExperienciaLaboralRegistrada(Int32? IdEmpleo, Int32? AplicanteId)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();           
            try
            {
                myCon = this.getConexion();
                myCon.Open();
                cmd = new SqlCommand("UPAdmPre_EliExperienciaLaboral", myCon);   
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEmploymentId", IdEmpleo));
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
