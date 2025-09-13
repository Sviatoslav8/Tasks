using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class UserLog
    {
        public string Login {  get; set; }
        public string Password { get; set; }
        public DateTime Date;
        
        public UserLog(string login,string password,DateTime date)
        {
            this.Login = login;
            this.Password = password;
            this.Date = date;
        }
        public UserLog()
        {

        }
    }
}
