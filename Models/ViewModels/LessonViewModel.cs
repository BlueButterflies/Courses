using System;
using System.Data;

namespace Courses.Models.ViewModels
{
    public class LessonViewModel
    {
        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

        public static LessonViewModel FromDataRow(DataRow lessonRow)
        {
            LessonViewModel lessonView = new LessonViewModel
            {
                Title = lessonRow["Title"].ToString(),
                Duration = (TimeSpan)lessonRow["Duration"]
            };

            return lessonView;
        }
    }
}