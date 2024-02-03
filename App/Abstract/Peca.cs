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

        public void Mover(PosicaoTabuleiro posicao) {
            this.posicao = posicao;
            posicao.ColocarPeca(this);
        }

        public object Clone() {
            return new Peca(Cor, posicao);
        }
    }
}
