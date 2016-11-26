using System;
using System.Collections.Generic;
using System.Web;
using Framework.Log;

namespace Framework.Cookies
{
    /// <summary>
    /// Cookies Session处理
    /// </summary>
    public class CookieHandle
    {
        /// <summary>
        /// //单位小时
        /// </summary>
        private int DefaultExpiresTime = 24;
        /// <summary>
        /// Cookies名称
        /// </summary>
        public string strCookiesName { get; set; }
        /// <summary>
        /// Cookies值
        /// </summary>
        public string strCookiesValue { get; set; }

        /// <summary>
        /// Cookies值
        /// </summary>
        public Dictionary<string, string> strCookiesParm { get; set; }

        public int ExpiresTime{ get; set; } //单位小时

        /// <summary>
        /// 构造函数
        /// </summary>
        public CookieHandle() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_strCookiesName"></param>
        public CookieHandle(string _strCookiesName) {
            this.strCookiesName = _strCookiesName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_strCookiesName">Cookies名称</param>
        /// <param name="_strCookiesValue">Cookies值</param>
        public CookieHandle(string _strCookiesName, string _strCookiesValue)
        {
            this.strCookiesName = _strCookiesName;
            this.strCookiesValue = _strCookiesValue;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_strCookiesName">Cookies名称</param>
        /// <param name="_strCookiesValue">Cookies值</param>
        /// <param name="_ExpiresTime">过期时间（小时）</param>
        public CookieHandle(string _strCookiesName,string _strCookiesValue, int _ExpiresTime)
        {
            this.strCookiesName = _strCookiesName;
            this.strCookiesValue = _strCookiesValue;
            this.ExpiresTime = _ExpiresTime;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_strCookiesName">Cookies名称</param>
        /// <param name="_strCookiesParm">Cookies值[名称/值]</param>
        public CookieHandle(string _strCookiesName, Dictionary<string, string> _strCookiesParm)
        {
            this.strCookiesName = _strCookiesName;
            this.strCookiesParm = _strCookiesParm;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_strCookiesName">Cookies名称</param>
        /// <param name="_strCookiesParm">Cookies值[名称/值]</param>
        /// <param name="_ExpiresTime">过期时间（小时）</param>
        public CookieHandle(string _strCookiesName, Dictionary<string, string> _strCookiesParm, int _ExpiresTime)
        {
            this.strCookiesName = _strCookiesName;
            this.strCookiesParm = _strCookiesParm;
            this.ExpiresTime = _ExpiresTime;
        }


        #region 设置Cookies操作
        /// <summary>
        /// 设置Cookies
        /// </summary>
        /// <returns></returns>
        public bool SetCookies()
        {
            bool flag = false;
            try
            {
                if (!String.IsNullOrEmpty(strCookiesName) && !String.IsNullOrEmpty(strCookiesValue))
                {
                    flag = SetCookiesValue(strCookiesName, strCookiesValue);
                }
                if (!String.IsNullOrEmpty(strCookiesName) && (strCookiesParm != null && strCookiesParm.Count >0))
                {
                    flag = SetCookiesKeyValue(strCookiesName, strCookiesParm);
                }
            }
            catch (Exception ex)
            {
                Log.LogService.logDebug(ex);
            }
            return flag;
        }

        /// <summary>
        /// 设置Cookies
        /// </summary>
        /// <param name="CookiesName">Cookies名称</param>
        /// <param name="CookiesValue">Cookies值</param>
        /// <returns></returns>
        private bool SetCookiesValue(string CookiesName, string CookiesValue)
        {
            bool flag = false;
            try
            {
                //过滤特殊字符
                CookiesValue = CookiesValue.Replace("\r", "").Replace("\n", "").Replace("%0d%0a", "");
                if (String.IsNullOrEmpty(CookiesName) || String.IsNullOrEmpty(CookiesValue))
                    return false;

                if (this.ExpiresTime < 1)
                    this.ExpiresTime = DefaultExpiresTime;

                CookUtils.AddCookie(CookiesName, CookiesValue, DateTime.Now.AddHours(ExpiresTime));
                flag = true;
            }
            catch (Exception ex)
            {
                Log.LogService.logDebug(ex);
            }
            return flag;
        }

        /// <summary>
        /// 设置Cookies
        /// </summary>
        /// <param name="CookiesName">Cookies名称</param>
        /// <param name="_strCookiesParm">Cookies键值</param>
        /// <returns></returns>
        private bool SetCookiesKeyValue(string CookiesName, Dictionary<string, string> CookiesParm)
        {
            bool flag = false;
            try
            {
                if (String.IsNullOrEmpty(CookiesName) || CookiesParm == null || CookiesParm.Count < 1)
                    return false;

                if (this.ExpiresTime < 1)
                    this.ExpiresTime = DefaultExpiresTime;

                CookUtils.AddCookie(CookiesName, CookiesParm, DateTime.Now.AddHours(ExpiresTime));
                flag = true;
            }
            catch (Exception ex)
            {
                LogService.logDebug(ex);
            }
            return flag;
        }

        /// <summary>
        /// 获取Cookies
        /// </summary>
        /// <param name="CookiesName">Cookies名称</param>
        /// <param name="CookiesKeyName">Cookies子名称</param>
        /// <returns></returns>
        public string GetCookies(string CookiesName, string CookiesKeyName = null)
        {
            string strCookValue = "";
            try
            {
                if (!String.IsNullOrEmpty(CookiesName) && !String.IsNullOrEmpty(CookiesKeyName))
                    return CookUtils.GetCookieValue(CookiesName, CookiesKeyName);
                else if (!String.IsNullOrEmpty(CookiesName))
                    return CookUtils.GetCookieValue(CookiesName);
            }
            catch (Exception ex)
            {
                Log.LogService.logDebug(ex);
            }
            return strCookValue;
        }

        public bool RemoveCookies()
        {
            bool flag = false;
            try
            {
                //删除所有的Cookies数据
                int limit = System.Web.HttpContext.Current.Request.Cookies.Count;
                string cookieName = "";
                for (int i = 0; i < limit; i++)
                {
                    cookieName = System.Web.HttpContext.Current.Request.Cookies[i].Name;
                    if (cookieName == "rebate_user" || cookieName == "YmCps_user")
                        continue;

                    CookUtils.RemoveCookie(cookieName);
                }
                flag = true;
            }
            catch (Exception ex)
            {
                LogService.logDebug(ex);
            }
            return flag;
        }
        #endregion

        #region Session操作
        /// <summary>
        /// 保存Session数据
        /// </summary>
        /// <param name="saveName">名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SetSession(string saveName, string value)
        {
            bool flage = false;
            try
            {
                System.Web.HttpContext.Current.Session[saveName] = value;
                flage = true;
            }
            catch (Exception ex)
            {
                LogService.logDebug(ex);
            }
            return flage;
        }
        /// <summary>
        /// 取 Session数据
        /// </summary>
        /// <param name="saveName">名称</param>
        /// <returns></returns>
        public string GetSession(string saveName)
        {
            try
            {
                if (System.Web.HttpContext.Current.Session[saveName] != null)
                {
                    return System.Web.HttpContext.Current.Session[saveName].ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.logDebug(ex);
                return "";
            }
            return "";
        }

        /// <summary>
        /// 删除Session
        /// </summary>
        /// <param name="SessionName">Session名称</param>
        /// <returns></returns>
        public bool RemoveSession(string SessionName)
        {
            bool flage = false;
            try
            {
                if (System.Web.HttpContext.Current.Session[SessionName] != null)
                {
                    System.Web.HttpContext.Current.Session.Remove(SessionName);
                    flage = true;
                }
            }
            catch (Exception ex)
            {
                LogService.logDebug(ex);
            }
            return flage;
        }

        #endregion
    }
}
