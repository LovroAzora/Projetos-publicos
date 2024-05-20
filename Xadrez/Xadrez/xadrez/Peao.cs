﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Xadrez.tabuleiro;

namespace Xadrez.xadrez
{
    internal class Peao : Peca
    {

  
            public Peao(Tabuleiro tab, Cor cor) : base(tab, cor) { }

            public override string ToString()
            {
                return "P";
            }
            private bool podeMover(Posicao pos)
            {
                Peca p = tab.peca(pos);
                return p == null || p.cor != this.cor;
            }
        private bool existeInimigo (Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p!=null && p.cor != this.cor;
        }
        private bool livre (Posicao pos)
        {
            return tab.peca(pos) == null;
        }
            
            public override bool[,] movimentosPossiveis()
            {
                bool[,] mat = new bool[tab.linhas, tab.colunas];
                Posicao pos = new Posicao(0, 0);
                //n
               
            if (cor == Cor.Branca)
            {
                pos.definirValores(posicao.Linha - 1, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha,pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha - 2, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qtdMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha - 1, posicao.Coluna -1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha - 1, posicao.Coluna+1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }else
            {

                pos.definirValores(posicao.Linha + 1, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha + 2, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qtdMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }

            return mat;
            }
     }
}