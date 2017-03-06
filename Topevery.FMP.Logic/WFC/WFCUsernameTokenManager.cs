#if WCF
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Text;

namespace Topevery.FMP.Logic.WFC
{
    public class WFCUsernameTokenManager : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (null == userName || null == password)
            {
                throw new ArgumentNullException();
            }

            if (userName != password)
            {
                throw new SecurityTokenException("Unknown Username or Incorrect Password");
            }
        }
    }
}
#endif