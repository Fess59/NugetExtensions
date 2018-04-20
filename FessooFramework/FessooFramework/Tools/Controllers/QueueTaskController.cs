using FessooFramework.Tools.Helpers;
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
        public int TaskCount = 1;
        private Task task { get; set; }
        private List<Task> tasks = new List<Task>();
        private object taskLock = new Object();
        #endregion
        internal override bool CheckCreate()
        {
            if (TaskCount == 1)
                return task == null || task.IsCompleted;
            else
            {
                lock (taskLock)
                {
                    tasks = tasks.Where(q => !q.IsCompleted).ToList();
                    var count = tasks.Count;
                    //ConsoleHelper.SendMessage($"QueueTaskController count execute task {count}. Free task quota {TaskCount - count}");
                    var result = count < TaskCount;
                    if (result)
                    {
                        //ConsoleHelper.SendMessage($"QueueTaskController count execute task {count}. Free task quota {TaskCount - count}");
                    }
                    return result;
                }
            }
        }

        internal override void Create(Action execute)
        {
            if (TaskCount == 1)
                task = Task.Factory.StartNew(execute);
            else
                tasks.Add(Task.Factory.StartNew(execute));
        }
    }
}
