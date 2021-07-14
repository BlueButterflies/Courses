using Courses.Models.Services.Infrastructure;
using Courses.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly CoursesDbContext DbContext;

        public EfCoreCourseService(CoursesDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            IQueryable<CourseDetailViewModel> queryDetail = DbContext.Courses
                .Where(course => course.Id == id)
                .Select(course =>
                new CourseDetailViewModel
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Descriptions,
                    ImgPath = course.ImagePath,
                    Author = course.Author,
                    Rating = course.Rating,
                    FullPrice = course.FullPrice,
                    DiscountPrice = course.DiscountPrice,

                    Lessons = course.Lessons.Select(lesson => new LessonViewModel
                    {
                        Id = lesson.Id,
                        Duration = lesson.Duration,
                        Title = lesson.Title,
                        Description = lesson.Descriptions,
                    }).ToList()
                });
            
            CourseDetailViewModel courseDetailView = await queryDetail.AsNoTracking().SingleAsync();

            return courseDetailView;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {

            IQueryable<CourseViewModel> query =DbContext.Courses.Select(course =>
                                 new CourseViewModel
                                 {
                                     Id = course.Id,
                                     Title = course.Title,
                                     ImgPath = course.ImagePath,
                                     Author = course.Author,
                                     Rating = course.Rating,
                                     FullPrice = course.FullPrice,
                                     DiscountPrice = course.DiscountPrice

                                 });

            List<CourseViewModel> courses =  await query.AsNoTracking().ToListAsync();

            return courses;
        }
    }
}
