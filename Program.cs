using Damas.App;
using Damas.App.Abstract;
using Damas.App.Game;

var tabuleiro = new Tabuleiro(8, 8);

// criação do tabuleiro. NÃO É A FORMA IDEAL DE SER FEITO!
// caso tenha curiosidade, pesquise por Factory.
tabuleiro.AdicionarPeca(new PecaAzul(), 0, 1);
tabuleiro.AdicionarPeca(new PecaAzul(), 0, 3);
tabuleiro.AdicionarPeca(new PecaAzul(), 0, 5);
tabuleiro.AdicionarPeca(new PecaAzul(), 0, 7);
tabuleiro.AdicionarPeca(new PecaAzul(), 1, 0);
tabuleiro.AdicionarPeca(new PecaAzul(), 1, 2);
tabuleiro.AdicionarPeca(new PecaAzul(), 1, 4);
tabuleiro.AdicionarPeca(new PecaAzul(), 1, 6);
tabuleiro.AdicionarPeca(new PecaAzul(), 2, 1);
tabuleiro.AdicionarPeca(new PecaAzul(), 2, 3);
tabuleiro.AdicionarPeca(new PecaAzul(), 2, 5);
tabuleiro.AdicionarPeca(new PecaAzul(), 2, 7);

tabuleiro.AdicionarPeca(new PecaAzul(), 4, 1);
tabuleiro.AdicionarPeca(new PecaVermelha(), 3, 4);

tabuleiro.AdicionarPeca(new PecaVermelha(), 5, 0);
tabuleiro.AdicionarPeca(new PecaVermelha(), 5, 2);
tabuleiro.AdicionarPeca(new PecaVermelha(), 5, 4);
tabuleiro.AdicionarPeca(new PecaVermelha(), 5, 6);
tabuleiro.AdicionarPeca(new PecaVermelha(), 6, 1);
tabuleiro.AdicionarPeca(new PecaVermelha(), 6, 3);
tabuleiro.AdicionarPeca(new PecaVermelha(), 6, 5);
tabuleiro.AdicionarPeca(new PecaVermelha(), 6, 7);
tabuleiro.AdicionarPeca(new PecaVermelha(), 7, 0);
tabuleiro.AdicionarPeca(new PecaVermelha(), 7, 2);
tabuleiro.AdicionarPeca(new PecaVermelha(), 7, 4);
tabuleiro.AdicionarPeca(new PecaVermelha(), 7, 6);


bool jogoFinalizou = false;
while(!jogoFinalizou) {
    int linhaSelecionada;
    int colunaSelecionada;
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
