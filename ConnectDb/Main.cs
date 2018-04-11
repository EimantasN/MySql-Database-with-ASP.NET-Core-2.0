//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConnectDb
//{
//    public class Main
//    {
//        const string ConnectionString = "server=localhost;uid=root;pwd=;database=skelbimai";

//        public List<Citys> GetCitys()
//        {
//            List<Citys> cityList = new List<Citys>();
//            using (SqlConnection connection = new SqlConnection(ConnectionString))
//            {
//                connection.Open();

//                SqlCommand command = new SqlCommand("SELECT * FROM City;", connection);

//                using (SqlDataReader reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        var model = new Citys
//                        {
//                            Id = (int)reader[0],
//                            Name = (string)reader[1]
//                        };
//                        cityList.Add(model);
//                    }
//                }
//            }
//            return cityList;
//        }
//    }
//}
