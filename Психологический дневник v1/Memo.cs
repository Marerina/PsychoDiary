using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Психологический_дневник_v1
{
     public partial class Memo : Form
    {
        
        /// <summary>
        /// Инициализация заметки
        /// </summary>
        public Memo(string path, List<string> categories)
        {
            InitializeComponent();
            textBox2.Visible = button1.Visible = false;
            Categories = categories;
            foreach (var v in Categories)
                comboBox1.Items.Add(v);
        }
        public List<string> Categories { get; set; }
        public string Link { get; set; }
        public string Title { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string Text { get { return richTextBox1.Text; } set { richTextBox1.Text = value; } }
        public string Category { get { return (string)comboBox1.SelectedItem; } set { comboBox1.SelectedItem = value; } }
        public List<Images> images { get; set; }
        string path;
        private void button2_Click(object sender, EventArgs e)
        {

        }
        public bool Save(string path)
        {
            DateTime dt = DateTime.Now;
            string memoname = @"\mmotxt_" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString();
            string memoimage_name= @"\mmoimg_"+dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString();
            try
            {
                using (StreamWriter sw = new StreamWriter(path+memoname))
                {
                    sw.WriteLine(Category);
                    sw.WriteLine(Link);
                    sw.WriteLine(Title);
                    sw.WriteLine(Text);
                }
                using(StreamWriter sw = new StreamWriter(path + memoimage_name))
                {
                    foreach(Images v in images)
                    {
                        sw.WriteLine(v.Url + '$' + v.Title);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category = (string)comboBox1.SelectedItem;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox2.Visible = button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Categories.Add(textBox2.Text);
            comboBox1.Items.Add(textBox2.Text);
        }
    }
    public class Images { public string Title { get; set; } public string Url { get; set; } public Images(string t, string u) { Title = t; Url = u; } }

}
