using Damas.App.Abstract;
using Damas.App.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Damas.App
{
    class PosicaoTabuleiro {

        public int Linha;
        public int Coluna;
        
        private Peca peca;
        private Tabuleiro tabuleiro;
        private PosicaoTabuleiroStatus status;

        public PosicaoTabuleiro(int linha, int coluna, Tabuleiro tabuleiro) {
            Linha = linha;
            Coluna = coluna;
            this.tabuleiro = tabuleiro;
        }

        public void ColocarPeca(Peca peca) {
            status = PosicaoTabuleiroStatus.Preenchido;
            this.peca = peca;
        }

        public bool TemPeca() {
            return PegarPeca() != null;
        }

        public Peca PegarPeca() {
            return this.peca;
        }

        public void RemoverPeca() {
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
            int linha = Linha - 1;
            int coluna = Coluna + 1;
            return tabuleiro.PegarPosicao(linha, coluna);
        }

        public PosicaoTabuleiro InferiorEsquerdo() {
            int linha = Linha + 1;
            int coluna = Coluna - 1;
            return tabuleiro.PegarPosicao(linha, coluna);
        }

        public PosicaoTabuleiro InferiorDireito() {
            int linha = Linha + 1;
            int coluna = Coluna + 1;
            return tabuleiro.PegarPosicao(linha, coluna);
        }

        #region Operadores
        /// <summary>As posições devem pertencer a um mesmo tabuleiro.</summary>
        public static PosicaoTabuleiro operator -(PosicaoTabuleiro p1, PosicaoTabuleiro p2) {
            if(p1.tabuleiro != p2.tabuleiro) {
                return default(PosicaoTabuleiro);
            }
            int linha = p1.Linha - p2.Linha;
            int coluna = p1.Coluna - p2.Coluna;
            return new PosicaoTabuleiro(linha, coluna, p1.tabuleiro);
        }
        #endregion

    }

    enum PosicaoTabuleiroStatus {

        Vazio,
        Preenchido,
        Selecionado,
        Simulado,

    }
}
