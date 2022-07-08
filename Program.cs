using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PyramidCard
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PyramidForm f = new PyramidForm();
            f.Show();
            Application.Run();
            
        }
        
        /*static void Main()
        {

            String[] files = Directory.GetFiles("D:\\Documents\\homework\\c#\\PyramidCard\\bin\\Debug\\image");
            foreach (String filename in files)
            {
                int lastpath = filename.LastIndexOf("\\");
                int lastdot = filename.LastIndexOf(".");
                int length = lastdot - lastpath - 1;
                String beginpart = filename.Substring(0, lastpath + 1);
                String namenoext = filename.Substring(lastpath + 1, length);
                String ext = filename.Substring(lastdot);
                Card card = new Card();
                int newnameSuitIndex;
                string newnameSuit = "";
                int newnameValue;
                Console.WriteLine("文件夹："+beginpart);
                Console.WriteLine(namenoext);
                Console.WriteLine(ext);
                newnameValue = int.Parse(namenoext) / 4 + 1;
                newnameSuitIndex = int.Parse(namenoext) % 4;
                switch (newnameSuitIndex)
                {
                    case 0:newnameSuit = "club";break;
                    case 1:newnameSuit = "diamond";break;
                    case 2:newnameSuit = "heart";break;
                    case 3:newnameSuit = "spade";break;
                }
                String newName = newnameSuit + newnameValue;
                String fullnewname = beginpart + newName + ext;
                File.Move(filename, fullnewname);

            }
        
        }
        */
    }
}
