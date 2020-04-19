using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;

namespace MyApp
{
    class MyCustUsernamepassValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            var qr = from u in UserData.GetSampleDatas()
                     where (u.Username == userName) && (u.Userpassword == password)
                     select u;
            if(qr.Count() == 0)
            {
                throw new Exception("用户名或密码错误");
            }
        }
    }
}
