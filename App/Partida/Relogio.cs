using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damas.App.Partida {
    class Relogio {

        /// <summary>
        /// Espera {segundos} segundos para continuar a executar o código.
        /// </summary>
        public static void EsperarSegundos(int segundos) {
            Thread.Sleep(segundos * 1000);
        }

    }
}
