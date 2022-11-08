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
    public class DocumentoDL : ConexionBD
    {
        #region "Constructores"

        public DocumentoDL()
        { }

        #endregion "Constructores"

        #region "Atributos"

        private SqlCommand cmd;

        #endregion

        #region "Métodos No Transaccionales"

        public DataTable ListaTiposArchivosPermitidos(Int32? idTipoConfiguracion)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlConnection myCon = new SqlConnection();  
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelFormatosValidos", myCon);        
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationFormSettingId", idTipoConfiguracion));

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

        public String GetStatusCargaDocs(Int32? idApplication)
        {
            String codUpd = "";
            DataTable dt = new DataTable();
            SqlConnection myCon = new SqlConnection();  
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelStatusCargaDocs", myCon);       
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", idApplication));

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

        public String EnvioEmailInformacionObligCompletada(String JefeAdmision, Int32 idApplication)
        {
            String codUpd = "";
            DataTable dt = new DataTable();
            SqlConnection myCon = new SqlConnection();  
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_EnvioMailDocCompletos", myCon);      
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_JefeAdmision", JefeAdmision));
                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", idApplication));

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

        public DocumentoBE obtenerDocumentoAdjunto(DocumentoBE oDocumentoBE)
        {
            DocumentoBE documentoBETemp = null;
            SqlDataReader dr = null;
            SqlConnection myCon = new SqlConnection();  
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_SelDocAdjuntoByIdAplic", myCon);     
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_IdAplicacion", oDocumentoBE.IdAplicacion));
                cmd.Parameters.Add(new SqlParameter("@VC_TituloDocumento", oDocumentoBE.TituloDocumento));

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    documentoBETemp = CargarDocumentoDeAplicPorId(dr);
                }
            }
            catch (Exception ex)
            {
                documentoBETemp = null;
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                cmd.Dispose();
                myCon.Close();
            }
            return documentoBETemp;
        }

        //Inicio JC.DelgadoV [Preformalización]
        public DocumentoBE PreformalizacionObtenerDocumentoAdjunto(DocumentoBE oDocumentoBE)
        {
            DocumentoBE documentoBETemp = null;
            SqlDataReader dr = null;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_SelDocAdjuntoByIdAplic", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_IdAplicacion", oDocumentoBE.IdAplicacion));
                cmd.Parameters.Add(new SqlParameter("@VC_TituloDocumento", oDocumentoBE.TituloDocumento));

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    documentoBETemp = PreformalizacionCargarDocumentoDeAplicPorId(dr);
                }
            }
            catch (Exception ex)
            {
                documentoBETemp = null;
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                cmd.Dispose();
                myCon.Close();
            }
            return documentoBETemp;
        }

        private static DocumentoBE PreformalizacionCargarDocumentoDeAplicPorId(IDataReader dataReader)
        {
            DocumentoBE oDocumentoBE = new DocumentoBE();

            Int32 col = dataReader.GetOrdinal("ApplicationAttachmentFormalizacionId");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.IdDocumento = dataReader.GetInt32(col);
            }

            col = dataReader.GetOrdinal("ApplicationId");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.IdAplicacion = dataReader.GetInt32(col);
            }

            col = dataReader.GetOrdinal("FileTitle");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.TituloDocumento = dataReader.GetString(col);
            }

            col = dataReader.GetOrdinal("FileType");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.IdTipoMedio = dataReader.GetInt32(col);
            }

            col = dataReader.GetOrdinal("Extension");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.Extension = dataReader.GetString(col);
            }

            col = dataReader.GetOrdinal("MimeType");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.TipoMedio = dataReader.GetString(col);
            }

            col = dataReader.GetOrdinal("FileContent");

            //dataReader.GetBytes(col, 0, fileContent, 0, bufferSize);
            long fileContentSize = dataReader.GetBytes(col, 0, null, 0, 0);  //get the length of file content
            byte[] fileContent = new byte[fileContentSize];
            int bufferSize = 100;
            long bytesRead = 0;
            int curPos = 0;

            while (bytesRead < fileContentSize)
            {
                bytesRead += dataReader.GetBytes(col, curPos, fileContent, curPos, bufferSize);
                curPos += bufferSize;
            }

            oDocumentoBE.ContenidoDocumento = fileContent;

            return oDocumentoBE;
        }

        //Fin JC.DelgadoV [Preformalización]

        private static DocumentoBE CargarDocumentoDeAplicPorId(IDataReader dataReader)
        {
            DocumentoBE oDocumentoBE = new DocumentoBE();

            Int32 col = dataReader.GetOrdinal("ApplicationAttachmentId");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.IdDocumento = dataReader.GetInt32(col);
            }

            col = dataReader.GetOrdinal("ApplicationId");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.IdAplicacion = dataReader.GetInt32(col);
            }

            col = dataReader.GetOrdinal("FileTitle");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.TituloDocumento = dataReader.GetString(col);
            }

            col = dataReader.GetOrdinal("FileType");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.IdTipoMedio = dataReader.GetInt32(col);
            }

            col = dataReader.GetOrdinal("Extension");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.Extension = dataReader.GetString(col);
            }

            col = dataReader.GetOrdinal("MimeType");
            if (!dataReader.IsDBNull(col))
            {
                oDocumentoBE.TipoMedio = dataReader.GetString(col);
            }

            col = dataReader.GetOrdinal("FileContent");

            //dataReader.GetBytes(col, 0, fileContent, 0, bufferSize);
            long fileContentSize = dataReader.GetBytes(col, 0, null, 0, 0);  //get the length of file content
            byte[] fileContent = new byte[fileContentSize];
            int bufferSize = 100;
            long bytesRead = 0;
            int curPos = 0;

            while (bytesRead < fileContentSize)
            {
                bytesRead += dataReader.GetBytes(col, curPos, fileContent, curPos, bufferSize);
                curPos += bufferSize;
            }

            oDocumentoBE.ContenidoDocumento = fileContent;

            return oDocumentoBE;
        }

        /*Ini[Christian Ramirez - caso76999]*/
        public string ObtenerDocumentosRequeridosNota()
        {
            string rpta = "";

            using (SqlConnection myCon = this.getConexion())
            {
                if (myCon.State == ConnectionState.Closed) myCon.Open();

                cmd = new SqlCommand("UPAdmPre_DocumentosRequeridosNota", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                rpta = cmd.ExecuteScalar().ToString();
            }

            return rpta;
        }
        /*Fin[Christian Ramirez - caso76999]*/
        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        public Boolean insertarDocumentoAplicante(DocumentoBE oDocumentoBE)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();  
            try
            {
                myCon = this.getConexion(); 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_InsApplicationAttachment", myCon);    
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@ApplicationId", oDocumentoBE.IdAplicacion));
                cmd.Parameters.Add(new SqlParameter("@FileTitle", oDocumentoBE.TituloDocumento));
                cmd.Parameters.Add(new SqlParameter("@FileType", oDocumentoBE.IdTipoMedio));
                cmd.Parameters.Add(new SqlParameter("@FileExtension", oDocumentoBE.Extension));
                cmd.Parameters.Add(new SqlParameter("@FileContent", oDocumentoBE.ContenidoDocumento));
                cmd.Parameters.Add(new SqlParameter("@RevisionUser", oDocumentoBE.UsuarioActualiza));
                cmd.Parameters.Add(new SqlParameter("@Estado", oDocumentoBE.Estado));
                cmd.Parameters.Add("@ApplicationAttachmentId", SqlDbType.Int).Direction = ParameterDirection.Output;
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

        //Inicio JC.DelgadoV [Preformalizacion]
        public Boolean PreformalizacionInsertarDocumentoAplicante(DocumentoBE oDocumentoBE)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_InsApplicationAttachmentFormalizacion", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@ApplicationId", oDocumentoBE.IdAplicacion));
                cmd.Parameters.Add(new SqlParameter("@FileTitle", oDocumentoBE.TituloDocumento));
                cmd.Parameters.Add(new SqlParameter("@FileType", oDocumentoBE.IdTipoMedio));
                cmd.Parameters.Add(new SqlParameter("@FileExtension", oDocumentoBE.Extension));
                cmd.Parameters.Add(new SqlParameter("@FileContent", oDocumentoBE.ContenidoDocumento));
                cmd.Parameters.Add(new SqlParameter("@RevisionUser", oDocumentoBE.UsuarioActualiza));
                cmd.Parameters.Add(new SqlParameter("@Estado", oDocumentoBE.Estado));
                cmd.Parameters.Add("@ApplicationAttachmentFormalizacionId", SqlDbType.Int).Direction = ParameterDirection.Output;
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

        public Boolean PreformalizacionModificarDocumentoAplicante(DocumentoBE oDocumentoBE)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();
            myCon = this.getConexion();
            try
            {
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ModDocumentoAdjunto", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@ApplicationAttachmentFormalizacionId", oDocumentoBE.IdDocumento));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", oDocumentoBE.IdAplicacion));
                cmd.Parameters.Add(new SqlParameter("@FileTitle", oDocumentoBE.TituloDocumento));
                cmd.Parameters.Add(new SqlParameter("@FileType", oDocumentoBE.IdTipoMedio));
                cmd.Parameters.Add(new SqlParameter("@FileExtension", oDocumentoBE.Extension));
                cmd.Parameters.Add(new SqlParameter("@FileContent", oDocumentoBE.ContenidoDocumento));
                cmd.Parameters.Add(new SqlParameter("@RevisionUser", oDocumentoBE.UsuarioActualiza));
                cmd.Parameters.Add(new SqlParameter("@Estado", oDocumentoBE.Estado));
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

        //Fin JC.DelgadoV [Preformalizacion]

        public Boolean modificarDocumentoAplicante(DocumentoBE oDocumentoBE)
        {
            Boolean Respuesta = true;
            SqlConnection myCon = new SqlConnection();  
            myCon = this.getConexion();
            try
            { 
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_ModDocumentoAdjunto", myCon);   
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@ApplicationAttachmentId", oDocumentoBE.IdDocumento));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", oDocumentoBE.IdAplicacion));
                cmd.Parameters.Add(new SqlParameter("@FileTitle", oDocumentoBE.TituloDocumento));
                cmd.Parameters.Add(new SqlParameter("@FileType", oDocumentoBE.IdTipoMedio));
                cmd.Parameters.Add(new SqlParameter("@FileExtension", oDocumentoBE.Extension));
                cmd.Parameters.Add(new SqlParameter("@FileContent", oDocumentoBE.ContenidoDocumento));
                cmd.Parameters.Add(new SqlParameter("@RevisionUser", oDocumentoBE.UsuarioActualiza));
                cmd.Parameters.Add(new SqlParameter("@Estado", oDocumentoBE.Estado));
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

        public int UpdDocumentoAdjunto(int ApplicationAttachmentId,int ApplicationId, string DocumentId)
        {
            int Resp = 0;
            SqlConnection myCon = new SqlConnection(); 

            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_DelDocAdjunto", myCon);  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@ApplicationAttachmentId", ApplicationAttachmentId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@DocumentId", DocumentId));

                Resp = Convert.ToInt32(cmd.ExecuteScalar());
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
            return Resp;
        }

        //Inicio JC.DelgadoV [Preformalizacion]
        public int PreformalizacionUpdDocumentoAdjunto(int ApplicationAttachmentId, int ApplicationId, string DocumentId)
        {
            int Resp = 0;
            SqlConnection myCon = new SqlConnection();

            try
            {
                myCon = this.getConexion();
                myCon.Open();

                cmd = new SqlCommand("UPAdmPre_Preformalizacion_DelDocAdjunto", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@ApplicationAttachmentFormalizacionId", ApplicationAttachmentId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@DocumentId", DocumentId));

                Resp = Convert.ToInt32(cmd.ExecuteScalar());
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
            return Resp;
        }
        //Fin JC.DelgadoV [Preformalizacion]

        #endregion "Métodos Transaccionales"
    }
}
