/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework;

namespace XNAProyecto.Tiles
{
    public static class Scrolling
    {
        //posición del punto de vista.
        private static Vector2 _posicion = Vector2.Zero;
        public static Vector2 Posicion
        {
            get { return _posicion; }
            set{
                _posicion = new Vector2(
                    //restringimos x e y, entre un valor máximo(x e y) y 
                    //máximo(tamaño del mundo menos anchura o la altura).
                    //con esto evitamos salir de los ejes del mundo.
                    MathHelper.Clamp(value.X,_tamañoMundo.X, _tamañoMundo.Width - PosicionVistaX),
                    MathHelper.Clamp(value.Y, _tamañoMundo.Y,_tamañoMundo.Height - PosicionVistaY));
                
            }
        }
        private static Rectangle _tamañoMundo = new Rectangle(0, 0, 0, 0);
        public static Rectangle TamañoMundo
        {
            get { return _tamañoMundo; }
            set { _tamañoMundo = value; }
        }
        //vector que representa el número de píxeles a la derecha y hacia abajo desde
        //la posición que  están cubiertos por el área de visualización.
        private static Vector2 _posicionVista = Vector2.Zero;
        public static float PosicionVistaX
        {
            get { return _posicionVista.X; } 
            set { _posicionVista.X = value; }
        }
        public static float PosicionVistaY
        {
            get { return _posicionVista.Y; }
            set { _posicionVista.Y = value; }
        }
       
      

        #region Public Methods
        public static void CambiarPosicionScroll(Vector2 posicion)
        {
            Posicion += posicion;
        }

      

        public static Vector2 Transform(Vector2 situacion)
        {
            return situacion - _posicion;
        }
         
        public static Rectangle Transform(Rectangle rectangulo)
        {
            return new Rectangle(
                rectangulo.Left - (int)_posicion.X,
                rectangulo.Top - (int)_posicion.Y,
                rectangulo.Width,
                rectangulo.Height);
        }
        #endregion

    }
}
