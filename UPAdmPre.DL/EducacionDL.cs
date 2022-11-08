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
    public class EducacionDL : ConexionBD
    {
        #region "Atributos"

        private SqlCommand cmd;

        #endregion

        #region "Constructores"

        public EducacionDL()
            : base()
        { }

        public EducacionDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
            : base()
        {
            this.connection = miConexion;
            this.miTransaccion = miTransaccion;
        }

        #endregion "Constructores"

        #region "Métodos No Transaccionales"

        public List<EducacionBE> ListarColegios(String prefijo, String txtDegreeId)
        {
            List<EducacionBE> listaEducacionBE = new List<EducacionBE>();
            SqlConnection myCon = new SqlConnection();  
            myCon = this.getConexion(); 

            myCon.Open();

            cmd = new SqlCommand("UPAdmPre_SelTablaMaestra", myCon);  
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            switch (txtDegreeId)
            {
                case "41":
                    cmd.Parameters.Add(new SqlParameter("tipo", "COLEBACHILLERATO")); ;
                    break;
                case "57":
                    cmd.Parameters.Add(new SqlParameter("tipo", "COLEBACHILLERATO")); ;
                    break;
                case "58":
                    cmd.Parameters.Add(new SqlParameter("tipo", "COLEBACHILLERATO")); ;
                    break;
                case "49":
                    cmd.Parameters.Add(new SqlParameter("tipo", "COLEEXCELENCIA")); ;
                    break;
                default:
                    cmd.Parameters.Add(new SqlParameter("tipo", "COLECOMP")); ;
                    break;
            }
            cmd.Parameters.Add(new SqlParameter("@name", prefijo));

            try
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        EducacionBE _EducacionBE = new EducacionBE();
                        _EducacionBE.IdEducacion = dr.GetInt32(0);
                        _EducacionBE.NombreInstitucion = dr.GetString(1);
                        _EducacionBE.Direccion = dr.GetString(2);
                        _EducacionBE.CiudadInstitucion = dr.GetString(3);
                        _EducacionBE.DepartamentoDes = dr.GetString(4);
                        _EducacionBE.ModCode = dr.GetString(5);
                        _EducacionBE.TipoEvaluacionECL = dr.GetString(6); /*Se agrega:Christian Ramirez - REQ110609*/

                        listaEducacionBE.Add(_EducacionBE);
                    }
                }
            }
            catch (Exception ex)
            {
                listaEducacionBE = null;
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                myCon.Close();
            }
            return listaEducacionBE;
        }

        public List<EducacionBE> ListarUniversidades(String prefijo, String txtDegreeId)
        {
            List<EducacionBE> listaEducacionBE = new List<EducacionBE>();
            SqlConnection myCon = new SqlConnection();  
            myCon = this.getConexion(); 

            myCon.Open();

            cmd = new SqlCommand("UPAdmPre_SelTablaMaestra", myCon);  
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            switch (txtDegreeId)
            {
                case "41":
                    cmd.Parameters.Add(new SqlParameter("tipo", "COLEBACHILLERATO")); ;
                    break;
                case "49":
                    cmd.Parameters.Add(new SqlParameter("tipo", "COLEEXCELENCIA")); ;
                    break;
                default:
                    cmd.Parameters.Add(new SqlParameter("tipo", "UNIV")); ;
                    break;
            }
            cmd.Parameters.Add(new SqlParameter("@name", prefijo));

            try
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        EducacionBE _EducacionBE = new EducacionBE();
                        _EducacionBE.IdEducacion = dr.GetInt32(0);
                        _EducacionBE.NombreInstitucion = dr.GetString(1);
                        _EducacionBE.Direccion = dr.GetString(2);
                        _EducacionBE.CiudadInstitucion = dr.GetString(3);
                        _EducacionBE.DepartamentoDes = dr.GetString(4);
                        _EducacionBE.ModCode = dr.GetString(5);
                        listaEducacionBE.Add(_EducacionBE);
                    }
                }
            }
            catch (Exception ex)
            {
                listaEducacionBE = null;
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                myCon.Close();
            }
            return listaEducacionBE;
        }

        /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
        public string ObtenerTipoEntrevista(int ApplicationEducationId, int AplicanteId)
        {
            SqlConnection myCon = new SqlConnection();  
            myCon = this.getConexion();
            myCon.Open();
            string rpta = string.Empty;

            try
            {
                cmd = new SqlCommand("UPAdmPre_ObtenerTipoEntrevista", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId ", AplicanteId));
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", ApplicationEducationId));

                rpta = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rpta;
        }
        /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/


        /*Ini:Christian Ramirez - GIIT[caso 64015] - 20190619*/
        public string ObtenerTipoColegio(string codModular, string anioAcademico)
        {
            SqlConnection myCon = new SqlConnection();  
            myCon = this.getConexion();
            myCon.Open();
            string rpta = "";

            try
            {
                cmd = new SqlCommand("UPAdmPre_ObtenerTipoColegio", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codModular", codModular));
                cmd.Parameters.Add(new SqlParameter("@anioAcademico", anioAcademico)); /*Se agrega:Christian Ramirez - REQ91569*/
                rpta = cmd.ExecuteScalar().ToString();
            }
            catch (Exception)
            {

                throw;
            }

            return rpta;

        }
        /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/

        public string ObtenerTipoColegioLimaProv(string codModular, string codModalidad)
        {
            SqlConnection myCon = new SqlConnection();  
            myCon = this.getConexion();
            myCon.Open();
            string rpta = "";

            try
            {
                cmd = new SqlCommand("UPAdmPre_ObtTipoColegioProvLima", myCon); 
                //cmd.CommandText = "UPAdmPre_ObtTipoColegioProvLima";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codModular", codModular));
                cmd.Parameters.Add(new SqlParameter("@codModalidad", codModalidad));
                rpta = cmd.ExecuteScalar().ToString();
            }
            catch (Exception)
            {

                throw;
            }

            return rpta;

        }
        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        public Boolean insertarEducacionAplicante(AplicanteBE oAplicanteBE, EducacionBE oEducacionBE, Boolean transaccionIniciada)
        {
            Int32 codInsertado = 0;
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
                cmd = new SqlCommand("UPAdmPre_InsColegio", myCon); 
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oEducacionBE.IdEducacion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", oEducacionBE.IdEducacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", DBNull.Value));
                }

                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", DBNull.Value));
                }

                if (oEducacionBE.NombreInstitucion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_InstitutionName", oEducacionBE.NombreInstitucion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_InstitutionName", DBNull.Value));
                }

                if (oEducacionBE.Institucion != null && UIConvertNull.Int32(oEducacionBE.Institucion.Codigo) > 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OrganizationId", oEducacionBE.Institucion.Codigo));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OrganizationId", DBNull.Value));
                }

                if (oEducacionBE.SeccionEnFormulario != UIConstantes.SECCION_EN_FORMULARIO.NINGUNO)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PositionForm", oEducacionBE.SeccionEnFormulario.ToString()));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PositionForm", DBNull.Value));
                }

                if (oAplicanteBE.SituacionAcademica != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_SituacionAcademica", oAplicanteBE.SituacionAcademica));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_SituacionAcademica", DBNull.Value));
                }
                if (oEducacionBE.Revision_Opid != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RevisionOpid", oEducacionBE.Revision_Opid));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RevisionOpid", DBNull.Value));
                }
                if (oAplicanteBE.TipoColeProvId.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoColeProvId", oAplicanteBE.TipoColeProvId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoColeProvId", DBNull.Value));
                }

                cmd.Parameters.Add("@MiRegInsert", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                codInsertado = (Int32)cmd.Parameters["@MiRegInsert"].Value;
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

        public Boolean insertarDetalleEducacion(AplicanteBE oAplicanteBE, EducacionDetalleBE oDetalleEducacionBE, Boolean transaccionIniciada)
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
                cmd = new SqlCommand("UPAdmPre_InsRendimientoAcademico", myCon); 
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oDetalleEducacionBE.IdApplication.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oDetalleEducacionBE.IdApplication));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdApplicationEducation.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", oDetalleEducacionBE.IdApplicationEducation));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdApplicationEducationEnroll.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationEnrollId", oDetalleEducacionBE.IdApplicationEducationEnroll));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationEnrollId", DBNull.Value));
                }

                if (oDetalleEducacionBE.FechaInicio.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", oDetalleEducacionBE.FechaInicio));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", DBNull.Value));
                }

                if (oDetalleEducacionBE.FechaFin.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", oDetalleEducacionBE.FechaFin));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdGrado.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeId", oDetalleEducacionBE.IdGrado));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeId", DBNull.Value));
                }

                if (oDetalleEducacionBE.CantidadEstudiantes != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_QuantityStudents", oDetalleEducacionBE.CantidadEstudiantes));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_QuantityStudents", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdMerito != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeName", oDetalleEducacionBE.IdMerito));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeName", DBNull.Value));
                }

                ///Insertando las Notas
                if (oDetalleEducacionBE.NotaMateTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateTercero", oDetalleEducacionBE.NotaMateTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaLengTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengTercero", oDetalleEducacionBE.NotaLengTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaPromTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromTercero", oDetalleEducacionBE.NotaPromTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaMateCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateCuarto", oDetalleEducacionBE.NotaMateCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaLengCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengCuarto", oDetalleEducacionBE.NotaLengCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaPromCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromCuarto", oDetalleEducacionBE.NotaPromCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaMateQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateQuinto", oDetalleEducacionBE.NotaMateQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaLengQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengQuinto", oDetalleEducacionBE.NotaLengQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaPromQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromQuinto", oDetalleEducacionBE.NotaPromQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaMateTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateTercero", oDetalleEducacionBE.OtraNotaMateTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaLengTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengTercero", oDetalleEducacionBE.OtraNotaLengTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaPromTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromTercero", oDetalleEducacionBE.OtraNotaPromTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaMateCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateCuarto", oDetalleEducacionBE.OtraNotaMateCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaLengCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengCuarto", oDetalleEducacionBE.OtraNotaLengCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaPromCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromCuarto", oDetalleEducacionBE.OtraNotaPromCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaMateQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateQuinto", oDetalleEducacionBE.OtraNotaMateQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaLengQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengQuinto", oDetalleEducacionBE.OtraNotaLengQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaPromQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromQuinto", oDetalleEducacionBE.OtraNotaPromQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.Revision_Opid != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oDetalleEducacionBE.Revision_Opid));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", DBNull.Value));
                }

                /*Ini:Christian Ramirez - REQ91569*/
                if (oDetalleEducacionBE.CodTipoCalificacion == null) 
                    cmd.Parameters.Add(new SqlParameter("@VC_CodTipoCalificacion", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@VC_CodTipoCalificacion", oDetalleEducacionBE.CodTipoCalificacion));

                if (oDetalleEducacionBE.DescTipoCalificacion == null)
                    cmd.Parameters.Add(new SqlParameter("@VC_DescTipoCalificacion", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@VC_DescTipoCalificacion", oDetalleEducacionBE.DescTipoCalificacion));
                /*Fin:Christian Ramirez - REQ91569*/

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

        public Boolean insertarDetalleEducacionFormVeintiuno(AplicanteBE oAplicanteBE, EducacionDetalleBE oDetalleEducacionBE, Boolean transaccionIniciada)
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
                cmd = new SqlCommand("UPAdmPre_InsRendimientoAcademico_Form1", myCon);
                cmd.Transaction = this.miTransaccion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oDetalleEducacionBE.IdApplication.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oDetalleEducacionBE.IdApplication));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdApplicationEducation.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", oDetalleEducacionBE.IdApplicationEducation));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdApplicationEducationEnroll.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationEnrollId", oDetalleEducacionBE.IdApplicationEducationEnroll));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationEnrollId", DBNull.Value));
                }

                if (oDetalleEducacionBE.FechaInicio.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", oDetalleEducacionBE.FechaInicio));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", DBNull.Value));
                }

                if (oDetalleEducacionBE.FechaFin.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", oDetalleEducacionBE.FechaFin));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdGrado.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeId", oDetalleEducacionBE.IdGrado));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeId", DBNull.Value));
                }

                if (oDetalleEducacionBE.CantidadEstudiantes != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_QuantityStudents", oDetalleEducacionBE.CantidadEstudiantes));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_QuantityStudents", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdMerito != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeName", oDetalleEducacionBE.IdMerito));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeName", DBNull.Value));
                }

                ///Insertando las Notas
                if (oDetalleEducacionBE.NotaMateTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateTercero", oDetalleEducacionBE.NotaMateTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaLengTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengTercero", oDetalleEducacionBE.NotaLengTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaPromTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromTercero", oDetalleEducacionBE.NotaPromTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaMateCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateCuarto", oDetalleEducacionBE.NotaMateCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaLengCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengCuarto", oDetalleEducacionBE.NotaLengCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaPromCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromCuarto", oDetalleEducacionBE.NotaPromCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaMateQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateQuinto", oDetalleEducacionBE.NotaMateQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaLengQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengQuinto", oDetalleEducacionBE.NotaLengQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaPromQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromQuinto", oDetalleEducacionBE.NotaPromQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaMateTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateTercero", oDetalleEducacionBE.OtraNotaMateTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaLengTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengTercero", oDetalleEducacionBE.OtraNotaLengTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaPromTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromTercero", oDetalleEducacionBE.OtraNotaPromTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaMateCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateCuarto", oDetalleEducacionBE.OtraNotaMateCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaLengCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengCuarto", oDetalleEducacionBE.OtraNotaLengCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaPromCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromCuarto", oDetalleEducacionBE.OtraNotaPromCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaMateQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateQuinto", oDetalleEducacionBE.OtraNotaMateQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaLengQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengQuinto", oDetalleEducacionBE.OtraNotaLengQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaPromQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromQuinto", oDetalleEducacionBE.OtraNotaPromQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.Revision_Opid != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oDetalleEducacionBE.Revision_Opid));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", DBNull.Value));
                }

                /*Ini:Christian Ramirez - REQ91569*/
                if (oDetalleEducacionBE.CodTipoCalificacion == null)
                    cmd.Parameters.Add(new SqlParameter("@VC_CodTipoCalificacion", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@VC_CodTipoCalificacion", oDetalleEducacionBE.CodTipoCalificacion));

                if (oDetalleEducacionBE.DescTipoCalificacion == null)
                    cmd.Parameters.Add(new SqlParameter("@VC_DescTipoCalificacion", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@VC_DescTipoCalificacion", oDetalleEducacionBE.DescTipoCalificacion));
                /*Fin:Christian Ramirez - REQ91569*/

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

        public Boolean insertarDetalleEducacionFormVeinte(AplicanteBE oAplicanteBE, EducacionDetalleBE oDetalleEducacionBE, Boolean transaccionIniciada)
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
                cmd = new SqlCommand("UPAdmPre_InsRendimientoAcademico_Form20", myCon);
                cmd.Transaction = this.miTransaccion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oDetalleEducacionBE.IdApplication.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oDetalleEducacionBE.IdApplication));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdApplicationEducation.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", oDetalleEducacionBE.IdApplicationEducation));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdApplicationEducationEnroll.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationEnrollId", oDetalleEducacionBE.IdApplicationEducationEnroll));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationEnrollId", DBNull.Value));
                }

                if (oDetalleEducacionBE.FechaInicio.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", oDetalleEducacionBE.FechaInicio));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", DBNull.Value));
                }

                if (oDetalleEducacionBE.FechaFin.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", oDetalleEducacionBE.FechaFin));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdGrado.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeId", oDetalleEducacionBE.IdGrado));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeId", DBNull.Value));
                }

                if (oDetalleEducacionBE.CantidadEstudiantes != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_QuantityStudents", oDetalleEducacionBE.CantidadEstudiantes));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_QuantityStudents", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdMerito != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeName", oDetalleEducacionBE.IdMerito));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeName", DBNull.Value));
                }

                ///Insertando las Notas
                if (oDetalleEducacionBE.NotaMateTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateTercero", oDetalleEducacionBE.NotaMateTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaLengTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengTercero", oDetalleEducacionBE.NotaLengTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaPromTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromTercero", oDetalleEducacionBE.NotaPromTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaMateCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateCuarto", oDetalleEducacionBE.NotaMateCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaLengCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengCuarto", oDetalleEducacionBE.NotaLengCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaPromCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromCuarto", oDetalleEducacionBE.NotaPromCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaMateQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateQuinto", oDetalleEducacionBE.NotaMateQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaMateQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaLengQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengQuinto", oDetalleEducacionBE.NotaLengQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaLengQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.NotaPromQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromQuinto", oDetalleEducacionBE.NotaPromQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_NotaPromQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaMateTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateTercero", oDetalleEducacionBE.OtraNotaMateTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaLengTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengTercero", oDetalleEducacionBE.OtraNotaLengTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaPromTercero != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromTercero", oDetalleEducacionBE.OtraNotaPromTercero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromTercero", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaMateCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateCuarto", oDetalleEducacionBE.OtraNotaMateCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaLengCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengCuarto", oDetalleEducacionBE.OtraNotaLengCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaPromCuarto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromCuarto", oDetalleEducacionBE.OtraNotaPromCuarto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromCuarto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaMateQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateQuinto", oDetalleEducacionBE.OtraNotaMateQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaMateQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaLengQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengQuinto", oDetalleEducacionBE.OtraNotaLengQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaLengQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.OtraNotaPromQuinto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromQuinto", oDetalleEducacionBE.OtraNotaPromQuinto));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OtraNotaPromQuinto", DBNull.Value));
                }

                if (oDetalleEducacionBE.Revision_Opid != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oDetalleEducacionBE.Revision_Opid));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", DBNull.Value));
                }

                /*Ini:Christian Ramirez - REQ91569*/
                if (oDetalleEducacionBE.CodTipoCalificacion == null)
                    cmd.Parameters.Add(new SqlParameter("@VC_CodTipoCalificacion", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@VC_CodTipoCalificacion", oDetalleEducacionBE.CodTipoCalificacion));

                if (oDetalleEducacionBE.DescTipoCalificacion == null)
                    cmd.Parameters.Add(new SqlParameter("@VC_DescTipoCalificacion", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@VC_DescTipoCalificacion", oDetalleEducacionBE.DescTipoCalificacion));
                /*Fin:Christian Ramirez - REQ91569*/

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
        public bool insertarUniversidaAplicante(AplicanteBE oAplicanteBE, EducacionBE oEducacionBE, Boolean transaccionIniciada)
        {
            int codInsertado = 0;
            bool respuesta = true;
            SqlConnection myCon = new SqlConnection();  
            try
            {
                //if (!transaccionIniciada)
                //{
                    myCon = this.getConexion();
                    myCon.Open();
                //}

                cmd = new SqlCommand("UPAdmPre_InsUniversidad", myCon); 
                //cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oEducacionBE.IdEducacion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", oEducacionBE.IdEducacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", DBNull.Value));
                }
                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", DBNull.Value));
                }

                if (oEducacionBE.NombreInstitucion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_InstitutionName", oEducacionBE.NombreInstitucion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_InstitutionName", DBNull.Value));
                }
                if (oEducacionBE.Institucion != null && oEducacionBE.Institucion.Codigo > 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OrganizationId", oEducacionBE.Institucion.Codigo));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_OrganizationId", DBNull.Value));
                }

                if (oEducacionBE.SeccionEnFormulario != UIConstantes.SECCION_EN_FORMULARIO.NINGUNO)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PositionForm", oEducacionBE.SeccionEnFormulario.ToString()));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PositionForm", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_Revision_Opid", oAplicanteBE.RedId));
                cmd.Parameters.Add("@MiRegInsert", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                codInsertado = (int)cmd.Parameters["@MiRegInsert"].Value;

                if (codInsertado > 0)
                {
                    oEducacionBE.IdEducacion = codInsertado;
                    if (oEducacionBE.LDetalleEducacion != null)
                    {
                        foreach (EducacionDetalleBE oEducacionDetalleBE in oEducacionBE.LDetalleEducacion)
                        {
                            oEducacionDetalleBE.Revision_Opid = oAplicanteBE.RedId;
                            insertarDetalleUniversidad(oEducacionBE, oEducacionDetalleBE, transaccionIniciada);
                        }
                    }
                }
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

        private Boolean insertarDetalleUniversidad(EducacionBE oEducacionBE, EducacionDetalleBE oDetalleEducacionBE, Boolean transaccionIniciada)
        {
            bool respuesta = true;
            SqlConnection myCon = new SqlConnection();  
            try
            {
                //if (!transaccionIniciada)
                //{
                    myCon = this.getConexion();
                    myCon.Open();
                //}

                cmd = new SqlCommand("UPAdmPre_InsUniversidadDetalle", myCon); 
                //cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oEducacionBE.IdEducacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", oEducacionBE.IdEducacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdDetalleEducacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationEnrollId", oDetalleEducacionBE.IdDetalleEducacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationEnrollId", DBNull.Value));
                }
                if (oDetalleEducacionBE.IdCarrera.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CarreraId", oDetalleEducacionBE.IdCarrera));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CarreraId", DBNull.Value));
                }
                if (oDetalleEducacionBE.NombreCarrera != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CarreraNombre", oDetalleEducacionBE.NombreCarrera));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CarreraNombre", DBNull.Value));
                }
                if (oDetalleEducacionBE.FechaInicio.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", oDetalleEducacionBE.FechaInicio));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", DBNull.Value));
                }

                if (oDetalleEducacionBE.FechaFin.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", oDetalleEducacionBE.FechaFin));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", DBNull.Value));
                }

                if (oDetalleEducacionBE.CantidadCiclosCursados != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_quantityCycleCourse", oDetalleEducacionBE.CantidadCiclosCursados));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_quantityCycleCourse", DBNull.Value));
                }
                if (oDetalleEducacionBE.CantidadCreditosAprobados != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_quantityCreditoPass", oDetalleEducacionBE.CantidadCreditosAprobados));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_quantityCreditoPass", DBNull.Value));
                }

                if (oDetalleEducacionBE.IdGrado.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeId", oDetalleEducacionBE.IdGrado));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeId", DBNull.Value));
                }
                if (oDetalleEducacionBE.NombreGrado != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeName", oDetalleEducacionBE.NombreGrado));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DegreeName", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oDetalleEducacionBE.Revision_Opid));
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

        public Boolean insertarOtrosEstudiosAplicante(AplicanteBE oAplicanteBE, EducacionBE oEducacionBE, Boolean transaccionIniciada)
        {
            Int32 codInsertado = 0;
            Boolean respuesta = true;
            SqlConnection myCon = new SqlConnection();  
            try
            {
                //if (!transaccionIniciada)
                //{
                    myCon = this.getConexion();
                    myCon.Open();
                //}

                cmd = new SqlCommand("UPAdmPre_InsOtrosEstudios", myCon); 
                //cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oEducacionBE.IdEducacion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", oEducacionBE.IdEducacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", DBNull.Value));
                }

                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", DBNull.Value));
                }

                if (oEducacionBE.NombreInstitucion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_InstitutionName", oEducacionBE.NombreInstitucion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_InstitutionName", DBNull.Value));
                }

                if (oEducacionBE.SeccionEnFormulario != UIConstantes.SECCION_EN_FORMULARIO.NINGUNO)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PositionForm", oEducacionBE.SeccionEnFormulario.ToString()));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PositionForm", DBNull.Value));
                }
                if (oEducacionBE.Revision_Opid != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Revision_Opid", oEducacionBE.Revision_Opid));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Revision_Opid", DBNull.Value));
                }
                cmd.Parameters.Add("@MiRegInsert", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                codInsertado = (Int32)cmd.Parameters["@MiRegInsert"].Value;

                ///Detalle de Educación
                if (codInsertado > 0)
                {
                    oEducacionBE.IdEducacion = codInsertado;
                    if (oEducacionBE.LDetalleEducacion != null)
                    {
                        foreach (EducacionDetalleBE oEducacionDetalleBE in oEducacionBE.LDetalleEducacion)
                        {
                            insertarDetalleOtrosEstudios(oEducacionBE, oEducacionDetalleBE, transaccionIniciada);
                        }
                    }
                }
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

        private Boolean insertarDetalleOtrosEstudios(EducacionBE oEducacionBE, EducacionDetalleBE oEducacionDetalleBE, Boolean transaccionIniciada)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();  
            try
            {
                //if (!transaccionIniciada)
                //{
                    myCon = this.getConexion();
                    myCon.Open();
               // }

                cmd = new SqlCommand("UPAdmPre_InsOtrosEstudiosDetalle", myCon); 
               // cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oEducacionDetalleBE.IdDetalleEducacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationEnrollId", oEducacionDetalleBE.IdDetalleEducacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationEnrollId", DBNull.Value));
                }

                if (oEducacionBE.IdEducacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", oEducacionBE.IdEducacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", DBNull.Value));
                }

                if (oEducacionDetalleBE.FechaInicio.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", oEducacionDetalleBE.FechaInicio));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StartDate", DBNull.Value));
                }

                if (oEducacionDetalleBE.FechaFin.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", oEducacionDetalleBE.FechaFin));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EndDate", DBNull.Value));
                }

                if (oEducacionDetalleBE.NombreCarrera != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CarreraNombre", oEducacionDetalleBE.NombreCarrera));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CarreraNombre", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oEducacionDetalleBE.Revision_Opid));
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

        public Boolean EliminaColegioRegistrado(Int32? IdEducacion, Int32? AplicanteId)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();  
            try
            {
                myCon = this.getConexion();
                myCon.Open();
                cmd = new SqlCommand("UPAdmPre_EliColegio", myCon); 
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", IdEducacion));
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

        public Boolean EliminaEstudioUniversitarioRegistrado(Int32? IdEducacion, Int32? IdDetEducacion, Int32? AplicanteId)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();  
            try
            {
                myCon = this.getConexion();
                myCon.Open();
                cmd = new SqlCommand("UPAdmPre_EliEstudioUniversitario", myCon); 
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", IdEducacion));
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationEnrollId", IdDetEducacion));
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

        /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
        public Boolean ModTipoEntrevista(AplicanteBE oAplicanteBE, int ApplicationEducationId, string TipoEntrevista)
        {
            Boolean respuesta = false;
            SqlConnection myCon = new SqlConnection();  

            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_ModTipoEntrevista", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationEducationId", ApplicationEducationId));
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                cmd.Parameters.Add(new SqlParameter("@TipoEntrevista", TipoEntrevista));
                cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oAplicanteBE.RedId));

                int rpta = cmd.ExecuteNonQuery();
                if (rpta > -1) respuesta = true;
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
        /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/
        #endregion "Métodos Transaccionales"
    }
}
