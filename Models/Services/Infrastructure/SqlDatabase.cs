using Courses.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models.Services.Infrastructure
{
    public class SqlDatabase : IDatabaseAccessor
    {
        public SqlDatabase(IOptionsMonitor<ConnectionStringOptions> configuration)
        {
            Configuration = configuration;
        }

        public IOptionsMonitor<ConnectionStringOptions> Configuration { get; }

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

            string connectionDb = Configuration.CurrentValue.DbConnect;

            using (SqlConnection database = new SqlConnection(connectionDb))
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
