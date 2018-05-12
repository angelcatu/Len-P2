using GeneradorDeCarpetas.Analisis;
using GeneradorDeCarpetas.Grafica;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneradorDeCarpetas.Modelos
{
    class Archivo
    {

        private List<Token> listaTokens = AnalizadorLexico.listaTokens;
        private List<Token> listaErrores = AnalizadorLexico.listaErrores;
        private List<String> codigos = Graphviz.codigoConcatenado;

        private String nombre;
        private String extension;
        private String ruta;
        private String rutaO;

        public Archivo()
        {
            this.rutaO = "C:\\Users\\ang_e\\Documents\\";
        }

        public Archivo(String nombre, String extension)
        {
            this.nombre = nombre;
            this.extension = extension;
            this.ruta = "..\\..\\Reportes\\";
        }


        public void generarReporteDeTokens()
        {
            try
            {
                String rutaSave = Path.Combine(Application.StartupPath, ruta);

                File.WriteAllText(rutaSave + nombre + "." + extension, htmlTokens());

                abrirDocumento(rutaSave, nombre, extension);

            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo generar el repote de tokens", "Error");
            }
        }

        private string htmlTokens()
        {
            String contenido = "<html>\n"
                  + "<head>"
                  + "<utf-8>"
                  + "\n<title>REPORTE DE TOKENS</title>\n"
                  + "<style>"
                  + "body{"
                  + "background-color: #ead48a;"
                  + "}"
                  + ""
                  + "table: hover{"
                  + "width: 50%;"
                  + "}"
                  + ""
                  + "th{"
                  + "height: 25px;"
                  + "}"
                  + "</style>"
                  + ""
                  + "</head>"
                  + "<body align='center'>\n"
                  + "<table border = '1' align = 'center'>"
                  + "<caption> <h3>Reporte de tokens </h3> </caption>"
                  + "<tr>"
                  + "<th> <strong>   </strong>  </th>\n"
                  + "<th> <strong> Token   </strong>  </th>\n"
                  + "<th> <strong> Lexema   </strong>  </th>\n"
                  + "<th> <strong> Fila   </strong>  </th>\n"
                  + "<th> <strong> Columna   </strong>  </th>\n"
                  + "</tr>";

            foreach (Token token in listaTokens)
            {
                contenido += "<tr>\n"
                        + "<td align = 'center' >" + token.getId() + "</td>\n"
                        + "<td align = 'center' >" + token.getToken() + "</td>\n"
                        + "<td align = 'center' >" + token.getLexema() + "</td>\n"
                        + "<td align = 'center' >" + token.getFila() + "</td>\n"
                        + "<td align = 'center' >" + token.getColumna() + "</td>\n"

                        + "</tr>\n";
            }


            contenido += "</table>\n</body>\n</html>";



            return contenido;
        }

        private void abrirDocumento(String ruta, String nombre, String extension)
        {
            String doc = "";


            try
            {
                if (!extension.Equals(""))
                {
                    String path = ruta + nombre + "." + extension;
                    doc = Path.Combine(Application.StartupPath, path);
                }
                else
                {
                    doc = Path.Combine(Application.StartupPath, ruta + nombre);
                }


                Process.Start(doc);
            }
            catch (Exception)
            {
                MessageBox.Show("No existe el archivo", "Error");
            }
        }

        internal void generarReporteDeErrores()
        {
            try
            {
                String rutaSave = Path.Combine(Application.StartupPath, ruta);

                File.WriteAllText(rutaSave + nombre + "." + extension, htmlErrores());

                abrirDocumento(rutaSave, nombre, extension);

            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo generar el repote de tokens", "Error");
            }
        }

        private string htmlErrores()
        {

            {
                String contenido = "<html>\n"
                      + "<head>"
                      + "<utf-8>"
                      + "\n<title>REPORTE DE ERRORES</title>\n"
                      + "<style>"
                      + "body{"
                      + "background-color: #ead48a;"
                      + "}"
                      + ""
                      + "table: hover{"
                      + "width: 50%;"
                      + "}"
                      + ""
                      + "th{"
                      + "height: 25px;"
                      + "}"
                      + "</style>"
                      + ""
                      + "</head>"
                      + "<body align='center'>\n"
                      + "<table border = '1' align = 'center'>"
                      + "<caption> <h3>Reporte de errores </h3> </caption>"
                      + "<tr>"
                      + "<th> <strong>   </strong>  </th>\n"
                      + "<th> <strong> Descripcion   </strong>  </th>\n"
                      + "<th> <strong> Lexema   </strong>  </th>\n"
                      + "<th> <strong> Fila   </strong>  </th>\n"
                      + "<th> <strong> Columna   </strong>  </th>\n"
                      + "</tr>";

                foreach (Token token in listaErrores)
                {
                    contenido += "<tr>\n"
                            + "<td align = 'center' >" + token.getId() + "</td>\n"
                            + "<td align = 'center' >" + token.getDescripcion() + "</td>\n"
                            + "<td align = 'center' >" + token.getLexema() + "</td>\n"
                            + "<td align = 'center' >" + token.getFila() + "</td>\n"
                            + "<td align = 'center' >" + token.getColumna() + "</td>\n"

                            + "</tr>\n";
                }


                contenido += "</table>\n</body>\n</html>";



                return contenido;
            }
        }

        internal void abrirManual()
        {
            String path = "..\\..\\Manuales\\";

            String ruta = Path.Combine(Application.StartupPath, path);

            abrirDocumento(ruta, this.nombre, this.extension);
           
        }
    }
}
