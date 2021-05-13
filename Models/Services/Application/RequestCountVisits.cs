using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Courses.Models.Services.Application
{
    public class RequestCountVisits : IRequestVisits
    {
        private int count = 0;

        public void IcrementCountVisits()
        {
            Interlocked.Increment(ref count);
        }
    }
}
