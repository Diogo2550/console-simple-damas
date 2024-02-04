using Damas.App;
using Damas.App.Abstract;

var tabuleiro = new Tabuleiro(8, 8);

// criação do tabuleiro. NÃO É A FORMA IDEAL DE SER FEITO!
// caso tenha curiosidade, pesquise por Factory.
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 0, 1);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 0, 3);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 0, 5);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 0, 7);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 1, 0);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 1, 2);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 1, 4);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 1, 6);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 2, 1);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 2, 3);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 2, 5);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Blue), 2, 7);

tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 3, 4);

tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 5, 0);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 5, 2);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 5, 4);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 5, 6);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 6, 1);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 6, 3);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 6, 5);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 6, 7);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 7, 0);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 7, 2);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 7, 4);
tabuleiro.AdicionarPeca(new Peca(ConsoleColor.Red), 7, 6);


bool jogoFinalizou = false;
while(!jogoFinalizou) {
    int linhaSelecionada = -1;
    int colunaSelecionada = -1;
    bool podeMover = false;
    Tabuleiro simulacao = null;
    PosicaoTabuleiro posicaoSelecionada;

    do {

        // 1. mostrar o tabuleiro
        tabuleiro.Exibir();

        // 2. selecionar uma peça
        Console.WriteLine("");
        Console.Write("Selecione uma peça (formato: {linha coluna}): ");
        string posPecaInicial = Console.ReadLine();
        linhaSelecionada = int.Parse(posPecaInicial.Split(' ')[0]) - 1;
        colunaSelecionada = int.Parse(posPecaInicial.Split(' ')[1]) - 1;

        posicaoSelecionada = tabuleiro.PegarPosicao(linhaSelecionada, colunaSelecionada);
        Console.WriteLine(posicaoSelecionada.PegarPeca());
        if(!posicaoSelecionada.TemPeca()) {
            Console.WriteLine("Não há peça na posição escolhida.");
            Console.ReadKey();
        } else {
            simulacao = tabuleiro.SimularJogada(posicaoSelecionada);
            if(simulacao == null) {
                Console.WriteLine("Não há como se mover com a peça escolhida.");
                Console.ReadKey();
            }
        }
        Console.WriteLine(posicaoSelecionada.TemPeca());
        Console.ReadKey();
    } while(simulacao == null);
    Console.WriteLine(posicaoSelecionada.TemPeca());

    // exibir possíveis jogadas
    simulacao.Exibir();

    // 3. mover a peça
    Console.WriteLine();
    Console.Write("Você deseja fazer a jogada 1 (esquerda) ou 2(direita)?: ");
    int jogada = int.Parse(Console.ReadLine());

    PosicaoTabuleiro posicaoJogada;
    if(jogada == 1) {
        Console.WriteLine(posicaoSelecionada.TemPeca());
        posicaoJogada = posicaoSelecionada.PegarPeca().JogadaEsquerda();
    } else {
        posicaoJogada = posicaoSelecionada.PegarPeca().JogadaDireita();
    }
    tabuleiro.MoverPeca(posicaoSelecionada, posicaoJogada);

    
}
