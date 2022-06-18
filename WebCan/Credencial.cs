using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebCan
{
    public partial class Credencial : Form
    {
        public Image _Usuario;
        public string _Nom;
        public string _Fecha;
        public string _Sexo;
        public string _Ape;
        public string _Eda;
        public string _TipoSA;



        public Credencial()
        {
             InitializeComponent();
        }

        private void Credencial_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = _Usuario;
            AB.Text = _Nom;
            AC.Text = _Fecha;
            AD.Text = _Sexo;
            AE.Text = _Ape;
            A7.Text = _Eda;
            A8.Text = _TipoSA;

        }
           

        
    }
}
