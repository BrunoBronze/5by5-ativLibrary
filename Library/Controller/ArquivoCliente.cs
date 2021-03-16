using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Controller
{
    public class ArquivoCliente : ArquivoCSV
    {
        public static string fileName = @"\CLIENTE.csv";
        private string filePath = $@"{DirectoryPath}\{fileName}";

        public bool CriarArquivo()
        {
            bool criou = false;
            if (!File.Exists(filePath))
            {
                FileStream file = File.Create(filePath);
                file.Close();

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("IdCliente;CPF;Nome;DataNascimento;Telefone;Logradouro;Bairro;Cidade;Estado;CEP");
                    criou = true;
                }
            }
            return criou;
        }

        public List<Cliente> Leitura()
        {
            List<string> lines = base.Leitura(fileName);
            List<Cliente> clientes = new List<Cliente>();

            for (int i = 1; i < lines.Count(); i++)
            {
                string line = lines[i];
                Cliente clienteLine = new Cliente();
                clienteLine.endereco = new Endereco();

                string[] clientecsv = line.Split(';');
                clienteLine.IdCliente = long.Parse(clientecsv[0]);
                clienteLine.CPF = clientecsv[1];
                clienteLine.Nome = clientecsv[2];
                clienteLine.DataNascimento = DateTime.ParseExact(clientecsv[3], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                clienteLine.Telefone = clientecsv[4];

                clienteLine.endereco.Logradouro = clientecsv[5];
                clienteLine.endereco.Bairro = clientecsv[6];
                clienteLine.endereco.Cidade = clientecsv[7];
                clienteLine.endereco.Estado = clientecsv[8];
                clienteLine.endereco.CEP = clientecsv[9];
                clientes.Add(clienteLine);
            }
            return clientes;
        }
        public void Salvar(Cliente cliente)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{cliente.IdCliente};");
            sb.Append($"{cliente.CPF};");
            sb.Append($"{cliente.Nome};");
            sb.Append($"{cliente.DataNascimento:dd/MM/yyyy};");
            sb.Append($"{cliente.Telefone};");
            sb.Append($"{cliente.endereco.Logradouro};");
            sb.Append($"{cliente.endereco.Bairro};");
            sb.Append($"{cliente.endereco.Cidade};");
            sb.Append($"{cliente.endereco.Estado};");
            sb.Append($"{cliente.endereco.CEP}");

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(sb.ToString());
            }
        }
    }
}
