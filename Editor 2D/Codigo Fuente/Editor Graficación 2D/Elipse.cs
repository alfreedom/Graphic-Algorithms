using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tao.OpenGl;

namespace Editor_Graficación_2D
{
    /// <summary>
    /// Clase para crear, manipular y dibujar una Elipse.
    /// </summary>
    [Serializable]
    class Elipse:Figura
    {
        /// <summary>
        /// Guarda el Radio A de la Elipse.
        /// </summary>
        private int _ra;
        /// <summary>
        /// Guarda el Radio B de la Elipse
        /// </summary>
        private int _rb;
        /// <summary>
        /// Guarda las coordenadas del origen de la Elipse.
        /// </summary>
        private Point _origen;
        /// <summary>
        /// Guarda el punto final de la Elipse.
        /// Se utiliza para calcular los radios.
        /// </summary>
        private Point _pf;
        /// <summary>
        /// Guarda el ángulo de rotación de la Elipse.
        /// </summary>
        private int angulo;

        /// <summary>
        /// Contructor de la Clase Elipse
        /// </summary>
        /// <param name="XI">Coordenada X inical del Origen.</param>
        /// <param name="YI">Coordenada Y inical del Origen.</param>
        /// <param name="XF">Coordenada X Final.</param>
        /// <param name="YF">Coordenada Y Final.</param>
        /// <param name="Color">Color de la Elipse.</param>
        /// <param name="Ancho">Ancho de la Linea.</param>
        public Elipse(int XI, int YI, int XF, int YF, Color Color,int Ancho)
            : this(new Point(XI, YI), new Point(XF, YF), Color, Ancho)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PI">Punto inicial de la Elipse (Origen).</param>
        /// <param name="PF">Punto final de la Elipse.</param>
        /// <param name="Color">Color de la Elipse.</param>
        /// <param name="Ancho">Ancho de la Linea.</param>
        public Elipse(Point PI, Point PF, Color Color, int Ancho)
            : base(Color, Figura.Tipo.Ellipse, Ancho)
        {
            this._ra = Math.Abs(PF.X - PI.X);
            this._rb = Math.Abs(PF.Y - PI.Y);
            this._origen = PI;
            this._pf = PF;
            _listPixeles = new List<Point>();
             CalculaPixeles();
        }

        /// <summary>
        /// Establece los nuevos radios para la Elipse. El radio A se encuentra en 
        /// la componente X del punto y el radio B en la componente Y.
        /// </summary>
        public Point Radio
        {
            set
            {
                this._ra = Math.Abs(value.X);
                this._rb = Math.Abs(value.Y);
                ClearPixeles();
                CalculaPixeles();
            }
            
        }
       
        /// <summary>
        /// Obtiene o estable el radio A (radio en X) de la Elipse.
        /// </summary>
        public int RadioA
        {
            set
            {
                this._ra = value;
                ClearPixeles();
                CalculaPixeles();
            }
            get
            {
                return _ra;
            }
        }        
       
        /// <summary>
        /// Obtiene o establece el radio B (radio en Y) de la Elipse.
        /// </summary>
        public int RadioB
        {
            set
            {
                this._rb =Math.Abs(value);
                ClearPixeles();
                CalculaPixeles();
            }
            get
            {
                return _rb;
            }
        }
        
        /// <summary>
        /// Obtiene o establece el origen de la Elipse.
        /// </summary>
        public Point Origen
        {
            get
            {
                return _origen;
            }
            set
            {
                _origen = value;
                ClearPixeles();
                CalculaPixeles();
            }
        }

        /// <summary>
        /// Obtiene o establece el angulo de rotacion de la elipse.
        /// </summary>
        public int AnguloRotacion
        {
            get
            {
                return angulo;
            }
            set
            {
                angulo = value;
                if (angulo == 360)
                    angulo = 0;
            }
        }
        
        /// <summary>
        /// Obtiene la lista de pixeles que componen la Elipse.
        /// </summary>
        public List<Point> Pixeles
        {
            get
            {
                return _listPixeles;
            }
        }
        
