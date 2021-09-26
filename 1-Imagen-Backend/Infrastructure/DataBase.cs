using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;

namespace Ms_sena.Infrastructure
{
    public class DataBase : IDataBase
    {
        private readonly string connString;
        private readonly IConfiguration _configuration;
        public DataBase(IConfiguration configuration)
        {
            _configuration = configuration;
            connString = _configuration["MYSQL_CONNECTION_STRING"];
        }


        public async Task<TimeDto> AddTimeAsync(TimeDto time)
        {
            try
            {
                string query = @"INSERT INTO TimeData (Date,Number) VALUES (@Date,@Number)";
                var param = new DynamicParameters();
                param.Add("@Date", time.Date.ToString("yyyy-MM-dd H:mm:ss"));
                param.Add("@Number", time.Number);

                TimeDto timeResult =new TimeDto();
                using (var connection = new MySqlConnection(connString))
                {
                    var result = await connection.ExecuteAsync(query, param, null, null, CommandType.Text);
                    if (result > 0)
                    {
                        timeResult = time;
                    }
                }
                return timeResult;
                
            }
            catch (Exception e)
            {
                throw new Exception("The data could not be added", e);
            }
        }

        public async Task<List<TimeDto>> GetAllAsync()
        {
            var timeResult = new List<TimeDto>();
            try
            {
                string query = @"SELECT * FROM TimeData";
                using (var connection = new MySqlConnection(connString))
                {
                    var result = await connection.QueryAsync<TimeDto>(query, CommandType.Text);
                    timeResult = result.ToList();
                }
                return timeResult;
            }
            catch (Exception e)
            {
                throw new Exception("The data could not be readed", e);
            }
        }
    }
}
