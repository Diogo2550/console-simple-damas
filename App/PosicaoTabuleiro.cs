using Damas.App.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damas.App {
    class PosicaoTabuleiro {

        public int Linha;
        public int Coluna;
        
        private Peca peca;
        private Tabuleiro tabuleiro;
        private PosicaoTabuleiroStatus status;

        public PosicaoTabuleiro(int linha, int coluna, Peca peca, Tabuleiro tabuleiro) {
            Linha = linha;
            Coluna = coluna;
            this.peca = peca;
            this.tabuleiro = tabuleiro;
        }

        public void ColocarPeca(Peca peca) {
            status = PosicaoTabuleiroStatus.Preenchido;
            this.peca = peca;
        }

        public Peca PegarPeca() {
            return this.peca;
        }

        public void RemoverPega() {
            peca = null;
            status = PosicaoTabuleiroStatus.Vazio;
        }

        public void AlterarStatus(PosicaoTabuleiroStatus status) {
            this.status = status;
        }

        public ConsoleColor PegarCor() {
            switch(status) {
                case PosicaoTabuleiroStatus.Vazio:
                    return ConsoleColor.White;
                case PosicaoTabuleiroStatus.Preenchido:
                    return peca.Cor;
                case PosicaoTabuleiroStatus.Selecionado:
                    return ConsoleColor.Yellow;
                case PosicaoTabuleiroStatus.Simulado:
                    return ConsoleColor.DarkGray;
            }
            throw new Exception($"Aparentemente o status {status} não possui cor.");
        }

        public PosicaoTabuleiro SuperiorEsquerdo() {
            int linha = Linha - 1;
            int coluna = Coluna - 1;
            return tabuleiro.PegarPosicao(linha, coluna);
        }

        public PosicaoTabuleiro SuperiorDireito() {
            int linha = Linha + 1;
            int coluna = Coluna - 1;
            return tabuleiro.PegarPosicao(linha, coluna);
        }

        public PosicaoTabuleiro InferiorEsquerdo() {
            int linha = Linha - 1;
            int coluna = Coluna + 1;
            return tabuleiro.PegarPosicao(linha, coluna);
        }

        public PosicaoTabuleiro InferiorDireito() {
            int linha = Linha + 1;
            int coluna = Coluna + 1;
            return tabuleiro.PegarPosicao(linha, coluna);
        }

    }

    enum PosicaoTabuleiroStatus {

        Vazio,
        Preenchido,
        Selecionado,
        Simulado,

    }
}
