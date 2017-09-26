using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace z.Console
{
    /// <summary>
    /// LJ 20150907
    /// Helper for communicating in console app
    /// </summary>
    public class CClient : IDisposable
    {

        public ConsoleApp app { get; private set; }
        public string appPath { get; private set; }

        private Dictionary<string, Action<dynamic>> Events = new Dictionary<string, Action<dynamic>>();

        public delegate void StatusHandler(CClient client, string Message);
        public event StatusHandler OnStatus;

        public CClient(string appPath)
        {
            this.appPath = appPath;
            this.app = new ConsoleApp(this.appPath);
            this.app.ConsoleOutput += App_ConsoleOutput;
            this.app.Exited += App_Exited;
        }

        public void Start()
        {
           // this.app.CreateNoWindow = false;
            this.app.Run();
        }

        public void Stop()
        {
            this.app.Stop();
        }

        public virtual void App_Exited(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void App_ConsoleOutput(object sender, ConsoleOutputEventArgs e)
        {
            if (e.Line == "") return;
            if (e.Line.Substring(0, 2) == "->")
            {
                string[] strlst = e.Line.Substring(2).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var action = Events.Where(x => x.Key == strlst[0]);
                if (action.Any())
                {
                    try
                    {

                        var act = action.SingleOrDefault();
                        act.Value(strlst.Skip(1).ToArray());
                    }
                    catch(Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                if (OnStatus != null) OnStatus(this, e.Line);
            }
        }

        public void Invoke(string MethodName, params object[] args)
        {
            this.app.WriteLine("->{0}|{1}", MethodName, string.Join("|", args));
        }

        public void On(string Name, Action<dynamic> callback)
        {
            if (this.Events.ContainsKey(Name)) return;
            this.Events.Add(Name, callback);
        }
        
        public void Dispose()
        {
            this.app.Dispose();
            this.Events = null;
            GC.Collect();
            GC.SuppressFinalize(this);
        }
        
    }
}
