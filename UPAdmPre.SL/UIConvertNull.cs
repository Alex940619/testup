using System;
using System.Text;
using System.Data;
using System.Globalization;

namespace UPAdmPre.SL
{
    public sealed class UIConvertNull
    {
        public static Int16? Int16(Object value)
        {
            if ((value == DBNull.Value) || (value == null))
            {
                return null;
            }
            else
            {
                return Convert.ToInt16(value);
            }
        }

        public static Int16? Int16(String value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return Convert.ToInt16(value);
            }
        }

        public static Int32? Int32(Object value)
        {
            if (Convert.IsDBNull(value) || (value == null))
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        public static Int32? Int32(String value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        public static Decimal? Decimal(Object value)
        {
            if (Convert.IsDBNull(value) || (value == null))
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(value);
            }
        }

        public static Double? Double(Object value)
        {
            if (Convert.IsDBNull(value) || (value == null))
            {
                return null;
            }
            else
            {
                return Convert.ToDouble(value);
            }
        }

        public static String String(String value)
        {
            if (Convert.IsDBNull(value) || (string.IsNullOrEmpty(value)))
            {
                return null;
            }
            else
            {
                return Convert.ToString(value).Trim();
            }
        }

        public static String String(Object value)
        {
            if (Convert.IsDBNull(value) || (value == null))
            {
                return null;
            }
            else
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)) == true)
                {
                    return null;
                }
                return Convert.ToString(value).Trim();
            }
        }

        public static DateTime? DateTime(Object value)
        {
            if (Convert.IsDBNull(value) || (value == null) || (value.ToString().Length == 0))
            {
                return null;
            }
            else
            {
                CultureInfo cInfo = new CultureInfo("fr-FR");
                return Convert.ToDateTime(value, cInfo);
            }
        }

        public static DateTime? DateTime(String value, String format)
        {
            if (Convert.IsDBNull(value) || (string.IsNullOrEmpty(value)) || (value.Trim().Length == 0))
            {
                return null;
            }
            else
            {
                return System.DateTime.ParseExact(value, format, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        public static Byte? Byte(Object value)
        {
            if ((value == DBNull.Value) || (value == null))
            {
                return null;
            }
            else
            {
                return Convert.ToByte(value);
            }
        }

        public static Boolean? Boolean(Object value)
        {
            if ((value == DBNull.Value) || (value == null))
            {
                return null;
            }
            else
            {
                return Convert.ToBoolean(value);
            }
        }

        private static void ValidarArgumentNullException(DataRow oDataRow)
        {
            if (oDataRow == null)
            {
                throw new ArgumentNullException("oDataRow", "Objeto DataRow es Null, no esta permitido");
            }
        }

        public static String String(DataRow oDataRow, String NombreColumna)
        {
            ValidarArgumentNullException(oDataRow);
            String value = String(oDataRow[NombreColumna]);
            return value;
        }

        public static Int32? Int32(DataRow oDataRow, String NombreColumna)
        {
            ValidarArgumentNullException(oDataRow);
            Int32? value = Int32(oDataRow[NombreColumna]);
            return value;
        }

        public static Int16? Int16(DataRow oDataRow, String NombreColumna)
        {
            ValidarArgumentNullException(oDataRow);
            Int16? value = Int16(oDataRow[NombreColumna]);
            return value;
        }

        public static DateTime? DateTime(DataRow oDataRow, String NombreColumna)
        {
            ValidarArgumentNullException(oDataRow);
            DateTime? value = DateTime(oDataRow[NombreColumna]);
            return value;
        }

        public static Decimal? Decimal(DataRow oDataRow, String NombreColumna)
        {
            ValidarArgumentNullException(oDataRow);
            Decimal? value = Decimal(oDataRow[NombreColumna]);
            return value;
        }

        public static Double? Double(DataRow oDataRow, String NombreColumna)
        {
            ValidarArgumentNullException(oDataRow);
            Double? value = Double(oDataRow[NombreColumna]);
            return value;
        }

        public static Boolean? Boolean(DataRow oDataRow, String NombreColumna)
        {
            ValidarArgumentNullException(oDataRow);
            Boolean? value = Boolean(oDataRow[NombreColumna]);
            return value;
        }

    }
}
