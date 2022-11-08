using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class PostulanteBE
    {
        public PostulanteBE()
        { }

        #region "Atributos"

        private String _codigo = null;
        private String _nombre = null;
        private String _decision = null;
        private String _username = null;
        private String _modalidad = null;
        private String _proceso = null;
        private String _escala = null;

        #endregion "Atributos"

        #region "Propiedades"

        public String Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public String Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public String Decision
        {
            get { return _decision; }
            set { _decision = value; }
        }
        public String UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        public String Modalidad
        {
            get { return _modalidad; }
            set { _modalidad = value; }
        }

        public String Proceso
        {
            get { return _proceso; }
            set { _proceso = value; }
        }

        public String Escala
        {
            get { return _escala; }
            set { _escala = value; }
        }

        #endregion "Propiedades"
    }
}
