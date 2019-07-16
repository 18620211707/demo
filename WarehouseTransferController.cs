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
    public class WarehouseTransferController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="model"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult PageList(WarehouseTransfer model, int currentPage = 1, int pageSize = 20)
        {
            int recordCount = 0;
            List<WarehouseTransfer> WarehouseTransfer = new DALWarehouseTransfer().QueryPagination(currentPage, pageSize, out recordCount, model).ToList();

            ViewBag.RecordCount = recordCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.PageSize = pageSize;
            ViewBag.PageCount = Math.Ceiling((double)recordCount / (double)pageSize);
            ViewBag.Model = model;

            return View(WarehouseTransfer);
        }

        /// <summary>
        /// 编辑仓库
        /// </summary>
        /// <param name="Rid"></param>
        /// <returns></returns>
        public ActionResult Edit(int Rid)
        {
            var model = new WarehouseTransfer();
            if (Rid > 0) model = new DALWarehouseTransfer().GetSingleWarehouseTransfer(Rid);

            ViewBag.WarehouseList = new DALWarehouse().QueryList();
            ViewBag.ProductList = new DALProduct().QueryList();

            return View(model);
        }

        /// <summary>
        /// 添加/修改保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Save(WarehouseTransfer model)
        {
            ResultMsg msg = new ResultMsg();

            //修改
            if (model.Rid > 0) msg = new DALWarehouseTransfer().Update(model);
            //添加
            else if (model.Rid == 0) msg = new DALWarehouseTransfer().Insert(model);

            return Json(new { Success = msg.Success, Result = msg.ReturnInt, ErrMsg = msg.ErrMsg });
        }

        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <param name="Rid"></param>
        /// <returns></returns>
        public ActionResult Delete(int Rid)
        {
            ResultMsg msg = new ResultMsg();
            if (Rid > 0) msg = new DALWarehouseTransfer().Delete(Rid);

            return Json(new { Success = msg.Success, Result = msg.ReturnInt, ErrMsg = msg.ErrMsg });
        }
    }
}
