using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fonery.Model;
using Fonery.DAL;
using System.Web.Security;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace CementPSI.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public ActionResult Login(string UserName, string Password)
        {
            Users user = new DALUsers().CheckPassword(UserName, Password);
            if (user != null && user.Rid > 0)
            {
                var data = JsonConvert.SerializeObject(user);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, user.LoginName, DateTime.Now,
                    DateTime.Now.AddHours(72), true, data, FormsAuthentication.FormsCookiePath);

                //返回登录结果、用户信息、用户验证票据信息
                var Ticket = FormsAuthentication.Encrypt(ticket);

                //将身份信息保存在cookie中 返回给客户端
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Ticket);
                cookie.HttpOnly = true;
                //是否为持久化cookie  会话性cookie保存于内存中。关闭浏览器则会话性cookie会过期消失；持久化cookie则不会，直至过期时间已到或确认注销。
                if (ticket.IsPersistent)
                {
                    //设置cookie到期时间
                    cookie.Expires = ticket.Expiration;
                }

                var context = System.Web.HttpContext.Current;
                if (context == null)
                    throw new InvalidOperationException();
                //把票据信息写入Cookie和Session  
                //SetAuthCookie方法用于标识用户的Identity状态为true 
                //若不设置cookia的过期时间，默认关闭浏览器（会话）清空cookia,若有设置则按照设置的过期时间
                context.Response.Cookies.Remove(cookie.Name);
                context.Response.Cookies.Add(cookie);

                return Json(new { Success = true, Result = user, ErrMsg = "", Token = Ticket });
            }
            else
            {
                return Json(new { Success = false, Result = "", ErrMsg = "账号名或密码错误", Token = "" });
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            var context = System.Web.HttpContext.Current;
            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            //userData = JsonConvert.DeserializeObject<T>(ticket.UserData);

            FormsAuthentication.SignOut();
            return Redirect("~/Login");
        }

        /// <summary>
        /// 检查是否已登录
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckLogin()
        {
            var result = new { s = System.Web.HttpContext.Current.Request.IsAuthenticated };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}