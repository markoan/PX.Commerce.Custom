using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{

    /// <summary>
    /// Class for request segments
    /// </summary>
    public class UrlSegments
    {
        private readonly Dictionary<string, string> _urlSegments = new Dictionary<string, string>();

        public void Add(string key, string value)
        {
            _urlSegments.Add(key, value);
        }

        public void Delete(string key)
        {
            if (_urlSegments.ContainsKey(key))
            {
                _urlSegments.Remove(key);
            }
        }

        public Dictionary<string, string> GetUrlSegments()
        {
            return _urlSegments;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in _urlSegments)
            {
                sb.Append(pair.Key);
                sb.Append("=");
                sb.Append(pair.Value);
                sb.Append(";");
            }
            return sb.ToString();
        }
    }
}
