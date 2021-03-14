using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EmprestimoLivro
    {
        public long IdCliente { get; set; }
        public long NumeroTombo { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public int StatusEmprestimo { get; set; }

        public override string ToString()
        {
            string status;
            if (StatusEmprestimo == 1)
            {
                status = "Emprestado";
            }
            else
            {
                status = "Devolvido";
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Data de emprestimo: {DataEmprestimo:dd/MM/yyyy}");
            sb.AppendLine($"Data de devolucao: {DataDevolucao:dd/MM/yyyy}");
            sb.AppendLine($"Status de Emprestimo: {status}");

            return sb.ToString();
        }
    }
}
