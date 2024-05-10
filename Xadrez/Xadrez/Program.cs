using System.Net.Http.Headers;
using Xadrez.tabuleiro;
using Xadrez.xadrez;

namespace Xadrez
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Tabuleiro tab = new Tabuleiro(8,8);
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 4));
                tab.colocarPeca(new Rei(tab, Cor.Branca), new Posicao(0, 5));
                Tela.imprimirTabuleiro(tab);
            }
            catch (TabuleiroException ex) 
            {
                Console.WriteLine((ex.Message));
            }

        }
    }
}
