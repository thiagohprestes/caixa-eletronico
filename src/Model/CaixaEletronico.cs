using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace CaixaEletronico.Model
{
    class CaixaEletronico
    {
        public List<Cedula> SaldoDasNotas { get; private set; } = new List<Cedula>();
        public List<Saque> Saques { get; private set; } = new List<Saque>();
        public List<Deposito> Depositos { get; private set; } = new List<Deposito>();

        public int SaldoTotal
        {
            get
            {
                int saldoTotal = 0;
                SaldoDasNotas.ForEach(s => saldoTotal += s.Quantidade * s.Valor);
                
                return saldoTotal;
            }
        }

        public void ExibirExtrato()
        {
            ImprimeLancamentos(Depositos.ToList<Lancamento>(), "Depositos");
            ImprimeLancamentos(Saques.ToList<Lancamento>(), "Saques");
        }

        public void ExibirSaldo()
        {
            WriteLine("\nSaldo do Caixa:\n");
            var saldo = SaldoDasNotas;
            ImprimeDetalheNotas(cedulas: SaldoDasNotas.ToArray());

            WriteLine("\nValor total: R$ {0}", SaldoTotal);
        }

        public void RealizarDeposito(int valor)
        {
            var deposito = new Deposito(CaixaEletronico: this, DataHora: DateTime.Now, Valor: valor);

            deposito.RealizarOperacao();
        }

        public void RealizarSaque(int valor) 
            => new Saque(CaixaEletronico: this, DataHora: DateTime.Now, Valor: valor).RealizarOperacao();

        public void AbastecerCaixa(params Cedula[] cedulas) 
            => new Deposito(CaixaEletronico: this).AbastecerCaixa(cedulas);

        public Cedula Cedula(int valor)
        {
            var saldo = SaldoDasNotas.Find(x => x.Valor == valor);

            return saldo ?? new Cedula(Valor: valor, Quantidade: 0 );
        }

        private void ImprimeLancamentos(List<Lancamento> lancamentos, string nomeLancamento)
        {
            WriteLine($"\nExtrato dos {nomeLancamento}:\n");
            if (lancamentos != null && lancamentos.Count > 0)
            {
                lancamentos.ForEach(l => l.ImprimeLancamento(imprimeSucesss: false));
            }

            else
            {
                WriteLine($"Nao foram efetuados {nomeLancamento} nesse caixa eletronico");
            }
        }

        public void ImprimeDetalheNotas(bool imprimirVazios = true, params Cedula[] cedulas)
        {
            if (imprimirVazios)
            {
                cedulas.ToList().ForEach(c => WriteLine($"{c.Quantidade} Notas de R$ {c.Valor}"));
            }

            else
            {
                cedulas.ToList().ForEach(c =>
                {
                    if (c.Quantidade > 0)
                    {
                        WriteLine($"{c.Quantidade} Notas de R$ {c.Valor}");
                    }
                });
            }
        }
    }
}