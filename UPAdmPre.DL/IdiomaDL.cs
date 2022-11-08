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
    public class IdiomaDL : ConexionBD
    {
        #region "Constructores"

        public IdiomaDL()
            : base()
        { }

        public IdiomaDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
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

        public Boolean insertarIdiomaAplicante(AplicanteBE oAplicanteBE, IdiomaBE oIdiomaBE, Boolean transaccionIniciada)
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
                cmd = new SqlCommand("UPAdmPre_InsIdioma", myCon);  
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oIdiomaBE.IdApplicationEducation.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", oIdiomaBE.IdApplicationEducation));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                cmd.Parameters.Add(new SqlParameter("@VC_IdiomaId", oIdiomaBE.IdIdioma));
                cmd.Parameters.Add(new SqlParameter("@VC_Nivel_Lectura", oIdiomaBE.NivelLectura));
                cmd.Parameters.Add(new SqlParameter("@VC_Nivel_Escritura", oIdiomaBE.NivelEscritura));
                cmd.Parameters.Add(new SqlParameter("@VC_Nivel_Habla", oIdiomaBE.NivelHabla));
                cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oIdiomaBE.Revision_Opid));

                if (oIdiomaBE.CertificacionId.HasValue && oIdiomaBE.CertificacionId != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CertificacionId", oIdiomaBE.CertificacionId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CertificacionId", DBNull.Value));
                }
                if (oIdiomaBE.OtrosIdiomas != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtrosIdiomas", oIdiomaBE.OtrosIdiomas));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtrosIdiomas", DBNull.Value));
                }
                
                //if (oIdiomaBE.OtrasCertificaciones != null)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@VC_OtraCertificacion", oIdiomaBE.OtrasCertificaciones));
                //}
                //else
                //{
                //    cmd.Parameters.Add(new SqlParameter("@VC_OtraCertificacion", DBNull.Value));
                //}
                //if (oIdiomaBE.Puntaje != null)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@VC_Puntaje", oIdiomaBE.Puntaje));
                //}
                //else
                //{
                //    cmd.Parameters.Add(new SqlParameter("@VC_Puntaje", DBNull.Value));
                //}
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

        public Boolean EliminaIdiomaRegistrado(Int32? IdApplicationEducation, Int32? AplicanteId)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();           
            try
            {
                myCon = this.getConexion();
                myCon.Open();
                cmd = new SqlCommand("UPAdmPre_EliIdioma", myCon);  
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", IdApplicationEducation));
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
