using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartMES.Helpers
{
    public class DebugHelper
    {
        public static IDictionary<string, string> GetParameter(string queryString)
        {
            if (string.IsNullOrWhiteSpace(queryString)) return null;

            return ParseQueryString(queryString);
        }


        public static Dictionary<string, string> ParseQueryString(string s)
        {
            return Micube.Framework.SmartControls.Helpers.StringHelper.ParseQueryString(s);
        }
    }
}
