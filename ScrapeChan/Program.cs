using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeChan
{
    class Program
    {
        static void Main(string[] args)
        {
            var os = Environment.OSVersion;

            if (os.Platform.Equals("Win32NT"))
            {
                //
            }
            else if (os.Platform.Equals("MacOSX"))
            {
                //
            }
            else if (os.Platform.Equals("Unix"))
            {
                //
            }
        }
    }
}
