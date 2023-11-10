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
using System.Diagnostics;

namespace 마크서버_만들기
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            if (File.Exists("text.lal"))
            {
                StreamReader stream = new StreamReader("text.lal");
                textBox1.Text = stream.ReadLine();
                stream.Close();
            }
            else
            {
                textBox1.Text = "서버 버킷을 찾아 주세요";
            }

            if (File.Exists(textBox1.Text))
            {
                //파일이 있을경우 넘기고... 
            }
            else
            {
                textBox1.Text = "파일이 삭제 된것 같습니다!";
            }
        }

    private void button1_Click(object sender, EventArgs e)
        {
            //서버준비
            //Form2로
            //파일 있는지 채크
            string m = Path.GetDirectoryName(textBox1.Text);
            if (Directory.Exists(m))
            {
                Form2 form2 = new Form2(textBox1.Text);
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("파일이 없습니다");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //서버 월드
            //파일 있는지 채크
            string m = Path.GetDirectoryName(textBox1.Text);
            if (Directory.Exists(m))
            {
                if (Directory.Exists(m + @"\world_nether"))
                {
                    MessageBox.Show("세이브 파일을 열린 파일들에 넣으시면 됩니다");
                    Process n = new Process();
                    n.StartInfo.FileName = m + @"\world";
                    n.Start();
                    Process b = new Process();
                    b.StartInfo.FileName = m + @"\world_nether";
                    b.Start();
                    Process C = new Process();
                    C.StartInfo.FileName = m + @"\world_the_end";
                    C.Start();
                }
                else if (Directory.Exists(m + @"\world2"))
                {
                    MessageBox.Show("세이브 파일을 열린 파일들에 넣으시면 됩니다");
                    Process n = new Process();
                    n.StartInfo.FileName = m + @"world";
                    n.Start();
                    Process b = new Process();
                    b.StartInfo.FileName = m + @"\world2";
                    b.Start();
                    Process C = new Process();
                    C.StartInfo.FileName = m + @"\world3";
                    C.Start();
                }
                else
                {
                    MessageBox.Show("너무 구버전 이신가 봐요! \n"+m+@"\world"+"\n 세이브 파일을 넣으시면 됩니다");
                }
            }
            else
            {
                MessageBox.Show("파일이 없습니다 \n 서버 여신후 해보세요!");
            }
        }
    

        private void button3_Click(object sender, EventArgs e)
        {
            //서버 플러그인
            //파일 있는지 확인
            string B = Path.GetDirectoryName(textBox1.Text);
            if(Directory.Exists(B+@"\plugins"))
            {
                MessageBox.Show("파일을 넣은후 다시시작 하세요!");
                Process n = new Process();
                n.StartInfo.FileName = B + @"\plugins";
                n.Start();
            }
            else
            {
                MessageBox.Show("실행후 클릭해 주세요!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //서버찾기
            MessageBox.Show("서버 찾기 입니다 다운 아닙니다!!");
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "자바(*.jar)|*.jar";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = Path.GetFullPath(dialog.FileName);
                string[] strings = textBox1.Text.Split();
                File.WriteAllLines("text.lal", strings);
            }
            else
            {
                MessageBox.Show("죄송합니다 서버 다운은 코드 보기에서 참조하세요");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ghrl1121/minecraft_helper_server");
        }
    }
}
