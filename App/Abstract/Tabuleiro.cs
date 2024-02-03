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

            for(int linha = 0; linha < width; linha++) {
                for(int coluna = 0; coluna < height; coluna++) {
                    Pecas[linha, coluna] = new PosicaoTabuleiro(linha, coluna, this);
                }
            }
        }

        public PosicaoTabuleiro PegarPosicao(int linha, int coluna) {
            if(linha < 0 || linha >= Height || coluna < 0 || coluna >= Width)
                return null;
            return Pecas[linha, coluna];
        }

        public void AdicionarPeca(Peca peca, int linha, int coluna) {
            if(peca == null) {
                return;
            }
            var posicao = PegarPosicao(linha, coluna);
            peca.Mover(posicao);
        }

        public bool TemPecaNaPosicao(int linha, int coluna) {
            return this.PegarPosicao(linha, coluna).PegarPeca() != null;
        }

        public Tabuleiro SimularJogada(PosicaoTabuleiro posicao) {
            if(!posicao.TemPeca()) {
                return null;
            }

            Tabuleiro tabSimulado = (Tabuleiro)this.Clone();
            var posicaoSimulado = tabSimulado.PegarPosicao(posicao.Linha, posicao.Coluna);
            var posicaoEsquerda = posicaoSimulado.InferiorEsquerdo();
            var posicaoDireita = posicaoSimulado.InferiorDireito();
            bool podeMover = false;

            // define a posicao atual como a selecionada
            tabSimulado.PegarPosicao(posicao.Linha, posicao.Coluna).AlterarStatus(PosicaoTabuleiroStatus.Selecionado);

            // se a posicao esquerda tiver
            if(posicaoEsquerda.TemPeca()) {
                var peca = posicaoSimulado.PegarPeca();
                var pecaEsquerda = posicaoEsquerda.PegarPeca();

                // se a peça for inimiga
                if(pecaEsquerda.Cor != peca.Cor) {
                    var posicaoEsquerda2 = posicaoEsquerda.InferiorEsquerdo();

                    // se não tiver peça na próxima posicao
                    if(posicaoEsquerda2.TemPeca()) {
                        posicaoEsquerda2.AlterarStatus(PosicaoTabuleiroStatus.Simulado);
                        podeMover = true;
                    }
                }
            } else {
                // senao: pode andar
                posicaoEsquerda.AlterarStatus(PosicaoTabuleiroStatus.Simulado);
                podeMover = true;
            }

            // se a posicao direita tiver peca
            if(posicaoDireita.TemPeca()) {
                var peca = posicaoSimulado.PegarPeca();
                var pecaDireita = posicaoDireita.PegarPeca();

                // se a peça for inimiga
                if(pecaDireita.Cor != peca.Cor) {
                    var posicaoDireita2 = posicaoDireita.InferiorDireito();

                    // se não tiver peça na próxima posicao
                    if(!posicaoDireita2.TemPeca()) {
                        posicaoDireita2.AlterarStatus(PosicaoTabuleiroStatus.Simulado);
                        podeMover = true;
                    }
                }
            } else {
                // senao: pode andar
                posicaoDireita.AlterarStatus(PosicaoTabuleiroStatus.Simulado);
                podeMover = true;
            }

            return podeMover ? tabSimulado : null;
        }


        public void Exibir() {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("   1 2 3 4 5 6 7 8");
            Console.WriteLine();
            for(int linha = 0; linha < Width; linha++) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(linha + 1 + "  ");
                for(int coluna = 0; coluna < Height; coluna++) {
                    var posicao = PegarPosicao(linha, coluna);
                    posicao.PegarCor();
                    Console.ForegroundColor = posicao.PegarCor();

                    if(!posicao.TemPeca()) {
                        Console.Write("_ ");
                    } else {
                        Console.Write("X ");
                    }                    
                }
                Console.WriteLine();
            }
        }

        public object Clone() {
            var tabClonado = new Tabuleiro(Width, Height);

            for(int linha = 0; linha < Width; linha++) {
                for(int coluna = 0; coluna < Height; coluna++) {
                    var peca = (Peca)PegarPosicao(linha, coluna).PegarPeca()?.Clone();
                    tabClonado.AdicionarPeca(peca, linha, coluna);
                }
            }

            return tabClonado;
        }

    }
}
