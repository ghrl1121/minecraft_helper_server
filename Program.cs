using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 마크서버_만들기
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Java"))
            {
                string A = Environment.SpecialFolder.ProgramFiles + @"\Java";
                MessageBox.Show(A + "입니다");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Java"))
            {
                string B = Environment.SpecialFolder.ProgramFilesX86 + @"\Java";
                MessageBox.Show(B + "(x86)" + "입니다");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else if(Directory.Exists(@"C:\Program Files\java"))
            {
                string C = "C:\\Program Files\\java";
                MessageBox.Show(C + "입니다");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else if(Directory.Exists(@"C:\Program Files (x86)\java"))
            {
                string D = "C;\\Program Files (x86)\\java";
                MessageBox.Show(D + "입니다");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                MessageBox.Show("자바가 없습니다! 실행이 취소 됩니다 \r\n 자바설치후에 다시 실행 하세요!");              
            }
            
        }
    }
}
