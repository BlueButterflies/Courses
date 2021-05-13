using System;

namespace Courses.Models.ViewModels
{
    public class LessonViewModel
    {
        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

       //public TimeSpan TotalCourseDuration { get => TimeSpan.FromSeconds(Lessons?.Sum(l => l.Duration.TotalSeconds) ?? 0); }
    }
}