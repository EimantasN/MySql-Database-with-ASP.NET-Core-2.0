using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Reflection;

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


        #region Generic SQL method

        //Sukuria nauja savybe dinaminian objektui
        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        public List<object> GetList(object table)
        {
            List<object> List = new List<object>();
            List<string> ColNames = new List<string>();

            dynamic expando = new ExpandoObject();

            object prop = table.GetType().GetProperties();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                foreach (var property in table.GetType().GetProperties())
                {
                    ColNames.Add(property.Name);
                }

                MySqlCommand command = new MySqlCommand("SELECT * FROM `" + table.GetType().Name + "`;", conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        object data = new object();

                        //Sukuriu dinaminio objekto savybes
                        foreach (var a in ColNames)
                        {
                            AddProperty(expando, a, null);
                        }

                        //Uzsipildau statinio obekta sukurtais savybemis
                        var props = table.GetType().GetProperties();
                        var obj = Activator.CreateInstance(table.GetType());
                        var values = (IDictionary<string, object>)expando;
                        foreach (var propxx in props)
                            propxx.SetValue(obj, values[propxx.Name]);

                        //uzpildau objekta data
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            try
                            {
                                if (reader[i].GetType().Name.Contains("String"))
                                {
                                    obj.GetType().GetProperty(ColNames[i]).SetValue(obj, reader[i].ToString());
                                }
                                else if (reader[i].GetType().Name.ToString().Contains("Int"))
                                {
                                    obj.GetType().GetProperty(ColNames[i]).SetValue(obj, Int32.Parse(reader[i].ToString()));
                                }
                                else if (reader[i].GetType().Name.ToString().Contains("Date"))
                                {
                                    obj.GetType().GetProperty(ColNames[i]).SetValue(obj, reader[i].ToString());
                                }
                                else
                                {
                                    try
                                    {
                                        obj.GetType().GetProperty(ColNames[i]).SetValue(obj, Boolean.Parse(reader[i].ToString()));
                                    }
                                    catch { obj.GetType().GetProperty(ColNames[i]).SetValue(obj, false); }
                                }
                            }
                            catch
                            {
                                obj.GetType().GetProperty(ColNames[i]).SetValue(obj, "");
                            }
                        }
                        List.Add(obj);
                    }
                    conn.Close();
                }
            }
            return List;
        }
        //public List<object> GetList(Users table)
        //{
        //    List<object> List = new List<object>();
        //    List<string> ColNames = new List<string>();

        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();

        //        MySqlCommand command = new MySqlCommand("SELECT * FROM `" + table.GetType().Name + "`;", conn);
        //        using (var reader = command.ExecuteReader())
        //        {
        //            foreach (var property in table.GetType().GetRuntimeProperties())
        //            {
        //                ColNames.Add(property.Name);
        //            }
        //            while (reader.Read())
        //            {
        //                Users obj = new Users();
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    try
        //                    {
        //                        string x = reader[i].GetType().Name.ToString();
        //                        string value = reader[i].ToString();
        //                        if (reader[i].GetType().Name.Contains("String"))
        //                        {
        //                            obj.GetType().GetProperty(ColNames[i]).SetValue(obj, value);
        //                        }
        //                        else if (reader[i].GetType().Name.ToString().Contains("Int"))
        //                        {
        //                            obj.GetType().GetProperty(ColNames[i]).SetValue(obj, Int32.Parse(value));
        //                        }
        //                        else
        //                            obj.GetType().GetProperty(ColNames[i]).SetValue(obj, Boolean.Parse(value));
        //                    }
        //                    catch { }
        //                }
        //                List.Add(obj);
        //            }
        //            conn.Close();
        //        }
        //    }
        //    return List;
        //}
        //public List<object> GetList(Items table)
        //{
        //    List<object> List = new List<object>();
        //    List<string> ColNames = new List<string>();

        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();

        //        MySqlCommand command = new MySqlCommand("SELECT * FROM `" + table.GetType().Name + "`;", conn);
        //        using (var reader = command.ExecuteReader())
        //        {
        //            foreach (var property in table.GetType().GetRuntimeProperties())
        //            {
        //                ColNames.Add(property.Name);
        //            }
        //            while (reader.Read())
        //            {
        //                Items obj = new Items();
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    try
        //                    {
        //                        string x = reader[i].GetType().Name.ToString();
        //                        string value = reader[i].ToString();
        //                        if (reader[i].GetType().Name.Contains("String"))
        //                        {
        //                            obj.GetType().GetProperty(ColNames[i]).SetValue(obj, value);
        //                        }
        //                        else if (reader[i].GetType().Name.ToString().Contains("Int"))
        //                        {
        //                            obj.GetType().GetProperty(ColNames[i]).SetValue(obj, Int32.Parse(value));
        //                        }
        //                        else
        //                            obj.GetType().GetProperty(ColNames[i]).SetValue(obj, Boolean.Parse(value));
        //                    }
        //                    catch { }
        //                }
        //                List.Add(obj);
        //            }
        //            conn.Close();
        //        }
        //    }
        //    return List;
        //}
        //public List<object> GetList(Prices table)
        //{
        //    List<object> List = new List<object>();
        //    List<string> ColNames = new List<string>();

        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();

        //        MySqlCommand command = new MySqlCommand("SELECT * FROM `" + table.GetType().Name + "`;", conn);
        //        using (var reader = command.ExecuteReader())
        //        {
        //            foreach (var property in table.GetType().GetRuntimeProperties())
        //            {
        //                ColNames.Add(property.Name);
        //            }
        //            while (reader.Read())
        //            {
        //                Prices obj = new Prices();
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    try
        //                    {
        //                        string x = reader[i].GetType().Name.ToString();
        //                        string value = reader[i].ToString();
        //                        if (reader[i].GetType().Name.Contains("String"))
        //                        {
        //                            obj.GetType().GetProperty(ColNames[i]).SetValue(obj, value);
        //                        }
        //                        else if (reader[i].GetType().Name.ToString().Contains("Int"))
        //                        {
        //                            obj.GetType().GetProperty(ColNames[i]).SetValue(obj, Int32.Parse(value));
        //                        }
        //                        else
        //                            obj.GetType().GetProperty(ColNames[i]).SetValue(obj, Boolean.Parse(value));
        //                    }
        //                    catch { }
        //                }
        //                List.Add(obj);
        //            }
        //            conn.Close();
        //        }
        //    }
        //    return List;
        //}

        public void Add(object data)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                List<string> properties = new List<string>();
                List<string> values = new List<string>();

                string Datatable = data.GetType().Name;
                JObject json = JObject.FromObject(data);

                foreach (JProperty property in json.Properties())
                {
                    if (property.Name.ToLower() != "id")
                    {
                        properties.Add(property.Name);
                        values.Add(property.Value.ToString());
                    }
                }

                string prop = String.Join(",", properties);
                string val = String.Join("','", values);

                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO `" + Datatable + "` (" + prop + ") VALUES('" + val + "')";
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Edit(int? id, object data)
        {
            if (data != null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    List<string> properties = new List<string>();
                    List<string> values = new List<string>();

                    string Datatable = data.GetType().Name;
                    JObject json = JObject.FromObject(data);

                    foreach (JProperty property in json.Properties())
                    {
                        if (property.Name.ToLower() != "id" && property.Name.ToLower() != "created")
                        {
                            properties.Add(property.Name);
                            values.Add(property.Value.ToString());
                        }
                    }
                    List<string> editData = new List<string>();
                    int i = 0;
                    foreach (string prop in properties)
                    {
                        string newEdit = prop + "=" + "'" + values[i] + "'";
                        editData.Add(newEdit);
                        i++;
                    }

                    string update = String.Join(",", editData);

                    MySqlCommand command = conn.CreateCommand();
                    //command.CommandText = "INSERT INTO `" + Datatable + "` (" + prop + ") VALUES('" + val + "')";
                    command.CommandText = "UPDATE `" + Datatable + "` SET " + update + " WHERE id = " + id + "";
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public object Details(int? id, object table)
        {
            List<string> ColNames = new List<string>();

            dynamic expando = new ExpandoObject();

            object prop = table.GetType().GetProperties();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                foreach (var property in table.GetType().GetProperties())
                {
                    ColNames.Add(property.Name);
                }

                MySqlCommand command = new MySqlCommand("SELECT * FROM `" + table.GetType().Name + "` WHERE id=" + id + ";", conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        object data = new object();

                        //Sukuriu dinaminio objekto savybes
                        foreach (var a in ColNames)
                        {
                            AddProperty(expando, a, null);
                        }

                        //Uzsipildau statinio obekta sukurtais savybemis
                        var props = table.GetType().GetProperties();
                        var obj = Activator.CreateInstance(table.GetType());
                        var values = (IDictionary<string, object>)expando;
                        foreach (var propxx in props)
                            propxx.SetValue(obj, values[propxx.Name]);

                        //uzpildau objekta data
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            try
                            {
                                string x = reader[i].GetType().Name.ToString();
                                string value = reader[i].ToString();
                                if (reader[i].GetType().Name.Contains("String"))
                                {
                                    obj.GetType().GetProperty(ColNames[i]).SetValue(obj, value);
                                }
                                else if (reader[i].GetType().Name.ToString().Contains("Int"))
                                {
                                    obj.GetType().GetProperty(ColNames[i]).SetValue(obj, Int32.Parse(value));
                                }
                                else if (reader[i].GetType().Name.ToString().Contains("Date"))
                                {
                                    obj.GetType().GetProperty(ColNames[i]).SetValue(obj, reader[i].ToString());
                                }
                                else
                                    try
                                    {
                                        obj.GetType().GetProperty(ColNames[i]).SetValue(obj, Boolean.Parse(reader[i].ToString()));
                                    }
                                    catch { obj.GetType().GetProperty(ColNames[i]).SetValue(obj, false); }
                            }
                            catch
                            {
                                obj.GetType().GetProperty(ColNames[i]).SetValue(obj, "");
                            }
                        }
                        conn.Close();
                        return obj;
                    }

                }
            }
            //jei nepavyko atlikti
            return null;
        }
        public void Delete(int? id, object data)
        {
            if (data != null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    MySqlCommand command = conn.CreateCommand();
                    //command.CommandText = "INSERT INTO `" + Datatable + "` (" + prop + ") VALUES('" + val + "')";
                    command.CommandText = "DELETE FROM `" + data.GetType().Name + "` WHERE id = " + id + "";
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        //TODO add properties
        public List<Citys> GetItems()
        {
            List<Citys> cityList = new List<Citys>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `items`;", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cityList.Add(new Citys
                        {
                            id = (int)reader[1],
                            Name = reader[0].ToString()
                        });
                    }
                }
            }
            return cityList;
        }
    }
}
