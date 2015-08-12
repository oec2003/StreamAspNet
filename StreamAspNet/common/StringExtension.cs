using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace EntApp.Common.Extension
{
    public static class StringExtension
    {
        public static string ReplaceSql(this string msg)
        {
            if (msg != "" && msg != null)
            {
                msg = msg.Replace("'", "");
            }
            return msg;
        }

        public static string FormatWith(this string str, params object[] args)
        {
            return string.Format(str,args);
        }

        

        public static string Resource(this string value)
        {
            string Symbol = string.Empty;
            int enSymbolIndex = value.IndexOf(':');
            int cnSymbolIndex = value.IndexOf('：');
            if (enSymbolIndex != -1)
            {
                Symbol = value.Substring(enSymbolIndex, 1);
            }
            else if (cnSymbolIndex != -1)
            {
                Symbol = value.Substring(cnSymbolIndex, 1);
            }
            string resourceKey = value;
            if (!string.IsNullOrEmpty(Symbol))
            {
                resourceKey = value.Replace(Symbol, string.Empty); 
            }
            var resText = HttpContext.GetGlobalResourceObject("Resource", resourceKey.Trim());
            if (string.IsNullOrEmpty((string)resText))
            {
                //if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                //{
                //    System.IO.File.AppendAllText("d:\\resource.txt", resourceKey + Environment.NewLine);
                //}
            }
            value = !string.IsNullOrEmpty((string)resText) ? resText.ToString() : resourceKey;
            value += Symbol;
            return value;
        }

        #region 转换
        public static decimal ToDecimal(this string str, decimal defaultValue)
        {
            decimal res = defaultValue;
            if (decimal.TryParse(str, out res))
                return res;
            else
                return defaultValue;
        }

        public static int ToInt(this string str)
        {
            int res = 0;
            int.TryParse(str, out res);
            return res;
        }
        public static int ToInt(this string str, int defaultValue)
        {
            int res = defaultValue;
            if (int.TryParse(str, out res))
                return res;
            else
                return defaultValue;
        }
        public static int? ToIntOrNull(this string str)
        {
            int res = 0;
            if (int.TryParse(str, out res))
                return res;
            else
                return null;
        }
        public static bool ToBool(this string str)
        {
            bool res = false;
            bool.TryParse(str, out res);
            return res;
        }
        public static Guid? ToGuidOrNull(this string target)
        {
            Guid? result = null;

            try
            {
                result = new Guid(target);
                return result;
            }
            catch { }

            return result;
        }

        public static Guid ToGuid(this string target)
        {
            Guid result = Guid.Empty;
            
            Guid.TryParse(target, out result);
            return result;
        }
        public static DateTime? ToDateTimeOrNull(this string str)
        {
            DateTime res; 
            DateTime? res1 = null;
            return DateTime.TryParse(str, out res) ? res : res1;
        }

        public static DateTime ToDateTime(this string str,DateTime defaultValue)
        {
            DateTime res = defaultValue;
            if (DateTime.TryParse(str, out res))
            {
                return res;
            }
            else
            {
                return defaultValue;
            }
        }
        public static object ConvertToType(this string str, Type valType)
        {
            switch (valType.FullName)
            {
                case "System.Guid":
                    return new Guid(str);
                default:
                    return Convert.ChangeType(str, valType);
            }
        }
        #endregion

        #region Join
        /// <summary>
        /// Removes the last specified char.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="c">The c.</param>
        public static StringBuilder RemoveLastSpecifiedChar(this StringBuilder stringBuilder, char c)
        {
            if (stringBuilder.Length == 0)
                return stringBuilder;
            if (stringBuilder[stringBuilder.Length - 1] == c)
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            return stringBuilder;
        }

        /// <summary>
        /// Removes the last specified char.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="c">The c.</param>
        public static StringBuilder RemoveStartSpecifiedChar(this StringBuilder stringBuilder, char c)
        {
            if (stringBuilder.Length == 0)
                return stringBuilder;
            if (stringBuilder[0] == c)
            {
                stringBuilder.Remove(0, 1);
            }
            return stringBuilder;
        }

        /// <summary>
        /// Joins the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string JoinString<T>(this IEnumerable<T> enumerable, string separator)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in enumerable)
            {
                stringBuilder.AppendFormat("{0},", item.ToString());
            }
            stringBuilder.RemoveLastSpecifiedChar(',');
            return stringBuilder.ToString();
        }
        #endregion

        public static string RemoveLastCharacter(this string value, string lastCharacter)
        {
            if (value.EndsWith(lastCharacter))
            {
                value = value.Substring(0, value.Length - 1);
            }
            return value;
        }

        public static string RemoveStartCharacter(this string value, string startCharacter)
        {
            if (value.StartsWith(startCharacter))
            {
                value = value.Remove(0, 1);
            }
            return value;
        }
    }
}
