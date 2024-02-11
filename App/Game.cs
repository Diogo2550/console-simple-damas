using Damas.App.Abstract;
using Damas.App.Partida;

namespace Damas.App {
    class Game {

        private Type[] Jogadores = new Type[] { typeof(PecaAzul), typeof(PecaVermelha) };

        private Tabuleiro tabuleiro;
        private Tabuleiro simulacao;
        private bool jogoFinalizado;

        public void CriarPartida(int widthTabuleiro, int heightTabuleiro) {
            CriarTabuleiro(widthTabuleiro, heightTabuleiro);
        }

        public void IniciarPartida() {
            PreJogo();
            LoopDeJogo();
            PosJogo();
        }

        private void LoopDeJogo(int iteracao = 0) {
            // para de executar o jogo quando o mesmo é finalizado
            if(jogoFinalizado) {
                return;
            }

            Type pecaAJogar = Jogadores[iteracao % Jogadores.Length];
            
            // selecionar peça
            var pInicial = SelecionarPeca(pecaAJogar);

            // exibe a simulação
            simulacao.Exibir();

            // seleciona a jogada
            var pFinal = SelecionarJogada(pInicial);

            // realizar jogada
            RealizarJogada(pInicial, pFinal);

            // TODO: verificar se o jogo finalizou

            // recursão: manterá o jogo rodando infinitamente
            LoopDeJogo(++iteracao);
        }

        private PosicaoTabuleiro SelecionarPeca(Type tipoPeca) {
            try {
                // 1. mostrar o tabuleiro
                tabuleiro.Exibir();

                Console.WriteLine();
                Console.WriteLine("Rodada do jogador " + tipoPeca.Name + "!");

                // 2. selecionar uma peça
                Console.WriteLine();
                string posPecaInicial = Utils.ReadLine("Selecione uma peça (formato: {linha coluna})");
                var linhaSelecionada = int.Parse(posPecaInicial.Split(' ')[0]) - 1;
                var colunaSelecionada = int.Parse(posPecaInicial.Split(' ')[1]) - 1;

                var posicaoSelecionada = tabuleiro.PegarPosicao(linhaSelecionada, colunaSelecionada);
                if(!posicaoSelecionada.TemPeca()) {
                    MensagemError("Não há peça na posição escolhida.");
                    return SelecionarPeca(tipoPeca);
                } else {
                    if(posicaoSelecionada.PegarPeca().GetType() != tipoPeca) {
                        MensagemError("A peça selecionada deve ser da cor do jogador.");
                        return SelecionarPeca(tipoPeca);
                    }

                    simulacao = tabuleiro.SimularJogada(posicaoSelecionada);
                    if(simulacao == null) {
                        MensagemError("Não há como se mover com a peça escolhida.");
                        return SelecionarPeca(tipoPeca);
                    }
                }

                return posicaoSelecionada;
            } catch(Exception) {
                MensagemError("Peça inválida selecionada. Tente novamente!");
                return SelecionarPeca(tipoPeca);
            }
        }

        private PosicaoTabuleiro SelecionarJogada(PosicaoTabuleiro posicaoSelecionada) {
            try {
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

                return posicaoJogada;
            } catch(Exception) {
                MensagemError("Jogada inválida selecionada. Tente novamente!");
                return SelecionarJogada(posicaoSelecionada);
            }
        }

        private void RealizarJogada(PosicaoTabuleiro pInical, PosicaoTabuleiro pFinal) {
            tabuleiro.MoverPeca(pInical, pFinal);
        }

        /// <summary>
        /// Utilizado para adicionar o comportamento pré jogo. Você pode, por exemplo, solicitar o nome dos
        /// jogadores e exibir as regras.
        /// </summary>
        private void PreJogo() { }

        /// <summary>
        /// Utilizado para adicionar o comportamento pós jogo. Você pode, por exemplo, exibir a quantidade
        /// de peças comidas por cada lado e parabenizar o vencedor.
        /// </summary>
        private void PosJogo() {
            Console.WriteLine("Partida finalizada. Parabéns!");
        }

        /// <summary>
        /// Não deveria estar dentro desta classe.
        /// </summary>
        private void MensagemError(string mensagem) {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(mensagem);
            Console.BackgroundColor = ConsoleColor.Black;
            Relogio.EsperarSegundos(2);
        }

        private void CriarTabuleiro(int widthTabuleiro, int heightTabuleiro) {
            tabuleiro = new Tabuleiro(widthTabuleiro, heightTabuleiro);

            // criação do tabuleiro. NÃO É A FORMA IDEAL DE SER FEITO!
            // caso tenha curiosidade, pesquise por Factory.
            // também é possível criar o tabuleiro a partir de um .txt
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
        }

    }
}
