using ExempleASPReactDAL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExempleASPReactDAL.Helpers
{
    public static class TitleHelper
    {
        public static string GetTitle(string type)
        {
            var result = "";
            DCT.Execute(c =>
            {
                switch (type)
                {
                    default:
                        result = "May be title?";
                        break;
                }
            });
            return result;
        }
    }
}
