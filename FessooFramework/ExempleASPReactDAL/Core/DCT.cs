using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExempleASPReactDAL.Core
{
    public static class DCT
    {
        public static void Execute(Action<DCTContext> execute)
        {
            try
            {
                if (execute == null)
                    throw new Exception("Execute action cannot be null");
                execute?.Invoke(new DCTContext());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
