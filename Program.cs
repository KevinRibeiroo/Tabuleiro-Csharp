using tabuleiro;
using xadrez;
using xadrez.tabuleiro;
using xadrez.Xadrez;

internal class Program
{
    private static void Main(string[] args)
    {
        Tabuleiro tab = new Tabuleiro(8,8);

        tab.ColocarPeca(new Torre(Cores.Preta, tab), new Posicao(0, 0));
        tab.ColocarPeca(new Torre(Cores.Preta, tab), new Posicao(1, 3));
        tab.ColocarPeca(new Rei(Cores.Preta, tab), new Posicao(2, 4));

        Tela.imprimirTabuleiro(tab);

        Console.ReadLine();
    }
}