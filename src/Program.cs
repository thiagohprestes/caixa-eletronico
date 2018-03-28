using CaixaEletronico.Model;
using System;
using static System.Console;

namespace CaixaEletronico
{
    class Program
    {
        public static Model.CaixaEletronico caixaEletronico = new Model.CaixaEletronico();

        static void Main(string[] args)
        {
            caixaEletronico.AbastecerCaixa(new Cedula(Valor: 100, Quantidade: 10),
                                           new Cedula(Valor: 50, Quantidade: 12),
                                           new Cedula(Valor: 20, Quantidade: 15),
                                           new Cedula(Valor: 10, Quantidade: 20));

            MenuPrincipal();
        }

        private static void MenuPrincipal(int opcao = default)
        {
            do
            {
                if (opcao.Equals(default))
                {
                    Write("\nDigite a opcao desejada:\n" +
                                  "\n1 - Exibir Extrato" +
                                  "\n2 - Exibir Saldo" +
                                  "\n3 - Realizar Deposito" +
                                  "\n4 - Realizar Saque" +
                                  "\n9 - Sair\n\n>>");


                    opcao = TratarEntrada(Menu.Principal);
                }

                Clear();

                switch (opcao)
                {
                    case 1:
                        caixaEletronico.ExibirExtrato();
                        MenuNavegacao(opcao);
                        break;
                    case 2:
                        caixaEletronico.ExibirSaldo();
                        MenuNavegacao(opcao);
                        break;
                    case 3:
                        EfetuarDeposito(opcao: opcao);
                        break;
                    case 4:
                        EfetuarSaque(opcao: opcao);
                        break;
                    case 9:
                        break;
                    default:
                        WriteLine("\nOpcao Invalida\n");
                        break;
                }

            } while (opcao != 9);

            Environment.Exit(0);
        }

        private static void EfetuarSaque(Menu menu = Menu.Principal, int opcao = default)
        {
            Write("\nInforme o valor do Saque\n\n>>");
            caixaEletronico.RealizarSaque((TratarEntrada(menu)));
            MenuNavegacao(opcao);
        }

        private static void EfetuarDeposito(Menu menu = Menu.Principal, int opcao = default)
        {
            Write("\nInforme o valor do Deposito\n\n>>");
            caixaEletronico.RealizarDeposito(TratarEntrada(menu));
            MenuNavegacao(opcao);
        }

        private static void MenuNavegacao(int opcao = default)
        {
            do
            {
                switch (opcao)
                {
                    case 1:
                    case 2:
                        TratarNavegacao();
                        break;
                    case 3:
                        TratarNavegacao("\n2 - Fazer novo Depósito");
                        EfetuarDeposito(opcao: opcao);
                        break;
                    case 4:
                        TratarNavegacao("\n2 - Fazer novo Saque");
                        EfetuarSaque(opcao: opcao);
                        break;
                    case 9:
                        break;
                }
            } while (opcao != 9);

            Environment.Exit(0);
        }

        private static void TratarNavegacao(string bodyMsg = default)
        {
            Write($"\nDigite a opcao desejada:\n" +
                  $" \n1 - Voltar " +
                  $"{bodyMsg}" +
                  $"\n9 - Sair\n\n>>");

            int opcao = TratarEntrada(Menu.Navegacao);

            if (1.Equals(opcao))
                MenuPrincipal();

            else if (9.Equals(opcao))
                Environment.Exit(0);           
        }

        public static int TratarEntrada(Menu menu)
        {
            int value = 0;

            try
            {
                value = Convert.ToInt32(ReadLine());
                Clear();
            }
            catch
            {
                Clear();
                WriteLine("\nValor Invalido\n");

                switch (menu)
                {
                    case Menu.Principal:
                        MenuPrincipal();
                        break;
                    case Menu.Navegacao:
                        MenuNavegacao();
                        break;
                    default:
                        break;
                }
            }

            return value;
        }
    }

    public enum Menu
    {
        Principal,
        Navegacao
    }
}