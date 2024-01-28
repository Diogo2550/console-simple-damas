var tabuleiro = new int[8, 8];

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

// 1. mostrar o tabuleiro
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
        } else {
            Console.Write("_ ");
        }
    }
    Console.WriteLine();
}

// 2. selecionar uma peça
Console.WriteLine("");
Console.Write("Selecione uma peça (formato: {linha coluna}): ");
string posPecaInicial = Console.ReadLine();
int linhaSelecionadaInicial = int.Parse(posPecaInicial.Split(' ')[0]) - 1;
int colunaSelecionadaInicial = int.Parse(posPecaInicial.Split(' ')[1]) - 1;

int pecaSelecionada = tabuleiro[linhaSelecionadaInicial, colunaSelecionadaInicial];
if(pecaSelecionada == 0) {
    Console.WriteLine("Não há peça na posição escolhida.");
} else {
    Console.WriteLine("A peça escolhida foi " + pecaSelecionada);
}

// exibir possíveis jogadas
Console.Clear();
Console.WriteLine("   1 2 3 4 5 6 7 8");
Console.WriteLine();
for(int linha = 0; linha < 8; linha++) {
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write(linha + 1 + "  ");
    for(int coluna = 0; coluna < 8; coluna++) {
        var peca = tabuleiro[linha, coluna];
        Console.ForegroundColor = ConsoleColor.White;

        if(linha == linhaSelecionadaInicial && coluna == colunaSelecionadaInicial) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("X ");
        } else if(linha == linhaSelecionadaInicial + 1 && (coluna == colunaSelecionadaInicial - 1 || coluna == colunaSelecionadaInicial + 1)) {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("_ ");
        } else {
            if(peca == 1) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("X ");
            } else if(peca == 2) {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("X ");
            } else {
                Console.Write("_ ");
            }
        }
    }
    Console.WriteLine();
}

// 3. mover a peça
Console.WriteLine();
Console.Write("Você deseja fazer a jogada 1 (esquerda) ou 2(direita)?: ");
int jogada = int.Parse(Console.ReadLine());

int linhaSelecionadaFinal = linhaSelecionadaInicial + 1;
int colunaSelecionadaFinal = colunaSelecionadaInicial + (jogada == 1 ? -1 : +1);

tabuleiro[linhaSelecionadaInicial, colunaSelecionadaInicial] = 0;
tabuleiro[linhaSelecionadaFinal, colunaSelecionadaFinal] = pecaSelecionada;


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
        } else {
            Console.Write("_ ");
        }
    }
    Console.WriteLine();
}