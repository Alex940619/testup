using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAdmPre.SL;

namespace UPAdmPre.BE
{
   public class IdiomaBE
    {
       public IdiomaBE()
       { }

        #region "Atributos"

       private Int32? _ApplicationEducationId = null;
       private Int32? _idAplicacion = UIConstantes.idValorNulo;
       private Int32? _idIdioma = UIConstantes.idValorNulo;
       private String _nivelLectura = null;
       private String _nivelEscritura = null;
       private String _nivelHabla = null;
       private Int32? _isdeleted = UIConstantes.idValorNulo;
       private String _revision_opid = null;
       private Int32? _certificacionid = UIConstantes.idValorNulo;
       private String _otrosIdiomas = null;
       private String _otrasCertificaciones = null;
       private String _puntaje = null;

        #endregion "Atributos"

        #region "Propiedades"

       public Int32? IdApplicationEducation
       {
           get { return this._ApplicationEducationId; }
           set { this._ApplicationEducationId = value; }
       }
       public Int32? IdAplicacion
       {
           get { return this._idAplicacion; }
           set { this._idAplicacion = value; }
       }
       public Int32? IdIdioma
       {
           get { return this._idIdioma; }
           set { this._idIdioma = value; }
       }
       public String NivelLectura
       {
           get { return this._nivelLectura; }
           set { this._nivelLectura = value; }
       }
       public String NivelEscritura
       {
           get { return this._nivelEscritura; }
           set { this._nivelEscritura = value; }
       }

       public String NivelHabla
       {
           get { return this._nivelHabla; }
           set { this._nivelHabla = value; }
       }

       public Int32? IsDeleted
       {
           get { return this._isdeleted; }
           set { this._isdeleted = value; }
       }

       public String Revision_Opid
       {
           get { return this._revision_opid; }
           set { this._revision_opid = value; }
       }

       public Int32? CertificacionId
       {
           get { return this._certificacionid; }
           set { this._certificacionid = value; }
       }

       public String OtrosIdiomas
       {
           get { return this._otrosIdiomas; }
           set { this._otrosIdiomas = value; }
       }

       public String OtrasCertificaciones
       {
           get { return this._otrasCertificaciones; }
           set { this._otrasCertificaciones = value; }
       }

       public String Puntaje
       {
           get { return this._puntaje; }
           set { this._puntaje = value; }
       }

        #endregion "Propiedades"
    }
}
