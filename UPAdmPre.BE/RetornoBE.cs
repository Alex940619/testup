using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class RetornoBE
    {
        public RetornoBE()
        { }

        public Boolean status { get; set; } //estado de la transacción

        public String message { get; set; } //mensaje de respuesta
        
        public String aditional { get; set; } //datos adicionales
    }
}
