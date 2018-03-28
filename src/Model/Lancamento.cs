using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CaixaEletronico.Model
{
    abstract class Lancamento
    {
        public DateTime DataHora { get; protected set; }
        public int Valor { get; protected set; }
        public TipoLancamento Tipo { get; protected set; }
        public CaixaEletronico CaixaEletronico { get; protected set; }
        public virtual List<Cedula> Notas
        {
            get
            {
                var listNotas = new List<Cedula>();
                var valor = Valor;

                CaixaEletronico.SaldoDasNotas.ForEach(s =>
                {
                    var qtdeNecesaria = valor / s.Valor;

                    if (qtdeNecesaria > 0)
                    {
                        var cedula = new Cedula(Valor: s.Valor, Quantidade: qtdeNecesaria);
                        listNotas.Add(cedula);

                        valor -= qtdeNecesaria * s.Valor;
                    }
                });

                return listNotas;
            }
        }
        public string DescricaoLancamento => Enum.GetName(typeof(TipoLancamento), Tipo);

        public Lancamento(DateTime DataHora, int Valor, TipoLancamento Tipo, CaixaEletronico CaixaEletronico)
        {
            this.DataHora = DataHora;
            this.Valor = Valor;
            this.Tipo = Tipo;
            this.CaixaEletronico = CaixaEletronico;
        }

        public Lancamento(DateTime DataHora, int Valor, CaixaEletronico CaixaEletronico)
        {
            this.DataHora = DataHora;
            this.Valor = Valor;
            this.CaixaEletronico = CaixaEletronico;
        }

        public Lancamento(CaixaEletronico CaixaEletronico)
        {
            this.CaixaEletronico = CaixaEletronico;
        }

        public void ImprimeLancamento(bool imprimeSucesss = true)
        {
            if (imprimeSucesss)
            {
                Console.WriteLine($"\n{DescricaoLancamento} efetuado com sucesso!\n");
            }

            Console.WriteLine($"Data e Hora: {DataHora} Valor: R$ {Valor}");
        }

        public virtual bool ValidarLancamento()
        {
            if (Valor <= 0)
            {
                Console.WriteLine($"\nO valor do {DescricaoLancamento} deve ser positivo");
                return false;
            }

            return true;
        }

        public abstract bool RealizarOperacao();
    }

    public enum TipoLancamento
    {
        [Description("Saque")]
        Saque,
        [Description("Deposito")]
        Deposito
    }
}
