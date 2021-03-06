using System;
using Model;
using Controller;
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

            ArquivoCliente arquivoCliente = new ArquivoCliente();
            ArquivoEmprestimo arquivoEmprestimo = new ArquivoEmprestimo();
            ArquivoLivro arquivoLivro = new ArquivoLivro();

            List<Cliente> clientes = new List<Cliente>();
            List<Livro> livros = new List<Livro>();
            List<EmprestimoLivro> emprestimos = new List<EmprestimoLivro>();

            #region Criar Arquivos

            if (!ArquivoCSV.CriarDiretorio())
            {
                if (!arquivoCliente.CriarArquivo())
                {
                    clientes = arquivoCliente.Leitura();
                }
                if (!arquivoLivro.CriarArquivo())
                {
                    livros = arquivoLivro.Leitura();
                }
                if (!arquivoEmprestimo.CriarArquivo())
                {
                    emprestimos = arquivoEmprestimo.Leitura();
                }
            }
            else
            {
                arquivoCliente.CriarArquivo();
                arquivoEmprestimo.CriarArquivo();
                arquivoLivro.CriarArquivo();
            }

            #endregion

            Console.WriteLine("Bem vindo a livraria 5by5\n");

            #region Menu

            do //Repita enquanto a opção de sair não seja escolhida
            {
                Menu();
                Console.Write(">>> ");
                op = Console.ReadLine();
                string cpf;
                long numeroTombo;
                string nTombo;
                bool funcionar;

                Livro livro = new Livro();
                Cliente cliente = new Cliente(); ;
                EmprestimoLivro emprestimo = new EmprestimoLivro(); ;

                switch (op)
                {
                    case "1":
                        #region Cadastro Cliente

                        //Cadastro do Cliente

                        Console.Clear();
                        Console.WriteLine(">>> Cadastro de clientes <<<");

                        cliente = new Cliente();

                        Console.Write("Digite o CPF do cliente: ");
                        cpf = Console.ReadLine();

                        cliente = clientes.Find(c => c.CPF == cpf);

                        if (cliente != null)
                        {
                            //trazendo informações do cliente
                            Console.WriteLine("\nCliente já cadastrado!");
                            Console.WriteLine(cliente);
                        }
                        else
                        {
                            cliente = CadastroCliente(cpf);
                            cliente.endereco = CadastroEndereco();
                            if (!(clientes.Count > 0))
                            {
                                cliente.IdCliente = 1;
                            }
                            else
                            {
                                cliente.IdCliente = clientes.Last().IdCliente + 1;
                            }
                            arquivoCliente.Salvar(cliente);
                            clientes.Add(cliente);

                            Console.WriteLine("Cliente cadastrado...");
                        }
                        Console.Write("Pressione qualquer tecla para voltar ao menu principal...");
                        Console.ReadKey();
                        Console.Clear();

                        break;

                    #endregion

                    case "2":
                        #region Cadastro do livro

                        //Cadastro do livro

                        Console.Clear();
                        Console.WriteLine(">>> Cadastro de livros <<<");

                        Console.Write("\nDigite o ISBN do livro: ");
                        string isbn = Console.ReadLine();

                        livro = livros.Find(l => l.ISBN == isbn);

                        if (livro != null) // se estiver cadastrado
                        {
                            Console.WriteLine("\nLivro já cadastrado!");
                            Console.WriteLine(livro);
                            Console.Write("Pressione qualquer tecla para voltar ao menu principal...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else //se não estiver cadastrado
                        {
                            if (!(livros.Count > 0))
                            {
                                numeroTombo = 1;
                            }
                            else
                            {
                                numeroTombo = livros.Last().NumeroTombo + 1;
                            }
                            
                            livro = CadastroLivro(numeroTombo, isbn);
                            arquivoLivro.Salvar(livro);
                            livros.Add(livro);


                            Console.WriteLine("\nLivro cadastrado...");
                            Console.WriteLine($"O numero do tombo é: {numeroTombo}");
                            Console.Write("Pressione qualquer tecla para voltar ao menu principal...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;

                    #endregion

                    case "3":
                        #region Empréstimo de Livro

                        //Empréstimo de Livro
                        Console.Clear();
                        Console.WriteLine(">>> Empréstimo de livros <<<");
                        if (!(livros.Count > 0))
                        {
                            Console.WriteLine("\nNenhum livro cadastrado");
                        }
                        else
                        {
                            do
                            {
                                funcionar = false;
                                do
                                {
                                    Console.Write("Digite o numero do tombo: ");
                                    nTombo = Console.ReadLine();

                                    if (!long.TryParse(nTombo, out numeroTombo))
                                    {
                                        Console.WriteLine("Digite um número inteiro !\n");
                                    }
                                    else
                                    {
                                        funcionar = true;
                                    }
                                } while (!funcionar);

                                livro = livros.Find(l => l.NumeroTombo == numeroTombo);

                                if (livro == null)
                                {
                                    Console.WriteLine("\nLivro indisponível para empréstimo");
                                    Console.WriteLine("Deseja tentar novamente? ");

                                    op = Confirmacao();

                                }
                                else
                                {
                                    do
                                    {
                                        Console.Write("Digite o CPF do cliente: ");
                                        cpf = Console.ReadLine();
                                        cliente = clientes.Find(c => c.CPF == cpf);
                                        if (cliente == null)
                                        {
                                            Console.WriteLine("\nCliente não cadastrado");
                                            Console.WriteLine("Deseja tentar novamente? ");

                                            op = Confirmacao();
                                        }
                                        else
                                        {
                                            emprestimo = CadastradoEmprestimo(numeroTombo, cliente.IdCliente);
                                            arquivoEmprestimo.Salvar(emprestimo);
                                            emprestimos.Add(emprestimo);

                                            Console.WriteLine("\nEmprestimo cadastrado...");
                                            op = "sair"; // condição para sair do laço
                                        }
                                    } while (op != "sair");
                                }
                            } while (op != "sair");
                        }

                        Console.Write("Pressione qualquer tecla para voltar ao menu principal...");
                        Console.ReadKey();
                        Console.Clear();

                        break;

                    #endregion

                    case "4":
                        #region Devolução do Livro

                        //Devolução do Livro

                        Console.Clear();
                        Console.WriteLine(">>> Devolução de livros <<<");

                        if (!(emprestimos.Count > 0))
                        {
                            Console.WriteLine("\nNenhum livro cadastrado para emprestimo");
                        }
                        else
                        {
                            funcionar = false;
                            do
                            {
                                Console.Write("Digite o numero do tombo: ");
                                nTombo = Console.ReadLine();

                                if (!long.TryParse(nTombo, out numeroTombo))
                                {
                                    Console.WriteLine("Digite um número inteiro !\n");
                                }
                                else
                                {
                                    funcionar = true;
                                }
                            } while (!funcionar);

                            int index = arquivoEmprestimo.ProcuraNumeroTombo(numeroTombo);

                            if (index == -1) //ProcuraNumeroTombo retorna -1 caso não encontre o index
                            {
                                Console.WriteLine("\nLivro não encontrado para devolução");
                            }
                            else
                            {
                                double multa = EmprestimoLivro.CalculaMulta(emprestimos.ElementAt(index));
                                if (multa > 0)
                                {
                                    Console.WriteLine($"\nMulta a ser paga: R$ {multa:F2}");
                                    string resposta;
                                    do
                                    {
                                        Console.Write("A multa foi paga sim ou não(s/n)? ");
                                        resposta = Console.ReadLine().ToLower();
                                        if (resposta == "s")
                                        {
                                            emprestimos.ElementAt(index).StatusEmprestimo = 2;
                                            arquivoEmprestimo.Devolucao(index);
                                            //alterar situação
                                            Console.WriteLine("\nSituação alterada para \"Devolvido\"...");
                                        }
                                        else if (resposta != "n")
                                        {
                                            Console.WriteLine("Digite \"s\" ou \"n\"");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nSituação não foi alterada...");
                                        }
                                    } while (resposta != "s" && resposta != "n");
                                }
                                else
                                {
                                    Console.WriteLine("\nO prazo da devolução foi cumprido\n");

                                    emprestimos.ElementAt(index).StatusEmprestimo = 2;
                                    arquivoEmprestimo.Devolucao(index);
                                    //alterar situação
                                    Console.WriteLine("salvando situação para \"Devolvido\"...");
                                }

                                //Alterar para devolvido (2) no arquivoEmprestimo
                            }
                        }

                        Console.Write("Pressione qualquer tecla para voltar ao menu principal...");
                        Console.ReadKey();
                        Console.Clear();

                        break;

                    #endregion

                    case "5":
                        #region Relatório de Emprestimos e Devoluções
                        Console.Clear();
                        Console.WriteLine(">>> Relatório de Emprestimos e Devoluções <<<");
                        int j = 0;
                        do
                        {
                            if (emprestimos.Count == 0)
                            {
                                Console.WriteLine("\nNenhum emprestimo cadastrado!");
                                j = -1;
                            }
                            else
                            {
                                Relatorio relatorio = new Relatorio();

                                Console.Write("\n" + relatorio.Imprimir(clientes, livros, emprestimos[j]));

                                j = relatorio.Menu(emprestimos, j);
                            }
                        } while (j != -1);

                        Console.Write("Pressione qualquer tecla para voltar ao menu principal...");
                        Console.ReadKey();
                        Console.Clear();

                        break;

                    #endregion

                    case "0":
                        #region Finalizar o Programa

                        //Finalizar o Programa

                        Console.Clear();

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
                                Console.Write("Pressione qualquer tecla para voltar ao menu principal...");
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

            Console.Write("Pressione qualquer tecla para encerrar...");
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
        static Cliente CadastroCliente(string cpf)
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
                CPF = cpf,
                Nome = nome,
                DataNascimento = dataNascimento,
                Telefone = telefone
            };

            return cliente;
        }
        static Endereco CadastroEndereco()
        {


            Console.WriteLine("\n>> Cadastro do endereço <<");
            Console.Write("Digite o logradouro do cliente: ");
            string logradouro = Console.ReadLine();

            Console.Write("Digite o bairro: ");
            string bairro = Console.ReadLine();

            Console.Write("Digite a cidade: ");
            string cidade = Console.ReadLine();

            Console.Write("Digite o estado: ");
            string estado = Console.ReadLine();

            Console.Write("Digite o CEP: ");
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
        static Livro CadastroLivro(long numeroTombo, string isbn)
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
                NumeroTombo = numeroTombo,
                ISBN = isbn,
                Titulo = titulo,
                Genero = genero,
                DataPublicao = dataPublicacao,
                Autor = autor
            };

            return livro;
        }
        static EmprestimoLivro CadastradoEmprestimo(long numeroTombo, long idCliente)
        {
            DateTime dataDevolucao;
            bool dataCorreta = false;

            do
            {
                Console.Write("Digite a data de devolução do livro (dd/mm/aaaa): ");
                string dDevolucao = Console.ReadLine();

                if (!DateTime.TryParseExact(dDevolucao, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDevolucao))
                {
                    Console.WriteLine("\nDigite novamente a data no formato dd/mm/aaaa");
                }
                else
                {
                    if (dataDevolucao < DateTime.Today)
                    {
                        Console.WriteLine("\nData de devolução anterior ao hoje, digite uma data futura...");
                    }
                    else
                    {
                        dataCorreta = true;
                    }
                }
            } while (!dataCorreta);

            EmprestimoLivro emprestimo = new EmprestimoLivro
            {
                NumeroTombo = numeroTombo,
                IdCliente = idCliente,
                DataEmprestimo = DateTime.Now,
                DataDevolucao = dataDevolucao,
                StatusEmprestimo = 1
            };

            return emprestimo;
        }
        static string Confirmacao()
        {
            string op;
            do
            {
                Console.WriteLine("Digite \"s\" pra sim ou \"n\" para não\n");
                Console.Write(">>> ");
                op = Console.ReadLine().ToLower();
                if (op == "s")
                {
                    op = "tentar";
                    Console.Clear();
                }
                else if ((op == "n"))
                {
                    op = "sair";
                }
                else
                {
                    Console.WriteLine("Digito inválido");
                }
            } while (op != "sair" && op != "tentar");
            return op;
        }
    }
}
