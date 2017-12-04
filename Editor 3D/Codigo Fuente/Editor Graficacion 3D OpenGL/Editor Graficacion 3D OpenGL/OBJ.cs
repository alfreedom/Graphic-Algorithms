using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Editor_Graficacion_3D_OpenGL
{
    class OBJ
    {
        /// <summary>
        /// Variable para guardar la lista de vertices del archivo *.obj.
        /// </summary>
        private List<Vertice> list_vertices;
        /// <summary>
        /// Variable para guardar la lista de poligonos del archivo *.obj.
        /// </summary>
        private List<Poligono> list_poligonos;
        private int contador,contador_v,contador_p;
        private String _texto,_texto_v, _texto_p;
        /// <summary>
        /// Bandera para saber si el archivo se abrio correctamente.
        /// </summary>
        private Boolean _open;

        /// <summary>
        /// Constructor de la clase OBJ.
        /// </summary>
        public OBJ()
        {
            contador = contador_v = 1;
            list_poligonos = new List<Poligono>();
            list_vertices = new List<Vertice>();
        }
        /// <summary>
        /// Abre el archivo con el nombre especificado por "NombreOBJ".
        /// </summary>
        /// <param name="NombreOBJ">Nombre del archivo *.obj que se desea abrir.</param>
        /// <returns>Regresa TRUE si el archivo se abrio correctamente, de lo controario regresa FALSE.</returns>
        public Boolean Abrir(String NombreOBJ)
        {
            FileStream stream = new FileStream(NombreOBJ, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            Boolean ret = false;
            if (stream != null)
            {
                _texto = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                stream = new FileStream(NombreOBJ, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(stream);
               
                list_vertices.Clear();
                list_poligonos.Clear();
                contador_v = 1;
                contador_p = 1;

                while (!reader.EndOfStream)
                    CodificaLinea(reader.ReadLine());

                ret = true;
                _open = true;
            }
            
            String cad = "";
            foreach (Vertice v in this.list_vertices)
            {
                cad += v.ToString();
                cad += "\r\n";
            }
            _texto_v = cad;
            cad = "";
            int i = 1;
            foreach (Poligono p in this.list_poligonos)
            {

                cad += p.ToString();
                cad += "\r\n";
            }
            _texto_p = cad;
            
            reader.Close();
            stream.Close();
                return ret;
        
        }

        /// <summary>
        /// Separa una linea de texto del archivo *.obj y la agrega a su lista 
        /// correspondiente, ya sea un Vertice o un Poligono.
        /// </summary>
        /// <param name="linea">Liea del archivo *.obj a codificar.</param>
        private void CodificaLinea(String linea)
        {
            
            if (linea != "")
            {
                linea += "\r";
                if (linea[0] == 'v' && linea[1] == ' ')
                {
                    Vertice v = LeeVertice(linea);
                    list_vertices.Add(v);
                }
                else
                    if (linea[0] == 'f' && linea[1] == ' ')
                    {
                        Poligono p = LeePoligono(linea);
                        list_poligonos.Add(p);
                    }
            }

        }

        /// <summary>
        /// Trata la "linea" de texto como si fuera un Vertice, y lee sus
        /// valores de X, Y y Z.
        /// </summary>
        /// <param name="linea">Linea de texto del archivo *.obj</param>
        /// <returns>Regresa el Vertice de la linea de texto con sus componentes X, Y y Z.</returns>
        private Vertice LeeVertice(String linea)
        {
            Vertice v = new Vertice(contador_v);

            int i = 1;
            int numdato = 0;
            string leido = "";

            while (linea[i] == ' ')
                i++;
            while (linea[i] != '\r')
            {
                if (linea[i] == '-' || linea[i] == '.' || linea[i] == 'e' || linea[i] == 'E' || char.IsNumber(linea[i]))
                    leido += linea[i++];

                if (linea[i] == ' ' || linea[i] == '\r')
                {
                    switch (numdato)
                    {
                        case 0:
                            v.X = double.Parse(leido);
                            break;
                        case 1:
                            v.Y = double.Parse(leido);
                            break;
                        case 2:
                            v.Z = double.Parse(leido);
                            break;

                    }
                    numdato++;
                    if (linea[i] != '\r')
                        i++;
                    leido = "";
                }

            }

            contador_v++;
            return v;
        }

        /// <summary>
        /// Trata la "linea" de texto como si fuera un Poligono, y lee los
        /// Vertices que lo componen.
        /// </summary>
        /// <param name="linea">Linea de texto del archivo *.obj</param>
        /// <returns>Regresa el Poligono con sus Vertices.</returns>
        private Poligono LeePoligono(String linea)
        {
            
            Poligono p = new Poligono(contador_p++);
            int i = 2;
            Boolean insertado = false;
            string leido = "";

            while (linea[i] != '\r')
            {
                if (char.IsNumber(linea[i]) && !insertado)
                    leido += linea[i];

                i++;
                if ((linea[i] == '/' || linea[i] == '\r') && !insertado)
                {
                    int numv = int.Parse(leido);
                    p.AgregaVertice(list_vertices[numv - 1]);

                    if (linea[i] != '\r')
                        i++;
                    leido = "";
                    insertado = true;
                }
                if (linea[i] == ' ' && linea[i + 1] != '\r')
                    insertado = false;
            }
            return p;
        }

        /// <summary>
        /// Obtiene La lista de vertices del Archivo *.obj previamente abierto.
        /// </summary>
        public List<Vertice> Vertices
        {
            get { return list_vertices; }
        }
        /// <summary>
        /// Obtiene La lista de poligonos del Archivo *.obj previamente abierto.
        /// </summary>
        public List<Poligono> Poligonos
        {
            get { return list_poligonos; }
        }

        /// <summary>
        /// Obtiene el contenido completo del archivo *.obj previamente abierto.
        /// </summary>
        public String Texto
        { 
            get { return _texto; }        
        }

        /// <summary>
        /// Obtiene la lista de Poligonos en texto con formato.
        /// </summary>
        public String TextoPoligonos
        {
            get {
                String cad = "";
                int i = 1;
                foreach (Poligono p in this.list_poligonos)
                {

                    cad += p.ToString();
                    cad += "\r\n";
                }
                _texto_p = cad; 
                return _texto_p;
            }
        }
        /// <summary>
        /// Obtiene la lista de Vertices en texto formateado.
        /// </summary>
        public String TextoVertices
        {
            get {
                String cad = "";
                foreach (Vertice v in this.list_vertices)
                {
                    cad += v.ToString();
                    cad += "\r\n";
                }
                _texto_v = cad; 
                return _texto_v;
            }
        }
    }

    public class Vertice
    {

        /// <summary>
        /// Guarda la coordenada X del Vertice.
        /// </summary>
        private double _x;
        /// <summary>
        /// Guarda la coordenada Y del Vertice.
        /// </summary>
        private double _y;
        /// <summary>
        /// Guarda la coordenada Z del Vertice.
        /// </summary>
        private double _z;
        /// <summary>
        /// Guarda el numero del Vertice.
        /// </summary>
        private int _numvertice;

        /// <summary>
        /// Constructor de la clase Vertice.
        /// </summary>
        /// <param name="Numero">Numero del Vertice.</param>
        public Vertice(int Numero)
            : this(0, 0, 0, Numero)
        {

        }
        /// <summary>
        /// Constructor de la clase Vertice.
        /// </summary>
        /// <param name="X">Coordenada en X del Vertice.</param>
        /// <param name="Y">Coordenada en Y del Vertice.</param>
        /// <param name="Z">Coordenada en Z del Vertice.</param>
        /// <param name="Numero">Numero del Vertice.</param>
        public Vertice(double X, double Y, double Z, int Numero)
        {
            _numvertice = Numero;
            this._x = X;
            this._y = Y;
            this._z = Z;
        }

        /// <summary>
        /// Obtiene o establece la coordenada X del Vertice.
        /// </summary>
        public double X
        {
            get { return _x; }
            set { _x = value; }
        }
        /// <summary>
        /// Obtiene o establece la coordenada Y del Vertice.
        /// </summary>
        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }
        /// <summary>
        /// Obtiene o establece la coordenada Z del Vertice.
        /// </summary>
        public double Z
        {
            get { return _z; }
            set { _z = value; }
        }

        /// <summary>
        /// Convierte el numero y las coordenadas X, Y y Z del vertice en una 
        /// linea de texto formateada.
        /// </summary>
        /// <returns>Devuelve una cadena de texto formateada con las coordenadas y el numero del Vertice.</returns>
        public override String ToString()
        {
            String s = "V-" + _numvertice + " = " + _x + "  " + _y + "  " + _z;
            return s;
        }

        /// <summary>
        /// Obtiene el numero del Vertice.
        /// </summary>
        public int Numero
        {
            get { return _numvertice; }
        }
    }

    public class Poligono
    {
        /// <summary>
        /// Guarda la lista de vertices que comforman el Poligono.
        /// </summary>
        private List<Vertice> _vertices;
        /// <summary>
        /// Guarda el numero del Poligono.
        /// </summary>
        private int num_p;

        /// <summary>
        /// Constructor de la clase Poligono.
        /// </summary>
        /// <param name="Numero">Numero del Poligono.</param>
        public Poligono(int Numero)
        {
            num_p = Numero;
            _vertices = new List<Vertice>();
        }

        /// <summary>
        /// Agrega el Vertice 'v' a la lista de poligonos.
        /// </summary>
        /// <param name="v">Vertice que se agregará al Poligono.</param>
        public void AgregaVertice(Vertice v)
        {
            _vertices.Add(v);
        }

        /// <summary>
        /// Obtiene la lista de Vertices que comforman el poligono.
        /// </summary>
        public List<Vertice> Vertices
        {
            get { return _vertices; }
        }
        /// <summary>
        /// Obtiene le numero del Poligono.
        /// </summary>
        public int Numero
        {
            get { return num_p; }
        }
        /// <summary>
        /// Convierte el numero del Poligono y el numero de los Vertices del Poligono en una 
        /// linea de texto formateada.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String s = "P-"+num_p+" = ";

            for (int i = 0; i < _vertices.Count; i++)
            {
                s += _vertices[i].Numero;
                if (i >= 0 && i < _vertices.Count - 1)
                    s += "/";
            }
            return s;
        }
    }

    

}
