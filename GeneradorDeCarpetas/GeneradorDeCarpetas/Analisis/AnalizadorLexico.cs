using GeneradorDeCarpetas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorDeCarpetas.Analisis
{
    class AnalizadorLexico
    {
        private List<String> listaPalabrasReservadas = new List<string>();
        public static List<Token> listaTokens = new List<Token>();

        private int idToken = 0;
        private String lexema = "";
        private int fila = 1;
        private int columna = 0;
        private int estado = 0;


        public void setIdToken(int idToken)
        {
            this.idToken = idToken;
        }


        public AnalizadorLexico()
        {
            
        }


        public void analizarTexto(String texto)
        {
            Char[] cadena = texto.ToCharArray();


            for (int indice = 0; indice < texto.Length; indice++)
            {
                switch (estado)
                {
                    case 0:


                        // {
                        if(cadena[indice] == 123)
                        {


                            // Comilla simple '
                        }else if(cadena[indice] == 39)
                        {


                            // :
                        }else if(cadena[indice] == 58)
                        {


                            // <
                        }else if(cadena[indice] == 60)
                        {


                            // >
                        }else if(cadena[indice] == 62)
                        {


                            // /
                        }else if(cadena[indice] == 47)
                        {


                            // .
                        }else if(cadena[indice] == 46)
                        {


                            // }
                        }else if(cadena[indice] == 125)
                        {

                        }



                        break;

                    case 1:
                        break;

                    case 2:

                        break;

                    case 3:
                        break;                    
                }
            }
        }


        private void cargarPalabrasReservadas()
        {
            listaPalabrasReservadas.Add("CrearEstructura");
            listaPalabrasReservadas.Add("'Ubicacion'");
            listaPalabrasReservadas.Add("'Estructura'");
            listaPalabrasReservadas.Add("<Carpeta>");
            listaPalabrasReservadas.Add("<Archivo>");
            listaPalabrasReservadas.Add("<Nombre>");
            listaPalabrasReservadas.Add("</Nombre>");
            listaPalabrasReservadas.Add("<Extension>");
            listaPalabrasReservadas.Add("</Extension>");
            listaPalabrasReservadas.Add("<Text>");
        }
    }
}
