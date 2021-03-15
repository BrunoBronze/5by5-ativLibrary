﻿using System;
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
                switch (op)
                {
                    

                    case "1":
                        #region Cadastro Cliente

                        //Cadastro do Cliente
                        Console.Clear();
                        Cliente cliente = new Cliente();

                        Console.Write("Digite o CPF do cliente: ");
                        cpf = Console.ReadLine();

                        if (ClienteCadastrado(cpf))
                        {
                            //trazendo informações do cliente
                            Console.WriteLine("Cliente já cadastrado!");
                            Console.WriteLine("Trazer informações do cliente");
                        }
                        else
                        {
                            cliente = CadastroCliente(cpf);
                            cliente.IdCliente = 3; // criar função para gerar ID
                            cliente.endereco = CadastroEndereco();

                            clientes.Add(cliente);
                            arquivoCliente.Salvar(cliente);

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
                            long numeroTombo = 3; //criar função para gerar numeroTombo
                            Livro livro = CadastroLivro(numeroTombo, isbn);
                            arquivoLivro.Salvar(livro);
                            livros.Add(livro);

                            Console.Clear();
                            Console.Write("Livro cadastrado...\n\n");
                        }
                        break;

                    #endregion

                    case "3":
                        #region Empréstimo de Livro

                        //Empréstimo de Livro

                        if (!LivroCadastrado())
                        {
                            Console.WriteLine("Livro não disponível");
                        }
                        else
                        {
                            Console.Write("Digite o CPF do cliente: ");
                            cpf = Console.ReadLine();
                            if (!ClienteCadastrado(cpf))
                            {
                                Console.WriteLine("Cliente não cadastrado");
                            }
                            else
                            {
                                emprestimos.Add(EmprestimoCadastrado());
                                //salvar no arquivo
                            }
                        }

                        Console.Clear();
                        break;

                    #endregion

                    case "4":
                        #region Devolução do Livro

                        //Devolução do Livro

                        Console.Clear();

                        if (EmprestimoCadastrado("apenas para funcionar"))
                        {
                            Console.WriteLine("Livro não encontrado para devolução");
                        }
                        else
                        {
                            int nEmprestimo = 0; //procurar o index do emprestimo

                            double multa = EmprestimoLivro.CalculaMulta(emprestimos.ElementAt(nEmprestimo));
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
                                        //alterar situação
                                        Console.WriteLine("\nSituação alterada para \"Devolvido\"...\n");
                                    }
                                    else if (resposta != "n")
                                    {
                                        Console.WriteLine("Digite \"s\" ou \"n\"");
                                    }
                                    else
                                    {
                                        Console.WriteLine();//apenas para pular uma linha
                                    }
                                } while (resposta != "s" && resposta != "n");
                            }
                            else
                            {
                                Console.WriteLine("\nO prazo da devolução foi cumprido\n");
                                //alterar situação
                                Console.WriteLine("salvando situação para \"Devolvido\"...\n");
                            }

                            //Alterar para devolvido (2) no arquivoEmprestimo
                        }

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
            //verifica o cpf

            return false;
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
        static bool LivroCadastrado()
        {
            string nTombo;
            long numeroTombo;
            bool funcionar = false;
            do
            {
                Console.Write("Digite o numero do tombo: ");
                nTombo = Console.ReadLine();

                if (!long.TryParse(nTombo, out numeroTombo))
                {
                    Console.WriteLine("Digite um número inteiro !");
                }
                else
                {
                    funcionar = true;
                }
            } while (!funcionar);

            //Verifica Livro

            return false;
        }
        static Livro CadastroLivro(long numeroTombo , string isbn)
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
        static EmprestimoLivro EmprestimoCadastrado()
        {
            DateTime dataDevolucao;
            bool dataCorreta = false;

            do
            {
                Console.Write("Digite a data de publicação do livro (dd/mm/aaaa): ");
                string dDevolucao = Console.ReadLine();

                if (!DateTime.TryParseExact(dDevolucao, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDevolucao))
                {
                    Console.WriteLine("Digite novamente a data no formato dd/mm/aaaa");
                }
                else
                {
                    dataCorreta = true;
                }
            } while (!dataCorreta);

            EmprestimoLivro emprestimo = new EmprestimoLivro
            {
                DataEmprestimo = DateTime.Now,
                DataDevolucao = dataDevolucao,
                StatusEmprestimo = 1
            };

            return emprestimo;
        }
        static bool EmprestimoCadastrado(string exemplo) //apenas para exemplificar outro metodo
        {
            string nTombo;
            long numeroTombo;
            bool funcionar = false;
            do
            {
                Console.Write("Digite o numero do tombo: ");
                nTombo = Console.ReadLine();

                if (!long.TryParse(nTombo, out numeroTombo))
                {
                    Console.WriteLine("Digite um número inteiro !");
                }
                else
                {
                    funcionar = true;
                }
            } while (!funcionar);

            //Verifica Livro

            return false;
        }
    }
}
