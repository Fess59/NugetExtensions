using FessooFramework.Tools.DCT;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExampleWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WebWorkerQueueTest();
            WebWorker.Current.Execute(() => WebWorker.Current.SetBrowser(new System.Windows.Forms.WebBrowser()));
            WebWorker.Current.DownloadPage("https://spinningline.ru/katushki-c-7.html?page=74&allproducts=true&", download);
        }
        private static void download(string obj)
        {
            var r = 1;
        }
        private void WebWorkerQueueTest()
        {
            DCT.ExecuteAsync(c => {
                ConsoleHelper.SendMessage("Start");
                //Start
                for (int i = 0; i < 100; i++)
                {
                    var ii = i;
                    var result = ii.ToString();
                    WebWorker.Current.Execute(() =>
                    {
                        Thread.Sleep(100 - ii);
                        ConsoleHelper.SendMessage(result);
                    });
                }
                Thread.Sleep(10000);
                //Continue
                for (int i = 101; i < 130; i++)
                {
                    var result = i.ToString();
                    WebWorker.Current.Execute(() => ConsoleHelper.SendMessage(result));
                }
                //Break
                Thread.Sleep(35000);
                //New start
                for (int i = 131; i < 250; i++)
                {
                    var result = i.ToString();
                    WebWorker.Current.Execute(() => ConsoleHelper.SendMessage(result));
                }
                ConsoleHelper.SendMessage("Finish");
            });
           
        }
    }
}
