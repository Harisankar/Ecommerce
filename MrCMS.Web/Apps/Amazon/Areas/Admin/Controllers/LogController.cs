﻿using MrCMS.Web.Apps.Amazon.Entities.Logs;
using MrCMS.Web.Apps.Amazon.Models;
using MrCMS.Web.Apps.Amazon.Services.Logs;
using MrCMS.Website.Controllers;
using System.Web.Mvc;

namespace MrCMS.Web.Apps.Amazon.Areas.Admin.Controllers
{
    public class LogController : MrCMSAppAdminController<AmazonApp>
    {
        private readonly IAmazonLogService _eBayLogService;

        public LogController(IAmazonLogService eBayLogService)
        {
            _eBayLogService = eBayLogService;
        }

        [HttpGet]
        public ViewResult Index(AmazonLogType? type, AmazonLogStatus? status, int page = 1)
        {
            var model = _eBayLogService.GetEntriesPaged(page, type, status);
            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public RedirectToRouteResult Index_POST(AmazonLogType? type, AmazonLogStatus? status)
        {
            return RedirectToAction("Index", new { type,status });
        }

        public ViewResult Details(AmazonLog entry)
        {
            return View(entry);
        }

        [HttpGet]
        public ActionResult DeleteAllLogs()
        {
            return PartialView();
        }

        [HttpPost]
        [ActionName("DeleteAllLogs")]
        public ActionResult DeleteAllLogs_POST()
        {
            _eBayLogService.DeleteAllLogs();

            return RedirectToAction("Index");
        }
    }
}
