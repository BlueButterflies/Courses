using Courses.Models.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            switch (feature.Error)
            {
                case CourseNotFoundException exc:
                    ViewData["Title"] = "Corso non trovato";
                    Response.StatusCode = 404;
                    return View("CourseNotFound");

                //case UserUnknownException exc:
                //    ViewData["Title"] = "Utente sconosciuto";
                //    Response.StatusCode = 400;
                //    return View();

                //case SendException exc:
                //    ViewData["Title"] = "Non è stato possibile inviare il messaggio, riprova più tardi";
                //    Response.StatusCode = 500;
                //    return View();

                default:
                    ViewData["Title"] = "Errore";
                    return View();
            }

        }
    }
}
