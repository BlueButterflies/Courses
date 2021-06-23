using Courses.Models.ViewModels;
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

        public AdoNetCourseService(IDatabaseAccessor database)
        {
            this.Database = database;
        }

        public async Task<CourseDatailViewModel> GetCourseAsync(int id)
        {
            FormattableString selectFromDb = $@"Select [Id],[Title],[Description],[ImagePath],[Author],[Rating],[FullPrice],[FullPrice_Currency],[DiscountPrice],[DiscountPrice_Currency] From [dbo].[Courses] WHERE [Id]={id};SELECT [Id],[Title],[Descriptions],[Duration] FROM[dbo].[Lessons] WHERE [CourseId]={id}";

            DataSet dataSet = await Database.QueryAsync(selectFromDb);

            //Course
            DataTable dataTableCourse = dataSet.Tables[0];

            if (dataTableCourse.Rows.Count != 1)
            {
                throw new InvalidOperationException($"Did not return exactly 1 row for Course {id}");
            }

            var courseRow = dataTableCourse.Rows[0];
            var detailsCourse = CourseDatailViewModel.FromDataRow(courseRow);

            //Lession Course
            DataTable dataTableLesson = dataSet.Tables[1];

            foreach (DataRow lessonRow in dataTableLesson.Rows)
            {
                LessonViewModel lessonView = LessonViewModel.FromDataRow(lessonRow);
                detailsCourse.Lessons.Add(lessonView);
            }

            return (CourseDatailViewModel)detailsCourse;
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
