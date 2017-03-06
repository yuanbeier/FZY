using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Web.Services.Tokens;

namespace Topevery.FMP.Logic.WebService
{
    public class WebServiceUserNameTokenManager : UserNameTokenManager 
    {
        protected override string AuthenticateToken(UserNameToken token)
        {
            return token.UserName;
        }
    }
}
