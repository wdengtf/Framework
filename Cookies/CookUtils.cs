using System;
using System.Collections.Generic;
using System.Web;

namespace Framework.Cookies
{
    /// <summary>
    /// Cookie帮助类
    /// </summary>
    public class CookUtils
    {
        #region 添加Cookie

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="key">名称</param>
        /// <param name="value">值</param>
        public static void AddCookie(string key, string value)
        {
            AddCookie(new System.Web.HttpCookie(key, value));
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="key">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期日期</param>
        public static void AddCookie(string key, string value, DateTime expires)
        {
            HttpCookie cookie = new System.Web.HttpCookie(key, value);
            cookie.Expires = expires;
            AddCookie(cookie);
        }

        /// <summary>
        /// 添加为Cookie.Values集合
        /// </summary>
        /// <param name="cookieName">集合名称</param>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        public static void AddCookie(string cookieName, string key, string value)
        {
            HttpCookie cookie = new System.Web.HttpCookie(cookieName);
            cookie.Values.Add(key, value);
            AddCookie(cookie);
        }

        /// <summary>
        /// 添加为Cookie集合
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="expires">过期时间</param>
        public static void AddCookie(string cookieName, DateTime expires)
        {
            HttpCookie cookie = new System.Web.HttpCookie(cookieName);
            cookie.Expires = expires;
            AddCookie(cookie);
        }


        /// <summary>
        /// 添加为Cookie.Values集合
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        public static void AddCookie(string cookieName, string key, string value, DateTime expires)
        {
            HttpCookie cookie = new System.Web.HttpCookie(cookieName);
            cookie.Expires = expires;
            cookie.Values.Add(key, value);
            AddCookie(cookie);
        }

        /// <summary>
        /// 添加为Cookie.Values集合
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="parm">参数</param>
        /// <param name="expires">过期时间</param>
        public static void AddCookie(string cookieName, IDictionary<string, string> parm, DateTime expires)
        {
            if (parm == null || parm.Count < 1)
                return;

            HttpCookie cookie = new System.Web.HttpCookie(cookieName);
            cookie.Expires = expires;

            foreach (KeyValuePair<string, string> kv in parm)
                cookie.Values.Add(kv.Key, kv.Value);

            AddCookie(cookie);
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="cookie"></param>
        private static void AddCookie(HttpCookie cookie)
        {
            HttpResponse response = System.Web.HttpContext.Current.Response;
            if (response != null)
            {
                //指定客户端脚本是否可以访问[默认为false]
                cookie.HttpOnly = false;
                //指定统一的Path，比便能通存通取
                cookie.Path = "/";
                //设置跨域,这样在其它二级域名下就都可以访问到了
                //cookie.Domain = "wangcl.com";
                response.AppendCookie(cookie);
            }
        }

        #endregion

        #region 设置/修改Cookie

        /// <summary>
        /// 设置Cookie子键的值
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCookie(string cookieName, string key, string value)
        {
            SetCookie(cookieName, key, value, null);
        }

        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCookie(string key, string value)
        {
            SetCookie(key, null, value, null);
        }

        /// <summary>
        /// 设置Cookie值和过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        public static void SetCookie(string key, string value, DateTime expires)
        {
            SetCookie(key, null, value, expires);
        }

        /// <summary>
        /// 设置Cookie过期时间
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="expires"></param>
        public static void SetCookie(string cookieName, DateTime expires)
        {
            SetCookie(cookieName, null, null, expires);
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        private static void SetCookie(string cookieName, string key, string value, DateTime? expires)
        {
            HttpResponse response = System.Web.HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                        cookie.Values.Set(key, value);
                    else
                        if (!string.IsNullOrEmpty(value))
                            cookie.Value = value;
                    if (expires != null)
                        cookie.Expires = expires.Value;
                    response.SetCookie(cookie);
                }
            }

        }

        #endregion

        #region 获取Cookie
        /// <summary>
        /// 获得Cookie的值
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetCookieValue(string cookieName)
        {
            return GetCookieValue(cookieName, null);
        }

        /// <summary>
        /// 获得Cookie的值
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCookieValue(string cookieName, string key)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (request != null)
                return GetCookieValue(request.Cookies[cookieName], key);
            return "";
        }

        /// <summary>
        /// 获得Cookie的子键值
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetCookieValue(System.Web.HttpCookie cookie, string key)
        {
            if (cookie != null)
            {
                if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                    return cookie.Values[key];
                else
                    return cookie.Value;
            }
            return "";
        }

        /// <summary>
        /// 获得Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static System.Web.HttpCookie GetCookie(string cookieName)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (request != null)
                return request.Cookies[cookieName];
            return null;
        }

        #endregion

        #region 删除Cookie

        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        public static void RemoveCookie(string cookieName)
        {
            RemoveCookie(cookieName, null);
        }

        /// <summary>
        /// 删除Cookie的子键
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        private static void RemoveCookie(string cookieName, string key)
        {
            HttpResponse response = System.Web.HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                        cookie.Values.Remove(key);
                    else
                        response.Cookies.Remove(cookieName);
                }
            }
        }

        #endregion
    }
}
