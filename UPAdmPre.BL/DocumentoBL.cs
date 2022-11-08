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
    public class DocumentoBL
    {
        public DocumentoBL()
        { }

        #region "Métodos No Transaccionales"

        public DataTable ListaTiposArchivosPermitidos(Int32? idTipoConfiguracion)
        {
            DocumentoDL oDocumentoDL = null;
            try
            {
                oDocumentoDL = new DocumentoDL();
                return oDocumentoDL.ListaTiposArchivosPermitidos(idTipoConfiguracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDocumentoDL = null;
            }
        }

        public String GetStatusCargaDocs(Int32? filtro)
        {
            DocumentoDL oDocumentoDL = null;
            try
            {
                oDocumentoDL = new DocumentoDL();
                return oDocumentoDL.GetStatusCargaDocs(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDocumentoDL = null;
            }
        }

        public DocumentoBE obtenerDocumentoAdjunto(DocumentoBE oDocumentoBE)
        {
            DocumentoDL oDocumentoDL = null;
            try
            {
                oDocumentoDL = new DocumentoDL();
                return oDocumentoDL.obtenerDocumentoAdjunto(oDocumentoBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDocumentoDL = null;
            }
        }

        //Inicio JC.DelgadoV [Preformalización]
        public DocumentoBE PreformalizacionObtenerDocumentoAdjunto(DocumentoBE oDocumentoBE)
        {
            DocumentoDL oDocumentoDL = null;
            try
            {
                oDocumentoDL = new DocumentoDL();
                return oDocumentoDL.PreformalizacionObtenerDocumentoAdjunto(oDocumentoBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDocumentoDL = null;
            }
        }
        //Fin JC.DelgadoV [Preformalización]

        /*Ini[Christian Ramirez - caso76999]*/
        public string ObtenerDocumentosRequeridosNota()
        {
            string rpta = "";
            DocumentoDL oDocumentoDL = null;

            try {
                oDocumentoDL = new DocumentoDL();
                rpta = oDocumentoDL.ObtenerDocumentosRequeridosNota(); 
            }
            catch (Exception ex) { throw ex; }
            finally { oDocumentoDL = null; }

            return rpta;
        }
        /*Fin[Christian Ramirez - caso76999]*/

        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        public Boolean insertarDocumentoAplicante(DocumentoBE oDocumentoBE)
        {
            DocumentoDL oDocumentoDL = null;
            try
            {
                oDocumentoDL = new DocumentoDL();
                return oDocumentoDL.insertarDocumentoAplicante(oDocumentoBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDocumentoDL = null;
            }
        }

        //Inicio JC.DelgadoV [Preformalización]
        public Boolean PreformalizacionInsertarDocumentoAplicante(DocumentoBE oDocumentoBE)
        {
            DocumentoDL oDocumentoDL = null;
            try
            {
                oDocumentoDL = new DocumentoDL();
                return oDocumentoDL.PreformalizacionInsertarDocumentoAplicante(oDocumentoBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDocumentoDL = null;
            }
        }

        public Boolean PreformalizacionModificarDocumentoAplicante(DocumentoBE oDocumentoBE)
        {
            DocumentoDL oDocumentoDL = null;
            try
            {
                oDocumentoDL = new DocumentoDL();
                return oDocumentoDL.PreformalizacionModificarDocumentoAplicante(oDocumentoBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDocumentoDL = null;
            }
        }

        //Fin JC.DelgadoV [Preformalización]

        public Boolean modificarDocumentoAplicante(DocumentoBE oDocumentoBE)
        {
            DocumentoDL oDocumentoDL = null;
            try
            {
                oDocumentoDL = new DocumentoDL();
                return oDocumentoDL.modificarDocumentoAplicante(oDocumentoBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDocumentoDL = null;
            }
        }

        public int UpdDocumentoAdjunto(int ApplicationAttachmentId, int ApplicationId, string DocumentId)
        {
            DocumentoDL oDocumentoDL = null;
            try
            {
                oDocumentoDL = new DocumentoDL();
                return oDocumentoDL.UpdDocumentoAdjunto(ApplicationAttachmentId, ApplicationId, DocumentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDocumentoDL = null;
            }
        }

        //Inicio JC.DelgadoV [Preformalizacion]
        public int PreformalizacionUpdDocumentoAdjunto(int ApplicationAttachmentId, int ApplicationId, string DocumentId)
        {
            DocumentoDL oDocumentoDL = null;
            try
            {
                oDocumentoDL = new DocumentoDL();
                return oDocumentoDL.PreformalizacionUpdDocumentoAdjunto(ApplicationAttachmentId, ApplicationId, DocumentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDocumentoDL = null;
            }
        }
        //Fin JC.DelgadoV [Preformalizacion]

        #endregion "Métodos Transaccionales"
    }
}
