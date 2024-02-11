using Damas.App.Abstract;

namespace Damas.App.Game {
    class PecaVermelha : Peca {

        public PecaVermelha() {
            Cor = ConsoleColor.Red;
        }

        public override PosicaoTabuleiro JogadaEsquerda() {
            var posicaoEsquerda = posicao.SuperiorEsquerdo();
            var posicaoEsquerda2 = posicaoEsquerda?.SuperiorEsquerdo();

            return CalcularJogada(posicaoEsquerda, posicaoEsquerda2);
        }

        public override PosicaoTabuleiro JogadaDireita() {
            var posicaoDireita = posicao.SuperiorDireito();
            var posicaoDireita2 = posicaoDireita?.SuperiorDireito();

            return CalcularJogada(posicaoDireita, posicaoDireita2);
        }

        public override object Clone() {
            return new PecaVermelha();
        }

    }
}
