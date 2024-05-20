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
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada)
                {
                    try
                    {


                        Console.Clear();
                        Tela.ImprimirPartida(partida);
               

                        Console.Write("Origem:");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                        Console.Clear();
                      
                            Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);
                            Console.Write("Destino:");
                            Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                            partida.validarPosicaoDeDestino(origem, destino);
                            partida.realizaJogada(origem, destino);
                      
                    }
                    catch (TabuleiroException ex) { 
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }

                }
                Console.Clear ();
                Tela.ImprimirPartida(partida);




            }
            catch (TabuleiroException ex) 
            {
                Console.WriteLine((ex.Message));
            }

        }
    }
}
