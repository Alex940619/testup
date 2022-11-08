using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml;

namespace UPAdmPre.SL
{
    public class ConexionBD
    {
        #region ATRIBUTOS

        private string cadenaConexion;
        public SqlConnection connection;
        public SqlTransaction miTransaccion;

        #endregion

        #region CONSTRUCTOR

        public ConexionBD()
        {
            this.cadenaConexion = string.Empty;
            this.connection = null;
            this.miTransaccion = null;
        }

        #endregion

        #region PROPIEDADES

        #endregion

        #region METODOS

        /// <summary>
        /// Función             : Conexion
        /// Descripción         : Contiene la cadena de conexion a la Base de Datos, utiliza los datos por default del XML
        /// Fecha Creacion      : 12/04/2012
        /// Creador             : Eduardo Loo
        /// Ultimo en modificar : Eduardo Loo
        /// Ultima modificacion : 12/04/2012
        /// </summary>
        public void Conexion()
        {
            XmlDocument docxml = new XmlDocument();
            string dataSource;
            string initialCatalog;
            string userID;
            string password;

            try
            {
                docxml.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\DatosConexion.xml");
                dataSource = docxml.ChildNodes[1].ChildNodes[0].InnerText;
                initialCatalog = docxml.ChildNodes[1].ChildNodes[1].InnerText;
                userID = docxml.ChildNodes[1].ChildNodes[2].InnerText;
                password = docxml.ChildNodes[1].ChildNodes[3].InnerText;

                //cadenaConexion =  "Data Source=" + dataSource + ";Persist Security Info=True;User ID=" + userID + ";Password=" + password + ";Unicode=True";
                cadenaConexion = "Data Source=" + dataSource + ";Initial Catalog=" + initialCatalog + ";User ID=" + userID + ";Password=" + password + ";Pooling=No;Min Pool Size=0;Connection Timeout=30;";
                //cadenaConexion = "Data Source=" + dataSource + ";Initial Catalog=" + initialCatalog + ";User ID=" + userID + ";Password=" + password + ";Pooling=Yes;Max Pool Size=200;Min Pool Size=0;Connection Timeout=30;";
                connection = new SqlConnection();
                connection.ConnectionString = cadenaConexion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlConnection getConexion()
        {
            XmlDocument docxml = new XmlDocument();
            string dataSource;
            string initialCatalog;
            string userID;
            string password;
            SqlConnection cn;
            try
            {
                docxml.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\DatosConexion.xml");
                dataSource = docxml.ChildNodes[1].ChildNodes[0].InnerText;
                initialCatalog = docxml.ChildNodes[1].ChildNodes[1].InnerText;
                userID = docxml.ChildNodes[1].ChildNodes[2].InnerText;
                password = docxml.ChildNodes[1].ChildNodes[3].InnerText;

                //cadenaConexion =  "Data Source=" + dataSource + ";Persist Security Info=True;User ID=" + userID + ";Password=" + password + ";Unicode=True";
                cadenaConexion = "Data Source=" + dataSource + ";Initial Catalog=" + initialCatalog + ";User ID=" + userID + ";Password=" + password + ";Pooling=No;Min Pool Size=0;Connection Timeout=30;";
                //cadenaConexion = "Data Source=" + dataSource + ";Initial Catalog=" + initialCatalog + ";User ID=" + userID + ";Password=" + password + ";Pooling=Yes;Max Pool Size=200;Min Pool Size=0;Connection Timeout=30;";
                connection = new SqlConnection();
                //connection.ConnectionString = cadenaConexion;
                cn = new SqlConnection(cadenaConexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cn;
        }

        public void ConexionCapacitacion()
        {
            XmlDocument docxml = new XmlDocument();
            string dataSource;
            string initialCatalog;
            string userID;
            string password;

            try
            {
                //docxml.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\DatosConexionCapacitacion.xml");
                docxml.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\DatosConexion.xml");
                dataSource = docxml.ChildNodes[1].ChildNodes[0].InnerText;
                initialCatalog = docxml.ChildNodes[1].ChildNodes[1].InnerText;
                userID = docxml.ChildNodes[1].ChildNodes[2].InnerText;
                password = docxml.ChildNodes[1].ChildNodes[3].InnerText;

                //cadenaConexion =  "Data Source=" + dataSource + ";Persist Security Info=True;User ID=" + userID + ";Password=" + password + ";Unicode=True";
                cadenaConexion = "Data Source=" + dataSource + ";Initial Catalog=" + initialCatalog + ";User ID=" + userID + ";Password=" + password + ";Pooling=Yes;Max Pool Size=100;Min Pool Size=0;Connection Timeout=60;";
                connection = new SqlConnection();
                connection.ConnectionString = cadenaConexion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Función             : AbrirConexion
        /// Descripción         : Abre la conexion a la Base de Datos
        /// Fecha Creacion      : 12/04/2012
        /// Creador             : Eduardo Loo
        /// Ultimo en modificar : Eduardo Loo
        /// Ultima modificacion : 12/04/2012
        /// </summary>
        public bool AbrirConexion()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                connection.Dispose();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Función             : AbrirConexion
        /// Descripción         : Cierra la conexion a la Base de Datos
        /// Fecha Creacion      : 12/04/2012
        /// Creador             : Eduardo Loo
        /// Ultimo en modificar : Eduardo Loo
        /// Ultima modificacion : 12/04/2012
        /// </summary>
        public bool CerrarConexion()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                    connection = null;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        //public SqlTransaction MiTransaccion
        //{
        //    get { return miTransaccion; }
        //    set { miTransaccion = value; }
        //}

        /// <summary>
        /// Función             : Iniciar Transacción
        /// Descripción         : Cierra la conexion a la Base de Datos
        /// Fecha Creacion      : 12/04/2012
        /// Creador             : Eduardo Loo
        /// Ultimo en modificar : Eduardo Loo
        /// Ultima modificacion : 12/04/2012
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    this.miTransaccion = connection.BeginTransaction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Función             : Guardar Cambios
        /// Descripción         : Cierra la conexion a la Base de Datos
        /// Fecha Creacion      : 12/04/2012
        /// Creador             : Eduardo Loo
        /// Ultimo en modificar : Eduardo Loo
        /// Ultima modificacion : 12/04/2012
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open && miTransaccion != null)
                {
                    miTransaccion.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Función             : Deschacer Cambios
        /// Descripción         : Cierra la conexion a la Base de Datos
        /// Fecha Creacion      : 12/04/2012
        /// Creador             : Eduardo Loo
        /// Ultimo en modificar : Eduardo Loo
        /// Ultima modificacion : 12/04/2012
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open && miTransaccion != null)
                {
                    miTransaccion.Rollback();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
