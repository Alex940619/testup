using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
    public class AplicanteBE
    {
        public AplicanteBE()
        { }

        #region "Atributos"

        private String _Carrera = null;
        private Int32? _idAplicante = null;
        private DateTime? _fechaCreacion = null;
        private Int32? _estado = null;
        private Int32? _idPerson = null;
        private Int32? _prefijo = null;
        private String _primerNombre = null;
        private String _segundoNombre = null;
        private String _apellidos = null;
        private Int32? _sufijo = null;
        private String _alias = null;
        private String _apellidosAntiguos = null;
        private String _correoPersonal = null;
        private String _correoLaboral = null;
        private DateTime? _fechaNacimiento = null;
        private Int32? _genero = null;
        private Int32? _etnicidad = null;
        private Int32? _estadoMarital = null;
        private Int32? _religion = null;
        private Int32? _estadoVeterano = null;
        private Boolean _esRetirado = false;
        private Int32? _nacionalidadPrimaria = null;
        private Int32? _nacionalidadSecundaria = null;
        private Int32? _paisNacimiento = null;
        private Int32? _dptoNacimiento = null;
        private Int32? _idiomaPrimario = null;
        private Int32? _idiomaSecundario = null;
        private Int32? _mesesPais = null;
        private String _documentoIdentidad = null;       

        /*Se modifica: Christian Ramirez GIIT - Caso45903 - 20180607*/
        private String _ubigeoNacimiento;  /*Se agrega: Christian Ramirez[GIIT] - Caso43692 - 20180423*/
        /*Se modifica: Christian Ramirez GIIT - Caso45903 - 20180607*/

        private Int32? _visa = null;
        private String _numeroVisa = null;
        private Int32? _paisVisa = null;
        private DateTime? _fechaExpiracionVisa = null;
        private String _numeroPasaporte = null;
        private Int32? _paisPasaporte = null;
        private DateTime? _fechaExpiracionPasaporte = null;
        private Int32? _idPeriodoSesion = null;
        private Int32? _estadoAsistenciaUniversidad = null;
        private Boolean _estaBuscandoEstudio = false;
        private Boolean _estaInteresadoExtraCurricular = false;
        private Boolean _estaInteresadoFinanzas = false;
        private Int32? _idFuente = null;
        private Int32? _idTransaccionPago = null;
        private Int32? _preferenciaResidencia = null;
        private Int32? _estaInteresadoPlanComida = null;
        private Int32? _estaInteresadoPlanResidenciaUni = null;
        private Int32? _estaInterasadoResidenciaEnCampus = null;
        private Int32? _estaInterasadoResidenciaEnEdificio = null;
        private Int32? _idTelefonoPrimario = null;
        private Int32? _idDireccionPrimaria = null;
        private Int32? _idConfiguracionAplicacion = null;
        private Int32? _idConsulado = null;
        private String _otrasFuentes = null;
        private Int32? _idMaestria = null;
        private Int32? _estudioEnUP = null;
        private Int32? _idMaestriaAntUP = null;
        private Int32? _anhoMaestriaAntUP = null;
        private DireccionAplicanteBE _direccion = null;
        private TelefonoBE _telefono = null;
        private TelefonoBE _telefono1 = null;
        private TelefonoBE _celular = null;
        private TelefonoBE _celular1 = null;
        private List<EmpleadorBE> _lEmpleador = null;
        private List<EducacionBE> _lEducacion = null;
        private List<EducacionDetalleBE> _lDetalleEducacion = null;
        private List<NotasBE> _lNotas = null;
        private List<IdiomaBE> _lIdioma = null;
        private List<ReferenciaBE> _lReferencia = null;
        private String _redId = null;
        private String _InstitucionNoUPAnt = null;
        private String _gradoProgramaIB = null;
        private List<ActividadBE> _lActividad = null;
        private List<RelacionBE> _lRelacion = null;
        private Int32? _modalidadPostulacion = null;
        private List<RequisitoBE> _lRequisito = null;
        private List<CalificacionBE> _lCalificacion = null;
        private DataTable _dtRequisito = null;
        private DataTable _dtCalificacion = null;

        private String _Tipos = null;
        private Decimal? _Puntaje = null;
        private DateTime? _Fecha = null;
        private String _Comentario = null;
        private String _People = null;

        private Double _cantidadPagada = UIConstantes.idValorNulo;
        private String _numeroBoletaPago = null;
        private DateTime? _fechaEntrevista = null;
        private DateTime? _fechaPresentacion = null;

        private EvaluacionBE _evaluacion = null;
        private LogAplicanteBE _logAplicante = null;

        private String _order_by = null;
        private String _de_forma = null;
        private String _revision_opid = null;
        private String _ape_paterno = null;
        private String _ape_materno = null;
        private String _tipodocumento = null;
        private Int32? _programofstudy = null;
        private String _assign_opid = null;
        private Boolean _pendiente = false;
        private Boolean _isdiscapacitado = false;
        private String _discapacitado = null;

        private NotasBE _notas = null;
        private DataTable _dtNotas = null;

        private Int32? _situacionacademica = null;
        private Int32? _idcondicionacademica = null;
        private String _applicantencrypt = null;

        private String _horario = null;
        private Int32? _idtipopostulacion = null;
        private Int32? _idAntecedentes = null;
        private Int32? _idAutorizacion = null;
        private Int32? _idAutorizacionTerceros = null;

        private List<InteresBE> _lInteres = null;
        private String _TipoUsuario = null;
        private String _strDireccionConcatenado = null;
        private String _codigosap = null;
        private Int32? _sessionId = null;
        private Int32? _becaId = null;
        private Int32? _TipoColeProvId = null;

        //Inicio JC.DelgadoV [Preformalización]
        private Int32? _CambioCarrera = null;
        private Int32? _SeguroRentaEstudiantil = null;
        private Int32? _ReservaMatricula = null;
        private Int32? _DeportistaDestacado = null;
        private List<ExamenFormalizacionBE> _lExamenFormalizacion = null;
        private List<CursoConvalidacionFormalizacionBE> _lCursoConvalidacionFormalizacion = null;
        //Fin JC.DelgadoV [Preformalización]

        //INICIO: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRC)
        private Int32? _EncuestaRGC_ResP1 = null;
        private Int32? _EncuestaRGC_ResP2 = null;
        private Int32? _EncuestaRGC_ResP3 = null;
        private String _EncuestaRGC_ResP4 = null;
        private Int32? _EncuestaRGC_ResP5 = null;
        private Int32? _EncuestaRGC_ResP6 = null;
        //FIN: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRC)

        //INICIO: JC.DelgadoV[RQ107362] Términos y Condiciones
        private bool _AceptTermCond = false;
        private bool _Mayor14ConsentimientoDatPer = false;
        private bool _ApoderadoLegalTitularDatPer = false;
        //FIN: JC.DelgadoV[RQ107362] Términos y Condiciones

        #endregion "Atributos"

        #region "Propiedades"

        public String Carrera
        {
            get { return this._Carrera; }
            set { this._Carrera = value; }
        }

        public Int32? IdAplicante
        {
            get { return this._idAplicante; }
            set { this._idAplicante = value; }
        }

        public DateTime? FechaCreacion
        {
            get { return this._fechaCreacion; }
            set { this._fechaCreacion = value; }
        }

        public Int32? Estado
        {
            get { return this._estado; }
            set { this._estado = value; }
        }

        public Int32? IdPerson
        {
            get { return this._idPerson; }
            set { this._idPerson = value; }
        }

        public Int32? Prefijo
        {
            get { return this._prefijo; }
            set { this._prefijo = value; }
        }

        public String PrimerNombre
        {
            get { return this._primerNombre; }
            set { this._primerNombre = value; }
        }

        public String SegundoNombre
        {
            get { return this._segundoNombre; }
            set { this._segundoNombre = value; }
        }

        public String Apellidos
        {
            get { return this._apellidos; }
            set { this._apellidos = value; }
        }

        public Int32? Sufijo
        {
            get { return this._sufijo; }
            set { this._sufijo = value; }
        }

        public String Alias
        {
            get { return this._alias; }
            set { this._alias = value; }
        }

        public String ApellidosAntiguos
        {
            get { return this._apellidosAntiguos; }
            set { this._apellidosAntiguos = value; }
        }

        public String CorreoPersonal
        {
            get { return this._correoPersonal; }
            set { this._correoPersonal = value; }
        }

        public String CorreoLaboral
        {
            get { return this._correoLaboral; }
            set { this._correoLaboral = value; }
        }

        public DateTime? FechaNacimiento
        {
            get { return this._fechaNacimiento; }
            set { this._fechaNacimiento = value; }
        }

        public Int32? Genero
        {
            get { return this._genero; }
            set { this._genero = value; }
        }

        public Int32? Etnicidad
        {
            get { return this._etnicidad; }
            set { this._etnicidad = value; }
        }

        public Int32? EstadoMarital
        {
            get { return this._estadoMarital; }
            set { this._estadoMarital = value; }
        }

        public Int32? Religion
        {
            get { return this._religion; }
            set { this._religion = value; }
        }

        public Int32? EstadoVeterano
        {
            get { return this._estadoVeterano; }
            set { this._estadoVeterano = value; }
        }

        public Boolean EsRetirado
        {
            get { return this._esRetirado; }
            set { this._esRetirado = value; }
        }

        public Int32? NacionalidadPrimaria
        {
            get { return this._nacionalidadPrimaria; }
            set { this._nacionalidadPrimaria = value; }
        }

        public Int32? NacionalidadSecundaria
        {
            get { return this._nacionalidadSecundaria; }
            set { this._nacionalidadSecundaria = value; }
        }

        public Int32? PaisNacimiento
        {
            get { return this._paisNacimiento; }
            set { this._paisNacimiento = value; }
        }

        public Int32? DptoNacimiento
        {
            get { return this._dptoNacimiento; }
            set { this._dptoNacimiento = value; }
        }

        public Int32? IdiomaPrimario
        {
            get { return this._idiomaPrimario; }
            set { this._idiomaPrimario = value; }
        }

        public Int32? IdiomaSecundario
        {
            get { return this._idiomaSecundario; }
            set { this._idiomaSecundario = value; }
        }

        public Int32? MesesPais
        {
            get { return this._mesesPais; }
            set { this._mesesPais = value; }
        }

        public String DocumentoIdentidad
        {
            get { return this._documentoIdentidad; }
            set { this._documentoIdentidad = value; }
        }

        /*Ini: Christian Ramirez[GIIT] - Caso43692 - 20180423*/
        public string UbigeoNacimiento /*Se modifica: Christian Ramirez GIIT - Caso45903 - 20180607*/
        {
            get { return this._ubigeoNacimiento; }
            set { this._ubigeoNacimiento = value; }
        }
        /*Fin: Christian Ramirez[GIIT] - Caso43692 - 20180423*/

        public Int32? Visa
        {
            get { return this._visa; }
            set { this._visa = value; }
        }

        public String NumeroVisa
        {
            get { return this._numeroVisa; }
            set { this._numeroVisa = value; }
        }

        public Int32? PaisVisa
        {
            get { return this._paisVisa; }
            set { this._paisVisa = value; }
        }

        public DateTime? FechaExpiracionVisa
        {
            get { return this._fechaExpiracionVisa; }
            set { this._fechaExpiracionVisa = value; }
        }

        public String NumeroPasaporte
        {
            get { return this._numeroPasaporte; }
            set { this._numeroPasaporte = value; }
        }

        public Int32? PaisPasaporte
        {
            get { return this._paisPasaporte; }
            set { this._paisPasaporte = value; }
        }

        public DateTime? FechaExpiracionPasaporte
        {
            get { return this._fechaExpiracionPasaporte; }
            set { this._fechaExpiracionPasaporte = value; }
        }

        public Int32? IdPeriodoSesion
        {
            get { return this._idPeriodoSesion; }
            set { this._idPeriodoSesion = value; }
        }

        public Int32? EstadoAsistenciaUniversidad
        {
            get { return this._estadoAsistenciaUniversidad; }
            set { this._estadoAsistenciaUniversidad = value; }
        }

        public Boolean EstaBuscandoEstudio
        {
            get { return this._estaBuscandoEstudio; }
            set { this._estaBuscandoEstudio = value; }
        }

        public Boolean EstaInteresadoExtraCurricular
        {
            get { return this._estaInteresadoExtraCurricular; }
            set { this._estaInteresadoExtraCurricular = value; }
        }

        public Boolean EstaInteresadoFinanzas
        {
            get { return this._estaInteresadoFinanzas; }
            set { this._estaInteresadoFinanzas = value; }
        }

        public Int32? IdFuente
        {
            get { return this._idFuente; }
            set { this._idFuente = value; }
        }

        public Int32? IdTransaccionPago
        {
            get { return this._idTransaccionPago; }
            set { this._idTransaccionPago = value; }
        }

        public Int32? PreferenciaResidencia
        {
            get { return this._preferenciaResidencia; }
            set { this._preferenciaResidencia = value; }
        }

        public Int32? EstaInteresadoPlanComida
        {
            get { return this._estaInteresadoPlanComida; }
            set { this._estaInteresadoPlanComida = value; }
        }

        public Int32? EstaInteresadoPlanResidenciaUni
        {
            get { return this._estaInteresadoPlanResidenciaUni; }
            set { this._estaInteresadoPlanResidenciaUni = value; }
        }

        public Int32? EstaInterasadoResidenciaEnCampus
        {
            get { return this._estaInterasadoResidenciaEnCampus; }
            set { this._estaInterasadoResidenciaEnCampus = value; }
        }

        public Int32? EstaInterasadoResidenciaEnEdificio
        {
            get { return this._estaInterasadoResidenciaEnEdificio; }
            set { this._estaInterasadoResidenciaEnEdificio = value; }
        }

        public Int32? IdTelefonoPrimario
        {
            get { return this._idTelefonoPrimario; }
            set { this._idTelefonoPrimario = value; }
        }

        public Int32? IdDireccionPrimaria
        {
            get { return this._idDireccionPrimaria; }
            set { this._idDireccionPrimaria = value; }
        }

        public Int32? IdConfiguracionAplicacion
        {
            get { return this._idConfiguracionAplicacion; }
            set { this._idConfiguracionAplicacion = value; }
        }

        public Int32? IdConsulado
        {
            get { return this._idConsulado; }
            set { this._idConsulado = value; }
        }

        public String OtrasFuentes
        {
            get { return this._otrasFuentes; }
            set { this._otrasFuentes = value; }
        }

        public Int32? IdMaestria
        {
            get { return this._idMaestria; }
            set { this._idMaestria = value; }
        }

        public Int32? EstudioEnUP
        {
            get { return this._estudioEnUP; }
            set { this._estudioEnUP = value; }
        }

        public Int32? IdMaestriaAntUP
        {
            get { return this._idMaestriaAntUP; }
            set { this._idMaestriaAntUP = value; }
        }

        public Int32? AnhoMaestriaAntUP
        {
            get { return this._anhoMaestriaAntUP; }
            set { this._anhoMaestriaAntUP = value; }
        }

        public DireccionAplicanteBE Direccion
        {
            get { return this._direccion; }
            set { this._direccion = value; }
        }

        public TelefonoBE Telefono
        {
            get { return this._telefono; }
            set { this._telefono = value; }
        }

        public TelefonoBE Telefono1
        {
            get { return this._telefono1; }
            set { this._telefono1 = value; }
        }

        public TelefonoBE Celular
        {
            get { return this._celular; }
            set { this._celular = value; }
        }

        public TelefonoBE Celular1
        {
            get { return this._celular1; }
            set { this._celular1 = value; }
        }

        public List<EmpleadorBE> LEmpleador
        {
            get { return this._lEmpleador; }
            set { this._lEmpleador = value; }
        }

        public List<EducacionBE> LEducacion
        {
            get { return this._lEducacion; }
            set { this._lEducacion = value; }
        }

        public List<EducacionDetalleBE> LDetalleEducacion
        {
            get { return this._lDetalleEducacion; }
            set { this._lDetalleEducacion = value; }
        }

        public List<NotasBE> LNotas
        {
            get { return this._lNotas; }
            set { this._lNotas = value; }
        }
        public List<IdiomaBE> LIdioma
        {
            get { return this._lIdioma; }
            set { this._lIdioma = value; }
        }

        public List<ReferenciaBE> LReferencia
        {
            get { return this._lReferencia; }
            set { this._lReferencia = value; }
        }

        public String RedId
        {
            get { return this._redId; }
            set { this._redId = value; }
        }

        public String InstitucionNoUPAnt
        {
            get { return this._InstitucionNoUPAnt; }
            set { this._InstitucionNoUPAnt = value; }
        }

        public String GradoProgramaIB
        {
            get { return this._gradoProgramaIB; }
            set { this._gradoProgramaIB = value; }
        }

        public List<ActividadBE> LActividad
        {
            get { return this._lActividad; }
            set { this._lActividad = value; }
        }

        public List<RelacionBE> LRelacion
        {
            get { return this._lRelacion; }
            set { this._lRelacion = value; }
        }

        public Int32? ModalidadPostulacion
        {
            get { return this._modalidadPostulacion; }
            set { this._modalidadPostulacion = value; }
        }

        public List<RequisitoBE> LRequisito
        {
            get { return this._lRequisito; }
            set { this._lRequisito = value; }
        }

        public List<CalificacionBE> LCalificacion
        {
            get { return this._lCalificacion; }
            set { this._lCalificacion = value; }
        }

        public DataTable DtRequisito
        {
            get { return this._dtRequisito; }
            set { this._dtRequisito = value; }
        }

        public DataTable DtCalificacion
        {
            get { return this._dtCalificacion; }
            set { this._dtCalificacion = value; }
        }

        public String Tipos
        {
            get { return this._Tipos; }
            set { this._Tipos = value; }
        }

        public DateTime? Fecha
        {
            get { return this._Fecha; }
            set { this._Fecha = value; }
        }

        public Decimal? Puntaje
        {
            get { return this._Puntaje; }
            set { this._Puntaje = value; }
        }

        public String Comentario
        {
            get { return this._Comentario; }
            set { this._Comentario = value; }
        }

        public String People
        {
            get { return this._People; }
            set { this._People = value; }
        }

        public double CantidadPagada
        {
            get { return this._cantidadPagada; }
            set { this._cantidadPagada = value; }
        }

        public String NumeroBoletaPago
        {
            get { return this._numeroBoletaPago; }
            set { this._numeroBoletaPago = value; }
        }

        public DateTime? FechaEntrevista
        {
            get { return this._fechaEntrevista; }
            set { this._fechaEntrevista = value; }
        }

        public DateTime? FechaPresentacion
        {
            get { return this._fechaPresentacion; }
            set { this._fechaPresentacion = value; }
        }

        public EvaluacionBE Evaluacion
        {
            get { return this._evaluacion; }
            set { this._evaluacion = value; }
        }

        public String Order_by
        {
            get { return this._order_by; }
            set { this._order_by = value; }
        }

        public String De_forma
        {
            get { return this._de_forma; }
            set { this._de_forma = value; }
        }

        public LogAplicanteBE LogAplicante
        {
            get { return this._logAplicante; }
            set { this._logAplicante = value; }
        }

        public String Revision_Opid
        {
            get { return this._revision_opid; }
            set { this._revision_opid = value; }
        }

        public String Ape_Paterno
        {
            get { return this._ape_paterno; }
            set { this._ape_paterno = value; }
        }

        public String Ape_Materno
        {
            get { return this._ape_materno; }
            set { this._ape_materno = value; }
        }

        public String TipoDocumento
        {
            get { return this._tipodocumento; }
            set { this._tipodocumento = value; }
        }

        public Int32? ProgramOfStudy
        {
            get { return this._programofstudy; }
            set { this._programofstudy = value; }
        }

        public String Assign_Opid
        {
            get { return this._assign_opid; }
            set { this._assign_opid = value; }
        }

        public Boolean Pendiente
        {
            get { return this._pendiente; }
            set { this._pendiente = value; }
        }

        public Boolean isDiscapacitado
        {
            get { return this._isdiscapacitado; }
            set { this._isdiscapacitado = value; }
        }

        public String Discapacitado
        {
            get { return this._discapacitado; }
            set { this._discapacitado = value; }
        }

        public NotasBE Notas
        {
            get { return this._notas; }
            set { this._notas = value; }
        }

        public DataTable DtNotas
        {
            get { return this._dtNotas; }
            set { this._dtNotas = value; }
        }

        public Int32? SituacionAcademica
        {
            get { return this._situacionacademica; }
            set { this._situacionacademica = value; }
        }
        public Int32? IdCondicionAcademica
        {
            get { return this._idcondicionacademica; }
            set { this._idcondicionacademica = value; }
        }
        
        public String ApplicantEncrypt
        {
            get { return this._applicantencrypt; }
            set { this._applicantencrypt = value; }
        }

        public String Horario
        {
            get { return this._horario; }
            set { this._horario = value; }
        }

        public Int32? IdTipoPostulacion
        {
            get { return this._idtipopostulacion; }
            set { this._idtipopostulacion = value; }
        }

        public Int32? idAntecedentes
        {
            get { return this._idAntecedentes; }
            set { this._idAntecedentes = value; }
        }

        public Int32? Autorizacion
        {
            get { return this._idAutorizacion; }
            set { this._idAutorizacion = value; }
        }
        public Int32? AutorizacionTerceros
        {
            get { return this._idAutorizacionTerceros; }
            set { this._idAutorizacionTerceros = value; }
        }
        public List<InteresBE> LInteres
        {
            get { return this._lInteres; }
            set { this._lInteres = value; }
        }

        public String TipoUsuario
        {
            get { return this._TipoUsuario; }
            set { this._TipoUsuario = value; }
        }

        public String DireccionConcatenado
        {
            get { return this._strDireccionConcatenado; }
            set { this._strDireccionConcatenado = value; }
        }

        public String CodigoSap
        {
            get { return this._codigosap; }
            set { this._codigosap = value; }
        }

        public Int32? becaId
        {
            get { return this._becaId; }
            set { this._becaId = value; }
        }

        public Int32? TipoColeProvId
        {
            get { return this._TipoColeProvId; }
            set { this._TipoColeProvId = value; }
        }

        public Int32? sessionId
        {
            get { return this._sessionId; }
            set { this._sessionId = value; }
        }

        //Inicio JC.DelgadoV [Preformalización]
        public Int32? CambioCarrera
        {
            get { return this._CambioCarrera; }
            set { this._CambioCarrera = value; }
        }

        public Int32? SeguroRentaEstudiantil
        {
            get { return this._SeguroRentaEstudiantil; }
            set { this._SeguroRentaEstudiantil = value; }
        }

        public Int32? ReservaMatricula
        {
            get { return this._ReservaMatricula; }
            set { this._ReservaMatricula = value; }
        }

        public Int32? DeportistaDestacado
        {
            get { return this._DeportistaDestacado; }
            set { this._DeportistaDestacado = value; }
        }

        public List<ExamenFormalizacionBE> LExamenFormalizacion
        {
            get { return this._lExamenFormalizacion; }
            set { this._lExamenFormalizacion = value; }
        }

        public List<CursoConvalidacionFormalizacionBE> LCursoConvalidacionFormalizacion
        {
            get { return this._lCursoConvalidacionFormalizacion; }
            set { this._lCursoConvalidacionFormalizacion = value; }
        }

        //Fin JC.DelgadoV [Preformalización]

        //INICIO: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRC)
        public Int32? EncuestaRGC_ResP1
        {
            get { return this._EncuestaRGC_ResP1; }
            set { this._EncuestaRGC_ResP1 = value; }
        }

        public Int32? EncuestaRGC_ResP2
        {
            get { return this._EncuestaRGC_ResP2; }
            set { this._EncuestaRGC_ResP2 = value; }
        }

        public Int32? EncuestaRGC_ResP3
        {
            get { return this._EncuestaRGC_ResP3; }
            set { this._EncuestaRGC_ResP3 = value; }
        }

        public String EncuestaRGC_ResP4
        {
            get { return this._EncuestaRGC_ResP4; }
            set { this._EncuestaRGC_ResP4 = value; }
        }

        public Int32? EncuestaRGC_ResP5
        {
            get { return this._EncuestaRGC_ResP5; }
            set { this._EncuestaRGC_ResP5 = value; }
        }

        public Int32? EncuestaRGC_ResP6
        {
            get { return this._EncuestaRGC_ResP6; }
            set { this._EncuestaRGC_ResP6 = value; }
        }

        //FIN: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRC)

        //INICIO: JC.DelgadoV[RQ107362] Términos y Condiciones
        public bool AceptTermCond
        {
            get { return this._AceptTermCond; }
            set { this._AceptTermCond = value; }
        }

        public bool Mayor14ConsentimientoDatPer
        {
            get { return this._Mayor14ConsentimientoDatPer; }
            set { this._Mayor14ConsentimientoDatPer = value; }
        }

        public bool ApoderadoLegalTitularDatPer
        {
            get { return this._ApoderadoLegalTitularDatPer; }
            set { this._ApoderadoLegalTitularDatPer = value; }
        }
        //FIN: JC.DelgadoV[RQ107362] Términos y Condiciones

        public List<RendimientoAcademicoBE> ListaRendimientoAcademicoBE { get; set; } /*Se agrega:Christian Ramirez - REQ91569*/
        #endregion "Propiedades"
    }
}
