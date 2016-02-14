using Razor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Razor.Controllers
{
    public class HomeController : Controller
    {

        cPerson myPerson = new cPerson(12);

        public ActionResult Index()
        {
            return View(myPerson);
        }

        public FileContentResult GetImage()
        {
            if (myPerson.Photo != null)
            {
                return File(myPerson.Photo, "jpg");
            }
            else
            {
                return null;
            }
        }
    }
}