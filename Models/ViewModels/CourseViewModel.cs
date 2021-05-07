using Courses.Models.Services.Application;
using System;
using System.Collections.Generic;
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
    }
}
