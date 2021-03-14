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

        public void CriarArquivo()
        {
            string filePath = $@"{DirectoryPath}\{fileName}";
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("NumeroTombo ; ISBN; Titulo; Genero; DataPublicacao; Autor ");
                }
            }
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
    }
}
