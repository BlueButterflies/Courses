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

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Catalog";

            List<CourseViewModel> courseViewModels = await CourseService.GetCoursesAsync();

            return View(courseViewModels);
        }

        public async Task<IActionResult> Detail(int id)
        {
            CourseDatailViewModel viewModel = await CourseService.GetCourseAsync(id);

            ViewBag.Title = viewModel.Title;

            return View(viewModel);
        }
    }
}
