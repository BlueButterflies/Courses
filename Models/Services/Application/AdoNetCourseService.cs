using Courses.Controllers;
using Courses.Models.Exceptions;
using Courses.Models.ViewModels;
using Courses.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor Database;

        public AdoNetCourseService(ILogger<AdoNetCourseService> logger,IDatabaseAccessor database, IOptionsMonitor<ConnectionStringOptions> options)
        {
            Logger = logger;
            this.Database = database;
            this.Options = options;
        }

        public ILogger<AdoNetCourseService> Logger { get; }

        public IOptionsMonitor<ConnectionStringOptions> Options { get; }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            Logger.LogInformation("Course {id} requested", id);

            FormattableString selectFromDb = $@"Select [Id],[Title],[Description],[ImagePath],[Author],[Rating],[FullPrice],[FullPrice_Currency],[DiscountPrice],[DiscountPrice_Currency] From [dbo].[Courses] WHERE [Id]={id};SELECT [Id],[Title],[Descriptions],[Duration] FROM[dbo].[Lessons] WHERE [CourseId]={id}";

            DataSet dataSet = await Database.QueryAsync(selectFromDb);

            //Course
            DataTable dataTableCourse = dataSet.Tables[0];

            if (dataTableCourse.Rows.Count != 1)
            {
                Logger.LogWarning("Course {id} requested", id);

                throw new CourseNotFoundException(id);
            }

            var courseRow = dataTableCourse.Rows[0];

            var detailsCourse = CourseDetailViewModel.FromDataRow(courseRow);

            //Lession Course
            DataTable dataTableLesson = dataSet.Tables[1];

            foreach (DataRow lessonRow in dataTableLesson.Rows)
            {
                LessonViewModel lessonView = LessonViewModel.FromDataRow(lessonRow);
             
                detailsCourse.Lessons.Add(lessonView);
            }

            return (CourseDetailViewModel)detailsCourse;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            FormattableString selectFromDb = $"Select [Id],[Title],[ImagePath],[Author],[Rating],[FullPrice],[FullPrice_Currency],[DiscountPrice],[DiscountPrice_Currency] From [dbo].[Courses]";

            DataSet dataSet = await Database.QueryAsync(selectFromDb);

            DataTable dataTable = dataSet.Tables[0];

            List<CourseViewModel> courseViews = new List<CourseViewModel>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                CourseViewModel course =  CourseViewModel.FromDataRow(dataRow);

                courseViews.Add(course);
            }

            return courseViews;
        }
    }
}
