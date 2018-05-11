using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorDeCarpetas.Grafica
{
    class Graphviz
    {
        private List<String> listaDePadres = new List<String>();
        private int numNodo;
        private String nodoPadre;
        private String nodoHijo;

        public void buscarNodos()
        {
            listaDePadres.Clear();
            numNodo = 0;
            nodoPadre = "";
            nodoHijo = "";

            graficar();
        }

        private void graficar()
        {
            
        }
    }
}
