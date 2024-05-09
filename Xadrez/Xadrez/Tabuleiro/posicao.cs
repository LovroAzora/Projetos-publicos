using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez.Tabuleiro
{
     class posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }
        public posicao() { }

        public posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }
        public override string ToString()
        {
            return Linha +" , "+Coluna;
        }
    }
}
