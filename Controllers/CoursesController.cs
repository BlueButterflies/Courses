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
        private readonly ICourseService CourseService;

        public CoursesController(ICourseService courseService)
        {
            this.CourseService = courseService;
        }        

        public IActionResult Index()
        {
            ViewBag.Title = "Catalog";

            List<CourseViewModel> courseViewModels = CourseService.GetCourses();

            return View(courseViewModels);
        }

        public IActionResult Detail(int id)
        {
            CourseDatailViewModel viewModel = CourseService.GetCourse(id);

            ViewBag.Title = viewModel.Title;

            return View(viewModel);
        }
    }
}
