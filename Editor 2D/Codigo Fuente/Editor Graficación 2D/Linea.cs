using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tao.OpenGl;

namespace Editor_Graficación_2D
{

    /// <summary>
    /// Clase para crear, manipular y dibujar una Linea.
    /// </summary>
    [Serializable]
    class Linea : Figura
    {
        /// <summary>
        /// Guarda el punto inicial de la Linea.
        /// </summary>
        private Point _pi;
        /// <summary>
        /// Guarda el punto el punto final de la Linea
        /// </summary>
        private Point _pf;
        /// <summary>
        /// Guarda la delta X para calcular los puntos de la Linea.
        /// </summary>
        private int _dx;
        /// <summary>
        /// Guarda la delta Y para calcular los puntos de la Linea.
        /// </summary>
        private int _dy;
        /// <summary>
        /// Guarda la delta nueva para calcular los puntos de la Linea.
        /// </summary>
        private int _d0;
        /// <summary>
        /// Guarda la pendiente de la Linea.
        /// </summary>
        private double _pendiente;
        
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Xi">X inicial de la linea</param>
        /// <param name="Yi">Y inicial de la linea</param>
        /// <param name="Xf">X final de la linea</param>
        /// <param name="Yf">Y final de la linea</param>
        /// <param name="Color">Color de la linea</param>
        /// <param name="Ancho">Ancho de la Linea.</param>
        public Linea(int Xi,int Yi, int Xf, int Yf, Color Color,int Ancho)
            :this(new Point(Xi,Yi),new Point(Xf,Yf),Color,Ancho)
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Pi">Punto inicial de la linea.</param>
        /// <param name="Pf">Punto final de la linea.</param>
        /// <param name="Color">Color de la linea</param>
        /// <param name="Ancho">Ancho de la Linea.</param>
        public Linea(Point Pi, Point Pf, Color Color, int Ancho)
            : base(Color, Figura.Tipo.Line,Ancho)
        {
            _pi = Pi;
            _pf = Pf;
            _listPixeles = new List<Point>();
            CalculaPixeles();
        }

        /// <summary>
        /// Calcula los pixeles que componen la linea.
        /// Se utiliza el algoritmo de Breseham.
        /// </summary>
        private void CalculaPixeles()
        {
            Boolean cambio = false;
            int aux_i=0,aux_f=0;
            

            //_listPixeles.Add(_pi);

            //Checamos si es una linea vertical.
            if (_pi.X==_pf.X)
            {
                aux_i = _pi.Y;
                aux_f =_pf.Y;
                if (_pi.Y > _pf.Y)
                {
                    aux_i = _pf.Y;
                    aux_f = _pi.Y;
                }

                int a = aux_i;
                for (int i = 0; i < _grosor/4+1; i++)
                {
                    while (aux_i++ != aux_f)
                        _listPixeles.Add(new Point(_pi.X+i, aux_i));
                    aux_i = a;
                }
                aux_i = a;
            }
            else //si no, checamos si es horizontal
                if (_pi.Y == _pf.Y)
                {
                    aux_i = _pi.X;
                    aux_f = _pf.X;
                    if (_pi.X > _pf.X)
                    {
                        aux_i = _pf.X;
                        aux_f = _pi.X;
                    }
                    int a = aux_i;
                    for (int i = 0; i < _grosor/4+1; i++)
                    {
                        while (aux_i++ != aux_f)
                            _listPixeles.Add(new Point(aux_i, _pi.Y+i));
                        aux_i = a;
                    }
                    aux_i = a;
                }
                else //si no es horizontal ni vertical, aplicamos algun algoritmo de linea
                {
                    if ((_pf.X < _pi.X && _pf.Y > _pi.Y) || (_pf.X<_pi.X ))
                    {
                        Point p = _pi;
                        _pi = _pf;
                        _pf = p;
                        cambio = true;
                    }

                    _dx = _pf.X - _pi.X;
                    _dy = _pi.Y - _pf.Y;
                   
                    _listPixeles.Add(_pi);
                    _listPixeles.Add(_pf);
                    _pendiente = ((double)_dy / (double)_dx);
                        if (_pendiente > 0 && _pendiente <= 1)
                        {
                            BresehamE_NE();
                           
                        }
                        else
                            if (_pendiente < 0 && _pendiente >= -1)
                                BresehamE_SE();
                            else
                                if (_pendiente > 1)
                                    BresehamN_NE();
                                else
                                    BresehamS_SE();
                    
                    if (cambio)
                    {
                        Point p = _pi;
                        _pi = _pf;
                        _pf = p;
                    }
                }
        }

