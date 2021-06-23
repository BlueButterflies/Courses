using System;
using System.Data;
using System.Threading.Tasks;

namespace Courses
{
    public interface IDatabaseAccessor
    {
        public Task<DataSet> QueryAsync(FormattableString selectFromDb);
    }
}