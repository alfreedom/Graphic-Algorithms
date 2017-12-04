using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tao.OpenGl;


namespace Editor_Graficación_2D
{
    /// <summary>
    /// Clase abstracta que contiene metodos y atributos comunes
    /// para las figuras.
    /// </summary>
    [Serializable]
    abstract class Figura
    {

        /// <summary>
        /// Guarda el color de la figura.
        /// </summary>
        protected Color _color;
        /// <summary>
        /// Guarda el tipo de figura.
        /// </summary>
        protected Figura.Tipo _tipo;
        /// <summary>
        /// Guarda el grosor de Linea de la figura.
        /// </summary>
        protected int _grosor;
        /// <summary>
        /// Guarda el estilo de linea de la figura.
        /// </summary>
        protected int _estilo;
        /// <summary>
        /// Guarda la lista de puntos que conforman a la figura.
        /// </summary>
        protected List<Point> _listPixeles;
        /// <summary>
        /// Enumeracion que contiene los tipos de figura.
        /// </summary>
        public enum Tipo
        {
            Pixel = 1,
            Line = 2,
            Circle = 3,
            Ellipse = 4,
            CHermite = 5,
            CBezier = 6,
        }

        /// <summary>
        /// Constructor de la clase figura.
        /// </summary>
        /// <param name="Color">Color de la figura.</param>
        /// <param name="Tipo">Tipo de la Figura.</param>
        /// <param name="Ancho">Ancho de la Linea.</param>
        public Figura(Color Color, Figura.Tipo Tipo, int Ancho)
        {
            this._color = Color;
            this._tipo = Tipo;
            this._grosor = Ancho;
        }

        /// <summary>
        /// Dibuja los pixeles que componen la figura.
        /// </summary>
        public abstract void Dibuja();
        /// <summary>
        /// Checa si el punto "pt" se encuentra en la figura.
        /// </summary>
        /// <param name="pt">Punto a checar.</param>
        /// <param name="range">Rango de alcanze.</param>
        /// <returns>Regresa un valor diferente de 0 dependiendo de la figura.</returns>
        public abstract int PointInFig(Point pt, int range);
        

        /// <summary>
        /// Obtiene o establece el color de la figura.
        /// </summary>
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
        /// <summary>
        /// Obtiene el tipo de figura.
        /// </summary>
        public Figura.Tipo TipoFigura
        {
            get
            {
                return _tipo;
            }
        }
        /// <summary>
        /// Obtiene o establece el grosor de la linea.
        /// </summary>
        public int Grosor
        {
            get
            {
                return _grosor;
            }
            set
            {
                _grosor = value;
            }
        }
        /// <summary>
        /// Obtiene o establece el Estilo de la linea.
        /// </summary>
        public int Estilo
        {
            get
            {
                return _estilo;
            }
            set
            {
                _estilo = value;
            }
        }
        /// <summary>
        /// Dibuja un pixel en las coordenadas X y Y especificas.
        /// </summary>
        /// <param name="x">Coordenada X del pixel.</param>
        /// <param name="y">Coordenada Y del pixel</param>
        /// <param name="color">Color del Pixel.</param>
        public static void PutPixel(int x, int y, Color color)
        {
            Gl.glBegin(Gl.GL_POINTS);
            Gl.glColor4ub(color.R, color.G, color.B, color.A);
            Gl.glVertex3i(x, y, 0);
            Gl.glEnd();
        }

        /// <summary>
        /// Rota la figura en cierto angulo.
        /// </summary>
        /// <param name="Grados">Numero de grados que la figura se rotara.</param>
        public void Rota(int Grados)
        {
            List<Point> laux= new List<Point>();
            long xaux, yaux, x, y;
            double g = (Grados * Math.PI) / 180;
          
                Point p = new Point();
                switch (_tipo)
                { 
                    case Figura.Tipo.Line:
                        xaux=((Linea)this).PI.X;
                        yaux = ((Linea)this).PI.Y;
                        x = ((Linea)this).PF.X;
                        y = ((Linea)this).PF.Y;

                        p.X = (int)(Math.Cos(g) * x + (-Math.Sin(g) * y) + (xaux * (1 - Math.Cos(g)) + (yaux * Math.Sin(g))));
	                    p.Y = (int)(Math.Sin(g) * x + ( Math.Cos(g) * y) + (yaux * (1 - Math.Cos(g)) - (xaux * Math.Sin(g))));

                        ((Linea)this).PF = p;
                    break;
                    case Figura.Tipo.Ellipse:

                       xaux = ((Elipse)this).Origen.X;
                       yaux = ((Elipse)this).Origen.Y;
                        foreach (Point po in _listPixeles)
                        {                            
                            x = po.X;
                            y = po.Y;
                            p.X = (int)(Math.Cos(g) * x + (-Math.Sin(g) * y) + (xaux * (1 - Math.Cos(g)) + (yaux * Math.Sin(g))));
                            p.Y = (int)(Math.Sin(g) * x + (Math.Cos(g) * y) + (yaux * (1 - Math.Cos(g)) - (xaux * Math.Sin(g))));
                            laux.Add(p);
                        }
                        _listPixeles.Clear();
                        _listPixeles = laux;
                        
                    break;
                    case Figura.Tipo.CHermite:
                    case Figura.Tipo.CBezier:

                    xaux = ((Curva)this).PI.X;
                    yaux = ((Curva)this).PI.Y;

                        x = ((Curva)this).PF.X;
                        y = ((Curva)this).PF.Y;
                        p.X = (int)(Math.Cos(g) * x + (-Math.Sin(g) * y) + (xaux * (1 - Math.Cos(g)) + (yaux * Math.Sin(g))));
                        p.Y = (int)(Math.Sin(g) * x + (Math.Cos(g) * y) + (yaux * (1 - Math.Cos(g)) - (xaux * Math.Sin(g))));
                        ((Curva)this).PF=p;

                        x = ((Curva)this).PCTRL1.X;
                        y = ((Curva)this).PCTRL1.Y;
                        p.X = (int)(Math.Cos(g) * x + (-Math.Sin(g) * y) + (xaux * (1 - Math.Cos(g)) + (yaux * Math.Sin(g))));
                        p.Y = (int)(Math.Sin(g) * x + (Math.Cos(g) * y) + (yaux * (1 - Math.Cos(g)) - (xaux * Math.Sin(g))));
                        ((Curva)this).PCTRL1=p;

                        x = ((Curva)this).PCTRL2.X;
                        y = ((Curva)this).PCTRL2.Y;
                        p.X = (int)(Math.Cos(g) * x + (-Math.Sin(g) * y) + (xaux * (1 - Math.Cos(g)) + (yaux * Math.Sin(g))));
                        p.Y = (int)(Math.Sin(g) * x + (Math.Cos(g) * y) + (yaux * (1 - Math.Cos(g)) - (xaux * Math.Sin(g))));
                        ((Curva)this).PCTRL2=p;
                   

                    break;
                }
            
        }
    }

    
}
