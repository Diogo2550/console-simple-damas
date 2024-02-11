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
        Console.WriteLine();
        string posPecaInicial = Utils.ReadLine("Selecione uma peça (formato: {linha coluna})");
        linhaSelecionada = int.Parse(posPecaInicial.Split(' ')[0]) - 1;
        colunaSelecionada = int.Parse(posPecaInicial.Split(' ')[1]) - 1;

        posicaoSelecionada = tabuleiro.PegarPosicao(linhaSelecionada, colunaSelecionada);
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
    } while(simulacao == null);

    // exibir possíveis jogadas
    simulacao.Exibir();

    // 3. mover a peça
    PosicaoTabuleiro posicaoJogada;
    var jogadaEsquerda = posicaoSelecionada.PegarPeca().JogadaEsquerda();
    var jogadaDireita = posicaoSelecionada.PegarPeca().JogadaDireita();
    if(jogadaEsquerda != null && jogadaDireita != null) {
        do {
            Console.WriteLine();
            int jogada = int.Parse(Utils.ReadLine("Você deseja fazer a jogada 1 (esquerda) ou 2(direita)?"));

            if(jogada == 1) {
                posicaoJogada = jogadaEsquerda;
            } else {
                posicaoJogada = jogadaDireita;
            }
        } while(posicaoJogada == null);
    } else {
        posicaoJogada = jogadaEsquerda ?? jogadaDireita;
    }
    
    tabuleiro.MoverPeca(posicaoSelecionada, posicaoJogada);

    
}
