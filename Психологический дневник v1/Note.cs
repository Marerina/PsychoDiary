using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Психологический_дневник_v1
{
    public class Note
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Nastro { get; set; }
        public string category { get; set; }
        public DateTime date { get; set; }
        public string url { get; set; }

        public bool Save(string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(date);
                    sw.WriteLine(Nastro);
                    sw.WriteLine(category);
                    sw.WriteLine(Title);
                    sw.WriteLine(Text);
                }
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Open(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    date = DateTime.Parse(sr.ReadLine());
                    Nastro = sr.ReadLine();
                    category = sr.ReadLine();
                    Title = sr.ReadLine();
                    Text = sr.ReadToEnd();
                    url = path;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    
}
