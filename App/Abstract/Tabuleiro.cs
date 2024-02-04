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

        public PosicaoTabuleiro? PegarPosicao(int linha, int coluna) {
            if(linha < 0 || linha >= Height || coluna < 0 || coluna >= Width)
                return null;
            return Pecas[linha, coluna];
        }

        public void AdicionarPeca(Peca peca, int linha, int coluna) {
            if(peca == null) {
                return;
            }
            var posicao = PegarPosicao(linha, coluna);
            peca.MoverPara(posicao);
        }

        public bool TemPecaNaPosicao(int linha, int coluna) {
            return this.PegarPosicao(linha, coluna).PegarPeca() != null;
        }

        /// <param name="posicao"></param>
        /// <param name="jogada">1 para jogada da esquerda. 2 para jogada da direta.</param>
        public void MoverPeca(PosicaoTabuleiro pInicial, PosicaoTabuleiro pFinal) {
            var sub = pFinal - pInicial;
            if(Math.Abs(sub.Coluna) > 1) {
                // comemos uma peça
                if(sub.Coluna < 0) {
                    // esquerda
                    pInicial.InferiorEsquerdo().RemoverPeca();
                } else {
                    pInicial.InferiorDireito().RemoverPeca();
                }
            }
            //pInicial.PegarPeca().MoverPara(pFinal);
        }

        public Tabuleiro SimularJogada(PosicaoTabuleiro posicao) {
            if(!posicao.TemPeca()) {
                return null;
            }

            Tabuleiro tabSimulado = (Tabuleiro)this.Clone();
            PosicaoTabuleiro jogada;
            var posicaoSimulado = tabSimulado.PegarPosicao(posicao.Linha, posicao.Coluna);
            bool podeMover = false;

            // define a posicao atual como a selecionada
            tabSimulado.PegarPosicao(posicaoSimulado.Linha, posicaoSimulado.Coluna).AlterarStatus(PosicaoTabuleiroStatus.Selecionado);

            // se a posicao esquerda tiver
            jogada = posicaoSimulado.PegarPeca().JogadaEsquerda();
            if(jogada != null) {
                podeMover = true;
                jogada.AlterarStatus(PosicaoTabuleiroStatus.Simulado);
            }

            // se a posicao direita tiver peca
            jogada = posicaoSimulado.PegarPeca().JogadaDireita();
            if(jogada != null) {
                podeMover = true;
                jogada.AlterarStatus(PosicaoTabuleiroStatus.Simulado);
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
