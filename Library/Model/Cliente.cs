using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cliente
    {
        public long IdCliente { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public Endereco endereco { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"IdCliente : {IdCliente}");
            sb.AppendLine($"CPF : {CPF}");
            sb.AppendLine($"Nome : {Nome}");
            sb.AppendLine($"Data de Nascimento : {DataNascimento:dd/MM/yyyy}");
            sb.AppendLine($"Telefone : {Telefone}");
            sb.AppendLine($"Endereço : {endereco}");

            return sb.ToString();
        }
    }
}
