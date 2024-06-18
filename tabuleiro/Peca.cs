using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez.tabuleiro
{
    public abstract class Peca
    {
        public Posicao Posicao { get; set; }

        public Cores Cores { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab {  get; protected set; }

        public Peca(Cores cores, Tabuleiro tab)
        {
            Posicao = null;
            Cores = cores;
            this.tab = tab;
            this.qteMovimentos = 0;
        }

        public void IncrementarQtdMovimentos() 
        {
            qteMovimentos++; 
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public abstract bool[,] MovimentosPossiveis();

        public abstract bool PodeMover(Posicao pos);
    }
}
