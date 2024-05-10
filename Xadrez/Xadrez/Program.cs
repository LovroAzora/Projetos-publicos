using System.Net.Http.Headers;
using Xadrez.tabuleiro;
using Xadrez.xadrez;

namespace Xadrez
{
    internal class Program
    {
        static void Main(string[] args)
        {

            PosicaoXadrez pos = new PosicaoXadrez('C', 1);
            Console.WriteLine(pos);
            Console.WriteLine(pos.toPosicao());

        }
    }
}
