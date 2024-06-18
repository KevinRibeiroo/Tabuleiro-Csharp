using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez.tabuleiro;
using Xadrez;

namespace xadrez
{
    public class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++) 
            {
                Console.Write(8 - i + "");
                for (int j = 0; j < tab.Colunas; j++)
                {
                   
                        ImprimirPeca(tab.Peca(i, j));
                        
                    
                };
                Console.WriteLine();
            }
            Console.WriteLine(" a b c d e f g h");
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posPossiveis[i,j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor= fundoOriginal;
                    }
                    ImprimirPeca(tab.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                };
                Console.WriteLine();
            }
            Console.WriteLine("   a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cores == Cores.Preta)
                    Console.Write(peca);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }

        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
           string s =  Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPartida(PartidaDeXadrez partidaDeXadrez)
        {
            imprimirTabuleiro(partidaDeXadrez.Tabuleiro);
            Console.WriteLine();
            ImprimirPecasCapturadas(partidaDeXadrez);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partidaDeXadrez.Turno);
            if (!partidaDeXadrez.Terminada)
            {
                Console.WriteLine("Aguardando jogada do: " + partidaDeXadrez.Jogadoratual);

                if (partidaDeXadrez.Xeque)
                {
                    Console.WriteLine("Vc está em xeque");
                }
            } 
            else
            {
                Console.WriteLine("XequeMate");
                Console.WriteLine("Vencedor: " + partidaDeXadrez.Jogadoratual);
            }
           
        }

        private static void ImprimirPecasCapturadas(PartidaDeXadrez partidaDeXadrez)
        {
            Console.WriteLine("Peças Capturadas: ");
            Console.WriteLine("Brancas: ");
            ImprimirConjunto(partidaDeXadrez.PecasCapturadas(Cores.Branca));
            Console.WriteLine();
            Console.WriteLine("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            ImprimirConjunto(partidaDeXadrez.PecasCapturadas(Cores.Preta));
            Console.ForegroundColor = aux;
        }

        private static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca p in conjunto)
            {
                Console.WriteLine(p + "");
            }
            Console.WriteLine("]");
        }
    }
}
