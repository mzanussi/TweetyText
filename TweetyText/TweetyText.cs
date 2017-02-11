using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetyText
{
    class TweetyText
    {
        private const int TWEET_LEN = 140;
        private const string CONTINUED = "...";

        // Given a line of characters, returns an ArrayList breaking 
        // down the line into a series of lines <= 140 characters.
        private void ChunkIt(string line, ArrayList lines)
        {
            if (line.Length <= TWEET_LEN)
            {
                lines.Add(line);
            }
            else
            {
                int ptr = TWEET_LEN - CONTINUED.Length - 1;
                while (line[ptr] != ' ')
                {
                    ptr--;
                }
                string str = line.Substring(0, ptr);
                str = str.TrimEnd() + CONTINUED;
                lines.Add(str);
                ChunkIt(line.Substring(ptr + 1), lines);
            }
        }

        public ArrayList Process(string filename)
        {
            ArrayList file = new ArrayList();

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
                        if (cur_line.Length <= TWEET_LEN)
                        {
                            line_count++;
                            file.Add(cur_line);
                        }
                        else
                        {
                            ArrayList lines = new ArrayList();
                            ChunkIt(cur_line, lines);
                            foreach (string line in lines)
                            {
                                line_count++;
                                file.Add(line);
                            }
                        }
                    }

                }

            }

            return file;

        }

        static void Main(string[] args)
        {
            TweetyText p = new TweetyText();
            ArrayList lines = p.Process(@"C:\Temp\Data\handbook.txt");
            foreach (string line in lines)
            {
                Console.WriteLine("{0}", line);
            }
        }
    }
}
