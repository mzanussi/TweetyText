using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetyText
{
    class Program
    {
        // Given a line of characters, returns an ArrayList breaking 
        // down the line into a series of lines <= 140 characters.
        private void ChunkIt(string line, ArrayList lines)
        {
            if (line.Length <= 140)
            {
                lines.Add(line);
            }
            else
            {
                int ptr = 140 - "...".Length - 1;
                while (line[ptr] != ' ')
                {
                    ptr--;
                }
                string str = line.Substring(0, ptr);
                str = str.TrimEnd() + "...";
                lines.Add(str);
                ChunkIt(line.Substring(ptr + 1), lines);
            }
        }

        public void Process(string filename)
        {
            bool isDone = false;
            int line_count = 0;

            using (StreamReader sr = new StreamReader(filename))
            {
                while (!isDone)
                {
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
                            line_count++;
                            Console.WriteLine("{0}: {1}", line_count, cur_line);
                        }
                        else
                        {
                            ArrayList lines = new ArrayList();
                            ChunkIt(cur_line, lines);
                            foreach (string line in lines)
                            {
                                line_count++;
                                Console.WriteLine("{0}: {1}", line_count, line);
                            }
                        }
                    }

                }

            }

        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Process(@"C:\Temp\Data\mands.txt");
        }
    }
}
