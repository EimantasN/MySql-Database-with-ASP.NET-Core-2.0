using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Database
{
    public class Context
    {
        public string ConnectionString { get; set; }

        public Context(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Citys> GetCitys()
        {
            List<Citys> cityList = new List<Citys>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `citys`;", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cityList.Add(new Citys
                        {
                            Id = (int)reader[1],
                            Name = reader[0].ToString()
                        });
                    }
                }
            }
            return cityList;
        }
    }
}
