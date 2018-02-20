using FessooFramework.Tools.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FessooFramework.Tools.Helpers
{
    public class WebWorker : QueueController<WebWorker>
    {
        #region Property
        private Thread WorkThread { get; set; }
        internal System.Windows.Forms.WebBrowser WebBrowser { get; set; }
        internal HtmlDocument WebDocument
        {
            get
            {
                HtmlDocument document = null;
                DCT.DCT.ExecuteMainThread(d => document = WebBrowser.Document);
                return document;
            }
        }
        internal Action IntializationComplite { get; set; }
        #endregion
        #region Methods
        internal override bool CheckCreate()
        {
            return WorkThread == null || !WorkThread.IsAlive;
        }
        internal override void Create(Action execute)
        {
            WorkThread = new Thread(() => execute());
            WorkThread.SetApartmentState(ApartmentState.STA);
            WorkThread.IsBackground = true;
            WorkThread.Start();
            IntializationComplite?.Invoke();
        }
        public void SetBrowser(WebBrowser browser)
        {
            WebBrowser = browser;
        }
        #endregion
    }
}
