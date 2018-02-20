using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Controllers
{
    public class QueueTaskController : QueueController<QueueTaskController>
    {
        #region Property
        private Task task { get; set; }
        #endregion
        internal override bool CheckCreate()
        {
           return task == null || task.IsCompleted;
        }

        internal override void Create(Action execute)
        {
            task = Task.Factory.StartNew(execute);
        }
    }
}
