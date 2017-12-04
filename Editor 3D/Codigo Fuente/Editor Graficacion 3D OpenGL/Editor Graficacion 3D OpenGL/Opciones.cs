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
    public partial class Opciones : Form
    {
        /// <summary>
        /// Variable para guardar el valor de ZP.
        /// </summary>
        private double _zp;
        /// <summary>
        /// Variable para guardar el valor de Q.
        /// </summary>
        private double _q;
        /// <summary>
        /// Variable para guardar el valor de DX.
        /// </summary>
        private double _dx;
        /// <summary>
        /// Variable para guardar el valor de DY.
        /// </summary>
        private double _dy;
        /// <summary>
        /// Variable para guardar el valor de DZ.
        /// </summary>
        private double _dz;
        /// <summary>
        /// Variable para guardar la coordenada X de dispositivo.
        /// </summary>
        private int _x;
        /// <summary>
        /// Variable para guardar la coordenada X de dispositivo.
        /// </summary>
        private int _y;
        /// <summary>
        /// Variable para guardar el tipo de visualizacion.
        /// </summary>
        private Boolean _visualizacion;
        /// <summary>
        /// Variable para guardar el tipo de dibujado.
        /// </summary>
        private Boolean _dibujado;
        /// <summary>
        /// Variable para guardar al formulario padre de este cuadro de dialogo.
        /// </summary>
        private Form1 f;


        /// <summary>
        /// Constructor del dialogo Opciones.
        /// </summary>
        public Opciones()
        {
            InitializeComponent();
            _zp = 1;
            _q = 1000;
            _dx = 0;
            _dy = 0;
            _dz = 1;
            _x = 640;
            _y = 400;

            TB_ZP.Text = "" + _zp;
            TB_Q.Text = "" + _q;
            TB_DX.Text = "" + _dx;
            TB_DY.Text = "" + _dy;
            TB_DZ.Text = "" + _dz;
            TB_X.Text = "" + _x;
            TB_Y.Text = "" + _y;

            _visualizacion = !RB_visibles.Checked;
            _dibujado = RB_Lineas.Checked;
        }
        /// <summary>
        /// Constructor del dialogo Opciones.
        /// </summary>
        /// <param name="parent">Formulario padre que crea el dialogo (Para llamar a metodos publicos del padre).</param>
        public Opciones(Form1 parent):this()
        {
            f = parent;
            _x = f.ClientSize.Width - f.ClientSize.Width/3;
            _y = f.ClientSize.Height - f.ClientSize.Height / 3;
            TB_X.Text = "" + _x;
            TB_Y.Text = "" + _y;
           
        }

        /// <summary>
        /// Obtiene el valor de ZP.
        /// </summary>
        public double ZP
        {
            get { return _zp; }
        }
        /// <summary>
        /// Obtiene el valor de Q.
        /// </summary>
        public double Q
        {
            get { return _q; }
        }
        /// <summary>
        /// Obtiene el valor de DX.
        /// </summary>
        public double DX
        {
            get { return _dx; }
        }
        /// <summary>
        /// Obtiene el valor de DY.
        /// </summary>
        public double DY
        {
            get { return _dy; }
        }
        /// <summary>
        /// Obtiene el valor de DZ.
        /// </summary>
        public double DZ
        {
            get { return _dz; }
        }
        /// <summary>
        /// Obtiene el valor de la Coordenada de Dispositivo X.
        /// </summary>
        public int X
        {
            get { return _x; }
        }
        /// <summary>
        /// Obtiene el valor de la Coordenada de Dispositivo Y.
        /// </summary>
        public int Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Obtiene el tipo de visualizacion.
        /// </summary>
        public Boolean Visualizacion
        {
            get { return _visualizacion; }
        }
        /// <summary>
        /// Obtiene el tipo de dibujado.
        /// </summary>
        public Boolean Dbujado
        {
            get { return _dibujado; }
        }


        /// <summary>
        /// Valida todos los parametros antes de aplicarlos al objeto 3D y lo actualiza.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_OK_Click(object sender, EventArgs e)
        {
            if (TB_DX.Text == "" || TB_DY.Text == "" || TB_DZ.Text == "" || TB_Q.Text == "" || TB_ZP.Text == "" || TB_X.Text == "" || TB_Y.Text == "")
                MessageBox.Show("Uno o mas campos estan vacios, intente llenarlos");
            else
            {
                _zp = double.Parse(TB_ZP.Text);
                if (_zp == 0)
                {
                    _zp = 1;
                    TB_ZP.Text = "1";
                }
                _q = double.Parse(TB_Q.Text);
                _dx = double.Parse(TB_DX.Text);
                _dy = double.Parse(TB_DY.Text);
                _dz = double.Parse(TB_DZ.Text);
                if (_dz == 0)
                {
                    _dz = 1;
                    TB_DZ.Text = "1";
                }
                _x = int.Parse(TB_X.Text);
                _y = int.Parse(TB_Y.Text);
                _visualizacion = !RB_visibles.Checked;
                _dibujado = RB_Lineas.Checked;
                f.ActualizaParametros();
            }
            
        }

        private void TB_ZP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-' || e.KeyChar == '.')
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

        private void TB_Q_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-' || e.KeyChar == '.')
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

        private void TB_DX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-' || e.KeyChar == '.')
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

        private void TB_DY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-' || e.KeyChar == '.')
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

        private void TB_DZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-' || e.KeyChar == '.')
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

        private void TB_X_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
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

        private void TB_Y_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
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
        /// Cierra el cuadro de dialogo al darle click al boton cerrar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_CANCEL_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
