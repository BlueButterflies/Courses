 using Courses.Models.ViewModels;
using Courses.Models.Services.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models.Services.Application
{
    public class CourseService : ICourseService
    {
        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            List<CourseViewModel> courseList =  new List<CourseViewModel>();

            Random random = new Random();

            for (int i = 1; i <= 20; i++)
            {
                decimal price = Convert.ToDecimal(random.NextDouble() * 10 + 10);

                CourseViewModel course = new CourseViewModel
                {
                    Id = i,
                    Title = $"Course {i}",
                    DiscountPrice = new Money(Currency.EUR, price),
                    FullPrice = new Money(Currency.EUR, random.NextDouble() > 0.5 ? price : price - 1),
                    Author = "Name Last Name",
                    Rating = random.NextDouble() * 5.00,
                    ImgPath = "~/images/logo.png"

                };

                courseList.Add(course);
            }
            return courseList;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            Random random = new Random();
            decimal price = Convert.ToDecimal(random.NextDouble() * 10 + 10);

            CourseDetailViewModel course = new CourseDetailViewModel
            {
                Id = id,
                Title = $"Course {id}",
                DiscountPrice = new Money(Currency.EUR, price),
                FullPrice = new Money(Currency.EUR, random.NextDouble() > 0.5 ? price : price - 1),
                Author = "Name Last Name",
                Rating = random.NextDouble() * 5.00,
                ImgPath = "~/images/logo.png",
                Description = $"Description {id}",
                Lessons = new List<LessonViewModel>()
            };

            for (int i = 1; i <= 5; i++)
            {
                LessonViewModel lession = new LessonViewModel
                {
                    Title = $"Lesson {i}",
                    Duration = TimeSpan.FromSeconds(random.Next(40, 90))
                };

                course.Lessons.Add(lession);
            }

            return course;
        }
    }
}
