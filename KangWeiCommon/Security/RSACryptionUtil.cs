using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace YiLianHongKang.Common.Security
{
    /// <summary> 
    /// RSA���ܽ��ܼ�RSAǩ������֤
    /// </summary> 
    public class RSAUtil
    {
        #region RSA ����Կ���� 

        /// <summary>
        /// RSA ����Կ���� ����˽Կ �͹�Կ 
        /// </summary>
        /// <param name="xmlKeys"></param>
        /// <param name="xmlPublicKey"></param>
        public static void CreateRSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
        #endregion

        #region RSA�ļ��ܺ��� 
        //############################################################################## 
        //RSA ��ʽ���� 
        //˵��KEY������XML����ʽ,���ص����ַ��� 
        //����һ����Ҫ˵�������ü��ܷ�ʽ�� ���� ���Ƶģ��� 
        //############################################################################## 

        /// <summary>
        /// RSA����
        /// </summary>
        /// <param name="xmlPublicKey">��Կ</param>
        /// <param name="publicStr">����</param>
        /// <returns></returns>
        public static string Encrypt(string xmlPublicKey, string publicStr)
        {
            byte[] data;
            byte[] CypherTextBArray;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            data = Encoding.UTF8.GetBytes(publicStr);
            int maxBlockSize = rsa.KeySize / 8 - 11; //���ܿ���󳤶�����
            if (data.Length <= maxBlockSize)
            {
                CypherTextBArray = rsa.Encrypt(data, false);
                return Convert.ToBase64String(CypherTextBArray);
            }
            MemoryStream plaiStream = new MemoryStream(data);
            MemoryStream crypStream = new MemoryStream();
            Byte[] buffer = new Byte[maxBlockSize];
            int blockSize = plaiStream.Read(buffer, 0, maxBlockSize);
            while (blockSize > 0)
            {
                Byte[] toEncrypt = new Byte[blockSize];
                Array.Copy(buffer, 0, toEncrypt, 0, blockSize);
                Byte[] cryptograph = rsa.Encrypt(toEncrypt, false);
                crypStream.Write(cryptograph, 0, cryptograph.Length);
                blockSize = plaiStream.Read(buffer, 0, maxBlockSize);
            }
            string x= Convert.ToBase64String(crypStream.ToArray(), Base64FormattingOptions.None);
            plaiStream.Close();
            crypStream.Close();
            return x;
        }
        ///// <summary>
        ///// RSA����
        ///// </summary>
        ///// <param name="xmlPublicKey">��Կ</param>
        ///// <param name="EncryptString">��������byte����</param>
        ///// <returns></returns>
        //public static string Encrypt(string xmlPublicKey, byte[] EncryptString)
        //{

        //    byte[] CypherTextBArray;
        //    string Result;
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //    rsa.FromXmlString(xmlPublicKey);
        //    CypherTextBArray = rsa.Encrypt(EncryptString, false);
        //    Result = Convert.ToBase64String(CypherTextBArray);
        //    return Result;

        //}
        #endregion

        #region RSA�Ľ��ܺ��� 
        /// <summary>
        /// RSA����
        /// </summary>
        /// <param name="xmlPrivateKey">˽Կ</param>
        /// <param name="m_strDecryptString">����</param>
        /// <returns></returns>
        public static string Decrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            byte[] data;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            data = Convert.FromBase64String(m_strDecryptString);
            int maxBlockSize = rsa.KeySize / 8; //���ܿ���󳤶�����
            if (data.Length <= maxBlockSize)
            {
                byte[] cipherbytes = rsa.Decrypt(data, false);
                return Encoding.UTF8.GetString(cipherbytes);
            }
            MemoryStream crypStream = new MemoryStream(data);
            MemoryStream plaiStream = new MemoryStream();
            Byte[] buffer = new Byte[maxBlockSize];
            int blockSize = crypStream.Read(buffer, 0, maxBlockSize);
            while (blockSize > 0)
            {
                Byte[] toDecrypt = new Byte[blockSize];
                Array.Copy(buffer, 0, toDecrypt, 0, blockSize);
                Byte[] cryptograph = rsa.Decrypt(toDecrypt, false);
                plaiStream.Write(cryptograph, 0, cryptograph.Length);
                blockSize = crypStream.Read(buffer, 0, maxBlockSize);
            }
            string x= Encoding.UTF8.GetString(plaiStream.ToArray());
            plaiStream.Close();
            crypStream.Close();
            return x;
        }

        ///// <summary>
        ///// RSA�Ľ��ܺ���
        ///// </summary>
        ///// <param name="xmlPrivateKey">˽Կ</param>
        ///// <param name="DecryptString">���ܺ���ֽ�����</param>
        ///// <returns></returns>
        //public static string Decrypt(string xmlPrivateKey, byte[] DecryptString)
        //{
        //    byte[] DypherTextBArray;
        //    string Result;
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //    rsa.FromXmlString(xmlPrivateKey);
        //    DypherTextBArray = rsa.Decrypt(DecryptString, false);
        //    Result = Encoding.UTF8.GetString(DypherTextBArray);
        //    return Result;

        //}
        #endregion

        #region RSA����ǩ�� 

        #region ��ȡHash������ 
        //��ȡHash������ 
        public static bool GetHash(string m_strSource, ref byte[] HashData)
        {
            //���ַ�����ȡ��Hash���� 
            byte[] Buffer;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            HashData = MD5.ComputeHash(Buffer);

            return true;
        }

        //��ȡHash������ 
        public static bool GetHash(string m_strSource, ref string strHashData)
        {

            //���ַ�����ȡ��Hash���� 
            byte[] Buffer;
            byte[] HashData;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            HashData = MD5.ComputeHash(Buffer);

            strHashData = Convert.ToBase64String(HashData);
            return true;

        }

        //��ȡHash������ 
        public static bool GetHash(System.IO.FileStream objFile, ref byte[] HashData)
        {

            //���ļ���ȡ��Hash���� 
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            HashData = MD5.ComputeHash(objFile);
            objFile.Close();

            return true;

        }

        //��ȡHash������ 
        public static bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {

            //���ļ���ȡ��Hash���� 
            byte[] HashData;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            HashData = MD5.ComputeHash(objFile);
            objFile.Close();

            strHashData = Convert.ToBase64String(HashData);

            return true;

        }
        #endregion

        #region RSAǩ�� 

        /// <summary>
        /// RSAǩ��
        /// </summary>
        /// <param name="p_strKeyPrivate"></param>
        /// <param name="HashbyteSignature"></param>
        /// <param name="EncryptedSignatureData"></param>
        /// <returns></returns>
        public static bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //����ǩ�����㷨ΪMD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            return true;

        }

        //RSAǩ�� 
        public static bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref string m_strEncryptedSignatureData)
        {

            byte[] EncryptedSignatureData;

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //����ǩ�����㷨ΪMD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);

            return true;

        }

        //RSAǩ�� 
        public static bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {

            byte[] HashbyteSignature;

            HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //����ǩ�����㷨ΪMD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            return true;

        }

        //RSAǩ�� 
        public static bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref string m_strEncryptedSignatureData)
        {

            byte[] HashbyteSignature;
            byte[] EncryptedSignatureData;

            HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //����ǩ�����㷨ΪMD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);

            return true;

        }
        #endregion

        #region RSA ǩ����֤ 

        public static bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, byte[] DeformatterData)
        {

            byte[] HashbyteDeformatter;

            HashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter);

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, string p_strDeformatterData)
        {

            byte[] DeformatterData;

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            DeformatterData = Convert.FromBase64String(p_strDeformatterData);

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {

            byte[] DeformatterData;
            byte[] HashbyteDeformatter;

            HashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            DeformatterData = Convert.FromBase64String(p_strDeformatterData);

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        #endregion


        #endregion
    }
}
