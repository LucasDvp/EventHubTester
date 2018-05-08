using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubTester.FileDealing
{
    public class LocalFileAccess
    {
        public LocalFileAccess() { }
        public static string ReadFile(string path)
        {
            try
            {
                using (var sr = new StreamReader(path))
                    return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static bool WriteFile(string path, string text)
        {
            try
            {
                using (var sw = new StreamWriter(path))
                    sw.WriteLine(text);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
