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
    /// Crea un cuadro de dialogo que muestra informacion sobre el
    /// proyecto.
    /// </summary>
    public partial class About : Form
    {
        /// <summary>
        /// Crea un nuevo cuadro de dialogo.
        /// </summary>
        public About()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Cierra el cuadro de dialogo al dar click en el boton Aceptar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
 
        private void BTAceptar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        
    }
}
