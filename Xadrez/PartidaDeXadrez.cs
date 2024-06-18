using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque {  get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            Jogadoratual = Cores.Branca;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            Xeque = false;
            ColocarPecas();
            Terminada = false;
        }

        private Cores Adversaria(Cores cores)
        {
            if (cores == Cores.Branca) 
            {
            return Cores.Preta;
            }
            else
            {
                return Cores.Branca;
            }
        }

        public bool IsReiEmXeque(Cores cores)
        {
            Peca R = Rei(cores);
            if (R == null) 
            {
                throw new TabuleiroException("Não exista rei da cor: " + cores + "No jogo");
            }
            foreach(Peca P in PecasEmJogo(Adversaria(cores))) 
            {
                bool[,] mat = P.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        private Peca Rei(Cores cores) 
        {
            foreach (Peca p in PecasEmJogo(cores))
            {
                if (p is Rei)
                {
                    return p;
                }
            }

            return null;
        }
        public Peca ExecutaMovimento(Posicao origem,Posicao destino)
        {
            Peca p = Tabuleiro.RetirarPrca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPrca(destino);
            Tabuleiro.ColocarPeca(p, destino);

            if (pecaCapturada != null) 
            {
                Capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna,linha).ConverterPosicaoTab());
            Pecas.Add(peca);
        }
        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Cores.Branca, Tabuleiro));
            ColocarNovaPeca('b', 1, new Cavalo(Cores.Branca, Tabuleiro));
            ColocarNovaPeca('c', 1, new Bispo(Cores.Branca, Tabuleiro));
            ColocarNovaPeca('d', 1, new Dama(Cores.Branca, Tabuleiro));
            ColocarNovaPeca('e', 1, new Rei(Cores.Branca, Tabuleiro, this));
            ColocarNovaPeca('f', 1, new Bispo(Cores.Branca, Tabuleiro));
            ColocarNovaPeca('g', 1, new Cavalo(Cores.Branca, Tabuleiro));
            ColocarNovaPeca('h', 1, new Torre(Cores.Branca, Tabuleiro));
            ColocarNovaPeca('a', 2, new Peao(Cores.Branca, Tabuleiro, this));
            ColocarNovaPeca('b', 2, new Peao(Cores.Branca, Tabuleiro, this));
            ColocarNovaPeca('c', 2, new Peao(Cores.Branca, Tabuleiro, this));
            ColocarNovaPeca('d', 2, new Peao(Cores.Branca, Tabuleiro, this));
            ColocarNovaPeca('e', 2, new Peao(Cores.Branca, Tabuleiro, this));
            ColocarNovaPeca('f', 2, new Peao(Cores.Branca, Tabuleiro, this));
            ColocarNovaPeca('g', 2, new Peao(Cores.Branca, Tabuleiro, this));
            ColocarNovaPeca('h', 2, new Peao(Cores.Branca, Tabuleiro, this));

            ColocarNovaPeca('a', 8, new Torre(Cores.Preta, Tabuleiro));
            ColocarNovaPeca('b', 8, new Cavalo(Cores.Preta, Tabuleiro));
            ColocarNovaPeca('c', 8, new Bispo(Cores.Preta, Tabuleiro));
            ColocarNovaPeca('d', 8, new Dama(Cores.Preta, Tabuleiro));
            ColocarNovaPeca('e', 8, new Rei(Cores.Preta, Tabuleiro, this));
            ColocarNovaPeca('f', 8, new Bispo(Cores.Preta, Tabuleiro));
            ColocarNovaPeca('g', 8, new Cavalo(Cores.Preta, Tabuleiro));
            ColocarNovaPeca('h', 8, new Torre(Cores.Preta, Tabuleiro));
            ColocarNovaPeca('a', 7, new Peao(Cores.Preta, Tabuleiro, this));
            ColocarNovaPeca('b', 7, new Peao(Cores.Preta, Tabuleiro, this));
            ColocarNovaPeca('c', 7, new Peao(Cores.Preta, Tabuleiro, this));
            ColocarNovaPeca('d', 7, new Peao(Cores.Preta, Tabuleiro, this));
            ColocarNovaPeca('e', 7, new Peao(Cores.Preta, Tabuleiro, this));
            ColocarNovaPeca('f', 7, new Peao(Cores.Preta, Tabuleiro, this));
            ColocarNovaPeca('g', 7, new Peao(Cores.Preta, Tabuleiro, this));
            ColocarNovaPeca('h', 7, new Peao(Cores.Preta, Tabuleiro, this));
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
         Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if(IsReiEmXeque(Jogadoratual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você n pode se colocar em check");
            };
            Peca p = Tabuleiro.Peca(destino);

            // #jogadaespecial promocao
            if (p is Peao)
            {
                if ((p.Cores == Cores.Branca && destino.Linha == 0) || (p.Cores == Cores.Preta && destino.Linha == 7))
                {
                    p = Tabuleiro.RetirarPrca(destino);
                    Pecas.Remove(p);
                    Peca dama = new Dama(p.Cores, Tabuleiro);
                    Tabuleiro.ColocarPeca(dama, destino);
                    Pecas.Add(dama);
                }
            }


            if (IsReiEmXeque(Adversaria(Jogadoratual)))
            {
                Xeque = true;
                
            }
            else
            {
                Xeque=false;
            };

            if (TesteXequeMate(Adversaria(Jogadoratual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudarJogador();
            }
            // #jogadaespecial en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
        }

        private void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tabuleiro.RetirarPrca(destino);
            p.DecrementarQtdMovimento();
            if(pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(p, origem);
        }

        public bool TesteXequeMate(Cores cores)
        {
            if (!IsReiEmXeque(cores))
            {
                return false;
            }
            foreach (Peca p in PecasEmJogo(cores))
            {
                bool[,] mat = p.MovimentosPossiveis();
                for (int i = 0; i < Tabuleiro.Linhas; i++)
                {
                    for (int j = 0; j < Tabuleiro.Colunas; j++)
                    {
                        Posicao origem = p.Posicao;
                        Posicao destino = new Posicao(i, j);
                        Peca pecaCapturada = ExecutaMovimento(origem, destino);
                        bool testeXeque = IsReiEmXeque(cores);
                        DesfazerMovimento(origem, destino, pecaCapturada);
                        if (!testeXeque)
                        {
                            return false;
                        }
                    }

                }
            }
            return true;
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

        public HashSet<Peca> PecasCapturadas(Cores cores)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if(x.Cores == cores)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cores cores)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cores == cores)
                {
                    aux.Add(x);
                }
            }
            
            aux.ExceptWith(PecasCapturadas(cores));
            return aux;
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if(!Tabuleiro.Peca(origem).POdeMoverPara(destino))
            {
                throw new TabuleiroException("posição de destino inválida.");
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
