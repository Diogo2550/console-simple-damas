using Damas.App.Abstract;

namespace Damas.App.Game {
    class PecaAzul : Peca {

        public PecaAzul() {
            Cor = ConsoleColor.Blue;
        }

        public override PosicaoTabuleiro JogadaEsquerda() {
            var posicaoEsquerda = posicao.InferiorEsquerdo();
            var posicaoEsquerda2 = posicaoEsquerda?.InferiorEsquerdo();
    
            return CalcularJogada(posicaoEsquerda, posicaoEsquerda2);
        }

        public override PosicaoTabuleiro JogadaDireita() {
            var posicaoDireita = posicao.InferiorDireito();
            var posicaoDireita2 = posicaoDireita?.InferiorDireito();

            return CalcularJogada(posicaoDireita, posicaoDireita2);
        }

        public override object Clone() {
            return new PecaAzul();
        }

    }
}
