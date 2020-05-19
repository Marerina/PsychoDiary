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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            nast = new List<string>();
            category = new List<string>();
            all_notes = new List<Note>();
        }
        List<Note> all_notes;
        void LoadItems()
        {
            while (comboBox1.Items.Count > 0)
                comboBox1.Items.RemoveAt(0);
            while (comboBox2.Items.Count > 0)
                comboBox2.Items.RemoveAt(0);
            using (StreamReader sr = new StreamReader(path+ "items.cats"))
            {
                string s = sr.ReadLine();
                while (s != null)
                {
                    category.Add(s);
                    comboBox1.Items.Add(s);
                    s = sr.ReadLine();   
                }
                comboBox1.Items.Add("Все");
            }
            using (StreamReader sr = new StreamReader(path + "items.nastr"))
            {
                string s = sr.ReadLine();
                while (s != null)
                {
                    nast.Add(s);
                    comboBox2.Items.Add(s);
                    s = sr.ReadLine();
                }
                comboBox2.Items.Add("Все");
            }
        }

        void LoadData()
        {
            try
            {
                using (StreamReader sr = new StreamReader(configPath))
                {
                    path = sr.ReadLine();
                    if (path.Length == 0) throw new Exception();
                }
                
            }
            catch {
                if (MessageBox.Show("Файл config.cnf поврежден! Загрузка старых данных невозможна.\nПересоздать файл по умолчанию?", "Ошибка!", MessageBoxButtons.YesNo) == DialogResult.OK)
                {
                    using(StreamWriter sw = new StreamWriter(configPath))
                    {
                        sw.WriteLine(@"files\");
                        path = @"files\";
                        MessageBox.Show("Файл восстановлен по умолчанию");
                    }
                }
                else this.Close();
            }
            LoadItems();
            LoadFiles();
        }
        public void LoadFiles()
        {
            DirectoryInfo di = new DirectoryInfo(path);
            all_notes = new List<Note>();
            foreach(var v in di.GetFiles())
            {
                if(v.Extension == ".tx" && v.Name.Substring(0,10) == "stndrt-nt-")
                {
                    Note n = new Note();
                    n.Open(v.FullName);
                    all_notes.Add(n);
                }
            }
            while (listBox1.Items.Count > 0)
                listBox1.Items.RemoveAt(0);
            foreach (var v in all_notes)
            {
                listBox1.Items.Add(
                    string.Format("{0}.{1}.{2} | {3}",
                    (v.date.Day < 10) ? "0" + v.date.Day.ToString() : v.date.Day.ToString(),
                   (v.date.Month < 10) ? "0" + v.date.Month.ToString() : v.date.Month.ToString(), v.date.Year, v.Title));
            }
        }
        string configPath = @"config.cnf";
        string path;
        public List<string> nast;
        public List<string> category;
        private void обычныйДневникToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void записьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StandartNote sn = new StandartNote(path, nast,category);
            sn.MdiParent = this;
            sn.Show();
        }

        private void словариToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionaries d = new Dictionaries(nast, category);
            d.ShowDialog();
            nast = d.nast; category = d.category;
            using (StreamWriter sw = new StreamWriter(path + "items.cats"))
            {
                foreach(var v in category)
                    sw.WriteLine(v);
            }
            using (StreamWriter sw = new StreamWriter(path + "items.nastr"))
            {
                foreach (var v in nast)
                    sw.WriteLine(v);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            tabControl1.Visible = просмотретьЗаписиToolStripMenuItem.Checked;
            numericUpDown1.Maximum = this.Width / 2;
            numericUpDown1.Minimum = 240;
            numericUpDown1.Value = tabControl1.Width;
        }

        private void просмотретьЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = просмотретьЗаписиToolStripMenuItem.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            tabControl1.Width = int.Parse(numericUpDown1.Value.ToString());
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }


        void Filter()
        {
            while (listBox1.Items.Count > 0)
                listBox1.Items.RemoveAt(0);
            if ((comboBox2.SelectedItem == null||comboBox2.SelectedItem.ToString() == "Все" || 
                comboBox2.SelectedItem.ToString() == "" )&& 
                (comboBox1.SelectedItem == null||comboBox1.SelectedItem.ToString() == "Все" ||
                comboBox1.SelectedItem.ToString() == ""))
            {
                foreach (var v in all_notes)
                {
                    listBox1.Items.Add(
                        string.Format("{0}.{1}.{2} | {3}",
                        (v.date.Day < 10) ? "0" + v.date.Day.ToString() : v.date.Day.ToString(),
                       (v.date.Month < 10) ? "0" + v.date.Month.ToString() : v.date.Month.ToString(), v.date.Year, v.Title));
                }
            }
            else
                if(
                (comboBox2.SelectedItem == null||comboBox2.SelectedItem.ToString() == "Все" ||
                comboBox2.SelectedItem.ToString() == "" )
                )
            {
                foreach (var v in all_notes)
                {
                    if (v.category == comboBox1.SelectedItem.ToString())
                    {
                        listBox1.Items.Add(
                            string.Format("{0}.{1}.{2} | {3}",
                            (v.date.Day < 10) ? "0" + v.date.Day.ToString() : v.date.Day.ToString(), v.date.Month, v.date.Year, v.Title));
                    }
                }
            }
            else
                if(comboBox1.SelectedItem == null||comboBox1.SelectedItem.ToString() == "Все" ||
                comboBox1.SelectedItem.ToString() == "" )
            {
                foreach (var v in all_notes)
                {
                    if (v.Nastro == comboBox2.SelectedItem.ToString())
                    {
                        listBox1.Items.Add(
                            string.Format("{0}.{1}.{2} | {3}",
                            (v.date.Day < 10) ? "0" + v.date.Day.ToString() : v.date.Day.ToString(), v.date.Month, v.date.Year, v.Title));
                    }
                }
            }
            else
            {
                foreach (var v in all_notes)
                {
                    if (v.category == comboBox1.SelectedItem.ToString() && v.Nastro == comboBox2.SelectedItem.ToString())
                    {
                        listBox1.Items.Add(
                            string.Format("{0}.{1}.{2} | {3}",
                            (v.date.Day < 10) ? "0" + v.date.Day.ToString() : v.date.Day.ToString(), v.date.Month, v.date.Year, v.Title));
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(listBox1.SelectedItem != null || listBox1.SelectedItem.ToString() != "")
            {
                string s = listBox1.SelectedItem.ToString();
                listBox1.Items.Remove(s);
                Note n01 = new Note();
                foreach (var v in all_notes)
                    if (string.Format("{0}.{1}.{2} | {3}",
                    (v.date.Day < 10) ? "0" + v.date.Day.ToString() : v.date.Day.ToString(),
                   (v.date.Month < 10) ? "0" + v.date.Month.ToString() : v.date.Month.ToString(), v.date.Year, v.Title) == s) n01 = v;
                all_notes.Remove(n01);
                File.Delete(n01.url);
            }

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null || listBox1.SelectedItem.ToString() != "")
            {
                string s = listBox1.SelectedItem.ToString();
                Note n01 = new Note();
                foreach (var v in all_notes)
                    if (string.Format("{0}.{1}.{2} | {3}",
                    (v.date.Day < 10) ? "0" + v.date.Day.ToString() : v.date.Day.ToString(),
                   (v.date.Month < 10) ? "0" + v.date.Month.ToString() : v.date.Month.ToString(), v.date.Year, v.Title) == s) n01 = v;
                StandartNote stnt = new StandartNote(path, nast, category, n01);
                stnt.MdiParent = this;
                stnt.Show();
            }
        }

        private void чтоПоестьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FoodPlanner fp = new FoodPlanner();
            fp.ShowDialog();
        }

        private void новаяЗаметкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Memo m = new Memo(path,category);
            m.Show();
        }
    }
}
