using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez.tabuleiro;

namespace xadrez.Xadrez
{
    public class Torre : Peca
    {
            public Torre(Cores cores, Tabuleiro tab) : base(cores, tab)
            {
            }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            //acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna); 
            while (tab.PosicaoValida(pos) && PodeMover(pos)) 
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(tab.Peca(pos) != null && tab.Peca(pos).Cores != Cores)
                {
                    break;
                }
                pos.Linha = pos.Linha - 1;
            }

            //Direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).Cores != Cores)
                {
                    break;
                }
                pos.Coluna = pos.Coluna + 1;
            }

            //Esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).Cores != Cores)
                {
                    break;
                }
                pos.Coluna = pos.Coluna - 1;
            }

            //Abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).Cores != Cores)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1;
            }
            return mat;
        }

        public override bool PodeMover(Posicao pos)
        {
            Peca p = tab.Peca(pos);
            return p == null || p.Cores != this.Cores;
        }

        public override string ToString()
            {
                return "T";
            }
        }
    
}
