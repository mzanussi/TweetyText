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
        private string kHashtag;

        /// <summary>
        /// Given a line of characters, returns an ArrayList breaking down the
        /// line into a series of lines less than are equal to 140 characters.
        /// </summary>
        private void ChunkIt(string line, ArrayList lines)
        {
            if (line.Length + 1 + kHashtag.Length <= TWEET_LEN)
            {
                lines.Add(line + " " + kHashtag);
            }
            else
            {
                int ptr = TWEET_LEN - CONTINUED.Length - kHashtag.Length - 1;
                while (line[ptr] != ' ')
                {
                    ptr--;
                }
                string str = line.Substring(0, ptr);
                str = str.TrimEnd() + CONTINUED + " " + kHashtag;
                lines.Add(str);
                ChunkIt(line.Substring(ptr + 1), lines);
            }
        }

        /// <summary>
        /// Give it a Gutenberg text file and get back a 
        /// string array of Tweetable strings.
        /// </summary>
        public string[] Tweetify(string filename)
        {
            ArrayList contents = new ArrayList();

            bool isDone = false;
            int line_count = 0;

            using (StreamReader sr = new StreamReader(filename))
            {
                // First line of file is a hashtag name for file.
                // Stop if no hashtag was found.
                if (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    if (line[0] != '#')
                    {
                        return null;
                    }
                    else
                    {
                        kHashtag = line;
                    }
                }
                else
                {
                    return null;
                }

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
                        // Trim line first.
                        cur_line = cur_line.TrimEnd();
                        // If line is <= 140 characters (with hashtag added) just output 
                        // the line plus the hashtag. Otherwise, break up line into 
                        // <= 140 chararacter chunks.
                        if (cur_line.Length + 1 + kHashtag.Length <= TWEET_LEN)
                        {
                            line_count++;
                            contents.Add(cur_line + " " + kHashtag);
                        }
                        else
                        {
                            ArrayList lines = new ArrayList();
                            ChunkIt(cur_line, lines);
                            foreach (string line in lines)
                            {
                                line_count++;
                                contents.Add(line);
                            }
                        }
                    }
                }
            }

            return (string[])contents.ToArray(typeof(string));
        }

        static void Main(string[] args)
        {
            TweetyText p = new TweetyText();
            string[] lines = p.Tweetify(@"C:\Temp\Data\handbook.txt");
            foreach (string line in lines)
            {
                Console.WriteLine("{0}", line);
            }
        }
    }
}
