using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class TestInheritance
    {
        public virtual void Set()
        {
            Console.WriteLine("The First");
        }
    }

    public class TestInheritance_2 : TestInheritance
    {
        public override void Set()
        {
            base.Set();
            Console.WriteLine("The Second");
        }
    }

    public class TestInheritance_3 : TestInheritance_2
    {
        public override void Set()
        {
            base.Set();
            Console.WriteLine("The Third");
        }
    }
}
