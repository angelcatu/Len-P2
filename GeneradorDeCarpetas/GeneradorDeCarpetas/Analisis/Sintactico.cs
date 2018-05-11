using GeneradorDeCarpetas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneradorDeCarpetas.Analisis
{
    class Sintactico
    { 
        private Boolean errorSintactico;
        private int actualPreanalisis;
        private int actualPreanalisisAux;
        private Token preanalisisAux;
        private Token preanalisis;
        private List<Token> listaDeTokens;
        private List<Token> listaErrores = AnalizadorLexico.listaErrores;

        public void analizarSintactico(List<Token> listaTokens)
        { 
            listaDeTokens = listaTokens;
            listaDeTokens.Add(new Token(0, "#", "", 0, 0));
            preanalisis = listaTokens.ElementAt(0);
            actualPreanalisis = 0;
            errorSintactico = false;

            S();
        }

        private void S()
        {
            Ins();            
        }

        private void Ins()
        {
            noLeerSaltoDeLinea();

                match("Tk_CrearEstructura");
                match("Tk_LlaveAbierta");
                NTerm();
                match("Tk_LlaveCerrada");
                match("Tk_Coma");
                Ins0();
               

            if (!preanalisis.getToken().Equals("#"))
            {
                Ins();
            }                
            
        }

        private void Ins0()
        {
            noLeerSaltoDeLinea();

            if (preanalisis.getToken().Equals("Tk_LeerArchivo"))
            {
                match("Tk_LeerArchivo");
                match("Tk_LlaveAbierta");
                    Ins2();
                match("Tk_LlaveCerrada");

                InsComa();                
            }else if (preanalisis.getToken().Equals("Tk_CrearEstructura"))
            {
                Ins();
            }

        }

        private void InsComa()
        {
            if (preanalisis.getToken().Equals("Tk_Coma"))
            {
                match("Tk_Coma");
                Ins0();
            }
        }

        private void Ins2()
        {
            noLeerSaltoDeLinea();
           
                match("Tk_Ubicacion");
                match("Tk_DosPuntos");
                match("Tk_Comilla");
                match("Tk_Cadena");
                match("Tk_Comilla");
             
        }

        private void NTerm()
        {
            noLeerSaltoDeLinea();
            
                match("Tk_Ubicacion");
                match("Tk_DosPuntos");
                match("Tk_Comilla");
                match("Tk_Cadena");
                match("Tk_Comilla");
                match("Tk_Coma");
                match("Tk_Estructura");
                match("Tk_DosPuntos");
                match("Tk_LlaveAbierta");
                NTerm2();
                match("Tk_LlaveCerrada");
            
        }
        
        private void NTerm2()
        {

            noLeerSaltoDeLinea();

            match("Tk_Carpeta");
            match("Tk_Comilla");
            match("Tk_Cadena");
            match("Tk_Comilla");
            NTerm3();
            match("Tk_CarpetaCierre");

            NTerm3();
        }

        private void NTerm3()
        {
            noLeerSaltoDeLinea();
            if(preanalisis.getToken().Equals("Tk_Carpeta"))
            {
                NTerm2();
            }else if (preanalisis.getToken().Equals("Tk_Archivo"))
            {
                NTerm4();
            }
        }
        
        private void NTerm4()
        {
            noLeerSaltoDeLinea();

            if (preanalisis.getToken().Equals("Tk_Archivo"))
            {
                match("Tk_Archivo");
                NTerm5();
                match("Tk_ArchivoCierre");
                NTerm4();
            }else if(preanalisis.getToken().Equals("Tk_Carpeta"))
            {
                NTerm3();
            }
        }

        private void NTerm5()
        {
            noLeerSaltoDeLinea();

            match("Tk_Nombre");
            match("Tk_Comilla");
            match("Tk_Cadena");
            match("Tk_Comilla");
            match("Tk_NombreCierre");

            match("Tk_Extension");
            match("Tk_Formato");
            match("Tk_ExtensionCierre");

            match("Tk_Texto");
            match("Tk_Comilla");
            match("Tk_Cadena");
            match("Tk_Comilla");
            match("Tk_TextoCierre");
        }

        private void match(String tokenActual)
        {

            noLeerSaltoDeLinea();
            
            if(errorSintactico == false)
            {
                if (!tokenActual.Equals(preanalisis.getToken()) && (!preanalisis.getToken().Equals("Tk_SaltoLinea") && !preanalisis.getToken().Equals("#")))
                {
                    //mandar a lista de errores sintácticos y avanzar hasta el próximo salto de linea
                            
                    listaErrores.Add(new Token(listaErrores.Count+1, preanalisis.getFila(), preanalisis.getColumna(), preanalisis.getLexema(), "Error cerca de " + preanalisis.getLexema()));
                    buscarSaltoDeLinea("Tk_SaltoLinea", actualPreanalisis);
                    errorSintactico = true;
                }

                if (!preanalisis.getToken().Equals("#") && errorSintactico == false)
                {
                    actualPreanalisis++;
                    preanalisis = listaDeTokens.ElementAt(actualPreanalisis);

                }
            }
            else
            {
                actualPreanalisis++;
                preanalisis = listaDeTokens.ElementAt(actualPreanalisis);
            }
                                                          
        }
       
        private void noLeerSaltoDeLinea()
        {
            if (preanalisis.getToken().Equals("Tk_SaltoLinea") && errorSintactico == false)
            {
                actualPreanalisis++;
                preanalisis = listaDeTokens.ElementAt(actualPreanalisis);                
                noLeerSaltoDeLinea();
            }

            if (preanalisis.getToken().Equals("Tk_SaltoLinea") && errorSintactico == true)
            {                
                errorSintactico = false;
                preanalisis = preanalisisAux;
                actualPreanalisis = actualPreanalisisAux;
                noLeerSaltoDeLinea();
            }
        }

        private void buscarSaltoDeLinea(string token, int actualPreanalisis)
        {
            for (int i = actualPreanalisis; i < listaDeTokens.Count; i++)
            {
                if (listaDeTokens[i].getToken().Equals(token))
                {
                    actualPreanalisis = i+1;
                    actualPreanalisisAux = actualPreanalisis;
                    preanalisisAux = listaDeTokens.ElementAt(actualPreanalisis);
                    i = listaDeTokens.Count;
                }
            }
        }

        private void buscarSaltoDeLinea(String token)
        {
            if (!token.Equals(preanalisis.getToken()))
            {
                actualPreanalisis++;
                preanalisis = listaDeTokens.ElementAt(actualPreanalisis);

                buscarSaltoDeLinea("Tk_SaltoLinea");
            }
            else
            {
                int posSalto = preanalisis.getId();
            }
        }
    }
}
