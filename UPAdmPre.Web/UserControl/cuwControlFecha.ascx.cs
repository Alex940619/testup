using System;
using System.Threading;
using System.Globalization;
using UPAdmPre.SL;

namespace UPAdmPre.WEB.usrctrl
{
    public partial class cuwControlFecha : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-PE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-PE");
        }

        public String ClientIdText
        {
            get
            {
                return this.txtTextoFecha.ClientID;
            }
        }

        public String ControlId
        {
            get
            {
                return this.txtTextoFecha.ClientID;
            }
        }

        public String TextCssClass
        {
            get
            {
                return this.txtTextoFecha.CssClass;
            }
            set
            {
                this.txtTextoFecha.CssClass = value;
            }
        }

        public System.Drawing.Color BackColor
        {
            get
            {
                return this.txtTextoFecha.BackColor;
            }
            set
            {
                this.txtTextoFecha.BackColor = value;
            }
        }

        public bool EsNulo
        {
            get
            {
                return (this.txtTextoFecha.Text.Length == 0);
            }
        }

        public bool Enabled
        {
            get
            {
                return !this.imgCalendario.Visible;
            }
            set
            {
                this.txtTextoFecha.Enabled = value;
                this.imgCalendario.Enabled = value;
                this.rfvFechaControl.Enabled = value;
                this.mevTextoFecha.Enabled = value;
                this.clxCalendario.Enabled = value;
                this.meeTextoFecha.Enabled = value;
            }
        }

        public String ErrorMessages
        {
            set
            {
                this.rfvFechaControl.ErrorMessage = value;
            }
        }

        public bool IsDateEmpty
        {
            get
            {
                if (String.IsNullOrEmpty(this.txtTextoFecha.Text))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public DateTime? SelectedDate
        {
            get
            {
                if (IsDate(this.txtTextoFecha.Text))
                {
                    DateTime dtmFecha = DateTime.ParseExact(this.txtTextoFecha.Text, UIConstantes.FormatoFechas._FormatoFechaConsulta, null);
                    return dtmFecha;
                }
                else
                {
                    return UIConvertNull.DateTime(this.txtTextoFecha.Text);
                }
            }
            set
            {
                this.txtTextoFecha.Text = value.Value.ToString(UIConstantes.FormatoFechas._FormatoFechaConsulta);
            }
        }

        public String ObtenerFecha
        {
            get
            {
                if (IsDate(this.txtTextoFecha.Text))
                {
                    String dtmFecha = DateTime.ParseExact(this.txtTextoFecha.Text, UIConstantes.FormatoFechas._FormatoFechaConsulta, null).ToString(UIConstantes.FormatoFechas._FormatoFechaConsulta);
                    return dtmFecha;
                }
                else
                {
                    return UIConstantes._valorCadenaVacia;
                }
            }
        }

        public String ValidationGroup
        {
            set
            {
                mevTextoFecha.ValidationGroup = value;
                this.rfvFechaControl.ValidationGroup = value;
            }
        }

        public String MinimunValue
        {
            set
            {
                mevTextoFecha.MinimumValue = value;
            }
        }

        public String MaximunValue
        {
            set
            {
                mevTextoFecha.MaximumValue = value;
            }
        }

        public bool IsValidEmpty
        {
            get
            {
                return mevTextoFecha.IsValidEmpty;
            }
            set
            {
                mevTextoFecha.IsValidEmpty = value;
            }
        }

        public String InValidValueMessage
        {
            set
            {
                mevTextoFecha.InvalidValueMessage = value;
                mevTextoFecha.InvalidValueBlurredMessage = "* " + value;
                mevTextoFecha.Text = "*";
            }
        }

        public void Limpiar()
        {
            this.txtTextoFecha.Text = UIConstantes._valorCadenaVacia;
        }

        public bool IsValidDate
        {
            get
            {
                if (IsDate(this.txtTextoFecha.Text))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool LimpiarFiltro
        {
            set
            {
                if (value == true)
                {
                    this.txtTextoFecha.Attributes.Add("limpiarFiltro", "1");
                }
                else
                {
                    this.txtTextoFecha.Attributes.Add("limpiarFiltro", "0");
                }
            }
        }

        public String MensajeErrorVacio
        {
            set
            {
                rfvFechaControl.ErrorMessage = value;
            }
        }

        public String AddDateScript()
        {
            String strID = this.txtTextoFecha.ClientID;
            return strID;
        }

        public static bool IsDate(Object oObject)
        {
            try
            {
                if (UIHelper.IsDate(oObject) == true)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        //public static bool IsDate(Object obj)
        //{
        //    String strDate = obj.ToString();
        //    try
        //    {
        //        DateTime dt = DateTime.Parse(strDate);
        //        if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}       

    }
}