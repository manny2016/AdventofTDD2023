using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_1_Trebuchet
{
    class Calibration
    {
        public Calibration() { }
        static void Main(string[] args)
        {
            //FileStream input = new FileStream("./input.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            //StreamReader input = new StreamReader("./input.txt", Encoding.UTF8);
            string path = "../../../input.txt";
            string[] input = File.ReadAllLines(path);
            TextLinesPart2 textLines = new TextLinesPart2(input);
            Console.WriteLine(textLines.getSum());
        }
    }
}
