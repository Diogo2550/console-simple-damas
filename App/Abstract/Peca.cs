using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damas.App.Abstract {
    class Peca : ICloneable {

        public ConsoleColor Cor;
        private PosicaoTabuleiro posicao;

        public Peca(ConsoleColor cor, PosicaoTabuleiro posicao) {
            Cor = cor;
            this.posicao = posicao;
        }

        public Peca(ConsoleColor cor) {
            Cor = cor;
        }

        public void MoverPara(PosicaoTabuleiro posicao) {
            this.posicao = posicao;
            posicao.ColocarPeca(this);
        }

        public PosicaoTabuleiro JogadaEsquerda() {
            var posicaoEsquerda = posicao.InferiorEsquerdo();

            // se não tiver peça: é a posição que queremos
            if(posicaoEsquerda != null) {
                if(!posicaoEsquerda.TemPeca()) {
                    return posicaoEsquerda;
                } else if(posicaoEsquerda.PegarPeca().Cor != Cor) {
                    // se tiver, deve ser inimiga (cor diferente)
                    var posicaoEsquerda2 = posicaoEsquerda.InferiorEsquerdo();

                    if(posicaoEsquerda2.TemPeca()) {
                        return posicaoEsquerda2;
                    }
                }
            }

            return null;
        }

        public PosicaoTabuleiro JogadaDireita() {
            var posicaoDireita = posicao.InferiorDireito();

            // se não tiver peça: é a posição que queremos
            if(posicaoDireita != null) {
                if(!posicaoDireita.TemPeca()) {
                    return posicaoDireita;
                } else if(posicaoDireita.PegarPeca().Cor != Cor) {
                    // se tiver, deve ser inimiga (cor diferente)
                    var posicaoDireita2 = posicaoDireita.InferiorDireito();

                    if(!posicaoDireita2.TemPeca()) {
                        return posicaoDireita2;
                    }
                }
            }

            return null;
        }

        public object Clone() {
            return new Peca(Cor, posicao);
        }
    }
}
