using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetyText
{
    class Program
    {
        static void Main(string[] args)
        {
            //foreach (string line in File.ReadLines(@"C:\Temp\Data\mands.txt"))
            //{
            //    string cline = "";
            //}
            string filename = @"C:\Temp\Data\mands.txt";

            bool isDone = false;

            using (StreamReader sr = new StreamReader(filename))
            {
                while (!isDone) {

                    string cur_line = "";

                    // Read in text until end of paragraph has been reached
                    // (empty line) or end of file is encountered. Append
                    // the text to the current line.
                    while (true)
                    {
                        if (sr.Peek() >= 0)
                        {
                            string line = sr.ReadLine();
                            if (string.IsNullOrEmpty(line))
                            {
                                break;
                            }
                            cur_line += line + " ";
                        }
                        else
                        {
                            // end of file
                            isDone = true;
                            break;
                        }

                    }

                    if (cur_line.Length > 0)
                    {
                        // trim first
                        cur_line = cur_line.TrimEnd();
                        // if line is <= 140 characters just output the line
                        // otherwise, break up line into <= 140 char chunks.
                        if (cur_line.Length <= 140)
                        {
                        }
                        else
                        {
                        }
                    }

                }

            }

        }
    }
}
