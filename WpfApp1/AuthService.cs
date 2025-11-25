using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Data;
using WpfApp1.Models;


namespace WpfApp1
{
    public class AuthService
    {
        public Role TryAuth(string login, string password)
        {
            using (var context = new TestContext())
            {
                User user = context.Users
                    .FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user == null) return null;

                return context.Roles.FirstOrDefault(r => r.Id == user.RoleId);
            }
        }
    }
}