        /// <summary>
        /// Calcula los pixeles que componen la elipse.
        /// </summary>
        private void CalculaPixeles()
        {
            List<Point> listaux= new List<Point>();
            double d0;
            int ra2, rb2, x, y;
            ra2=_ra*_ra;
            rb2=_rb*_rb;
            x=0;
            y=_rb;
            d0 = rb2 - (ra2 * _rb) + (ra2 / 4);

            
            while ((ra2 * (y - .5)) > (rb2 * (x + 1)))
            {
                if (d0 > 0)
                {
                    listaux.Add(new Point(++x, --y));
                    d0 = d0 + (rb2 * (2 * x + 3) + ra2 * (-2 * y + 2));
                }
                else
                {
                    listaux.Add(new Point(++x, y));
                    d0 = d0 + (rb2 * (2 * x + 3) );
                }
            }
            x++;
            d0 = (rb2 * x) + (ra2 * 2 * y) + (rb2 / 4) + ra2;
            
            while (y != 0)
            {
                if (d0 > 0)
                {
                    listaux.Add(new Point(x, --y));
                    d0 = d0 + ((-2 * ra2 * y) + (3 * ra2));
                }
                else
                {
                    listaux.Add(new Point(++x, --y));
                    d0 = d0 + (rb2 * (2 * x + 2) + ra2 * (-2 * y + 3));
                }
            }

            _listPixeles.Add(new Point(_origen.X,_origen.Y- _rb));
            _listPixeles.Add(new Point(_origen.X, _origen.Y + _rb));
            foreach (Point p in listaux)
            {
                _listPixeles.Add(new Point(_origen.X + p.X, _origen.Y - p.Y));
                _listPixeles.Add(new Point(_origen.X - p.X, _origen.Y - p.Y));
                _listPixeles.Add(new Point(_origen.X - p.X, _origen.Y + p.Y));
                _listPixeles.Add(new Point(_origen.X + p.X, _origen.Y + p.Y));
            }
            this.Rota(angulo);
        }
       
        /// <summary>
        /// Dibuja los pixeles que componene la Elipse.
        /// </summary>
        public override void Dibuja()
        {
            int i = 0;
            bool band = true;
            Random rnd = new Random();
            bool line = true, dot = false;
            Gl.glColor4ub(_color.R, _color.G, _color.B, _color.A);
            Gl.glPointSize(_grosor);
            Gl.glBegin(Gl.GL_POINTS); //se inicia el pintado    
            foreach (Point P in _listPixeles)
            {
                switch (_estilo)
                {
                    case 2:
                        if (i % 40 == 0)
                            band = !band;
                        break;
                    case 3:
                        if (i % 20 == 0)
                            band = !band;
                        else
                            if (i % 4 == 0)
                                band = false;
                        break;
                    case 4:
                        if (line)
                        {
                            if (i % 20 == 0)
                            {
                                band = !band;
                                if (!band)
                                {
                                    line = false;
                                    dot = true;
                                }
                            }
                        }
                        else
                            if (dot)
                            {
                                if (i % 5 != 0)
                                {
                                    band = !band;
                                    if (!band)
                                    {
                                        line = false;
                                        dot = true;
                                    }
                                }
                            }


                        break;
                }
                if (band)
                    Gl.glVertex2d(P.X, P.Y);

                i++;
            }

            Gl.glEnd();               //se finaliza el pintado  
            
        }
        
        /// <summary>
        /// Checa si el punto "pt"  esta en la Elipse.
        /// </summary>
        /// <param name="pt">Punto a checar.</param>
        /// <param name="range">Rango de alcanze</param>
        /// <returns>1 si el punto esta cerca ó en el origen o 2 si el punto
        /// esta en la circunferencia.</returns>
        public override int PointInFig(Point pt, int range)
        {
            int ret = 0;
            if (pt.X >= _origen.X - range - 5 && pt.X <= _origen.X + range + 5 && pt.Y >= _origen.Y - range - 5 && pt.Y <= _origen.Y + range + 5)
                ret = 1;
            else
            {

                foreach (Point pix in _listPixeles)
                {
                    if (pt.X >= pix.X - range && pt.X <= pix.X + range && pt.Y >= pix.Y - range && pt.Y <= pix.Y + range)
                    {
                        ret = 2;
                        break;
                    }
                }
            }

            return ret;
        }
       
        /// <summary>
        /// Borra la lista de pixeles que componen la Elipse.
        /// </summary>
        private void ClearPixeles()
        {
            _listPixeles.RemoveRange(0, _listPixeles.Count);
        }
    }
}
