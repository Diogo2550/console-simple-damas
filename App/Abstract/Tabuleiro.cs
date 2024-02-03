using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damas.App.Abstract {
    internal class Tabuleiro : ICloneable {

        PosicaoTabuleiro[,] Pecas;
        int Width;
        int Height;

        public Tabuleiro(int width, int height) {
            Width = width;
            Height = height;

            Pecas = new PosicaoTabuleiro[Width, Height];
        }

        public PosicaoTabuleiro PegarPosicao(int linha, int coluna) {
            if(linha < 0 || linha >= Height || coluna < 0 || coluna >= Width)
                return null;
            return Pecas[linha, coluna];
        }

        public void AdicionarPeca(Peca peca, int linha, int coluna) {
            var posicao = PegarPosicao(linha, coluna);
            peca.Mover(posicao);
        }

        public bool TemPecaNaPosicao(int linha, int coluna) {
            return this.PegarPosicao(linha, coluna).PegarPeca() != null;
        }

        public Tabuleiro SimularJogada(PosicaoTabuleiro posicao) {
            if(posicao == null) {
                return null;
            }

            Tabuleiro tabSimulado = new Tabuleiro(Width, Height);
            var posicaoEsquerda = posicao.InferiorEsquerdo();
            var posicaoDireita = posicao.InferiorDireito();

            bool podeMover = false;
            for(int linha = 0; linha < 8; linha++) {
                for(int coluna = 0; coluna < 8; coluna++) {
                    var posicaoAtual = tabSimulado.PegarPosicao(linha, coluna);

                    // pula o preenchimento caso já tenha sido preenchido em outra etapa
                    if(posicaoAtual.PegarPeca() != null)
                        continue;

                    var pecaSimulada = (Peca)posicaoAtual.Clone();

                    // preenche tabuleiro simulado
                    tabSimulado.AdicionarPeca(pecaSimulada);

                    if(linha == peca.Posicao.Linha && coluna == peca.Posicao.Coluna) {
                        // se for a peça selecionada
                        pecaSimulada.MudarCor(3);
                    } else if(linha == peca.Linha + 1 && coluna == peca.Coluna - 1) {
                        // se for a diagonal esquerda inferior
                        if(posicaoEsquerda == null) {
                            pecaSimulada.MudarCor(4);
                            podeMover = true;
                        } else if(posicaoEsquerda.Cor != peca.Cor) {
                            var pecaEsquerda2 = this.PegarPeca(peca.Linha + 2, peca.Coluna - 2);
                            if(pecaEsquerda2 == null) {
                                pecaSimulada.MudarCor(4);
                                podeMover = true;
                            }
                        }
                    } else if(linha == peca.Linha + 1 && coluna == peca.Coluna + 1) {
                        // se for a diagonal direita inferior
                        if(posicaoDireita == null) {
                            pecaSimulada.MudarCor(4);
                            podeMover = true;
                        } else if(posicaoDireita != peca) {
                            var pecaDireita2 = this.PegarPeca(peca.Linha + 2, peca.Coluna + 2);
                            if(pecaDireita2 == null) {
                                pecaSimulada.MudarCor(4);
                                podeMover = true;
                            }
                        }
                    }
                }
            }

            if(podeMover)
                return tabSimulado;
            return null;
        }


        public void Exibir() {
            Console.Clear();
            Console.WriteLine("   1 2 3 4 5 6 7 8");
            Console.WriteLine();
            for(int linha = 0; linha < Width; linha++) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(linha + 1 + "  ");
                for(int coluna = 0; coluna < Height; coluna++) {
                    var peca = PegarPeca(linha, coluna);
                    Console.ForegroundColor = ConsoleColor.White;

                    if(peca == null) {
                        Console.Write("_ ");
                    } else {
                        if(peca.Cor == 1) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("X ");
                        } else if(peca.Cor == 2) {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("X ");
                        } else if(peca.Cor == 3) {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("X ");
                        } else if(peca.Cor == 4) {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("_ ");
                        }
                    }                    
                }
                Console.WriteLine();
            }
        }

        public object Clone() {
            var tabClonado = new Tabuleiro(Width, Height);

            for(int linha = 0; linha < Width; linha++) {
                for(int coluna = 0; coluna < Height; coluna++) {
                    var peca = PegarPosicao(linha, coluna).PegarPeca();
                    tabClonado.AdicionarPeca(peca, linha, coluna);
                }
            }

            return tabClonado;
        }

    }
}
