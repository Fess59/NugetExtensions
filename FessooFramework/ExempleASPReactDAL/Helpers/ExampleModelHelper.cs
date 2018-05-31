using ExempleASPReactDAL.Core;
using ExempleASPReactDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExempleASPReactDAL.Helpers
{
    public static class ExampleModelHelper
    {
        static List<ExampleModel> models = new List<ExampleModel>();

        static ExampleModelHelper()
        {
         
        }

        public static ExampleModel Add(string title, string description)
        {
            ExampleModel result = null;
            DCT.Execute(c => 
            {
                result = new ExampleModel();
                result.Title = title;
                result.Description= description;
                models.Add(result);
            });
            return result;
        }
        public static IEnumerable<ExampleModel> All()
        {
            Add("1", "Один");
            Add("11", "First");
            Add("2", "Второй");
            Add("22", "Second");
            return models.ToArray();
        }
    }
}
