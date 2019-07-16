using CementPSI.App_Start;
using Fonery.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CementPSI.Controllers
{
    /// <summary>
    /// 控制器基类。
    /// <remarks>所有授权控制器全部继承此类</remarks>
    /// </summary>
    public partial class BaseController : Controller
    {
        /// <summary>
        /// 当前控制器名称Controller
        /// </summary>
        protected string CurrentAreaName
        {
            get
            {
                // return RouteData.Route.GetRouteData(this.HttpContext).Values["controller"].ToString();
                return RouteData.Values["area"].ToString();
            }
        }
        /// <summary>
        /// 当前控制器名称Controller
        /// </summary>
        protected string CurrentControllerName
        {
            get
            {
                // return RouteData.Route.GetRouteData(this.HttpContext).Values["controller"].ToString();
                return RouteData.Values["controller"].ToString();
            }
        }
        /// <summary>
        /// 当前动作名称Action
        /// </summary>
        protected string CurrentActionName
        {
            get
            {
                //return RouteData.Route.GetRouteData(this.HttpContext).Values["action"].ToString();
                return RouteData.Values["action"].ToString();
            }
        }
        /*
        二、当前controller、action的获取
        RouteData.Route.GetRouteData(this.HttpContext).Values["controller"]
        RouteData.Route.GetRouteData(this.HttpContext).Values["action"]
        或
        RouteData.Values["controller"]
        RouteData.Values["action"]

        如果在视图中可以用
        ViewContext.RouteData.Route.GetRouteData(this.Context).Values["controller"]
        ViewContext.RouteData.Route.GetRouteData(this.Context).Values["action"]
        或
        ViewContext.RouteData.Values["controller"]
        ViewContext.RouteData.Values["action"]
        */


        public BaseController()
        {
            ViewBag.ts = DateTime.Now.ToString("yyyyMMddhhmmss");
        }
        
        /// <summary>
        /// 获取当前的登录用户者
        /// </summary>
        public Users GetLoginUser()
        {
            return FormsAuth.GetUserData();
        }

        /// <summary>
        /// 获取页面需要用到的url地址
        /// </summary>
        //protected dynamic PageUrls
        //{
        //    get
        //    {
        //        return this.InitPageUrls();
        //    }
        //}

        ///// <summary>
        ///// 获取当前用户的菜单权限按钮
        ///// </summary>
        ///// <param name="menucode">菜单代码</param>
        ///// <returns>dynamic</returns>
        //protected dynamic GetUserMenuButtons(string menucode)
        //{
        //    return Base_MenuService.Instance.GetUserMenuButtons(this.CurrentBaseLoginer.UserId, menucode);
        //}

        ///// <summary>
        ///// 获取页面需要用到的url地址
        ///// </summary>
        ///// <param name="areas"></param>
        ///// <param name="controller"></param>
        ///// <param name="extend"></param>
        ///// <returns></returns>
        //protected dynamic InitPageUrls(string areas, string controller, object extend = null)
        //{
        //    var expando = (IDictionary<string, object>)new ExpandoObject();
        //    expando["list"] = string.Format("/{0}/{1}/GetList", areas, controller);
        //    expando["pagelist"] = string.Format("/{0}/{1}/GetPageList", areas, controller);
        //    expando["add"] = string.Format("/{0}/{1}/Add", areas, controller);
        //    expando["edit"] = string.Format("/{0}/{1}/Edit", areas, controller);
        //    expando["delete"] = string.Format("/{0}/{1}/Delete", areas, controller);
        //    if (extend != null)
        //        EachHelper.EachObjectProperty(extend, (i, name, value) => { expando[name] = value; });

        //    OnInitPageUrls(expando);

        //    return expando;
        //}

        /// <summary>
        /// 获取页面需要用到的url地址
        /// </summary>
        /// <param name="extend"></param>
        /// <returns></returns>
        //protected dynamic InitPageUrls(object extend = null)
        //{
        //    return InitPageUrls(this.CurrentAreaName, this.CurrentControllerName, extend);
        //}

        /// <summary>
        /// 【虚方法】子类进行重写添加其他的url地址
        /// </summary>
        /// <param name="expando"></param>
        protected virtual void OnInitPageUrls(IDictionary<string, object> expando)
        {
            //子类进行重写添加其他的url地址
        }

        ///// <summary>
        ///// 初始化返回结果动态模型对象
        ///// </summary>
        ///// <param name="extend"></param>
        ///// <returns></returns>
        //protected dynamic InitResultModel(object extend = null)
        //{
        //   // var expando = new ExpandoObject();
        //    object baseModel = new { urls = this.PageUrls, loginer = this.CurrentUser };

        //    var ov = OnInitResultModel();
        //    if (ov != null)
        //        ObjectMapper.CopyProperties(ov, baseModel);

        //    if (extend != null)
        //        ObjectMapper.CopyProperties(extend, baseModel);


        //    return baseModel;
        //}

        ///// <summary>
        ///// 【虚方法】子类进行重写初始化返回结果动态模型对象，默认：return null;
        ///// </summary>
        ///// <param name="expando"></param>
        //protected virtual dynamic OnInitResultModel()
        //{
        //    //子类进行重写初始化返回结果动态模型对象
        //    return null;
        //}





        /// <summary>
        /// 创建Easyui的DataGrid格式数据
        /// </summary>
        /// <param name="rows">数据</param>
        /// <returns>dynamic</returns>
        protected dynamic CreateJsonData_DataGrid(int total, object rows)
        {
            dynamic result = new ExpandoObject();
            result.rows = rows;
            result.total = total;
            return result;
        }

        /// <summary>
        /// 创建Json数据，Easyui的DataGrid专用
        /// </summary>
        /// <param name="total">总记录数</param>
        /// <param name="rows">当前页记录数</param>
        /// <param name="footer">表格footer</param>
        /// <returns>字典Dictionary<string, object> </returns>
        protected Dictionary<string, object> CreateJsonData_DataGrid(int total, object rows, object footer)
        {
            Dictionary<string, object> dicData = new Dictionary<string, object>();
            dicData.Add("total", total);
            dicData.Add("rows", rows);
            if (footer != null)
            {
                dicData.Add("footer", footer);
            }
            return dicData;
            /*
             var footer = new[]
            {
                new {  SL = list.Sum(p => p.SL), JE = list.Sum(p => p.JE), GGMC = "合计" }
            };
             */
        }

        /// <summary>
        /// 创建Json数据，Easyui的DataGrid专用
        /// </summary>
        /// <param name="total">总记录数</param>
        /// <param name="rows">当前页记录数</param>
        /// <param name="footer">表格footer</param>
        /// <returns>string</returns>
        protected string CreateJsonString_DataGrid(int total, object rows, object footer)
        {
            Dictionary<string, object> dicData = new Dictionary<string, object>();
            dicData.Add("total", total);
            dicData.Add("rows", rows);
            if (footer != null)
            {
                dicData.Add("footer", footer);
            }
            string jsonResultString = JsonConvert.SerializeObject(dicData, Formatting.Indented, this.DefaultTimeConverter);
            return jsonResultString;
            /*
             var footer = new[]
            {
                new {  SL = list.Sum(p => p.SL), JE = list.Sum(p => p.JE), GGMC = "合计" }
            };
             */
        }

        /// <summary>
        /// 获取json序列化是日期格式方式。
        /// </summary>
        protected IsoDateTimeConverter DefaultTimeConverter
        {
            get
            {
                return new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss",
                    DateTimeStyles = System.Globalization.DateTimeStyles.AllowInnerWhite
                };
            }
        }

        /// <summary>
        /// 摘要: 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">参数: data: 要序列化的 JavaScript 对象图。</param>
        /// <returns> 返回结果:将指定对象序列化为 JSON 格式的 JSON 结果对象。在执行此方法所准备的结果对象时，ASP.NET MVC 框架会将该对象写入响应。</returns>
        protected internal JsonResult JsonNet(object data)
        {
            return new JsonNetResult(data);
        }

        /// <summary>
        /// 摘要: 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">参数: data: 要序列化的 JavaScript 对象图。</param>
        /// <param name="behavior">指定是否允许来自客户端的 HTTP GET 请求。</param>
        /// <returns> 返回结果:将指定对象序列化为 JSON 格式的 JSON 结果对象。在执行此方法所准备的结果对象时，ASP.NET MVC 框架会将该对象写入响应。</returns>
        protected internal JsonResult JsonNet(object data, JsonRequestBehavior behavior)
        {
            return new JsonNetResult(data, behavior);
        }
    }

    /// <summary>
    /// 定义自JsonResult对象JsonNetResult
    /// </summary>
    public class JsonNetResult : JsonResult
    {
        public JsonNetResult() { }
        public JsonNetResult(object data, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet, string contentType = null, Encoding contentEncoding = null)
        {
            this.Data = data;
            this.JsonRequestBehavior = behavior;
            this.ContentEncoding = contentEncoding;
            this.ContentType = contentType;
        }

        /// <summary>
        /// 获取json序列化是日期格式方式。
        /// </summary>
        protected IsoDateTimeConverter IsoDateTimeConverter
        {
            get
            {
                return new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss",
                    DateTimeStyles = System.Globalization.DateTimeStyles.AllowInnerWhite
                };
            }
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;
            if (this.Data == null)
                return;

            var json = JsonConvert.SerializeObject(this.Data, Formatting.Indented, this.IsoDateTimeConverter);
            response.Write(json);
        }
    }
}