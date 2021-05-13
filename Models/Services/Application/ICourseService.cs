using Courses.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models.Services.Application
{
    public interface ICourseService
    {
        List<CourseViewModel> GetCourses();

        CourseDatailViewModel GetCourse(int id);
    }
}
