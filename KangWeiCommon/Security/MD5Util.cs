using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KangWeiCommon.Security
{
    /// <summary>
    /// MD5单向散列加密操作类
    /// </summary>
    public  class MD5Util
    {
        /// <summary>
        /// 获取md5加密字符串
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="salt">在尾部添加随机字符串</param>
        /// <returns></returns>
        public static string GetMd5String(string str, string salt = "")
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = null;
            buffer = Encoding.UTF8.GetBytes(str + salt);
            byte[] newBuffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in newBuffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static String GetStreamMD5(Stream stream)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;
            MD5CryptoServiceProvider oMD5Hasher =new MD5CryptoServiceProvider();
            arrbytHashValue = oMD5Hasher.ComputeHash(stream); //计算指定Stream 对象的哈希值
            //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
            strHashData = System.BitConverter.ToString(arrbytHashValue);
            //替换-
            strHashData = strHashData.Replace("-", "");
            strResult = strHashData;
            return strResult;
        }
    }
}
