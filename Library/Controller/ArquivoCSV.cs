using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ArquivoCSV
    {
        public static string DirectoryPath = @"\5by5-ativLibrary";

        public static bool CriarDiretorio()
        {
            bool criou = false;
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
                criou = true;
            }
            return criou;
        }

        public virtual List<string> Leitura(string fileName)
        {
            List<string> lines = new List<string>();
            
            return lines = File.ReadAllLines(DirectoryPath + "\\" + fileName).ToList();
        }
    }
}
