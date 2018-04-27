using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorDeCarpetas.Modelos
{
    class Token
    {
        private int id;
        private String token;
        private String lexema;
        private int fila;
        private int columna;


        public Token(int id, String token, String lexema, int fila, int columna)
        {
            this.id = id;
            this.token = token;
            this.lexema = lexema;
            this.fila = fila;
            this.columna = columna;
        }

        public int getId()
        {
            return id;
        }

        public String getToken()
        {
            return token;
        }

        public void setToken(String token)
        {
            this.token = token;
        }

        public String getLexema()
        {
            return lexema;
        }

        public void setLexema(String lexema)
        {
            this.lexema = lexema;
        }

        public int getFila()
        {
            return fila;
        }

        public void setFila(int fila)
        {
            this.fila = fila;
        }

        public int getColumna()
        {
            return columna;
        }

        public void setColumna(int columna)
        {
            this.columna = columna;
        }


    }
}
