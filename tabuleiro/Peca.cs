using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez.tabuleiro
{
    public class Peca
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
    }
}
