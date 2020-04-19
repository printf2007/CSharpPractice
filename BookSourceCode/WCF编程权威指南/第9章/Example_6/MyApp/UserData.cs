using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    class UserData
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Userpassword { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        public static UserData[] GetSampleDatas() => new UserData[]
            {
                new UserData{ Username = "Bob", Userpassword = "123", Role = Roles.Admin },
                new UserData { Username = "Tim", Userpassword = "456", Role = Roles.User }
            };
    }

    class Roles
    {
        /// <summary>
        /// 表示管理员
        /// </summary>
        public const string Admin = "admin";
        /// <summary>
        /// 表示普通用户
        /// </summary>
        public const string User = "user";
    }
}
