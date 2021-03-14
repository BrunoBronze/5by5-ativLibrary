using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Model;
using System.Globalization;

namespace Controller
{
    public class ArquivoEmprestimo : ArquivoCSV
    {
        public string fileName = @"\EMPRESTIMO.csv";

        public void CriarArquivo()
        {
            string filePath = $@"{DirectoryPath}\{fileName}";
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("IdCliente;NumeroTombo;DataEmprestimo;DataDevolucao;StatusEmprestimo");
                }
            }
        }

        public List<EmprestimoLivro> Leitura()
        {
            List<string> lines = base.Leitura(fileName);

            List<EmprestimoLivro> emprestimos = new List<EmprestimoLivro>();

            for (int i = 1; i < lines.Count(); i++)
            {
                string line = lines[i];
                EmprestimoLivro emprestimoLine = new EmprestimoLivro();

                string[] emprestimocsv = line.Split(';');
                emprestimoLine.IdCliente = long.Parse(emprestimocsv[0]);
                emprestimoLine.NumeroTombo = long.Parse(emprestimocsv[1]);
                emprestimoLine.DataEmprestimo = DateTime.ParseExact(emprestimocsv[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                emprestimoLine.DataDevolucao = DateTime.ParseExact(emprestimocsv[3], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                emprestimoLine.StatusEmprestimo = int.Parse(emprestimocsv[4]);

                emprestimos.Add(emprestimoLine);
            }
            return emprestimos;
        }
    }
}
