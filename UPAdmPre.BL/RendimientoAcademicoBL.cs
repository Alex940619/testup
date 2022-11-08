using System;
using System.Data;
using UPAdmPre.BE;
using UPAdmPre.DL;

namespace UPAdmPre.BL
{
    public class RendimientoAcademicoBL
    {
        private RendimientoAcademicoDL oRendimientoAcademicoDL;

        public RendimientoAcademicoBL()
        {
            oRendimientoAcademicoDL = new RendimientoAcademicoDL();
        }

        #region Metodos No Transaccionales
        public DataTable ObtenerModalidadesPorCompetencia(string redid,
            RendimientoAcademicoBE oRendimientoAcademicoBE)
        {
            DataTable dtModalidades = new DataTable();

            try
            {
                dtModalidades = oRendimientoAcademicoDL.ObtenerModalidadesPorCompetencia(redid, 
                    oRendimientoAcademicoBE);
            }
            catch (Exception)
            {
                dtModalidades = null;
            }

            return dtModalidades;
        }

        public DataTable ObtenerModalidadesPorRendimiento(string redid,
            RendimientoAcademicoBE oRendimientoAcademicoBE)
        {
            DataTable dtModalidades = new DataTable();

            try
            {
                dtModalidades = oRendimientoAcademicoDL.ObtenerModalidadesPorRendimiento(redid,
                    oRendimientoAcademicoBE);
            }
            catch (Exception)
            {
                dtModalidades = null;
            }

            return dtModalidades;
        }

        public int InsertarDatosFormVeintiUno_CantidadCompetencia(AplicanteBE oAplicanteBE)
        {
            int rpta = 0;

            try
            {
                rpta = oRendimientoAcademicoDL.InsertarDatosFormVeintiUno_CantidadCompetencia(oAplicanteBE);
            }
            catch (Exception ex)
            {
                rpta = 0;
            }

            return rpta;
        }

        public int InsertarDatosFormVeinte_CantidadCompetencia(AplicanteBE oAplicanteBE)
        {
            int rpta = 0;

            try
            {
                rpta = oRendimientoAcademicoDL.InsertarDatosFormVeinte_CantidadCompetencia(oAplicanteBE);
            }
            catch (Exception ex)
            {
                rpta = 0;
            }

            return rpta;
        }

        public RendimientoAcademicoBE ObtenerNotasADsPorCompetenciaRegistrada_Cuarto(AplicanteBE oAplicanteBE)
        {
            RendimientoAcademicoBE oRendimientoAcademicoBE = null;

            try
            {
                oRendimientoAcademicoBE = 
                    oRendimientoAcademicoDL.ObtenerNotasADsPorCompetenciaRegistrada_Cuarto(oAplicanteBE);
            }
            catch (Exception ex)
            {
                oRendimientoAcademicoBE = null;
            }

            return oRendimientoAcademicoBE;
        }

        public RendimientoAcademicoBE ObtenerNotasADsPorCompetenciaRegistrada_Quinto(AplicanteBE oAplicanteBE)
        {
            RendimientoAcademicoBE oRendimientoAcademicoBE = null;

            try
            {
                oRendimientoAcademicoBE =
                    oRendimientoAcademicoDL.ObtenerNotasADsPorCompetenciaRegistrada_Quinto(oAplicanteBE);
            }
            catch (Exception ex)
            {
                oRendimientoAcademicoBE = null;
            }

            return oRendimientoAcademicoBE;
        }

        public DataTable ObtenerListaCalificacionRendimientoAcademico()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = oRendimientoAcademicoDL.ObtenerListaCalificacionRendimientoAcademico();
            }
            catch (Exception)
            {
                dt = null;
            }

            return dt;
        }

        //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ103573
        public DataTable ObtenerModalidadesParaGraduadoTraslado(string redid, int sessionPeriodId)
        {
            DataTable dtModalidades = new DataTable();

            try
            {
                dtModalidades = oRendimientoAcademicoDL.ObtenerModalidadesParaGraduadoTraslado(redid, sessionPeriodId);
            }
            catch (Exception)
            {
                dtModalidades = null;
            }

            return dtModalidades;
        }

        public DataTable ObtenerModalidadesParaAdmisionPrePacifico(string redid, int sessionPeriodId)
        {
            DataTable dtModalidades = new DataTable();

            try
            {
                dtModalidades = oRendimientoAcademicoDL.ObtenerModalidadesParaAdmisionPrePacifico(redid, sessionPeriodId);
            }
            catch (Exception)
            {
                dtModalidades = null;
            }

            return dtModalidades;
        }
        //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ103573

        //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
        public DataTable ObtenerModalidadesPorCompetenciaEstudiante(string redid, RendimientoAcademicoBE oRendimientoAcademicoBE)
        {
            DataTable dtModalidades = new DataTable();

            try
            {
                dtModalidades = oRendimientoAcademicoDL.ObtenerModalidadesPorCompetenciaEstudiante(redid, oRendimientoAcademicoBE);
            }
            catch (Exception)
            {
                dtModalidades = null;
            }

            return dtModalidades;
        }

        public RendimientoAcademicoBE ObtenerNotasADsPorCompetenciaRegistrada_Tercero(AplicanteBE oAplicanteBE)
        {
            RendimientoAcademicoBE oRendimientoAcademicoBE = null;

            try
            {
                oRendimientoAcademicoBE = oRendimientoAcademicoDL.ObtenerNotasADsPorCompetenciaRegistrada_Tercero(oAplicanteBE);
            }
            catch (Exception ex)
            {
                oRendimientoAcademicoBE = null;
            }

            return oRendimientoAcademicoBE;
        }

        //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

        //Ini:Christian Ramirez - REQ113651
        public DataTable ObtenerModalidadExamenAdmisionRegular(string redid, int sessionPeriodId)
        {
            DataTable dtModalidades = new DataTable();

            try
            {
                dtModalidades = oRendimientoAcademicoDL.ObtenerModalidadExamenAdmisionRegular(redid, sessionPeriodId);
            }
            catch (Exception)
            {
                dtModalidades = null;
            }

            return dtModalidades;
        }
        //Fin:Christian Ramirez - REQ113651
        #endregion
    }
}
