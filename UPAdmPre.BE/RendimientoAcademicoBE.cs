using System.Collections.Generic;

namespace UPAdmPre.BE
{
    public class RendimientoAcademicoBE
    {
        public int ApplicationId { get; set; }
        public int ApplicationEducationId { get; set; }
        public int CodTipoCalificacion { get; set; } 
        public int SessionPeriodId { get; set; } 
        public int DegreeId { get; set; }
        public string CodigoModular { get; set; }
        public string CodigoModular2 { get; set; }
        public string CodigoModular3 { get; set; }
        //public int TotalCompentencias { get; set; } 
        //public int TotalCompetencia_AD { get; set; } 
        //public int TotalCompetencia_A { get; set; } 
        //public int TotalCompetencia_B { get; set; } 
        //public int TotalCompetencia_C { get; set; }

        //JC.DelgadoV
        public int OrdenMeritoTercero { get; set; }
        public int NroAlumnosPromocionTercero { get; set; }


        public int OrdenMeritoCuarto { get; set; }
        public int NroAlumnosPromocionCuarto { get; set; }

        public int OrdenMeritoQuinto { get; set; }
        public int NroAlumnosPromocionQuinto { get; set; }

        public int TotalCompentencias_Cuarto { get; set; }
        public int TotalCompetencia_AD_Cuarto { get; set; }
        public int TotalCompetencia_A_Cuarto { get; set; }
        public int TotalCompetencia_B_Cuarto { get; set; }
        public int TotalCompetencia_C_Cuarto { get; set; }

        public int TotalCompentencias_Quinto { get; set; }
        public int TotalCompetencia_AD_Quinto { get; set; }
        public int TotalCompetencia_A_Quinto { get; set; }
        public int TotalCompetencia_B_Quinto { get; set; }
        public int TotalCompetencia_C_Quinto { get; set; }

        //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
        public int TotalCompentencias_Tercero { get; set; }
        public int TotalCompetencia_AD_Tercero { get; set; }
        public int TotalCompetencia_A_Tercero { get; set; }
        public int TotalCompetencia_B_Tercero { get; set; }
        public int TotalCompetencia_C_Tercero { get; set; }
        //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

        //***********

        public List<RendimientoAcademicoEvaluacionBE> ListaRendimientoAcademicoEvaluacionBE { get; set; }

        public RendimientoAcademicoBE()
        {
            ListaRendimientoAcademicoEvaluacionBE = new List<RendimientoAcademicoEvaluacionBE>();
        }
    }
}
