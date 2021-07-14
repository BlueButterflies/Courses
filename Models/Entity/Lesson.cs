using System;
using System.Collections.Generic;

#nullable disable

namespace Courses.Models.Entity
{
    public partial class Lesson
    {
        public int Id { get; private set; }
        public int CourseId { get; private set; }
        public string Title { get; private set; }
        public string Descriptions { get; private set; }
        public TimeSpan Duration { get; private set; }

        public virtual Course Course { get; private set; }
    }
}
