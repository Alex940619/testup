using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPAdmPre.BE
{
    public class AvailableFileType
    {
        #region "Constructor"

        public AvailableFileType()
        { }

        public AvailableFileType(Int32 mediaTypeId, String mediaTypeIcon, String fileExtension, String description)
        {
            this._mediaTypeId = mediaTypeId;
            this._mediaTypeIcon = mediaTypeIcon;
            this._fileExtension = fileExtension;
            this._description = description;
        }

        #endregion "Constructor"

        #region "Atributos"

        private Int32 _mediaTypeId;
        private String _mediaTypeIcon;
        private String _fileExtension;
        private String _description;

        #endregion "Atributos"

        #region "Propiedades"

        public int MediaTypeId
        {
            get { return this._mediaTypeId; }
            set { this._mediaTypeId = value; }
        }
        public string MediaTypeIcon
        {
            get { return this._mediaTypeIcon; }
            set { this._mediaTypeIcon = value; }
        }
        public string FileExtension
        {
            get { return this._fileExtension; }
            set { this._fileExtension = value; }
        }
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        #endregion "Propiedades"
    }
}
