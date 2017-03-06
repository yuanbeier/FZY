using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Topevery.FMP.Data.OracleClient
{
    public class Common
    {
        public static System.Data.OracleClient.OracleParameter GetCursorParameter(string name)
        {
            System.Data.OracleClient.OracleParameter result = new System.Data.OracleClient.OracleParameter();
            result.OracleType = System.Data.OracleClient.OracleType.Cursor;
            result.Direction = System.Data.ParameterDirection.Output;
            result.ParameterName = name;
            result.Value = DBNull.Value;
            return result;
        }
    }
}
