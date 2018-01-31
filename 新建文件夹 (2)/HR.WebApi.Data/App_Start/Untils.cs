using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Data
{
    public class Untils
    {
        #region Method 常见值转换
        public static int ConvertToInt(object content)
        {
            int result = 0;
            if (int.TryParse(ConvertToString(content), out result))
            {
                return result;
            }
            return result;
        }
        public static decimal ConvertToDecimal(object content)
        {
            decimal result = 0;
            if (decimal.TryParse(ConvertToString(content), out result))
            {
                return result;
            }
            return result;
        }
        public static float ConvertToFloat(object content)
        {
            float result = 0;
            if (float.TryParse(ConvertToString(content), out result))
            {
                return result;
            }
            return result;
        }
        public static string ConvertToString(object content)
        {
            if (content != null)
            {
                return content.ToString();
            }
            return string.Empty;
        }
        public static DateTime ConvertToDateTime(object time)
        {
            DateTime dt = new DateTime();
            if (DateTime.TryParse(ConvertToString(time), out dt))
            {
                return dt;
            }
            return dt;
        }

        /// <summary>
        /// 将数据库中timespan转换成十六进制字符串
        /// </summary>
        /// <param name="objTs">从数据库中获取的timespan值</param>      
        /// <returns>timespan十六进制字符串</returns>
        public static string ConvertToTimeSpanString(object objTs)
        {
            byte[] btTsArray = objTs as byte[];
            string strTimeSpan = "0x" + BitConverter.ToString(btTsArray).Replace("-", "");
            return strTimeSpan;
        }

        // 把字节型转换成十六进制字符串  
        public static string ByteToString(byte[] InBytes)
        {
            string StringOut = "";
            foreach (byte InByte in InBytes)
            {
                StringOut = StringOut + String.Format("{0:X2} ", InByte);
            }
            return StringOut;
        }
        public string ByteToString(byte[] InBytes, int len)
        {
            string StringOut = "";
            for (int i = 0; i < len; i++)
            {
                StringOut = StringOut + String.Format("{0:X2} ", InBytes[i]);
            }
            return StringOut;
        }
        // 把十六进制字符串转换成字节型  
        public static byte[] StringToByte(string InString)
        {
            string[] ByteStrings;
            ByteStrings = InString.Split(" ".ToCharArray());
            byte[] ByteOut;
            ByteOut = new byte[ByteStrings.Length - 1];
            for (int i = 0; i == ByteStrings.Length - 1; i++)
            {
                ByteOut[i] = Convert.ToByte(("0x" + ByteStrings[i]));
            }
            return ByteOut;
        }

        #endregion
    }
}