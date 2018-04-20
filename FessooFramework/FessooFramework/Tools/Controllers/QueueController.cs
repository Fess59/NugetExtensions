using FessooFramework.Objects;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Controllers
{
    public abstract class QueueController<T> : SystemObject
        where T : QueueController<T>, new()
    {
        #region Property
        ConcurrentQueue<Action> queue = new ConcurrentQueue<Action>();
        int WaitCount { get; set; }
        public int WaitCountMax = 30;
        #endregion
        #region Methods
        internal abstract bool CheckCreate();
        internal abstract void Create(Action execute);
        public void Execute(Action executeAction, Action completeAction = null)
        {
            DCT.DCT.Execute(data2 =>
            {
                if (queue == null)
                    queue = new ConcurrentQueue<Action>();
                if (CheckCreate())
                    Create(execute);
                queue.Enqueue(() =>
                DCT.DCT.Execute(data =>
                {
                    if (executeAction == null) throw new NullReferenceException("QueueController.Execute Параметр не может быть пустым");
                    executeAction?.Invoke();
                    completeAction?.Invoke();
                }));
            });
        }

        private void execute()
        {
            DCT.DCT.Execute(c =>
            {
                WaitCount = 0;
                while (true)
                {
                    Action next;
                    var result = queue.TryDequeue(out next);
                    if (result && next != null)
                    {
                        next();
                        //ConsoleHelper.SendMessage($"QueueController {typeof(T).Name} execute");
                        WaitCount = 0;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        //ConsoleHelper.SendMessage($"QueueController {typeof(T).Name} wait");
                        WaitCount += 1;
                        if (WaitCount >= WaitCountMax)
                        {
                            DCT.DCT._Send($"QueueController {typeof(T).Name} complete");
                            break;
                        }
                    }
                }
            });
        }
        #endregion
        #region Singlton
        public static T Current { get { return getInstance(); } }
        private static T getInstance()
        {
            return Nested.current;
        }
        private class Nested
        {
            internal static readonly T current = new T();
        }
        private enum BootstrapperState
        {
            Create = 0,
            Load = 1,
            Run = 2,
            Complete = 3,
            Abort = 4
        }
        #endregion
    }
}
