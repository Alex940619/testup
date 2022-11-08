using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.BE;
using UPAdmPre.DL;

namespace UPAdmPre.BL
{
    public class EducacionBL
    {
        public EducacionBL()
        { }

        #region "Métodos No Transaccionales"

        public List<EducacionBE> ListarColegios(String prefijo, String txtDegreeId)
        {
            EducacionDL oEducacionDL = null;
            try
            {
                oEducacionDL = new EducacionDL();
                return oEducacionDL.ListarColegios(prefijo, txtDegreeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EducacionBE> ListarUniversidades(String prefijo, String txtDegreeId)
        {
            EducacionDL oEducacionDL = null;
            try
            {
                oEducacionDL = new EducacionDL();
                return oEducacionDL.ListarUniversidades(prefijo, txtDegreeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
        public string ObtenerTipoEntrevista(Int32? ApplicationEducationId, Int32? AplicanteId)
        {
            EducacionDL oEducacionDL = null;
            string rpta = string.Empty;

            try
            {
                oEducacionDL = new EducacionDL();
                rpta = oEducacionDL.ObtenerTipoEntrevista(ApplicationEducationId.Value, AplicanteId.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rpta;
        }
        /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/
        #endregion "Métodos No Transaccionales"


        /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
        #region "Métodos Transaccionales"
        public Boolean ModTipoEntrevista(AplicanteBE oAplicanteBE, Int32? ApplicationEducationId, string ipoEntrevista)
        {
            AplicanteDL oAplicanteDL = null;
            EducacionDL oEducacionDL = new EducacionDL();
            Boolean Respuesta = true;

            try
            {
                oAplicanteDL = new AplicanteDL();
                oAplicanteDL.Conexion();
                oAplicanteDL.AbrirConexion();
                Respuesta = oEducacionDL.ModTipoEntrevista(oAplicanteBE, ApplicationEducationId.Value, ipoEntrevista);
            }
            catch (Exception ex)
            {
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
        /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/


        /*Ini:Christian Ramirez -GIIT[caso 64015] - 20190619*/
        public string ObtenerTipoColegio(string codModular, string anioAcademico)
        {
            EducacionDL oEducacionDL = new EducacionDL();
            string rpta = "";

            try
            {
                rpta = oEducacionDL.ObtenerTipoColegio(codModular, anioAcademico);/*Se agrega:Christian Ramirez - REQ91569*/
            }
            catch (Exception)
            {

                throw;
            }

            return rpta;

        }        /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/

        public string ObtenerTipoColegioLimaProv(string codModular, string codModalidad)
        {
            EducacionDL oEducacionDL = new EducacionDL();
            string rpta = "";

            try
            {
                rpta = oEducacionDL.ObtenerTipoColegioLimaProv(codModular, codModalidad);
            }
            catch (Exception)
            {

                throw;
            }

            return rpta;

        }

    }
}
