using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace System
{
    /// <summary>
    /// 常用加密解密方法整理
    /// </summary>
    public class SecurityHelper
    {
        #region Url加密解密 ,HTML 加密解密
        /// <summary>
        /// URL 加密
        /// </summary>
        /// <param name="content">url地址</param>
        /// <returns></returns>
        public static string URLEncode(string content)
        {
            return HttpUtility.UrlEncode(content);
        }
        /// <summary>
        /// URL 解密
        /// </summary>
        /// <param name="content">url解密</param>
        /// <returns></returns>
        public static string URLDecode(string content)
        {
            return HttpUtility.UrlDecode(content);
        }
        /// <summary>
        /// HTML 编码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string HTMLEncode(string content)
        {
            return HttpUtility.HtmlEncode(content);
        }
        /// <summary>
        /// HTML 解码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string HTMLDecode(string content)
        {
            return HttpUtility.HtmlDecode(content);
        }
        #endregion

        #region Base64位加密解密
        /// <summary>
        /// 将字符串转换成base64格式,使用UTF8字符集
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <returns></returns>
        public static string Base64Encode(string content)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 将base64格式，转换utf8
        /// </summary>
        /// <param name="content">解密内容</param>
        /// <returns></returns>
        public static string Base64Decode(string content)
        {
            byte[] bytes = Convert.FromBase64String(content);
            return Encoding.UTF8.GetString(bytes);
        }
        #endregion

        #region Md5 加密
        /// <summary>
        /// MD5加密字符串（32位大写）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //将字符串转换成字节
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            //加密处理
            bytes = md5.ComputeHash(bytes);
            //转换成16位字符
            string result = BitConverter.ToString(bytes);
            return result.Replace("-", "");
        }
        /// <summary>
        /// MD5加密字符串（16位大写）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5_16(string source)
        {
            return MD5(source).Substring(8, 16);
        }
        #endregion

        #region AES加密解密 
        /// <summary>
        /// 128位处理key 
        /// </summary>
        /// <param name="keyArray">原字节</param>
        /// <param name="key">处理key</param>
        /// <returns></returns>
        private static byte[] GetAesKey(byte[] keyArray, string key)
        {
            byte[] newArray = new byte[16];
            if (keyArray.Length < 16)
            {
                for (int i = 0; i < newArray.Length; i++)
                {
                    if (i >= keyArray.Length)
                    {
                        newArray[i] = 0;
                    }
                    else
                    {
                        newArray[i] = keyArray[i];
                    }
                }
            }
            return newArray;
        }
        /// <summary>
        /// 使用AES加密字符串,按128位处理key
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <param name="key">秘钥，需要128位、256位.....</param>
        /// <returns>Base64字符串结果</returns>
        public static string AESEncrypt(string content, string key = HexKey, bool autoHandle = true)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            if (autoHandle)
            {
                keyArray = GetAesKey(keyArray, key);
            }
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(content);

            SymmetricAlgorithm des = Aes.Create();
            des.Key = keyArray;
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = des.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray);
        }
        /// <summary>
        /// 使用AES解密字符串,按128位处理key
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="key">秘钥，需要128位、256位.....</param>
        /// <returns>UTF8解密结果</returns>
        public static string AESDecrypt(string content, string key = HexKey, bool autoHandle = true)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            if (autoHandle)
            {
                keyArray = GetAesKey(keyArray, key);
            }
            byte[] toEncryptArray = Convert.FromBase64String(content);

            SymmetricAlgorithm des = Aes.Create();
            des.Key = keyArray;
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = des.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
        #endregion

        #region DES 加密解密
        /// <summary>
        /// 常量key定义
        /// </summary>
        const string HexKey = "Key123Ace#321ABC";

        //默认密钥向量 
        private static byte[] Keys = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        /// <summary> 
        /// DES加密字符串 
        /// </summary> 
        /// <param name="encryptString">待加密的字符串</param> 
        /// <param name="encryptKey">加密密钥,要求为16位</param> 
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns> 
        public static string DESEncrypt(string encryptString, string encryptKey = HexKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 16));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message + encryptString;
            }

        }
        /// <summary> 
        /// DES解密字符串 
        /// </summary> 
        /// <param name="decryptString">待解密的字符串</param> 
        /// <param name="decryptKey">解密密钥,要求为16位,和加密密钥相同</param> 
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns> 
        public static string DESDecrypt(string decryptString, string decryptKey = HexKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 16));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                Byte[] inputByteArrays = new byte[inputByteArray.Length];
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message + decryptString;
            }
        }
        #endregion
    }
}
