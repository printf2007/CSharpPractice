using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;

namespace MyApp
{
    class CustAuthorPolicy : IAuthorizationPolicy
    {
        Guid pid = Guid.NewGuid();

        public ClaimSet Issuer => ClaimSet.System;

        public string Id => pid.ToString();

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            // 获取用户标识
            List<IIdentity> idts = evaluationContext.Properties["Identities"] as List<IIdentity>;
            if (idts != null && idts.Count > 0)
            {
                IIdentity usid = idts[0];
                // 查找用户信息
                UserData[] users = UserData.GetSampleDatas();
                UserData userinfo = users.FirstOrDefault(su => su.Username == usid.Name);
                if (userinfo != null)
                {
                    // 获得用户角色
                    string role = userinfo.Role;
                    // 创建安全实体
                    GenericPrincipal princp = new GenericPrincipal(usid, new string[] { role });
                    evaluationContext.Properties["Principal"] = princp;
                }
            }

            // 添加到授权上下文
            evaluationContext.AddClaimSet(this, this.Issuer);
            return true;
        }
    }
}
