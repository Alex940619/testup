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
    public class GeneralDL : ConexionBD
    {
        public GeneralDL()
        { }

        #region "Atributos"

        private SqlCommand cmd;

        #endregion

        #region "Métodos No Transaccionales"
        /*Usuario Creacion: Jaqueline DB
         Fecha Creaciom: 07/04/2020 */
        public DataTable ListarPeridoRadioButton()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPPRESPListarPerido", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
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

        public DataTable ObtenerTipoCodigoPC(String Tipo, String Name, Int32? TipoPost, Int32? ModPostulacion = null) /*Se modifica: Christian Ramirez - GIIT[caso 56427] - 20190108*/
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            { 
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelTablaMaestra", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@tipo", Tipo));
                cmd.Parameters.Add(new SqlParameter("@name", Name));
                cmd.Parameters.Add(new SqlParameter("@Parametro1", TipoPost));
                cmd.Parameters.Add(new SqlParameter("@ModPostulacion", ModPostulacion)); /*Se agrega: Christian Ramirez - GIIT[caso 56427] - 20190108*/
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

        //Inicio JC.DelgadoV [Preformalización]
        public DataTable PreformalizacionObtenerExamenesXModalidad(int AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ListarExamenesXModalidad", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_IdAplicante", AplicanteId));
                
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

        public DataTable PreformalizacionObtenerCursosConvalidar(int AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ListarCursosConvalidar", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_IdAplicante", AplicanteId));

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

        public DataTable PreformalizacionObtenerDatosFinales(int AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ObtenerDatosFinales", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", AplicanteId));

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

        public DataTable PreformalizacionObtenerEstado(int AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ObtenerEstado", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", AplicanteId));

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

        public DataTable PreformalizacionObtenerAutorizacionDatos(int AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ObtenerAutorizacionDatos", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", AplicanteId));

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
        //Fin JC.DelgadoV [Preformalización]

        public DataTable ObtenerDescripcionModalidad(Int32? IdModalidad, Int32? IdCursos, Int32? ApplicationFormSettingId,string AnioPerido)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelDescripcionModalidad", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_DegreeId", IdModalidad));
                if (IdCursos != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_IdCursos", IdCursos));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_IdCursos", DBNull.Value));
                }
                if (ApplicationFormSettingId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", ApplicationFormSettingId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", DBNull.Value));
                }
                if (ApplicationFormSettingId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_AnioPerido", AnioPerido));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_AnioPerido", DBNull.Value));
                }
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

        public DataTable ObtenerDescripcionDePrograma(Int32? IdPrograma)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelDescripcionPrograma", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@DegreeId", IdPrograma));

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

        public DataTable obtenerDepartamentoPorId(Int32? idPais)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelDpto_PorId", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_PaisId", idPais));

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

        public DataTable obtenerProvinciaPorId(Int32? idDpto)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelProvincia_PorId", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_DptoId", idDpto));

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

        public DataTable obtenerDistritoPorId(Int32? idProvincia)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelDistrito_PorId", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ProvId", idProvincia));

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

        public DataTable ObtenerDocsPorModalidadPostulacion(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SELDocPendiente", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_IdAplicante", AplicanteId));

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

        //Inicio JC.DelgadoV [Preformalización]
        public DataTable PreformalizacionObtenerDocsPorModalidadPostulacion(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_SelDocPendiente", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_IdAplicante", AplicanteId));

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
        //Fin JC.DelgadoV [Preformalización]

        public DataTable ObtenerDocsAdicionales(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelDocAdicionales", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_IdAplicante", AplicanteId));

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

        public DataTable obtenerMensajeporId(Int32 TextId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelMensajePorTextID", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_TextID", TextId));

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

        public DataTable obtenerSiguientePagina(Int32? ModalidadId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelSiguientePagina", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ModalidadId", ModalidadId));

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

        public DataTable GetPasosInscripcion(int? modalidadId,int? applicationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelPasosInscripcion", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@ModalidadId", modalidadId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", applicationId));

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

        /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
        public DataTable ObtenerHorariosDeEntrevista(Int32? AplicanteId, string tipEvaluacion = "")
        /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_ObtenerHorariosDeEntrevista", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationID", AplicanteId));
                cmd.Parameters.Add(new SqlParameter("@VC_TipoEvaluacion", tipEvaluacion)); /*Se agrega:Christian Ramirez - GIIT[caso60747] - 20190521*/
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

        public DataTable ObtenerHorariosDeFormalizacion(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_ObtenerHorariosFormalizacion", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationID", AplicanteId));
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

        public DataTable ObtenerDescripcionPeriodo(Int32? IdPeriodo, Int32?  ApplicationFormSettingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelDescripcionPeriodo", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_sessionid", IdPeriodo));                
                if (ApplicationFormSettingId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", ApplicationFormSettingId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", DBNull.Value));
                }
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

        public DataTable ObtenerDescripcionBeca(Int32? IdPeriodo, Int32? ApplicationFormSettingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelDescripcionBeca", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_sessionid", IdPeriodo));
                if (ApplicationFormSettingId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", ApplicationFormSettingId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", DBNull.Value));
                }
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
        public DataTable ObtenerDescripcionAviso(Int32? IdPeriodo, Int32? ApplicationFormSettingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelDescripcionAviso", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_sessionid", IdPeriodo));
                if (ApplicationFormSettingId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", ApplicationFormSettingId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", DBNull.Value));
                }
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
        
        public DataTable ObtenerDescTipoEvaluacion(Int32? IdPeriodo, Int32? ApplicationFormSettingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelDescripcionTipoEvaluacion", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_sessionid", IdPeriodo));
                if (ApplicationFormSettingId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", ApplicationFormSettingId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", DBNull.Value));
                }
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

        //INI: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRC)
        public DataSet PreformalizacionObtenerEncuesta(int AplicanteId)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ObtenerEncuestaRGC", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", AplicanteId));

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
            }
            return ds;
        }

        public DataTable PreformalizacionComprobarEncuesta(int AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_EstadoEncuestaRGC", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", AplicanteId));

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

        public DataTable PreformalizacionLlenarEncuesta(int AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_LlenarEncuestaRGC", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", AplicanteId));

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

        public DataTable PreformalizacionGuardarEncuesta(AplicanteBE oAplicanteBE)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_GuardarEncuestaRGC", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", oAplicanteBE.IdAplicante));
                cmd.Parameters.Add(new SqlParameter("@EncuestaRGC_ResP1", oAplicanteBE.EncuestaRGC_ResP1));
                cmd.Parameters.Add(new SqlParameter("@EncuestaRGC_ResP2", oAplicanteBE.EncuestaRGC_ResP2));
                cmd.Parameters.Add(new SqlParameter("@EncuestaRGC_ResP3", oAplicanteBE.EncuestaRGC_ResP3));
                cmd.Parameters.Add(new SqlParameter("@EncuestaRGC_ResP4", oAplicanteBE.EncuestaRGC_ResP4));
                if(oAplicanteBE.EncuestaRGC_ResP5 != null && oAplicanteBE.EncuestaRGC_ResP5 != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@EncuestaRGC_ResP5", oAplicanteBE.EncuestaRGC_ResP5));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@EncuestaRGC_ResP5", DBNull.Value));
                }

                if (oAplicanteBE.EncuestaRGC_ResP6 != null && oAplicanteBE.EncuestaRGC_ResP6 != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@EncuestaRGC_ResP6", oAplicanteBE.EncuestaRGC_ResP6));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@EncuestaRGC_ResP6", DBNull.Value));
                }

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
        //FIN: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRC)

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"
        #endregion "Métodos Transaccionales"
    }
}
