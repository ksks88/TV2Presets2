using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TV2Presets2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Channel presets";
            return View();
        }

        public ActionResult Start()
        {
            return PartialView();
        }

        public ActionResult Downlink()
        {
            return PartialView();
        }

        public ActionResult Uplink()
        {
            return PartialView();
        }

        public ActionResult Setup()
        {
            return PartialView();
        }

        public ActionResult ApplyDownlinkPresetDialog()
        {
            return PartialView();
        }
    }
}
