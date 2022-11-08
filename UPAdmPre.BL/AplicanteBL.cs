using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UPAdmPre.BE;
using UPAdmPre.DL;
using UPAdmPre.BL.Proxy;
using SimpleInjector;

namespace UPAdmPre.BL
{
    public class AplicanteBL
    {
        string _marcado = "";
        public static Container container;
        public static IHubService hubService;

        private AplicanteDL oAplicanteDL;

        public AplicanteBL()
        { }

        #region "Métodos No Transaccionales"

        public SocioNegocioBE ObtenerSocioNegocio(DataTable dtsocio, AplicanteBE aplicantebe)
        {
            SocioNegocioBE socionegocio = new SocioNegocioBE();
            DataRow drsocio = dtsocio.Rows[0];
            if (!string.IsNullOrEmpty(drsocio["CodigoSocioNegocio"].ToString())) { socionegocio.CodigoSocioNegocio = drsocio["CodigoSocioNegocio"].ToString(); }
            if (!string.IsNullOrEmpty(drsocio["NombreSocioNegocio"].ToString())) { socionegocio.NombreSocioNegocio = drsocio["NombreSocioNegocio"].ToString(); }
            if (!string.IsNullOrEmpty(drsocio["ClasificacionSocioNegocio"].ToString())) { socionegocio.ClasificacionSocioNegocio = drsocio["ClasificacionSocioNegocio"].ToString(); }
            if (!string.IsNullOrEmpty(drsocio["NroDocumento"].ToString())) { socionegocio.NroDocumento = drsocio["NroDocumento"].ToString(); }
            if (!string.IsNullOrEmpty(drsocio["AsesorVentas"].ToString())) { socionegocio.AsesorVentas = (int)drsocio["AsesorVentas"]; }
            if (!string.IsNullOrEmpty(drsocio["GrupoSocioNegocio"].ToString())) { socionegocio.GrupoSocioNegocio = (int)drsocio["GrupoSocioNegocio"]; }
            if (aplicantebe.Telefono != null) { socionegocio.numerotelefono1 = aplicantebe.Telefono.NroCelular; }
            if (aplicantebe.Telefono1 != null) { socionegocio.numerotelefono2 = aplicantebe.Telefono1.NroCelular; }
            if (aplicantebe.Celular != null) { socionegocio.numerocelular1 = aplicantebe.Celular.NroCelular; }
            if (!string.IsNullOrEmpty(drsocio["E_mail"].ToString())) { socionegocio.email1 = drsocio["E_mail"].ToString(); }
            if (!string.IsNullOrEmpty(drsocio["ApellidoPaterno"].ToString())) { socionegocio.ApellidoPaterno = drsocio["ApellidoPaterno"].ToString(); }
            if (!string.IsNullOrEmpty(drsocio["ApellidoMaterno"].ToString())) { socionegocio.ApellidoMaterno = drsocio["ApellidoMaterno"].ToString(); }
            switch (aplicantebe.TipoDocumento)
            {
                case "D.N.I.":
                    socionegocio.TipoDocumento = "1";
                    break;
                case "PASAPORTE":
                    socionegocio.TipoDocumento = "7";
                    break;
                case "CE":
                    socionegocio.TipoDocumento = "4";
                    break;
                default:
                    socionegocio.TipoDocumento = "0";
                    break;
            }


            if (!string.IsNullOrEmpty(drsocio["PrimerNombre"].ToString())) { socionegocio.PrimerNombre = drsocio["PrimerNombre"].ToString(); }
            if (!string.IsNullOrEmpty(drsocio["SegundoNombre"].ToString())) { socionegocio.SegundoNombre = drsocio["SegundoNombre"].ToString(); }
            if (!string.IsNullOrEmpty(drsocio["FecNacimiento"].ToString())) { socionegocio.FecNacimiento = (DateTime)drsocio["FecNacimiento"]; }
            if (!string.IsNullOrEmpty(drsocio["UsuarioRed"].ToString())) { socionegocio.UsuarioRed = drsocio["UsuarioRed"].ToString(); }

            return socionegocio;
        }
        public DataTable ListarCarrerasPorModalidad(Int32? DegreeId, Int32? SettingsId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ListarCarrerasPorModalidad(DegreeId, SettingsId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
        }

        public DataTable ListarCursosPorPrograma(Int32? IdPrograma, String strUsrRedId, Int32? IdAplicante)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ListarCursosPorPrograma(IdPrograma, strUsrRedId, IdAplicante);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
        }

