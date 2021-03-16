using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Relatorio
    {
        public string Imprimir(List<Cliente> clientes, List<Livro> livros, EmprestimoLivro emprestimo)
        {
            Cliente cliente = new Cliente();
            Livro livro = new Livro();

            long id = emprestimo.IdCliente;
            long tombo = emprestimo.NumeroTombo;

            cliente = clientes.Find(c => c.IdCliente == id);
            livro = livros.Find(l => l.NumeroTombo == tombo);

            string status;
            if (emprestimo.StatusEmprestimo == 1)
            {
                status = "Emprestado";
            }
            else
            {
                status = "Devolvido";
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CPF do cliente: {cliente.CPF}");
            sb.AppendLine($"Título do livro: {livro.Titulo}");
            sb.AppendLine($"Status do empréstimo: {status}");
            sb.AppendLine($"Data do empréstimo: {emprestimo.DataEmprestimo:dd/MM/yyyy}");
            sb.AppendLine($"Data da devolução: {emprestimo.DataDevolucao:dd/MM/yyyy}");

            return sb.ToString();
        }
    }
}
