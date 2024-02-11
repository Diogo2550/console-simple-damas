namespace Damas.App.Abstract {
    abstract class Peca : ICloneable {

        public ConsoleColor Cor;
        protected PosicaoTabuleiro posicao;

        public Peca(PosicaoTabuleiro posicao) {
            this.posicao = posicao;
        }

        public Peca() { }

        public void MoverPara(PosicaoTabuleiro posicao) {
            if(this.posicao != null) {
                this.posicao.RemoverPeca();
            }
            this.posicao = posicao;
            this.posicao.ColocarPeca(this);
        }

        /// <summary>
        /// Verifica se é possível mover a peça para uma posicao específica (realizar jogada).
        /// É necessário enviar a primeira posição e a possível segunda posição.
        /// </summary>
        protected PosicaoTabuleiro CalcularJogada(PosicaoTabuleiro p1, PosicaoTabuleiro p2) {
            if(p1 != null) {
                if(!p1.TemPeca()) {
                    return p1;
                } else if(p1.PegarPeca().Cor != Cor) {
                    // se tiver, deve ser inimiga (cor diferente)
                    if(p2 != null && !p2.TemPeca() && p2.PegarCor() != Cor) {
                        return p2;
                    }
                }
            }
            return null;
        }

        abstract public PosicaoTabuleiro JogadaEsquerda();
        abstract public PosicaoTabuleiro JogadaDireita();
        abstract public object Clone();
    }
}
