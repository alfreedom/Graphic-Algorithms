using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.Platform.Windows;
using Tao.Platform;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Editor_Graficación_2D
{
    /// <summary>
    /// Clase principal del formulario.
    /// En esta clase se manipulan las acciones para la selección, edición,
    /// y dibujado de las figuras, ya sea mediante el teclado o el ratón.
    /// </summary>
    public partial class Principal : Form
    {
        /// <summary>
        /// Bandera para saber si el dibujo se ha guardado.
        /// </summary>
        private Boolean guardado;
        /// <summary>
        /// Guarda el nombre del archivo abierto.
        /// </summary>
        private string nomarch;
        /// <summary>
        /// Guarda el rango para la busqueda de un punto en un figura.
        /// </summary>
        private const int RANGO = 10;
        /// <summary>
        /// Guarda la opcion seleccionada por el usuario.
        /// </summary>
        private char _op;
        /// <summary>
        /// Guarda el color primario para dibujar.
        /// </summary>
        private Color _pixelcolor;
        /// <summary>
        /// Guarda el punto del raton al dar click.
        /// </summary>
        private Point _p1;
        /// <summary>
        /// Guarda el punto cada que se desplaza el mouse.
        /// </summary>
        private Point _p2;
        /// <summary>
        /// Guarda las figuras creadas en una lista de figuras.
        /// </summary>
        private List<Figura> _listaFiguras;
        /// <summary>
        /// Guarda la figura actual, para el efecto de arrastre.
        /// </summary>
        private Figura _figura;
        /// <summary>
        /// Guarda el resultado de la llamada a la funcion
        /// PointInFig(Point p, int Range), que depende de
        /// la figura checada.
        /// </summary>
        private int _ptresult;
        /// <summary>
        /// Guarda el indice de la paleta activa que se
        /// esta vsualizando.
        /// </summary>
        private int _indicepaleta;
        /// <summary>
        /// Guarda las paletas de colores.
        /// </summary>
        private List<Image> _listapaletas;
        /// <summary>
        /// Guarda el cursor del mouse que estaba activo
        /// antes de modificarlo.
        /// </summary>
        private Cursor _oldcursor;
        /// <summary>
        /// Guarda el radio ya sea de un Circulo o de una Elipse.
        /// </summary>
        private int _radio;
        /// <summary>
        /// Guarda el ancho de linea para crear las figuras.
        /// </summary>
        private int _ancholinea;
        /// <summary>
        /// Guarda el estilo de linea para crear las figuras.
        /// </summary>
        private int _estilolinea;
        /// <summary>
        /// Guarda los grados que se debe rotar una figura.
        /// </summary>
        private int _gradosarotar;
        


        /// <summary>
        /// Constructor del formulario principal.
        /// Se inicializan todos los componentes de la aplicación.
        /// </summary>
        public Principal()
        {
            InitializeComponent();
            _listaFiguras = new List<Figura>();
            GLOpengl.InitializeContexts();
        }
        /// <summary>
        /// Se carga el formulario y se inicializan todas las variables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            _op = '0';
            _ancholinea = 1;
            _estilolinea = 1;
            _pixelcolor = PB_color1.BackColor;
            _p1 = new Point(0,menuStrip1.Height);
            _indicepaleta = 0;
            this.CargaPaletas();
            _oldcursor = this.Cursor;
            Statusgrados.Visible = false;
            guardado = true;
            TB_R.Text = "" + _pixelcolor.R;
            TB_G.Text = "" + _pixelcolor.G;
            TB_B.Text = "" + _pixelcolor.B;
            
            
        }
        /// <summary>
        /// Carga las imagenes de las paletas en una lista.
        /// </summary>
        private void CargaPaletas()
        {
            _listapaletas = new List<Image>();            
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_default);
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_1);
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_2);
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_3);
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_4);
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_5);
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_6);
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_7);
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_8);
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_9);
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_10);
            _listapaletas.Add(Editor_Graficación_2D.Properties.Resources.Paleta_11);
            PB_paleta.Image = _listapaletas[_indicepaleta];
            
        }

        //*****************************************************************
        //Operaciones con los botones del menu
        /// <summary>
        /// Selecciona la opccion para dibujar un pixel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pixelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _op = 'p';
            ActivaOpcionMenu();

        }
        /// <summary>
        /// Selecciona la opcion para dibujar una linea.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lineaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _op = 'l';
            ActivaOpcionMenu();
            GLOpengl.Refresh();
        }
        /// <summary>
        /// Selecciona la opcion para dibujar un circulo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void circuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _op = 'c';
            ActivaOpcionMenu();
            GLOpengl.Refresh();
        }
        /// <summary>
        /// Selecciona la opcion para dibujar un Elipse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void elipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _op = 'e';
            ActivaOpcionMenu();
            GLOpengl.Refresh();
            
        }
        /// <summary>
        /// Selecciona la opcion para dibujar una Curva de Hermite.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void curvaDeHermiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _op = 'h';
            ActivaOpcionMenu();
            GLOpengl.Refresh();
        }
        /// <summary>
        /// Selecciona la opcion para cambiar el color de la figura.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cambiarColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _op = 'z';
            ActivaOpcionMenu();
            GLOpengl.Refresh();
        }
        /// <summary>
        /// Selecciona la opcion para mover la figura.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _op = 'm';
            ActivaOpcionMenu();
            GLOpengl.Refresh();
        }
        /// <summary>
        /// Selecciona la opcion para borrar una figura.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void borrarFiguraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _op = 'd';
            ActivaOpcionMenu();
            GLOpengl.Refresh();
        }
        /// <summary>
        /// Selecciona la opcion para dibujar una Curva de Bezier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void curvaDeBezierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _op = 'b';
            ActivaOpcionMenu();
            GLOpengl.Refresh();
        }

        /// <summary>
        /// Selecciona el color del pixel mediante un cuadro de dialogo de color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorDePixelToolStripMenuItem_Click(object sender, EventArgs e)
        {
           _pixelcolor= GetColor();

        }      
        /// <summary>
        /// Limpia la pantalla y elimina las figuras.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void limpiaPantallaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listaFiguras.RemoveRange(0, _listaFiguras.Count);
            _figura = null;
            GLOpengl.Refresh();
            Status_fig.Text = "Figuras:   " + _listaFiguras.Count;
        }
        /// <summary>
        /// Activa visualmente la opción actual.
        /// </summary>
        public void ActivaOpcionMenu()
        {
            Statusgrados.Visible = false;
            lineaToolStripMenuItem.Checked = false;
            pixelToolStripMenuItem.Checked = false;
            moverToolStripMenuItem.Checked = false;
            cambiarColorToolStripMenuItem.Checked = false;
            elipseToolStripMenuItem.Checked = false;
            circuloToolStripMenuItem.Checked = false;
            rotarToolStripMenuItem.Checked = false;
            curvaDeHermiteToolStripMenuItem.Checked = false;
            borrarFiguraToolStripMenuItem.Checked = false;
            curvaDeBezierToolStripMenuItem.Checked = false;
            BT_pixel.Image = Editor_Graficación_2D.Properties.Resources.pixel_unselect;
            BT_line.Image = Editor_Graficación_2D.Properties.Resources.line_unselect;
            BT_circle.Image = Editor_Graficación_2D.Properties.Resources.circle_unselect;
            BT_move.Image = Editor_Graficación_2D.Properties.Resources.hand_unselect;
            BT_cambiacolor.Image = Editor_Graficación_2D.Properties.Resources.changecolor_unselect;
            BT_elipse.Image = Editor_Graficación_2D.Properties.Resources.ellipse_unselect;
            BT_hermite.Image = Editor_Graficación_2D.Properties.Resources.curve_unselect;
            BT_bezier.Image = Editor_Graficación_2D.Properties.Resources.bezier_unselect;
            BT_delete.Image = Editor_Graficación_2D.Properties.Resources.delete_unselect;
            BT_rotar.Image = Editor_Graficación_2D.Properties.Resources.rotar_unselect;
            switch (_op)
            {
                case 'l':
                    lineaToolStripMenuItem.Checked = true;
                    BT_line.Image = Editor_Graficación_2D.Properties.Resources.line_select;
                    break;
                case 'p':
                    pixelToolStripMenuItem.Checked = true;
                    BT_pixel.Image = Editor_Graficación_2D.Properties.Resources.pixel_select;
                    break;
                case 'b':
                    curvaDeBezierToolStripMenuItem.Checked = true;
                    BT_bezier.Image = Editor_Graficación_2D.Properties.Resources.bezier_select;
                    break;
                case 'c':
                    circuloToolStripMenuItem.Checked = true;
                    BT_circle.Image = Editor_Graficación_2D.Properties.Resources.circle_select;
                    break;
                case 'd':
                    BT_delete.Image = Editor_Graficación_2D.Properties.Resources.delete_select;
                    borrarFiguraToolStripMenuItem.Checked = true;
                    break;
                case 'e':
                    elipseToolStripMenuItem.Checked = true;
                    BT_elipse.Image = Editor_Graficación_2D.Properties.Resources.ellipse_select;
                    break;
                case 'h':                    
                    curvaDeHermiteToolStripMenuItem.Checked = true;
                    BT_hermite.Image = Editor_Graficación_2D.Properties.Resources.curve_select;
                    break;
                case 'm':
                    moverToolStripMenuItem.Checked = true;
                    BT_move.Image = Editor_Graficación_2D.Properties.Resources.hand_select;
                    break;
                case 'r':
                    rotarToolStripMenuItem.Checked = true;
                    BT_rotar.Image = Editor_Graficación_2D.Properties.Resources.rotar_select;
                    break;
                case 'z':
                    cambiarColorToolStripMenuItem.Checked = true;
                    BT_cambiacolor.Image = Editor_Graficación_2D.Properties.Resources.changecolor_select;
                    break;
            }
        }
        /// <summary>
        /// Invierte el color de fondo de blanco a negro y viceverza.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invertirColorDeFondoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GLOpengl.BackColor == Color.White)
                GLOpengl.BackColor = Color.Black;
            else
                if (GLOpengl.BackColor == Color.Black)
                    GLOpengl.BackColor = Color.White;
        }
        //*****************************************************************
        

        //*****************************************************************
        //Operaciones De dibujado de figuras
        /// <summary>
        /// Dibuja todas las figuras que haya en la lista de figuras.
        /// </summary>
        public void DibujaFiguras()
        {
            foreach (Figura f in _listaFiguras)
            {
                Gl.glPointSize(f.Grosor);
                f.Dibuja();
            }

        }
        /// <summary>
        /// Dibuja las marcas de los origenes de la figura para aplicar transformaciones.
        /// </summary>
        public void DibujaOrigenes()
        {
            Point p=new Point();
            Point p2;
            int inc = 8;
            foreach (Figura f in _listaFiguras)
            {
                Gl.glColor4ub(f.Color.R, f.Color.G, f.Color.B, 0);
                if (f.Color == GLOpengl.BackColor)
                    if (f.Color == Color.White)
                        Gl.glColor4ub(0, 0, 0, 0);
                    else
                        Gl.glColor4ub(250, 250, 250, 0);                    
                    
                switch (f.TipoFigura)
                {
                    case Figura.Tipo.Line:
                        p = ((Linea)f).PI;
                        p2=((Linea)f).PF;


                        Gl.glVertex2d(p2.X, p2.Y - inc);
                        Gl.glVertex2d(p2.X, p2.Y + inc);
                        Gl.glVertex2d(p2.X - inc, p2.Y);
                        Gl.glVertex2d(p2.X + inc, p2.Y);
                    break;
                    case Figura.Tipo.Circle:
                        p=((Circulo)f).Origen;
                    break;
                    case Figura.Tipo.Ellipse:
                        p = ((Elipse)f).Origen;
                    break;
                    case Figura.Tipo.CHermite:
                    case Figura.Tipo.CBezier:
                        p = ((Curva)f).PI;
                        p2=((Curva)f).PF;
                        Point p3, p4;
                        p3 = ((Curva)f).PCTRL1;
                        p4 = ((Curva)f).PCTRL2;
                        Gl.glVertex2d(p2.X, p2.Y - inc);
                        Gl.glVertex2d(p2.X, p2.Y + inc);
                        Gl.glVertex2d(p2.X - inc, p2.Y);
                        Gl.glVertex2d(p2.X + inc, p2.Y);

                        Gl.glVertex2d(p3.X, p3.Y - inc);
                        Gl.glVertex2d(p3.X, p3.Y + inc);
                        Gl.glVertex2d(p3.X - inc, p3.Y);
                        Gl.glVertex2d(p3.X + inc, p3.Y);

                        Gl.glVertex2d(p4.X, p4.Y - inc);
                        Gl.glVertex2d(p4.X, p4.Y + inc);
                        Gl.glVertex2d(p4.X - inc, p4.Y);
                        Gl.glVertex2d(p4.X + inc, p4.Y);
                        
                    break;
                }


                Gl.glVertex2d(p.X, p.Y - inc);
                Gl.glVertex2d(p.X, p.Y + inc);
                Gl.glVertex2d(p.X - inc, p.Y);
                Gl.glVertex2d(p.X + inc, p.Y);
                
              
            }
        }
        /// <summary>
        /// Regresa el color seleccionado en el cuadro de dialogo de color.
        /// </summary>
        /// <returns></returns>
        private Color GetColor()
        {
            Color ret = _pixelcolor;
            dlg_color.Color = _pixelcolor;
            if (dlg_color.ShowDialog() == DialogResult.OK)
                ret = dlg_color.Color;
            return ret;
        }
        //*****************************************************************
       

        //*****************************************************************
        //Operaciones con el objeto OpenGL
        /// <summary>
        /// Se produce cuando el se da click en el objeto GL.
        /// Sirve para inicializar las coordenadas iniciales de
        /// la figura seleccionada, para cambiar el color de alguna
        /// figura o para obtener las coordenadas de una figura respecto
        /// al mouse.
        /// </summary>
        private void GIOpengl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                PB_invertircolor_Click(null, null);
            else
            if (e.Button == MouseButtons.Left)
            {
                _p1=_p2 = e.Location;                
                switch (_op)
                {
                    case 'p':
                        _figura = new Pixel(_p1, _pixelcolor,_ancholinea);
                        GLOpengl.Refresh();
                    break;
                    case 'l':
                    _figura = new Linea(_p1, _p2, _pixelcolor, _ancholinea);
                    break;
                    case 'c':
                    _figura = new Circulo(_p1, _p2, _pixelcolor, _ancholinea);
                        GLOpengl.Refresh();
                    break;
                    case 'e':
                    _figura = new Elipse(_p1, _p2, _pixelcolor, _ancholinea);
                        GLOpengl.Refresh();
                    break;
                    case 'h':
                    _figura = new Curva(_p1, _p2, _pixelcolor, Figura.Tipo.CHermite, _ancholinea);
                         GLOpengl.Refresh();
                    break;
                    case 'b':
                    _figura = new Curva(_p1, _p2, _pixelcolor, Figura.Tipo.CBezier, _ancholinea);
                         GLOpengl.Refresh();
                    break;
                    case 'r':
                        _figura = null;
                        foreach (Figura fi in _listaFiguras)
                        {
                            _ptresult = fi.PointInFig(e.Location, RANGO);
                            if (_ptresult != 0)
                            {
                                _figura = fi;
                                break;
                            }
                        }

                        if (_figura != null)
                        {
                            DialogRotar dlgr = new DialogRotar();
                            if (dlgr.ShowDialog() == DialogResult.OK)
                            {
                                if (_figura.TipoFigura == Figura.Tipo.Ellipse)
                                    ((Elipse)_figura).AnguloRotacion += dlgr.Grados;
                                _figura.Rota(dlgr.Grados);
                                GLOpengl.Refresh();
                                guardado = false;
                            }
                            dlgr.Dispose();
                        }
                    break;
                    case 'm':
                        foreach (Figura fi in _listaFiguras)
                        {
                            _ptresult = fi.PointInFig(e.Location, RANGO);
                            if (_ptresult != 0)
                            {
                                _figura = fi;
                                break;
                            }
                        }
                    break;
                    case 'd':
                    foreach (Figura fi in _listaFiguras)
                    {
                        _ptresult = fi.PointInFig(e.Location, RANGO);
                        if (_ptresult != 0)
                        {
                            _listaFiguras.Remove(fi);
                            _figura = null;
                            GLOpengl.Refresh();
                            guardado = false;
                            break;
                        }
                    }
                    break;
                    case 'z':
                        foreach (Figura fi in _listaFiguras)
                        {
                            if (fi.PointInFig(e.Location, RANGO) != 0)
                            {
                                fi.Color = _pixelcolor;
                                GLOpengl.Refresh();
                                guardado = false;
                                break;
                            }
                        }
                    break;
                }

            }

            
        }
        /// <summary>
        /// Se produce cuando se mueve el mouse en el objeto GL.
        /// Sirve para dibujar con arraste, mover, cambiar color,
        /// escalar y rotar las figuras.
        /// </summary>
        private void GIOpengl_MouseMove(object sender, MouseEventArgs e)
        {
           
            StatusCoord.Text="X="+e.X+" , Y="+e.Y;
            if (e.Button == MouseButtons.Left)
            {
                _p2 = e.Location;
                switch (_op)
                {
                    case 'l':
                        _figura = new Linea(_p1, _p2, _pixelcolor, _ancholinea);
                        _figura.Estilo = _estilolinea;
                        GLOpengl.Refresh();
                        break;
                    case 'c':
                        _figura = new Circulo(_p1, _p2, _pixelcolor, _ancholinea);
                        _figura.Estilo = _estilolinea;
                        GLOpengl.Refresh();
                        break;
                    case 'e':
                        _figura = new Elipse(_p1, _p2, _pixelcolor, _ancholinea);
                        _figura.Estilo = _estilolinea;
                        GLOpengl.Refresh();
                        break;
                    case 'h':
                        _figura = new Curva(_p1, _p2, _pixelcolor, Figura.Tipo.CHermite, _ancholinea);
                        _figura.Estilo = _estilolinea;
                        GLOpengl.Refresh();
                        break;
                    case 'b':
                        _figura = new Curva(_p1, _p2, _pixelcolor, Figura.Tipo.CBezier, _ancholinea);
                        _figura.Estilo = _estilolinea;
                        GLOpengl.Refresh();
                        break;
                    /*case 'r':
                        if (_figura != null)
                        {
                            int inc = _p2.X - _p1.X;
                            _gradosarotar += inc/2;
                            _p1 = _p2;
                            _figura.Rota(_gradosarotar);
                            Statusgrados.Text = "Grados=" + _gradosarotar;
                            GLOpengl.Refresh();
                        }
                    break;*/
                    case 'm':
                        if(_figura!=null)
                        switch(_ptresult)
                        {
                            case 1:
                                {
                                    switch (_figura.TipoFigura)
                                    {
                                        case Figura.Tipo.Line:
                                            ((Linea)_figura).PI = e.Location;
                                            GLOpengl.Refresh();
                                            guardado = false;
                                            break;
                                        case Figura.Tipo.Pixel:
                                            ((Pixel)_figura).Location = e.Location;
                                            GLOpengl.Refresh();
                                            guardado = false;
                                            break;
                                        case Figura.Tipo.Circle:                                             
                                            ((Circulo)_figura).Origen = e.Location;
                                            GLOpengl.Refresh();
                                            guardado = false;
                                            break;
                                        case Figura.Tipo.Ellipse:
                                            ((Elipse)_figura).Origen = e.Location;
                                            GLOpengl.Refresh();
                                            guardado = false;
                                            break;
                                        case Figura.Tipo.CHermite:
                                        case Figura.Tipo.CBezier:
                                            ((Curva)_figura).PI = e.Location;
                                            GLOpengl.Refresh();
                                            guardado = false;
                                            break;
                                    }
                                }
                            break;
                            case 2:
                                switch (_figura.TipoFigura)
                                {
                                    case Figura.Tipo.Line:
                                        ((Linea)_figura).PF = e.Location;
                                        GLOpengl.Refresh();
                                        guardado = false;
                                        break;
                                    case Figura.Tipo.Circle:
                                        _p1 = ((Circulo)_figura).Origen;
                                        _radio = (int)Math.Sqrt(Math.Pow((_p2.X - _p1.X), 2) + Math.Pow((_p2.Y - _p1.Y), 2));
                                        ((Circulo)_figura).Radio = _radio;
                                        GLOpengl.Refresh();
                                        guardado = false;
                                    break;
                                    case Figura.Tipo.Ellipse:
                                        Point newrad = new Point();
                                        newrad.X=((Elipse)_figura).RadioA;
                                        newrad.Y=((Elipse)_figura).RadioB; 
                                        int incx = _p2.X - _p1.X, incy = _p2.Y - _p1.Y;
                                        if ((_p1.X > ((Elipse)_figura).Origen.X ))
                                            newrad.X +=  incx;
                                        else
                                            newrad.X -= incx;

                                        if ((_p1.Y > ((Elipse)_figura).Origen.Y))
                                            newrad.Y += incy;
                                        else
                                            newrad.Y -= incy;

                                        ((Elipse)_figura).Radio = newrad;
                                        GLOpengl.Refresh();
                                        _p1 = _p2;
                                        guardado = false;
                                        break;
                                    case Figura.Tipo.CHermite:
                                    case Figura.Tipo.CBezier:
                                        ((Curva)_figura).PF = e.Location;
                                        GLOpengl.Refresh();
                                        guardado = false;
                                        break;
                                }
                            break;
                            case 3:
                                switch (_figura.TipoFigura)
                                {
                                    case Figura.Tipo.Line:
                                        int incx = _p2.X - _p1.X, incy = _p2.Y - _p1.Y;
                                        ((Linea)_figura).PF = new Point(((Linea)_figura).PF.X+incx,((Linea)_figura).PF.Y+  incy);
                                        ((Linea)_figura).PI= new Point(((Linea)_figura).PI.X+incx,((Linea)_figura).PI.Y+  incy);
                                        _p1 = _p2;
                                        GLOpengl.Refresh();
                                        guardado = false;
                                    break;
                                    case Figura.Tipo.CHermite:
                                    case Figura.Tipo.CBezier:
                                    ((Curva)_figura).PCTRL1 = e.Location;
                                    GLOpengl.Refresh();
                                    guardado = false;
                                    break;
                                }
                            break;
                            case 4:
                            switch (_figura.TipoFigura)
                            {
                                case Figura.Tipo.CHermite:
                                case Figura.Tipo.CBezier:
                                    ((Curva)_figura).PCTRL2 = e.Location;
                                    GLOpengl.Refresh();
                                    guardado = false;
                                    break;
                            }
                            break;
                            case 5:
                            switch (_figura.TipoFigura)
                            {
                                case Figura.Tipo.CHermite:
                                case Figura.Tipo.CBezier:
                                    if (((Curva)_figura).PCTRL1.X == ((Curva)_figura).PI.X || ((Curva)_figura).PCTRL1.Y == ((Curva)_figura).PI.Y)
                                    {
                                        ((Curva)_figura).PCTRL1 = e.Location;
                                        _ptresult = 3;
                                        guardado = false;
                                    }
                                    else
                                        if (((Curva)_figura).PCTRL2.X == ((Curva)_figura).PF.X || ((Curva)_figura).PCTRL2.Y == ((Curva)_figura).PF.Y)
                                        {
                                            ((Curva)_figura).PCTRL2 = e.Location;
                                            _ptresult = 4;
                                            guardado = false;
                                        }
                                        else
                                        {
                                            int incx = _p2.X - _p1.X, incy = _p2.Y - _p1.Y;
                                            ((Curva)_figura).PF = new Point(((Curva)_figura).PF.X + incx, ((Curva)_figura).PF.Y + incy);
                                            ((Curva)_figura).PI = new Point(((Curva)_figura).PI.X + incx, ((Curva)_figura).PI.Y + incy);
                                            ((Curva)_figura).PCTRL1 = new Point(((Curva)_figura).PCTRL1.X + incx, ((Curva)_figura).PCTRL1.Y + incy);
                                            ((Curva)_figura).PCTRL2 = new Point(((Curva)_figura).PCTRL2.X + incx, ((Curva)_figura).PCTRL2.Y + incy);

                                            _p1 = _p2;

                                            guardado = false;
                                        }
                                        
                                    GLOpengl.Refresh();
                                    
                                break;
                            }
                            break;
                        }
                            
                    break;
                   
                }

               
            }
            else
            {
                switch (_op)
                {
                    case 'm':
                        foreach (Figura fi in _listaFiguras)
                        {
                            if (fi.PointInFig(e.Location, RANGO) !=0)
                            {
                                try
                                {
                                    this.Cursor = new Cursor(Editor_Graficación_2D.Properties.Resources.cursor_move.GetHicon());
                                    break;
                                }
                                catch { }
                            }
                            else
                                this.Cursor = _oldcursor;
                        }
                     break;
                    case 'r':
                     foreach (Figura fi in _listaFiguras)
                     {
                         if (fi.PointInFig(e.Location, RANGO) != 0)
                         {
                             try
                             {
                                 this.Cursor = Cursors.Cross;
                                 break;
                             }
                             catch { }
                         }
                         else
                             this.Cursor = _oldcursor;
                     }
                     break;
                    case 'z':
                     foreach (Figura fi in _listaFiguras)
                     {
                         if (fi.PointInFig(e.Location, RANGO) !=0)
                         {
                             try
                             {
                                 this.Cursor = new Cursor(Editor_Graficación_2D.Properties.Resources.cursor_changecolor.GetHicon());
                                 break;
                             }
                             catch { }
                         }
                         else
                             this.Cursor = _oldcursor;     
                     }
                     break;
                    case 'd':
                        if(_listaFiguras.Count==0)
                            this.Cursor = _oldcursor;
                        else
                     foreach (Figura fi in _listaFiguras)
                     {
                         if (fi.PointInFig(e.Location, RANGO) != 0)
                         {
                             try
                             {
                                 this.Cursor = new Cursor(Editor_Graficación_2D.Properties.Resources.cursor_delete.GetHicon());
                                 break;
                             }
                             catch { }
                         
                         }
                         else
                             this.Cursor = _oldcursor;
                     }
                     break;
                }
            }

        }
        /// <summary>
        /// Se produce cuando se levanta el boton de mouse en el objeto GL.
        /// Se utiliza para finalizar el pintado de una figura y agregarlo
        /// a la lista de figuras.
        /// </summary>
        private void GIOpengl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if (_op != 'm' && _op != 'z' && _op != 'd' && _op != '0' && _op != 'r')
                {
                    guardado = false;
                    _listaFiguras.Add(_figura);
                    _figura = null;
                }

           
            Status_fig.Text = "Figuras:   " + _listaFiguras.Count;
        }
        /// <summary>
        /// Se produce cuando el objeto GL necesita pintarse.
        /// Se inicializa el pintado OpenGL, se dibujan las figuras y
        /// se finaliza el pintado.
        /// </summary>
        private void GIOpengl_Paint(object sender, PaintEventArgs e)
        {
            //color de fondo para limpiar la pantalla
            
            Gl.glClearColor(GLOpengl.BackColor.R, GLOpengl.BackColor.G, GLOpengl.BackColor.B, 0);
            //Limpia la pantalla
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(GLOpengl.Left, GLOpengl.Right, GLOpengl.Bottom, GLOpengl.Top);
            //para evitar que se reescale la imagen de fondo.
            Gl.glViewport(0, 0, GLOpengl.Width, GLOpengl.Height);
            
                
            
            
            this.DibujaFiguras();       //dibujamos las figuras de la lista             
            if (_figura != null)
            {                         
                _figura.Dibuja();
            }




            if (_op == 'm' || _op == 'z' || _op == 'd' || _op == 'r')
            {
                Gl.glBegin(Gl.GL_LINES); //se inicia el pintado        

                this.DibujaOrigenes();

                Gl.glEnd();
                foreach (Figura figur in _listaFiguras)
                    if (figur.TipoFigura == Figura.Tipo.CBezier || figur.TipoFigura == Figura.Tipo.CHermite)
                        ((Curva)figur).DibujaControles();

            }
        }
        //*****************************************************************

        //*****************************************************************
        //Operaciones con los botones de figura y paletas de colores
        /// <summary>
        /// Se produce cuando se da click en el PictureBox de color primario.
        /// El color primario se establece igual que al del PictureBox.
        /// </summary>
        private void PB_color1_Click(object sender, EventArgs e)
        {
            PB_color1.BackColor = GetColor();
            _pixelcolor = PB_color1.BackColor;

            TB_R.Text = "" + _pixelcolor.R;
            TB_G.Text = "" + _pixelcolor.G;
            TB_B.Text = "" + _pixelcolor.B;
        }
        /// <summary>
        /// Se produce cuando se da click en el PictureBox de color secundario.
        /// El color secundario se establece igual que al del PictureBox.
        /// </summary>
        private void PB_color2_Click(object sender, EventArgs e)
        {
            PB_color2.BackColor = GetColor();
            _pixelcolor = PB_color1.BackColor;
        }
        /// <summary>
        /// Se produce cuando se da click en el PictureBox de colores por default.
        /// Se establece el color primario como negro y el secundario como blanco.
        /// </summary>
        private void PB_colordefault_Click(object sender, EventArgs e)
        {
            PB_color1.BackColor = Color.Black;
            PB_color2.BackColor = Color.White;
            _pixelcolor = PB_color1.BackColor;

            TB_R.Text = "" + _pixelcolor.R;
            TB_G.Text = "" + _pixelcolor.G;
            TB_B.Text = "" + _pixelcolor.B;
        }
        /// <summary>
        /// Se produce cuando se da click en el PictureBox de invertir color.
        /// invierta el color primario por el secundario y viceverza.
        /// </summary>
        private void PB_invertircolor_Click(object sender, EventArgs e)
        {
            Color auxcolor = PB_color1.BackColor;
            PB_color1.BackColor = PB_color2.BackColor;
            PB_color2.BackColor = auxcolor;
            _pixelcolor = PB_color1.BackColor;

            TB_R.Text = "" + _pixelcolor.R;
            TB_G.Text = "" + _pixelcolor.G;
            TB_B.Text = "" + _pixelcolor.B;
        }
        /// <summary>
        /// Se produce cuando se mueve el raton en el PictureBox de la paleta lateral.
        /// si se da click izquierdo en el, se establece el color primario con el color
        /// de las coordenadas X y Y, si se da click derecho, el color secundario
        /// es establecido.
        /// </summary>
        private void PB_paletalateral_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {            
                Color pixel = GetPixelPictureBox(PB_paletalateral, e.Location);
                PB_color1.BackColor = pixel;
                _pixelcolor = pixel;

                TB_R.Text = "" + _pixelcolor.R;
                TB_G.Text = "" + _pixelcolor.G;
                TB_B.Text = "" + _pixelcolor.B;
            }
            else
                if (e.Button == MouseButtons.Right)
                {
                    Color pixel = GetPixelPictureBox(PB_paletalateral, e.Location);
                    PB_color2.BackColor = pixel;
                }
        }
        /// <summary>
        /// Regresa el color de pixel seleccionado por el punto Location
        /// en el PictureBox PB.
        /// </summary>
        /// /// <param name="PB">PictureBox del cual se extrae el Color.</param>
        /// /// <param name="Location">Punto del cual se extraere el color del Pixel.</param>
        private Color GetPixelPictureBox(PictureBox PB, Point Location)
        {
            Bitmap bm = (Bitmap)PB.Image;
            Color pixel = _pixelcolor;
            try
            {
                pixel= bm.GetPixel(Location.X, Location.Y);
            }
            catch
            { 
            }
            return pixel;
        }
        /// <summary>
        /// Se produce cuando se da click en el PictureBox de la paleta lateral.
        /// Obtiene el color seleccionado por las coordenadas del mouse en la 
        /// paleta lateral.
        /// </summary>
        private void PB_paletalateral_MouseDown(object sender, MouseEventArgs e)
        {
            this.PB_paletalateral_MouseMove(sender, e);
        }
        /// <summary>
        /// Se produce cuando se mueve el raton en el PictureBox de la paleta principal.
        /// si se da click izquierdo en el, se establece el color primario con el color
        /// de las coordenadas X y Y, si se da click derecho, el color secundario
        /// es establecido.
        /// </summary>
        private void PB_paleta_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Color pixel = GetPixelPictureBox(PB_paleta, e.Location);
                PB_color1.BackColor = pixel;
                _pixelcolor = pixel;
                TB_R.Text = "" + _pixelcolor.R;
                TB_G.Text = "" + _pixelcolor.G;
                TB_B.Text = "" + _pixelcolor.B;
            }
            else
                if (e.Button == MouseButtons.Right)
                {
                    Color pixel = GetPixelPictureBox(PB_paleta, e.Location);
                    PB_color2.BackColor = pixel;
                }


        }
        /// <summary>
        /// Se produce cuando se da click en el PictureBox de la paleta.
        /// </summary>
        private void PB_paleta_MouseDown(object sender, MouseEventArgs e)
        {
            this.PB_paleta_MouseMove(sender, e);
        }
        /// <summary>
        /// Se produce cuando se da click en el boton de direccion izquierda
        /// de la paleta, y cambia la paleta actual por otra establecida en
        /// la lista de paletas.
        /// </summary>
        private void BT_paletaleft_Click(object sender, EventArgs e)
        {
            if (_indicepaleta == 0)
                _indicepaleta = _listapaletas.Count - 1;
            else
                _indicepaleta--;

            PB_paleta.Image = _listapaletas[_indicepaleta];
            PB_paleta.Refresh();
        }
        /// <summary>
        /// Se produce cuando se da click en el boton de direccion derecha
        /// de la paleta, y cambia la paleta actual por otra establecida en
        /// la lista de paletas.
        /// </summary>
        private void BT_paletaright_Click(object sender, EventArgs e)
        {
            if (_indicepaleta == _listapaletas.Count - 1)
                _indicepaleta = 0;
            else
                _indicepaleta++;

            PB_paleta.Image = _listapaletas[_indicepaleta];
            PB_paleta.Refresh();
            
        }
       
        /// <summary>
        /// Se produce cuando el mouse esta sobre el boton de seleccion de pixel.
        /// Se cambia la imagen para dar un efecto de seleccion.
        /// </summary>
        private void BT_pixel_MouseHover(object sender, EventArgs e)
        {
            BT_pixel.Image = Editor_Graficación_2D.Properties.Resources.pixel_select;
        }
        /// <summary>
        /// Se produce cuando el mouse ya no esta sobre el boton de seleccion de pixel.
        /// Reestablece la imagen original.
        /// </summary>
        private void BT_pixel_MouseLeave(object sender, EventArgs e)
        {
            if(_op!='p')
            BT_pixel.Image = Editor_Graficación_2D.Properties.Resources.pixel_unselect;
        }
        /// <summary>
        /// Se produce cuando el mouse esta sobre el boton de seleccion de Linea.
        /// Se cambia la imagen para dar un efecto de selección.
        /// </summary>
        private void BT_line_MouseHover(object sender, EventArgs e)
        {
            BT_line.Image = Editor_Graficación_2D.Properties.Resources.line_select;
        }
        /// <summary>
        /// Se produce cuando el mouse ya no esta sobre el boton de seleccion de Linea.
        /// Reestablece la imagen original.
        /// </summary>
        private void BT_line_MouseLeave(object sender, EventArgs e)
        {
            if (_op != 'l')
            BT_line.Image = Editor_Graficación_2D.Properties.Resources.line_unselect;
        }
        /// <summary>
        /// Se produce cuando el mouse esta sobre el boton de seleccion de Mover.
        /// Se cambia la imagen para dar un efecto de selección.
        /// </summary>
        private void BT_move_MouseHover(object sender, EventArgs e)
        {
            BT_move.Image = Editor_Graficación_2D.Properties.Resources.hand_select;
        }
        /// <summary>
        /// Se produce cuando el mouse ya no esta sobre el boton de seleccion de Mover.
        /// Reestablece la imagen original.
        /// </summary>
        private void BT_move_MouseLeave(object sender, EventArgs e)
        {
            if (_op != 'm')
                BT_move.Image = Editor_Graficación_2D.Properties.Resources.hand_unselect;
        }
        /// <summary>
        /// Se produce cuando el mouse esta sobre el boton de seleccion de Circulo.
        /// Se cambia la imagen para dar un efecto de selección.
        /// </summary>
        private void BT_circle_MouseHover(object sender, EventArgs e)
        {
            BT_circle.Image = Editor_Graficación_2D.Properties.Resources.circle_select;
        }
        /// <summary>
        /// Se produce cuando el mouse ya no esta sobre el boton de seleccion de Circulo.
        /// Reestablece la imagen original.
        /// </summary>
        private void BT_circle_MouseLeave(object sender, EventArgs e)
        {
            if (_op != 'c')
                BT_circle.Image = Editor_Graficación_2D.Properties.Resources.circle_unselect;
        }
        /// <summary>
        /// Se produce cuando el mouse esta sobre el boton de seleccion de Cambiar Color.
        /// Se cambia la imagen para dar un efecto de selección.
        /// </summary>
        private void BT_cambiacolor_MouseHover(object sender, EventArgs e)
        {
            BT_cambiacolor.Image = Editor_Graficación_2D.Properties.Resources.changecolor_select;
        }
        /// <summary>
        /// Se produce cuando el mouse ya no esta sobre el boton de seleccion de Cambiar Color.
        /// Reestablece la imagen original.
        /// </summary> 
        private void BT_cambiacolor_MouseLeave(object sender, EventArgs e)
        {
            if(_op!='z')
            BT_cambiacolor.Image = Editor_Graficación_2D.Properties.Resources.changecolor_unselect;
        }
        /// <summary>
        /// Se produce cuando el mouse esta sobre el boton de seleccion de Elipse.
        /// Se cambia la imagen para dar un efecto de selección.
        /// </summary>
        private void BT_elipse_MouseHover(object sender, EventArgs e)
        {
            BT_elipse.Image = Editor_Graficación_2D.Properties.Resources.ellipse_select;
        }
        /// <summary>
        /// Se produce cuando el mouse ya no esta sobre el boton de seleccion de Elipse.
        /// Reestablece la imagen original.
        /// </summary>
        private void BT_elipse_MouseLeave(object sender, EventArgs e)
        {
            if (_op != 'e')
                BT_elipse.Image = Editor_Graficación_2D.Properties.Resources.ellipse_unselect;
        }

        /// <summary>
        /// Se produce cuando el mouse esta sobre el boton de seleccion de Curva de Hermite
        /// Se cambia la imagen para dar un efecto de selección.
        /// </summary>
        private void BT_hermite_MouseHover(object sender, EventArgs e)
        {
            BT_hermite.Image = Editor_Graficación_2D.Properties.Resources.curve_select;
        }
        /// <summary>
        /// Se produce cuando el mouse ya no esta sobre el boton de seleccion de Curva de Hermite.
        /// Reestablece la imagen original.
        /// </summary>
        private void BT_hermite_MouseLeave(object sender, EventArgs e)
        {
            if (_op != 'h')
                BT_hermite.Image = Editor_Graficación_2D.Properties.Resources.curve_unselect;
        }
        /// <summary>
        /// Se produce cuando el mouse esta sobre el boton de seleccion de Curva de Bezier.
        /// Se cambia la imagen para dar un efecto de selección.
        /// </summary>
        private void BT_bezier_MouseHover(object sender, EventArgs e)
        {
            BT_bezier.Image = Editor_Graficación_2D.Properties.Resources.bezier_select;
        }
        /// <summary>
        /// Se produce cuando el mouse ya no esta sobre el boton de seleccion de Curva de Bezier.
        /// Reestablece la imagen original.
        /// </summary>
        private void BT_bezier_MouseLeave(object sender, EventArgs e)
        {
            if (_op != 'b')
                BT_bezier.Image = Editor_Graficación_2D.Properties.Resources.bezier_unselect;
        }
        /// <summary>
        /// Se produce cuando el mouse esta sobre el boton de seleccion de Eliminar Figura.
        /// Se cambia la imagen para dar un efecto de selección.
        /// </summary>
        private void BT_delete_MouseHover(object sender, EventArgs e)
        {
            BT_delete.Image = Editor_Graficación_2D.Properties.Resources.delete_select;
        }
        /// <summary>
        /// Se produce cuando el mouse ya no esta sobre el boton de seleccion de Eliminar Figura.
        /// Reestablece la imagen original.
        /// </summary>
        private void BT_delete_MouseLeave(object sender, EventArgs e)
        {
            if (_op != 'd')
                BT_delete.Image = Editor_Graficación_2D.Properties.Resources.delete_unselect;
        }
        /// <summary>
        /// Se selecciona la opcion para dibujar un Pixel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_pixel_Click(object sender, EventArgs e)
        {
            pixelToolStripMenuItem_Click(null, null);
        }
        /// <summary>
        /// Se selecciona la opcion para dibujar una Linea.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_line_Click(object sender, EventArgs e)
        {
            lineaToolStripMenuItem_Click(null, null);
        }
        /// <summary>
        /// Se selecciona la opcion para mover figuras.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_move_Click(object sender, EventArgs e)
        {
            moverToolStripMenuItem_Click(null, null);
        }
        /// <summary>
        /// Se selecciona la opcion para dibujar un circulo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_circle_Click(object sender, EventArgs e)
        {
            circuloToolStripMenuItem_Click(null, null);
        }
        /// <summary>
        /// Se selecciona la opcion para dibujar una elipse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_elipse_Click(object sender, EventArgs e)
        {
            elipseToolStripMenuItem_Click(null, null);
        }
        /// <summary>
        /// Se selecciona la opcion para cambiar el color de las figuras.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_cambiacolor_Click(object sender, EventArgs e)
        {
            cambiarColorToolStripMenuItem_Click(null,null);
        }

        /// <summary>
        /// Se selecciona la opcion para dibujar una curva de hermite.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_hermite_Click(object sender, EventArgs e)
        {
            curvaDeHermiteToolStripMenuItem_Click(null, null);
        }
        /// <summary>
        /// Se selecciona la opcion para eliminar una figura.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_delete_Click(object sender, EventArgs e)
        {
            borrarFiguraToolStripMenuItem_Click(null, null);
        }
        /// <summary>
        /// Se selecciona la opcion para dibujar una curva de bezier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_bezier_Click(object sender, EventArgs e)
        {
            this.curvaDeBezierToolStripMenuItem_Click(null, null);
        }

        /// <summary>
        /// Se produce cuando cambia el valor del ancho de linea,
        /// guardando el nuevo valor.
        /// </summary>
        private void NUD_grosor_ValueChanged(object sender, EventArgs e)
        {
            _ancholinea = (int)NUD_grosor.Value;
        }

        /// <summary>
        /// Se produce al salir de la aplicacion.
        /// Si no se ha guardado el dibujo, pregunta que si se desea guardar
        /// antes de salir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resu = DialogResult.No;
            if (!guardado)
            {
                 resu = MessageBox.Show("No ha guardado las figuras, \n ¿desea guardarlas?", "Advertencia", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Information
                                        ,MessageBoxDefaultButton.Button1);
                if (resu == DialogResult.Yes)
                {
                    guardarToolStripMenuItem_Click(null, null);
                }
            }

            if (resu != DialogResult.Cancel)
                this.Dispose();
            
        }

        /// <summary>
        /// Se produce cuando cambia el valor del estilo de linea,
        /// guardando el nuevo valor.
        /// </summary>
        private void NUD_estilo_ValueChanged(object sender, EventArgs e)
        {
            _estilolinea = (int)NUD_estilo.Value;
        }

        /// <summary>
        /// Se selecciona la opcion para rotar las figuras.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_rotar_Click(object sender, EventArgs e)
        {
            rotarToolStripMenuItem_Click(null,null);
        }

        /// <summary>
        /// Selecciona la opcion para rotar las figuras.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rotarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _op = 'r';
            _gradosarotar = 0;
            ActivaOpcionMenu();
            GLOpengl.Refresh();
            Statusgrados.Visible = true;
            Statusgrados.Text = "Grados=" + _gradosarotar;
        }

        /// <summary>
        /// Restaura la paleta de colores principal (Regresa a la primera paleta).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_paletares_Click(object sender, EventArgs e)
        {
            _indicepaleta = 0;
            PB_paleta.Image = _listapaletas[_indicepaleta];
            PB_paleta.Refresh();
        }

        /// <summary>
        /// Se produce cuando el mouse esta sobre el boton de seleccion de Rotar Figura.
        /// Se cambia la imagen para dar un efecto de selección.
        /// </summary>
        private void BT_rotar_MouseHover(object sender, EventArgs e)
        {
            BT_rotar.Image = Editor_Graficación_2D.Properties.Resources.rotar_select;
        }
        /// <summary>
        /// Se produce cuando el mouse ya no esta sobre el boton de seleccion de Rotar Figura.
        /// Reestablece la imagen original.
        /// </summary>
        private void BT_rotar_MouseLeave(object sender, EventArgs e)
        {
            if (_op != 'r')
               BT_rotar.Image = Editor_Graficación_2D.Properties.Resources.rotar_unselect;
        }

        /// <summary>
        /// Guarda el dibujo cuando se seleccione la opcion "Guardar" en el
        /// menú.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardardlg= new SaveFileDialog();

            guardardlg.AddExtension = true;
            guardardlg.DefaultExt = ".g2d";
            guardardlg.Filter = "Archivos Graficacion 2D (*.g2d)|*.g2d";

            if (guardardlg.ShowDialog() == DialogResult.OK)
            {
                nomarch =guardardlg.FileName;
                FileStream stream = new FileStream(nomarch, FileMode.Create);
                BinaryFormatter formato = new BinaryFormatter();
                formato.Serialize(stream, _listaFiguras);
                stream.Close();
                guardado = true;
                _figura = null;
                _op = '0';
                this.ActivaOpcionMenu();
                guardado = true;
                this.Text = "Graficacion 2D - " + nomarch;
                GLOpengl.Refresh();
            }
            guardardlg.Dispose();
        }

        /// <summary>
        /// Abre un dibujo con extencion *.g2d y lo dibuja en pantalla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrirdlg = new OpenFileDialog();

            abrirdlg.AddExtension = true;
            abrirdlg.DefaultExt = ".g2d";
            abrirdlg.Filter = "Archivos Graficacion 2D (*.g2d)|*.g2d";
            
            DialogResult resu=DialogResult.No;
            if (!guardado)
            {
                resu = MessageBox.Show("No ha guardado el dibujo, \n ¿Desea Guardarlo?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information
                                       , MessageBoxDefaultButton.Button1); if (resu == DialogResult.Yes)
                {
                    guardarToolStripMenuItem_Click(null, null);
                }
            }
                
            if (resu == DialogResult.No)
                if (abrirdlg.ShowDialog() == DialogResult.OK)
                {
                    _listaFiguras.Clear();
                    nomarch = abrirdlg.SafeFileName;                    
                    FileStream stream = new FileStream(abrirdlg.FileName, FileMode.Open);
                    BinaryFormatter formato = new BinaryFormatter();
                    _listaFiguras = (List<Figura>)formato.Deserialize(stream);
                    stream.Close();
                    this.Text = "Graficacion 2D - " + nomarch;
                    _figura = null;
                    _op = '0';
                    this.ActivaOpcionMenu();
                    guardado = true;
                    GLOpengl.Refresh();
                    
                    
                }

            abrirdlg.Dispose();
        }

        /// <summary>
        /// Verifica si se ha guardado el dibujo antes de cerrar el programa,
        /// y da al usuario las opciones para guardar el dibujo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!guardado)
            {
                DialogResult resu = MessageBox.Show("No ha guardado el dibujo, \n ¿Desea Guardarlo?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information
                                                        , MessageBoxDefaultButton.Button1); 
                if (resu == DialogResult.Yes)
                {
                    guardarToolStripMenuItem_Click(null, null);
                }
                else
                    if (resu == DialogResult.No)
                        e.Cancel = false;
                    else
                        e.Cancel = true;
            }
        }

        /// <summary>
        /// Muestra el cuadro de dialogo con información del programa
        /// al presionarse la opcion "Acerca de..." en el menu "?".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About acercade = new About();

            acercade.ShowDialog();
            acercade.Dispose();
        }
        
        //*****************************************************************
    }
}