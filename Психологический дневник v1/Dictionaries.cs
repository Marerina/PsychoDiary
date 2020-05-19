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
    public partial class Dictionaries : Form
    {
        public Dictionaries(List<string> nast, List<string> category)
        {
            InitializeComponent();
            this.nast = nast;
            this.category = category;
        }
        public List<string> nast;
        public List<string> category;

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            category.Add(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(textBox2.Text);
            nast.Add(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = "";
            foreach (var v in category)
                if (v == (string)listBox1.SelectedItem) s = (string)listBox1.SelectedItem;
            listBox1.Items.Remove(s);
            
            category.Remove(s);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string s = "";
            foreach (var v in nast)
                if (v == (string)listBox2.SelectedItem) s = (string)listBox2.SelectedItem;
            listBox2.Items.Remove(s);
            nast.Remove(s);
        }

        private void Dictionaries_Load(object sender, EventArgs e)
        {
            foreach (var v in category)
                listBox1.Items.Add(v);
                
            foreach (var v in nast)
                listBox2.Items.Add(v);
        }
    }
}
