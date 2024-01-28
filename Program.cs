var tabuleiro = new int[8, 8];
var tabuleiroSimulado = new int[8, 8];

// criação do tabuleiro
tabuleiro[0, 1] = 1;
tabuleiro[0, 3] = 1;
tabuleiro[0, 5] = 1;
tabuleiro[0, 7] = 1;
tabuleiro[1, 0] = 1;
tabuleiro[1, 2] = 1;
tabuleiro[1, 4] = 1;
tabuleiro[1, 6] = 1;
tabuleiro[2, 1] = 1;
tabuleiro[2, 3] = 1;
tabuleiro[2, 5] = 1;
tabuleiro[2, 7] = 1;

tabuleiro[3, 4] = 2;

tabuleiro[5, 0] = 2;
tabuleiro[5, 2] = 2;
tabuleiro[5, 4] = 2;
tabuleiro[5, 6] = 2;
tabuleiro[6, 1] = 2;
tabuleiro[6, 3] = 2;
tabuleiro[6, 5] = 2;
tabuleiro[6, 7] = 2;
tabuleiro[7, 0] = 2;
tabuleiro[7, 2] = 2;
tabuleiro[7, 4] = 2;
tabuleiro[7, 6] = 2;

bool jogoFinalizou = false;
while(!jogoFinalizou) {
    int linhaSelecionadaInicial = -1;
    int colunaSelecionadaInicial = -1;
    bool podeMover = false;

    do {
        // 1. mostrar o tabuleiro
        ExibirTabuleiro(tabuleiro);

        // 2. selecionar uma peça
        Console.WriteLine("");
        Console.Write("Selecione uma peça (formato: {linha coluna}): ");
        string posPecaInicial = Console.ReadLine();
        linhaSelecionadaInicial = int.Parse(posPecaInicial.Split(' ')[0]) - 1;
        colunaSelecionadaInicial = int.Parse(posPecaInicial.Split(' ')[1]) - 1;

        int pecaSelecionada = tabuleiro[linhaSelecionadaInicial, colunaSelecionadaInicial];
        if(pecaSelecionada == 0) {
            Console.WriteLine("Não há peça na posição escolhida.");
            Console.ReadKey();
        } else {
            podeMover = SimulaTabuleiro(linhaSelecionadaInicial, colunaSelecionadaInicial);
            if(!podeMover) {
                Console.WriteLine("Não há como se mover com a peça escolhida.");
                Console.ReadKey();
            }
        }
    } while(!podeMover);

    

    // exibir possíveis jogadas
    ExibirTabuleiro(tabuleiroSimulado);

    // 3. mover a peça
    Console.WriteLine();
    Console.Write("Você deseja fazer a jogada 1 (esquerda) ou 2(direita)?: ");
    int jogada = int.Parse(Console.ReadLine());

    // para mover a peça, basta mover a peça atual para a cinza (4) no tabuleiro simulado
    // caso a coluna do cinza seja antes da peça, é a jogada 1
    // caso a coluna do cinza seja depois da peça, é a jogada 2
    tabuleiro[linhaSelecionadaInicial, colunaSelecionadaInicial] = 0;
    for(int linha = 0; linha < 8; linha++) {
        for(int coluna = 0; coluna < 8; coluna++) {
            if(tabuleiroSimulado[linha, coluna] == 4) {
                if(jogada == 1 && coluna < colunaSelecionadaInicial) {
                    tabuleiro[linha, coluna] = 1;
                    if(linha > linhaSelecionadaInicial + 1) {
                        tabuleiro[linha - 1, coluna + 1] = 0;
                    }
                }
                if(jogada == 2 && coluna > colunaSelecionadaInicial) {
                    tabuleiro[linha, coluna] = 1;
                    if(linha > linhaSelecionadaInicial + 1) {
                        tabuleiro[linha - 1, coluna - 1] = 0;
                    }
                }
            }
        }
    }
}




void ExibirTabuleiro(int[,] tabuleiro) {
    Console.Clear();
    Console.WriteLine("   1 2 3 4 5 6 7 8");
    Console.WriteLine();
    for(int linha = 0; linha < 8; linha++) {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(linha + 1 + "  ");
        for(int coluna = 0; coluna < 8; coluna++) {
            var peca = tabuleiro[linha, coluna];
            Console.ForegroundColor = ConsoleColor.White;

            if(peca == 1) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("X ");
            } else if(peca == 2) {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("X ");
            } else if(peca == 3) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("X ");
            } else if(peca == 4) {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("_ ");
            } else {
                Console.Write("_ ");
            }
        }
        Console.WriteLine();
    }
}

bool SimulaTabuleiro(int linhaSelecionada, int colunaSelecionada) {
    var pecaEsquerda = tabuleiro[linhaSelecionada + 1, colunaSelecionada - 1];
    var pecaDireita = tabuleiro[linhaSelecionada + 1, colunaSelecionada + 1];
    var peca = tabuleiro[linhaSelecionada, colunaSelecionada];

    if(peca == 0) {
        return false;
    }
    
    tabuleiroSimulado = new int[8, 8];
    bool podeMover = false;

    for(int linha = 0; linha < 8; linha++) {
        for(int coluna = 0; coluna < 8; coluna++) {
            // pula o preenchimento caso já tenha sido preenchido em outra etapa
            if(tabuleiroSimulado[linha, coluna] != 0)
                continue;

            // preenche tabuleiro simulado
            tabuleiroSimulado[linha, coluna] = tabuleiro[linha, coluna];
            
            if(linha == linhaSelecionada && coluna == colunaSelecionada) {
                // se for a peça selecionada
                tabuleiroSimulado[linha, coluna] = 3;
            } else if(linha == linhaSelecionada + 1 && coluna == colunaSelecionada - 1) {
                // se for a diagonal esquerda inferior
                if(pecaEsquerda == 0) {
                    tabuleiroSimulado[linha, coluna] = 4;
                } else if(pecaEsquerda != peca) {
                    var pecaEsquerda2 = tabuleiro[linhaSelecionada + 2, colunaSelecionada - 2];
                    if(pecaEsquerda2 == 0) {
                        tabuleiroSimulado[linha + 1, coluna - 1] = 4;
                    }
                }
            } else if(linha == linhaSelecionada + 1 && coluna == colunaSelecionada + 1) {
                // se for a diagonal direita inferior
                if(pecaDireita == 0) {
                    tabuleiroSimulado[linha, coluna] = 4;
                } else if(pecaDireita != peca) {
                    var pecaDireita2 = tabuleiro[linhaSelecionada + 2, colunaSelecionada + 2];
                    if(pecaDireita2 == 0) {
                        tabuleiroSimulado[linha + 1, coluna + 1] = 4;
                    }
                }
            }

            if(tabuleiroSimulado[linha, coluna] == 4) {
                podeMover = true;
            }
        }
    }
    return podeMover;
}