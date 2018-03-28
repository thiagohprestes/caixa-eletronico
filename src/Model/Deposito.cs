using System;
using System.Linq;

namespace CaixaEletronico.Model
{
    class Deposito : Lancamento
    {
        public Deposito(DateTime DataHora, int Valor, CaixaEletronico CaixaEletronico)
        : base(DataHora, Valor, CaixaEletronico) => Tipo = TipoLancamento.Deposito;

        public Deposito(CaixaEletronico CaixaEletronico) : base(CaixaEletronico)
        {
        }

        /// <summary>
        /// Efetua Deposito
        /// </summary>
        public sealed override bool RealizarOperacao()
        {
            if (ValidarLancamento())
            {
                AbastecerCaixa(Notas.ToArray());

                CaixaEletronico.Depositos.Add(this);

                ImprimeLancamento();

                return true;
            }

            return false;
        }

        public void AbastecerCaixa(params Cedula[] cedulas) => cedulas.ToList().ForEach(c =>
        {
            var saldoNotas = CaixaEletronico.SaldoDasNotas.Find(s => s.Valor == c.Valor);

            if (saldoNotas != null)
            {
                saldoNotas.Quantidade += c.Quantidade;
            }

            else
            {
                CaixaEletronico.SaldoDasNotas.Add(c);
            }
        });
    }
}
