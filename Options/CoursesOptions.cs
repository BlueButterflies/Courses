﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Options
{
    //public class CoursesOptions
    //{
    //}

    public class CoursesOptions
    {
        public long PerPage { get; set; }
        public CoursesOrderOptions Order { get; set; }
    }

    public class CoursesOrderOptions
    {
        public string By { get; set; }
        public bool Ascending { get; set; }
        public string[] Allow { get; set; }
    }
}



