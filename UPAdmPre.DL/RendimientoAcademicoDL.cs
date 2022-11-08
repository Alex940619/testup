using System;
using System.Data;
using System.Data.SqlClient;
using UPAdmPre.BE;
using UPAdmPre.SL;

namespace UPAdmPre.DL
{
    public class RendimientoAcademicoDL : ConexionBD
    {
        public RendimientoAcademicoDL()
        {
        }

        public RendimientoAcademicoDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
            : base()
        {
            this.connection = miConexion;
            this.miTransaccion = miTransaccion;
        }

        #region Metodos No Transaccionales
        public DataTable ObtenerModalidadesPorCompetencia(string redid,
            RendimientoAcademicoBE oRendimientoAcademicoBE)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            myCon = getConexion();
            myCon.Open();

            cmd = new SqlCommand("UPAdmPre_RendAcademico_ObtenerModalidadesCompetencia", myCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RedId", redid));
            cmd.Parameters.Add(new SqlParameter("@SessionPeriodId", oRendimientoAcademicoBE.SessionPeriodId));
            //cmd.Parameters.Add(new SqlParameter("@CodigoModular", oRendimientoAcademicoBE.CodigoModular));
            if (oRendimientoAcademicoBE.CodigoModular2 != "" && oRendimientoAcademicoBE.CodigoModular2 != null)
            {
                if (oRendimientoAcademicoBE.CodigoModular3 != "" && oRendimientoAcademicoBE.CodigoModular3 != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@CodigoModular", oRendimientoAcademicoBE.CodigoModular + "," + oRendimientoAcademicoBE.CodigoModular2 + "," + oRendimientoAcademicoBE.CodigoModular3));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@CodigoModular", oRendimientoAcademicoBE.CodigoModular + "," + oRendimientoAcademicoBE.CodigoModular2));
                }
            }
            else
                cmd.Parameters.Add(new SqlParameter("@CodigoModular", oRendimientoAcademicoBE.CodigoModular));

            cmd.Parameters.Add(new SqlParameter("@TotalCompetenciaCuarto", oRendimientoAcademicoBE.TotalCompentencias_Cuarto));
            cmd.Parameters.Add(new SqlParameter("@TotalADsCuarto", oRendimientoAcademicoBE.TotalCompetencia_AD_Cuarto));
            cmd.Parameters.Add(new SqlParameter("@TotalAsCuarto", oRendimientoAcademicoBE.TotalCompetencia_A_Cuarto));
            cmd.Parameters.Add(new SqlParameter("@TotalBsCuarto", oRendimientoAcademicoBE.TotalCompetencia_B_Cuarto));
            cmd.Parameters.Add(new SqlParameter("@TotalCsCuarto", oRendimientoAcademicoBE.TotalCompetencia_C_Cuarto));
            cmd.Parameters.Add(new SqlParameter("@TotalCompetenciaQuinto", oRendimientoAcademicoBE.TotalCompentencias_Quinto));
            cmd.Parameters.Add(new SqlParameter("@TotalADsQuinto", oRendimientoAcademicoBE.TotalCompetencia_AD_Quinto));
            cmd.Parameters.Add(new SqlParameter("@TotalAsQuinto", oRendimientoAcademicoBE.TotalCompetencia_A_Quinto));
            cmd.Parameters.Add(new SqlParameter("@TotalBsQuinto", oRendimientoAcademicoBE.TotalCompetencia_B_Quinto));
            cmd.Parameters.Add(new SqlParameter("@TotalCsQuinto", oRendimientoAcademicoBE.TotalCompetencia_C_Quinto));
            cmd.Parameters.Add(new SqlParameter("@pOrdenMerito3", oRendimientoAcademicoBE.OrdenMeritoTercero));
            cmd.Parameters.Add(new SqlParameter("@pCantidadAlumno3", oRendimientoAcademicoBE.NroAlumnosPromocionTercero));
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (da != null) da.Dispose();
            cmd.Dispose();
            myCon.Close();

