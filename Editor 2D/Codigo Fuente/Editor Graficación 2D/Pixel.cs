using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tao.OpenGl;

namespace Editor_Graficación_2D
{
    /// <summary>
    /// Clase que crea, manipula y dibuja un Pixel.
    /// </summary>
    [Serializable]
    class Pixel:Figura
    {
        /// <summary>
        /// Guarda las coordenadas del pixel.
        /// </summary>
        private Point _point;

        /// <summary>
        /// Constructor de la clase pixel.
        /// </summary>
        /// <param name="p">Punto del Pixel.</param>
        /// <param name="Color">Color del Pixel.</param>
        /// <param name="Ancho">Ancho de la Linea.</param>
        public Pixel(Point p, Color Color,int Ancho)
            : this(p.X, p.Y, Color,Ancho)
        {
        }

        /// <summary>
        /// Contructor de la Clase Pixel
        /// </summary>
        /// <param name="x">Coordenada X del Pixel.</param>
        /// <param name="y">Coordenada Y del Pixel.</param>
        /// <param name="Color">Color del Pixel.</param>
        /// <param name="Ancho">Ancho del Pixel</param>
        public Pixel(int x, int y, Color Color, int Ancho)
            : base(Color, Figura.Tipo.Pixel,Ancho)
        {
            _point = new Point(x, y);
        }       

        /// <summary>
        /// Devuelve o establece la posicion del Pixel.
        /// </summary>
        public Point Location
        {
            set
            {
                _point = value;
            }
            get
            {
                return _point;
            }
        }

        /// <summary>
        /// Obtiene o establece la coordenada X del pixel.
        /// </summary>
        public int X
        {
            set
            {
                _point.X = value;
            }
            get
            {
                return _point.X;
            }
        }

        /// <summary>
        /// Obtiene o establece la coordenada Y del pixel.
        /// </summary>
        public int Y
        {
            set
            {
                _point.Y = value;
            }
            get
            {
                return _point.Y;
            }
        }
        
        /// <summary>
        /// Dibuja el Pixel.
        /// </summary>
        public override void Dibuja()
        {
            Gl.glColor4ub(_color.R, _color.G, _color.B, _color.A);
            Gl.glVertex2d(_point.X, _point.Y);
           
        }
       
        /// <summary>
        /// Checa si el punto "pt" esta cerca del Pixel.
        /// </summary>
        /// <param name="pt">Punto a checar.</param>
        /// <param name="range">Rango de acercamiento.</param>
        /// <returns>Regresa 1 si esta cerca ó en el Pixel, de lo contrario
        /// regresa 0.</returns>
        public override int PointInFig(Point pt, int range)
        {
            int ret = 0;

            if (pt.X <= _point.X + range && pt.X >= _point.X - range && pt.Y < _point.Y + range && pt.Y > _point.Y - range)
                ret = 1;

            return ret;
        }
       
    }
}
