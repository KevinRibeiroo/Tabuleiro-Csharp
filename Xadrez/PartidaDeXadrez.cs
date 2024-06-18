using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez.tabuleiro;
using xadrez.Xadrez;

namespace Xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cores Jogadoratual { get; private set; }
        public bool Terminada {  get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            Jogadoratual = Cores.Branca;
            ColocarPecas();
            Terminada = false;
        }

        public void ExecutaMovimento(Posicao origem,Posicao destino)
        {
            Peca p = Tabuleiro.RetirarPrca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPrca(destino);
            Tabuleiro.ColocarPeca(p, destino);
        }

        private void ColocarPecas()
        {
            Tabuleiro.ColocarPeca(new Torre(Cores.Branca, Tabuleiro), new PosicaoXadrez('c', 1).ConverterPosicaoTab());
            Tabuleiro.ColocarPeca(new Torre(Cores.Branca, Tabuleiro), new PosicaoXadrez('c', 2).ConverterPosicaoTab());
            Tabuleiro.ColocarPeca(new Torre(Cores.Branca, Tabuleiro), new PosicaoXadrez('d', 2).ConverterPosicaoTab());
            Tabuleiro.ColocarPeca(new Torre(Cores.Branca, Tabuleiro), new PosicaoXadrez('e', 1).ConverterPosicaoTab());
            Tabuleiro.ColocarPeca(new Torre(Cores.Branca, Tabuleiro), new PosicaoXadrez('e', 2).ConverterPosicaoTab());
            Tabuleiro.ColocarPeca(new Rei(Cores.Branca, Tabuleiro), new PosicaoXadrez('d', 1).ConverterPosicaoTab());

            Tabuleiro.ColocarPeca(new Torre(Cores.Preta, Tabuleiro), new PosicaoXadrez('c', 7).ConverterPosicaoTab());
            Tabuleiro.ColocarPeca(new Torre(Cores.Preta, Tabuleiro), new PosicaoXadrez('c', 8).ConverterPosicaoTab());
            Tabuleiro.ColocarPeca(new Torre(Cores.Preta, Tabuleiro), new PosicaoXadrez('d', 7).ConverterPosicaoTab());
            Tabuleiro.ColocarPeca(new Torre(Cores.Preta, Tabuleiro), new PosicaoXadrez('e', 7).ConverterPosicaoTab());
            Tabuleiro.ColocarPeca(new Torre(Cores.Preta, Tabuleiro), new PosicaoXadrez('e', 8).ConverterPosicaoTab());
            Tabuleiro.ColocarPeca(new Rei(Cores.Preta, Tabuleiro), new PosicaoXadrez('d', 8).ConverterPosicaoTab());
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
          ExecutaMovimento(origem, destino);
            Turno++;
            MudarJogador();
        }

        public void ValidarPosicaoOrigem(Posicao posicao)
        {
            if (Tabuleiro.Peca(posicao) == null)
            {
                throw new TabuleiroException("Não exista peça na posição de origem escolhida.");
            }
            if (Jogadoratual != Tabuleiro.Peca(posicao).Cores)
            {
                throw new TabuleiroException("A peça que deseja mexer, não é sua.");
            }
            if (!Tabuleiro.Peca(posicao).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não exista peça na posição de origem escolhida.");
            }
        }

        private void MudarJogador()
        {
            if (Jogadoratual == Cores.Branca)
            {
                Jogadoratual = Cores.Preta;
            }
            else
            {
                Jogadoratual = Cores.Branca;
            }
        }
    }
}
