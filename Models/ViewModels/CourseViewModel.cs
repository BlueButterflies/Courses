using Courses.Models.Services.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImgPath { get; set; }

        public string Author { get; set; }

        public double Rating { get; set; }

        public Money FullPrice { get; set; }

        public Money DiscountPrice { get; set; }

        public string Description { get; set; }

        public List<LessonViewModel> Lessons { get; set; }

        public static CourseViewModel FromDataRow(DataRow dataRow)
        {
            CourseViewModel courseView = new CourseViewModel
            {
                Id = (int)dataRow["Id"],
                Title = dataRow["Title"].ToString(),
                ImgPath = dataRow["ImagePath"].ToString(),
                Author = dataRow["Author"].ToString(),
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
                )
            };

            return courseView;
        }
    }
}
