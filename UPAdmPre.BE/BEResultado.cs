using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class BEResultado
    {
        #region ATRIBUTOS
        private string _status;
        private string _message;
        private string _resultado;
        #endregion

        #region CONSTRUCTOR
        public BEResultado()
        {
            _status = string.Empty;
            _resultado = string.Empty;
            _message = string.Empty;
        }

        public BEResultado(string Status,
                    string Message,
                    string Resultado
            )
        {
            _status = Status;
            _message = Message;
            _resultado = Resultado;
        }
        #endregion

        #region PROPIEDADES
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string Resultado
        {
            get { return _resultado; }
            set { _resultado = value; }
        }

        #endregion

    }
}
