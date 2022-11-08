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
    public class DireccionAplicanteDL : ConexionBD
    {
        #region "Atributos"

        private SqlCommand cmd;

        #endregion "Atributos"

        #region "Constructores"

        public DireccionAplicanteDL()
            : base()
        { }

        public DireccionAplicanteDL(ref SqlConnection miConexion, ref SqlTransaction miTransaccion)
            : base()
        {
            this.connection = miConexion;
            this.miTransaccion = miTransaccion;
        }

        #endregion "Constructores"

        #region "Métodos No Transaccionales"
        #endregion "Métodos No Transaccionales"

        #region "Métodos Transaccionales"

        public Boolean insertarDireccionAplicante(AplicanteBE oAplicanteBE, DireccionAplicanteBE oDireccionAplicanteBE, Boolean transaccionIniciada)
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
                cmd = new SqlCommand("UPAdmPre_InsDireccion", myCon);    
                cmd.Transaction = this.miTransaccion; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                cmd.Parameters.Add(new SqlParameter("@VC_AddressTypeId", oDireccionAplicanteBE.IdTipoDireccion.ToString("D")));
                cmd.Parameters.Add(new SqlParameter("@VC_Line1", oDireccionAplicanteBE.Direccion1));

                if (oDireccionAplicanteBE.Direccion2 != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Line2", oDireccionAplicanteBE.Direccion2));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Line2", DBNull.Value));
                }

                if (oDireccionAplicanteBE.Direccion3 != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Line3", oDireccionAplicanteBE.Direccion3));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Line3", DBNull.Value));
                }
                
                if(oDireccionAplicanteBE.Distrito !=null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_City", oDireccionAplicanteBE.Distrito));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_City",DBNull.Value ));
                }
                

                if (oDireccionAplicanteBE.Departamento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StateProvinceId", oDireccionAplicanteBE.Departamento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StateProvinceId", DBNull.Value));
                }

                if(oDireccionAplicanteBE.CodigoPostal!=null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PostalCode", oDireccionAplicanteBE.CodigoPostal));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PostalCode", DBNull.Value ));
                }
                
                if (oDireccionAplicanteBE.Provincia.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_County", oDireccionAplicanteBE.Provincia));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_County", DBNull.Value));
                }

                if (oDireccionAplicanteBE.Pais.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Country", oDireccionAplicanteBE.Pais));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Country", DBNull.Value));
                }

                //if(oDireccionAplicanteBE.Revision_Opid !=null)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID", oDireccionAplicanteBE.Revision_Opid));
                //}
                //else
                //{
                //    cmd.Parameters.Add(new SqlParameter("@VC_Revision_OPID",DBNull.Value ));
                //}
                
                if (!string.IsNullOrEmpty(oDireccionAplicanteBE.Number))
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Number", oDireccionAplicanteBE.Number));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Number", string.Empty));
                }
                if (!string.IsNullOrEmpty(oDireccionAplicanteBE.Interior))
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Interior", oDireccionAplicanteBE.Interior));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Interior", string.Empty));
                }
                if (!string.IsNullOrEmpty(oDireccionAplicanteBE.Reference))
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Reference", oDireccionAplicanteBE.Reference));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Reference", string.Empty));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_TipoVia", oDireccionAplicanteBE.TipoVia));                
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

        //Inicio JC.DelgadoV [Preformalización]
        public Boolean actualizarDireccionAplicante(AplicanteBE oAplicanteBE, DireccionAplicanteBE oDireccionAplicanteBE, Boolean transaccionIniciada)
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
                cmd = new SqlCommand("UPAdmPre_Preformalizacion_ActualizarDireccion", myCon);
                cmd.Transaction = this.miTransaccion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("@VC_ApplicationId", oAplicanteBE.IdAplicante));
                cmd.Parameters.Add(new SqlParameter("@VC_AddressTypeId", oDireccionAplicanteBE.IdTipoDireccion.ToString("D")));
                cmd.Parameters.Add(new SqlParameter("@VC_Line1", oDireccionAplicanteBE.Direccion1));

                if (oDireccionAplicanteBE.Direccion2 != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Line2", oDireccionAplicanteBE.Direccion2));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Line2", DBNull.Value));
                }

                if (oDireccionAplicanteBE.Direccion3 != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Line3", oDireccionAplicanteBE.Direccion3));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Line3", DBNull.Value));
                }

                if (oDireccionAplicanteBE.Distrito != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_City", oDireccionAplicanteBE.Distrito));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_City", DBNull.Value));
                }


                if (oDireccionAplicanteBE.Departamento.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StateProvinceId", oDireccionAplicanteBE.Departamento));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_StateProvinceId", DBNull.Value));
                }

                if (oDireccionAplicanteBE.CodigoPostal != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PostalCode", oDireccionAplicanteBE.CodigoPostal));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_PostalCode", DBNull.Value));
                }

                if (oDireccionAplicanteBE.Provincia.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_County", oDireccionAplicanteBE.Provincia));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_County", DBNull.Value));
                }

                if (oDireccionAplicanteBE.Pais.HasValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Country", oDireccionAplicanteBE.Pais));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Country", DBNull.Value));
                }

                if (oAplicanteBE.RedId != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", oAplicanteBE.RedId));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_RedID", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(oDireccionAplicanteBE.Number))
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Number", oDireccionAplicanteBE.Number));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Number", string.Empty));
                }
                if (!string.IsNullOrEmpty(oDireccionAplicanteBE.Interior))
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Interior", oDireccionAplicanteBE.Interior));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Interior", string.Empty));
                }
                if (!string.IsNullOrEmpty(oDireccionAplicanteBE.Reference))
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Reference", oDireccionAplicanteBE.Reference));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@VC_Reference", string.Empty));
                }
                cmd.Parameters.Add(new SqlParameter("@VC_TipoVia", oDireccionAplicanteBE.TipoVia));
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

        #endregion "Métodos Transaccionales"
    }
}
