using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace 마크서버_만들기
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string data)
        {
            InitializeComponent();
            label5.Text = data;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label1.Text = "M";
                label2.Text = "M";
                textBox1.Text = "4080";
                textBox2.Text = "4080";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                label1.Text = "G";
                label2.Text = "G";
                textBox1.Text = "4";
                textBox2.Text = "4";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("선택하세요! \n\r M 와 G 가 있습니다");
            }
            else if(comboBox1.SelectedIndex == 0)
            {
                int aq=int.Parse(textBox1.Text);
                int aw=int.Parse(textBox2.Text);
                string AA = Path.GetFileName(label5.Text);
                string AB = Path.GetDirectoryName(label5.Text);
                string BA = label1.Text;
                BA = BA.ToLower();
                if(aq <4080 ||aw <4080)
                {
                    MessageBox.Show("4080 이하를 하시면 안됩니다");
                }
                else
                {
                    string[] lines = { "@echo off", "java -Xmx" +aq + "M" + " -Xms" + aw + "M" +" -jar "+ AA+ " nogui", "pause"};
                    File.WriteAllLines(AB+@"\comms.bat",lines);
                    Process A =new Process();
                    A.StartInfo.FileName = "comms.bat";
                    A.StartInfo.WorkingDirectory = AB;
                    A.Start();
                    A.WaitForExit(1000);
                    File.Delete(AB + @"\comms.bat");
                    Close();
                }
            }
            else if(comboBox1.SelectedIndex == 1) 
            {
                int aq= int.Parse(textBox1.Text);
                int aw= int.Parse(textBox2.Text);
                string aa = Path.GetFileName(label5.Text);
                string ab = Path.GetDirectoryName(label5.Text);
                string BA = label1.Text;
                BA= BA.ToLower();
                if(aq <4 || aw <4)
                {
                    MessageBox.Show("4 이하를 하시면 안됩니다");
                }
                else
                {
                    string[] lines = { "@echo", "java -Xmx" + aq + "G" + " -Xms" + aw+ "G" + " -jar " + aa + " nogui", "pause" };
                    File.WriteAllLines(ab + @"\comms.bat",lines);
                    Process A =new Process();
                    A.StartInfo.FileName = "comms.bat";
                    A.StartInfo.WorkingDirectory = ab;
                    A.Start();
                    A.WaitForExit(1000);
                    File.Delete(ab + @"\comms.bat");
                    Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}
