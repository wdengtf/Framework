using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using System.Web.Security;


namespace Framework.Utils
{
    public class SignUtil
    {
        private readonly static string myKey = "q0m3sd8l";

        #region 加密方法
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="pToEncrypt">需要加密字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string pToEncrypt)
        {
            try
            {
                if (pToEncrypt.Length > 0)
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    //把字符串放到byte数组中


                    //原来使用的UTF8编码，我改成Unicode编码了，不行
                    byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

                    //建立加密对象的密钥和偏移量


                    //使得输入密码必须输入英文文本
                    des.Key = ASCIIEncoding.ASCII.GetBytes(myKey);
                    des.IV = ASCIIEncoding.ASCII.GetBytes(myKey);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    StringBuilder ret = new StringBuilder();
                    foreach (byte b in ms.ToArray())
                    {
                        ret.AppendFormat("{0:X2}", b);
                    }
                    ret.ToString();
                    return ret.ToString();
                }
            }
            catch
            {
                //JS.Alert("写入配置信息失败，详细信息：" + ex.Message.Replace("\r\n", "").Replace("'", ""));
            }

            return "";
        }
        #endregion

        #region 解密方法
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="pToDecrypt">需要解密的字符串</param>
        /// <param name="sKey">密匙</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string pToDecrypt)
        {
            try
            {
                if (pToDecrypt.Length > 0)
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                    for (int x = 0; x < pToDecrypt.Length / 2; x++)
                    {
                        int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                        inputByteArray[x] = (byte)i;
                    }

                    //建立加密对象的密钥和偏移量，此值重要，不能修改
                    des.Key = ASCIIEncoding.ASCII.GetBytes(myKey);
                    des.IV = ASCIIEncoding.ASCII.GetBytes(myKey);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
                    StringBuilder ret = new StringBuilder();
                    return System.Text.Encoding.Default.GetString(ms.ToArray());
                }
            }
            catch
            {
                //JS.Alert("读取配置信息失败，详细信息：" + ex.Message.Replace("\r\n", "").Replace("'", ""));
            }
            return "";
        }
        #endregion

        /// <summary>
        /// 签名检测
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userKey"></param>
        /// <returns></returns>
        public static String encode(String userName, String userKey)
        {
            List<string> list = new List<string>() { userName, userKey };
            list.Sort();
            string tempstr = string.Join("", list);

            return MD5Hash(tempstr).ToUpper();
        }


        public static string GetSign(string secret_key,List<string> vals)
        {

            List<String> list = new List<string>();
            list.Add(MD5Hash(secret_key).ToLower());
            foreach (String val in vals)
            {
                if (!string.IsNullOrEmpty(val))
                {
                   list.Add(MD5Hash(val).ToLower());
                }
            }
            return  MD5Hash(string.Join("", list)).ToUpper();
        }

        /// <summary>
        /// 生成加密字符串 转小写
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string CreateSignStr(List<string> list)
        {
            list.Sort();
            string tempstr = string.Join("", list);//转换成string
            tempstr = MD5Hash(tempstr);//MD5加密
            tempstr = tempstr.ToLower(); //转小写
            return tempstr;
        }

        /// <summary>
        /// 生成加密字符串 MD5转大写
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string CreateSignStrUpper(List<string> list)
        {
            list.Sort();
            string tempstr = string.Join("", list);//转换成string
            tempstr = MD5Hash(tempstr);//MD5加密
            tempstr = tempstr.ToUpper(); //转大写
            return tempstr;
        }

        /// <summary>
        /// 创建签名 将参数和值一起签名
        /// </summary>
        /// <param name="param"></param>
        /// <param name="DC_Key"></param>
        /// <returns></returns>
        public static string CreateSign(IDictionary<String, String> param, string DC_Key)
        {
            List<string> _list = new List<string>();
            foreach (var item in param)
            {
                _list.Add(item.Key + "=" + item.Value);
            }
            _list.Add("key=" + DC_Key);
            _list.Sort();


            return MD5Hash(string.Join("", _list)).ToLower();
        }


        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <returns></returns>
        public static string MD5Hash(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            return sBuilder.ToString();
        }
    }
}
