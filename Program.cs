using Exceptions;
using System.Linq.Expressions;
using tabuleiro;
using xadrez;
using xadrez.tabuleiro;
using xadrez.Xadrez;
using Xadrez;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            PosicaoXadrez posicaoXadrez = new PosicaoXadrez('c',7);

            PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();

            while(!partidaDeXadrez.Terminada)
            {
                try
                {
                    Console.Clear();
                    Tela.ImprimirPartida(partidaDeXadrez);
                    Console.WriteLine();

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ConverterPosicaoTab();
                    partidaDeXadrez.ValidarPosicaoOrigem(origem);

                    bool[,] posicaoPossiveis = partidaDeXadrez.Tabuleiro.Peca(origem).MovimentosPossiveis();

                    Console.Clear();
                    Tela.imprimirTabuleiro(partidaDeXadrez.Tabuleiro, posicaoPossiveis);
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ConverterPosicaoTab();
                    partidaDeXadrez.ValidarPosicaoDestino(origem, destino);

                    partidaDeXadrez.RealizaJogada(origem, destino);
                } 
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();
                }
                
            }

        }
        catch(TabuleiroException ex) 
        {
            Console.WriteLine(ex.ToString());
        }
        Console.ReadLine();
    }
}