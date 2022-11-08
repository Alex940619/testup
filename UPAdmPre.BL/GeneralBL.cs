using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UPAdmPre.BE;
using UPAdmPre.DL;

namespace UPAdmPre.BL
{
    public class GeneralBL
    {
        public GeneralBL()
        { }

        #region "Métodos No Transaccionales"
        /*Usuario Creacion: Jaqueline DB
         Fecha Creaciom: 07/04/2020 */
        public DataTable ListarPeridoRadioButton()
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ListarPeridoRadioButton();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable ObtenerTipoCodigoPC(String Tipo, String Name, Int32? TipoPost, Int32? ModPostulacion = null)/*Se modifica: Christian Ramirez - GIIT[caso 56427] - 20190108*/
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ObtenerTipoCodigoPC(Tipo, Name, TipoPost, ModPostulacion); /*Se modifica: Christian Ramirez - GIIT[caso 56427] - 20190108*/
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        //Inicio JC.DelgadoV [Preformalización]
        public DataTable PreformalizacionObtenerExamenesXModalidad(int AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.PreformalizacionObtenerExamenesXModalidad(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable PreformalizacionObtenerCursosConvalidar(int AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.PreformalizacionObtenerCursosConvalidar(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable PreformalizacionObtenerDatosFinales(int AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.PreformalizacionObtenerDatosFinales(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable PreformalizacionObtenerEstado(int AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.PreformalizacionObtenerEstado(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable PreformalizacionObtenerAutorizacionDatos(int AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.PreformalizacionObtenerAutorizacionDatos(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }
        //Fin JC.DelgadoV [Preformalización]

        public DataTable ObtenerDescripcionModalidad(Int32? IdModalidad, Int32? IdCursos, Int32? ApplicationFormSettingId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ObtenerDescripcionModalidad(IdModalidad, IdCursos, ApplicationFormSettingId, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable ObtenerDescripcionModalidadMensaje(Int32? IdModalidad, Int32? IdCursos, Int32? ApplicationFormSettingId, string anioperiodo)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ObtenerDescripcionModalidad(IdModalidad, IdCursos, ApplicationFormSettingId, anioperiodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable obtenerDepartamentoPorId(Int32? idPais)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.obtenerDepartamentoPorId(idPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable obtenerProvinciaPorId(Int32? idDpto)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.obtenerProvinciaPorId(idDpto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable obtenerDistritoPorId(Int32? idProvincia)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.obtenerDistritoPorId(idProvincia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable ObtenerDocsPorModalidadPostulacion(Int32? AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ObtenerDocsPorModalidadPostulacion(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        //Inicio JC.DelgadoV [Preformalización]
        public DataTable PreformalizacionObtenerDocsPorModalidadPostulacion(Int32? AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.PreformalizacionObtenerDocsPorModalidadPostulacion(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }
        //Fin JC.DelgadoV [Preformalización]

        public DataTable ObtenerDocsAdicionales(Int32? AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ObtenerDocsAdicionales(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable obtenerMensajeporId(Int32 TextId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.obtenerMensajeporId(TextId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable obtenerSiguientePagina(Int32? ModalidadId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.obtenerSiguientePagina(ModalidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable GetPasosInscripcion(int? modalidadId,int? applicationId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.GetPasosInscripcion(modalidadId, applicationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
        public DataTable ObtenerHorariosDeEntrevista(Int32? AplicanteId, string tipEvaluacion = "")
        /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ObtenerHorariosDeEntrevista(AplicanteId, tipEvaluacion); /*Se agrega:Christian Ramirez - GIIT[caso60747] - 20190521*/
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable ObtenerHorariosDeFormalizacion(Int32? AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ObtenerHorariosDeFormalizacion(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }
        public DataTable ObtenerDescripcionPeriodo(Int32? IdPeriodo, Int32? ApplicationFormSettingId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ObtenerDescripcionPeriodo(IdPeriodo, ApplicationFormSettingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable ObtenerDescripcionBeca(Int32? IdPeriodo, Int32? ApplicationFormSettingId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ObtenerDescripcionBeca(IdPeriodo, ApplicationFormSettingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }
        public DataTable ObtenerDescripcionAviso(Int32? IdPeriodo, Int32? ApplicationFormSettingId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ObtenerDescripcionAviso(IdPeriodo, ApplicationFormSettingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }
               
        public DataTable ObtenerDescTipoEvaluacion(Int32? IdPeriodo, Int32? ApplicationFormSettingId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.ObtenerDescTipoEvaluacion(IdPeriodo, ApplicationFormSettingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        //INI: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRC)
        public DataSet PreformalizacionObtenerEncuesta(int AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.PreformalizacionObtenerEncuesta(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable PreformalizacionComprobarEncuesta(int AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.PreformalizacionComprobarEncuesta(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable PreformalizacionLlenarEncuesta(int AplicanteId)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.PreformalizacionLlenarEncuesta(AplicanteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        public DataTable PreformalizacionGuardarEncuesta(AplicanteBE oAplicanteBE)
        {
            GeneralDL oGeneralDL = null;
            try
            {
                oGeneralDL = new GeneralDL();
                return oGeneralDL.PreformalizacionGuardarEncuesta(oAplicanteBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralDL = null;
            }
        }

        //FIN: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRC)

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        #endregion "Métodos Transaccionales"
    }
}
