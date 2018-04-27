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
        public static List<Token> listaErrores = new List<Token>();

        private int idToken = 0;
        private int idError = 0;
        private String lexema = "";
        private int fila = 1;
        private int columna = 0;
        private int estado = 0;


        public void setIdToken(int idToken)
        {
            this.idToken = idToken;
        }

        public void setIdError(int idError)
        {
            this.idError = idError;
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

                        columna++;

                        // {
                        if (cadena[indice] == 123)
                        {

                            if (!lexema.Equals(""))
                            {
                                validarToken(lexema);
                                lexema = "";
                            }

                            idToken++;
                            listaTokens.Add(new Token(idToken, "Tk_LlaveAbierta", "{", fila, columna));

                            // Comilla simple '
                        }
                        else if (cadena[indice] == 39)
                        {
                            lexema += cadena[indice];

                            // :
                        }
                        else if (cadena[indice] == 58)
                        {

                            if (!lexema.Equals(""))
                            {
                                validarToken(lexema);
                                lexema = "";
                            }

                            idToken++;
                            listaTokens.Add(new Token(idToken, "Tk_DosPuntos", ":", fila, columna));

                            // <
                        }
                        else if (cadena[indice] == 60)
                        {

                            lexema += cadena[indice];                           


                            // >
                        }
                        else if (cadena[indice] == 62)
                        {
                            lexema += cadena[indice];

                            if (!lexema.Equals(""))
                            {
                                idToken++;
                                validarToken(lexema);
                                lexema = "";
                            }

                            // /
                        }
                        else if (cadena[indice] == 47)
                        {
                            lexema += cadena[indice];

                            // .
                        }
                        else if (cadena[indice] == 46)
                        {

                            lexema += cadena[indice];


                            // }
                        }
                        else if (cadena[indice] == 125)
                        {
                            idToken++;
                            listaTokens.Add(new Token(idToken, "Tk_LlaveCerrada", "}", fila, columna));

                            // ,
                        }
                        else if (cadena[indice] == 44)
                        {
                            idToken++;
                            listaTokens.Add(new Token(idToken, "Tk_Coma", ",", fila, columna));
                            // Letras
                        }
                        else if (Char.IsLetter(cadena[indice]))
                        {

                            lexema += cadena[indice];


                            estado = 1;


                            // Comillas
                        }
                        else if (cadena[indice] == 34)
                        {

                            idToken++;
                            listaTokens.Add(new Token(idToken, "Tk_Comilla", "\"", fila, columna));

                            estado = 2;


                            //salto de línea
                        }
                        else if (cadena[indice] == 10)
                        {
                            fila++;
                            columna = 0;

                            //retorno de carro
                        }
                        else if (cadena[indice] == 13)
                        {
                            columna++;


                            // espacio
                        }
                        else if (cadena[indice] == 32)
                        {
                            columna++;
                        }
                        //Retorno
                        else if (cadena[indice] == 13)
                        {
                            columna = 0;
                            fila++;                        
                        }else if(cadena[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            // Error
                            idError++;
                            listaErrores.Add(new Token(idError, fila, columna, cadena[indice].ToString(), "Simbolo desconocido0"));
                            

                        }
                        break;

                    case 1:

                        columna++;

                        if (Char.IsLetter(cadena[indice]))
                        {
                            lexema += cadena[indice];
                            // >
                        }
                        else if (cadena[indice] == 62)
                        {
                            
                            indice--;
                            estado = 0;
                            // <
                        }
                        else if (cadena[indice] == 60)
                        {

                            if (!lexema.Equals(""))
                            {
                                idToken++;
                                listaTokens.Add(new Token(idToken, "Tk_Formato", lexema, fila, columna));
                                lexema = "";
                            }

                            lexema += cadena[indice];                            

                            estado = 0;
                            // '
                        }
                        else if (cadena[indice] == 39)
                        {                            
                            indice--;
                            estado = 0;
                        } //{
                        else if (cadena[indice] == 123)
                        {
                            //VAlidar
                            indice--;

                            estado = 0;

                            // :
                        }else if(cadena[indice] == 58)
                        {
                            indice--;
                            estado = 0;
                        
                        }    //salto de línea

                        else if (cadena[indice] == 10)
                        {
                            fila++;
                            columna = 0;
                            estado = 1;

                            //retorno de carro
                        }
                        else if (cadena[indice] == 13)
                        {
                            columna++;
                            estado = 1;

                            // espacio
                        }
                        else if (cadena[indice] == 32)
                        {
                            columna++;
                            estado = 1;
                        }else if(cadena[indice] == 13)
                        {
                            columna = 0;
                            fila++;
                        }
                        else if (cadena[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error;
                            idError++;
                            listaErrores.Add(new Token(idError, fila, columna, cadena[indice].ToString(), "Simbolo desconocido1"));
                            
                        }


                        break;

                    case 2:

                        columna++;

                        //Todos los caracteres
                        if (cadena[indice] == 58 || Char.IsLetter(cadena[indice]) || cadena[indice] == 47 || cadena[indice] == 95 ||
                            cadena[indice] == 46 || cadena[indice] == 33)
                        {
                            lexema += cadena[indice];

                            
                        }else if (Char.IsDigit(cadena[indice]))
                        {
                            lexema += cadena[indice];
                        }
                        //Comillas
                        else if (cadena[indice] == 34)
                        {                            
                            if (!lexema.Equals(""))
                            {
                                idToken++;
                                listaTokens.Add(new Token(idToken, "Tk_Cadena", lexema, fila, columna));
                                lexema = "";
                            }

                            idToken++;
                            listaTokens.Add(new Token(idToken, "Tk_Comilla", "\"", fila, columna));

                            estado = 3;
                        }
                        else if (cadena[indice] == 13)
                        {
                            columna = 0;
                            fila++;
                        }

                        //Espacio
                        else if (cadena[indice] == 32)
                        {
                            columna++;
                            lexema += cadena[indice];
                        }
                        else if (cadena[indice] == 10)
                        {
                            columna = 0;
                            fila++;
                            
                        }
                        else if (cadena[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            idError++;
                            listaErrores.Add(new Token(idError, fila, columna, cadena[indice].ToString(), "Simbolo desconocido2"));
                        }
                         
                            break;

                    case 3:


                        // espacio
                        if (cadena[indice] == 32)
                        {
                            estado = 3;
                            // ,
                        }
                        else if (cadena[indice] == 44)
                        {

                            idToken++;
                            listaTokens.Add(new Token(idToken, "Tk_Coma", ",", fila, columna));
                            //Valida                            

                            estado = 0;

                            // }
                        }else if (cadena[indice] == 125)
                        {
                            indice--;
                            estado = 0;
                        
                            // <
                        }
                        else if (cadena[indice] == 60)
                        {
                            //Validar

                            //**********                            
                            indice--;
                            estado = 0;                                                        

                        } //salto de línea

                        else if (cadena[indice] == 10)
                        {
                            fila++;
                            columna = 0;

                            if (!lexema.Equals(""))
                            {
                                idToken++;
                                listaTokens.Add(new Token(idToken, "Tk_Cadena", lexema, fila, columna));
                                lexema = "";

                                estado = 0;
                            }
                            else
                            {
                                estado = 3;
                            }
                            
                            //retorno de carro
                        }
                        else if (cadena[indice] == 13)
                        {
                            columna++;
                            estado = 3;

                            // espacio
                            
                        }
                        else if (cadena[indice] == 32)
                        {
                            columna++;                            
                        }
                        else if (cadena[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            // Error
                            idError++;
                            listaErrores.Add(new Token(idError, fila, columna, cadena[indice].ToString(), "Simbolo desconocido3"));
                        
                        }

                        break;                    
                }
            }
        }

        private void validarToken(string lexema)
        {
            idToken++;

            switch (lexema)
            {
                case "CrearEstructura":
                    listaTokens.Add(new Token(idToken, "Tk_CrearEstructura", lexema, fila, columna));
                    break;

                case "'Ubicacion'":
                    listaTokens.Add(new Token(idToken, "Tk_Ubicacion", lexema, fila, columna));
                    break;

                case "'Estructura'":
                    listaTokens.Add(new Token(idToken, "Tk_Estructura", lexema, fila, columna));
                    break;

                case "<Carpeta>":
                    listaTokens.Add(new Token(idToken, "Tk_Carpeta", "&lt;"+"Carpeta"+"&gt;", fila, columna));
                    break;

                case "<Archivo>":
                    listaTokens.Add(new Token(idToken, "Tk_Archivo", "&lt;" + "Archivo" + "&gt;", fila, columna));
                    break;

                case "<Nombre>":
                    listaTokens.Add(new Token(idToken, "Tk_Nombre", "&lt;" + "Nombre" + "&gt;", fila, columna));
                    break;

                case "</Nombre>":
                    listaTokens.Add(new Token(idToken, "Tk_NombreCierre", "&lt;" + "/Nombre" + "&gt;", fila, columna));
                    break;

                case "<Extension>":
                    listaTokens.Add(new Token(idToken, "Tk_Extension", "&lt;" + "Extension" + "&gt;", fila, columna));
                    break;

                case "</Extension>":
                    listaTokens.Add(new Token(idToken, "Tk_ExtensionCierre", "&lt;" + "/Extension" + "&gt;", fila, columna));
                    break;

                case "<Texto>":
                    listaTokens.Add(new Token(idToken, "Tk_Texto", "&lt;" + "Texto" + "&gt;", fila, columna));
                    break;

                case "</Texto>":
                    listaTokens.Add(new Token(idToken, "Tk_TextoCierre", "&lt;" + "/Texto" + "&gt;", fila, columna));
                    break;

                case "</Archivo>":
                    listaTokens.Add(new Token(idToken, "Tk_ArchivoCierre", "&lt;" + "/Archivo" + "&gt;", fila, columna));
                    break;

                case "</Carpeta>":
                    listaTokens.Add(new Token(idToken, "Tk_CarpetaCierre", "&lt;" + "/Carpeta" + "&gt;", fila, columna));
                    break;

                case "LeerArchivo":
                    listaTokens.Add(new Token(idToken, "Tk_LeerArchivo", lexema, fila, columna));
                    break;

                default:                    
                    listaTokens.Add(new Token(idToken, "Tk_Desconocido", lexema, fila, columna));

                    break;
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
            listaPalabrasReservadas.Add("<Texto>");
            listaPalabrasReservadas.Add("</Texto>");
            listaPalabrasReservadas.Add("</Archivo>");
            listaPalabrasReservadas.Add("</Carpeta>");
            listaPalabrasReservadas.Add("LeerArchivo");
        }
    }
}