            return dt;
        }

        public DataTable ObtenerModalidadesPorRendimiento(string redid,
            RendimientoAcademicoBE oRendimientoAcademicoBE)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            myCon = getConexion();
            myCon.Open();

            cmd = new SqlCommand("UPAdmPre_RendAcademico_ObtenerModalidadesRendimiento", myCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RedId", redid));
            cmd.Parameters.Add(new SqlParameter("@SessionPeriodId", oRendimientoAcademicoBE.SessionPeriodId));
            //cmd.Parameters.Add(new SqlParameter("@CodigoModular", oRendimientoAcademicoBE.CodigoModular));

            if (oRendimientoAcademicoBE.CodigoModular2 != "" && oRendimientoAcademicoBE.CodigoModular2 != null)
            {
                if (oRendimientoAcademicoBE.CodigoModular3 != "" &&  oRendimientoAcademicoBE.CodigoModular3 != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@CodigoModular", oRendimientoAcademicoBE.CodigoModular + "," + oRendimientoAcademicoBE.CodigoModular2 + "," + oRendimientoAcademicoBE.CodigoModular3));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@CodigoModular", oRendimientoAcademicoBE.CodigoModular + "," + oRendimientoAcademicoBE.CodigoModular2));
                }
            }
            else
                cmd.Parameters.Add(new SqlParameter("@CodigoModular", oRendimientoAcademicoBE.CodigoModular));
            cmd.Parameters.Add(new SqlParameter("@pOrdenMerito3", oRendimientoAcademicoBE.OrdenMeritoTercero));
            cmd.Parameters.Add(new SqlParameter("@pCantidadAlumno3", oRendimientoAcademicoBE.NroAlumnosPromocionTercero));
            cmd.Parameters.Add(new SqlParameter("@pOrdenMerito4", oRendimientoAcademicoBE.OrdenMeritoCuarto));
            cmd.Parameters.Add(new SqlParameter("@pCantidadAlumno4", oRendimientoAcademicoBE.NroAlumnosPromocionCuarto));
            cmd.Parameters.Add(new SqlParameter("@pOrdenMerito5", oRendimientoAcademicoBE.OrdenMeritoQuinto));
            cmd.Parameters.Add(new SqlParameter("@pCantidadAlumno5", oRendimientoAcademicoBE.NroAlumnosPromocionQuinto));
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (da != null) da.Dispose();
            cmd.Dispose();
            myCon.Close();

            return dt;
        }

        public RendimientoAcademicoBE ObtenerNotasADsPorCompetenciaRegistrada_Cuarto(AplicanteBE oAplicanteBE)
        {
            RendimientoAcademicoBE oRendimientoAcademicoBE = null;
            using (SqlConnection myCon = getConexion())
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand("UPAdmPre_SelNotaADsCompetenciaRegistrada_Cuarto", myCon))
                {
                    SqlDataReader dr;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", oAplicanteBE.IdAplicante));
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            int ApplicationId = dr.GetOrdinal("ApplicationId");
                            int CodigoModular = dr.GetOrdinal("CodigoModular");
                            int DegreeId = dr.GetOrdinal("DegreeId");
                            int SessionPeriodId = dr.GetOrdinal("SessionPeriodId");
                            int TotalCompetencias = dr.GetOrdinal("TotalCompetencias");
                            int TotalCompetencia_AD = dr.GetOrdinal("TotalCompetencia_AD");
                            int TotalCompetencia_A = dr.GetOrdinal("TotalCompetencia_A");
                            int TotalCompetencia_B = dr.GetOrdinal("TotalCompetencia_B");
                            int TotalCompetencia_C = dr.GetOrdinal("TotalCompetencia_C");

                            oRendimientoAcademicoBE = new RendimientoAcademicoBE();
                            oRendimientoAcademicoBE.ApplicationId = Convert.ToInt32(dr[ApplicationId]);
                            oRendimientoAcademicoBE.CodigoModular = dr[CodigoModular].ToString();
                            oRendimientoAcademicoBE.DegreeId = Convert.ToInt32(dr[DegreeId]);
                            oRendimientoAcademicoBE.SessionPeriodId = Convert.ToInt32(dr[SessionPeriodId]);
                            oRendimientoAcademicoBE.TotalCompentencias_Cuarto = Convert.ToInt32(dr[TotalCompetencias]);
                            oRendimientoAcademicoBE.TotalCompetencia_AD_Cuarto = Convert.ToInt32(dr[TotalCompetencia_AD]);
                            oRendimientoAcademicoBE.TotalCompetencia_A_Cuarto = Convert.ToInt32(dr[TotalCompetencia_A]);
                            oRendimientoAcademicoBE.TotalCompetencia_B_Cuarto = Convert.ToInt32(dr[TotalCompetencia_B]);
                            oRendimientoAcademicoBE.TotalCompetencia_C_Cuarto = Convert.ToInt32(dr[TotalCompetencia_C]);
                        }

                    dr.Close();
                }
            }

            return oRendimientoAcademicoBE;
        }

        public RendimientoAcademicoBE ObtenerNotasADsPorCompetenciaRegistrada_Quinto(AplicanteBE oAplicanteBE)
        {
            RendimientoAcademicoBE oRendimientoAcademicoBE = null;
            using (SqlConnection myCon = getConexion())
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand("UPAdmPre_SelNotaADsCompetenciaRegistrada_Quinto", myCon))
                {
                    SqlDataReader dr;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", oAplicanteBE.IdAplicante));
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            int ApplicationId = dr.GetOrdinal("ApplicationId");
                            int CodigoModular = dr.GetOrdinal("CodigoModular");
                            int DegreeId = dr.GetOrdinal("DegreeId");
                            int SessionPeriodId = dr.GetOrdinal("SessionPeriodId");
                            int TotalCompetencias = dr.GetOrdinal("TotalCompetencias");
                            int TotalCompetencia_AD = dr.GetOrdinal("TotalCompetencia_AD");
                            int TotalCompetencia_A = dr.GetOrdinal("TotalCompetencia_A");
                            int TotalCompetencia_B = dr.GetOrdinal("TotalCompetencia_B");
                            int TotalCompetencia_C = dr.GetOrdinal("TotalCompetencia_C");

                            oRendimientoAcademicoBE = new RendimientoAcademicoBE();
                            oRendimientoAcademicoBE.ApplicationId = Convert.ToInt32(dr[ApplicationId]);
                            oRendimientoAcademicoBE.CodigoModular = dr[CodigoModular].ToString();
                            oRendimientoAcademicoBE.DegreeId = Convert.ToInt32(dr[DegreeId]);
                            oRendimientoAcademicoBE.SessionPeriodId = Convert.ToInt32(dr[SessionPeriodId]);
                            oRendimientoAcademicoBE.TotalCompentencias_Quinto = Convert.ToInt32(dr[TotalCompetencias]);
                            oRendimientoAcademicoBE.TotalCompetencia_AD_Quinto = Convert.ToInt32(dr[TotalCompetencia_AD]);
                            oRendimientoAcademicoBE.TotalCompetencia_A_Quinto = Convert.ToInt32(dr[TotalCompetencia_A]);
                            oRendimientoAcademicoBE.TotalCompetencia_B_Quinto = Convert.ToInt32(dr[TotalCompetencia_B]);
                            oRendimientoAcademicoBE.TotalCompetencia_C_Quinto = Convert.ToInt32(dr[TotalCompetencia_C]);
                        }

                    dr.Close();
                }
            }

            return oRendimientoAcademicoBE;
        }

        public int InsertarDatosFormVeintiUno_CantidadCompetencia(AplicanteBE oAplicanteBE)
        {
            int rpta = 0;
            
            using (SqlConnection myCon = getConexion())
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand("UPAdmPre_InsCantidadCompetencia", myCon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", oAplicanteBE.IdAplicante));
                    cmd.Parameters.Add(new SqlParameter("@VC_SessionPeriodId", oAplicanteBE.ListaRendimientoAcademicoBE[0].SessionPeriodId));
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeId", oAplicanteBE.ListaRendimientoAcademicoBE[0].DegreeId));
                    cmd.Parameters.Add(new SqlParameter("@VC_CodigoModular", oAplicanteBE.ListaRendimientoAcademicoBE[0].CodigoModular));

                    //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                    if (oAplicanteBE.SituacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.ESTUDIANTE)
                    {
                        cmd.Parameters.Add(new SqlParameter("@VC_TotalCompetencia_Tercero", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompentencias_Tercero));
                        cmd.Parameters.Add(new SqlParameter("@VC_TotalADs_Tercero", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_AD_Tercero));
                        cmd.Parameters.Add(new SqlParameter("@VC_TotalAs_Tercero", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_A_Tercero));
                        cmd.Parameters.Add(new SqlParameter("@VC_TotalBs_Tercero", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_B_Tercero));
                        cmd.Parameters.Add(new SqlParameter("@VC_TotalCs_Tercero", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_C_Tercero));
                    }
                    //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

                    cmd.Parameters.Add(new SqlParameter("@VC_TotalCompetencia_Cuarto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompentencias_Cuarto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalADs_Cuarto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_AD_Cuarto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalAs_Cuarto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_A_Cuarto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalBs_Cuarto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_B_Cuarto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalCs_Cuarto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_C_Cuarto));

                    cmd.Parameters.Add(new SqlParameter("@VC_TotalCompetencia_Quinto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompentencias_Quinto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalADs_Quinto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_AD_Quinto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalAs_Quinto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_A_Quinto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalBs_Quinto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_B_Quinto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalCs_Quinto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_C_Quinto));

                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));

                    SqlParameter parOut = new SqlParameter("@RowCount", SqlDbType.Int);
                    parOut.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parOut);

                    cmd.ExecuteNonQuery();
                    rpta = (int)cmd.Parameters["@RowCount"].Value;
                }
            }

            return rpta;
        }

        public int InsertarDatosFormVeinte_CantidadCompetencia(AplicanteBE oAplicanteBE)
        {
            int rpta = 0;

            using (SqlConnection myCon = getConexion())
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand("UPAdmPre_InsCantidadCompetenciaFormalizacion", myCon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", oAplicanteBE.IdAplicante));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalCompetencia_Quinto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompentencias_Quinto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalADs_Quinto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_AD_Quinto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalAs_Quinto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_A_Quinto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalBs_Quinto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_B_Quinto));
                    cmd.Parameters.Add(new SqlParameter("@VC_TotalCs_Quinto", oAplicanteBE.ListaRendimientoAcademicoBE[0].TotalCompetencia_C_Quinto));
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));

                    SqlParameter parOut = new SqlParameter("@RowCount", SqlDbType.Int);
                    parOut.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parOut);

                    cmd.ExecuteNonQuery();
                    rpta = (int)cmd.Parameters["@RowCount"].Value;
                }
            }

            return rpta;
        }

        public DataTable ObtenerListaCalificacionRendimientoAcademico()
        {
            DataTable dt = new DataTable();
           
            using (SqlConnection myCon = getConexion())
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand("UPAdmPre_SelListaCalificacionRendimientoAcademico", myCon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (da != null) da.Dispose();
                }
            }

            return dt;
        }

        public int InsertarDatosFormVeintiUno_NotasADsCompetencia()
        {
            int rpta = 0;

            using (SqlConnection myCon = getConexion())
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand("UPAdmPre_InsRendimientoAcademicoCompetencias", myCon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
            }

            return rpta;    
        }

        public bool InsertarCompentencias(RendimientoAcademicoBE oRendimientoAcademicoBE
            , RendimientoAcademicoEvaluacionBE oRendimientoAcademicoEvaluacionBE,  string RedId)
        {
            bool rpta = true;
            SqlConnection myCon = this.connection;

            using (SqlCommand cmd = new SqlCommand("UPAdmPre_InsRendimientoAcademicoCompetencia", myCon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = this.miTransaccion;

                cmd.Parameters.Add(new SqlParameter("@ApplicationId", oRendimientoAcademicoBE.ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationEducationId", oRendimientoAcademicoBE.ApplicationEducationId));
                cmd.Parameters.Add(new SqlParameter("@DegreeId", oRendimientoAcademicoBE.DegreeId));
                cmd.Parameters.Add(new SqlParameter("@RedID", RedId));
                cmd.Parameters.Add(new SqlParameter("@CursoId", oRendimientoAcademicoEvaluacionBE.CursoId));
                cmd.Parameters.Add(new SqlParameter("@CompetenciaId", oRendimientoAcademicoEvaluacionBE.CompetenciaId));

                if (oRendimientoAcademicoEvaluacionBE.CalificacionId != null)
                {
                    int? calificacionId = oRendimientoAcademicoEvaluacionBE.CalificacionId.Value == 0 ? null
                        : oRendimientoAcademicoEvaluacionBE.CalificacionId;
                    cmd.Parameters.Add(new SqlParameter("@CalificacionId", calificacionId));
                }


                int result = cmd.ExecuteNonQuery();
                if (result <= 0) rpta = false;
            }

            return rpta;
        }


        //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ103573
        public DataTable ObtenerModalidadesParaGraduadoTraslado(string redid, int sessionPeriodId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            myCon = getConexion();
            myCon.Open();

            cmd = new SqlCommand("UPAdmPre_ObtenerModalidadesGraduadoTraslado", myCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RedId", redid));
            cmd.Parameters.Add(new SqlParameter("@SessionPeriodId", sessionPeriodId));

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (da != null) da.Dispose();
            cmd.Dispose();
            myCon.Close();

            return dt;
        }

        public DataTable ObtenerModalidadesParaAdmisionPrePacifico(string redid, int sessionPeriodId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            myCon = getConexion();
            myCon.Open();

            cmd = new SqlCommand("UPAdmPre_ObtenerModalidadesAdmisionPrePacifico", myCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RedId", redid));
            cmd.Parameters.Add(new SqlParameter("@SessionPeriodId", sessionPeriodId));

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (da != null) da.Dispose();
            cmd.Dispose();
            myCon.Close();

            return dt;
        }
        //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ103573

        //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
        public DataTable ObtenerModalidadesPorCompetenciaEstudiante(string redid, RendimientoAcademicoBE oRendimientoAcademicoBE)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            myCon = getConexion();
            myCon.Open();

            //Validamos competencias para 3ro (obligatorio)
            cmd = new SqlCommand("UPAdmPre_RendAcademico_ObtenerModalidadesCompetenciaEstudiante", myCon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@RedId", redid));
            cmd.Parameters.Add(new SqlParameter("@SessionPeriodId", oRendimientoAcademicoBE.SessionPeriodId));

            if (oRendimientoAcademicoBE.CodigoModular2 !="" && oRendimientoAcademicoBE.CodigoModular2 != null)
            {
                if (oRendimientoAcademicoBE.CodigoModular3 != "" && oRendimientoAcademicoBE.CodigoModular3 != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@CodigoModular", oRendimientoAcademicoBE.CodigoModular + "," + oRendimientoAcademicoBE.CodigoModular2 + "," + oRendimientoAcademicoBE.CodigoModular3));
                }
                else 
                {
                    cmd.Parameters.Add(new SqlParameter("@CodigoModular", oRendimientoAcademicoBE.CodigoModular + "," + oRendimientoAcademicoBE.CodigoModular2));
                }
            }
            else
            cmd.Parameters.Add(new SqlParameter("@CodigoModular", oRendimientoAcademicoBE.CodigoModular));

            cmd.Parameters.Add(new SqlParameter("@TotalCompetenciaTercero", oRendimientoAcademicoBE.TotalCompentencias_Tercero));
            cmd.Parameters.Add(new SqlParameter("@TotalADsTercero", oRendimientoAcademicoBE.TotalCompetencia_AD_Tercero));
            cmd.Parameters.Add(new SqlParameter("@TotalAsTercero", oRendimientoAcademicoBE.TotalCompetencia_A_Tercero));
            cmd.Parameters.Add(new SqlParameter("@TotalBsTercero", oRendimientoAcademicoBE.TotalCompetencia_B_Tercero));
            cmd.Parameters.Add(new SqlParameter("@TotalCsTercero", oRendimientoAcademicoBE.TotalCompetencia_C_Tercero));

            cmd.Parameters.Add(new SqlParameter("@TotalCompetenciaCuarto", oRendimientoAcademicoBE.TotalCompentencias_Cuarto));
            cmd.Parameters.Add(new SqlParameter("@TotalADsCuarto", oRendimientoAcademicoBE.TotalCompetencia_AD_Cuarto));
            cmd.Parameters.Add(new SqlParameter("@TotalAsCuarto", oRendimientoAcademicoBE.TotalCompetencia_A_Cuarto));
            cmd.Parameters.Add(new SqlParameter("@TotalBsCuarto", oRendimientoAcademicoBE.TotalCompetencia_B_Cuarto));
            cmd.Parameters.Add(new SqlParameter("@TotalCsCuarto", oRendimientoAcademicoBE.TotalCompetencia_C_Cuarto));

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            
            if (da != null) da.Dispose();
            cmd.Dispose();
            myCon.Close();

            return dt;
        }
 
        public RendimientoAcademicoBE ObtenerNotasADsPorCompetenciaRegistrada_Tercero(AplicanteBE oAplicanteBE)
        {
            RendimientoAcademicoBE oRendimientoAcademicoBE = null;
            using (SqlConnection myCon = getConexion())
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand("UPAdmPre_SelNotaADsCompetenciaRegistrada_Tercero", myCon))
                {
                    SqlDataReader dr;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", oAplicanteBE.IdAplicante));
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            int ApplicationId = dr.GetOrdinal("ApplicationId");
                            int CodigoModular = dr.GetOrdinal("CodigoModular");
                            int DegreeId = dr.GetOrdinal("DegreeId");
                            int SessionPeriodId = dr.GetOrdinal("SessionPeriodId");
                            int TotalCompetencias = dr.GetOrdinal("TotalCompetencias");
                            int TotalCompetencia_AD = dr.GetOrdinal("TotalCompetencia_AD");
                            int TotalCompetencia_A = dr.GetOrdinal("TotalCompetencia_A");
                            int TotalCompetencia_B = dr.GetOrdinal("TotalCompetencia_B");
                            int TotalCompetencia_C = dr.GetOrdinal("TotalCompetencia_C");

                            oRendimientoAcademicoBE = new RendimientoAcademicoBE();
                            oRendimientoAcademicoBE.ApplicationId = Convert.ToInt32(dr[ApplicationId]);
                            oRendimientoAcademicoBE.CodigoModular = dr[CodigoModular].ToString();
                            oRendimientoAcademicoBE.DegreeId = Convert.ToInt32(dr[DegreeId]);
                            oRendimientoAcademicoBE.SessionPeriodId = Convert.ToInt32(dr[SessionPeriodId]);
                            oRendimientoAcademicoBE.TotalCompentencias_Tercero = Convert.ToInt32(dr[TotalCompetencias]);
                            oRendimientoAcademicoBE.TotalCompetencia_AD_Tercero = Convert.ToInt32(dr[TotalCompetencia_AD]);
                            oRendimientoAcademicoBE.TotalCompetencia_A_Tercero = Convert.ToInt32(dr[TotalCompetencia_A]);
                            oRendimientoAcademicoBE.TotalCompetencia_B_Tercero = Convert.ToInt32(dr[TotalCompetencia_B]);
                            oRendimientoAcademicoBE.TotalCompetencia_C_Tercero = Convert.ToInt32(dr[TotalCompetencia_C]);
                        }

                    dr.Close();
                }
            }

            return oRendimientoAcademicoBE;
        }
        //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565


        //Ini:Christian Ramirez - REQ113651
        public DataTable ObtenerModalidadExamenAdmisionRegular(string redid, int sessionPeriodId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            myCon = getConexion();
            myCon.Open();

            cmd = new SqlCommand("UPAdmPre_ObtenerModalidadExamenAdmisionRegular", myCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RedId", redid));
            cmd.Parameters.Add(new SqlParameter("@SessionPeriodId", sessionPeriodId));

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (da != null) da.Dispose();
            cmd.Dispose();
            myCon.Close();

            return dt;
        }
        //Fin:Christian Ramirez - REQ113651
        #endregion
    }
}
