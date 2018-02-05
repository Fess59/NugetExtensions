using FessooFramework.Objects;
using FessooFramework.Tools.DCT;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Controllers
{
    /// <summary>   A controller for handling actions.
    ///             Расширяет возможность Action с помощью очереди выполения и приоретета </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

    public class ActionController : SystemObject
    {
        #region Models
        public class PriorityAction
        {
            public Action Action { get; set; }
            public int Priority { get; set; }
            public PriorityAction(Action action, int priority)
            {
                Priority = priority;
                Action = action;
            }
        }
        #endregion
        #region Property

        /// <summary>   The dictionary. </summary>
        Dictionary<int, List<Action>> dict = new Dictionary<int, List<Action>>();
        #endregion
        #region Method
        /// <summary>
        /// Метод добавляет событие в список выполнения 
        /// С возможностью указать приоритет выполения и отлова ошибки в каждом отдельном методе вызова
        /// Чем выше значение приоритета тем раньше будет выполенен метод
        /// </summary>
        /// <param name="action"></param>
        /// <param name="priority"></param>
        public void Set(Action action, int priority = 0)
        {
            DCTDefault.Execute(c => {
                if (action == null) throw new NullReferenceException("ActionController. Вызываемый метод при заверешнии измения объекта не может быть равен NULL");
                DCTDefault.Send("Привязка создана объектом " + action.Target.ToString());
                if (!dict.ContainsKey(priority))
                    dict.Add(priority, new List<Action>());
                dict.FirstOrDefault(q => q.Key == priority).Value.Add(action);
            });
        }
        public void Execute()
        {
            DCTDefault.Execute((data) =>{
                try
                {
                    if (dict == null && !dict.Any()) return;
                    var collections = dict.OrderByDescending(q => q.Key).ToArray();
                    foreach (var list in collections)
                    {
                        foreach (var action in list.Value.ToArray())
                            execute(action);
                    }
                }
                catch (Exception ex)
                {
                    DCTDefault.SendExceptions(ex, "CRITICAL");
                    throw;
                }
            });
        }
        /// <summary>
        /// Метод удаляет подписка на событие 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="priority"></param>
        public void Remove(Action action)
        {
            DCTDefault.Execute((data) =>{
            if (action == null) throw new NullReferenceException("ActionController. Вызываемый метод при заверешнии измения объекта не может быть равен NULL");
                if (!dict.Any()) return;
                foreach (var list in dict.ToArray())
                {
                    if (list.Value.Any() && list.Value.Any(q => EqualityComparer<object>.Default.Equals(q, action)))
                        list.Value.RemoveAll(q => q.Equals(action));
                }
            });
        }
        private void execute(Action action)
        {
            DCTDefault.Execute((data) =>{
            try
            {
                    action();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("!!! Критическая ошибка при выполнении execute - один из Action вызывает ошибку при выполении!!!" + Environment.NewLine + ex);
                    throw;
                }
            });
        }
        public void Dispose()
        {
            ObjectHelper.Dispose(dict);
        }
        #endregion
        #region Operators
        public static ActionController operator +(ActionController obj, Action action)
        {
            obj.Set(action);
            return obj;
        }

        public static ActionController operator +(ActionController obj, PriorityAction priorityAction)
        {
            obj.Set(priorityAction.Action, priorityAction.Priority);
            return obj;
        }
        public static ActionController operator -(ActionController obj, Action action)
        {
            obj.Remove(action);
            return obj;
        }
        public static ActionController operator -(ActionController obj, PriorityAction priorytyAction)
        {
            obj.Remove(priorytyAction.Action);
            return obj;
        }
        #endregion
    }
}
