﻿using Courses.Models.Entity;
using Courses.Models.Services.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models.ViewModels
{
    public class CourseDetailViewModel : CourseViewModel
    {
        public string Description { get; set; }

        public List<LessonViewModel> Lessons { get; set; }

        public TimeSpan TotalCourseDuration
        {
            get => TimeSpan.FromMinutes(Lessons?.Sum(l => l.Duration.TotalMinutes) ?? 0);
        }

        public static new CourseViewModel FromDataRow(DataRow dataRow)
        {
            CourseViewModel courseView = new CourseViewModel
            {
                Id = (int)dataRow["Id"],
                Title = dataRow["Title"].ToString(),
                Author = dataRow["Author"].ToString(),
                Description = dataRow["Description"].ToString(),
                ImgPath = dataRow["ImagePath"].ToString(),
                Rating = Convert.ToDouble(dataRow["Rating"]),
                FullPrice = new Money
                (
                    Enum.Parse<Currency>(dataRow["FullPrice_Currency"].ToString()),
                    Convert.ToDecimal(dataRow["FullPrice_Amount"])
                ),
                DiscountPrice = new Money
                (
                    Enum.Parse<Currency>(dataRow["DiscountPrice_Currency"].ToString()),
                    Convert.ToDecimal(dataRow["DiscountPrice_Amount"])
                ),

                Lessons = new List<LessonViewModel>()
            };

            return courseView;
        }

        internal static object FromEntity(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