        public AplicanteBE ListarDatosPersonalesPorUsrRed(String UserIdRed)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ListarDatosPersonalesPorUsrRed(UserIdRed);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
        }
        public DataTable ListarCondicionAcademica()
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ListarCondicionAcademica();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
        }

        public DataTable ObtenerModalidadRegistrada(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerModalidadRegistrada(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
        }

        public DataTable ObtenerProgramaRegistrado(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerProgramaRegistrado(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
        }

        public AplicanteBE ListarDatosPersonalesPorIdAplicante(Int32? IdAplicante)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ListarDatosPersonalesPorIdAplicante(IdAplicante);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
        }

        public AplicanteBE ObtenerColegioRegistrado(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerColegioRegistrado(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AplicanteBE ObtenerUniversidadesRegistradas(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerUniversidadesRegistradas(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable LLenarColegioRegistradoCombo(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.LLenarColegioRegistradoCombo(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
        }

        public AplicanteBE ObtenerRendAcademicoRegistrado(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerRendAcademicoRegistrado(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean? ObtenerAnioAcademico(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerAnioAcademico(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }


        public DataTable ObtenerActExtracurricularRegistrado(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerActExtracurricularRegistrado(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AplicanteBE ObtenerIdiomaRegistrado(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerIdiomaRegistrado(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AplicanteBE ObtenerInfoPadresRegistrado(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerInfoPadresRegistrado(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AplicanteBE ObtenerInfoReferenciasRegistrados(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerInfoReferenciasRegistrados(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public ReferenciaBE ListarDatosReferenciaPorEmail(String strEmail, Int32? NumRefe)
        //{
        //    ReferenciaDL oReferenciaDL = null;
        //    try
        //    {
        //        oReferenciaDL = new ReferenciaDL();
        //        return oReferenciaDL.ListarDatosReferenciaPorEmail(strEmail, NumRefe);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public AplicanteBE ObtenerInfoTerminosCondicionesRegistrados(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerInfoTerminosCondicionesRegistrados(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AplicanteBE ObtenerExperienciasRegistradas(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerExperienciasRegistradas(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AplicanteBE ObtenerOtrosEstudiosRegistrados(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerOtrosEstudiosRegistrados(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean CreacionSocioNegocioSAP(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            Boolean Respuesta = true;

            try
            {
                DataTable _dtSN = new DataTable();
                DataTable _dtSNext = new DataTable();
                DataTable dt = null;

                oAplicanteDL = new AplicanteDL();
                _dtSN = oAplicanteDL.ObtenerCorrelativoSN();
                _dtSNext = oAplicanteDL.ObtenerCorrelativoSNExt();

                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                dt = oAplicanteDL.ValidarExisteSocioNegocio(oAplicanteBE.IdAplicante, true);

                oAplicanteDL.CommitTransaction();

                string strTipoDocumento = oAplicanteBE.TipoDocumento.ToString().Trim().Replace(" ", "").Replace(".", "").ToUpper();

                string cliente = string.Empty;
                string proveedor = string.Empty;

                if (strTipoDocumento == "CE" || strTipoDocumento == "PASAPORTE")
                {
                    cliente = _dtSNext.Rows[0]["Cliente"].ToString();
                    proveedor = _dtSNext.Rows[0]["Proveedor"].ToString();
                }
                else
                {
                    cliente = _dtSN.Rows[0]["Cliente"].ToString();
                    proveedor = _dtSN.Rows[0]["Proveedor"].ToString();
                }

                //=============================================
                //INSERTANDO CLIENTE SAP
                //=============================================
                if (string.IsNullOrEmpty(dt.Rows[0]["CodigoSocioNegocio"].ToString()))
                {
                    SocioNegocioBE socionegocio = null;
                    socionegocio = ObtenerSocioNegocio(dt, oAplicanteBE);
                    BEResultado resultado = new BEResultado();
                    resultado = CrearSocioNegocio(socionegocio, cliente, proveedor);
                    if (resultado.Resultado != string.Empty)
                    {
                        oAplicanteDL = new AplicanteDL();
                        return oAplicanteDL.registraApplicationUserDefined(oAplicanteBE.IdAplicante.ToString(), resultado.Resultado, "", false);
                        oAplicanteDL = null;
                    }
                    else
                    {
                        Respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }

            return Respuesta;
        }
        public DataSet ObtenerPermisoEmisionBoleta(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerPermisoEmisionBoleta(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerCorrelativoSNExt()
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerCorrelativoSNExt();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ObtenerEstadoPagoBoleta(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerEstadoPagoBoleta(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerTextoInformativo(Int32? AplicanteId, Int32? TipoMensajesId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerTextoInformativo(AplicanteId, TipoMensajesId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int32 ConsultaDNIRegistrado(Int32? AplicanteId, String NroDNI)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ConsultaDNIRegistrado(AplicanteId, NroDNI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int32 ConsultaEmailRegistrado(Int32? AplicanteId, String strEmail)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ConsultaEmailRegistrado(AplicanteId, strEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerDatosPostulanteParaEntrevista(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerDatosPostulanteParaEntrevista(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerFlagFechaFinFormalizacion(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerFlagFechaFinFormalizacion(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerEntrevistaRegistrado(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerEntrevistaRegistrado(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerFormalizacionRegistrado(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerFormalizacionRegistrado(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerEstadoPostulante(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerEstadoPostulante(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerSiAplicaEntrevista(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerSiAplicaEntrevista(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerNombrePostulante(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerNombrePostulante(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String EnviarCorreoColegioNuevo(String FullName, String NomColegio, String Contacto, String Distrito, String RedId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.EnviarCorreoColegioNuevo(FullName, NomColegio, Contacto, Distrito, RedId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerValidacionOrdenMerito(Int32? AplicantionId, String ordMeritoTer, String ordMeritoCua, String ordMeritoQui, String cantAlumTer, String cantAlumCua, String cantAlumQui, Int32? idModalidad)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ObtenerValidacionOrdenMerito(AplicantionId, ordMeritoTer, ordMeritoCua, ordMeritoQui, cantAlumTer, cantAlumCua, cantAlumQui, idModalidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int32 ConsultaDocumentosCompletados(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ConsultaDocumentosCompletados(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean? EnviaCorreoSiDocCompleto(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.EnviaCorreoSiDocCompleto(AplicanteId, true);
            }
            catch (Exception)
            {
                throw;
            }
            return Respuesta;
        }

        public Int32 ValidarCruceHorarios(Int32? AplicanteId, string strCursos)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ValidarCruceHorarios(AplicanteId, strCursos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable obtejerListadoReferentesEstado(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.obtejerListadoReferentesEstado(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
        public int ValidarColegioEntrevista(Int32? AplicanteId)
        {
            AplicanteDL oAplicanteDL = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                return oAplicanteDL.ValidarColegioEntrevista(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/

        /*Ini:[Christian Ramirez - Caso78630]*/
        public DataTable ObtenerHorarioEclRegistrado(int aplicanteId)
        {
            AplicanteDL oAplicanteDL = new AplicanteDL();
            return oAplicanteDL.ObtenerHorarioEclRegistrado(aplicanteId);
        }

        public DataTable ValidarObtenerEclOnline(int modalidadId, int aplicanteId)
        {
            AplicanteDL oAplicanteDL = new AplicanteDL();
            DataTable dt = null;

            int rpta = oAplicanteDL.ValidarEclOnline(aplicanteId, modalidadId);
            if (rpta > 0) dt = oAplicanteDL.ObtenerEclOnline(aplicanteId);

            return dt;
        }
        /*Fin:[Christian Ramirez - Caso78630]*/

        /*Ini:[Juan Delgado - Caso81646] 20200928*/
        public DataTable ObtenerHorarioPCRegistrado(int aplicanteId)
        {
            AplicanteDL oAplicanteDL = new AplicanteDL();
            return oAplicanteDL.ObtenerHorarioPCRegistrado(aplicanteId);
        }

        public DataTable ValidarObtenerPCOnline(int modalidadId, int aplicanteId)
        {
            AplicanteDL oAplicanteDL = new AplicanteDL();
            DataTable dt = null;

            dt = oAplicanteDL.ObtenerPCOnline(aplicanteId);

            return dt;
        }

        /*Fin:[Juan Delgado - Caso81646] 20200928*/

        #endregion "Métodos No Transaccionales"




        #region "Métodos Transaccionales"

        public Int32? InsertaDatosFormDos_ModPostul(AplicanteBE oAplicanteBE, string strCursos)
        {
            AplicanteDL oAplicanteDL = null;
            Int32? codInsertado = null;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO MODALIDAD DE POSTULACION
                //=============================================
                if (oAplicanteBE.IdConfiguracionAplicacion == 19)
                {
                    codInsertado = oAplicanteDL.InsertaDatosFormDos_ProgramasEPU(oAplicanteBE, strCursos, true);
                }
                else
                {
                    codInsertado = oAplicanteDL.InsertaDatosFormDos_ModPostul(oAplicanteBE, true);
                }
                oAplicanteDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oAplicanteDL.CerrarConexion();
                oAplicanteDL = null;
            }
            return codInsertado;
        }

        public Boolean InsertaDatosFormTres_DatoPersonal(AplicanteBE oAplicanteBE)
        {   
            //CMCS Se modifica a solicitud de Cesar Ruiz (inicio)
            //string strTipoDocumento = oAplicanteBE.TipoDocumento.ToString().Trim().Replace(" ", "").Replace(".", "").ToUpper();

            //DataTable _dtSN = new DataTable();
            //DataTable _dtSNext = new DataTable();
            oAplicanteDL = new AplicanteDL();
            //_dtSN = oAplicanteDL.ObtenerCorrelativoSN();
            //_dtSNext = oAplicanteDL.ObtenerCorrelativoSNExt();

            //string cliente = string.Empty;
            //string proveedor = string.Empty;

            //if (strTipoDocumento == "CE" || strTipoDocumento == "PAS")
            //{
            //    cliente = _dtSNext.Rows[0]["Cliente"].ToString();
            //    proveedor = _dtSNext.Rows[0]["Proveedor"].ToString();
            //}
            //else
            //{
            //    cliente = _dtSN.Rows[0]["Cliente"].ToString();
            //    proveedor = _dtSN.Rows[0]["Proveedor"].ToString();
            //}
            //(Fin)


            //string cliente = _dtSN.Rows[0]["Cliente"].ToString();
            //string proveedor = _dtSN.Rows[0]["Proveedor"].ToString();

            DireccionAplicanteDL oDireccionAplicanteDL = null;
            TelefonoDL oTelefonoDL = null;
            Boolean Respuesta = true;
            DataTable dt = null;
            try
            {
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                dt = oAplicanteDL.InsertaDatosFormTres_DatoPersonal(oAplicanteBE, true);

                //if (Respuesta)
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Conforme"].ToString() == "1")
                        {
                            //=============================================
                            //INSERTANDO DIRECCION
                            //=============================================
                            if (oAplicanteBE.Direccion != null)
                            {
                                oDireccionAplicanteDL = new DireccionAplicanteDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                                Respuesta = oDireccionAplicanteDL.insertarDireccionAplicante(oAplicanteBE, oAplicanteBE.Direccion, true);
                                if (Respuesta == false)
                                {
                                    return false;
                                }
                            }

                            //=============================================
                            //INSERTANDO TELEFONO PERSONAL
                            //=============================================
                            if (oAplicanteBE.Telefono != null)
                            {
                                oTelefonoDL = new TelefonoDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                                Respuesta = oTelefonoDL.insertarTelefonoAplicante(oAplicanteBE, oAplicanteBE.Telefono, true);
                                if (Respuesta == false)
                                {
                                    return false;
                                }
                            }

                            //=============================================
                            //INSERTANDO TELEFONO PERSONAL 1
                            //=============================================
                            if (oAplicanteBE.Telefono1 != null)
                            {
                                oTelefonoDL = new TelefonoDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                                Respuesta = oTelefonoDL.insertarTelefonoAplicante(oAplicanteBE, oAplicanteBE.Telefono1, true);
                                if (Respuesta == false)
                                {
                                    return false;
                                }
                            }

                            //=============================================
                            //INSERTANDO CELULAR
                            //=============================================
                            if (oAplicanteBE.Celular != null)
                            {
                                oTelefonoDL = new TelefonoDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                                Respuesta = oTelefonoDL.insertarTelefonoAplicante(oAplicanteBE, oAplicanteBE.Celular, true);
                                if (Respuesta == false)
                                {
                                    return false;
                                }
                            }

                            //=============================================
                            //INSERTANDO CELULAR 1
                            //=============================================
                            if (oAplicanteBE.Celular1 != null)
                            {
                                oTelefonoDL = new TelefonoDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                                Respuesta = oTelefonoDL.insertarTelefonoAplicante(oAplicanteBE, oAplicanteBE.Celular1, true);
                                if (Respuesta == false)
                                {
                                    return false;
                                }
                            }

                            

                        }

                    }
                }
                oAplicanteDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oAplicanteDL.CerrarConexion();
                oAplicanteDL = null;
            }

            //CMCS inicio Se comenta a solicitud de Cesar Ruiz
            ////=============================================
            ////INSERTANDO CLIENTE SAP
            ////=============================================
            //if (string.IsNullOrEmpty(dt.Rows[0]["CodigoSocioNegocio"].ToString()))
            //{
            //    SocioNegocioBE socionegocio = null;
            //    socionegocio = ObtenerSocioNegocio(dt, oAplicanteBE);
            //    BEResultado resultado = new BEResultado();
            //    resultado = CrearSocioNegocio(socionegocio, cliente, proveedor);
            //    if (resultado.Resultado != string.Empty)
            //    {
            //        oAplicanteDL = new AplicanteDL();
            //        return oAplicanteDL.registraApplicationUserDefined(oAplicanteBE.IdAplicante.ToString(), resultado.Resultado, "", false);
            //        oAplicanteDL = null;
            //    }
            //}
            //CMCS Fin Se comenta a solicitud de Cesar Ruiz

            return Respuesta;
        }

        //Inicio JC.Delgado [Preformalización]
        public Boolean PreformalizacionActualizarDatosPersonales(AplicanteBE oAplicanteBE)
        {
            oAplicanteDL = new AplicanteDL();

            DireccionAplicanteDL oDireccionAplicanteDL = null;
            TelefonoDL oTelefonoDL = null;
            Boolean Respuesta = true;
            DataTable dt = null;
            try
            {
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                dt = oAplicanteDL.PreformalizacionActualizarDatosPersonales(oAplicanteBE, true);

                //*****************************
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Conforme"].ToString() == "1")
                        {
                            //=============================================
                            //INSERTANDO DIRECCION
                            //=============================================
                            if (oAplicanteBE.Direccion != null)
                            {
                                oDireccionAplicanteDL = new DireccionAplicanteDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                                Respuesta = oDireccionAplicanteDL.actualizarDireccionAplicante(oAplicanteBE, oAplicanteBE.Direccion, true);
                                if (Respuesta == false)
                                {
                                    return false;
                                }
                            }

                            //=============================================
                            //INSERTANDO TELEFONO PERSONAL
                            //=============================================
                            if (oAplicanteBE.Telefono != null)
                            {
                                oTelefonoDL = new TelefonoDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                                Respuesta = oTelefonoDL.ActualizarTelefonoAplicante(oAplicanteBE, oAplicanteBE.Telefono, true);
                                if (Respuesta == false)
                                {
                                    return false;
                                }
                            }

                            //=============================================
                            //INSERTANDO TELEFONO PERSONAL 1
                            //=============================================
                            if (oAplicanteBE.Telefono1 != null)
                            {
                                oTelefonoDL = new TelefonoDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                                Respuesta = oTelefonoDL.ActualizarTelefonoAplicante(oAplicanteBE, oAplicanteBE.Telefono1, true);
                                if (Respuesta == false)
                                {
                                    return false;
                                }
                            }

                            //=============================================
                            //INSERTANDO CELULAR
                            //=============================================
                            if (oAplicanteBE.Celular != null)
                            {
                                oTelefonoDL = new TelefonoDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                                Respuesta = oTelefonoDL.ActualizarTelefonoAplicante(oAplicanteBE, oAplicanteBE.Celular, true);
                                if (Respuesta == false)
                                {
                                    return false;
                                }
                            }

                            //=============================================
                            //INSERTANDO CELULAR 1
                            //=============================================
                            if (oAplicanteBE.Celular1 != null)
                            {
                                oTelefonoDL = new TelefonoDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                                Respuesta = oTelefonoDL.ActualizarTelefonoAplicante(oAplicanteBE, oAplicanteBE.Celular1, true);
                                if (Respuesta == false)
                                {
                                    return false;
                                }
                            }

                        }

                    }
                }
                //*****************************

                oAplicanteDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oAplicanteDL.CerrarConexion();
                oAplicanteDL = null;
            }

            return Respuesta;
        }

        public Boolean PreformalizacionGuardarDatosFinales(AplicanteBE oAplicanteBE) 
        {
            oAplicanteDL = new AplicanteDL();
            Boolean Respuesta = true;
            DataTable dt = null;

            try
            {
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                dt = oAplicanteDL.PreformalizacionRegistrarDatosFinales(oAplicanteBE, true);

                //*****************************
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Conforme"].ToString() == "1")
                        {
                            //=============================================
                            //INSERTANDO EXÁMENES
                            //=============================================
                            if (oAplicanteBE.LExamenFormalizacion != null)
                            {
                                foreach (ExamenFormalizacionBE examen in oAplicanteBE.LExamenFormalizacion)
                                {
                                    Respuesta = oAplicanteDL.PreformalizacionRegistrarExamen(oAplicanteBE, examen, true);
                                    if (Respuesta == false)
                                    {
                                        return false;
                                    }
                                }                                
                            }

                            //=============================================
                            //INSERTANDO CURSOS CONVALIDAR
                            //=============================================
                            if (oAplicanteBE.LExamenFormalizacion != null)
                            {
                                foreach (CursoConvalidacionFormalizacionBE curso in oAplicanteBE.LCursoConvalidacionFormalizacion)
                                {
                                    Respuesta = oAplicanteDL.PreformalizacionRegistrarCursoConvalidar(oAplicanteBE, curso, true);
                                    if (Respuesta == false)
                                    {
                                        return false;
                                    }
                                }
                            }

                        }

                    }
                }
                //*****************************
                oAplicanteDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oAplicanteDL.CerrarConexion();
                oAplicanteDL = null;
            }

            return Respuesta;
        }

        public Boolean PreformalizacionGuardarFormalizacion(AplicanteBE oAplicanteBE)
        {
            oAplicanteDL = new AplicanteDL();
            Boolean Respuesta = true;
            DataTable dt = null;

            try
            {
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                dt = oAplicanteDL.PreformalizacionRegistrarFormalizacion(oAplicanteBE, true);
                
                oAplicanteDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oAplicanteDL.CerrarConexion();
                oAplicanteDL = null;
            }

            return Respuesta;
        }

        //Fin JC.Delgado [Preformalización]

        public Boolean InsertaDatosFormCuatro_ColegioProc(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            EducacionDL oEducacionDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO DATOS DE COLEGIO
                //=============================================
                if (oAplicanteBE.LEducacion != null && oAplicanteBE.LEducacion.Count > 0)
                {
                    oEducacionDL = new EducacionDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (EducacionBE oEducacionBE in oAplicanteBE.LEducacion)
                    {
                        Respuesta = oEducacionDL.insertarEducacionAplicante(oAplicanteBE, oEducacionBE, true);
                        if (Respuesta == false)
                        {
                            return false;
                        }
                    }
                }
                oEducacionDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oEducacionDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oEducacionDL.CerrarConexion();
                oEducacionDL = null;
            }
            return Respuesta;
        }

        public Boolean InsertaDatosFormVeintiuno_OrdenMerito(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            EducacionDL oEducacionDL = null;
            RendimientoAcademicoDL oRendimientoAcademicoDL = null;

            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();
                //=============================================
                //INSERTANDO DATOS DETALLE DE EDUCACIÓN
                //=============================================
                if (oAplicanteBE.LDetalleEducacion != null && oAplicanteBE.LDetalleEducacion.Count > 0)
                {
                    oEducacionDL = new EducacionDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (EducacionDetalleBE oEducacionDetalleBE in oAplicanteBE.LDetalleEducacion)
                    {
                        Respuesta = oEducacionDL.insertarDetalleEducacionFormVeintiuno(oAplicanteBE, oEducacionDetalleBE, true);
                        if (Respuesta == false)
                        {
                            oEducacionDL.RollbackTransaction();
                            return false;
                        }
                    }

                    //if (Respuesta)
                    //{
                    //    oRendimientoAcademicoDL = new RendimientoAcademicoDL(ref oAplicanteDL.connection
                    //        , ref oAplicanteDL.miTransaccion);

                    //    foreach (RendimientoAcademicoBE oRenAca in oAplicanteBE.ListaRendimientoAcademicoBE)
                    //    {
                    //        foreach (RendimientoAcademicoEvaluacionBE oRenAcaEvaluacion in oRenAca.ListaRendimientoAcademicoEvaluacionBE)
                    //        {
                    //            Respuesta = oRendimientoAcademicoDL.InsertarCompentencias(oRenAca, oRenAcaEvaluacion, oAplicanteBE.RedId);
                    //            if (Respuesta == false)
                    //            {
                    //                oEducacionDL.RollbackTransaction();
                    //                return false;
                    //            }
                    //        }
                    //    }
                    //}
                }
                oEducacionDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oEducacionDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oEducacionDL.CerrarConexion();
                oEducacionDL = null;

                if (oRendimientoAcademicoDL != null) oRendimientoAcademicoDL.CerrarConexion();
                oRendimientoAcademicoDL = null;
            }
            return Respuesta;
        }

        /*Ini:Christian Ramirez - REQ91569*/
        public Boolean InsertaDatosFormCinco_OrdenMerito_Y_NotaLetra(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            EducacionDL oEducacionDL = null;
            RendimientoAcademicoDL oRendimientoAcademicoDL = null;

            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();
                //=============================================
                //INSERTANDO DATOS DETALLE DE EDUCACIÓN
                //=============================================
                if (oAplicanteBE.LDetalleEducacion != null && oAplicanteBE.LDetalleEducacion.Count > 0)
                {
                    oEducacionDL = new EducacionDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (EducacionDetalleBE oEducacionDetalleBE in oAplicanteBE.LDetalleEducacion)
                    {
                        Respuesta = oEducacionDL.insertarDetalleEducacion(oAplicanteBE, oEducacionDetalleBE, true);
                        if (Respuesta == false)
                        {
                            oEducacionDL.RollbackTransaction();
                            return false;
                        }
                    }

                    if (Respuesta)
                    {
                        oRendimientoAcademicoDL = new RendimientoAcademicoDL(ref oAplicanteDL.connection
                            , ref oAplicanteDL.miTransaccion);

                        foreach (RendimientoAcademicoBE oRenAca in oAplicanteBE.ListaRendimientoAcademicoBE)
                        {
                            foreach (RendimientoAcademicoEvaluacionBE oRenAcaEvaluacion in oRenAca.ListaRendimientoAcademicoEvaluacionBE)
                            {
                                Respuesta = oRendimientoAcademicoDL.InsertarCompentencias(oRenAca, oRenAcaEvaluacion, oAplicanteBE.RedId);
                                if (Respuesta == false)
                                {
                                    oEducacionDL.RollbackTransaction();
                                    return false;
                                }
                            }
                        }
                    }
                }
                oEducacionDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oEducacionDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oEducacionDL.CerrarConexion();
                oEducacionDL = null;

                if (oRendimientoAcademicoDL != null) oRendimientoAcademicoDL.CerrarConexion();
                oRendimientoAcademicoDL = null;
            }
            return Respuesta;
        }
        /*Fin:Christian Ramirez - REQ91569*/

        //INI: JC.DelgadoV[RQ103950] Observaciones Pre Formalización
        public Boolean InsertaDatosFormVeninte_NotaLetra_Formalizacion(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            EducacionDL oEducacionDL = null;
            RendimientoAcademicoDL oRendimientoAcademicoDL = null;

            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();
                //=============================================
                //INSERTANDO DATOS DETALLE DE EDUCACIÓN
                //=============================================
                if (oAplicanteBE.LDetalleEducacion != null && oAplicanteBE.LDetalleEducacion.Count > 0)
                {
                    oEducacionDL = new EducacionDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (EducacionDetalleBE oEducacionDetalleBE in oAplicanteBE.LDetalleEducacion)
                    {
                        Respuesta = oEducacionDL.insertarDetalleEducacionFormVeinte(oAplicanteBE, oEducacionDetalleBE, true);
                        if (Respuesta == false)
                        {
                            oEducacionDL.RollbackTransaction();
                            return false;
                        }
                    }

                    if (Respuesta)
                    {
                        oRendimientoAcademicoDL = new RendimientoAcademicoDL(ref oAplicanteDL.connection
                            , ref oAplicanteDL.miTransaccion);

                        foreach (RendimientoAcademicoBE oRenAca in oAplicanteBE.ListaRendimientoAcademicoBE)
                        {
                            foreach (RendimientoAcademicoEvaluacionBE oRenAcaEvaluacion in oRenAca.ListaRendimientoAcademicoEvaluacionBE)
                            {
                                Respuesta = oRendimientoAcademicoDL.InsertarCompentencias(oRenAca, oRenAcaEvaluacion, oAplicanteBE.RedId);
                                if (Respuesta == false)
                                {
                                    oEducacionDL.RollbackTransaction();
                                    return false;
                                }
                            }
                        }
                    }
                }
                oEducacionDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oEducacionDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oEducacionDL.CerrarConexion();
                oEducacionDL = null;

                if (oRendimientoAcademicoDL != null) oRendimientoAcademicoDL.CerrarConexion();
                oRendimientoAcademicoDL = null;
            }
            return Respuesta;
        }
        //FIN: JC.DelgadoV[RQ103950] Observaciones Pre Formalización

        public Boolean InsertaDatosFormSeis_ActivExtracurricular(List<ActividadBE> oList)
        {
            AplicanteDL oAplicanteDL = null;
            ActividadDL oActividadDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO DATOS DETALLE DE EDUCACIÓN
                //=============================================
                if (oList != null && oList.Count > 0)
                {
                    oActividadDL = new ActividadDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (ActividadBE oActividadBE in oList)
                    {
                        Respuesta = oActividadDL.insertarActividadAplicante(oActividadBE, true);
                        if (Respuesta == false)
                        {
                            return false;
                        }
                    }
                }
                oActividadDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oActividadDL.RollbackTransaction();
                throw ex;
            }
            finally
            {
                oActividadDL.CerrarConexion();
                oActividadDL = null;
            }
            return Respuesta;
        }

        public Boolean InsertaDatosFormSiete_Idiomas(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            IdiomaDL oIdiomaDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO DATOS DE IDIOMA
                //=============================================
                if (oAplicanteBE.LIdioma != null && oAplicanteBE.LIdioma.Count > 0)
                {
                    oIdiomaDL = new IdiomaDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (IdiomaBE oIdiomaBE in oAplicanteBE.LIdioma)
                    {
                        Respuesta = oIdiomaDL.insertarIdiomaAplicante(oAplicanteBE, oIdiomaBE, true);
                        if (Respuesta == false)
                        {
                            return false;
                        }
                    }
                }
                oIdiomaDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oIdiomaDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oIdiomaDL.CerrarConexion();
                oIdiomaDL = null;
            }
            return Respuesta;
        }

        public Boolean InsertaDatosFormOcho_InfoPadres(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            RelacionDL oRelacionDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO DATOS DE PADRES
                //=============================================
                if (oAplicanteBE.LRelacion != null && oAplicanteBE.LRelacion.Count > 0)
                {
                    oRelacionDL = new RelacionDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (RelacionBE oRelacionBE in oAplicanteBE.LRelacion)
                    {
                        Respuesta = oRelacionDL.insertarRelacionAplicante(oAplicanteBE, oRelacionBE, true);

                        ////=============================================
                        ////Ingresar Contact Persons SAP
                        ////=============================================
                        //string _codigosap = oAplicanteDL.ObtenerCodigoSAPAppUsrDefined(oAplicanteBE.IdAplicante);
                        //oAplicanteBE.CodigoSap = _codigosap;
                        //try
                        //{
                        //    IngresarSocioNegocioContacto(oAplicanteBE, oRelacionBE);
                        //}
                        //catch (Exception ex) { }
                        if (Respuesta == false)
                        {
                            return false;
                        }
                    }
                }
                oRelacionDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oRelacionDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oRelacionDL.CerrarConexion();
                oRelacionDL = null;
            }
            return Respuesta;
        }

        //Inicio JC.DelgadoV [Preformalización]
        public Boolean PreformalizacionActualizar_InfoPadres(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            RelacionDL oRelacionDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //ACTUALIZANDO DATOS DE PADRES
                //=============================================
                if (oAplicanteBE.LRelacion != null && oAplicanteBE.LRelacion.Count > 0)
                {
                    oRelacionDL = new RelacionDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (RelacionBE oRelacionBE in oAplicanteBE.LRelacion)
                    {
                        Respuesta = oRelacionDL.Preformalizacion_ActualizarRelacionAplicante(oAplicanteBE, oRelacionBE, true);
                        
                        if (Respuesta == false)
                        {
                            return false;
                        }
                    }
                }
                oRelacionDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oRelacionDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oRelacionDL.CerrarConexion();
                oRelacionDL = null;
            }
            return Respuesta;
        }
        //Fin JC.DelgadoV [Preformalización]

        public Boolean InsertaDatosFormNueve_InfoReferencias(AplicanteBE oAplicanteBE)
        {
            ReferenciaDL oReferenciaDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO DATOS DE REFERENCIA
                //=============================================
                if (oAplicanteBE.LReferencia != null && oAplicanteBE.LReferencia.Count > 0)
                {
                    oReferenciaDL = new ReferenciaDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (ReferenciaBE oReferenciaBE in oAplicanteBE.LReferencia)
                    {
                        EncryptBL _EncryptBL = new EncryptBL();
                        Respuesta = oReferenciaDL.insertarReferenciaAplicante(oAplicanteBE, oReferenciaBE, true);
                        String IdAplicacionEncrypt = _EncryptBL.EncryptKey(oReferenciaBE.IdReferencia.ToString());
                        Respuesta = oReferenciaDL.enviaEmailReferenciaAplicante(oReferenciaBE.IdReferencia, IdAplicacionEncrypt, true);
                        if (Respuesta == false)
                        {
                            return false;
                        }
                    }
                }
                oReferenciaDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oReferenciaDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oReferenciaDL.CerrarConexion();
                oReferenciaDL = null;
            }
            return Respuesta;
        }

        public Boolean InsertaDatosFormDieciseis_TermRef(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO DATOS DE TERMINOS Y CONDICIONES
                //=============================================
                if (oAplicanteBE.Autorizacion != null)
                {
                    Respuesta = oAplicanteDL.InsertaTerminosyCondiciones(oAplicanteBE);
                    if (Respuesta == false)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
            return Respuesta;
        }

        public Boolean EnviarDatosFormDieciseis_TermRef(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO DATOS DE TERMINOS Y CONDICIONES
                //=============================================
                if (oAplicanteBE.Autorizacion != null)
                {
                    Respuesta = oAplicanteDL.EnviarTerminosyCondiciones(oAplicanteBE);
                    if (Respuesta == false)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
            return Respuesta;
        }

        public Boolean InsertaDatosFormDoce_EstudiosUniversitarios(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            EducacionDL oEducacionDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO DATOS DE COLEGIO
                //=============================================
                if (oAplicanteBE.LEducacion != null && oAplicanteBE.LEducacion.Count > 0)
                {
                    oEducacionDL = new EducacionDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (EducacionBE oEducacionBE in oAplicanteBE.LEducacion)
                    {
                        Respuesta = oEducacionDL.insertarUniversidaAplicante(oAplicanteBE, oEducacionBE, true);
                        if (Respuesta == false)
                        {
                            return false;
                        }
                    }
                }
                oEducacionDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oEducacionDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oEducacionDL.CerrarConexion();
                oEducacionDL = null;
            }
            return Respuesta;
        }

        public Boolean InsertaDatosFormTrece_ExperienciaLaboral(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            EmpleadorDL oEmpleadorDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO DATOS DE EXPERIENCIA LABORAL
                //=============================================
                if (oAplicanteBE.LEmpleador != null && oAplicanteBE.LEmpleador.Count > 0)
                {
                    oEmpleadorDL = new EmpleadorDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (EmpleadorBE oEmpleadorBE in oAplicanteBE.LEmpleador)
                    {
                        Respuesta = oEmpleadorDL.insertarExperienciaLaboral(oAplicanteBE, oEmpleadorBE, true);
                        if (Respuesta == false)
                        {
                            return false;
                        }
                    }
                }
                oEmpleadorDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oEmpleadorDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oEmpleadorDL.CerrarConexion();
                oEmpleadorDL = null;
            }
            return Respuesta;
        }

        public Boolean InsertaDatosFormCatorce_OtrosEstudios(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            EducacionDL oEducacionDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                // INSERTANDO DATOS DE OTROS ESTUDIOS
                //=============================================
                if (oAplicanteBE.LEducacion != null && oAplicanteBE.LEducacion.Count > 0)
                {
                    oEducacionDL = new EducacionDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    foreach (EducacionBE oEducacionBE in oAplicanteBE.LEducacion)
                    {
                        Respuesta = oEducacionDL.insertarOtrosEstudiosAplicante(oAplicanteBE, oEducacionBE, true);
                        if (Respuesta == false)
                        {
                            return false;
                        }
                    }
                    oEducacionDL.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                oEducacionDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                if (oEducacionDL != null) 
                {
                    oEducacionDL.CerrarConexion();
                    oEducacionDL = null;                
                }
            }
            return Respuesta;
        }

        public Boolean InsertaDatosFormDieciOcho_HoraEntrevista(Int32? AplicanteId, String strHoraEntrevista)
        {
            AplicanteDL oAplicanteDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO HORARIO DE ENTREVISTA
                //=============================================
                Respuesta = oAplicanteDL.InsertaHorarioEntrevista(AplicanteId, strHoraEntrevista);
                if (Respuesta == false)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
            return Respuesta;
        }

        public Boolean InsertaDatosFormVeinte_HoraFormalizacion(Int32? AplicanteId, String strHoraEntrevista)
        {
            AplicanteDL oAplicanteDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                //=============================================
                //INSERTANDO HORARIO DE FORMALIZACION
                //=============================================
                Respuesta = oAplicanteDL.InsertaHorarioFormalizacion(AplicanteId, strHoraEntrevista);
                if (Respuesta == false)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                throw ex;
            }
            finally
            {
                oAplicanteDL = null;
            }
            return Respuesta;
        }

        public Boolean EliminaActividad(ActividadBE oActividadBE)
        {
            Boolean Respuesta = true;
            ActividadDL oActividadDL = null;
            try
            {
                oActividadDL = new ActividadDL();
                Respuesta = oActividadDL.EliminaActividad(oActividadBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oActividadDL = null;
            }
            return Respuesta;
        }

        public Boolean EliminaExperienciaLaboralRegistrada(Int32? IdEmpleo, Int32? AplicanteId)
        {
            Boolean Resultado = true;
            EmpleadorDL oEmpleadorDL = null;
            try
            {
                oEmpleadorDL = new EmpleadorDL();
                Resultado = oEmpleadorDL.EliminaExperienciaLaboralRegistrada(IdEmpleo, AplicanteId);
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oEmpleadorDL = null;
            }
        }

        public Boolean EliminaEstudioUniversitarioRegistrado(Int32? IdEducacion, Int32? IdDetEducacion, Int32? AplicanteId)
        {
            Boolean Resultado = true;
            EducacionDL oEducacionDL = null;
            try
            {
                oEducacionDL = new EducacionDL();
                Resultado = oEducacionDL.EliminaEstudioUniversitarioRegistrado(IdEducacion, IdDetEducacion, AplicanteId);
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oEducacionDL = null;
            }
        }

        public Boolean EliminaInfoReferenciaRegistrada(Int32? IdReferencia, Int32? AplicanteId)
        {
            Boolean Resultado = true;
            ReferenciaDL oReferenciaDL = null;
            try
            {
                oReferenciaDL = new ReferenciaDL();
                Resultado = oReferenciaDL.EliminaInfoReferenciaRegistrada(IdReferencia, AplicanteId);
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oReferenciaDL = null;
            }
        }

        public Boolean EliminaInfoPadresRegistrada(Int32? IdRelacionFam, Int32? AplicanteId)
        {
            Boolean Resultado = true;
            RelacionDL oRelacionDL = null;
            try
            {
                oRelacionDL = new RelacionDL();
                Resultado = oRelacionDL.EliminaInfoPadresRegistrada(IdRelacionFam, AplicanteId);
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oRelacionDL = null;
            }
        }

        public Boolean EliminaIdiomaRegistrado(Int32? IdApplicationEducation, Int32? AplicanteId)
        {
            Boolean Resultado = true;
            IdiomaDL oIdiomaDL = null;
            try
            {
                oIdiomaDL = new IdiomaDL();
                Resultado = oIdiomaDL.EliminaIdiomaRegistrado(IdApplicationEducation, AplicanteId);
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oIdiomaDL = null;
            }
        }

        public Boolean EliminaColegioRegistrado(Int32? IdEducacion, Int32? AplicanteId)
        {
            Boolean Resultado = true;
            EducacionDL oEducacionDL = null;
            try
            {
                oEducacionDL = new EducacionDL();
                Resultado = oEducacionDL.EliminaColegioRegistrado(IdEducacion, AplicanteId);
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oEducacionDL = null;
            }
        }

        public BEResultado CrearSocioNegocio(SocioNegocioBE socionegocio, string cliente, string proveedor)
        {
            
            if (!string.IsNullOrEmpty(socionegocio.CodPowerCampus)) { socionegocio.CodPowerCampus = socionegocio.CodPowerCampus.Replace("P", ""); }


            string strTipoPersona = string.Empty;


            Int32 caracteresDocumento = 0;
            Int32 caracterInicio = 0; 


            if (socionegocio.TipoDocumento == "4" || socionegocio.TipoDocumento == "7")
            {
                strTipoPersona = "SND";
            }
            else
            {
                caracteresDocumento = socionegocio.NroDocumento.Length;
                caracterInicio = Convert.ToInt32(socionegocio.NroDocumento.Substring(0, 2));

                if (caracteresDocumento == 8 || caracteresDocumento == 9)
                {
                    strTipoPersona = "TPN";
                }
                else if (caracteresDocumento == 11 && (caracterInicio == 10 || caracterInicio == 17 || caracterInicio == 15))
                {
                    strTipoPersona = "TPN";
                }
                else if (caracteresDocumento == 11 && caracterInicio == 20)
                {
                    strTipoPersona = "TPJ";
                }
                else
                {
                    strTipoPersona = "SND";
                }

            }

            SocioNegocioRespuesta oSocioNegocioRespuesta = new SocioNegocioRespuesta();
            SocioNegocioRespuesta oSocioNegocioRespuestap = new SocioNegocioRespuesta();

            BEResultado resultado = new BEResultado();
            resultado.Status = "0";
            resultado.Message = "";

            container = new Container();
            container.Register<IHubService>(() => new HubServiceClient("HubHTTP"));

            hubService = container.GetInstance<IHubService>();

            CamposAdicionalesLista camposAdicionales = new CamposAdicionalesLista();
            CamposAdicionalesLista camposAdicionalesProv = new CamposAdicionalesLista();

            CamposAdicionales _ApellidoPaterno = new CamposAdicionales() { Item1 = "U_SYP_BPAP", Item2 = socionegocio.ApellidoPaterno, Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_ApellidoPaterno);
            camposAdicionalesProv.Add(_ApellidoPaterno);

            CamposAdicionales _ApellidoMaterno = new CamposAdicionales() { Item1 = "U_SYP_BPAM", Item2 = socionegocio.ApellidoMaterno, Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_ApellidoMaterno);
            camposAdicionalesProv.Add(_ApellidoMaterno);

            CamposAdicionales _PrimerNombre = new CamposAdicionales() { Item1 = "U_SYP_BPNO", Item2 = socionegocio.PrimerNombre, Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_PrimerNombre);
            camposAdicionalesProv.Add(_PrimerNombre);

            CamposAdicionales _SegundoNombre = new CamposAdicionales() { Item1 = "U_SYP_BPN2", Item2 = socionegocio.SegundoNombre, Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_SegundoNombre);
            camposAdicionalesProv.Add(_SegundoNombre);

            CamposAdicionales _FecNacimiento = new CamposAdicionales() { Item1 = "U_UP_FECNAC", Item2 = null, Item3 = null, Item4 = null, Item5 = socionegocio.FecNacimiento };
            camposAdicionales.Add(_FecNacimiento);
            camposAdicionalesProv.Add(_FecNacimiento);

            CamposAdicionales _TipoDocumento = new CamposAdicionales() { Item1 = "U_SYP_BPTD", Item2 = socionegocio.TipoDocumento, Item3 = null, Item4 = null, Item5 = null };
            CamposAdicionales _TipoDocumentoProv = new CamposAdicionales() { Item1 = "U_SYP_BPTD", Item2 = "6", Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_TipoDocumento);
            camposAdicionalesProv.Add(_TipoDocumentoProv);

            CamposAdicionales _TipoPersona = new CamposAdicionales() { Item1 = "U_SYP_BPTP", Item2 = strTipoPersona, Item3 = null, Item4 = null, Item5 = null };
            CamposAdicionales _TipoPersonaProv = new CamposAdicionales() { Item1 = "U_SYP_BPTP", Item2 = "TPJ", Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_TipoPersona);
            camposAdicionalesProv.Add(_TipoPersonaProv);

            if (socionegocio.CodPowerCampus != string.Empty && socionegocio.CodPowerCampus != null)
            {
                CamposAdicionales _CodPowerCampus = new CamposAdicionales() { Item1 = "U_UP_CODALU", Item2 = socionegocio.CodPowerCampus.Replace("P", ""), Item3 = null, Item4 = null, Item5 = null };
                camposAdicionales.Add(_CodPowerCampus);
                camposAdicionalesProv.Add(_CodPowerCampus);
            }

            CamposAdicionales _UsuarioRed = new CamposAdicionales() { Item1 = "U_UP_USURED", Item2 = socionegocio.UsuarioRed, Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_UsuarioRed);
            camposAdicionalesProv.Add(_UsuarioRed);

            CamposAdicionales _U_UP_ORIGEN = new CamposAdicionales() { Item1 = "U_UP_ORIGEN", Item2 = socionegocio.Origen, Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_U_UP_ORIGEN);
            camposAdicionalesProv.Add(_U_UP_ORIGEN);
            

            //Tuple<string, string, int?, double?, DateTime?> _CodSpring = new Tuple<string, string, int?, double?, DateTime?>("U_UP_CODSPR", "123456", null, null, null);
            CamposAdicionales _CodSpring = new CamposAdicionales() { Item1 = "U_UP_CODSPR", Item2 = "", Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_CodSpring);

            //Tuple<string, string, int?, double?, DateTime?> _U_UP_PROCOR = new Tuple<string, string, int?, double?, DateTime?>("U_UP_PROCOR", "U_UP_PROCOR", null, null, null);
            CamposAdicionales _U_UP_PROCOR = new CamposAdicionales() { Item1 = "U_UP_PROCOR", Item2 = "", Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_U_UP_PROCOR);

            //Tuple<string, string, int?, double?, DateTime?> _U_UP_DEUTRI = new Tuple<string, string, int?, double?, DateTime?>("U_UP_DEUTRI", "Y", null, null, null);
            CamposAdicionales _U_UP_DEUTRI = new CamposAdicionales() { Item1 = "U_UP_DEUTRI", Item2 = "", Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_U_UP_DEUTRI);

            //Tuple<string, string, int?, double?, DateTime?> _U_UP_DEUFIN = new Tuple<string, string, int?, double?, DateTime?>("U_UP_DEUFIN", "Y", null, null, null);
            CamposAdicionales _U_UP_DEUFIN = new CamposAdicionales() { Item1 = "U_UP_DEUFIN", Item2 = "", Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_U_UP_DEUFIN);

            //Tuple<string, string, int?, double?, DateTime?> _U_UP_REFCOM = new Tuple<string, string, int?, double?, DateTime?>("U_UP_REFCOM", "U_UP_REFCOM", null, null, null);
            CamposAdicionales _U_UP_REFCOM = new CamposAdicionales() { Item1 = "U_UP_REFCOM", Item2 = "", Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_U_UP_REFCOM);

            //Tuple<string, string, int?, double?, DateTime?> _U_UP_ITAR = new Tuple<string, string, int?, double?, DateTime?>("U_UP_ITAR", "1", null, null, null);
            CamposAdicionales _U_UP_ITAR = new CamposAdicionales() { Item1 = "U_UP_ITAR", Item2 = "1", Item3 = null, Item4 = null, Item5 = null };
            camposAdicionales.Add(_U_UP_ITAR);


            SocioNegocio socioNegocioc = new SocioNegocio
            {
                CodigoSocioNegocio = cliente,
                NombreSocioNegocio = socionegocio.PrimerNombre + " " + socionegocio.SegundoNombre + " " + socionegocio.ApellidoPaterno + " " + socionegocio.ApellidoMaterno,
                ClasificacionSocioNegocio = "C",
                NroDocumento = socionegocio.NroDocumento,
                AsesorVentas = socionegocio.AsesorVentas,
                GrupoSocioNegocio = socionegocio.GrupoSocioNegocio,
                Tel1 = socionegocio.numerotelefono1,
                Tel2 = socionegocio.numerotelefono2,
                TelefonoCelular = socionegocio.numerocelular1,
                Fax = "",
                E_mail = socionegocio.email1,
                Alias = socionegocio.PrimerNombre,
                CamposAdicionales = camposAdicionales,
                //SocioNegocioContactoLista = _listSocioNegocioContacto,
                //SocioNegocioDireccionLista = _listSocioNegocioDireccion
            };
            oSocioNegocioRespuesta = hubService.CrearSocioNegocio(socioNegocioc);


            SocioNegocio socioNegociop = new SocioNegocio();
            socioNegociop = socioNegocioc;
            socioNegociop.CodigoSocioNegocio = proveedor;
            socioNegociop.ClasificacionSocioNegocio = "S";
            socioNegociop.GrupoSocioNegocio = 101;
            socioNegociop.CamposAdicionales = camposAdicionalesProv;

            oSocioNegocioRespuestap = hubService.CrearSocioNegocio(socioNegociop);
            resultado.Resultado = oSocioNegocioRespuesta.CardCode;
            return resultado;
        }

        //public bool registraApplicationUserDefined(string ApplicationId, string Codigo_Spring, string NroBoleta)
        //{
        //    //DataTable dt = null;
        //    AplicanteDL oAplicanteDL = null;
        //    oAplicanteDL = new AplicanteDL();
        //}

        public bool IngresarSocioNegocioContacto(AplicanteBE oAplicanteBE, RelacionBE oRelacionBE)
        {
            container = new Container();
            container.Register<IHubService>(() => new HubServiceClient("HubHTTP"));

            hubService = container.GetInstance<IHubService>();

            bool exito = true;
            SocioNegocioContactoLista socioNegocioContactoLista = new SocioNegocioContactoLista();
            SocioNegocioContacto socionegociocontacto = new SocioNegocioContacto
            {
                NombreContacto = oRelacionBE.Nombres,
                ApellidoContacto = oRelacionBE.Apellido,
                TelfContacto = oRelacionBE.NumeroTelefono,
                CorreoContacto = oRelacionBE.CorreoPersonal

            };
            socioNegocioContactoLista.Add(socionegociocontacto);

            SocioNegocio socionegocio = new SocioNegocio
            {
                CodigoSocioNegocio = oAplicanteBE.CodigoSap,
                SocioNegocioContactoLista = socioNegocioContactoLista
            };

            hubService.ActualizarSocioNegocio(socionegocio);

            return exito;
        }

        public string GenerarOVentaDerechoAdmision(DataSet _dsderecho)
        {
            string mensaje = "Exito";

            OVentaHeaderBE _BEOVentaHeaderDer = null;
            OVentaDetailBE _BEOVentaDetailDer = null;

            //DataSet _ds = new DataSet();
            //_ds = ObtenerOVentaCabeceraSAP(_up_id);
            if (_dsderecho != null)
            {
                foreach (DataTable _dt in _dsderecho.Tables)
                {
                    string _tipodato = _dt.Rows[0]["TipoDato"].ToString();
                    switch (_tipodato)
                    {
                        case "1M":
                            _BEOVentaHeaderDer = new OVentaHeaderBE();
                            _BEOVentaHeaderDer.Periodo = _dt.Rows[0]["Periodo"].ToString();
                            _BEOVentaHeaderDer.UnidadNegocio = _dt.Rows[0]["UnidadNegocio"].ToString();
                            _BEOVentaHeaderDer.CentroCosto = _dt.Rows[0]["CentroCosto"].ToString();
                            _BEOVentaHeaderDer.Establecimiento = _dt.Rows[0]["Establecimiento"].ToString();
                            _BEOVentaHeaderDer.TipoDocumento = _dt.Rows[0]["TipoDocumento"].ToString();
                            _BEOVentaHeaderDer.Serie = _dt.Rows[0]["Serie"].ToString();
                            _BEOVentaHeaderDer.Cuota = _dt.Rows[0]["Cuota"].ToString();
                            _BEOVentaHeaderDer.Usuario = _dt.Rows[0]["Usuario"].ToString();
                            _BEOVentaHeaderDer.UnidadReplicacion = _dt.Rows[0]["UnidadReplicacion"].ToString();
                            _BEOVentaHeaderDer.CodigoSpring = _dt.Rows[0]["CodigoSpring"].ToString();
                            _BEOVentaHeaderDer.FechaDocumento = _dt.Rows[0]["FechaDocumento"].ToString();
                            _BEOVentaHeaderDer.Proyecto = _dt.Rows[0]["Proyecto"].ToString();
                            _BEOVentaHeaderDer.a_nus_per_cod = _dt.Rows[0]["a_nus_per_cod"].ToString();
                            _BEOVentaHeaderDer.a_nus_per_nom = _dt.Rows[0]["a_nus_per_nom"].ToString();
                            _BEOVentaHeaderDer.indAuspicio = _dt.Rows[0]["indAuspicio"].ToString();
                            _BEOVentaHeaderDer.montoCuota = (decimal)_dt.Rows[0]["montoCuota"];
                            _BEOVentaHeaderDer.EsCEUP = _dt.Rows[0]["EsCEUP"].ToString();
                            _BEOVentaHeaderDer.Escala = _dt.Rows[0]["Escala"].ToString();
                            _BEOVentaHeaderDer.NroCreditos = (decimal)_dt.Rows[0]["NroCreditos"];
                            _BEOVentaHeaderDer.FecVencimiento = _dt.Rows[0]["FecVencimiento"].ToString();
                            _BEOVentaHeaderDer.MonedaDocumento = _dt.Rows[0]["MonedaDocumento"].ToString();
                            _BEOVentaHeaderDer.Comentarios = _dt.Rows[0]["Comentarios"].ToString();
                            _BEOVentaHeaderDer.TipoDato = _dt.Rows[0]["TipoDato"].ToString();
                            _BEOVentaHeaderDer.ApplicationId = (int)_dt.Rows[0]["ApplicationId"];
                            _BEOVentaHeaderDer.CodigoEmpresa = _dt.Rows[0]["CodigoEmpresa"].ToString();
                            _BEOVentaHeaderDer.montoCuotaEmpresa = (decimal)_dt.Rows[0]["montoCuotaEmpresa"];
                            _BEOVentaHeaderDer.Identificador = _dt.Rows[0]["Identificador"].ToString();
                            _BEOVentaHeaderDer.Propietario = (int)_dt.Rows[0]["Propietario"];

                            break;

                        case "1D":
                            _BEOVentaDetailDer = new OVentaDetailBE();
                            _BEOVentaDetailDer.CentroCosto = _dt.Rows[0]["CentroCosto"].ToString();
                            _BEOVentaDetailDer.TipoDocumento = _dt.Rows[0]["TipoDocumento"].ToString();
                            _BEOVentaDetailDer.NumeroBV = _dt.Rows[0]["NumeroBV"].ToString();
                            _BEOVentaDetailDer.Usuario = _dt.Rows[0]["Usuario"].ToString();
                            _BEOVentaDetailDer.Proyecto = _dt.Rows[0]["Proyecto"].ToString();
                            _BEOVentaDetailDer.a_nus_per_cod = _dt.Rows[0]["a_nus_per_cod"].ToString();
                            _BEOVentaDetailDer.a_nus_per_nom = _dt.Rows[0]["a_nus_per_nom"].ToString();
                            _BEOVentaDetailDer.ItemCodigo = _dt.Rows[0]["ItemCodigo"].ToString();
                            _BEOVentaDetailDer.Descripcion = _dt.Rows[0]["Descripcion"].ToString();
                            _BEOVentaDetailDer.ItemCodigo = _dt.Rows[0]["ItemCodigo"].ToString();
                            _BEOVentaDetailDer.PrecioUnitario = (decimal)_dt.Rows[0]["PrecioUnitario"];
                            _BEOVentaDetailDer.PrecioUnitarioEmpresa = (decimal)_dt.Rows[0]["PrecioUnitarioEmpresa"];
                            _BEOVentaDetailDer.Linea = _dt.Rows[0]["Linea"].ToString();
                            _BEOVentaDetailDer.TipoDato = _dt.Rows[0]["TipoDato"].ToString();
                            _BEOVentaDetailDer.CodigoSpring = _dt.Rows[0]["CodigoSpring"].ToString();
                            _BEOVentaHeaderDer.OVentaDetailBE = _BEOVentaDetailDer;

                            break;
                    }
                }

                if (_BEOVentaHeaderDer != null)
                {
                    mensaje = IntegrarOrdenVenta(_BEOVentaHeaderDer, "M");
                }

            }

            //IntegradorSAP();

            return mensaje;
        }

        private string IntegrarOrdenVenta(OVentaHeaderBE _BEOVentaHeader, string _tipo)
        {
            string _error = string.Empty;
            oAplicanteDL = new AplicanteDL();
            try
            {
                if (_BEOVentaHeader.montoCuota - _BEOVentaHeader.montoCuotaEmpresa == 0)
                {
                    _BEOVentaHeader.TipoDocumento = "FC";
                }
                container = new Container();
                container.Register<IHubService>(() => new HubServiceClient("HubHTTP"));

                string _moneda = string.Empty;
                string _CostingCode = string.Empty;
                string _CostingCode2 = string.Empty;
                string _UnidadNegocio = string.Empty;
                string _TipoDocumento = string.Empty;
                string _codigocliente = string.Empty;

                if (_BEOVentaHeader.MonedaDocumento == "LO")
                {
                    _moneda = "S/";
                }
                else
                {
                    _moneda = "US$";
                }
                //_CostingCode = "2000100";
                //_UnidadNegocio = "ADM";
                _CostingCode2 = "101";
                _TipoDocumento = "03";
                _codigocliente = _BEOVentaHeader.CodigoSpring.ToString();
                //_BEOVentaHeader.Serie = "101";


                hubService = container.GetInstance<IHubService>();
                CamposAdicionalesLista camposAdicionales = new CamposAdicionalesLista();
                CamposAdicionales tipoComprobanteSUNAT = new CamposAdicionales() { Item1 = "U_SYP_MDTD", Item2 = _TipoDocumento, Item3 = null, Item4 = null, Item5 = null };
                camposAdicionales.Add(tipoComprobanteSUNAT);
                CamposAdicionales serieComprobante = new CamposAdicionales() { Item1 = "U_SYP_MDSD", Item2 = _BEOVentaHeader.Serie, Item3 = null, Item4 = null, Item5 = null };
                camposAdicionales.Add(serieComprobante);
                CamposAdicionales centrocosto = new CamposAdicionales() { Item1 = "U_UP_CENCOS", Item2 = _BEOVentaHeader.CentroCosto, Item3 = null, Item4 = null, Item5 = null };
                camposAdicionales.Add(centrocosto);
                CamposAdicionales cuota = new CamposAdicionales() { Item1 = "U_UP_CUOTA", Item2 = _BEOVentaHeader.Cuota, Item3 = null, Item4 = null, Item5 = null };
                camposAdicionales.Add(cuota);
                CamposAdicionales periodo = new CamposAdicionales() { Item1 = "U_UP_PERIODO", Item2 = _BEOVentaHeader.Periodo, Item3 = null, Item4 = null, Item5 = null };
                camposAdicionales.Add(periodo);
                CamposAdicionales fechainicio = new CamposAdicionales() { Item1 = "U_UP_FECINIT", Item2 = null, Item3 = null, Item4 = null, Item5 = DateTime.Now.Date };
                camposAdicionales.Add(fechainicio);
                CamposAdicionales fechalimite = new CamposAdicionales() { Item1 = "U_UP_FECLIMIT", Item2 = null, Item3 = null, Item4 = null, Item5 = Convert.ToDateTime(_BEOVentaHeader.FecVencimiento) };
                camposAdicionales.Add(fechalimite);
                CamposAdicionales identificador = new CamposAdicionales() { Item1 = "U_UP_IDENTIFICADOR", Item2 = _BEOVentaHeader.Identificador, Item3 = null, Item4 = null, Item5 = null };
                camposAdicionales.Add(identificador);
                CamposAdicionales origen = new CamposAdicionales() { Item1 = "U_UP_ORIGEN", Item2 = "Portal Admisión Pregrado", Item3 = null, Item4 = null, Item5 = null };
                camposAdicionales.Add(origen);
                _error = "1";
                CamposAdicionalesLista camposAdicionalesline = new CamposAdicionalesLista();
                CamposAdicionales alumno = new CamposAdicionales() { Item1 = "U_UP_CARCCODE", Item2 = _BEOVentaHeader.OVentaDetailBE.CodigoSpring.ToString(), Item3 = null, Item4 = null, Item5 = null };
                camposAdicionalesline.Add(alumno);
                _error = "2";


                OrdenVentaLineaLista ordenVentaLineaLista = new OrdenVentaLineaLista();
                OrdenVentaLinea ordenVentaLinea = new OrdenVentaLinea
                {
                    CodigoLinea = _BEOVentaHeader.OVentaDetailBE.ItemCodigo,
                    NombreLinea = _BEOVentaHeader.OVentaDetailBE.Descripcion,
                    Cantidad = 1,
                    PrecioUnitario = (double)_BEOVentaHeader.OVentaDetailBE.PrecioUnitario,
                    Proyecto = _BEOVentaHeader.OVentaDetailBE.Proyecto,
                    CentroCosto = _BEOVentaHeader.CentroCosto,
                    PartidaPresupuestal = _CostingCode2,
                    Local = "001",
                    UnidadNegocio = _BEOVentaHeader.UnidadNegocio,
                    CamposAdicionales = camposAdicionalesline,
                };

                ordenVentaLineaLista.Add(ordenVentaLinea);

                OrdenVenta ordenVenta = new OrdenVenta
                {
                    Origen = "CRM",
                    FechaDocumento = _BEOVentaHeader.FechaDocumento,
                    FechaVencimiento = _BEOVentaHeader.FecVencimiento,
                    CodigoCliente = _codigocliente,
                    Moneda = _moneda,
                    Comentarios = _BEOVentaHeader.Comentarios + _marcado,
                    CamposAdicionales = camposAdicionales,
                    OrdenVentaLineaLista = ordenVentaLineaLista,
                    EmpleadoId = _BEOVentaHeader.Propietario,
                };
                OrdenVentaRespuesta ordenVentaRespuesta = new OrdenVentaRespuesta();
                ordenVentaRespuesta = hubService.CrearOrdenVenta(ordenVenta);

                if (ordenVentaRespuesta.DocEntry > 0)
                {
                    //Insertar en el ApplicationUserDefined
                    oAplicanteDL.registraApplicationUserDefined(_BEOVentaHeader.ApplicationId.ToString(), "", ordenVentaRespuesta.DocEntry.ToString(), false);
                    return "Exito";
                }
                else
                {
                    return "Error";
                }

            }
            catch (Exception ex)
            {
                return _error + ex.Message.ToString();
            }
            finally
            {
                //oCompany.Disconnect();
            }
        }

        /*Ini:[Christian Ramirez - Caso78630]*/
        public bool InsertaHorarioEcl(int aplicanteId, string horaEcl)
        {
            AplicanteDL oAplicanteDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                Respuesta = oAplicanteDL.InsertaHorarioEcl(aplicanteId, horaEcl);
                if (Respuesta == false) return false;
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                throw ex;
            }
            finally
            {
                oAplicanteDL.CerrarConexion();
                oAplicanteDL = null;
            }
            return Respuesta;
        }
        /*Fin:[Christian Ramirez - Caso78630]*/

        /*Ini:[Juan Delgado - Caso81646] 20200928*/
        public bool InsertaHorarioPC(int aplicanteId, string horaPC)
        {
            AplicanteDL oAplicanteDL = null;
            Boolean Respuesta = true;
            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                Respuesta = oAplicanteDL.InsertaHorarioPC(aplicanteId, horaPC);
                if (Respuesta == false) return false;
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                throw ex;
            }
            finally
            {
                oAplicanteDL.CerrarConexion();
                oAplicanteDL = null;
            }
            return Respuesta;
        }

        /*Fin:[Juan Delgado - Caso81646] 20200928*/

        //[Ini:Christian Ramirez - REQ114900]
        public bool InsertaDatosForm21_RequisitoDocumento(AplicanteBE oAplicanteBE)
        {
            AplicanteDL oAplicanteDL = null;
            bool Respuesta = true;

            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                oAplicanteDL.BeginTransaction();

                Respuesta = oAplicanteDL.InsertaDatosForm21_RequisitoDocumento(oAplicanteBE, true);
                oAplicanteDL.CommitTransaction();
            }
            catch (Exception ex)
            {
                oAplicanteDL.RollbackTransaction();
                InsertarLogError(oAplicanteBE, ex.Message);
                throw ex;
            }
            finally
            {
                oAplicanteDL.CerrarConexion();
            }
            return Respuesta;
        }
        //[Fin:Christian Ramirez - REQ114900]


        #endregion "Métodos Transaccionales"

        #region "Métodos Generales"

        private void InsertarLogError(AplicanteBE oAplicanteBE, string mensajeError)
        {
            AplicanteDL oAplicanteDL = null;
            LogAplicanteDL oLogAplicanteDL = null;

            if (oAplicanteBE != null && oAplicanteBE.LogAplicante != null)
            {
                try
                {
                    oAplicanteDL = new AplicanteDL();
                    oAplicanteDL.BeginTransaction();
                    oLogAplicanteDL = new LogAplicanteDL(ref oAplicanteDL.connection, ref oAplicanteDL.miTransaccion);
                    oAplicanteBE.LogAplicante.Error = mensajeError;
                    oLogAplicanteDL.insertarLogAplicante(oAplicanteBE.LogAplicante, true);
                    oAplicanteDL.CommitTransaction();
                }
                catch (Exception ex)
                {
                    oAplicanteDL.RollbackTransaction();
                    throw ex;
                }
                finally
                {
                    oAplicanteDL = null;
                }
            }
        }

        #endregion "Métodos Generales"
    }
}