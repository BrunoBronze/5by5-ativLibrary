using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            string op;
            List<Cliente> clientes = new List<Cliente>();
            List<Livro> livros = new List<Livro>();

            Console.WriteLine("Bem vindo a livraria 5by5\n");

            #region Menu

            do //Repita enquanto a opção de sair não seja escolhida
            {
                Menu();
                Console.Write(">>> ");
                op = Console.ReadLine();
                switch (op)
                {

                    case "1":
                        #region Cadastro Cliente

                        //Cadastro do Cliente
                        Console.Clear();
                        Cliente cliente = new Cliente();

                        Console.Write("Digite o CPF do cliente: ");
                        string cpf = Console.ReadLine();

                        if (ClienteCadastrado(cpf))
                        {
                            //trazendo informações do cliente
                            Console.WriteLine("Cliente já cadastrado!");
                            Console.WriteLine("Trazer informações do cliente");
                        }
                        else
                        {
                            cliente = CadastroCliente();
                            cliente.endereco = CadastroEndereco();

                            clientes.Add(cliente);
                            Console.Clear();
                            Console.Write("Cliente cadastrado...\n\n");
                        }
                        
                        break;

                    #endregion

                    case "2":
                        #region Cadastro do livro

                        //Cadastro do livro

                        Console.Write("\nDigite o ISBN do livro: ");
                        string isbn = Console.ReadLine();

                        if (LivroCadastrado(isbn))
                        {
                            //trazendo informações do livro
                            Console.WriteLine("Livro já cadastrado!");
                            Console.WriteLine("Trazer informações do livro");
                        }
                        else
                        {
                            livros.Add(CadastroLivro());

                            Console.Clear();
                            Console.Write("Livro cadastrado...\n\n");
                            //salvar o arquivo
                        }
                        break;

                    #endregion

                    case "3":
                        #region Empréstimo de Livro

                        //Empréstimo de Livro
                        Console.Clear();
                        break;

                    #endregion

                    case "4":
                        #region Devolução do Livro

                        //Devolução do Livro
                        Console.Clear();
                        break;

                    #endregion

                    case "5":
                        #region Relatório de Emprestimos e Devoluções

                        //Relatório de Emprestimos e Devoluções
                        Console.Clear();
                        break;

                    #endregion

                    case "0":
                        #region Finalizar o Programa

                        //Finalizar o Programa
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

                    #endregion

                    default:
                        Console.Clear();
                        Console.WriteLine("\nPor favor digite uma opção do menu !\n");
                        break;
                }
            } while (op != "0");

            #endregion

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
        static bool ClienteCadastrado(string cpf)
        {
            return false;
        }
        static Cliente CadastroCliente()
        {

            string nome;
            DateTime dataNascimento;
            string telefone;
            bool dataCorreta = false;

            Console.WriteLine("\n>>> Cadastro do cliente <<<");
            Console.Write("Digite o nome do cliente: ");
            nome = Console.ReadLine();

            
            do
            {
                Console.Write("Digite a data de nascimento do cliente (dd/mm/aaaa): ");
                string dnascimento = Console.ReadLine();
                
                if (!DateTime.TryParseExact(dnascimento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
                {
                    Console.WriteLine("Digite novamente a data no formato dd/mm/aaaa");
                }
                else
                {
                    dataCorreta = true;
                }
            } while (!dataCorreta);

            Console.Write("Digite o telefone do cliente: ");
            telefone = Console.ReadLine();

            Cliente cliente = new Cliente
            {
                Nome = nome,
                DataNascimento =  dataNascimento,
                Telefone = telefone
            };

            return cliente;
        }
        static Endereco CadastroEndereco()
        {


            Console.WriteLine(">> Cadastro do endereço <<");
            Console.Write("Digite o logradouro do cliente: ");
            string logradouro = Console.ReadLine();

            Console.Write("Digite o bairro do cliente: ");
            string bairro = Console.ReadLine();

            Console.Write("Digite a cidade do cliente: ");
            string cidade = Console.ReadLine();

            Console.Write("Digite o estado do cliente: ");
            string estado = Console.ReadLine();

            Console.Write("Digite o CEP do cliente: ");
            string cep = Console.ReadLine();

            Endereco endereco = new Endereco
            {
                Logradouro = logradouro,
                Bairro = bairro,
                Cidade = cidade,
                Estado = estado,
                CEP = cep
            };

            return endereco;
        }
        static bool LivroCadastrado(string isbn)
        {
            return false;
        }
        static Livro CadastroLivro()
        {
            string titulo;
            string genero;
            DateTime dataPublicacao;
            string autor;
            bool dataCorreta = false;

            Console.WriteLine("\n>>> Cadastro do livro <<<");
            Console.Write("Digite o titulo do livro: ");
            titulo = Console.ReadLine();

            Console.Write("Digite o genero do livro: ");
            genero = Console.ReadLine();

            do
            {
                Console.Write("Digite a data de publicação do livro (dd/mm/aaaa): ");
                string dPublicacao = Console.ReadLine();

                if (!DateTime.TryParseExact(dPublicacao, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataPublicacao))
                {
                    Console.WriteLine("Digite novamente a data no formato dd/mm/aaaa");
                }
                else
                {
                    dataCorreta = true;
                }
            } while (!dataCorreta);

            Console.Write("Digite o autor do livro: ");
            autor = Console.ReadLine();

            Livro livro = new Livro
            {
                Titulo = titulo,
                Genero = genero,
                DataPublicao = dataPublicacao,
                Autor = autor
            };

            return livro;
        }
    }
}
