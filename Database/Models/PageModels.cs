using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Models
{
    public class CityList
    {
        public List<Citys> citys { get; set; }
    }


    public class UserCreate
    {
        public Users user { get; set; }
        public List<Citys> Citys { get; set; }
        public List<Education> Educations { get; set; }
    }
    public class UserList
    {
        public List<Users> users { get; set; }
    }
}
