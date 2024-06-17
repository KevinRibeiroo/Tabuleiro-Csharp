using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez.tabuleiro;

namespace xadrez.Xadrez
{
    public class Rei : Peca
    {
        public Rei(Cores cores, Tabuleiro tab) : base( cores, tab)
        {
        }
        public override string ToString()
        {
            return "R";
        }
    }
}
