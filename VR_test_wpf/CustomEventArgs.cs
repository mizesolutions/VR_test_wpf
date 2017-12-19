using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR_test_wpf
{
    public class CustomEventArgs
    {
        public CustomEventArgs(string key)
        {
            _key = key;
        }
        private string _key;

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
    }
}
