using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez.tabuleiro
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }

        private Peca[,] Pecas;

        public Tabuleiro(int linha, int coluna)
        {
            Linhas = linha;
            Colunas = coluna;
            this.Pecas = new Peca[Linhas, Colunas];
        }

        public Peca Peca(int linha, int coluna) 
        {
            return Pecas[linha, coluna];
        }

        public void ColocarPeca(Peca peca, Posicao posicao) 
        {
            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }
    }
}
