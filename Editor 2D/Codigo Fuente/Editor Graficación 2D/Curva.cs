using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tao.OpenGl;

namespace Editor_Graficación_2D
{
    /// <summary>
    /// Clase que crea, manipula y dibuja una Curva.
    /// La curva puede ser una Curva de Hermitte, o una Curva de Bezier.
    /// </summary>
    [Serializable]
    class Curva : Figura
    {
        /// <summary>
        /// Guarda el punto inicial de la Curva.
        /// </summary>
        private Point _pi;
        /// <summary>
        /// Guarda el punto final de la Curva.
        /// </summary>
        private Point _pf;
        /// <summary>
        /// Guarda el punto de control 1 de la Curva.
        /// </summary>
        private Point _control_1;
        /// <summary>
        /// Guarda el punto de control 2 de la Curva.
        /// </summary>
        private Point _control_2;
        /// <summary>
        /// Guarda las lineas para dibujar la posicion de los puntos de control.
        /// </summary>
        Linea l1, l2;

        /// <summary>
        /// Contructor de la Clase Curva
        /// </summary>
        /// <param name="PI">Punto inicial de la curva.</param>
        /// <param name="PF">Punto final de la curva.</param>
        /// <param name="Color">Color de la Curva.</param>
        /// <param name="Tipo">Tipo de curva.</param>
        /// <param name="Ancho">Ancho de la Linea.</param>
        public Curva(Point PI, Point PF, Color Color,Figura.Tipo Tipo, int Ancho)
            : base(Color, Tipo,Ancho)
        {
            _pi=_control_1 = PI;
            _pf= _control_2= PF;            
            _listPixeles = new List<Point>();
            Linea l = new Linea(PI, PF, Color,Ancho);
            _listPixeles = l.Pixeles;
            l1 = new Linea(PI, _control_1, Color, 1);
            l2 = new Linea(PF, _control_2, Color, 1);
            //this.CalculaPixeles();
        }

        /// <summary>
        /// Calcula los pixeles de la curva dependiendo del tipo de curva, que puede ser
        /// la Curva de Hermite o la Curva de Bezier
        /// </summary>
        private void CalculaPixeles()
        {
            Point p=new Point();
            for (double u = 0; u < 1; u += .0005)
            {
                if (_tipo == Figura.Tipo.CHermite)
                {
                    //calculamos los puntos con la matriz de hermite.
                    p.X = (int)((_pi.X * (2 * Math.Pow(u, 3) - 3 * Math.Pow(u, 2) + 1)) + (_pf.X * (-2 * Math.Pow(u, 3) + 3 * Math.Pow(u, 2)))
                          + (_control_1.X * (Math.Pow(u, 3) - 2* Math.Pow(u, 2) + u)) + (_control_2.X * (Math.Pow(u, 3) - Math.Pow(u, 2))));

                    p.Y = (int)((_pi.Y * (2 * Math.Pow(u, 3) - 3 * Math.Pow(u, 2) + 1)) + (_pf.Y * (-2 * Math.Pow(u, 3) + 3 * Math.Pow(u, 2)))
                          + (_control_1.Y * (Math.Pow(u, 3) -  2*Math.Pow(u, 2) + u)) + (_control_2.Y * (Math.Pow(u, 3) - Math.Pow(u, 2))));

        
                }
                else
                    if (_tipo == Figura.Tipo.CBezier)
                    {
                        //calculamos puntos con la matriz de bezier
                        p.X = (int)((_pi.X * (Math.Pow(-u, 3) + 3 * Math.Pow(u, 2) - 3 * u + 1)) + (_control_1.X * (3 * Math.Pow(u, 3) - 6 * Math.Pow(u, 2) + 3*u))
                              + (_control_2.X * (-3 * Math.Pow(u, 3) + 3 * Math.Pow(u, 2) )) + (_pf.X * (Math.Pow(u, 3))));

                        p.Y = (int)((_pi.Y * (Math.Pow(-u, 3) + 3 * Math.Pow(u, 2) - 3 * u + 1)) + (_control_1.Y * (3 * Math.Pow(u, 3) - 6 * Math.Pow(u, 2) + 3*u))
                              + (_control_2.Y * (-3 * Math.Pow(u, 3) + 3 * Math.Pow(u, 2) )) + (_pf.Y * (Math.Pow(u, 3))));
               
                    }
                _listPixeles.Add(p);
            }
            _listPixeles.Add(_pi);
            _listPixeles.Add(_pf);
            l1 = new Linea(_pi, _control_1, Color, 1);
            l2 = new Linea(_pf, _control_2, Color, 1);
        }
        /// <summary>
        /// Dibuja los pixeles que componene la Curva.
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
                if (band)
                    Gl.glVertex2d(P.X, P.Y);

