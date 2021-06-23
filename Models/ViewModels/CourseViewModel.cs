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

        public string Description { get; internal set; }

        public List<LessonViewModel> Lessons { get; internal set; }

        public static CourseViewModel FromDataRow(DataRow dataRow)
        {
            CourseViewModel courseView = new CourseViewModel
            {
                Title = dataRow["Title"].ToString(),
                Author = dataRow["Author"].ToString(),
                ImgPath = dataRow["ImagePath"].ToString(),
                Rating = Convert.ToDouble(dataRow["Rating"]),
                FullPrice = new Money
                (
                    Enum.Parse<Currency>(dataRow["FullPrice_Currency"].ToString()),
                    Convert.ToDecimal(dataRow["FullPrice"])
                ),
                DiscountPrice = new Money
                (
                    Enum.Parse<Currency>(dataRow["DiscountPrice_Currency"].ToString()),
                    Convert.ToDecimal(dataRow["DiscountPrice"])
                ),
                Id = (int)dataRow["Id"]
            };

            return courseView;
        }
    }
}
