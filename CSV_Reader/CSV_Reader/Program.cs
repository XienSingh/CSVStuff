using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Reader
{
    class Program
    {
        static void Main(string[] args)
        {

            bool isheader = true;
            var reader = new StreamReader(File.OpenRead(@"~\Test.csv"));
            List<string> headers = new List<string>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                if (isheader)
                {
                    isheader = false;
                    headers = values.ToList();
                }
                else
                {
                    int i = 0;
                    for (i = 0; i < values.Length; i++)
                    {
                        Console.Write(string.Format("\n ##{0}## \n {1}", headers[i], values[i]));

                    }
                    Console.WriteLine("\n\t-------");

                }
            }
        }
    }
}