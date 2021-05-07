using Courses.Models.ViewModels;
using Courses.Models.Services.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models.Services.Application
{
    public class CourseService
    {
        internal List<CourseViewModel> GetCourseService()
        {
            List<CourseViewModel> courseList = new List<CourseViewModel>();

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
                    ImgPath = "/logo.png"

                };
                courseList.Add(course);
            }
            return courseList;
        }
    }
}
