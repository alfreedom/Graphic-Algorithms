using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Editor_Graficación_2D
{

    /// <summary>
    /// Esta clase crea un cuadro de dialogo que pide un angulo
    /// para aplicar la transformación de rotación.
    /// </summary>
    public partial class DialogRotar : Form
    {
        private int grados;

        /// <summary>
        /// Crea un nuevo cuadro de dialogo de rotacion.
        /// </summary>
        public DialogRotar()
        {
            InitializeComponent();
            grados = 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            grados=int.Parse(textBox1.Text)*-1;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                grados = int.Parse(textBox1.Text) * -1;
                this.Close();
            }
            else
            if (e.KeyChar=='-')
            {
                e.Handled = false;
            }
            else if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Regresa el numero de grados seleccionado.
        /// </summary>
        public int Grados
        {
            get
            {
                return grados;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
