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
        public static string fileName = @"\EMPRESTIMO.csv";
        string filePath = $@"{DirectoryPath}\{fileName}";

        public bool CriarArquivo()
        {
            bool criou = false;
            if (!File.Exists(filePath))
            {
                FileStream file = File.Create(filePath);
                file.Close();
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("IdCliente;NumeroTombo;DataEmprestimo;DataDevolucao;StatusEmprestimo");
                    criou = true;
                }
            }
            return criou;
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
        public void Salvar(EmprestimoLivro emprestimo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{emprestimo.IdCliente};");
            sb.Append($"{emprestimo.NumeroTombo};");
            sb.Append($"{emprestimo.DataEmprestimo:dd/MM/yyyy};");
            sb.Append($"{emprestimo.DataDevolucao:dd/MM/yyyy};");
            sb.Append($"{emprestimo.StatusEmprestimo}");

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(sb.ToString());
            }
        }
    }
}