        /// <summary>
        /// Agoritmo de Bresenham para el segmento E-NE
        /// </summary>
        private void BresehamE_NE()
        {
            int deltaE, deltaNE, yaux;
            Point paux;
            _d0 = (2 * _dy) - (_dx);
            deltaE = 2 * _dy;
            deltaNE = (2 * _dy) - (2 * _dx);
            yaux = _pi.Y;

            for (int i = _pi.X + 1; i < _pf.X; i++)
            {
                if (_d0 > 0)
                {
                    paux = new Point(i, --yaux);
                    _d0 = _d0 + deltaNE;
                }
                else
                {
                    paux = new Point(i, yaux);
                    _d0 = _d0 + deltaE;
                }

                _listPixeles.Add(paux);
            }
        }
        /// <summary>
        /// Agoritmo de Bresenham para el segmento E-SE
        /// </summary>
        private void BresehamE_SE()
        {
            int deltaE, deltaSE, yaux;
            Point paux;
            _d0 = (2 * _dy) + (_dx);
            deltaE = 2 * _dy;
            deltaSE = (2 * _dy) + (2 * _dx);
            yaux = _pi.Y;

            for (int i = _pi.X + 1; i < _pf.X; i++)
            {
                if (_d0 < 0)
                {
                    paux = new Point(i, ++yaux);
                    _d0 = _d0 + deltaSE;
                }
                else
                {
                    paux = new Point(i, yaux);
                    _d0 = _d0 + deltaE;
                }

                _listPixeles.Add(paux);
            }
        }
        /// <summary>
        /// Agoritmo de Bresenham para el segmento N-SE
        /// </summary>
        private void BresehamN_NE()
        {
            int deltaN, deltaNE, xaux;
            Point paux;
            _d0 = _dy - (2 * _dx);
            deltaN = (-2 * _dx);
            deltaNE = (2 * _dy) - (2 * _dx);
            xaux = _pi.X;

            for (int i = _pi.Y - 1; i > _pf.Y; i--)
            {
                if (_d0 < 0)
                {
                    paux = new Point(++xaux, i);
                    _d0 = _d0 + deltaNE;
                }
                else
                {
                    paux = new Point(xaux, i);
                    _d0 = _d0 + deltaN;
                }

                _listPixeles.Add(paux);
            }
        }
        /// <summary>
        /// Agoritmo de Bresenham para el segmento S-SE
        /// </summary>
        private void BresehamS_SE()
        {
            int deltaS, deltaSE, xaux;
            Point paux;
            _d0 = _dy + (2 * _dx);
            deltaS = (2 * _dx);
            deltaSE = (2 * _dy) + (2 * _dx);
            xaux = _pi.X;

            for (int i = _pi.Y + 1; i < _pf.Y; i++)
            {
                if (_d0 > 0)
                {
                    paux = new Point(++xaux, i);
                    _d0 = _d0 + deltaSE;
                }
                else
                {
                    paux = new Point(xaux, i);
                    _d0 = _d0 + deltaS;
                }

                _listPixeles.Add(paux);
            }
        }

        /// <summary>
        /// Obtiene o establece el punto inicial de la linea.
        /// </summary>
        public Point PI
        {
            set
            {
                _pi = value;
                this.ClearPixeles();
                CalculaPixeles();
            }
            get
            {
                return _pi;
            }
        }

