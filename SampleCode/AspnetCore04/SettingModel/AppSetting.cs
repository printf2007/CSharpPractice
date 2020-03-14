using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCore04.SettingModel
{
    public class AppSetting
    {
        public string ConnectionString { get; set; }

        public WebSetting WebSetting { get; set; }
    }
}
