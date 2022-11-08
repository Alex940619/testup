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
    public class AplicanteDL : ConexionBD
    {
        public AplicanteDL()
        { }

        #region "Atributos"

        private SqlCommand cmd;

        #endregion "Atributos"



        #region "Métodos No Transaccionales"

        public DataTable ListarCarrerasPorModalidad(Int32? DegreeId, Int32? SettingsId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelCarrerasPorModalidad", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@DegreeId", DegreeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationFormSettingId", SettingsId));
                myCon.Open();
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

        public DataTable ObtenerCorrelativoSN()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("[SAPB1].[SpObtenerCorrelativoSN]", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                myCon.Open();
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

        public DataTable ObtenerCorrelativoSNExt()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("[SAPB1].[SpObtenerCorrelativoSN_ext]", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                myCon.Open();
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

        public DataTable ListarCursosPorPrograma(Int32? IdPrograma, String strUsrRedId, Int32? IdAplicante)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelCursosPorProgramaEPU", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ProgramOfStudyId", IdPrograma));
                cmd.Parameters.Add(new SqlParameter("@VC_UsrRedId", strUsrRedId));
                cmd.Parameters.Add(new SqlParameter("@VC_IdAplicante", IdAplicante));
                myCon.Open();
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

        public AplicanteBE ListarDatosPersonalesPorUsrRed(String UserIdRed)
        {
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelDatoPersonalPorUserRed", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@UserIdRed", UserIdRed));
                myCon.Open();
                cmd.ExecuteNonQuery();
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null)
                {
                    oAplicanteBE = this.poblarDatosPersonalesAspirante(ds.Tables[(int)UIConstantes.ESTRUCTURA_TABLAS_ASPIRANTE.DATOS_PERSONALES], oAplicanteBE);
                    //oAplicanteBE = this.poblarDatosColegio(ds.Tables[(Int32)UIConstantes.ESTRUCTURA_TABLAS_ASPIRANTE.DATOS_COLEGIOS], oAplicanteBE);
                    //oAplicanteBE = this.poblarDatosIdioma(ds.Tables[(Int32)UIConstantes.ESTRUCTURA_TABLAS_ASPIRANTE.DATOS_IDIOMAS], oAplicanteBE);
                    //oAplicanteBE = this.poblarDatosPadres(ds.Tables[(Int32)UIConstantes.ESTRUCTURA_TABLAS_ASPIRANTE.DATOS_PADRES], oAplicanteBE);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ds = null;
                cmd.Dispose();
                myCon.Close();
            }
            return oAplicanteBE;
        }

        private AplicanteBE poblarDatosPersonalesAspirante(DataTable dtDatosPersonales, AplicanteBE oAplicanteBE)
        {
            if (dtDatosPersonales != null && dtDatosPersonales.Rows.Count > 0)
            {
                if (oAplicanteBE == null)
                {
                    oAplicanteBE = new AplicanteBE();
                }
                if (oAplicanteBE.Direccion == null)
                {
                    oAplicanteBE.Direccion = new DireccionAplicanteBE();
                }

                DataRow drDatosPersonales = dtDatosPersonales.Rows[0];
                //if (drDatosPersonales["CreateDatetime"] != DBNull.Value)
                //{
                //    CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                //    DateTime tempFechaCreacion;
                //    DateTime.TryParse(drDatosPersonales["CreateDatetime"].ToString(), cultura, System.Globalization.DateTimeStyles.None, out tempFechaCreacion);
                //    oAplicanteBE.FechaCreacion = tempFechaCreacion;
                //}
                if (drDatosPersonales["Status"] != DBNull.Value)
                {
                    Int32 tempEstado = 0;
                    int.TryParse(drDatosPersonales["Status"].ToString(), out tempEstado);
                    oAplicanteBE.Estado = tempEstado;
                }
                if (drDatosPersonales["PersonId"] != DBNull.Value)
                {
                    Int32 tempIdPerson = 0;
                    int.TryParse(drDatosPersonales["PersonId"].ToString(), out tempIdPerson);
                    oAplicanteBE.IdPerson = tempIdPerson;
                }
                //if (drDatosPersonales["Prefix"] != DBNull.Value)
                //{
                //    Int32 tempPrefijo = 0;
                //    int.TryParse(drDatosPersonales["Prefix"].ToString(), out tempPrefijo);
                //    oAplicanteBE.Prefijo = tempPrefijo;
                //}
                if (drDatosPersonales["FirstName"] != DBNull.Value)
                {
                    oAplicanteBE.PrimerNombre = drDatosPersonales["FirstName"].ToString();
                }
                if (drDatosPersonales["MiddleName"] != DBNull.Value)
                {
                    oAplicanteBE.SegundoNombre = drDatosPersonales["MiddleName"].ToString();
                }
                if (drDatosPersonales["LastName"] != DBNull.Value)
                {
                    oAplicanteBE.Apellidos = drDatosPersonales["LastName"].ToString();
                }
                //if (drDatosPersonales["Suffix"] != DBNull.Value)
                //{
                //    Int32 tempSufijo = 0;
                //    int.TryParse(drDatosPersonales["Suffix"].ToString(), out tempSufijo);
                //    oAplicanteBE.Sufijo = tempSufijo;
                //}
                //if (drDatosPersonales["Nickname"] != DBNull.Value)
                //{
                //    oAplicanteBE.Alias = drDatosPersonales["Nickname"].ToString();
                //}
                if (drDatosPersonales["Email"] != DBNull.Value)
                {
                    oAplicanteBE.CorreoPersonal = drDatosPersonales["Email"].ToString();
                }
                if (drDatosPersonales["BirthDate"] != DBNull.Value)
                {
                    CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaNac;
                    DateTime.TryParse(drDatosPersonales["BirthDate"].ToString(), cultura, System.Globalization.DateTimeStyles.None, out tempFechaNac);
                    oAplicanteBE.FechaNacimiento = tempFechaNac;
                }
                if (drDatosPersonales["Gender"] != DBNull.Value)
                {
                    Int32 tempGenero = 0;
                    int.TryParse(drDatosPersonales["Gender"].ToString(), out tempGenero);
                    oAplicanteBE.Genero = tempGenero;
                }

                ///*DIRECCION*/
                //oAplicanteBE.Direccion.IdTipoDireccion = UIConstantes.TIPO_DIRECCION.DOMICILIO;
                //if (drDatosPersonales["Line1"] != DBNull.Value)
                //{
                //    oAplicanteBE.Direccion.Direccion1 = drDatosPersonales["Line1"].ToString();
                //}
                //if (drDatosPersonales["City"] != DBNull.Value)
                //{
                //    oAplicanteBE.Direccion.Distrito = drDatosPersonales["City"].ToString();
                //}
                //if (drDatosPersonales["StateProvinceId"] != DBNull.Value)
                //{
                //    Int32 tempIdDpto = 0;
                //    int.TryParse(drDatosPersonales["StateProvinceId"].ToString(), out tempIdDpto);
                //    oAplicanteBE.Direccion.Departamento = tempIdDpto;
                //}
                //if (drDatosPersonales["County"] != DBNull.Value)
                //{
                //    Int32 tempIdProvincia = 0;
                //    int.TryParse(drDatosPersonales["County"].ToString(), out tempIdProvincia);
                //    oAplicanteBE.Direccion.Provincia = tempIdProvincia;
                //}
                //if (drDatosPersonales["Country"] != DBNull.Value)
                //{
                //    Int32 tempIdPais = 0;
                //    int.TryParse(drDatosPersonales["Country"].ToString(), out tempIdPais);
                //    oAplicanteBE.Direccion.Pais = tempIdPais;
                //}
                ///*-------------------*/
                if (drDatosPersonales["TipoDocumento"] != DBNull.Value)
                {
                    oAplicanteBE.TipoDocumento = drDatosPersonales["TipoDocumento"].ToString();
                }
                if (drDatosPersonales["GovernmentId"] != DBNull.Value)
                {
                    oAplicanteBE.DocumentoIdentidad = drDatosPersonales["GovernmentId"].ToString();
                }
                //if (drDatosPersonales["Ethnicity"] != DBNull.Value)
                //{
                //    Int32 tempEtnicidad = 0;
                //    int.TryParse(drDatosPersonales["Ethnicity"].ToString(), out tempEtnicidad);
                //    oAplicanteBE.Etnicidad = tempEtnicidad;
                //}
                //if (drDatosPersonales["MaritalStatus"] != DBNull.Value)
                //{
                //    Int32 tempEstadoMarital = 0;
                //    int.TryParse(drDatosPersonales["MaritalStatus"].ToString(), out tempEstadoMarital);
                //    oAplicanteBE.EstadoMarital = tempEstadoMarital;
                //}
                //if (drDatosPersonales["Religion"] != DBNull.Value)
                //{
                //    Int32 tempReligion = 0;
                //    int.TryParse(drDatosPersonales["Religion"].ToString(), out tempReligion);
                //    oAplicanteBE.Religion = tempReligion;
                //}
                //if (drDatosPersonales["VeteranStatus"] != DBNull.Value)
                //{
                //    Int32 tempVeterano = 0;
                //    int.TryParse(drDatosPersonales["VeteranStatus"].ToString(), out tempVeterano);
                //    oAplicanteBE.EstadoVeterano = tempVeterano;
                //}
                //if (drDatosPersonales["IsRetired"] != DBNull.Value)
                //{
                //    oAplicanteBE.EsRetirado = (drDatosPersonales["VeteranStatus"].ToString() == UIConstantes.idValorActivo.ToString() ? true : false);
                //}
                if (drDatosPersonales["PrimaryCitizenship"] != DBNull.Value)
                {
                    Int32 tempNacPrimaria = 0;
                    int.TryParse(drDatosPersonales["PrimaryCitizenship"].ToString(), out tempNacPrimaria);
                    oAplicanteBE.NacionalidadPrimaria = tempNacPrimaria;
                }
                if (drDatosPersonales["SecondaryCitizenship"] != DBNull.Value)
                {
                    Int32 tempNacSecundaria = 0;
                    int.TryParse(drDatosPersonales["SecondaryCitizenship"].ToString(), out tempNacSecundaria);
                    oAplicanteBE.NacionalidadPrimaria = tempNacSecundaria;
                }
                if (drDatosPersonales["CountryOfBirth"] != DBNull.Value)
                {
                    Int32 tempPaisNac = 0;
                    int.TryParse(drDatosPersonales["CountryOfBirth"].ToString(), out tempPaisNac);
                    oAplicanteBE.PaisNacimiento = tempPaisNac;
                }
                if (drDatosPersonales["CityOfBirth"] != DBNull.Value)
                {
                    Int32 tempDptoNac = 0;
                    int.TryParse(drDatosPersonales["CityOfBirth"].ToString(), out tempDptoNac);
                    oAplicanteBE.DptoNacimiento = tempDptoNac;
                }
                //if (drDatosPersonales["PrimaryLanguage"] != DBNull.Value)
                //{
                //    Int32 tempIdiomaPrimario = 0;
                //    int.TryParse(drDatosPersonales["PrimaryLanguage"].ToString(), out tempIdiomaPrimario);
                //    oAplicanteBE.IdiomaPrimario = tempIdiomaPrimario;
                //}
                //if (drDatosPersonales["SecondaryLanguage"] != DBNull.Value)
                //{
                //    Int32 tempIdiomaSecundario = 0;
                //    int.TryParse(drDatosPersonales["SecondaryLanguage"].ToString(), out tempIdiomaSecundario);
                //    oAplicanteBE.IdiomaSecundario = tempIdiomaSecundario;
                //}
                //if (drDatosPersonales["MonthsInCountry"] != DBNull.Value)
                //{
                //    Int32 tempMesesPais = 0;
                //    int.TryParse(drDatosPersonales["MonthsInCountry"].ToString(), out tempMesesPais);
                //    oAplicanteBE.MesesPais = tempMesesPais;
                //}
                //if (drDatosPersonales["Visa"] != DBNull.Value)
                //{
                //    Int32 tempVisa = 0;
                //    int.TryParse(drDatosPersonales["Visa"].ToString(), out tempVisa);
                //    oAplicanteBE.Visa = tempVisa;
                //}
                //if (drDatosPersonales["VisaNumber"] != DBNull.Value)
                //{
                //    oAplicanteBE.NumeroVisa = drDatosPersonales["VisaNumber"].ToString();
                //}
                //if (drDatosPersonales["VisaCountryIssued"] != DBNull.Value)
                //{
                //    Int32 tempPaisVisa = 0;
                //    int.TryParse(drDatosPersonales["VisaCountryIssued"].ToString(), out tempPaisVisa);
                //    oAplicanteBE.PaisVisa = tempPaisVisa;
                //}
                //if (drDatosPersonales["VisaExpirationDate"] != DBNull.Value)
                //{
                //    CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                //    DateTime tempFechaExp;
                //    DateTime.TryParse(drDatosPersonales["VisaExpirationDate"].ToString(), cultura, System.Globalization.DateTimeStyles.None, out tempFechaExp);
                //    oAplicanteBE.FechaExpiracionVisa = tempFechaExp;
                //}
                //if (drDatosPersonales["PassportNumber"] != DBNull.Value)
                //{
                //    oAplicanteBE.NumeroPasaporte = drDatosPersonales["PassportNumber"].ToString();
                //}
                //if (drDatosPersonales["PassportCountryIssued"] != DBNull.Value)
                //{
                //    Int32 tempPaisPasaporte = 0;
                //    int.TryParse(drDatosPersonales["PassportCountryIssued"].ToString(), out tempPaisPasaporte);
                //    oAplicanteBE.PaisPasaporte = tempPaisPasaporte;
                //}
                //if (drDatosPersonales["PassportExpirationDate"] != DBNull.Value)
                //{
                //    CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                //    DateTime tempFechaExpPas;
                //    DateTime.TryParse(drDatosPersonales["PassportExpirationDate"].ToString(), cultura, System.Globalization.DateTimeStyles.None, out tempFechaExpPas);
                //    oAplicanteBE.FechaExpiracionPasaporte = tempFechaExpPas;
                //}
                if (drDatosPersonales["SessionPeriodId"] != DBNull.Value)
                {
                    Int32 tempIdPeriodo = 0;
                    int.TryParse(drDatosPersonales["SessionPeriodId"].ToString(), out tempIdPeriodo);
                    oAplicanteBE.IdPeriodoSesion = tempIdPeriodo;
                }
                //if (drDatosPersonales["CollegeAttendStatus"] != DBNull.Value)
                //{
                //    Int32 tempEstadoAsistenciaUni = 0;
                //    int.TryParse(drDatosPersonales["CollegeAttendStatus"].ToString(), out tempEstadoAsistenciaUni);
                //    oAplicanteBE.EstadoAsistenciaUniversidad = tempEstadoAsistenciaUni;
                //}
                //if (drDatosPersonales["IsSeekingDegree"] != DBNull.Value)
                //{
                //    oAplicanteBE.EstaBuscandoEstudio = (drDatosPersonales["IsSeekingDegree"].ToString() == UIConstantes.idValorActivo.ToString() ? true : false);
                //}
                //if (drDatosPersonales["IsInterestedInExtraCurricular"] != DBNull.Value)
                //{
                //    oAplicanteBE.EstaInteresadoExtraCurricular = (drDatosPersonales["IsInterestedInExtraCurricular"].ToString() == UIConstantes.idValorActivo.ToString() ? true : false);
                //}
                //if (drDatosPersonales["IsInterestedInFinancialAid"] != DBNull.Value)
                //{
                //    oAplicanteBE.EstaInteresadoFinanzas = (drDatosPersonales["IsInterestedInFinancialAid"].ToString() == UIConstantes.idValorActivo.ToString() ? true : false);
                //}

                //==========================================================================
                //PARA SABER SI TIENE DNI O CARNET DE EXTRANJERIA
                //==========================================================================
                //if (drDatosPersonales["InquiryFormSettingId"] != DBNull.Value)
                //{
                //    Int32 tempIdFormatoConfig = 0;
                //    int.TryParse(drDatosPersonales["InquiryFormSettingId"].ToString(), out tempIdFormatoConfig);
                //    oAplicanteBE.IdConfiguracionAplicacion = tempIdFormatoConfig;
                //}
                //if (drDatosPersonales["CounselorId"] != DBNull.Value)
                //{
                //    Int32 tempIdConsulado = 0;
                //    int.TryParse(drDatosPersonales["CounselorId"].ToString(), out tempIdConsulado);
                //    oAplicanteBE.IdConsulado = tempIdConsulado;
                //}
                //if (drDatosPersonales["OtherSource"] != DBNull.Value)
                //{
                //    oAplicanteBE.OtrasFuentes = drDatosPersonales["OtherSource"].ToString();
                //}
                if (drDatosPersonales["SituacionAcademica"] != DBNull.Value)
                {
                    oAplicanteBE.SituacionAcademica = UIConvertNull.Int32(drDatosPersonales["SituacionAcademica"].ToString());
                }
                if (drDatosPersonales["RedID"] != DBNull.Value)
                {
                    oAplicanteBE.RedId = drDatosPersonales["RedID"].ToString();
                }
            }
            return oAplicanteBE;
        }

        public AplicanteBE ListarDatosPersonalesPorIdAplicante(Int32? IdAplicante)
        {
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelDatoPersonalPorIdAplicante", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_IdAplicante", IdAplicante));
                myCon.Open();
                cmd.ExecuteNonQuery();
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null)
                {
                    oAplicanteBE = this.poblarDatosPersonalesPorIdAplicante(ds.Tables[(int)UIConstantes.ESTRUCTURA_TABLAS_ASPIRANTE.DATOS_PERSONALES], oAplicanteBE);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ds = null;
                cmd.Dispose();
                myCon.Close();
            }
            return oAplicanteBE;
        }
        public DataTable ListarCondicionAcademica()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ObtenerCondicionAcademicaAdmisionPrePacifico", myCon);
                cmd.CommandType = CommandType.StoredProcedure;   
    
                myCon.Open();
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

        private AplicanteBE poblarDatosPersonalesPorIdAplicante(DataTable dtDatosPersonales, AplicanteBE oAplicanteBE)
        {
            if (dtDatosPersonales != null && dtDatosPersonales.Rows.Count > 0)
            {
                if (oAplicanteBE == null)
                {
                    oAplicanteBE = new AplicanteBE();
                }
                if (oAplicanteBE.Direccion == null)
                {
                    oAplicanteBE.Direccion = new DireccionAplicanteBE();
                }

                DataRow drDatosPersonales = dtDatosPersonales.Rows[0];
                if (drDatosPersonales["Status"] != DBNull.Value)
                {
                    Int32 tempEstado = 0;
                    int.TryParse(drDatosPersonales["Status"].ToString(), out tempEstado);
                    oAplicanteBE.Estado = tempEstado;
                }
                if (drDatosPersonales["FirstName"] != DBNull.Value)
                {
                    oAplicanteBE.PrimerNombre = drDatosPersonales["FirstName"].ToString();
                }
                if (drDatosPersonales["MiddleName"] != DBNull.Value)
                {
                    oAplicanteBE.SegundoNombre = drDatosPersonales["MiddleName"].ToString();
                }
                if (drDatosPersonales["LastName"] != DBNull.Value)
                {
                    oAplicanteBE.Apellidos = drDatosPersonales["LastName"].ToString();
                }
                if (drDatosPersonales["Email"] != DBNull.Value)
                {
                    oAplicanteBE.CorreoPersonal = drDatosPersonales["Email"].ToString();
                }
                if (drDatosPersonales["EmailLaboral"] != DBNull.Value)
                {
                    oAplicanteBE.CorreoLaboral = drDatosPersonales["EmailLaboral"].ToString();
                }
                if (drDatosPersonales["Gender"] != DBNull.Value)
                {
                    Int32 tempGenero = 0;
                    int.TryParse(drDatosPersonales["Gender"].ToString(), out tempGenero);
                    oAplicanteBE.Genero = tempGenero;
                }
                if (drDatosPersonales["BirthDate"] != DBNull.Value)
                {
                    CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaNac;
                    DateTime.TryParse(drDatosPersonales["BirthDate"].ToString(), cultura, System.Globalization.DateTimeStyles.None, out tempFechaNac);
                    oAplicanteBE.FechaNacimiento = tempFechaNac;
                }
                if (drDatosPersonales["GovernmentId"] != DBNull.Value)
                {
                    oAplicanteBE.DocumentoIdentidad = drDatosPersonales["GovernmentId"].ToString();
                }
                /*Ini: Christian Ramirez[GIIT] - Caso43692 - 20180423*/
                if (drDatosPersonales["ubigeoNacimiento"] != DBNull.Value)
                {
                    oAplicanteBE.UbigeoNacimiento = drDatosPersonales["ubigeoNacimiento"].ToString();/*Se modifica: Christian Ramirez GIIT - Caso45903 - 20180607*/
                }
                /*Fin: Christian Ramirez[GIIT] - Caso43692 - 20180423*/

                /*DIRECCION*/
                oAplicanteBE.Direccion.IdTipoDireccion = UIConstantes.TIPO_DIRECCION.DOMICILIO;
                if (drDatosPersonales["TipoVia"] != DBNull.Value)
                {
                    oAplicanteBE.Direccion.TipoVia = drDatosPersonales["TipoVia"].ToString();
                }
                if (drDatosPersonales["Line1"] != DBNull.Value)
                {
                    oAplicanteBE.Direccion.Direccion1 = drDatosPersonales["Line1"].ToString();
                }
                if (drDatosPersonales["Number"] != DBNull.Value)
                {
                    oAplicanteBE.Direccion.Number = drDatosPersonales["Number"].ToString();
                }
                if (drDatosPersonales["Interior"] != DBNull.Value)
                {
                    oAplicanteBE.Direccion.Interior = drDatosPersonales["Interior"].ToString();
                }
                if (drDatosPersonales["Interior"] != DBNull.Value)
                {
                    String TipoVia = drDatosPersonales["TipoVia"].ToString();
                    String Direccion = drDatosPersonales["Line1"].ToString();
                    String Numero = drDatosPersonales["Number"].ToString();
                    String Interior = drDatosPersonales["Interior"].ToString();
                    oAplicanteBE.Direccion.Direccion2 = TipoVia + " " + Direccion + " " + Numero + ", Interior: " + Interior;
                }
                if (drDatosPersonales["Reference"] != DBNull.Value)
                {
                    oAplicanteBE.Direccion.Reference = drDatosPersonales["Reference"].ToString();
                }
                if (drDatosPersonales["City"] != DBNull.Value)
                {
                    oAplicanteBE.Direccion.Distrito = drDatosPersonales["City"].ToString();
                }
                if (drDatosPersonales["County"] != DBNull.Value)
                {
                    Int32 tempIdProvincia = 0;
                    int.TryParse(drDatosPersonales["County"].ToString(), out tempIdProvincia);
                    oAplicanteBE.Direccion.Provincia = tempIdProvincia;
                }
                if (drDatosPersonales["StateProvinceId"] != DBNull.Value)
                {
                    Int32 tempIdDpto = 0;
                    int.TryParse(drDatosPersonales["StateProvinceId"].ToString(), out tempIdDpto);
                    oAplicanteBE.Direccion.Departamento = tempIdDpto;
                }
                if (drDatosPersonales["Country"] != DBNull.Value)
                {
                    Int32 tempIdPais = 0;
                    int.TryParse(drDatosPersonales["Country"].ToString(), out tempIdPais);
                    oAplicanteBE.Direccion.Pais = tempIdPais;
                }

                /*-----TELEFONO-----*/
                if (oAplicanteBE.Telefono == null)
                {
                    oAplicanteBE.Telefono = new TelefonoBE();
                }
                if (drDatosPersonales["Country"] != DBNull.Value)
                {
                    oAplicanteBE.Telefono.Pais = UIConvertNull.Int32(drDatosPersonales["Country"].ToString());
                }
                if (drDatosPersonales["PhoneNumber2"] != DBNull.Value)
                {
                    oAplicanteBE.Telefono.NroTelefono = drDatosPersonales["PhoneNumber2"].ToString();
                }

                /*-----CELULAR-----*/
                if (oAplicanteBE.Celular == null)
                {
                    oAplicanteBE.Celular = new TelefonoBE();
                }
                if (drDatosPersonales["Country"] != DBNull.Value)
                {
                    oAplicanteBE.Celular.Pais = UIConvertNull.Int32(drDatosPersonales["Country"].ToString());
                }
                if (drDatosPersonales["PhoneNumber"] != DBNull.Value)
                {
                    oAplicanteBE.Celular.NroCelular = drDatosPersonales["PhoneNumber"].ToString();
                }

                if (drDatosPersonales["PrimaryCitizenship"] != DBNull.Value)
                {
                    Int32 tempNacPrimaria = 0;
                    int.TryParse(drDatosPersonales["PrimaryCitizenship"].ToString(), out tempNacPrimaria);
                    oAplicanteBE.NacionalidadPrimaria = tempNacPrimaria;
                }
                if (drDatosPersonales["CountryOfBirth"] != DBNull.Value)
                {
                    Int32 tempPaisNac = 0;
                    int.TryParse(drDatosPersonales["CountryOfBirth"].ToString(), out tempPaisNac);
                    oAplicanteBE.PaisNacimiento = tempPaisNac;
                }
                if (drDatosPersonales["CityOfBirth"] != DBNull.Value)
                {
                    Int32 tempCityOfBirth = 0;
                    int.TryParse(drDatosPersonales["CityOfBirth"].ToString(), out tempCityOfBirth);
                    oAplicanteBE.DptoNacimiento = tempCityOfBirth;
                }
                if (drDatosPersonales["PassportNumber"] != DBNull.Value)
                {
                    oAplicanteBE.NumeroPasaporte = drDatosPersonales["PassportNumber"].ToString();
                }
                if (drDatosPersonales["SessionPeriodId"] != DBNull.Value)
                {
                    Int32 tempIdPeriodo = 0;
                    int.TryParse(drDatosPersonales["SessionPeriodId"].ToString(), out tempIdPeriodo);
                    oAplicanteBE.IdPeriodoSesion = tempIdPeriodo;
                }
                if (drDatosPersonales["PrimaryPhoneId"] != DBNull.Value)
                {
                    Int32 tempPrimaryPhoneId = 0;
                    int.TryParse(drDatosPersonales["PrimaryPhoneId"].ToString(), out tempPrimaryPhoneId);
                    oAplicanteBE.IdTelefonoPrimario = tempPrimaryPhoneId;
                }
                if (drDatosPersonales["PrimaryAddressId"] != DBNull.Value)
                {
                    Int32 tempPrimaryAddressId = 0;
                    int.TryParse(drDatosPersonales["PrimaryAddressId"].ToString(), out tempPrimaryAddressId);
                    oAplicanteBE.IdDireccionPrimaria = tempPrimaryAddressId;
                }
                if (drDatosPersonales["Apellido_Mat"] != DBNull.Value)
                {
                    oAplicanteBE.Ape_Materno = drDatosPersonales["Apellido_Mat"].ToString();
                }
                if (drDatosPersonales["Apellido_Pat"] != DBNull.Value)
                {
                    oAplicanteBE.Ape_Paterno = drDatosPersonales["Apellido_Pat"].ToString();
                }
                if (drDatosPersonales["TipoDoc"] != DBNull.Value)
                {
                    oAplicanteBE.TipoDocumento = drDatosPersonales["TipoDoc"].ToString();
                }
                if (drDatosPersonales["RedID"] != DBNull.Value)
                {
                    oAplicanteBE.RedId = drDatosPersonales["RedID"].ToString();
                }
            }
            return oAplicanteBE;
        }

        public DataTable ObtenerModalidadRegistrada(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelModalidadElegida", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", AplicanteId));
                myCon.Open();
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

        public DataTable ObtenerProgramaRegistrado(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelProgramaElegido", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", AplicanteId));

                myCon.Open();
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

        public AplicanteBE ObtenerColegioRegistrado(Int32? AplicanteId)
        {
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelColegiosRegistrados", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));

                myCon.Open();
                cmd.ExecuteNonQuery();

                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds != null)
                {
                    oAplicanteBE = this.poblarDatosColegio(ds.Tables[(Int32)UIConstantes.Estruct_Tabla_Colegio.COLEGIO], oAplicanteBE);
                }
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
            return oAplicanteBE;
        }

        public AplicanteBE ObtenerUniversidadesRegistradas(Int32? AplicanteId)
        {
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelUniversidadesRegistradas", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                myCon.Open(); 
                
                cmd.ExecuteNonQuery();
                
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds != null)
                {
                    oAplicanteBE = this.poblarDatosUniversidades(ds.Tables[(Int32)UIConstantes.Estruct_Tabla_Universidad.Universidad], ds.Tables[(Int32)UIConstantes.Estruct_Tabla_Universidad.UniversidadDet], oAplicanteBE);
                }
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
            return oAplicanteBE;
        }

        public DataTable LLenarColegioRegistradoCombo(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelColegiosRegistrados", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));

                myCon.Open();
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

        private AplicanteBE poblarDatosColegio(DataTable dtEducacion, AplicanteBE oAplicanteBE)
        {
            if (dtEducacion != null && dtEducacion.Rows.Count > 0)
            {
                if (oAplicanteBE == null)
                {
                    oAplicanteBE = new AplicanteBE();
                }
                if (oAplicanteBE.LEducacion == null)
                {
                    oAplicanteBE.LEducacion = new List<EducacionBE>();
                }

                foreach (DataRow drEducacion in dtEducacion.Rows)
                {
                    EducacionBE recEducacion = new EducacionBE();
                    recEducacion.Institucion = new InstitucionBE();

                    if (drEducacion["ApplicationEducationId"] != DBNull.Value)
                    {
                        int tempIdEducacion = 0;
                        int.TryParse(drEducacion["ApplicationEducationId"].ToString(), out tempIdEducacion);
                        recEducacion.IdEducacion = tempIdEducacion;
                    }
                    //if (drEducacion["ApplicationId"] != DBNull.Value)
                    //{
                    //    int tempIdAplicacion = 0;
                    //    int.TryParse(drEducacion["ApplicationId"].ToString(), out tempIdAplicacion);
                    //    recEducacion.IdAplicacion = tempIdAplicacion;
                    //}
                    if (drEducacion["InstitutionName"] != DBNull.Value)
                    {
                        recEducacion.NombreInstitucion = drEducacion["InstitutionName"].ToString();
                    }
                    if (drEducacion["OrganizationId"] != DBNull.Value)
                    {
                        int tempIdOrganizacion = 0;
                        int.TryParse(drEducacion["OrganizationId"].ToString(), out tempIdOrganizacion);
                        recEducacion.Institucion.Codigo = tempIdOrganizacion;
                    }
                    if (drEducacion["ORG_NAME"] != DBNull.Value)
                    {
                        recEducacion.Institucion.Nombre = drEducacion["ORG_NAME"].ToString();
                    }
                    if (drEducacion["ADDRESS_LINE_1"] != DBNull.Value)
                    {
                        recEducacion.Institucion.Direccion = drEducacion["ADDRESS_LINE_1"].ToString();
                    }
                    if (drEducacion["DESC_PROVINCIA"] != DBNull.Value)
                    {
                        recEducacion.Institucion.Provincia = drEducacion["DESC_PROVINCIA"].ToString();
                    }
                    if (drEducacion["DESC_DISTRITO"] != DBNull.Value)
                    {
                        recEducacion.Institucion.Distrito = drEducacion["DESC_DISTRITO"].ToString();
                    }
                    if (drEducacion["SituacionAcademica"] != DBNull.Value)
                    {
                        recEducacion.SituacionAcademica = drEducacion["SituacionAcademica"].ToString();
                    }
                    if (drEducacion["codModular"] != DBNull.Value)
                    {
                        recEducacion.Institucion.CodigoModular = drEducacion["codModular"].ToString();
                    }
                    oAplicanteBE.LEducacion.Add(recEducacion);
                }
            }
            return oAplicanteBE;
        }

        private AplicanteBE poblarDatosUniversidades(DataTable dtEducacion, DataTable dtEducacionDetalle, AplicanteBE oAplicanteBE)
        {
            if (dtEducacion != null && dtEducacion.Rows.Count > 0)
            {
                if (oAplicanteBE == null)
                {
                    oAplicanteBE = new AplicanteBE();
                }
                if (oAplicanteBE.LEducacion == null)
                {
                    oAplicanteBE.LEducacion = new List<EducacionBE>();
                }

                foreach (DataRow drEducacion in dtEducacion.Rows)
                {
                    EducacionBE oEducacionBE = new EducacionBE();
                    oEducacionBE.Institucion = new InstitucionBE();

                    if (drEducacion["ApplicationEducationId"] != DBNull.Value)
                    {
                        int tempIdEducacion = 0;
                        int.TryParse(drEducacion["ApplicationEducationId"].ToString(), out tempIdEducacion);
                        oEducacionBE.IdEducacion = tempIdEducacion;
                    }
                    if (drEducacion["ApplicationId"] != DBNull.Value)
                    {
                        int tempIdAplicacion = 0;
                        int.TryParse(drEducacion["ApplicationId"].ToString(), out tempIdAplicacion);
                        oEducacionBE.IdAplicacion = tempIdAplicacion;
                    }
                    if (drEducacion["InstitutionName"] != DBNull.Value)
                    {
                        oEducacionBE.NombreInstitucion = drEducacion["InstitutionName"].ToString();
                    }
                    if (drEducacion["OrganizationId"] != DBNull.Value)
                    {
                        int tempIdOrganizacion = 0;
                        int.TryParse(drEducacion["OrganizationId"].ToString(), out tempIdOrganizacion);
                        oEducacionBE.Institucion.Codigo = tempIdOrganizacion;
                    }
                    if (drEducacion["ORG_NAME"] != DBNull.Value)
                    {
                        oEducacionBE.Institucion.Nombre = drEducacion["ORG_NAME"].ToString();
                    }
                    if (drEducacion["DESC_PROVINCIA"] != DBNull.Value)
                    {
                        oEducacionBE.Institucion.Provincia = drEducacion["DESC_PROVINCIA"].ToString();
                    }
                    if (drEducacion["DESC_DISTRITO"] != DBNull.Value)
                    {
                        oEducacionBE.Institucion.Distrito = drEducacion["DESC_DISTRITO"].ToString();
                    }
                    if (drEducacion["ADDRESS_LINE_1"] != DBNull.Value)
                    {
                        oEducacionBE.Institucion.Direccion = drEducacion["ADDRESS_LINE_1"].ToString();
                    }

                    ///Detalle de Educación
                    foreach (DataRow drEducacionDetalle in dtEducacionDetalle.Rows)
                    {
                        EducacionDetalleBE oEducacionDetalleBE = new EducacionDetalleBE();
                        if (drEducacionDetalle["ApplicationEducationEnrollId"] != DBNull.Value)
                        {
                            oEducacionDetalleBE.IdDetalleEducacion = UIConvertNull.Int32(drEducacionDetalle["ApplicationEducationEnrollId"].ToString());
                        }
                        if (drEducacionDetalle["ApplicationEducationId"] != DBNull.Value)
                        {
                            oEducacionDetalleBE.IdEducacion = UIConvertNull.Int32(drEducacionDetalle["ApplicationEducationId"].ToString());
                        }
                        if (drEducacionDetalle["CarreraId"] != DBNull.Value)
                        {
                            oEducacionDetalleBE.IdCarrera = UIConvertNull.Int32(drEducacionDetalle["CarreraId"].ToString());
                        }
                        if (drEducacionDetalle["StartDate"] != DBNull.Value)
                        {
                            String tmpFechaInicio = drEducacionDetalle["StartDate"].ToString();
                            CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                            DateTime tmpFechaEduInicio;
                            DateTime.TryParse(tmpFechaInicio, cultura, System.Globalization.DateTimeStyles.None, out tmpFechaEduInicio);
                            oEducacionDetalleBE.FechaInicio = tmpFechaEduInicio;
                        }
                        if (drEducacionDetalle["EndDate"] != DBNull.Value)
                        {
                            String tmpFechaFin = drEducacionDetalle["EndDate"].ToString();
                            CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                            DateTime tmpFechaEduFin;
                            DateTime.TryParse(tmpFechaFin, cultura, System.Globalization.DateTimeStyles.None, out tmpFechaEduFin);
                            oEducacionDetalleBE.FechaFin = tmpFechaEduFin;
                        }
                        if (drEducacionDetalle["quantityCycleCourse"] != DBNull.Value)
                        {
                            oEducacionDetalleBE.CantidadCiclosCursados = UIConvertNull.Int32(drEducacionDetalle["quantityCycleCourse"].ToString());
                        }
                        if (drEducacionDetalle["quantityCreditoPass"] != DBNull.Value)
                        {
                            oEducacionDetalleBE.CantidadCreditosAprobados = UIConvertNull.Int32(drEducacionDetalle["quantityCreditoPass"].ToString());
                        }
                        if (drEducacionDetalle["DegreeId"] != DBNull.Value)
                        {
                            oEducacionDetalleBE.IdGrado = UIConvertNull.Int32(drEducacionDetalle["DegreeId"].ToString());
                        }
                        if (oEducacionBE.LDetalleEducacion == null)
                        {
                            oEducacionBE.LDetalleEducacion = new List<EducacionDetalleBE>();
                        }
                        oEducacionBE.LDetalleEducacion.Add(oEducacionDetalleBE);
                    }
                    oAplicanteBE.LEducacion.Add(oEducacionBE);
                }
            }
            return oAplicanteBE;
        }

        public AplicanteBE ObtenerRendAcademicoRegistrado(Int32? AplicanteId)
        {
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelRendAcademicoRegistrado", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                myCon.Open(); 
                
                cmd.ExecuteNonQuery();
                                
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds != null)
                {
                    oAplicanteBE = this.poblarRendAcademico(ds.Tables[(Int32)UIConstantes.Estruct_Tabla_RendAcademico.ORDEN_MERITO], oAplicanteBE);
                    oAplicanteBE = this.poblarNotas(ds.Tables[(Int32)UIConstantes.Estruct_Tabla_RendAcademico.NOTAS], oAplicanteBE);
                    oAplicanteBE = this.PoblarNotasLetras(ds.Tables[(Int32)UIConstantes.Estruct_Tabla_RendAcademico.NOTAS_LETRAS], oAplicanteBE);/*Agrega:Christian Ramirez - REQ91569*/
                }
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
            return oAplicanteBE;
        }

        public Boolean ObtenerAnioAcademico(Int32? AplicanteId)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_validaAnioAcademico", myCon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                cmd.Parameters.Add("@VC_habilitar", SqlDbType.Bit).Direction = ParameterDirection.Output;
                myCon.Open();

                cmd.ExecuteNonQuery();

                //Respuesta = (int)cmd.Parameters["@flag_director"].Value;
                Respuesta = (Boolean)cmd.Parameters["@VC_habilitar"].Value;//ahi queda?dale

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
            return Respuesta;
        }

        private AplicanteBE poblarRendAcademico(DataTable dtRendAcademico, AplicanteBE oAplicanteBE)
        {
            if (dtRendAcademico != null && dtRendAcademico.Rows.Count > 0)
            {
                if (oAplicanteBE == null) oAplicanteBE = new AplicanteBE();
                if (oAplicanteBE.LDetalleEducacion == null) oAplicanteBE.LDetalleEducacion = new List<EducacionDetalleBE>();
                foreach (DataRow drEducacionDetalle in dtRendAcademico.Rows)
                {
                    EducacionDetalleBE oEducacionDetalleBE = new EducacionDetalleBE();
                    if (drEducacionDetalle["ApplicationEducationEnrollId"] != DBNull.Value)
                    {
                        oEducacionDetalleBE.IdApplicationEducationEnroll = UIConvertNull.Int32(drEducacionDetalle["ApplicationEducationEnrollId"].ToString());
                    }
                    if (drEducacionDetalle["ApplicationEducationId"] != DBNull.Value)
                    {
                        oEducacionDetalleBE.IdApplicationEducation = UIConvertNull.Int32(drEducacionDetalle["ApplicationEducationId"].ToString());
                    }
                    if (drEducacionDetalle["StartDate"] != DBNull.Value)
                    {
                        String tmpFechaInicio = drEducacionDetalle["StartDate"].ToString();
                        CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                        DateTime tmpFechaEduInicio;
                        DateTime.TryParse(tmpFechaInicio, cultura, System.Globalization.DateTimeStyles.None, out tmpFechaEduInicio);
                        oEducacionDetalleBE.FechaInicio = tmpFechaEduInicio;
                    }
                    if (drEducacionDetalle["anioInicio"] != DBNull.Value)
                    {
                        oEducacionDetalleBE.anioInicio = UIConvertNull.Int32(drEducacionDetalle["anioInicio"].ToString());
                    }
                    if (drEducacionDetalle["EndDate"] != DBNull.Value)
                    {
                        String tmpFechaFin = drEducacionDetalle["EndDate"].ToString();
                        CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                        DateTime tmpFechaEduFin;
                        DateTime.TryParse(tmpFechaFin, cultura, System.Globalization.DateTimeStyles.None, out tmpFechaEduFin);
                        oEducacionDetalleBE.FechaFin = tmpFechaEduFin;
                    }
                    if (drEducacionDetalle["DegreeId"] != DBNull.Value)
                    {
                        oEducacionDetalleBE.IdGrado = UIConvertNull.Int32(drEducacionDetalle["DegreeId"].ToString());
                    }
                    if (drEducacionDetalle["QuantityStudents"] != DBNull.Value)
                    {
                        oEducacionDetalleBE.CantidadEstudiantes = UIConvertNull.Int32(drEducacionDetalle["QuantityStudents"].ToString());
                    }
                    if (drEducacionDetalle["DegreeName"] != DBNull.Value)
                    {
                        oEducacionDetalleBE.NombreGrado = drEducacionDetalle["DegreeName"].ToString();
                    }

                    /*Ini:Christian Ramirez - REQ91569*/
                    if (drEducacionDetalle["CodTipoCalificacion"] != DBNull.Value)
                        oEducacionDetalleBE.CodTipoCalificacion = (int)drEducacionDetalle["CodTipoCalificacion"];

                    if (drEducacionDetalle["DescTipoCalificacion"] != DBNull.Value)
                        oEducacionDetalleBE.DescTipoCalificacion = drEducacionDetalle["DescTipoCalificacion"].ToString();

                    if (drEducacionDetalle["SituacionAcademica"] != DBNull.Value)
                        oEducacionDetalleBE.SituaAcademica = (int)drEducacionDetalle["SituacionAcademica"];
                    /*Fin:Christian Ramirez - REQ91569*/

                    oAplicanteBE.LDetalleEducacion.Add(oEducacionDetalleBE);
                }
            }
            return oAplicanteBE;
        }

        private AplicanteBE poblarNotas(DataTable dtNotas, AplicanteBE oAplicanteBE)
        {
            if (dtNotas != null && dtNotas.Rows.Count > 0)
            {
                if (oAplicanteBE == null)
                {
                    oAplicanteBE = new AplicanteBE();
                }
                if (oAplicanteBE.LNotas == null)
                {
                    oAplicanteBE.LNotas = new List<NotasBE>();
                }

                foreach (DataRow drNotas in dtNotas.Rows)
                {
                    NotasBE oNotasBE = new NotasBE();

                    if (drNotas["M3"] != DBNull.Value)
                    {
                        oNotasBE.M3 = UIConvertNull.Int32(drNotas["M3"]);
                    }
                    if (drNotas["L3"] != DBNull.Value)
                    {
                        oNotasBE.L3 = UIConvertNull.Int32(drNotas["L3"]);
                    }
                    if (drNotas["P3"] != DBNull.Value)
                    {
                        oNotasBE.P3 = UIConvertNull.Int32(drNotas["P3"]);
                    }

                    if (drNotas["M4"] != DBNull.Value)
                    {
                        oNotasBE.M4 = UIConvertNull.Int32(drNotas["M4"]);
                    }
                    if (drNotas["L4"] != DBNull.Value)
                    {
                        oNotasBE.L4 = UIConvertNull.Int32(drNotas["L4"]);
                    }
                    if (drNotas["P4"] != DBNull.Value)
                    {
                        oNotasBE.P4 = UIConvertNull.Int32(drNotas["P4"]);
                    }

                    if (drNotas["M5"] != DBNull.Value)
                    {
                        oNotasBE.M5 = UIConvertNull.Int32(drNotas["M5"]);
                    }
                    if (drNotas["L5"] != DBNull.Value)
                    {
                        oNotasBE.L5 = UIConvertNull.Int32(drNotas["L5"]);
                    }
                    if (drNotas["P5"] != DBNull.Value)
                    {
                        oNotasBE.P5 = UIConvertNull.Int32(drNotas["P5"]);
                    }

                    if (drNotas["ONM3"] != DBNull.Value)
                    {
                        oNotasBE.ONM3 = UIConvertNull.String(drNotas["ONM3"]);
                    }
                    if (drNotas["ONL3"] != DBNull.Value)
                    {
                        oNotasBE.ONL3 = UIConvertNull.String(drNotas["ONL3"]);
                    }
                    if (drNotas["ONP3"] != DBNull.Value)
                    {
                        oNotasBE.ONP3 = UIConvertNull.String(drNotas["ONP3"]);
                    }

                    if (drNotas["ONM4"] != DBNull.Value)
                    {
                        oNotasBE.ONM4 = UIConvertNull.String(drNotas["ONM4"]);
                    }
                    if (drNotas["ONL4"] != DBNull.Value)
                    {
                        oNotasBE.ONL4 = UIConvertNull.String(drNotas["ONL4"]);
                    }
                    if (drNotas["ONP4"] != DBNull.Value)
                    {
                        oNotasBE.ONP4 = UIConvertNull.String(drNotas["ONP4"]);
                    }

                    if (drNotas["ONM5"] != DBNull.Value)
                    {
                        oNotasBE.ONM5 = UIConvertNull.String(drNotas["ONM5"]);
                    }
                    if (drNotas["ONL5"] != DBNull.Value)
                    {
                        oNotasBE.ONL5 = UIConvertNull.String(drNotas["ONL5"]);
                    }
                    if (drNotas["ONP5"] != DBNull.Value)
                    {
                        oNotasBE.ONP5 = UIConvertNull.String(drNotas["ONP5"]);
                    }
                    oAplicanteBE.LNotas.Add(oNotasBE);
                }
            }
            return oAplicanteBE;
        }

        /*Ini:Christian Ramirez - REQ91569*/
        private AplicanteBE PoblarNotasLetras(DataTable dtNotasLetras, AplicanteBE oAplicanteBE)
        {
            if (dtNotasLetras != null && dtNotasLetras.Rows.Count > 0)
            {
                if (oAplicanteBE.ListaRendimientoAcademicoBE is null) 
                    oAplicanteBE.ListaRendimientoAcademicoBE = new List<RendimientoAcademicoBE>();

                RendimientoAcademicoBE oRendimientoAcademicoBE = null;

                #region Obtener datos de tercero
                var dtTercero = dtNotasLetras
                    .Select($"DegreeId = { UIConstantes.GRADO_PREGRADO.TERCERO_SECUNDARIA.ToString("D") }")
                    .CopyToDataTable();

                if (dtTercero.Rows.Count > 0)
                {
                    RendimientoAcademicoEvaluacionBE oRendimientoAcademicoEvaluacionBE = null;
                    DataRow dr = dtTercero.Rows[0];

                    oRendimientoAcademicoBE = new RendimientoAcademicoBE();

                    oRendimientoAcademicoBE.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
                    oRendimientoAcademicoBE.ApplicationEducationId = Convert.ToInt32(dr["ApplicationEducationId"]);
                    oRendimientoAcademicoBE.DegreeId = Convert.ToInt32(dr["DegreeId"]);
                    oRendimientoAcademicoBE.CodTipoCalificacion = Convert.ToInt32(dr["CodTipoCalificacion"]);

                    foreach (DataRow row in dtTercero.Rows)
                    {
                        oRendimientoAcademicoEvaluacionBE = new RendimientoAcademicoEvaluacionBE();
                        oRendimientoAcademicoEvaluacionBE.CursoId = Convert.ToInt32(row["UPAdmPreTBRendAcaCursoId"]);
                        oRendimientoAcademicoEvaluacionBE.CompetenciaId = Convert.ToInt32(row["UPAdmPreTBRendAcaCompetenciaId"]);
                        oRendimientoAcademicoEvaluacionBE.CalificacionId = Convert.ToInt32(row["UPAdmPreTBRendAcaCalificacionId"]);
                        oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE.Add(oRendimientoAcademicoEvaluacionBE);
                    }
                }
                oAplicanteBE.ListaRendimientoAcademicoBE.Add(oRendimientoAcademicoBE);
                #endregion

                #region Obtener datos de cuarto
                var dtCuarto = dtNotasLetras
                    .Select($"DegreeId = { UIConstantes.GRADO_PREGRADO.CUARTO_SECUNDARIA.ToString("D") }")
                    .CopyToDataTable();

                if (dtCuarto.Rows.Count > 0)
                {
                    RendimientoAcademicoEvaluacionBE oRendimientoAcademicoEvaluacionBE = null;
                    DataRow dr = dtCuarto.Rows[0];

                    oRendimientoAcademicoBE = new RendimientoAcademicoBE();
                    oRendimientoAcademicoBE.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
                    oRendimientoAcademicoBE.ApplicationEducationId = Convert.ToInt32(dr["ApplicationEducationId"]);
                    oRendimientoAcademicoBE.DegreeId = Convert.ToInt32(dr["DegreeId"]);
                    oRendimientoAcademicoBE.CodTipoCalificacion = Convert.ToInt32(dr["CodTipoCalificacion"]);

                    foreach (DataRow row in dtCuarto.Rows)
                    {
                        oRendimientoAcademicoEvaluacionBE = new RendimientoAcademicoEvaluacionBE();
                        oRendimientoAcademicoEvaluacionBE.CursoId = Convert.ToInt32(row["UPAdmPreTBRendAcaCursoId"]);
                        oRendimientoAcademicoEvaluacionBE.CompetenciaId = Convert.ToInt32(row["UPAdmPreTBRendAcaCompetenciaId"]);
                        oRendimientoAcademicoEvaluacionBE.CalificacionId = Convert.ToInt32(row["UPAdmPreTBRendAcaCalificacionId"]);
                        oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE.Add(oRendimientoAcademicoEvaluacionBE);
                    }
                }
                oAplicanteBE.ListaRendimientoAcademicoBE.Add(oRendimientoAcademicoBE);
                #endregion

                #region Obtener datos de quinto
                var dtQuinto = dtNotasLetras
                    .Select($"DegreeId = { UIConstantes.GRADO_PREGRADO.QUINTO_SECUNDARIA.ToString("D") }")
                    .CopyToDataTable();

                if (dtQuinto.Rows.Count > 0)
                {
                    RendimientoAcademicoEvaluacionBE oRendimientoAcademicoEvaluacionBE = null;
                    DataRow dr = dtQuinto.Rows[0];

                    oRendimientoAcademicoBE = new RendimientoAcademicoBE();
                    oRendimientoAcademicoBE.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
                    oRendimientoAcademicoBE.ApplicationEducationId = Convert.ToInt32(dr["ApplicationEducationId"]);
                    oRendimientoAcademicoBE.DegreeId = Convert.ToInt32(dr["DegreeId"]);
                    oRendimientoAcademicoBE.CodTipoCalificacion = Convert.ToInt32(dr["CodTipoCalificacion"]);

                    foreach (DataRow row in dtQuinto.Rows)
                    {
                        oRendimientoAcademicoEvaluacionBE = new RendimientoAcademicoEvaluacionBE();
                        oRendimientoAcademicoEvaluacionBE.CursoId = Convert.ToInt32(row["UPAdmPreTBRendAcaCursoId"]);
                        oRendimientoAcademicoEvaluacionBE.CompetenciaId = Convert.ToInt32(row["UPAdmPreTBRendAcaCompetenciaId"]);
                        oRendimientoAcademicoEvaluacionBE.CalificacionId = Convert.ToInt32(row["UPAdmPreTBRendAcaCalificacionId"]);
                        oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE.Add(oRendimientoAcademicoEvaluacionBE);
                    }
                }
                oAplicanteBE.ListaRendimientoAcademicoBE.Add(oRendimientoAcademicoBE);
                #endregion

            }

            return oAplicanteBE;
        }
        /*Fin:Christian Ramirez - REQ91569*/

        public DataTable ObtenerActExtracurricularRegistrado(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelActExtCurriRegistrado", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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
            return dt;
        }

        public AplicanteBE ObtenerIdiomaRegistrado(Int32? AplicanteId)
        {
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelIdiomasRegistrado", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                myCon.Open(); 
                
                cmd.ExecuteNonQuery();
                                
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds != null)
                {
                    oAplicanteBE = this.poblarDatosIdioma(ds.Tables[(Int32)UIConstantes.ESTRUCTURA_TABLAS.IDIOMA], oAplicanteBE);
                }
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
            return oAplicanteBE;
        }

        private AplicanteBE poblarDatosIdioma(DataTable dtIdioma, AplicanteBE oAplicanteBE)
        {
            if (dtIdioma != null && dtIdioma.Rows.Count > 0)
            {
                if (oAplicanteBE == null)
                {
                    oAplicanteBE = new AplicanteBE();
                }
                if (oAplicanteBE.LIdioma == null)
                {
                    oAplicanteBE.LIdioma = new List<IdiomaBE>();
                }
                IdiomaBE oIdiomaBE = null;
                foreach (DataRow drIdioma in dtIdioma.Rows)
                {
                    oIdiomaBE = new IdiomaBE();
                    if (drIdioma["ApplicationEducationId"] != DBNull.Value)
                    {
                        oIdiomaBE.IdApplicationEducation = UIConvertNull.Int32(drIdioma["ApplicationEducationId"].ToString());
                    }
                    if (drIdioma["ApplicationId"] != DBNull.Value)
                    {
                        oIdiomaBE.IdAplicacion = UIConvertNull.Int32(drIdioma["ApplicationId"].ToString());
                    }
                    if (drIdioma["IdiomaId"] != DBNull.Value)
                    {
                        oIdiomaBE.IdIdioma = UIConvertNull.Int32(drIdioma["IdiomaId"].ToString());
                    }
                    if (drIdioma["Nivel_Lectura"] != DBNull.Value)
                    {
                        oIdiomaBE.NivelLectura = drIdioma["Nivel_Lectura"].ToString();
                    }
                    if (drIdioma["Nivel_Escritura"] != DBNull.Value)
                    {
                        oIdiomaBE.NivelEscritura = drIdioma["Nivel_Escritura"].ToString();
                    }
                    //if (drIdioma["Nivel_Habla"] != DBNull.Value)
                    //{
                    //    oIdiomaBE.NivelHabla = drIdioma["Nivel_Habla"].ToString();
                    //}
                    if (drIdioma["CertificacionId"] != DBNull.Value)
                    {
                        oIdiomaBE.CertificacionId = int.Parse(drIdioma["CertificacionId"].ToString());
                    }
                    if (drIdioma["OtrosIdiomas"] != DBNull.Value)
                    {
                        oIdiomaBE.OtrosIdiomas = drIdioma["OtrosIdiomas"].ToString();
                    }
                    //if (drIdioma["Puntaje"] != null)
                    //{
                    //    oIdiomaBE.Puntaje = drIdioma["Puntaje"].ToString();
                    //}
                    //if (drIdioma["OtraCertificacion"] != null)
                    //{
                    //    oIdiomaBE.OtrasCertificaciones = drIdioma["OtraCertificacion"].ToString();
                    //}
                    oAplicanteBE.LIdioma.Add(oIdiomaBE);
                }
            }
            return oAplicanteBE;
        }

        public AplicanteBE ObtenerInfoPadresRegistrado(Int32? AplicanteId)
        {
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelInfoPadresRegistrado", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                myCon.Open(); 
                
                cmd.ExecuteNonQuery();

                
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds != null)
                {
                    oAplicanteBE = this.poblarDatosPadres(ds.Tables[(Int32)UIConstantes.ESTRUCTURA_TABLAS.RELACIONES], oAplicanteBE);
                }
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
            return oAplicanteBE;
        }

        public string ObtenerCodigoSAPAppUsrDefined(Int32? AplicanteId)
        {
            string _codigosap = string.Empty;
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ObtenerAppUsrDefinedCodigoSAP", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                myCon.Open(); 
                
                cmd.ExecuteNonQuery();

                
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds != null)
                {
                    _codigosap = ds.Tables[0].Rows[0]["codigosap"].ToString();
                }
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
            return _codigosap;
        }
        private AplicanteBE poblarDatosPadres(DataTable dtRelacion, AplicanteBE oAplicanteBE)
        {
            if (dtRelacion != null && dtRelacion.Rows.Count > 0)
            {
                if (oAplicanteBE == null)
                {
                    oAplicanteBE = new AplicanteBE();
                }
                if (oAplicanteBE.LRelacion == null)
                {
                    oAplicanteBE.LRelacion = new List<RelacionBE>();
                }
                RelacionBE oRelacionBE = null;
                foreach (DataRow drRelacion in dtRelacion.Rows)
                {
                    oRelacionBE = new RelacionBE();
                    if (drRelacion["ApplicationRelationshipId"] != DBNull.Value)
                    {
                        oRelacionBE.IdApplicationRelationship = UIConvertNull.Int32(drRelacion["ApplicationRelationshipId"].ToString());
                    }
                    if (drRelacion["ApplicationId"] != DBNull.Value)
                    {
                        oRelacionBE.IdAplicacion = UIConvertNull.Int32(drRelacion["ApplicationId"].ToString());
                    }
                    if (drRelacion["RelationType"] != DBNull.Value)
                    {
                        oRelacionBE.IdTipoRelacion = UIConvertNull.Int32(drRelacion["RelationType"].ToString());
                    }
                    if (drRelacion["FirstName"] != DBNull.Value)
                    {
                        oRelacionBE.Nombres = drRelacion["FirstName"].ToString();
                    }
                    if (drRelacion["LastName"] != DBNull.Value)
                    {
                        oRelacionBE.Apellido = drRelacion["LastName"].ToString();
                    }
                    if (drRelacion["Email"] != DBNull.Value)
                    {
                        oRelacionBE.CorreoPersonal = drRelacion["Email"].ToString();
                    }
                    if (drRelacion["GovernmentId"] != DBNull.Value)
                    {
                        oRelacionBE.Documento = drRelacion["GovernmentId"].ToString();
                    }
                    if (drRelacion["PhoneNumber"] != DBNull.Value)
                    {
                        oRelacionBE.NumeroTelefono = drRelacion["PhoneNumber"].ToString();
                    }
                    if (drRelacion["IsStudyUP"] != DBNull.Value)
                    {
                        oRelacionBE.EstudioAntesUP = UIConvertNull.Int32(drRelacion["IsStudyUP"].ToString());
                    }
                    if (drRelacion["TipoDocIde"] != DBNull.Value)
                    {
                        oRelacionBE.TipoDocumento = drRelacion["TipoDocIde"].ToString();
                    }
                    if (drRelacion["Deceased"] != DBNull.Value)
                    {
                        oRelacionBE.Fallecido = UIConvertNull.Int32(drRelacion["Deceased"].ToString());
                    }
                    oAplicanteBE.LRelacion.Add(oRelacionBE);
                }
            }
            return oAplicanteBE;
        }

        public AplicanteBE ObtenerInfoReferenciasRegistrados(Int32? AplicanteId)
        {
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelInfoReferenciaRegistrado", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                myCon.Open(); 
                
                cmd.ExecuteNonQuery();

                
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds != null)
                {
                    oAplicanteBE = this.poblarDatosReferencias(ds.Tables[(Int32)UIConstantes.ESTRUCTURA_TABLAS.REFERENCIAS], oAplicanteBE);
                }
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
            return oAplicanteBE;
        }

        private AplicanteBE poblarDatosReferencias(DataTable dtReferencia, AplicanteBE oAplicanteBE)
        {
            if (dtReferencia != null && dtReferencia.Rows.Count > 0)
            {
                if (oAplicanteBE == null)
                {
                    oAplicanteBE = new AplicanteBE();
                }
                if (oAplicanteBE.LReferencia == null)
                {
                    oAplicanteBE.LReferencia = new List<ReferenciaBE>();
                }
                ReferenciaBE oReferenciaBE = null;
                foreach (DataRow drReferencia in dtReferencia.Rows)
                {
                    oReferenciaBE = new ReferenciaBE();
                    if (drReferencia["ApplicationReferenceId"] != DBNull.Value)
                    {
                        oReferenciaBE.IdReferencia = UIConvertNull.Int32(drReferencia["ApplicationReferenceId"].ToString());
                    }
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
                    oAplicanteBE.LReferencia.Add(oReferenciaBE);
                }
            }
            return oAplicanteBE;
        }

        public AplicanteBE ObtenerInfoTerminosCondicionesRegistrados(Int32? AplicanteId)
        {
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelTerminoCondicionRegistrado", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                myCon.Open(); 
                
                cmd.ExecuteNonQuery();

                
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null)
                {
                    oAplicanteBE = this.poblarDatosTerminosCondiciones(ds.Tables[(Int32)UIConstantes.ESTRUCTURA_TABLAS.TERMINOS], oAplicanteBE);
                }
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
            return oAplicanteBE;
        }

        private AplicanteBE poblarDatosTerminosCondiciones(DataTable dtTerminos, AplicanteBE oAplicanteBE)
        {
            if (dtTerminos != null && dtTerminos.Rows.Count > 0)
            {
                foreach (DataRow drTerminos in dtTerminos.Rows)
                {
                    oAplicanteBE = new AplicanteBE();
                    if (drTerminos["Discapacitado"] != DBNull.Value)
                    {
                        oAplicanteBE.Discapacitado = UIConvertNull.String(drTerminos["Discapacitado"].ToString());
                    }
                    if (drTerminos["AntecedentesPoliciales"] != DBNull.Value)
                    {
                        oAplicanteBE.idAntecedentes = UIConvertNull.Int32(drTerminos["AntecedentesPoliciales"].ToString());
                    }
                    if (drTerminos["AutorizaUsoDato"] != DBNull.Value)
                    {
                        oAplicanteBE.Autorizacion = UIConvertNull.Int32(drTerminos["AutorizaUsoDato"].ToString());
                    }
                    if (drTerminos["AutorizaUsoDatoTerceros"] != DBNull.Value)
                    {
                        oAplicanteBE.AutorizacionTerceros = UIConvertNull.Int32(drTerminos["AutorizaUsoDatoTerceros"].ToString());
                    }
                    if (drTerminos["AceptTermCond"] != DBNull.Value)
                    {
                        oAplicanteBE.AceptTermCond = Convert.ToBoolean(drTerminos["AceptTermCond"]);
                    }
                    if (drTerminos["Mayor14ConsentimientoDatPer"] != DBNull.Value)
                    {
                        oAplicanteBE.Mayor14ConsentimientoDatPer = Convert.ToBoolean(drTerminos["Mayor14ConsentimientoDatPer"]);
                    }
                    if (drTerminos["ApoderadoLegalTitularDatPer"] != DBNull.Value)
                    {
                        oAplicanteBE.ApoderadoLegalTitularDatPer = Convert.ToBoolean(drTerminos["ApoderadoLegalTitularDatPer"]);
                    }
                }
            }
            return oAplicanteBE;
        }

        public AplicanteBE ObtenerExperienciasRegistradas(Int32? AplicanteId)
        {
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelExpLaboralRegistrada", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                myCon.Open(); 
                
                cmd.ExecuteNonQuery();

                
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null)
                {
                    oAplicanteBE = this.poblarDatosExperienciaLaboral(ds.Tables[(Int32)UIConstantes.ESTRUCTURA_TABLAS.EMPLEOS], oAplicanteBE);
                }
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
            return oAplicanteBE;
        }

        private AplicanteBE poblarDatosExperienciaLaboral(DataTable dtExperienciaLab, AplicanteBE oAplicanteBE)
        {
            if (dtExperienciaLab != null && dtExperienciaLab.Rows.Count > 0)
            {
                if (oAplicanteBE == null)
                {
                    oAplicanteBE = new AplicanteBE();
                }
                if (oAplicanteBE.LEmpleador == null)
                {
                    oAplicanteBE.LEmpleador = new List<EmpleadorBE>();
                }
                EmpleadorBE oEmpleadorBE = null;
                foreach (DataRow drExperiencia in dtExperienciaLab.Rows)
                {
                    oEmpleadorBE = new EmpleadorBE();
                    if (drExperiencia["ApplicationEmploymentId"] != DBNull.Value)
                    {
                        oEmpleadorBE.IdEmpleador = UIConvertNull.Int32(drExperiencia["ApplicationEmploymentId"].ToString());
                    }
                    if (drExperiencia["EmployerName"] != DBNull.Value)
                    {
                        oEmpleadorBE.NombreEmpleador = drExperiencia["EmployerName"].ToString();
                    }
                    if (drExperiencia["EmployerCharge"] != DBNull.Value)
                    {
                        oEmpleadorBE.Cargo = drExperiencia["EmployerCharge"].ToString();
                    }
                    if (drExperiencia["StartDate"] != DBNull.Value)
                    {
                        String tmpFechaInicio = drExperiencia["StartDate"].ToString();
                        CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                        DateTime tmpFechaEduInicio;
                        DateTime.TryParse(tmpFechaInicio, cultura, System.Globalization.DateTimeStyles.None, out tmpFechaEduInicio);
                        oEmpleadorBE.FechaIngreso = tmpFechaEduInicio;
                    }
                    if (drExperiencia["EndDate"] != DBNull.Value)
                    {
                        String tmpFechaFin = drExperiencia["EndDate"].ToString();
                        CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                        DateTime tmpFechaEduFin;
                        DateTime.TryParse(tmpFechaFin, cultura, System.Globalization.DateTimeStyles.None, out tmpFechaEduFin);
                        oEmpleadorBE.FechaSalida = tmpFechaEduFin;
                    }
                    oAplicanteBE.LEmpleador.Add(oEmpleadorBE);
                }
            }
            return oAplicanteBE;
        }

        public AplicanteBE ObtenerOtrosEstudiosRegistrados(Int32? AplicanteId)
        {
            DataSet ds = null;
            AplicanteBE oAplicanteBE = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelOtrosEstudiosRegistrado", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                myCon.Open();
                
                cmd.ExecuteNonQuery();

                
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds != null)
                {
                    oAplicanteBE = this.poblarDatosOtrosEstudios(ds.Tables[(Int32)UIConstantes.ESTRUCTURA_TABLAS.EDUCACION], ds.Tables[(Int32)UIConstantes.ESTRUCTURA_TABLAS.DETALLE_EDUCACION], oAplicanteBE);
                }
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
            return oAplicanteBE;
        }

        private AplicanteBE poblarDatosOtrosEstudios(DataTable dtEducacion, DataTable dtEducacionDetalle, AplicanteBE oAplicanteBE)
        {
            if (dtEducacion != null && dtEducacion.Rows.Count > 0)
            {
                if (oAplicanteBE == null)
                {
                    oAplicanteBE = new AplicanteBE();
                }
                if (oAplicanteBE.LEducacion == null)
                {
                    oAplicanteBE.LEducacion = new List<EducacionBE>();
                }

                foreach (DataRow drEducacion in dtEducacion.Rows)
                {
                    EducacionBE oEducacionBE = new EducacionBE();

                    if (drEducacion["ApplicationEducationId"] != DBNull.Value)
                    {
                        oEducacionBE.IdEducacion = UIConvertNull.Int32(drEducacion["ApplicationEducationId"]);
                    }
                    if (drEducacion["InstitutionName"] != DBNull.Value)
                    {
                        oEducacionBE.NombreInstitucion = drEducacion["InstitutionName"].ToString();
                    }

                    foreach (DataRow drEducacionDetalle in dtEducacionDetalle.Rows)
                    {
                        EducacionDetalleBE oEducacionDetalleBE = new EducacionDetalleBE();
                        if (drEducacionDetalle["ApplicationEducationEnrollId"] != DBNull.Value)
                        {
                            oEducacionDetalleBE.IdApplicationEducationEnroll = UIConvertNull.Int32(drEducacionDetalle["ApplicationEducationEnrollId"].ToString());
                        }
                        if (drEducacionDetalle["ApplicationEducationId"] != DBNull.Value)
                        {
                            oEducacionDetalleBE.IdApplicationEducation = UIConvertNull.Int32(drEducacionDetalle["ApplicationEducationId"].ToString());
                        }
                        if (drEducacionDetalle["StartDate"] != DBNull.Value)
                        {
                            String tmpFechaInicio = drEducacionDetalle["StartDate"].ToString();
                            CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                            DateTime tmpFechaEduInicio;
                            DateTime.TryParse(tmpFechaInicio, cultura, System.Globalization.DateTimeStyles.None, out tmpFechaEduInicio);
                            oEducacionDetalleBE.FechaInicio = tmpFechaEduInicio;
                        }
                        if (drEducacionDetalle["EndDate"] != DBNull.Value)
                        {
                            String tmpFechaFin = drEducacionDetalle["EndDate"].ToString();
                            CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                            DateTime tmpFechaEduFin;
                            DateTime.TryParse(tmpFechaFin, cultura, System.Globalization.DateTimeStyles.None, out tmpFechaEduFin);
                            oEducacionDetalleBE.FechaFin = tmpFechaEduFin;
                        }
                        if (drEducacionDetalle["CarreraNombre"] != DBNull.Value)
                        {
                            oEducacionDetalleBE.NombreCarrera = drEducacionDetalle["CarreraNombre"].ToString();
                        }

                        if (oEducacionBE.LDetalleEducacion == null)
                        {
                            oEducacionBE.LDetalleEducacion = new List<EducacionDetalleBE>();
                        }
                        oEducacionBE.LDetalleEducacion.Add(oEducacionDetalleBE);
                    }
                    oAplicanteBE.LEducacion.Add(oEducacionBE);
                }
            }
            return oAplicanteBE;
        }

        public DataSet ObtenerPermisoEmisionBoleta(Int32? ApplicationId)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ObtenerPermisoEmisionBoleta", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", ApplicationId));

                myCon.Open();
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

        public DataTable ObtenerEstadoPagoBoleta(Int32? ApplicationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ObtenerEstadoPagoBoleta", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", ApplicationId));

                myCon.Open();
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

        public DataTable ObtenerTextoInformativo(Int32? AplicanteId, Int32? TipoMensajesId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ObtenerTextoInformativo", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                cmd.Parameters.Add(new SqlParameter("@VC_TipoMensajesId", TipoMensajesId));

                myCon.Open();
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

        public Int32 ConsultaDNIRegistrado(Int32? AplicanteId, String NroDNI)
        {
            Int32 Registrado = 0;
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelDNIRegistrado", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", AplicanteId));
                cmd.Parameters.Add(new SqlParameter("@VC_NroDNI", NroDNI));

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Registrado = int.Parse(dt.Rows[0][0].ToString());
                }
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
            return Registrado;
            ////
            //DataTable dt = new DataTable();
            //SqlDataAdapter da = null;
            //try
            //{
            //    this.Conexion();
            //    cmd = new SqlCommand();
            //    cmd.Connection = this.connection;
            //    this.AbrirConexion();

            //    cmd.CommandText = "UPAdmPre_SelDNIRegistrado";
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.Parameters.Clear();
            //    cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", AplicanteId));
            //    cmd.Parameters.Add(new SqlParameter("@VC_NroDNI", NroDNI));

            //    da = new SqlDataAdapter(cmd);
            //    da.Fill(dt);
            //}
            //catch (Exception ex)
            //{
            //    dt = null;
            //    throw ex;
            //}
            //finally
            //{
            //    if (da != null)
            //    {
            //        da.Dispose();
            //    }
            //    cmd.Dispose();
            //    this.CerrarConexion();
            //}
            //return dt;
        }

        public Int32 ConsultaEmailRegistrado(Int32? AplicanteId, String strEmail)
        {
            Int32 Registrado = 0;
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelEmailRegistrado", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", AplicanteId));
                cmd.Parameters.Add(new SqlParameter("@VC_strEmail", strEmail));

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Registrado = int.Parse(dt.Rows[0][0].ToString());
                }
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
            return Registrado;
        }

        public DataTable ObtenerDatosPostulanteParaEntrevista(Int32? ApplicationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelDatoParaEntrevista", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", ApplicationId));

                myCon.Open();
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

        public DataTable ObtenerFlagFechaFinFormalizacion(Int32? ApplicationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ComprobarFechaFinFormalizacion", myCon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", ApplicationId));

                myCon.Open();
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

        public DataTable ObtenerEntrevistaRegistrado(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelEntrevistaRegistrado", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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
            return dt;
        }

        public DataTable ObtenerFormalizacionRegistrado(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelFormalizacionRegistrado", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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
            return dt;
        }


        public DataTable ObtenerEstadoPostulante(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {     
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelEstadoPostulante", myCon); 
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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
            return dt;
        }

        public DataTable ObtenerSiAplicaEntrevista(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ObtenerSiAplicaEntrevista", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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
            return dt;
        }

        public DataTable ObtenerNombrePostulante(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ObtenerNombresPostulante", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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
            return dt;
        }

        public String EnviarCorreoColegioNuevo(String FullName, String NomColegio, String Contacto, String Distrito, String RedId)
        {
            String codUpd = "";
            DataTable dt = new DataTable();
            SqlConnection myCon = new SqlConnection();

            myCon = this.getConexion();

            cmd = new SqlCommand("UPAdmPre_SPEnviaEmailColegioNuevo", myCon);  
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();

            cmd.Parameters.Add(new SqlParameter("@FullName", FullName));
            cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", ""));
            cmd.Parameters.Add(new SqlParameter("@VC_NomColegio", NomColegio));
            cmd.Parameters.Add(new SqlParameter("@VC_Contacto", Contacto));
            cmd.Parameters.Add(new SqlParameter("@VC_Distrito", Distrito));
            cmd.Parameters.Add(new SqlParameter("@VC_RedId", RedId));

            try
            {
                myCon.Open(); 
                codUpd = Convert.ToString(cmd.ExecuteScalar());
                //cmd.ExecuteNonQuery();
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
            return codUpd;
        }

        public DataTable ObtenerValidacionOrdenMerito(Int32? AplicantionId, String ordMeritoTer, String ordMeritoCua, String ordMeritoQui, String cantAlumTer, String cantAlumCua, String cantAlumQui, Int32? idModalidad)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion();


                if (idModalidad == 40 || idModalidad == 58 || idModalidad == 60)
                {
                    cmd = new SqlCommand("UPAdmPre_SPValidaAdmSelectiva", myCon);  
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                }
                if (idModalidad == 49 || idModalidad == 57 || idModalidad == 59)
                {
                    cmd = new SqlCommand("UPAdmPre_SPValidaAdmExcelencia", myCon);  
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                }

                cmd.Parameters.Add(new SqlParameter("@VC_AplicantionId", AplicantionId));
                if (!String.IsNullOrEmpty(ordMeritoTer))
                {
                    cmd.Parameters.Add(new SqlParameter("@pOrdenMerito3", ordMeritoTer));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@pOrdenMerito3", DBNull.Value));
                }

                if (!String.IsNullOrEmpty(ordMeritoCua))
                {
                    cmd.Parameters.Add(new SqlParameter("@pOrdenMerito4", ordMeritoCua));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@pOrdenMerito4", DBNull.Value));
                }

                if (!String.IsNullOrEmpty(ordMeritoQui))
                {
                    cmd.Parameters.Add(new SqlParameter("@pOrdenMerito5", ordMeritoQui));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@pOrdenMerito5", DBNull.Value));
                }

                if (!String.IsNullOrEmpty(cantAlumTer))
                {
                    cmd.Parameters.Add(new SqlParameter("@pCantidadAlumno3", cantAlumTer));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@pCantidadAlumno3", DBNull.Value));
                }

                if (!String.IsNullOrEmpty(cantAlumCua))
                {
                    cmd.Parameters.Add(new SqlParameter("@pCantidadAlumno4", cantAlumCua));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@pCantidadAlumno4", cantAlumCua));
                }

                if (!String.IsNullOrEmpty(cantAlumQui))
                {
                    cmd.Parameters.Add(new SqlParameter("@pCantidadAlumno5", cantAlumQui));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@pCantidadAlumno5", DBNull.Value));
                }

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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
            return dt;
        }

        public Int32 ConsultaDocumentosCompletados(Int32? AplicanteId)
        {
            Int32 Registrado = 0;
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection(); 
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelValidaDocCompleto", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Registrado = int.Parse(dt.Rows[0][0].ToString());
                }
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
            return Registrado;
        }

        public Boolean EnviaCorreoSiDocCompleto(Int32? ApplicationId, Boolean transaccionIniciada)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_EnviaCorreoDocCompleto", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@p_intApplicationID", ApplicationId));
                myCon.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Respuesta = false;
            }
            finally
            {
                myCon.Close();
            }
            return Respuesta;
        }

        public Int32 ValidarCruceHorarios(Int32? AplicanteId, string strCursos)
        {
            Int32 Registrado = 0;
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SPValidaCruceHorarios", myCon); 
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                if (AplicanteId.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", AplicanteId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_AplicanteId", DBNull.Value));
                }

                if (!String.IsNullOrEmpty(strCursos))
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CursoHorario", strCursos));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CursoHorario", DBNull.Value));
                }
                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Registrado = int.Parse(dt.Rows[0][0].ToString());
                }
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
            return Registrado;
        }

        public DataTable obtejerListadoReferentesEstado(Int32? AplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ObtenerEstadoReferencia", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationID", AplicanteId));

                myCon.Open();
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

        /*Ini:[Christian Ramirez - Caso78630]*/
        public DataTable ObtenerHorarioEclRegistrado(int aplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();

            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelHorarioEclRegistrado", myCon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", aplicanteId));

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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
            return dt;
        }


        public int ValidarEclOnline(int aplicanteId, int modalidadId)
        {
            int rpta = 0;
            SqlConnection myCon = new SqlConnection();

            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ValidarHorariosDeECL", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationID", aplicanteId));
                cmd.Parameters.Add(new SqlParameter("@VC_ModalidadID", modalidadId));

                myCon.Open();
                rpta = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myCon.Close();
            }

            return rpta;
        }

        public DataTable ObtenerEclOnline(int aplicanteId)
        {
            DataTable dt = new DataTable();
            SqlConnection myCon = new SqlConnection();
            SqlDataAdapter dap = null;

            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ObtenerHorariosDeECL", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationID", aplicanteId));

                myCon.Open();
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myCon.Close();
            }

            return dt;
        }
        /*Fin:[Christian Ramirez - Caso78630]*/
        /*Ini:[Christian Ramirez - Caso78630]*/
        public bool InsertaHorarioEcl(int aplicanteId, string horaEcl)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                cmd = new SqlCommand("UPAdmPre_InsHorarioECL", myCon);
                myCon.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", aplicanteId));
                cmd.Parameters.Add(new SqlParameter("@VC_strHoraEcl", horaEcl));
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
        /*Fin:[Christian Ramirez - Caso78630]*/
        
        public int ValidarColegioEntrevista(Int32? AplicanteId)
        {
            int rpta = 0;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ValidarColegioEntrevista", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));

                myCon.Open();
                rpta = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myCon.Close();
            }

            return rpta;

        }
        /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/

        /*Ini:[Juan Delgado - Caso81646] 20200928*/
        public DataTable ObtenerHorarioPCRegistrado(int aplicanteId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();

            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_SelHorarioPCRegistrado", myCon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", aplicanteId));

                myCon.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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
            return dt;
        }        

        public DataTable ObtenerPCOnline(int aplicanteId)
        {
            DataTable dt = new DataTable();
            SqlConnection myCon = new SqlConnection();
            SqlDataAdapter dap = null;

            try
            {
                myCon = this.getConexion();

                cmd = new SqlCommand("UPAdmPre_ObtenerHorariosDePC", myCon);                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationID", aplicanteId));

                myCon.Open();
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myCon.Close();
            }

            return dt;
        }

        public bool InsertaHorarioPC(int aplicanteId, string horaPC)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                cmd = new SqlCommand("UPAdmPre_InsHorarioPC", myCon);
                myCon.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", aplicanteId));
                cmd.Parameters.Add(new SqlParameter("@VC_strHoraPC", horaPC));
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
        /*Fin:[Juan Delgado - Caso81646] 20200928*/

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        public Int32? InsertaDatosFormDos_ModPostul(AplicanteBE oAplicanteBE, Boolean transaccionIniciada)
        {
            Int32? codInsertado = 0;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }

                myCon = this.connection; 
                cmd = new SqlCommand("UPAdmPre_InsModalidadPostulacion", myCon); 
                cmd.Transaction = this.miTransaccion;                
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_Status", oAplicanteBE.Estado));
                if (oAplicanteBE.PrimerNombre != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_FirstName", oAplicanteBE.PrimerNombre));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_FirstName", DBNull.Value));
                }
                if (oAplicanteBE.SegundoNombre != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_MiddleName", oAplicanteBE.SegundoNombre));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_MiddleName", DBNull.Value));
                }
                if (oAplicanteBE.Apellidos != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_LastName", oAplicanteBE.Apellidos));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_LastName", DBNull.Value));
                }

                /**********/
                if (oAplicanteBE.CorreoPersonal != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", oAplicanteBE.CorreoPersonal));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", DBNull.Value));
                }
                if (oAplicanteBE.FechaNacimiento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_BirthDate", oAplicanteBE.FechaNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_BirthDate", DBNull.Value));
                }
                if (oAplicanteBE.Genero.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Genero", oAplicanteBE.Genero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Genero", DBNull.Value));
                }

                if (oAplicanteBE.PaisNacimiento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryOfBirth", oAplicanteBE.PaisNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryOfBirth", DBNull.Value));
                }

                if (oAplicanteBE.DptoNacimiento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CityOfBirth", oAplicanteBE.DptoNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CityOfBirth", DBNull.Value));
                }

                if (oAplicanteBE.DocumentoIdentidad != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovermentId", oAplicanteBE.DocumentoIdentidad));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovermentId", DBNull.Value));
                }
                /**********/

                cmd.Parameters.Add(new SqlParameter("@VC_FoodPlanInterest", oAplicanteBE.EstaInteresadoPlanComida));
                cmd.Parameters.Add(new SqlParameter("@VC_DormPlanInterest", oAplicanteBE.EstaInteresadoPlanResidenciaUni));

                if (oAplicanteBE.IdConfiguracionAplicacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", oAplicanteBE.IdConfiguracionAplicacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_ProgramOfStudyId", oAplicanteBE.ProgramOfStudy));

                if (oAplicanteBE.RedId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", DBNull.Value));
                }
                if (oAplicanteBE.ModalidadPostulacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ModalidadPostula", oAplicanteBE.ModalidadPostulacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ModalidadPostula", DBNull.Value));
                }

                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", oAplicanteBE.IdAplicante));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", DBNull.Value));
                }

                if (oAplicanteBE.sessionId.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_sessionId", oAplicanteBE.sessionId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_sessionId", DBNull.Value));
                }

                if (oAplicanteBE.becaId.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_becaId", oAplicanteBE.becaId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_becaId", DBNull.Value));
                }
                if (oAplicanteBE.IdCondicionAcademica.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_IdCondicionAcademica", oAplicanteBE.IdCondicionAcademica));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_IdCondicionAcademica", DBNull.Value));
                }

                cmd.Parameters.Add("@MiRegInsert", SqlDbType.Int).Direction = ParameterDirection.Output; 
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

        public Int32? InsertaDatosFormDos_ProgramasEPU(AplicanteBE oAplicanteBE, string strCursos, Boolean transaccionIniciada)
        {
            Int32? codInsertado = 0;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }

                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_InsProgramasEPU", myCon);    
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_Status", oAplicanteBE.Estado));
                if (oAplicanteBE.PrimerNombre != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_FirstName", oAplicanteBE.PrimerNombre));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_FirstName", DBNull.Value));
                }
                if (oAplicanteBE.SegundoNombre != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_MiddleName", oAplicanteBE.SegundoNombre));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_MiddleName", DBNull.Value));
                }
                if (oAplicanteBE.Apellidos != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_LastName", oAplicanteBE.Apellidos));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_LastName", DBNull.Value));
                }

                /**********/
                if (oAplicanteBE.CorreoPersonal != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", oAplicanteBE.CorreoPersonal));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", DBNull.Value));
                }
                if (oAplicanteBE.FechaNacimiento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_BirthDate", oAplicanteBE.FechaNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_BirthDate", DBNull.Value));
                }
                if (oAplicanteBE.Genero.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Genero", oAplicanteBE.Genero));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Genero", DBNull.Value));
                }
                if (oAplicanteBE.PaisNacimiento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryOfBirth", oAplicanteBE.PaisNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryOfBirth", DBNull.Value));
                }

                if (oAplicanteBE.DptoNacimiento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CityOfBirth", oAplicanteBE.DptoNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CityOfBirth", DBNull.Value));
                }

                if (oAplicanteBE.TipoDocumento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoDocumento", oAplicanteBE.TipoDocumento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoDocumento", DBNull.Value));
                }
                if (oAplicanteBE.DocumentoIdentidad != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovermentId", oAplicanteBE.DocumentoIdentidad));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovermentId", DBNull.Value));
                }
                /**********/

                cmd.Parameters.Add(new SqlParameter("@VC_FoodPlanInterest", oAplicanteBE.EstaInteresadoPlanComida));
                cmd.Parameters.Add(new SqlParameter("@VC_DormPlanInterest", oAplicanteBE.EstaInteresadoPlanResidenciaUni));

                if (oAplicanteBE.IdConfiguracionAplicacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", oAplicanteBE.IdConfiguracionAplicacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", DBNull.Value));
                }

                if (oAplicanteBE.ProgramOfStudy != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ProgramOfStudyId", oAplicanteBE.ProgramOfStudy));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ProgramOfStudyId", DBNull.Value));
                }

                if (!String.IsNullOrEmpty(strCursos))
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CursoHorario", strCursos));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CursoHorario", DBNull.Value));
                }

                if (oAplicanteBE.RedId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", DBNull.Value));
                }

                if (oAplicanteBE.ModalidadPostulacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ModalidadPostula", oAplicanteBE.ModalidadPostulacion.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ModalidadPostula", DBNull.Value));
                }

                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", oAplicanteBE.IdAplicante.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", DBNull.Value));
                }

                if (oAplicanteBE.SituacionAcademica.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_SituacionAcademica", oAplicanteBE.SituacionAcademica.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_SituacionAcademica", DBNull.Value));
                }
                cmd.Parameters.Add("@MiRegInsert", SqlDbType.Int).Direction = ParameterDirection.Output;
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

        public DataTable InsertaDatosFormTres_DatoPersonal(AplicanteBE oAplicanteBE, Boolean transaccionIniciada)
        {
            //Boolean Respuesta = true;
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }
                
                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_InsDatoPersonal", myCon);    
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", oAplicanteBE.IdAplicante));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_Status", oAplicanteBE.Estado));
                cmd.Parameters.Add(new SqlParameter("@VC_FirstName", oAplicanteBE.PrimerNombre));
                if (oAplicanteBE.SegundoNombre != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_MiddleName", oAplicanteBE.SegundoNombre));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_MiddleName", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_LastName", oAplicanteBE.Apellidos));

                if (oAplicanteBE.CorreoPersonal != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", oAplicanteBE.CorreoPersonal));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", DBNull.Value));
                }

                if (oAplicanteBE.CorreoLaboral != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EmailLaboral", oAplicanteBE.CorreoLaboral));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EmailLaboral", DBNull.Value));
                }

                if (oAplicanteBE.FechaNacimiento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_BirthDate", oAplicanteBE.FechaNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_BirthDate", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_Gender", oAplicanteBE.Genero));

                if (oAplicanteBE.NacionalidadPrimaria.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PrimaryCitizenship", oAplicanteBE.NacionalidadPrimaria));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PrimaryCitizenship", DBNull.Value));
                }

                if (oAplicanteBE.NacionalidadSecundaria.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_SecondaryCitizenship", oAplicanteBE.NacionalidadSecundaria));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_SecondaryCitizenship", DBNull.Value));
                }
                if (oAplicanteBE.PaisNacimiento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryOfBirth", oAplicanteBE.PaisNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryOfBirth", DBNull.Value));
                }
                if (oAplicanteBE.DptoNacimiento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CityOfBirth", oAplicanteBE.DptoNacimiento.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CityOfBirth", DBNull.Value));
                }
                if (oAplicanteBE.TipoDocumento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoDocumento", oAplicanteBE.TipoDocumento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoDocumento", DBNull.Value));
                }
                if (oAplicanteBE.DocumentoIdentidad != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovernmentId", oAplicanteBE.DocumentoIdentidad));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovernmentId", DBNull.Value));
                }
                /*Ini: Christian Ramirez[GIIT] - Caso43692 - 20180423*/
                if (oAplicanteBE.UbigeoNacimiento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_UbigeoNacimiento", oAplicanteBE.UbigeoNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_UbigeoNacimiento", DBNull.Value));
                }
                /*Fin: Christian Ramirez[GIIT] - Caso43692 - 20180423*/
                if (oAplicanteBE.NumeroPasaporte != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PassportNumber", oAplicanteBE.NumeroPasaporte));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PassportNumber", DBNull.Value));
                }
                if (oAplicanteBE.EstaInteresadoPlanComida.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_FoodPlanInterest", oAplicanteBE.EstaInteresadoPlanComida));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_FoodPlanInterest", DBNull.Value));
                }
                if (oAplicanteBE.EstaInteresadoPlanResidenciaUni.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DormPlanInterest", oAplicanteBE.EstaInteresadoPlanResidenciaUni));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_DormPlanInterest", DBNull.Value));
                }
                if (oAplicanteBE.IdTelefonoPrimario.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PrimaryPhoneId", oAplicanteBE.IdTelefonoPrimario));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PrimaryPhoneId", DBNull.Value));
                }
                if (oAplicanteBE.IdDireccionPrimaria.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PrimaryAddressId", oAplicanteBE.IdDireccionPrimaria));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PrimaryAddressId", DBNull.Value));
                }
                if (oAplicanteBE.IdConfiguracionAplicacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", oAplicanteBE.IdConfiguracionAplicacion));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", DBNull.Value));
                }
                if (oAplicanteBE.RedId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", DBNull.Value));
                }
                if (oAplicanteBE.ModalidadPostulacion.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ModalidadPostula", oAplicanteBE.ModalidadPostulacion.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ModalidadPostula", DBNull.Value));
                }
                if (oAplicanteBE.Ape_Paterno != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApellidoPaterno", oAplicanteBE.Ape_Paterno));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApellidoPaterno", DBNull.Value));
                }
                if (oAplicanteBE.Ape_Materno != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApellidoMaterno", oAplicanteBE.Ape_Materno));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApellidoMaterno", DBNull.Value));
                }
                if (oAplicanteBE.DireccionConcatenado != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Direccion", oAplicanteBE.DireccionConcatenado));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Direccion", DBNull.Value));
                }

                if (oAplicanteBE.Celular != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Telephone1", oAplicanteBE.Celular.NroTelefono.ToString()));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Telephone1", DBNull.Value));
                }


                //cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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
            return dt;
        }

        //Inicio JC.DelgadoV [Preformalización]
        public DataTable PreformalizacionActualizarDatosPersonales(AplicanteBE oAplicanteBE, Boolean transaccionIniciada)
        {
            //Boolean Respuesta = true;
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }

                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ActualizarDatosPersonales", myCon);
                cmd.Transaction = this.miTransaccion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", oAplicanteBE.IdAplicante));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_FirstName", oAplicanteBE.PrimerNombre));
                if (oAplicanteBE.SegundoNombre != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_MiddleName", oAplicanteBE.SegundoNombre));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_MiddleName", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_LastName", oAplicanteBE.Apellidos));

                if (oAplicanteBE.CorreoPersonal != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", oAplicanteBE.CorreoPersonal));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Email", DBNull.Value));
                }

                if (oAplicanteBE.CorreoLaboral != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EmailLaboral", oAplicanteBE.CorreoLaboral));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_EmailLaboral", DBNull.Value));
                }

                if (oAplicanteBE.FechaNacimiento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_BirthDate", oAplicanteBE.FechaNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_BirthDate", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_Gender", oAplicanteBE.Genero));

                if (oAplicanteBE.NacionalidadPrimaria.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PrimaryCitizenship", oAplicanteBE.NacionalidadPrimaria));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PrimaryCitizenship", DBNull.Value));
                }
                
                if (oAplicanteBE.PaisNacimiento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryOfBirth", oAplicanteBE.PaisNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CountryOfBirth", DBNull.Value));
                }

                if (oAplicanteBE.DptoNacimiento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CityOfBirth", oAplicanteBE.DptoNacimiento.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_CityOfBirth", DBNull.Value));
                }

                if (oAplicanteBE.TipoDocumento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoDocumento", oAplicanteBE.TipoDocumento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_TipoDocumento", DBNull.Value));
                }

                if (oAplicanteBE.DocumentoIdentidad != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovernmentId", oAplicanteBE.DocumentoIdentidad));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_GovernmentId", DBNull.Value));
                }
                /*Ini: Christian Ramirez[GIIT] - Caso43692 - 20180423*/
                if (oAplicanteBE.UbigeoNacimiento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_UbigeoNacimiento", oAplicanteBE.UbigeoNacimiento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_UbigeoNacimiento", DBNull.Value));
                }
                /*Fin: Christian Ramirez[GIIT] - Caso43692 - 20180423*/
                if (oAplicanteBE.NumeroPasaporte != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PassportNumber", oAplicanteBE.NumeroPasaporte));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PassportNumber", DBNull.Value));
                }
                
                if (oAplicanteBE.RedId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", DBNull.Value));
                }
                
                if (oAplicanteBE.Ape_Paterno != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApellidoPaterno", oAplicanteBE.Ape_Paterno));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApellidoPaterno", DBNull.Value));
                }
                if (oAplicanteBE.Ape_Materno != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApellidoMaterno", oAplicanteBE.Ape_Materno));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApellidoMaterno", DBNull.Value));
                }
                
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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
            return dt;
        }

        public DataTable PreformalizacionRegistrarDatosFinales(AplicanteBE oAplicanteBE, Boolean transaccionIniciada)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }

                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_Preformalizacion_RegistrarDatosFinales", myCon);
                cmd.Transaction = this.miTransaccion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", oAplicanteBE.IdAplicante));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", DBNull.Value));
                }

                if (oAplicanteBE.RedId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_ProgramOfStudyId", oAplicanteBE.CambioCarrera));
                cmd.Parameters.Add(new SqlParameter("@VC_SeguroRentaEstudiantil", oAplicanteBE.SeguroRentaEstudiantil));
                cmd.Parameters.Add(new SqlParameter("@VC_ReservaMatricula", oAplicanteBE.ReservaMatricula));
                cmd.Parameters.Add(new SqlParameter("@VC_DeportistaDestacado", oAplicanteBE.DeportistaDestacado));
                cmd.Parameters.Add(new SqlParameter("@VC_AutorizaDatos", oAplicanteBE.Autorizacion));
                cmd.Parameters.Add(new SqlParameter("@VC_AutorizaDatosTerceros", oAplicanteBE.AutorizacionTerceros));


                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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
            return dt;
        }

        public DataTable PreformalizacionRegistrarFormalizacion(AplicanteBE oAplicanteBE, Boolean transaccionIniciada)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }

                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPre_Preformalizacion_RegistrarFormalizacion", myCon);
                cmd.Transaction = this.miTransaccion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", oAplicanteBE.IdAplicante));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", DBNull.Value));
                }

                if (oAplicanteBE.RedId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", DBNull.Value));
                }


                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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
            return dt;
        }

        public Boolean PreformalizacionRegistrarExamen(AplicanteBE oAplicanteBE, ExamenFormalizacionBE examen, Boolean transaccionIniciada)
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
                cmd = new SqlCommand("UPAdmPre_Preformalizacion_RegistrarExamen", myCon);
                cmd.Transaction = this.miTransaccion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", oAplicanteBE.IdAplicante));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", DBNull.Value));
                }

                if (oAplicanteBE.RedId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_Examen", examen.NomExamen));
                cmd.Parameters.Add(new SqlParameter("@VC_Estado", examen.Estado));

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

        public Boolean PreformalizacionRegistrarCursoConvalidar(AplicanteBE oAplicanteBE, CursoConvalidacionFormalizacionBE curso, Boolean transaccionIniciada)
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
                cmd = new SqlCommand("UPAdmPre_Preformalizacion_RegistrarConvalidacion", myCon);
                cmd.Transaction = this.miTransaccion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                if (oAplicanteBE.IdAplicante.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", oAplicanteBE.IdAplicante));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", DBNull.Value));
                }

                if (oAplicanteBE.RedId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", DBNull.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@VC_Curso", curso.NomCursoConvalidacion));
                cmd.Parameters.Add(new SqlParameter("@VC_Estado", curso.Estado));

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

        public RetornoBE validarPersonaSpring(Int32? ApplicationID, Boolean transaccionIniciada)
        {
            SqlDataReader dr = null;
            RetornoBE respuesta = new RetornoBE();
            respuesta.status = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }

                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPreSpValidarPersonaSpring", myCon);    
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@ApplicationID", ApplicationID));

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta.aditional = dr["existeSpring"].ToString();
                    respuesta.message = dr["codigoSII"].ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta.status = false;
                throw ex;
            }
            finally
            {
                dr.Close();
                cmd.Dispose();
                if (!transaccionIniciada)
                {
                    myCon.Close();
                }
            }
            return respuesta;
        }

        public RetornoBE registrarApplicationUserDefined(String personaAnt, String persona, Int32? ApplicationID, Boolean transaccionIniciada)
        {
            RetornoBE respuesta = new RetornoBE();
            respuesta.status = true;
            SqlConnection myCon = new SqlConnection();

            if (!transaccionIniciada)
            {
                myCon = this.getConexion();
                myCon.Open();
            }

            myCon = this.connection;
            cmd = new SqlCommand("UPAdmPreSpRegistrarApplicationUserDefined", myCon);
            cmd.Transaction = this.miTransaccion; 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();

            cmd.Parameters.Add(new SqlParameter("@ApplicationID", ApplicationID));
            cmd.Parameters.Add(new SqlParameter("@personaAnt", personaAnt));
            cmd.Parameters.Add(new SqlParameter("@persona", persona));

            try
            {
                cmd.ExecuteNonQuery(); //'S' ó 'N'
            }
            catch (Exception ex)
            {
                respuesta.status = false;
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

        public Boolean generarTransaccionSpring(Int32? ApplicationID, Boolean transaccionIniciada)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            if (!transaccionIniciada)
            {
                myCon = this.getConexion();
                myCon.Open();
            }

            myCon = this.connection;
            cmd = new SqlCommand("UPAdmPreSpGenerarTransaccionSpring", myCon);   

            cmd.Transaction = this.miTransaccion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();

            cmd.Parameters.Add(new SqlParameter("@ApplicationID", ApplicationID));

            try
            {
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

        public Boolean InsertaTerminosyCondiciones(AplicanteBE oAplicanteBE)
        {
            Boolean Respuesta = true;

            SqlConnection myCon = new SqlConnection();

            try
            {
                myCon = this.getConexion();
                cmd = new SqlCommand("UPAdmPre_InsTerminosyCondiciones", myCon);
                myCon.Open(); 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                cmd.Parameters.Add(new SqlParameter("@VC_isDiscapacitado", oAplicanteBE.isDiscapacitado));
                if (oAplicanteBE.Discapacitado != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Discapacitado", oAplicanteBE.Discapacitado));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Discapacitado", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_Autorizacion", oAplicanteBE.Autorizacion));
                cmd.Parameters.Add(new SqlParameter("@VC_AutorizacionTerceros", oAplicanteBE.AutorizacionTerceros));
                cmd.Parameters.Add(new SqlParameter("@VC_idAntecedentes", oAplicanteBE.idAntecedentes));
                cmd.Parameters.Add(new SqlParameter("@VC_AceptTermCond", oAplicanteBE.AceptTermCond));
                cmd.Parameters.Add(new SqlParameter("@VC_Mayor14ConsentimientoDatPer", oAplicanteBE.Mayor14ConsentimientoDatPer));
                cmd.Parameters.Add(new SqlParameter("@VC_ApoderadoLegalTitularDatPer", oAplicanteBE.ApoderadoLegalTitularDatPer));
                cmd.Parameters.Add(new SqlParameter("@VC_RedId", oAplicanteBE.RedId));
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

        public Boolean EnviarTerminosyCondiciones(AplicanteBE oAplicanteBE)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion(); 
                cmd = new SqlCommand("UPAdmPre_InsEnviarInscripcion", myCon);
                myCon.Open();  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                cmd.Parameters.Add(new SqlParameter("@VC_isDiscapacitado", oAplicanteBE.isDiscapacitado));
                if (oAplicanteBE.Discapacitado != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Discapacitado", oAplicanteBE.Discapacitado));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Discapacitado", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_Autorizacion", oAplicanteBE.Autorizacion));
                cmd.Parameters.Add(new SqlParameter("@VC_idAntecedentes", oAplicanteBE.idAntecedentes));
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

        public Boolean InsertaHorarioEntrevista(Int32? AplicanteId, String strHoraEntrevista)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                cmd = new SqlCommand("UPAdmPre_InsHorarioEntrevista", myCon);
                myCon.Open();   
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                cmd.Parameters.Add(new SqlParameter("@VC_strHoraEntrevista", strHoraEntrevista));
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

        public Boolean InsertaHorarioFormalizacion(Int32? AplicanteId, String strHoraEntrevista)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                cmd = new SqlCommand("UPAdmPre_InsHorarioFormalizacion", myCon);
                myCon.Open();    
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", AplicanteId));
                cmd.Parameters.Add(new SqlParameter("@VC_strHoraEntrevista", strHoraEntrevista));
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

        public Boolean registraApplicationUserDefined(string ApplicationId, string Codigo_Spring, string NroBoleta, Boolean transaccionIniciada)
        {
            SqlConnection myCon = new SqlConnection();
            //BEPostulante PostulanteBE = new BEPostulante();
            if (!transaccionIniciada)
            {
                myCon = this.getConexion();
                myCon.Open(); 
            }

            //myCon = this.connection;
            cmd = new SqlCommand("UPAdmEpgInsertaApplicationUserDefined", myCon);    
            cmd.Transaction = this.miTransaccion;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
            cmd.Parameters.Add(new SqlParameter("@CodSpring", Codigo_Spring));
            cmd.Parameters.Add(new SqlParameter("@NroBoleta", NroBoleta));


            try
            {
                cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                return false;
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
            return true;

        }

        public DataTable ValidarExisteSocioNegocio(Int32? AplicanteId, Boolean transaccionIniciada)
        {
            //Boolean Respuesta = true;
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();           

            try
            {
                if (!transaccionIniciada)
                {
                    myCon = this.getConexion();
                    myCon.Open();
                }
                
                myCon = this.connection;
                cmd = new SqlCommand("UPAdmPreSPActualizaCodigoSAP", myCon);
                cmd.Transaction = this.miTransaccion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicantId", AplicanteId));

                //cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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
            return dt;
        }

        //[Ini:Christian Ramirez - REQ114900]
        public bool InsertaDatosForm21_RequisitoDocumento(AplicanteBE oAplicanteBE, Boolean transaccionIniciada)
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
                cmd = new SqlCommand("UPAdmPre_InsRequisitoDocumento", myCon);
                cmd.Transaction = this.miTransaccion;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@VC_ModalidadPostula", oAplicanteBE.ModalidadPostulacion));
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));

                cmd.Parameters.Add("@MiRegInsert", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                int rpta = (int)cmd.Parameters["@MiRegInsert"].Value;
                if (rpta < 0) respuesta = false;
            }
            catch (Exception ex)
            {
                respuesta = false;
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
        //[Fin:Christian Ramirez - REQ114900]
        #endregion "Métodos Transaccionales"
    }
}
