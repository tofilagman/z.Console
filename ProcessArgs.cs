using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace z.Console
{
    public class ProcessArgs
    {
        private StringBuilder sb = new StringBuilder();

        public ProcessArgs()
        {

        }

        public ProcessArgs(Dictionary<string, object> parameters)
        {
            foreach (KeyValuePair<string, object> param in parameters)
            {
                sb.AppendFormat("-{0}\"{1}\"", param.Key, param.Value);
                sb.Append(" ");
            }
        }

        public void Add(string Letter, object value)
        {
            sb.AppendFormat("-{0}\"{1}\"", Letter, value);
            sb.Append(" ");
        }

        public string GetArgumentString()
        {
            return this.sb.ToString();
        }
    }
}
