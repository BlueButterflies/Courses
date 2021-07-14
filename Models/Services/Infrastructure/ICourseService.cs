using Courses.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models.Services.Application
{
    public interface ICourseService
    {
        public Task<List<CourseViewModel>> GetCoursesAsync();

        public Task<CourseDetailViewModel> GetCourseAsync(int id);
      //  Task<List<CourseViewModel>> GetBestRatingCoursesAsync();
    }
}
