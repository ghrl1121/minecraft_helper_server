using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using NetFwTypeLib;


namespace 마크서버_만들기
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //공부중
            string ID = (textBox1.Text);
            int id = int.Parse(textBox1.Text);
            Class1.FirewallHelper.OpenPortFirewall(ID,id) ;
            MessageBox.Show("추가가 되었습니다");
            Close();
        }
    }
}
