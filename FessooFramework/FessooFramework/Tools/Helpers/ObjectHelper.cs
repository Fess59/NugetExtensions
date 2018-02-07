using FessooFramework.Tools.DCT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Helpers
{
    /// <summary>   An object helper.
    ///             Набор инструментов для рыботы с объектами </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

    public static class ObjectHelper
    {
        /// <summary>
        /// Releases the unmanaged resources used by the FessooFramework.Tools.Helpers.ObjectHelper and
        /// optionally releases the managed resources.
        /// Если объект является Task и он в сотоянии RanToCompletion / Canceled / Faulted, то объект пройдёт процесс Dispose
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>
        ///
        /// <param name="obj">  The object. </param>

        public static void Dispose(object obj)
        {
            var comment = "";
            DCT.DCT.Execute((data) =>
            {
                if (obj == null) return;
                if (obj is IDisposable)
                {
                    if (obj is System.Threading.Tasks.Task)
                    {
                        var t = obj as System.Threading.Tasks.Task;
                        if (t.Status != System.Threading.Tasks.TaskStatus.RanToCompletion && t.Status != System.Threading.Tasks.TaskStatus.Canceled && t.Status != System.Threading.Tasks.TaskStatus.Faulted)
                            return;
                        comment += t.Status;
                    }
                    ((IDisposable)obj).Dispose();
                }
                obj = null;
            }, /* TODO DCT comment: comment,*/ continueExceptionMethod: (data, ex) => { comment += ex.ToString(); });

        }
    }
}
