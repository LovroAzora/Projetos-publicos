﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Xadrez.tabuleiro;

namespace Xadrez.xadrez
{
     class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            xeque = false;
            colocarPecas();
            terminada = false;    
        }


        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem); 
            p.incrementarQtdMovimentos();
            Peca pecaCapturada =tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            //jogada especial
            //Roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                tab.colocarPeca(T, destinoT);

            }
            // Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                tab.colocarPeca(T, destinoT);

            }

            return pecaCapturada;
        }
        public void desfazMovimento(Posicao origem,Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdMovimentos();
            if(pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p,origem);
            // roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                tab.colocarPeca(T, origemT);
            }
            // Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                tab.colocarPeca(T, origemT);

            }

        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
           Peca pecaCapturada =  executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");

            }
            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else 
            { 
                xeque = false;
            }
            if (testeXequemate(adversaria(jogadorAtual))){
                terminada = true;
            }
            else { 

            turno++;
            mudaJogador();
            }
        }
        public void validarPosicaoDeOrigem (Posicao pos)
        {
            if(tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não e sua animal!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis");
            }

        }
        public void validarPosicaoDeDestino(Posicao origem,Posicao destino)
        {
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Não e possivel realizar este movimento");
            }
            

        }

        private void mudaJogador()
        {
            if (jogadorAtual== Cor.Branca)
            {
                jogadorAtual= Cor.Preta;
            } else
            {
                jogadorAtual = Cor.Branca;
            }
        }
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;

        }
        public HashSet<Peca> pecasEmJogo (Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            } else
            {
                return Cor.Branca;
            }
        }
        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque (Cor cor)
        {
            Peca R = rei(cor);
            if(R== null)
            {
                throw new TabuleiroException("Não tem rei da cor" + cor + "no tabuleiro!");
            }
            foreach ( Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.Linha, R.posicao.Coluna])
                    { 
                    return true; 
                }
            }
            return false;
        }
        public bool testeXequemate (Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0;j<tab.colunas; j++)
                    {
                        if (mat[i,j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem,destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem,destino,pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {
            colocarNovaPeca('A', 1, new Torre(tab, Cor.Branca));
            //colocarNovaPeca('B', 1, new Cavalo(tab, Cor.Branca));
            //colocarNovaPeca('C', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('E', 1, new Rei(tab, Cor.Branca,this));
            //colocarNovaPeca('D', 1, new Dama(tab, Cor.Branca));
            //colocarNovaPeca('F', 1, new Bispo(tab, Cor.Branca));
            //colocarNovaPeca('G', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('H', 1, new Torre(tab, Cor.Branca));
            
            colocarNovaPeca('A', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('B', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('C', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('D', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('E', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('F', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('G', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('H', 2, new Peao(tab, Cor.Branca));



            colocarNovaPeca('A', 8, new Torre(tab, Cor.Preta));
            //colocarNovaPeca('B', 8, new Cavalo(tab, Cor.Preta));
            //colocarNovaPeca('C', 8, new Bispo(tab, Cor.Preta));
            //colocarNovaPeca('D', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('E', 8, new Rei(tab, Cor.Preta, this));
            //colocarNovaPeca('F', 8, new Bispo(tab, Cor.Preta));
            //colocarNovaPeca('G', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('H', 8, new Torre(tab, Cor.Preta));

            colocarNovaPeca('A', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('B', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('C', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('D', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('E', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('F', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('G', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('H', 7, new Peao(tab, Cor.Preta));


        }
    }
}
