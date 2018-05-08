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
            MessageBox.Show("Cadena sin errores sintácticos", "Aviso");
        }

        private void Ins()
        {
            noLeerSaltoDeLinea();

                match("Tk_CrearEstructura");
                match("Tk_LlaveAbierta");
                NTerm();
                match("Tk_LlaveCerrada");
                match("Tk_Coma");
                match("Tk_LeerArchivo");
                match("Tk_LlaveAbierta");
                Ins2();
                match("Tk_LlaveCerrada");

                Ins();
            
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

            match("Tk_Archivo");
            NTerm5();
            match("Tk_ArchivoCierre");
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
                if (!tokenActual.Equals(preanalisis.getToken()) && (!preanalisis.getToken().Equals("Tk_SaltoLinea")))
                {
                    //mandar a lista de errores sintácticos y avanzar hasta el próximo salto de linea

                    listaErrores.Add(new Token(preanalisis.getId(), preanalisis.getFila(), preanalisis.getColumna(), preanalisis.getLexema(), "Error cerca de " + preanalisis.getLexema()));

                    errorSintactico = true;

                }

                if (!preanalisis.getToken().Equals("#"))
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
            if (preanalisis.getToken().Equals("Tk_SaltoLinea"))
            {
                actualPreanalisis++;
                preanalisis = listaDeTokens.ElementAt(actualPreanalisis);
                errorSintactico = false;
                noLeerSaltoDeLinea();
            }
        }
    }
}
