using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace WebCan
{
    public partial class Form1 : Form

    {
        Form frmActual;
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        Boolean max_min = false;
        public bool ExisteDispositivo = false;
        //private bool fotografiaHecha = false;
        private FilterInfoCollection DispositivoDeVideo;
        private VideoCaptureDevice FuenteDeVideo = null;



        public Form1()
        {
            InitializeComponent();
            BuscarDispositivos();
        }

        public void CargarDispositivos(FilterInfoCollection Dispositivos)
        {
            for (int i = 0; i < Dispositivos.Count; i++);
            cbxDispositivos.Items.Add(Dispositivos[0].Name.ToString());
            cbxDispositivos.Text = cbxDispositivos.Items[0].ToString();
        }

        public void BuscarDispositivos()
        {
            DispositivoDeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (DispositivoDeVideo.Count == 0)
            {
                ExisteDispositivo = false;
            }

            else
            {
                ExisteDispositivo = true;
                CargarDispositivos(DispositivoDeVideo);
            }
           
        }

        public void TerminarFuenteDeVideo()
        { 
            if(!(FuenteDeVideo==null))
                if (FuenteDeVideo.IsRunning)
                {
                    FuenteDeVideo.SignalToStop();
                    FuenteDeVideo = null;
                }
        }

        public void Video_NuevoFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            EspacioCamara.Image = Imagen;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (btnIniciar.Text == "Iniciar")
            {
                if (ExisteDispositivo)
                {
                    FuenteDeVideo = new VideoCaptureDevice(DispositivoDeVideo[cbxDispositivos.SelectedIndex].MonikerString);
                    FuenteDeVideo.NewFrame += new NewFrameEventHandler(Video_NuevoFrame);
                    FuenteDeVideo.Start();
                    Estado.Text = "Ejecutando Dispositivo...";
                    
                }

                else
                    Estado.Text = "Error:No se encuentra el dispositivo";
            }

           
        }

        private void Capturar()
        {
            if (FuenteDeVideo.IsRunning)
            {
                pictureBox1.Image = EspacioCamara.Image;
            }
        }
                            
        private void button1_Click(object sender, EventArgs e)
        {

            string mensaje;
            
            if (Nom.Text == "")
            {
                MessageBox.Show("Necesitas colocar tu nombre",
                     "Agregando  Nombre",
                     MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            else if (Ape.Text == "")
            {
                MessageBox.Show("Necesitas colocar tus apellidos",
                       "Agregando Apellidos",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);  
            }

            else if (Eda.Text == "")
            {
                MessageBox.Show("Necesitas agregar tu edad",
                       "Agregando Edad",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            else if (Muni.Text == "")
            {
                MessageBox.Show("Necesitas colocar tu Municipio",
                       "Agregando Municipio",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            else if (Esta.Text == "")
            {
                MessageBox.Show("Necesitas colocar tu Estado",
                       "Agregando Estado",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            else if (Fecha.Text == "")
            {
                MessageBox.Show("Necesitas colocar tu Fecha de nacimiento",
                       "Agregando Fecha de nacimiento",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            else if (TipoSA.Text == "")
            {
                MessageBox.Show("Necesitas ingresar el tipo de sangre",
                       "Agregando Tipo",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            else if (lbTutor.Text == "")
            {
                MessageBox.Show("Necesitas colocar el nombre de su tutor",
                       "Agregando Tutor",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            else if (Ma.Checked==false && Fe.Checked == false)
            {
                MessageBox.Show("Necesitas seleccionar tu sexo",
                   "Agregando Sexo",
                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            else if ( EspacioCamara.Image == null)
            {
                MessageBox.Show("Favor de capturar una imagen",
                   "Iniciando camara",
                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
            
            

            else
            {

                
             //este es la captura
            Capturar();
            ExisteDispositivo = true;

            mensaje = Nom.Text;
            Nom.Text = "";
            A0.Text = mensaje;      
        
            mensaje = Ape.Text;
            Ape.Text = "";
            A1.Text = mensaje;

            mensaje = Muni.Text;
            Muni.Text = "";
            A2.Text = mensaje;

            mensaje = Esta.Text;
            Esta.Text = "";
            A3.Text = mensaje;

            mensaje = Fecha.Text;
            Fecha.Text = "";
            A4.Text = mensaje;

            mensaje = Tutor.Text;
            Tutor.Text = "";
            A5.Text = mensaje;

            mensaje = Eda.Text;
            Eda.Text = "";
            AF.Text = mensaje;

            mensaje = TipoSA.Text;
            TipoSA.Text = "";
            AS.Text = mensaje;



            if (Fe.Checked)
            {
                Fe.Checked = false;
                A6.Text = Fe.Text;


            }


            if (Ma.Checked)
            {
                Ma.Checked = false;
                A6.Text = Ma.Text;

            }

            btnIniciar.Text = "Detener";
            cbxDispositivos.Enabled = false;
            groupBox1.Text = DispositivoDeVideo[cbxDispositivos.SelectedIndex].Name.ToString();

             
            if (FuenteDeVideo.IsRunning)
                {
                    TerminarFuenteDeVideo();
                    Estado.Text = "Dispositivo Detenido...";
                    btnIniciar.Text = "Iniciar";
                    cbxDispositivos.Enabled = true;
                               
                }

            EspacioCamara.Image = null;
            //button3.Visible = true;

            }
       
        }


                                    

        public ComboBox Imagen { get; set; }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No exixten datos capturados",
                  "Capturar Datos",
                  MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            else
            {
                Credencial _ver = new Credencial();
                _ver._Usuario = pictureBox1.Image;
                _ver._Nom = A0.Text;
                _ver._Fecha = A4.Text;
                _ver._Sexo = A6.Text;
                _ver._Ape = A1.Text;
                _ver._Eda = AF.Text;
                _ver._TipoSA = AS.Text;
                _ver.Show();

                pictureBox1.Image = EspacioCamara.Image = null;

                A0.Text = "";
                A1.Text = "";
                A2.Text = "";
                A3.Text = "";
                A4.Text = "";
                A5.Text = "";
                A6.Text = "";
                AF.Text = "";
                AS.Text = "";
            }

        }

        private void Nom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back)))
            {
                MessageBox.Show("Solo se permite letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                e.Handled = true;
                return;
            }
        }

        private void Ape_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back)))
            {
                MessageBox.Show("Solo se permite letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                e.Handled = true;
                return;
            }
        }

        private void Muni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back)))
            {
                MessageBox.Show("Solo se permite letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                e.Handled = true;
                return;
            }
        }

        private void Esta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back)))
            {
                MessageBox.Show("Solo se permite letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                e.Handled = true;
                return;
            }
        }

        private void Bachi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)  || char.IsWhiteSpace(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back)))
            {
                MessageBox.Show("Solo se permite letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                e.Handled = true;
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                activarNotificacion(e);

            base.OnClientSizeChanged(e);
        }

        private void activarNotificacion(EventArgs e)
        {
            Notificacion.Visible = true;
            Notificacion.ShowBalloonTip(0);
        }

        private void Notificacion_BalloonTipClicked(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Notificacion.Visible = false;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void reestablecerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Notificacion.Visible = false;
        }

        private void maximizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            Notificacion.Visible = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            if(max_min == false)
            {
                WindowState = FormWindowState.Maximized;
            }

            if(max_min == true)
            {
                WindowState = FormWindowState.Normal;
            }

            if(max_min == false)
            {
                max_min = true;
            }

            else
            {
                max_min = false;
            }

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            if(Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        /*public void tamanioVentana()
        {
            frmActual.Width = groupBox1.Width;
            frmActual.Height = groupBox1.Height;
        }*/
    }
}
