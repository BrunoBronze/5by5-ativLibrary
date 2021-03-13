using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            string op;

            Console.WriteLine("\tBem vindo a livraria 5by5\n");
            do //Repita enquanto a opção de sair não seja escolhida
            {
                Menu();
                Console.Write(">>> ");
                op = Console.ReadLine();
                switch (op)
                {
                    case "1": //Cadastro do Cliente
                        Console.Clear();
                        break;

                    case "2": //Cadastro do livro
                        Console.Clear();
                        break;

                    case "3": //Empréstimo de Livro
                        Console.Clear();
                        break;

                    case "4": //Devolução do Livro
                        Console.Clear();
                        break;

                    case "5": //Relatório de Emprestimos e Devoluções
                        Console.Clear();
                        break;

                    case "0": //Finalizar o Programa
                        do //Repita enquanto a resposta for diferente de sim ou não
                        {
                            Console.WriteLine("\nDeseja realmente sair?");
                            Console.WriteLine("Digite \"s\" pra sim ou \"n\" para não\n");
                            Console.Write(">>> ");
                            op = Console.ReadLine().ToLower();

                            if (op == "s")
                            {
                                op = "0";
                                Console.Write("\nObrigado por usar o sistema Livraria 5by5\n");
                            }
                            else if ((op == "n"))
                            {
                                Console.Write("\nPressione enter para voltar ao menu");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Digito inválido");
                            }
                        } while (op != "0" && op != "n");
                        
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("\nPor favor digite uma opção do menu !\n");
                        break;
                }
            } while (op != "0");

            Console.Write("Pressione enter para encerrar...");
            Console.ReadKey();
        }

        static void Menu()
        {
            Console.WriteLine(">>> Menu livraria <<<\n" +
                              "1 - Cadastro de Cliente\n" +
                              "2 - Cadastro de Livro\n" +
                              "3 - Empréstimo de Livro\n" +
                              "4 - Devolução do Livro\n" +
                              "5 - Relatório de Empréstimos e Devoluções\n" +
                              "0 - Finalizar o Programa\n");
        }
    }
}
