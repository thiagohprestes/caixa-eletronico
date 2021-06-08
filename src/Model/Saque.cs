using System;
using System.Collections.Generic;

namespace CaixaEletronico.Model
{
    class Saque : Lancamento
    {
        public sealed override List<Cedula> Notas
        {
            get
            {
                var listNotas = new List<Cedula>();

                CaixaEletronico.SaldoDasNotas.ForEach(s =>
                {
                    var valor = Valor;

                    if (valor > 0)
                    {
                        var qtdeNecesaria = valor / s.Valor;
                        var cedula = new Cedula(Valor: s.Valor);

                        if (s.Quantidade - qtdeNecesaria >= 0)
                        {
                            cedula.Quantidade = qtdeNecesaria;
                            valor -= qtdeNecesaria * s.Valor;
                        }

                        else
                        {
                            cedula.Quantidade = s.Quantidade;
                            valor -= s.Quantidade * s.Valor;
                        }

                        listNotas.Add(cedula);
                    }
                });

                return listNotas;
            }
        }

        public Saque(DateTime DataHora, int Valor, CaixaEletronico CaixaEletronico)
        : base(DataHora, Valor, CaixaEletronico) => Tipo = TipoLancamento.Saque;

        /// <summary>
        /// Efetua Saque
        /// </summary>
        public sealed override bool RealizarOperacao()
        {
            var cedulas = Notas;

            if (ValidarLancamento())
            {
                cedulas.ForEach(c =>
                {
                    var cedulaCaixa = CaixaEletronico.Cedula(c.Valor);
                    if (c.Quantidade > 0)
                    {
                        cedulaCaixa.Quantidade -= c.Quantidade;
                    }
                });

                CaixaEletronico.Saques.Add(this);

                Console.WriteLine("\nNotas do saque:\n");
                CaixaEletronico.ImprimeDetalheNotas(imprimirVazios: false, cedulas.ToArray());
                ImprimeLancamento();

                return true;
            }

            return false;
        }

        public override bool ValidarLancamento()
        {
            var lancamentoValido = base.ValidarLancamento();

            if (lancamentoValido && Tipo == TipoLancamento.Saque && CaixaEletronico.SaldoTotal < Valor)
            {
                Console.WriteLine("\nO caixa nao possui saldo suficiente para esta operacao");
                CaixaEletronico.ExibirSaldo();
                return false;
            }

            return lancamentoValido;
        }
    }
}
