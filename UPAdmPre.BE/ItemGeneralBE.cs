using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class ItemGeneralBE
    {
        public int codigo { get; set; }
        public string descripcion { get; set; }
        public string postulante { get; set; }
        public string fecha { get; set; }
        public string jurado { get; set; }
        public string Hora { get; set; }
        public string PeopleId { get; set; }
    }

    [Serializable]
    public class ItemGeneralListBE : List<ItemGeneralBE>
    {
    }
}
