
namespace CaixaEletronico.Model
{
    public class Cedula
    {
        public int Valor { get; internal set; }
        public int Quantidade { get; internal set; }

        public Cedula(int Valor, int Quantidade)
        {
            this.Valor = Valor;
            this.Quantidade = Quantidade;
        }

        public Cedula(int Valor)
        {
            this.Valor = Valor;
        }
    }
}