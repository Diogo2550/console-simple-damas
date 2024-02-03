using Damas.App.Abstract;

var tabuleiro = new Tabuleiro(8, 8);

// criação do tabuleiro. NÃO É A FORMA IDEAL DE SER FEITO!
// caso tenha curiosidade, pesquise por Factory.
tabuleiro.AdicionarPeca(new Peca(1, 0, 1));
tabuleiro.AdicionarPeca(new Peca(1, 0, 3));
tabuleiro.AdicionarPeca(new Peca(1, 0, 5));
tabuleiro.AdicionarPeca(new Peca(1, 0, 7));
tabuleiro.AdicionarPeca(new Peca(1, 1, 0));
tabuleiro.AdicionarPeca(new Peca(1, 1, 2));
tabuleiro.AdicionarPeca(new Peca(1, 1, 4));
tabuleiro.AdicionarPeca(new Peca(1, 1, 6));
tabuleiro.AdicionarPeca(new Peca(1, 2, 1));
tabuleiro.AdicionarPeca(new Peca(1, 2, 3));
tabuleiro.AdicionarPeca(new Peca(1, 2, 5));
tabuleiro.AdicionarPeca(new Peca(1, 2, 7));

tabuleiro.AdicionarPeca(new Peca(2, 3, 4));

tabuleiro.AdicionarPeca(new Peca(2, 5, 0));
tabuleiro.AdicionarPeca(new Peca(2, 5, 2));
tabuleiro.AdicionarPeca(new Peca(2, 5, 4));
tabuleiro.AdicionarPeca(new Peca(2, 5, 6));
tabuleiro.AdicionarPeca(new Peca(2, 6, 1));
tabuleiro.AdicionarPeca(new Peca(2, 6, 3));
tabuleiro.AdicionarPeca(new Peca(2, 6, 5));
tabuleiro.AdicionarPeca(new Peca(2, 6, 7));
tabuleiro.AdicionarPeca(new Peca(2, 7, 0));
tabuleiro.AdicionarPeca(new Peca(2, 7, 2));
tabuleiro.AdicionarPeca(new Peca(2, 7, 4));
tabuleiro.AdicionarPeca(new Peca(2, 7, 6));


bool jogoFinalizou = false;
while(!jogoFinalizou) {
    int linhaSelecionada = -1;
    int colunaSelecionada = -1;
    bool podeMover = false;
    Tabuleiro simulacao = null;

    do {

        // 1. mostrar o tabuleiro
        tabuleiro.Exibir();

        // 2. selecionar uma peça
        Console.WriteLine("");
        Console.Write("Selecione uma peça (formato: {linha coluna}): ");
        string posPecaInicial = Console.ReadLine();
        linhaSelecionada = int.Parse(posPecaInicial.Split(' ')[0]) - 1;
        colunaSelecionada = int.Parse(posPecaInicial.Split(' ')[1]) - 1;

        Peca pecaSelecionada = tabuleiro.PegarPeca(linhaSelecionada, colunaSelecionada);
        if(pecaSelecionada == null) {
            Console.WriteLine("Não há peça na posição escolhida.");
            Console.ReadKey();
        } else {
            simulacao = tabuleiro.SimularJogada(pecaSelecionada);
            if(simulacao == null) {
                Console.WriteLine("Não há como se mover com a peça escolhida.");
                Console.ReadKey();
            }
        }
    } while(simulacao == null);

    // exibir possíveis jogadas
    simulacao.Exibir();

    // 3. mover a peça
    Console.WriteLine();
    Console.Write("Você deseja fazer a jogada 1 (esquerda) ou 2(direita)?: ");
    int jogada = int.Parse(Console.ReadLine());

    
}
