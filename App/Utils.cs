using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damas.App {
    internal class Utils {

        public static string ReadLine(string text = "") {
            Console.Write(text + ": ");
            return Console.ReadLine().Trim();
        }

    }
}
