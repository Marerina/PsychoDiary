using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Психологический_дневник_v1
{
    public partial class StandartNote : Form
    {
        bool hasNote = false;
        Note n;
        public bool b = true;
        public List<string> nast;
        public List<string> category;
        string path;
        public StandartNote(string path, List<string> nast, List<string> category)
        {
            InitializeComponent();
            n = new Note();
            
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Text = "Новая запись";
            n.Text = textBox1.Text;
            this.path = path;
            this.nast = nast;
            this.category = category;
            foreach (var v in nast)
                comboBox2.Items.Add(v);
            foreach (var v in category)
                comboBox1.Items.Add(v);
        }
        public StandartNote(string path, List<string> nast, List<string> category, Note note)
        {
            hasNote = true;
            InitializeComponent();
            n = note;

            dateTimePicker1.Value = note.date;
            textBox1.Text = note.Title;
            richTextBox1.Text = note.Text;
            
            this.path = path;
            this.nast = nast;
            this.category = category;
            foreach (var v in nast)
                comboBox2.Items.Add(v);
            foreach (var v in category)
                comboBox1.Items.Add(v);
            comboBox1.SelectedItem = n.category;
            comboBox2.SelectedItem = n.Nastro;
        }
        void Save()
        {
            n.date = dateTimePicker1.Value;
            n.category = (comboBox1.SelectedItem == null) ? "" : comboBox1.SelectedItem.ToString();
            n.Nastro = (comboBox2.SelectedItem == null) ? "" : comboBox2.SelectedItem.ToString();
            n.Title = textBox1.Text;
            n.Text = richTextBox1.Text;
            n.Save(hasNote?n.url:path+"stndrt-nt-"+dateTimePicker1.Value.Year.ToString() + dateTimePicker1.Value.Month.ToString() + dateTimePicker1.Value.Day.ToString() + dateTimePicker1.Value.Hour.ToString() + dateTimePicker1.Value.Minute.ToString() + dateTimePicker1.Value.Second.ToString()+".tx");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void StandartNote_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            b = false;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.Text = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Save();
           ( (Form1)this.MdiParent).LoadFiles();
            this.Close();
        }
    }
}
