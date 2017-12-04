using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Editor_Graficacion_3D_OpenGL
{
    public partial class Giros : Form
    {
        /// <summary>
        /// Guarda el sentido en el que girara el objeto 3D.
        /// </summary>
        private int giro;
        /// <summary>
        /// Guarda la velocidad con la que girara el objeto 3D.
        /// </summary>
        private int velocidad;

        /// <summary>
        /// Constructor del dialogo de Giros.
        /// </summary>
        public Giros()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Se cierra el cuadro de dialogo al dar click en el boton "Cerrar".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Establece el giro en Y al seleccionarse el eje Y.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RB_Y_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Y.Checked)
                giro=2;            
        }
        /// <summary>
        /// Establece el giro en X al seleccionarse el eje X.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RB_X_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_X.Checked)
                giro=-1;
        }
        /// <summary>
        /// Establece el giro en Z al seleccionarse el eje Z.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RB_Z_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Z.Checked)
                giro=3;
        }
        /// <summary>
        /// Establece el giro en -X al seleccionarse el eje -X.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RB_XN_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_XN.Checked)
                giro=1;
        }
        /// <summary>
        /// Establece el giro en -Y al seleccionarse el eje -Y.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RB_YN_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_YN.Checked)
                giro=-2;
        }
        /// <summary>
        /// Establece el giro en -Y al seleccionarse el eje -Y.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RB_ZN_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_ZN.Checked)
                giro=-3;
        }

        /// <summary>
        /// Obtiene el sentido de Giro.
        /// </summary>
        public int GIRO
        {
            get { return giro; }
        }
        /// <summary>
        /// Obtiene la velocidad de Giro.
        /// </summary>
        public int Velocidad
        {
            get { return (int)numericUpDown1.Value; }
        }
    }
}
