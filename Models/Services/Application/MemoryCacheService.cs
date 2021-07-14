using Courses.Controllers;
using Courses.Models.Services.Application;
using Courses.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courses
{
    public class MemoryCacheService : ICachedCourseService
    {
        private readonly ICourseService courseService;
        private readonly IMemoryCache memoryCache;
       // private readonly IHttpContextAccessor httpContextAccessor;

        public MemoryCacheService(ICourseService courseService, IMemoryCache memoryCache)// IHttpContextAccessor httpContextAccessor)
        {
            this.courseService = courseService;
            this.memoryCache = memoryCache;
           // this.httpContextAccessor = httpContextAccessor;
        }

        //public Task<List<CourseViewModel>> GetBestRatingCoursesAsync()
        //{
        //    return memoryCache.GetOrCreateAsync($"BestRatingCourses", cacheEntry =>
        //    {
        //        cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
        //        return courseService.GetBestRatingCoursesAsync();
        //    });
        //}

        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry =>
            {
                cacheEntry.SetSize(1);

                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                return courseService.GetCourseAsync(id);
            });
        }

        public Task<List<CourseViewModel>> GetCoursesAsync()
        {
            return memoryCache.GetOrCreateAsync($"Courses", cacheEntry =>
            {
                cacheEntry.SetSize(1);

                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                return courseService.GetCoursesAsync();
            });
        }
    }
}