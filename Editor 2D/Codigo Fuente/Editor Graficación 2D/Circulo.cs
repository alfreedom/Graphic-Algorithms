using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tao.OpenGl;


namespace Editor_Graficación_2D
{
    /// <summary>
    /// Clase para crear, manipular y dibujar un Circulo.
    /// </summary>
    [Serializable]
    class Circulo:Figura
    {
        /// <summary>
        /// Guarda el punto de origen del Circulo.
        /// </summary>
        private Point _origen;
        /// <summary>
        /// Guarda el radio del Circulo.
        /// </summary>
        private int _radio;

        /// <summary>
        /// Constructor de la clase Circulo.
        /// </summary>
        /// <param name="PI">Punto inicial del Circulo (origen).</param>
        /// <param name="PF">Punto final del Circulo.</param>
        /// <param name="Color">Color del Circulo.</param>
        /// <param name="Ancho">Ancho de la Linea.</param>
        public Circulo(Point PI, Point PF, Color Color,int Ancho)
            : this(PI.X, PI.Y, PF.X, PF.Y, Color,Ancho)
        { }
        /// <summary>
        /// Constructor de la clase Circulo.
        /// </summary>
        /// <param name="XI">Coordenada X inicial del circulo (Coordenada X del origen).</param>
        /// <param name="YI">Coordenada Y inicial del circulo (Coordenada Y del origen).</param>
        /// <param name="XF">Coordenada X final del circulo.</param>
        /// <param name="YF">Coordenada Y final del circulo.</param>
        /// <param name="Color">Color del Circulo</param>
        /// <param name="Ancho">Ancho de la Linea.</param>
        public Circulo(int XI, int YI, int XF, int YF, Color Color,int Ancho)
            : base(Color, Figura.Tipo.Circle,Ancho)
        {
            this._origen = new Point(XI, YI);
            this._radio = (int)Math.Sqrt(Math.Pow((XF - XI), 2) + Math.Pow((YF - YI), 2));
            this._listPixeles = new List<Point>();
            this.CalculaPixeles();
        }
        /// <summary>
        /// Constructor de la clase Circulo.
        /// </summary>
        /// <param name="x">Coordenada X inicial del circulo (Coordenada X del origen).</param>
        /// <param name="y">Coordenada Y inicial del circulo (Coordenada Y del origen).</param>
        /// <param name="Radio">Radio del Circulo.</param>
        /// <param name="Color">Color del Circulo.</param>
        /// <param name="Ancho">Ancho de la Linea.</param>
        public Circulo(int x, int y, int Radio, Color Color,int Ancho)
            : this(new Point(x, y), Radio, Color,Ancho)
        { 
        }
        /// <summary>
        /// Constructor de la clase Circulo.
        /// </summary>
        /// <param name="Origen">Punto del origen del Circulo.</param>
        /// <param name="Radio">Radio del Circulo.</param>
        /// <param name="Color">Color del Circulo.</param>
        /// <param name="Ancho">Ancho de la Linea.</param>
        public Circulo(Point Origen, int Radio, Color Color,int Ancho)
            : base(Color, Figura.Tipo.Circle,Ancho)
        {
            this._origen = Origen;
            this._radio = Radio;
            this._listPixeles = new List<Point>();            
            this.CalculaPixeles();
            
        }

        /// <summary>
        /// Obtiene o establece el radio del Circulo.
        /// </summary>
        public int Radio
        {
            set
            {
                _radio = value;
                ClearPixeles();
                this.CalculaPixeles();
              
            }
            get
            {
                return _radio;
            }
        }
      
        /// <summary>
        /// Obtiene o establece el punto de origen del Circulo.
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
                this.CalculaPixeles();
            }
        }        
       
        /// <summary>
        /// Obtiene la lista de pixeles que componen el Circulo.
        /// </summary>
        public List<Point> Pixeles
        {
            get
            {
                return _listPixeles;
            }
        }
        
        /// <summary>
        /// Calcula los pixeles a pintar mediante el algoritmo del Punto Medio.
        /// </summary>
        private void CalculaPixeles()
        {
            int d0;
            List<Point> listaux= new List<Point>();
            int x=0, y=_radio;
            d0 = 1 - _radio;
            _listPixeles.Add(new Point(_origen.X + x, _origen.Y - y));
            _listPixeles.Add(new Point(_origen.X + _radio, _origen.Y - 0));
            _listPixeles.Add(new Point(_origen.X - _radio, _origen.Y - 0));
            _listPixeles.Add(new Point(_origen.X + x, _origen.Y +y));
            while(y>=x)
            {
                if (d0 < 0)
                {
                    _listPixeles.Add(new Point(_origen.X + x++, _origen.Y - y));
                    
                    d0=d0+(((2*x)+3));
                }
                else
                {
                    _listPixeles.Add(new Point(_origen.X + x++, _origen.Y - y--));
                    d0=d0+((2*x)-(2*y)+5);
                }
                _listPixeles.Add(new Point(_origen.X - x, _origen.Y - y));
                _listPixeles.Add(new Point(_origen.X + x, _origen.Y + y));
                _listPixeles.Add(new Point(_origen.X - x, _origen.Y + y));
                _listPixeles.Add(new Point(_origen.X + y, _origen.Y - x));
                _listPixeles.Add(new Point(_origen.X - y, _origen.Y - x));
                _listPixeles.Add(new Point(_origen.X + y, _origen.Y + x));
                _listPixeles.Add(new Point(_origen.X - y, _origen.Y + x));
            }

            //Calcula8Puntos(listaux);

        }

        /// <summary>
        /// Dibuja los pixeles que componene el Circulo.
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
        /// Checa si el punto "pt" esta en el Circulo.
        /// </summary>
        /// <param name="pt">Punto a checar.</param>
        /// <param name="range">Rango de alcanze</param>
        /// <returns>1 si el punto esta cerca o en el origen o 2 si el punto
        /// esta cerca o en la circunferencia, de lo contrario regresa 0.</returns>
        public override int PointInFig(Point pt, int range)
        {
            int ret = 0;
            if (pt.X >= _origen.X - range - 5 && pt.X <= _origen.X + range + 5 && pt.Y >= _origen.Y - range - 5 && pt.Y <= _origen.Y + range + 5)
                ret = 1;
            else
            {
                foreach (Point pix in _listPixeles)
                    if (pt.X >= pix.X - range && pt.X <= pix.X + range && pt.Y >= pix.Y - range && pt.Y <= pix.Y + range)
                    {
                        ret = 2;
                        break;
                    }
            }
            return ret;
        }

        /// <summary>
        /// Borra la lista de pixeles que componen el circulo.
        /// </summary>
        private void ClearPixeles()
        {
            _listPixeles.RemoveRange(0, _listPixeles.Count);
        }
    }
}
