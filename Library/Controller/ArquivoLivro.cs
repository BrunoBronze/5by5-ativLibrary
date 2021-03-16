using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ArquivoLivro : ArquivoCSV
    {
        public static string fileName = @"\LIVRO.csv";
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
                    sw.WriteLine("NumeroTombo;ISBN;Titulo;Genero;DataPublicacao;Autor");
                    criou = true;
                }
            }
            return criou;
        }
        public List<Livro> Leitura()
        {
            List<string> lines = base.Leitura(fileName);
            List<Livro> livros = new List<Livro>();

            for (int i = 1; i < lines.Count(); i++)
            {
                string line = lines[i];
                Livro livroLine = new Livro();

                string[] livrocsv = line.Split(';');
                livroLine.NumeroTombo = long.Parse(livrocsv[0]);
                livroLine.ISBN = livrocsv[1];
                livroLine.Titulo = livrocsv[2];
                livroLine.Genero = livrocsv[3];
                livroLine.DataPublicao = DateTime.ParseExact(livrocsv[4], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                livroLine.Autor = livrocsv[5];

                livros.Add(livroLine);
            }
            return livros;
        }
        public void Salvar(Livro livro)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{livro.NumeroTombo};");
            sb.Append($"{livro.ISBN};");
            sb.Append($"{livro.Titulo};");
            sb.Append($"{livro.Genero};");
            sb.Append($"{livro.DataPublicao:dd/MM/yyyy};");
            sb.Append($"{livro.Autor}");

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(sb.ToString());
            }
        }
    }
}