                i++;
            }

            Gl.glEnd();               //se finaliza el pintado               //se finaliza el pintado  

            
        }
        /// <summary>
        /// Dibuja las referencia a los puntos de control.
        /// </summary>
        public void DibujaControles()
        {
            l1.Estilo = 2;
            l2.Estilo = 2;
            l1.Dibuja();
            l2.Dibuja();
        }
        /// <summary>
        /// Obtiene o establece el punto inicial de la Curva.
        /// </summary>
        public Point PI
        {
            get
            {
                return _pi;
            }
            set
            {
                _pi = value;
                this.ClearPixeles();
                this.CalculaPixeles();
            }
        }
        /// <summary>
        /// Obtiene o establece el punto fina de la Curva.
        /// </summary>
        public Point PF
        {
            get
            {
                return _pf;
            }
            set
            {
                _pf = value;
                this.ClearPixeles();
                this.CalculaPixeles();
            }
        }
        /// <summary>
        /// Obtiene o establece el punto de control 1 de la Curva.
        /// </summary>
        public Point PCTRL1
        {
            get
            {
                return _control_1;
            }
            set
            {
                _control_1 = value;
                this.ClearPixeles();
                this.CalculaPixeles();
            }
        }
        /// <summary>
        /// Obtiene o establece el punto de control 2 de la Curva.
        /// </summary>
        public Point PCTRL2
        {
            get
            {
                return _control_2;
            }
            set
            {
                _control_2 = value;
                this.ClearPixeles();
                this.CalculaPixeles();
            }
        }
        /// <summary>
        /// Checa si el punto "pt" esta en la Curva.
        /// </summary>
        /// <param name="pt">Punto a Checar.</param>
        /// <param name="range">Rango de alcanze.</param>
        /// <returns>Regresa 1 si el punto esta cerca ó en el punto inicial, 2 si esta cerca ó en el punto final
        /// ,3 si esta cerca ó en el punto de control 1, 4 si esta cerca ó en el punto de control 2 y 5 si
        /// esta en cualquier otra parte de la curva, de lo contrario regresa 0.</returns>
        /// 
        public override int  PointInFig(Point pt, int range)
        {
            int ret = 0;
            if (pt.X >= _pi.X - range - 5 && pt.X <= _pi.X + range + 5 && pt.Y >= _pi.Y - range - 5 && pt.Y <= _pi.Y + range + 5)
                ret = 1;
            else
                if (pt.X >= _pf.X - range - 5 && pt.X <= _pf.X + range + 5 && pt.Y >= _pf.Y - range - 5 && pt.Y <= _pf.Y + range + 5)
                    ret = 2;
                else
                    if (pt.X >= _control_1.X - range - 5 && pt.X <= _control_1.X + range + 5 && pt.Y >= _control_1.Y - range - 5 && pt.Y <= _control_1.Y + range + 5)
                    ret = 3;
                    else
                        if (pt.X >= _control_2.X - range - 5 && pt.X <= _control_2.X + range + 5 && pt.Y >= _control_2.Y - range - 5 && pt.Y <= _control_2.Y + range + 5)
                            ret = 4;
                        else
                        {

                            foreach (Point pix in _listPixeles)
                            {
                                if (pt.X >= pix.X - range && pt.X <= pix.X + range && pt.Y >= pix.Y - range && pt.Y <= pix.Y + range)
                                {
                                    ret = 5;
                                    break;
                                }
                            }
                        }

            return ret;
        }
         /// <summary>
        /// Borra la lista de pixeles que componen la Curva.
        /// </summary>
        private void ClearPixeles()
        {
            _listPixeles.RemoveRange(0, _listPixeles.Count);
        }
    }
}
