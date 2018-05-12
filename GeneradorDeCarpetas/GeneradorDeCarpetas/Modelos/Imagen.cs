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
    class Imagen
    {
        private String ruta;

        private List<String> codigos = Graphviz.codigoConcatenado;

        public Imagen()
        {
            this.ruta = "C:\\Users\\ang_e\\Documents\\";
        }


        public void generaconDeArbol()
        {
            String ruta = "..\\..\\Grafos\\";
            String imagen = Path.Combine(Application.StartupPath, ruta);

            crearArchivoDOT();

            //String imagen = "..\\..\\Reportes\\";
            //String path = Path.Combine(Application.StartupPath, imagen);

            for (int i = 0; i < codigos.Count; i++)
            {
                generarImagen("grafo" + i + ".dot", imagen);
                //abrirDocumento(this.rutaO, "grafo" + i, "png");
            }
        }

        private void generarImagen(string nombre, string ruta)
        {
            try
            {
                var command = string.Format("dot -Tpng " + this.ruta + nombre + " -o {1}", Path.Combine(this.ruta, nombre), Path.Combine(this.ruta, nombre.Replace(".dot", ".png")));

                var procStarInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C " + command);

                var proc = new System.Diagnostics.Process();

                proc.StartInfo = procStarInfo;

                proc.Start();

                proc.WaitForExit();
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: no se puede generar la imagen", "Error");
            }
        }

        private void crearArchivoDOT()
        {
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = null;

                //Write a line of text
                if (codigos.Count > 0)
                {
                    for (int i = 0; i < codigos.Count; i++)
                    {
                        sw = new StreamWriter(@ruta + "grafo" + i + ".dot");
                        sw.WriteLine(codigos[i]);

                        //Close the file
                        sw.Close();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        public void reporteHTML(String nombreArchivo, String extension)
        {
            try
            {
                File.WriteAllText(this.ruta + nombreArchivo + "." + extension, reporteArbol(this.ruta));
                abrirDocumento(this.ruta, nombreArchivo, extension);
            }
            catch (Exception e)
            {
                
            }
        }

        private string reporteArbol(string rutaAbrir)
        {
            String contenido = "<html>\n"
                + "<head>"
                + "<utf-8>"
                + "\n<title>REPORTE DE GRAFICAS</title>\n"
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
                + "<caption> <h3> Graficas </h3> </caption>"
                + "<tr>"
                + "<th> <strong> Nombre </strong>  </th>\n"
                + "<th> <strong> Imagen </strong>  </th>\n"
                + "</tr>";

            for (int i = 0; i < codigos.Count; i++)
            {

                contenido += "<tr>\n"

                                 + "<td align = 'center'>" + "grafo" + i + "</td>"

                                 + "<td align = 'center'> \n"
                                          + "<img src = " + '"' + "grafo" + i + ".png" + '"' + "/>\n"
                                 + "</td>\n"

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
    }
}
