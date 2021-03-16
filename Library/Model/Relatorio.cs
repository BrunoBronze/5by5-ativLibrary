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
        public int Menu(List<EmprestimoLivro> emprestimos, int j)
        {
            Console.WriteLine("\nO que deseja fazer a seguir?\n" +
                                                  "1 - Proximo\n" +
                                                  "2 - Anterior\n" +
                                                  "3 - Primeiro\n" +
                                                  "4 - Ultimo\n" +
                                                  "5 - Sair\n");
            Console.Write(">>> ");
            string op1 = Console.ReadLine();

            switch (op1)
            {
                case "1":
                    Console.Clear();
                    if (j == (emprestimos.Count - 1))
                    {
                        Console.WriteLine("Você esta no fim da lista de emprestimo");
                    }
                    else
                    {
                        j++;
                    }

                    break;

                case "2":
                    Console.Clear();
                    if (j == 0)
                    {
                        Console.WriteLine("Você esta no inicio da lista de emprestimo");
                    }
                    else
                    {
                        j--;
                    }

                    break;

                case "3":
                    Console.Clear();
                    if (j == 0)
                    {
                        Console.WriteLine("Você já está no inicio");
                    }
                    else
                    {
                        j = 0;
                    }

                    break;

                case "4":
                    Console.Clear();
                    if (j == emprestimos.Count - 1)
                    {
                        Console.WriteLine("Você já está no fim");
                    }
                    else
                    {
                        j = emprestimos.Count - 1;
                    }

                    break;

                case "5":
                    j = -1;
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Digite uma opção do menu");
                    break;
            }
            return j;
        }
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
