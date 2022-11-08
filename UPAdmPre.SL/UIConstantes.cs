using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPAdmPre.SL
{
    public class UIConstantes
    {
        public static Int32 idValorNulo = 0;
        public static Int32 idValorActivo = 1;
        public static Int32 idDptoExt = 77;
        public static Int32 idProvExt = 17;
        public static Int32 idDistExt = 1839;

        public static String sRutaSubirArchivo = "Controles/frmDocumentoPRE.aspx";
        public static String sDescargaArchivo = "frmDescargaDocumento.aspx";
        public static String sRutaEnvioEmail = "Controles/FrmEnvioEmail.aspx";
        public static String sRutaEnvioEmailEPU = "../Controles/FrmEnvioEmail.aspx";

        public static String _strAplicacion = "UPAdmPre";
        public static String _valorCadenaVacia = "";
        public static String _TextoSeleccionar = "-- Seleccionar --";
        public static String _TextoNotaOtro = "Otro";
        public static String _valorCodigoTelfPeru = "+51";
        public static String _valorSignoMas = "+";
        public static String _valorSignoAsterisco = "*";
        public static String _valorSignoGuion = " - ";
        public static String _valorNotaOtro = "21";
        public static String _valorCadenaEspacio = " ";
        public static String _valorIdPaisPeru = "554";
        public static String _valorActivo = "1";
        public static String _TextoOtroIdioma = "OTRO";
        public static String _ValorOtroIdioma = "99";
        public static String _ValorOtraCertificacion = "7";

        public static Int32 _IdPostulacionPreGrado = 7;
        public static Int32? _IdProgramasEPU = 19;
        public static Int32? _valorNotaOtroEnt = 21;

        public static String strEstiloCorreo = "<style> .HeaderGrilla{font-size: 11px;color: #15498B;font-family: Arial;background-color: #CBDDF3;text-align: center;vertical-align: middle;height: 25px;} .ItemGrilla{font-size: 10px;color: #000080;font-family: Arial;background-color: #dddddd;height: 20px;text-align: left;vertical-align: middle;border-bottom-width: 1px;border-bottom-style: solid;border-bottom-color: #ffffff;} .AlternateItemGrilla{font-size: 10px;font-family: Arial;color: #000080;background-color: #eeeeee;height: 20px;text-align: left;vertical-align: middle;} </style>";

        #region enum
        public enum TIPO_FORMULARIO
        {
            NINGUNO = 0,
            PREGRADO = 7,
            POSTGRADO = 6,
            ADMISION_EPU = 19
        }

        public enum ESTRUCTURA_TABLAS
        {
            DATOS_PERSONALES = 0,
            DIRECCION = 1,
            TELECOMUNICACIONES = 2,
            EMPLEOS = 0,
            EDUCACION = 0,
            DETALLE_EDUCACION = 0,
            IDIOMA = 0,
            REFERENCIAS = 0,
            ACTIVIDADES = 8,
            RELACIONES = 0,
            REQUISITOS = 10,
            CALIFICACION = 11,
            NOTAS = 12,
            INTERES = 0,
            TERMINOS = 0
        }

        public enum Estruct_Tabla_Colegio
        {
            COLEGIO = 0
        }

        public enum Estruct_Tabla_Universidad
        {
            Universidad = 0,
            UniversidadDet = 0
        }

        public enum Estruct_Tabla_RendAcademico
        {
            ORDEN_MERITO = 0,
            NOTAS = 1,
            NOTAS_LETRAS = 2 /*Agrega:Christian Ramirez - REQ91569*/
        }

        public enum ESTRUCTURA_TABLAS_ASPIRANTE
        {
            DATOS_PERSONALES = 0,
            DATOS_COLEGIOS = 1,
            DATOS_IDIOMAS = 2,
            DATOS_PADRES = 3
        }

        public enum ESTADO_POSTULANTE
        {
            NINGUNO = 0,
            PENDIENTE = 1,
            APROBADO = 2,
            RECHAZADO = 3
        }

        public enum TIPO_CODIGO
        {
            NINGUNO = 0,
            DEPARTAMENTO = 1,
            PROVINCIA = 2,
            PAIS = 3,
            MAESTRIA = 4,
            CATEGORIA_EMPLEO = 5,
            HONOR = 6,
            ESPECIALIDAD = 7,
            //GRADO = 8,
            IDIOMA = 9,
            CARRERA_PREGRADO = 10,
            AREA_FUNCIONAL = 11,
            DISTRITO = 12,
            CARRERA_UP = 13,
            TIPO_CONTROL_INSTITUCION = 14,
            TIPO_POSTULACION_PREGRADO = 15,
            TIPO_ACTIVIDAD = 16,
            TIPO_FORMULARIO = 17,
            ESTADO_APLICACION = 18,
            PERIODO_PREGRADO = 19,
            PERIODO_POSTGRADO = 20,
            GRADO_PREGRADO = 21,
            GRADO_EPG = 22,
            CICLO_EPU = 23,
            HORARIO_CICLO_EPU = 24,
            TIPO_VIA = 25,
            CERTIFICADO_IDIOMA = 26,
            TIPO_POSTULACION_FICHA = 27,
            TIPO_POSTULACION_FICHA1 = 28,
            TIPO_POSTULACION_EPU = 29,
            TIPO_FORM_PRE = 30,
            ESTA_ACADEMICO = 31,
            TIPO_PROGRAMA_EPU_NUEVO = 32,
            TIPO_DOCUMENTO = 33,
            ESTA_ACADEMICOMOD = 34,
            TIPO_POSTULACION_PREGRADO_CONT = 35,
            TIPO_PROGRAMA_EPU_CONTINUA = 36,
            ANNIO = 37,
            ANNIO2 = 38,
            ESTA_ACADEMICOEXCACA = 39,
            PERIODOACA = 40,
            PREBECA = 41,
            TIPO_POSTULACION_PREGRADO2 = 42,
            PERIODOACACONT = 43,
            TIPOEVALUA = 44,
        }

        public enum TIPO_DIRECCION
        {
            NINGUNO = 0,
            GREAT_PLAINS_ADDRESS = 1,
            NEGOCIO = 5,
            DOMICILIO = 6,
            DOMICILIO_LEGAL = 9,
            FACTURACION = 10,
            FAMILIAR = 11,
            NACIMIENTO = 13,
            PO_BOX = 15,
            SEDE_PRINCIPAL = 17,
            TRABAJO = 18,
            DIRECCION_ELECTRÓNICA = 19
        }

        public enum TIPO_TELEFONO
        {
            NINGUNO = 0,
            CASA = 1,
            CELHIJO = 5,
            CELULAR = 6,
            OTRO = 9,
            TRABAJO = 10
        }

        public enum GRADO_PREGRADO
        {
            NINGUNO = 0,
            CUARTO_SECUNDARIA = 8,
            EGRESADO_COLEGIO = 34,
            QUINTO_SECUNDARIA = 9,
            TECNICO_SUPERIOR = 23,
            TERCERO_SECUNDARIA = 33
        }

        public enum TIPO_ORGANIZACION
        {
            NINGUNO = 0,
            EMPLEADOR = 1,
            EMPRESA = 2,
            ESCUELA = 3,
            FUERZAS_ARMADAS = 4,
            INSTITUTO_SUPERIOR = 5,
            PATROCINADOR = 6,
            PROVEE_A_LOS_ESTUDIANTES = 7,
            UNIVERSIDAD = 8
        }

        public enum TIPO_ERROR
        {
            NINGUNO = 0,
            ERROR_GENERAL = 1,
            ERROR_INSERTAR_REGISTRO = 2,
            ERROR_DATOS_FORMULARIO = 3,
            ERROR_CARGAR_DATOS_FORMULARIO = 4,
            ERROR_ENVIA_COLEGIO = 5,
            ERROR_ELIMINAR_REGISTRO = 6 /*Se agrega:Christian Ramirez - GIIT[caso 64015] - 20190619*/
        }

        public enum ESTRUCTURA_REPORTE_APLICANTE
        {
            NINGUNO = -1,
            ALUMNOS = 0,
            APLICANTE = 1,
            TRABAJO_ACTUAL = 2,
            TRABAJO_PREVIO = 3,
            ESTUDIOS = 4,
            IDIOMAS = 5,
            EXPERIENCIA_INVESTIGACION = 6,
            REFERENCIA = 7,
            MODALIDAD = 8,
            COLEGIO = 9,
            COLEGIO_2 = 10,
            INFO_ACADEMICA = 11,
            ACTIVIDADES = 12,
            ACTIVIDADES_2 = 13,
            ACTIVIDADES_3 = 14,
            PADRES = 15,
            ACTIVIDADES_4 = 16,
            ACTIVIDADES_5 = 17,
            CARRERA = 18,
        }

        public enum SECCION_EN_FORMULARIO
        {
            NINGUNO = 0,
            FORM_POSITION_1C = 1,
            FORM_POSITION_2C = 2,
            FORM_POSITION_3C = 3,
            FORM_POSITION_1U = 4,
            FORM_POSITION_2U = 5,
            FORM_POSITION_3U = 6,
            FORM_POSITION_1O = 7,
            FORM_POSITION_2O = 8,
            FORM_POSITION_3O = 9
        }

        /*Ini:Christian Ramirez - REQ91569*/
        public enum SITUACION_ACADEMICA
        {
            TERCERO_SECUNDARIA = 33,
            CUARTO_SECUNDARIA = 8,
            QUINTO_SECUNDARIA = 9,
            EGRESADO = 34,
            //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ103573
            TRASLADO_GRADUADO = 61,
            ADMISION_PRE_PACIFICO = 62,
            //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ103573
            ESTUDIANTE = 63 //Se agrega:CHRISTIAN RAMIREZ(EXT09) - REQ104565
            , EXAMEN_ADMISION_REGULAR = 64 //Se agrega:Christian Ramirez - REQ113651

        }

        public enum REND_ACADEMICO_TIPO_CALIFICACION
        {
            SELECCIONAR = 0,
            NUMERICO = 1,
            LETRAS = 2
        }

        public enum REND_ACADEMICO_CURSO
        {
            CienciaTecnologia = 1,
            CienciasSociales = 2,
            CompetenciasTransversales = 3,/*Se modifica:Christian Ramirez - REQ95070*/
            ComunicacionLenguaMaterna = 4,
            Matematica = 5
        }

        public enum REND_ACADEMICO_COMPETENCIA
        {
            Construye_interpretaciones_historicas = 1,
            Diseña_y_construye_soluciones_tecnológicas_para_resolver_problemas = 2,
            Escribe_diversos_tipos_de_texto = 3,
            Explica_el_mundo_físico_basándose_en_conocimientos_sobre_los_seres_vivos_materia_y_energía_biodiversidad = 4,
            Gestiona_responsablemente_el_espacio_y_el_ambiente = 5,
            Gestiona_responsablemente_los_recursos_económicos = 6, /*Se modifica:Christian Ramirez - REQ95070*/
            Gestiona_su_aprendizaje_de_manera_autonoma = 7,
            Indaga_mediante_metodos_científicos_para_construir_sus_conocimientos = 8,
            Lee_diversos_tipos_de_textos_escritos = 9,
            Resuelve_problemas_de_cantidad = 10,
            Resuelve_problemas_de_forma_y_movimiento = 11,
            Resuelve_problemas_de_gestion_de_datos_e_incertidumbre = 12,
            Resuelve_problemas_de_regularidad_equivalencia_y_cambio = 13,
            Se_comunica_oralmente = 14,
            Se_desenvuelve_en_los_entornos_virtuales_generados_por_las_TIC = 15
        }

        public enum REND_ACADEMICO_CALIFICACION
        {
            A = 1,
            B = 2,
            C = 3,
            AD = 4,
            CNE = 5
        }
        /*Fin:Christian Ramirez - REQ91569*/
        #endregion

        #region dictionary
        public static Dictionary<TIPO_ERROR, String> ObtenerTipoError()
        {
            Dictionary<TIPO_ERROR, String> oTipoError = new Dictionary<TIPO_ERROR, String>();

            oTipoError.Add(TIPO_ERROR.NINGUNO, "NINGUNO");
            oTipoError.Add(TIPO_ERROR.ERROR_GENERAL, "Error general");
            oTipoError.Add(TIPO_ERROR.ERROR_INSERTAR_REGISTRO, "Error de inserción");
            oTipoError.Add(TIPO_ERROR.ERROR_DATOS_FORMULARIO, "Error de datos del formulario");
            oTipoError.Add(TIPO_ERROR.ERROR_ENVIA_COLEGIO, "Error al enviar datos de colegio nuevo");

            /*Ini:Christian Ramirez - GIIT[caso 64015] - 20190619*/
            oTipoError.Add(TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO, "Error al cargar los datos del formulario");
            oTipoError.Add(TIPO_ERROR.ERROR_ELIMINAR_REGISTRO, "Error al eliminar registro");
            /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/

            return oTipoError;
        }

        public static Dictionary<TIPO_CODIGO, KeyValuePair<String, String>> ObtenerTipoCodigo()
        {
            Dictionary<TIPO_CODIGO, KeyValuePair<String, String>> oTipoCodigo = new Dictionary<TIPO_CODIGO, KeyValuePair<String, String>>();

            oTipoCodigo.Add(TIPO_CODIGO.NINGUNO, new KeyValuePair<String, String>("0", "<Seleccione un valor>"));
            oTipoCodigo.Add(TIPO_CODIGO.DEPARTAMENTO, new KeyValuePair<String, String>("DPTO", "DEPARTAMENTO"));
            oTipoCodigo.Add(TIPO_CODIGO.PROVINCIA, new KeyValuePair<String, String>("PROV", "PROVINCIA"));
            oTipoCodigo.Add(TIPO_CODIGO.PAIS, new KeyValuePair<String, String>("PAIS", "PAIS"));
            oTipoCodigo.Add(TIPO_CODIGO.MAESTRIA, new KeyValuePair<String, String>("MAES", "MAESTRIAS"));
            oTipoCodigo.Add(TIPO_CODIGO.CATEGORIA_EMPLEO, new KeyValuePair<String, String>("CATE", "CATEGORIAS EMPLEO"));
            oTipoCodigo.Add(TIPO_CODIGO.HONOR, new KeyValuePair<String, String>("HONOR", "HONOR"));
            oTipoCodigo.Add(TIPO_CODIGO.ESPECIALIDAD, new KeyValuePair<String, String>("ESPEC", "ESPECIALIDADES"));
            //oTipoCodigo.Add(TIPO_CODIGO.GRADO, new KeyValuePair<String, String>("GRADO", "GRADO"));
            oTipoCodigo.Add(TIPO_CODIGO.GRADO_PREGRADO, new KeyValuePair<String, String>("GRADOPRE", "GRADO PREGRADO"));
            oTipoCodigo.Add(TIPO_CODIGO.GRADO_EPG, new KeyValuePair<String, String>("GRADOEPG", "GRADO POSTGRADO"));
            oTipoCodigo.Add(TIPO_CODIGO.IDIOMA, new KeyValuePair<String, String>("IDIOMA", "IDIOMA"));
            oTipoCodigo.Add(TIPO_CODIGO.CARRERA_PREGRADO, new KeyValuePair<String, String>("CARRERAPRE", "CARRERAS PREGRADO"));
            oTipoCodigo.Add(TIPO_CODIGO.AREA_FUNCIONAL, new KeyValuePair<String, String>("AREAFUNC", "AREA FUNCIONAL"));
            oTipoCodigo.Add(TIPO_CODIGO.DISTRITO, new KeyValuePair<String, String>("DISTRITO", "DISTRITO"));
            oTipoCodigo.Add(TIPO_CODIGO.CARRERA_UP, new KeyValuePair<String, String>("CARRUPPRE", "CARRERA UP"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_CONTROL_INSTITUCION, new KeyValuePair<String, String>("TIPCONINST", "TIPO CONTROL INSTITUCION"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_POSTULACION_PREGRADO, new KeyValuePair<String, String>("TIPPOSTPRE", "TIPO POSTULACIÓN PREGRADO"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_ACTIVIDAD, new KeyValuePair<String, String>("TIPACT", "TIPO ACTIVIDAD"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_FORMULARIO, new KeyValuePair<String, String>("TIPFORM", "TIPO FORMULARIO"));
            oTipoCodigo.Add(TIPO_CODIGO.ESTADO_APLICACION, new KeyValuePair<String, String>("ESTAPLI", "ESTADO APLICACIÓN"));
            oTipoCodigo.Add(TIPO_CODIGO.PERIODO_PREGRADO, new KeyValuePair<String, String>("PERPRE", "PERIODO PREGRADO"));
            oTipoCodigo.Add(TIPO_CODIGO.PERIODO_POSTGRADO, new KeyValuePair<String, String>("PEREPG", "PERIODO POSTGRADO"));
            oTipoCodigo.Add(TIPO_CODIGO.CICLO_EPU, new KeyValuePair<String, String>("CIADMEPU", "CICLO EPU PARA ADMISION"));
            oTipoCodigo.Add(TIPO_CODIGO.HORARIO_CICLO_EPU, new KeyValuePair<String, String>("HOADMEPU", "HORARIO EPU PARA ADMISION"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_VIA, new KeyValuePair<String, String>("TIPOVIA", "TIPO DE VIA"));
            oTipoCodigo.Add(TIPO_CODIGO.CERTIFICADO_IDIOMA, new KeyValuePair<String, String>("CERTIDIO", "CERTIFICADO DE IDIOMAS"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_POSTULACION_FICHA, new KeyValuePair<String, String>("TIPPOSTFICHA", "TIPO POSTULACION FICHA"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_POSTULACION_FICHA1, new KeyValuePair<String, String>("TIPPOSTFICHA1", "TIPO POSTULACION FICHA1"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_POSTULACION_EPU, new KeyValuePair<String, String>("TIPOPOSTEPU", "TIPO POSTULACION EPU"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_FORM_PRE, new KeyValuePair<String, String>("TIPFORMPRE", "TIPO FORMULARIO PRE"));
            oTipoCodigo.Add(TIPO_CODIGO.ESTA_ACADEMICO, new KeyValuePair<String, String>("ESTACAD", "ESTADO ACADEMICO EPU"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_PROGRAMA_EPU_NUEVO, new KeyValuePair<String, String>("TIPPRGEPU", "TIPO DE PROGRAMA EPU"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_DOCUMENTO, new KeyValuePair<String, String>("TIPDOCU", "TIPO DE DOCUMENTO"));
            oTipoCodigo.Add(TIPO_CODIGO.ESTA_ACADEMICOMOD, new KeyValuePair<String, String>("ESTACADMOD", "ESTADO ACADEMICO APLICANTE"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_POSTULACION_PREGRADO_CONT, new KeyValuePair<String, String>("TIPPOSTPRECONT", "TIPO POSTULACIÓN PREGRADO CONTINUACION"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_PROGRAMA_EPU_CONTINUA, new KeyValuePair<String, String>("TIPPRGEPUCONT", "TIPO DE PROGRAMA EPU CONTINUACION"));
            oTipoCodigo.Add(TIPO_CODIGO.ANNIO, new KeyValuePair<String, String>("ANNIO", "AÑO LECTIVO"));
            oTipoCodigo.Add(TIPO_CODIGO.ANNIO2, new KeyValuePair<String, String>("ANNIO2", "AÑO LECTIVO"));
            oTipoCodigo.Add(TIPO_CODIGO.ESTA_ACADEMICOEXCACA, new KeyValuePair<String, String>("ESTAACADEMICOEXCACA", "ESTADO ACADEMICO APLICANTE SELECTIVA"));
            oTipoCodigo.Add(TIPO_CODIGO.PERIODOACA, new KeyValuePair<String, String>("PERIOPRE", "PERIODO PREGRADO"));
            oTipoCodigo.Add(TIPO_CODIGO.PREBECA, new KeyValuePair<String, String>("PREBECA", "PREBECA"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPO_POSTULACION_PREGRADO2, new KeyValuePair<String, String>("TIPPOSTPRE2", "TIPO POSTULACIÓN PREGRADO2"));
            oTipoCodigo.Add(TIPO_CODIGO.PERIODOACACONT, new KeyValuePair<String, String>("PERIODOACACONT", "PERIODOACACONT"));
            oTipoCodigo.Add(TIPO_CODIGO.TIPOEVALUA, new KeyValuePair<String, String>("TIPOEVALUA", "TIPOEVALUA"));

            return oTipoCodigo;
        }

        /*Ini:Christian Ramirez - REQ91569*/
        public static Dictionary<REND_ACADEMICO_TIPO_CALIFICACION, string> ObtenerRendAcademicoTipoCalificacion()
        {
            Dictionary<REND_ACADEMICO_TIPO_CALIFICACION, string> lista = new Dictionary<REND_ACADEMICO_TIPO_CALIFICACION, string>();
            lista.Add(REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR, "-- Seleccionar --");
            lista.Add(REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO, "Numérico");
            lista.Add(REND_ACADEMICO_TIPO_CALIFICACION.LETRAS, "Letras");
            return lista;
        }

        public static string ObtenerRendAcademicoCalificacion(string codigo)
        {
            Dictionary<string, string> lista = new Dictionary<string, string>();
            lista.Add("0", "");
            lista.Add(REND_ACADEMICO_CALIFICACION.A.ToString("D"), "A");
            lista.Add(REND_ACADEMICO_CALIFICACION.B.ToString("D"), "B");
            lista.Add(REND_ACADEMICO_CALIFICACION.C.ToString("D"), "C");
            lista.Add(REND_ACADEMICO_CALIFICACION.AD.ToString("D"), "AD");
            lista.Add(REND_ACADEMICO_CALIFICACION.CNE.ToString("D"), "CNE");

            return lista[codigo];
        }
        /*Fin:Christian Ramirez - REQ91569*/
        #endregion

        #region class
        //WTaipe
        public class Alert
        {
            public static String msgModalidadNoDisponible = "No se encontro modalidades de postulacion disponibles.";
            public static String msgCarreraNoDisponible = "No se encontro carreras disponibles para esta modalidad de postulación.";
            public static String msgProgramasNoDisponible = "No se encontro programas disponibles.";
            public static String msgCursosNoDisponible = "No se encontro cursos disponibles para este programa.";
            public static String msgConfirmaEliminarRegistro = "¿Desea eliminar el registro?.";
            public static String msgPeriodoNoDisponible = "No se encontro periodo de postulación disponible";/*Se agrega: Christian Ramirez - GIIT[caso 64015] - 20190619*/
        }

        /*Ini:Christian Ramirez - GIIT[caso 64015] - 20190619*/
        /*se comenta todo*/
        //public class Mensajes
        //{
        //    public const String _msgErrorCorreo1 = "Por Favor, dar atención a la siguiente observación a la brevedad posible.";
        //    public const String _msgErrorCorreo2 = "Gracias por su atención.";
        //    public const String _msgErrorCorreo3 = "Mensaje autogenerado por el Sistema UPAdmPre.";
        //}
        /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/

        public class EventosJS
        {
            public static readonly String JsEventoOnBlur = "OnBlur";
            public static readonly String JsEventoOnChange = "OnChange";
            public static readonly String JsEventoOnClick = "OnClick";
            public static readonly String JsEventoOndblclick = "Ondblclick";
            public static readonly String JsEventoOnFocus = "OnFocus";
            public static readonly String JsEventoOnKeydown = "OnKeydown";
            public static readonly String JsEventoOnKeypress = "OnKeypress";
            public static readonly String JsEventoOnKeyup = "OnKeyup";
            public static readonly String JsEventoOnMouseDown = "OnMouseDown";
            public static readonly String JsEventoOnMousemove = "OnMousemove";
            public static readonly String JsEventoOnMouseout = "OnMouseout";
            public static readonly String JsEventoOnMouseover = "OnMouseover";
            public static readonly String JsEventoOnSubmit = "OnSubmit";
        }

        public class FormatoFechas
        {
            public static String _FechaMinima = "01/01/1755";
            public static String _FormatoFechaCorto = "yyyyMMdd";
            public static String _FormatoFechaCompleto = "yyyyMMddHHmmss";
            public static String _FormatoFechaCortoDMA = "ddMMyyyy";
            public static String _FormatoFechaCortoDMATallyman = "yyMMdd";
            public static String _FormatoFechaConsulta = "dd/MM/yyyy";
            public static String _FormatoFechaCortoArchivo = "yyyy-MM-dd";
            public static String _FormatoHoraMinutoSegundo = "HH:mm:ss";
            public static String _FormatoDiasMes = "ddMM";

            public static String _valorFechaMinimoCarga = "01/01/1990";
            public static String _formatoFechaCompletaGeneral = "ddMMyyyy_HHmmss";
        }

        public class Numero
        {
            public const Int32 _ValorNumeroCero = 0;
            public const Int32 _ValorNumeroUno = 1;
            public const Int32 _ValorNumeroDos = 2;
            public const Int32 _ValorNumeroTres = 3;
            public const Int32 _ValorNumeroCuatro = 4;
            public const Int32 _ValorNumeroCinco = 5;
            public const Int32 _ValorNumeroSeis = 6;
            public const Int32 _ValorNumeroSiete = 7;
            public const Int32 _ValorNumeroOcho = 8;
            public const Int32 _ValorNumeroNueve = 9;
            public const Int32 _ValorNumeroDiez = 10;
        }

        public class TipoColegio
        {
            public const String _Bachillerato = "1";
            public const String _Normal = "2";
        }

        public class Grado
        {
            public static String _Tercero = "3er Año";
            public static String _Cuarto = "4to Año";
            public static String _Quinto = "5to Año";
            public static String _Sexto = "6to Año";
        }

        public class Modalidad
        {
            public static Int32 Selectiva = 40;
            public static Int32 Bachillerato = 41;
            public static Int32 IngDirectoEPU = 42;
            public static Int32 AdmRegular = 46;
            public static Int32 Traslados = 44;
            public static Int32 Graduados = 43;
            public static Int32 ExcelAcadem = 49;
            public static Int32 Reingresos = 45;
            public static Int32 Becados = 47;
            public static Int32 TallerEPU = 20;
            public static Int32 ProgramaEPU = 999;
            public static Int32 Exterior = 50;

            //christian
            public static Int32 BachilleratoMitadSuperior = 58;
            public static Int32 BachilleratoQuintoSuperior = 57;
            //christian
        }

        public class Formularios
        {
            public static String F01 = "frm01_VideoIntro.aspx";
            public static String F02 = "frm02_ModalidadPostula.aspx";
            public static String F03 = "frm03_ProgramasEPU.aspx";
            public static String F04 = "frm04_DatoPersonal.aspx";
            public static String F05 = "frm05_ColegioProcede.aspx";
            public static String F06 = "frm06_RendimientoAcademico.aspx";
            public static String F07 = "frm07_ActividadExtracurricular.aspx";
            public static String F08 = "frm08_Idiomas.aspx";
            public static String F09 = "frm09_InfoPadres.aspx";
            public static String F10 = "frm10_InfoReferencias.aspx";
            public static String F11 = "frm11_EstudiosUniversitarios.aspx";
            public static String F12 = "frm12_ExperienciaLaboral.aspx";
            public static String F13 = "frm13_OtrosEstudios.aspx";
            public static String F14 = "frm14_DocRequeridos.aspx";
            public static String F15 = "frm15_BoletaPagos.aspx";
            public static String F16 = "frm16_TerminosyCondiciones.aspx";
            public static String F17 = "frm17_ResumenFinal.aspx";
            public static String F18 = "frm18_Entrevista.aspx";
            public static String F19 = "frm19_EntrevistaOtrasModalidades.aspx";
            public static String F20 = "frm20_FormalizaIng.aspx";
            public static String F21 = "frm21_ModalidadColegioPostula.aspx"; /*Se agrega:Christian Ramirez - GIIT[caso 64015] - 20190619*/
            public static String F22 = "Default.aspx";
        }

        public class TitulosDepaginas
        {
            public static String TitForm01 = "Video";
            public static String TitForm02 = "Modalidad de Postulación";
            public static String TitForm03 = "Programas EPU";
            public static String TitForm04 = "Datos Personales";
            public static String TitForm05 = "Colegio de Procedencia";
            public static String TitForm06 = "Rendimiento Académico";
            public static String TitForm07 = "Actividades Extracurriculares";
            public static String TitForm08 = "Conocimiento de Idiomas ";
            public static String TitForm09 = "Información de Padres";
            public static String TitForm10 = "Referencias Personales";
            public static String TitForm11 = "Documentos Requeridos";
            public static String TitForm12 = "Terminos y Condiciones";
            public static String TitForm13 = "Estudios Universitarios";
            public static String TitForm14 = "Experiencia Laboral";
            public static String TitForm15 = "Otros Estudios";
        }

        public class EstadoDocumento
        {
            public static String strPendiente = "Pendiente";
            public static String strEnRevision = "En Revisión";
            public static String strObservado = "Observado";
            public static String strAprobado = "Aprobado";
        }

        /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
        public class TipoEntrevista
        {
            public static string EntrevEclVirtual = "Entrevista y ECL Virtual";
            public static string EntrevEclPresencial = "Entrevista y ECL Presencial";
            public static string EntrevVirtual = "Entrevista Virtual";
            public static string EntrevPrecencial = "Entrevista Presencial";
        }
        /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/
        #endregion
    }
}
