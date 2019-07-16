using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using System.Web.Mvc;
using Fonery.Model;
using Fonery.DAL;
using System.Collections.Generic;
using System.Linq;

namespace CementPSI.Controllers
{
    public class AnalysisController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.WarehouseList = new DALWarehouse().QueryList();
            return View();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="model"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult PageList(SalesLogQuery model, int currentPage = 1, int pageSize = 20)
        {
            if (model.endTime != null) model.endTime = Convert.ToDateTime(model.endTime).AddDays(1).AddSeconds(-1);

            int recordCount = 0;
            List<SalesLog> SalesLog = new DALSalesLog().QueryPagination(currentPage, pageSize, out recordCount, model).ToList();

            ViewBag.RecordCount = recordCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.PageSize = pageSize;
            ViewBag.PageCount = Math.Ceiling((double)recordCount / (double)pageSize);

            return View(SalesLog);
        }
    }
}
