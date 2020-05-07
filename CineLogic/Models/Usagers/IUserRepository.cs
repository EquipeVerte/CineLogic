using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineLogic.Models.Usagers
{
    interface IUserRepository
    {
        User ValidateUser(LoginViewModel login);

        void UpdateUser(PasswordChangeViewModel pass);

        int SaveChanges();
    }
}
