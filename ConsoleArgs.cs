using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace z.Console
{
    public class ConsoleArgs : Dictionary<string, object>
    {
        public ConsoleArgs(string[] Arguments)
        {
            foreach (string s in Arguments)
            {
                this.Add(s.Substring(1, 1), s.Substring(2));
            }
        }

        public string GetString(string key)
        {
            return Convert.ToString(this[key]);
        }

        public Int32 GetInt(string key)
        {
            return Convert.ToInt32(this[key]);
        }
    }
}
