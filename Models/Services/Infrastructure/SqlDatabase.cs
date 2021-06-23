using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models.Services.Infrastructure
{
    public class SqlDatabase : IDatabaseAccessor
    {
        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
            var queryArgument = formattableQuery.GetArguments();
            var sqlParameters = new List<SqlParameter>();

            for (int i = 0; i < queryArgument.Length; i++)
            {
                var parameter = new SqlParameter(i.ToString(), queryArgument[i]);

                sqlParameters.Add(parameter);

                queryArgument[i] = "@" + i;
            }

            string query = formattableQuery.ToString();

            using (SqlConnection database = new SqlConnection("Data Source=DESKTOP-PO35QJG;Initial Catalog=Courses;Integrated Security=True"))
            {
                await database.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(query, database))
                {
                    sqlCommand.Parameters.AddRange(sqlParameters.ToArray());

                    using (var reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        DataSet dataSet = new DataSet();

                        dataSet.EnforceConstraints = false;

                        do
                        {
                            DataTable dataTable = new DataTable();

                            dataSet.Tables.Add(dataTable);

                            dataTable.Load((IDataReader)reader);

                            return dataSet;
                        } while (!reader.IsClosed);

                    }
                }
            }
        }
    }
}
