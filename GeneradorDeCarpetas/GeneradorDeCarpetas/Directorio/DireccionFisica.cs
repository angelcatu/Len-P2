using GeneradorDeCarpetas.Analisis;
using GeneradorDeCarpetas.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneradorDeCarpetas.Directorio
{
    class DireccionFisica
    {
        
    
        private List<String> listaDirectoriosPadres = new List<String>();
        private List<Token> listaDeTokens = AnalizadorLexico.listaTokens;                
        private Token token;
        private int actualPreanalisis;

        
        private String rutaPadre;
        private String rutaHijo;
        private String archivo;
        

        private String directorio;

        public void crearCarpetas()
        {
            listaDirectoriosPadres.Clear();            
            rutaPadre = "";
            rutaHijo = "";
            actualPreanalisis = 0;

            token = listaDeTokens.ElementAt(0);

            generarDirectorio(actualPreanalisis);            
        }
        int a = 0;

        private void generarDirectorio(int actualPreanalisis)
        {
            token = listaDeTokens.ElementAt(actualPreanalisis);            
            int actualPreanalisisAux;

            if (token.getToken().Equals("Tk_CrearEstructura"))
            {
                
                actualPreanalisisAux = actualPreanalisis;
                generarDirectorio(actualPreanalisisAux + 1);

            }
            else if (token.getToken().Equals("Tk_Ubicacion"))
            {

                if (listaDeTokens[actualPreanalisis - 3].getToken().Equals("Tk_CrearEstructura"))
                {
                    rutaPadre = listaDeTokens[actualPreanalisis + 3].getLexema()+"/CarpetaProyecto/";
                    directorio = rutaPadre;
                    listaDirectoriosPadres.Add(directorio);       
                    
                    actualPreanalisisAux = actualPreanalisis;
                    generarDirectorio(actualPreanalisisAux + 1);
                }
                else
                {
                    actualPreanalisisAux = actualPreanalisis;
                    generarDirectorio(actualPreanalisisAux + 1);
                }
            }
            else if (token.getToken().Equals("Tk_Carpeta"))
            { 
                
                rutaPadre = listaDirectoriosPadres[listaDirectoriosPadres.Count - 1];
                rutaHijo = listaDeTokens[actualPreanalisis + 2].getLexema()+"/";

                directorio = rutaPadre + rutaHijo;
                //rutaPadre = rutaHijo;
                listaDirectoriosPadres.Add(directorio);

                actualPreanalisisAux = actualPreanalisis;
                generarDirectorio(actualPreanalisisAux + 1);


            }
            else if (token.getToken().Equals("Tk_Nombre"))
            {
                       
                rutaPadre = listaDirectoriosPadres[listaDirectoriosPadres.Count - 1];
                archivo = listaDeTokens[actualPreanalisis + 2].getLexema();

                crearDirectorio(rutaPadre);                

                actualPreanalisisAux = actualPreanalisis;
                generarDirectorio(actualPreanalisisAux + 1);


            }else if (token.getToken().Equals("Tk_Extension"))
            {
                archivo += listaDeTokens[actualPreanalisis + 1].getLexema();

                actualPreanalisisAux = actualPreanalisis;
                generarDirectorio(actualPreanalisisAux + 1);


            }else if (token.getToken().Equals("Tk_Texto"))
            {
                rutaHijo = archivo;
                String texto = listaDeTokens[actualPreanalisis + 2].getLexema();
                escribirArchivo(directorio, rutaHijo, texto);

                actualPreanalisisAux = actualPreanalisis;
                generarDirectorio(actualPreanalisisAux + 1);
            }
            else if (token.getToken().Equals("Tk_CarpetaCierre"))
            {

                listaDirectoriosPadres.RemoveAt(listaDirectoriosPadres.Count - 1);

                actualPreanalisisAux = actualPreanalisis;
                generarDirectorio(actualPreanalisisAux + 1);

            }else if (token.getToken().Equals("Tk_LeerArchivo"))
            {
                String archivo = listaDeTokens[actualPreanalisis + 6].getLexema();

                abrirDocumento(archivo);

                actualPreanalisisAux = actualPreanalisis;
                generarDirectorio(actualPreanalisisAux + 1);
            }
            else if (token.getToken().Equals("Tk_ArchivoCierre"))
            {
                
                actualPreanalisisAux = actualPreanalisis;
                generarDirectorio(actualPreanalisisAux + 1);

            }
            else if (token.getToken().Equals("#"))
            {

            }
            else
            {
                actualPreanalisisAux = actualPreanalisis;
                generarDirectorio(actualPreanalisisAux + 1);
            }
        }       

        private void escribirArchivo(String rutaPadre, String rutaHijo, String texto)
        {
            try
            {
                String path = System.IO.Path.Combine(rutaPadre, rutaHijo);
                StreamWriter sw = null;


              crearDirectorio(rutaPadre);


                using (System.IO.FileStream fs = System.IO.File.Create(path)) ;                   

                sw = new StreamWriter(path);
                sw.WriteLine(texto);

                //Close the file
                sw.Close();
                archivo = "";
            }
            catch(Exception e)
            {
                MessageBox.Show("Error: no se pudo generar el archivo en la ruta", "Error");
            }            
        }
        

        private void crearDirectorio(string rutaPadre)
        {
            try
            {
                DirectoryInfo di = Directory.CreateDirectory(rutaPadre);
            }
            catch(Exception e)
            {
                MessageBox.Show("Error: no se creó el directorio", "Error");
            }            
        }

        private void abrirDocumento(String ruta)
        {
            String doc = "";
            try
            {
                String path = ruta;
                doc = Path.Combine(Application.StartupPath, path);
             
                Process.Start(doc);
            }
            catch (Exception)
            {
                MessageBox.Show("No existe el archivo", "Error");
            }
        }
    }


}

