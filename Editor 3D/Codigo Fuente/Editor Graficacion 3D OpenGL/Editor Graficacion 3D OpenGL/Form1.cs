using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;

namespace Editor_Graficacion_3D_OpenGL
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Variable de la clase OBJ, que manipula archivos *.obj.
        /// Extrae sus vertices y poligonos.
        /// </summary>
        private OBJ obj;
        /// <summary>
        /// Lista que guarda los puntos de una figura para ser
        /// dibujados en pantalla.
        /// </summary>
        private List< Point> _listP;
        /// <summary>
        /// Variable que guarda el estado del archivo *.obj.
        /// vale TRUE cuando un archivo ha sido abierto, y FALSE
        /// cuando no se ha habierto ningun archivo *.obj.
        /// </summary>
        private Boolean _open;
        /// <summary>
        /// Variable de tipo Point para guardar temporalmente las coordenadas del mouse.
        /// </summary>
        private Point paux;

        
        private double _zp;
        private double _q;
        private double _dx;
        private double _dy;
        private double _dz;

        /// <summary>
        /// Guarda la coordenada en X para calcular los
        /// puntos de dispositivo del objeto 3D.
        /// </summary>
        private int _x;
        /// <summary>
        /// Guarda la coordenada en Y para calcular los
        /// puntos de dispositivo del objeto 3D.
        /// </summary>
        private int  _y;
        /// <summary>
        /// Variable que se usa para visualizar un cuadro de dialogo
        /// que muestra las opciones y parametros que modifican la
        /// visualizacion del objeto 3D.
        /// </summary>
        private Opciones dlgop;

        /// <summary>
        /// Variable para guardar el sentido de giro del objeto.
        /// </summary>
        private int _giro;

        /// <summary>
        /// Variable para crear un cuado de dialogo para girar la
        /// figura automaticamente.
        /// </summary>
        private Giros dlgiros;
        /// <summary>
        /// Variable que guarda el modo de dibujado
        /// TRUE - Dibuja la figura con lineas.
        /// FALSE - Dibuja la figura con relleno.
        /// </summary>
        private Boolean _lineas;
        /// <summary>
        /// Guarda el modo de visualizacion.
        /// TRUE - Visualiza todos los Poligonos del objeto.
        /// FALSE - Visualiza solo los Poligonos visibles del Objerto.
        /// </summary>
        private Boolean _caras;


        /// <summary>
        /// Constructor del formulario.
        /// Se inicializa la instancia de OpenGL.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            OpenGL.InitializeContexts();

        }

        /// <summary>
        /// Inicializa todas las variables cuando se crea el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            obj = new OBJ();
            dlgop = new Opciones(this);
            _listP = new List<Point>();
            dlgiros = new Giros();
            _open = false;
            this._zp = dlgop.ZP;
            this._q = dlgop.Q;
            this._dx = dlgop.DX;
            this._dy = dlgop.DY;
            this._dz = dlgop.DZ;
            this._x = dlgop.X;
            this._y = dlgop.Y;
            this._lineas = dlgop.Dbujado;
            this._caras = dlgop.Visualizacion;
            this.Form1_SizeChanged(null,null);
        }

        /// <summary>
        /// Metodo para pintar sobre OpenGL, aqui se especifica
        /// las propiedades de dibujado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenGL_Paint(object sender, PaintEventArgs e)
        {
            //color de fondo para limpiar la pantalla

            Gl.glClearColor(OpenGL.BackColor.R, OpenGL.BackColor.G, OpenGL.BackColor.B, 0);
            //Limpia la pantalla
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            //Gl.glOrtho(-OpenGL.Width / 2, OpenGL.Width / 2, -OpenGL.Height / 2, OpenGL.Height / 2, -1, 1);

            Glu.gluOrtho2D(OpenGL.Left, OpenGL.Right, OpenGL.Bottom, OpenGL.Top);
            //para evitar que se reescale la imagen de fondo.
            Gl.glViewport(-OpenGL.Width / 2, 0, OpenGL.Width + OpenGL.Width / 2, OpenGL.Height  + OpenGL.Height / 2);

           
            foreach (Poligono p in obj.Poligonos)
                DibujaPoligono(p);
            
        }

        /// <summary>
        /// Dibuja el Poligono 'p' en la pantalla con sus vertices correspondientes.
        /// </summary>
        /// <param name="p">Poligono que se desea dibujar.</param>
        private void DibujaPoligono(Poligono p)
        {

            Color _color = Color.Green;           


            Gl.glColor4ub(_color.R, _color.G, _color.B, _color.A);
            if (!_lineas)
                Gl.glBegin(Gl.GL_TRIANGLES);
            else
                Gl.glBegin(Gl.GL_LINE_LOOP); 
                

                if (_caras || PoligonoIsVisible(p))
                {
                     foreach (Vertice v in p.Vertices)
                         Gl.glVertex2f(_listP[v.Numero - 1].X, _listP[v.Numero - 1].Y);
                }
            


            Gl.glEnd();               //se finaliza el pintado  
           
        }

        /// <summary>
        /// Verifica si el Poligono 'p' se debe de dibujar o no.
        /// El método checa si el poligono se encuentra en la parte
        /// frontal del objeto, o en la parte trasera.
        /// </summary>
        /// <param name="p">Poligo al que se verifica su visibilidad.</param>
        /// <returns>Regresa TRUE si es visible, de lo contrario regresa FALSE.</returns>
        private Boolean PoligonoIsVisible(Poligono p)
        {
            Boolean ret = false;
            Vertice v1, v2,vnorm,vcop,vn;

            v1 = new Vertice(p.Vertices[1].X - p.Vertices[0].X, p.Vertices[1].Y - p.Vertices[0].Y, p.Vertices[1].Z - p.Vertices[0].Z, 0);
            v2 = new Vertice(p.Vertices[2].X - p.Vertices[0].X, p.Vertices[2].Y - p.Vertices[0].Y, p.Vertices[2].Z - p.Vertices[0].Z, 0);
            vnorm = new Vertice((v1.Y * v2.Z - v2.Y * v1.Z), -(v1.X * v2.Z - v2.X * v1.Z), (v1.X * v2.Y - v2.X * v1.Y), 0);
            vcop = new Vertice(_q * _dx, _q * _dy, _q * _dz, 0);
            vn = p.Vertices[1];
            vcop.X = vn.X - vcop.X;
            vcop.Y = vn.Y - vcop.Y;
            vcop.Z = vn.Z - vcop.Z;

            if (((vcop.X * vnorm.X) + (vcop.Y * vnorm.Y) + (vcop.Z * vnorm.Z)) <= 0)
                ret = true;

            return ret;
        }

        /// <summary>
        /// Metodo para abrir un archivo *.obj y visualizarlo en pantalla junto
        /// con su informacion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgopen = new OpenFileDialog();
            _open = false;

            if (dlgopen.ShowDialog() == DialogResult.OK)
            {
                if (obj.Abrir(dlgopen.FileName))
                {
                    _open = true;
                    TB_original.Text = " ******ARCHIVO ORIGINAL******\r\n\r\n\n"+ obj.Texto;
                    TB_poligonos.Text =" **********POLIGONOS*********\r\n#"+obj.Poligonos.Count+"\r\n\r\n\n" + obj.TextoPoligonos;
                    TB_vertices.Text = " **********VERTICES**********\r\n#"+obj.Vertices.Count+"\r\n\r\n\n" + obj.TextoVertices;
                    this.dibujaFiguraToolStripMenuItem_Click(null,null);
                    
                }
            }
        }    

        /// <summary>
        /// Este evento se produce cuando cambia el tamaño de la ventana.
        /// En el se modifican las dimensiones de los TextBox que contienen
        /// la informacion del archivo *obj para adaptarlas al nuevo tamaño
        /// de la ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int ancho = (this.Width / 3) - 30;
            TB_original.Width = ancho;
            TB_original.Location = new Point(10, TB_original.Location.Y);
            TB_poligonos.Width = ancho;
            TB_poligonos.Location = new Point(TB_original.Location.X + 10 + ancho, TB_poligonos.Location.Y);
            TB_vertices.Width = ancho;
            TB_vertices.Location = new Point(TB_poligonos.Location.X + 10 + ancho, TB_poligonos.Location.Y);
 
        }

        /// <summary>
        /// Este evento se dispara cuando se da click en el boton salir del menu Archivo,
        /// y cierra la aplicacion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Calcula las coordenadas del Vertice 'v', en coordenadas de 
        /// dispositivo.
        /// </summary>
        /// <param name="v">Vertice del cual se calculan las coordenadas de dispositivo.</param>
        /// <param name="ZP"> Parametro ZP</param>
        /// <param name="Q"> Parametro Q</param>
        /// <param name="DX"> Parametro DX</param>
        /// <param name="DY"> Parametro DY</param>
        /// <param name="DZ"> Parametro DZ</param>
        /// <param name="resX"> Resolucion del dispositivo en X</param>
        /// <param name="resY"> Resolucion del dispositivo en X</param>
        /// <returns>Regresesa un Punto con las coordenadas X y Y de dispositivo.</returns>
        private Point CalculaPuntoPantalla(Vertice v, double ZP, double Q, double DX, double DY, double DZ,int resX, int resY)
        {
            Point p = new Point();
            double x, y, z, w, xp, yp;

            x = (v.X) - ((DX * v.Z) / DZ) + ((ZP * DX) / DZ);
            y = v.Y - ((v.Z * DX) / DZ) + (ZP * (DY / DZ));
            z = ((-v.Z * ZP) / (Q * DZ)) + ((ZP * ZP) / (Q * DZ) + ZP);
            w = (-v.Z / (Q * DZ)) + (ZP / (Q * DZ) + 1);

            if (w != 1)
            {
                x /= w;
                y /= w;
                z /= w;
                w /= w;
            }

            xp = ((x - (z * (DX / DZ))) + (ZP * (DX / DZ))) / (((ZP - z) / (Q * DZ) )+ 1);
            yp = ((y - (z * (DY / DZ))) + (ZP * (DY / DZ))) / (((ZP - z) / (Q * DZ)) + 1);

            p.X = Math.Abs((int)(xp + (resX / z)));
            p.Y = Math.Abs((int)((resY / z) - yp));
            


            return p;
        }

        /// <summary>
        /// Calcula los puntos en pantalla y dibuja una figura abierta por la clase
        /// OBJ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dibujaFiguraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (_open)
            {
                
                _listP.Clear();
                foreach (Vertice v in obj.Vertices)
                {
                    Point p ;
                    p = CalculaPuntoPantalla(v, _zp, _q, _dx, _dy, _dz, _x, _y);
                    _listP.Add(p);
                }

                OpenGL.Refresh();
            }
        }

        /// <summary>
        /// Escala el objeto 3D actual.
        /// </summary>
        /// <param name="masmenos">True si se desea ampliar el objeto, False si se desea reducirlo.</param>
        /// <param name="x">Incremento en X</param>
        /// <param name="y">Incremento en Y</param>
        /// <param name="z">Incremento en Z</param>
        private void Escala(Boolean masmenos, double x, double y, double z)
        {
            for (int i = 0; i < obj.Vertices.Count; i++)
            { 
                if(masmenos)
                {
                    obj.Vertices[i].X = obj.Vertices[i].X * x;
                    obj.Vertices[i].Y = obj.Vertices[i].Y * y;
                    obj.Vertices[i].Z = obj.Vertices[i].Z * z;
                }
                else
                {
                    obj.Vertices[i].X = obj.Vertices[i].X / x;
                    obj.Vertices[i].Y = obj.Vertices[i].Y / y;
                    obj.Vertices[i].Z = obj.Vertices[i].Z / z;
                }
            }
        }
        /// <summary>
        /// Traslada el objeto 3D actual.
        /// </summary>
        /// <param name="x">Desplazamiento en X</param>
        /// <param name="y">Desplazamiento en Y</param>
        /// <param name="z">Desplazamiento en Z</param>
        private void Traslada(double x, double y, double z)
        {
            for (int i = 0; i < obj.Vertices.Count; i++)
            {
                    obj.Vertices[i].X = obj.Vertices[i].X - x;
                    obj.Vertices[i].Y = obj.Vertices[i].Y - y;
               
            }
        }
        /// <summary>
        /// Rota el objeto 3D actual.
        /// </summary>
        /// <param name="grados">Especifica cuantos grados se va a rotar el objeto.</param>
        /// <param name="eje">Especifica el eje en que se va a rotar el objeto. 1 - Rota la figura en el eje X, 2 - Rota la figura en el eje Y y 3 - Rota la figura en el eje Z</param>
        private void Rota(double grados, int eje)
        {
            double g = (grados * Math.PI) / 180;
            double gaux=Math.Atan(56);
            double x, y, z;


            for (int i = 0; i < obj.Vertices.Count; i++)
            {
                x = obj.Vertices[i].X;
                y = obj.Vertices[i].Y;
                z = obj.Vertices[i].Z;

                switch (eje)
                {
                    case 1:
                        obj.Vertices[i].Y = (y * Math.Cos(g)) - (z * Math.Sin(g));
                        obj.Vertices[i].Z = (y * Math.Sin(g)) + (z * Math.Cos(g));

                        break;
                    case 2:
                        obj.Vertices[i].X = (x * Math.Cos(g)) + (z * Math.Sin(g));
                        obj.Vertices[i].Z = (x * -Math.Sin(g)) + (z * Math.Cos(g));
                        break;
                    case 3:
                        obj.Vertices[i].X = (x * Math.Cos(g)) - (y * Math.Sin(g));
                        obj.Vertices[i].Y = (x * Math.Sin(g)) + (y * Math.Cos(g));
                        break;

                }
            }
        }

        /// <summary>
        /// Este evento se ejecuta cuando se mueve la rueda del mouse hacia adelante o hacia atras,
        /// y sirve para escalar el obejto 3D actual, segun se gire la rueda, la figura se amplia o
        /// se reduce.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenGL_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
                Escala(false, 1.1, 1.1, 1.1);
            else
                Escala(true, 1.1, 1.1, 1.1);

            this.dibujaFiguraToolStripMenuItem_Click(null, null);
        }

        /// <summary>
        /// Este evento se pruduce cuando se da click en la pantalla,
        /// y funciona para guardar la posicion del mouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenGL_MouseDown(object sender, MouseEventArgs e)
        {
            paux = e.Location;
            this.dibujaFiguraToolStripMenuItem_Click(null, null);
        }

        /// <summary>
        /// Este evento se produce cuando se mueve el raton del mouse por la pantalla.
        /// Si se mantiene presionado el boton izquiero o el boton central y se mueve el mouse, la figura
        /// se Rota.
        /// Si se mantiene presionado el boton derecho y se mueve el mouse, la figura se
        /// Traslada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenGL_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.None)
            {
                bool movx = false, movy = false;
                int deltax, deltay;
                deltax = e.X - paux.X;
                deltay = e.Y - paux.Y;
                if (_open && e.Button == MouseButtons.Left)
                {
                    if (Math.Abs(deltay) > Math.Abs(deltax))
                        movx = true;

                    if (Math.Abs(deltax) > Math.Abs(deltay))
                        movy = true;

                    if (movx)
                    {
                        if (deltay > 0)
                            Rota(2, 1);
                        else
                            Rota(-2, 1);
                    }
                    if (movy)
                    {
                        if (deltax > 0)
                            Rota(2, 2);
                        else
                            Rota(-2, 2);
                    }

                }
                else
                    if (_open && e.Button == MouseButtons.Right)
                    {
                        Traslada((paux.X - e.X) - .5, (e.Y - paux.Y) - .5, 0);
                    }
                    else
                        if (_open && e.Button == MouseButtons.Middle)
                        {

                            if (deltax < 0)
                                Rota(2, 3);
                            else
                                Rota(-2, 3);

                        }
                this.dibujaFiguraToolStripMenuItem_Click(null, null);

                paux = e.Location;
            }
        }       

        /// <summary>
        /// Este evento se produce cuando se suelta algun boton del mouse, y sirve
        /// para mostrar la lista actualizada de vertices del objeto actual.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenGL_MouseUp(object sender, MouseEventArgs e)
        {
            TB_vertices.Text = " **********VERTICES**********\r\n#" + obj.Vertices.Count + "\r\n\r\n\n" + obj.TextoVertices;
        }


        /// <summary>
        /// Este evento se dispara cada que se cumple un tiempo determinado del timer1.
        /// Sirve para rotar la figura automaticamente, lee la informacion para rotar
        /// del cuadro de dialogo Rotar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            _giro = dlgiros.GIRO;
            timer.Interval = (100-dlgiros.Velocidad)+1;
            switch (_giro)
            {
                case 1: Rota(1, 1); break;
                case 2: Rota(1, 2); break;
                case 3: Rota(1, 3); break;
                case -1: Rota(-1, 1); break;
                case -2: Rota(-1, 2); break;
                case -3: Rota(-1, 3); break;
            }
            this.dibujaFiguraToolStripMenuItem_Click(null, null);
            if (dlgiros.IsDisposed)
            {
                timer.Stop();
                dlgiros = new Giros();
                
            }
        }

        /// <summary>
        /// Muestra el cuadro de dialogo para girar e inicia el timer para girar
        /// automaticamente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void girarToolStripMenuItem_Click(object sender, EventArgs e)
        {              
                dlgiros.Show();               
                timer.Start();
        }

        /// <summary>
        /// Muestra u oculta la informacion del archivo *.obj.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ocultarMostrarInformacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripContainer1.Visible =!toolStripContainer1.Visible;
        }

        /// <summary>
        /// Muestra el cuadro de dialogo para modificar los parametros del objeto al seleccionar
        /// la opciopn "Parametros" en el menu Opciones.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parametrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlgop.IsDisposed)
                dlgop = new Opciones(this);
                dlgop.Show();
        }

        /// <summary>
        /// Actualiza los parametros de la figura leyendolos del cuadro de dialogo
        /// Opciones.
        /// </summary>
        public void ActualizaParametros()
        {
            this._zp = dlgop.ZP;
            this._q = dlgop.Q;
            this._dx = dlgop.DX;
            this._dy = dlgop.DY;
            this._dz = dlgop.DZ;
            this._x = dlgop.X;
            this._y = dlgop.Y;
            this._lineas = dlgop.Dbujado;
            this._caras = dlgop.Visualizacion;
            this.dibujaFiguraToolStripMenuItem_Click(null, null);
        }

        

        
    }
}
 