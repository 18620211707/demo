using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using System.Web.Mvc;
using Fonery.DAL;

namespace CementPSI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (!System.Web.HttpContext.Current.Request.IsAuthenticated)
                return RedirectToAction("Index", "Login");

            ViewBag.User = GetLoginUser();

            return View();
        }

        public ActionResult Welcome()
        {
            if (!System.Web.HttpContext.Current.Request.IsAuthenticated)
                return RedirectToAction("Index", "Login");

            int DayOrderNum ,MonthOrderNum ,YearOrderNum ,DaySaleNum ,MonthSaleNum ,YearSaleNum = 0;
            var salesStat = new DALSalesLog().SalesStat(out DayOrderNum, out MonthOrderNum, out YearOrderNum, out DaySaleNum, out MonthSaleNum, out YearSaleNum);

            ViewBag.User = GetLoginUser();
            ViewBag.SalesStat = salesStat;
            ViewBag.DayOrderNum = DayOrderNum;
            ViewBag.MonthOrderNum = MonthOrderNum;
            ViewBag.YearOrderNum = YearOrderNum;
            ViewBag.DaySaleNum = DaySaleNum;
            ViewBag.MonthSaleNum = MonthSaleNum;
            ViewBag.YearSaleNum = YearSaleNum;

            return View();
        }
    }
}
