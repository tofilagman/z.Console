using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace z.Console
{
    public abstract class CServer
    {

        private Dictionary<string, Action<dynamic>> Events = new Dictionary<string, Action<dynamic>>();

        /// <summary>
        /// Start the communication from program caller
        /// </summary>
        public void Start()
        {
            while (true)
            {
                string line = System.Console.ReadLine();
                if (line == "") continue;
                if (line.Substring(0, 2) == "->")
                {
                    string[] strlst = line.Substring(2).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
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
                    System.Console.WriteLine(line);
                }
            }
        }

        public void Invoke(string MethodName, params object[] args)
        {
            System.Console.WriteLine("->{0}|{1}", MethodName, string.Join("|", args));
        }

        public void On(string Name, Action<dynamic> callback)
        {
            if (this.Events.ContainsKey(Name)) return;
            Events.Add(Name, callback);
        }

    }
}