        /// <summary>
        /// Obtiene o establece el punto final de la linea.
        /// </summary>
        public Point PF
        {
            set
            {
                _pf = value;
                this.ClearPixeles();
                CalculaPixeles();
            }
            get
            {
                return _pf;
            }
        }
        
        /// <summary>
        /// Obtiene o establece la coordenada X del primer punto de la linea.
        /// </summary>
        public int XI
        {
            set
            {
                _pi.X = value;
                this.ClearPixeles();
                CalculaPixeles();
            }
            get
            {
                return _pi.X;
            }
        }

        /// <summary>
        /// Obtiene o establece la coordenada Y del primer punto de la linea.
        /// </summary>
        public int YI
        {
            set
            {
                _pi.Y = value;
                this.ClearPixeles();
                CalculaPixeles();
            }
            get
            {
                return _pi.Y;
            }
        }

        /// <summary>
        /// Obtiene o establece la coordenada X del segundo punto de la linea.
        /// </summary>
        public int XF
        {
            set
            {
                _pf.X = value;
                this.ClearPixeles();
                CalculaPixeles();
            }
            get
            {
                return _pf.X;
            }
        }

        /// <summary>
        /// Obtiene o establece la coordenada Y del segundo punto de la linea.
        /// </summary>
        public int YF
        {
            set
            {
                _pf.Y = value;
                this.ClearPixeles();
                CalculaPixeles();
            }
            get
            {
                return _pf.Y;
            }
        }       
        
        /// <summary>
        /// Obtiene la lista de pixeles que componen la linea.
        /// </summary>
        public List<Point> Pixeles
        {
            get
            {
                return _listPixeles;
            }
        }

        /// <summary>
        /// Dibuja los pixeles que componene la Linea.
        /// </summary>
        public override void Dibuja()
        {
            int i = 0;
            bool band = true;
            Random rnd= new Random();
            bool line=true, dot=false;
            Gl.glColor4ub(_color.R, _color.G, _color.B, _color.A);
            Gl.glPointSize(_grosor);
            Gl.glBegin(Gl.GL_POINTS); //se inicia el pintado    
            foreach (Point P in _listPixeles)
            {
                switch (_estilo)
                { 
                    case 2:
                        if (i % 10 == 0)
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
                                line = false;
                                dot = true;
                            }
                        }
                        else
                            if (dot)
                            {
                                if (i % 5 != 0)
                                {
                                    band = false;
                                    line = true;
                                    dot = false;
                                }
                            }

                    
                    break;
                }
                if(band)
                Gl.glVertex2d(P.X, P.Y);

                i++;
            }

            Gl.glEnd();               //se finaliza el pintado  
                        
        }
      

        /// <summary>
        /// Checa si el punto "pt"  esta en la Linea.
        /// </summary>
        /// <param name="pt">Punto a checar.</param>
        /// <param name="range">Rango de alcanze</param>
        /// <returns>1 si el punto esta cerca ó en el primer punto de la linea,
        /// o 2 si el punto esta cerca ó en el segundo punto de la linea o 3 si
        /// si el punto esta en cualquier otra parte de la linea
        /// de lo contrario regresa 0.</returns>
        public override int PointInFig(Point pt, int range)
        {
            int ret = 0;

            if (pt.X >= _pi.X - range && pt.X <= _pi.X + range && pt.Y >= _pi.Y - range && pt.Y <= _pi.Y + range)
                ret = 1;
            else
                if (pt.X >= _pf.X - range && pt.X <= _pf.X + range && pt.Y >= _pf.Y - range && pt.Y <= _pf.Y + range)
                    ret = 2;
                else
                {
                    foreach (Point pix in _listPixeles)
                        if (pt.X >= pix.X - range && pt.X <= pix.X + range && pt.Y >= pix.Y - range && pt.Y <= pix.Y + range)
                        {
                            ret = 3;
                            break;
                        }
                }
            return ret;
        }

        /// <summary>
        /// Borra la lista de pixeles que componen la Linea.
        /// </summary>
        private void ClearPixeles()
        {
            _listPixeles.RemoveRange(0, _listPixeles.Count);
        }

    }
}
