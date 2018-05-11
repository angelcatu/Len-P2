using GeneradorDeCarpetas.Analisis;
using GeneradorDeCarpetas.Directorio;
using GeneradorDeCarpetas.Grafica;
using GeneradorDeCarpetas.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneradorDeCarpetas
{
    public partial class Form1 : Form
    {

        private AnalizadorLexico analizador = new AnalizadorLexico();
        private Sintactico sintactico = new Sintactico();
        private Graphviz graphviz = new Graphviz();
        private DireccionFisica directorio = new DireccionFisica();

        private List<Token> listaTokens = AnalizadorLexico.listaTokens;
        private List<Token> listaErrores = AnalizadorLexico.listaErrores;


        private String pathDeArchivo = "";


        public Form1()
        {
            InitializeComponent();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Angel Manuel Elias Catú \n 201403982 \n Lenguajes Formales de Programación 1er Semestre", "Información");
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            String texto;
            String saveText = txtEntrada.Text;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo";
            ofd.Filter = "Documentos lf (*.lf)|*.lf";

            try
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    texto = System.IO.File.ReadAllText(ofd.FileName);
                    this.txtEntrada.Text = texto;

                    pathDeArchivo = ofd.FileName;

                }
            }
            catch (Exception error)
            {
                MessageBox.Show("No se pudo abrir el archivo");
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEntrada.Text = "";
            pathDeArchivo = "";            
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pathDeArchivo.Length > 0)
            {
                sobreescribir(pathDeArchivo);

            }
            else
            {
                guardarArchivo();
            }
        }

        private void sobreescribir(string pathDeArchivo)
        {
            try
            {
                StreamWriter writer = new StreamWriter(pathDeArchivo);

                writer.WriteLine(txtEntrada.Text);

                writer.Close();                                
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo guardar el archivo", "Error");
            }
        }

        private void guardarArchivo()
        {

            SaveFileDialog sfd = new SaveFileDialog();

            try
            {
                sfd.Filter = "Archivos lf (*.lf)|*.lf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, txtEntrada.Text);

                    pathDeArchivo = sfd.FileName;                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo guardar el archivo", "Error");
            }
        }

        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            analizarTexto();
        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            analizarTexto();
        }

        private void analizarTexto()
        {
            AnalizadorLexico.listaTokens.Clear();
            AnalizadorLexico.listaErrores.Clear();
            analizador.setIdToken(0);

            try
            {
                analizador.analizarTexto(txtEntrada.Text);
                sintactico.analizarSintactico(listaTokens);
                
                if(listaErrores.Count > 0)
                {
                    MessageBox.Show("El análisis tiene errores");
                    btnDirectorios.Enabled = false;
                    btnGrafo.Enabled = false;
                    
                }
                else
                {
                    btnDirectorios.Enabled = true;
                    btnGrafo.Enabled = true;
                    MessageBox.Show("Análisis terminado");
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Ocurrió un problema en el análisis de datos");
            }
            
        }

        private void btnDirectorios_Click(object sender, EventArgs e)
        {
            directorio.crearCarpetas();
        }

        private void btnGrafo_Click(object sender, EventArgs e)
        {
            graphviz.buscarNodos();

            if (listaErrores.Count == 0)
            {
                Archivo archivo = new Archivo();
                archivo.generaconDeArbol();
                MessageBox.Show("Grafos creados", "Información");
            }
            else
            {
                MessageBox.Show("Hay errores, favor de ver el reporte", "Información");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtEntrada.Clear();
        }

        private void tablaDeTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo archivo = new Archivo("ReporteTokens", "html");
            archivo.generarReporteDeTokens();
        }

        private void tablaDeErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo archivo = new Archivo("ReporteDeErrores", "html");
            archivo.generarReporteDeErrores();
        }      
    }
}
