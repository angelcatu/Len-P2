﻿using GeneradorDeCarpetas.Analisis;
using GeneradorDeCarpetas.Modelos;
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
        private List<Token> listaDeTokens = AnalizadorLexico.listaTokens;
        public static List<String> codigoGraphviz = new List<string>();
        public static List<String> codigoConcatenado = new List<String>();

        private Token token;
        private int actualPreanalisis;

        private int numNodo;
        private String nodoPadre;
        private String nodoHijo;

        private String codigo;

        public void buscarNodos()
        {
            listaDePadres.Clear();
            codigoGraphviz.Clear();
            codigoConcatenado.Clear();
            numNodo = 0;
            nodoPadre = "";
            nodoHijo = "";
            actualPreanalisis = 0;

            token = listaDeTokens.ElementAt(0);
            
            graficar(actualPreanalisis);

            concatenarCodigo();
        }

        private void graficar(int actualPreanalisis)
        {
            token = listaDeTokens.ElementAt(actualPreanalisis);
            String nodo = "nodo";
            String titulo;
            int actualPreanalisisAux;

            if (token.getToken().Equals("Tk_CrearEstructura"))
            {
                codigoGraphviz.Add("digraph G {graph [rankdir = \"TB\"]; node[fontsize = \"16\" shape = \"ellipse\" ]; ");
                actualPreanalisisAux = actualPreanalisis;
                graficar(actualPreanalisisAux + 1);

            }
            else if (token.getToken().Equals("Tk_Ubicacion"))
            {

                if (listaDeTokens[actualPreanalisis - 3].getToken().Equals("Tk_CrearEstructura"))
                {
                    agregarNodoGraphiz(nodo + Convert.ToString(numNodo), "Carpeta proyecto");
                    listaDePadres.Add(nodo + Convert.ToString(numNodo));
                    actualPreanalisisAux = actualPreanalisis;
                    graficar(actualPreanalisisAux + 1);
                }
                else
                {
                    actualPreanalisisAux = actualPreanalisis;
                    graficar(actualPreanalisisAux + 1);
                }

                    
            

            } else if (token.getToken().Equals("Tk_Carpeta"))
            {
                numNodo++;

                titulo = listaDeTokens.ElementAt(actualPreanalisis + 2).getLexema();
                agregarNodoGraphiz(nodo + Convert.ToString(numNodo), titulo);

                nodoPadre = listaDePadres[listaDePadres.Count - 1];
                nodoHijo = nodo + Convert.ToString(numNodo);

                agregarEnlaceNodo(nodoPadre, nodoHijo);
                nodoPadre = nodoHijo;
                listaDePadres.Add(nodoPadre);
                actualPreanalisisAux = actualPreanalisis;
                graficar(actualPreanalisisAux + 1);


            } else if (token.getToken().Equals("Tk_Nombre"))
            {
                numNodo++;

                titulo = listaDeTokens.ElementAt(actualPreanalisis + 2).getLexema();
                agregarNodoGraphiz(nodo + Convert.ToString(numNodo), titulo);

                nodoPadre = listaDePadres[listaDePadres.Count - 1];
                nodoHijo = nodo + Convert.ToString(numNodo);

                agregarEnlaceNodo(nodoPadre, nodoHijo);

                actualPreanalisisAux = actualPreanalisis;
                graficar(actualPreanalisisAux + 1);


            } else if (token.getToken().Equals("Tk_CarpetaCierre"))
            {

                listaDePadres.RemoveAt(listaDePadres.Count - 1);
                actualPreanalisisAux = actualPreanalisis;
                graficar(actualPreanalisisAux + 1);

            } else if (token.getLexema().Equals(",") && !listaDeTokens[actualPreanalisis - 1].getToken().Equals("Tk_Comilla"))
            {                
                    codigoGraphviz.Add("}");
                    actualPreanalisisAux = actualPreanalisis;
                    graficar(actualPreanalisisAux + 1);                                                                                        
                
            }else if (token.getToken().Equals("#"))
            {

            }
            else
            {
                actualPreanalisisAux = actualPreanalisis;
                graficar(actualPreanalisisAux + 1);
            }
        }

        int a = 0;
        private void agregarEnlaceNodo(string nodoPadre, string nodoHijo)
        {
            codigo = "\""+nodoPadre+"\"" +"-> "+ "\""+nodoHijo+"\";" ;

            codigoGraphviz.Add(codigo);
        }

        private void agregarNodoGraphiz(string nodo, string titulo)
        {

            codigo = "\""+nodo+"\"" + "["
                            + "label = "+"\""+titulo+"\""
                            + "shape = "+"\""+"record"+"\""
                            + "]; ";

            codigoGraphviz.Add(codigo);
        }
        
        private void concatenarCodigo()
        {
            String codigo = "";


            for (int i = 0; i < codigoGraphviz.Count; i++)
            {
                if (codigoGraphviz[i].Equals("}") && codigoGraphviz[i - 1].Equals("}"))
                {
                    codigoGraphviz.RemoveAt(i);
                    i--;
                }
            }
                     
                for (int j = 0; j < codigoGraphviz.Count; j++)
                {
                    if (codigoGraphviz[j].Equals("}"))
                    {
                        codigo += codigoGraphviz[j];                        
                        codigoConcatenado.Add(codigo);
                    codigo = "";
                    }
                    else
                    {
                        codigo += codigoGraphviz[j];
                    }

                                        
                }            
            
        }
    }
}
