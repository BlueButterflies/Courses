using Courses.Models.Services.Application;
using Courses.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            CourseService courseService = new CourseService();

            List<CourseViewModel> courseViewModels = courseService.GetCourseService();

            return View(courseViewModels);
        }

        public IActionResult Detail(string id)
        {
            return View();
        }
    }
}
