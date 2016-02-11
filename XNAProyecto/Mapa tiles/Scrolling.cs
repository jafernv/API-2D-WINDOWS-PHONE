/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISE�O Y ESPICIFICACI�N DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERN�NDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework;

namespace XNAProyecto.Tiles
{
    public static class Scrolling
    {
        //posici�n del punto de vista.
        private static Vector2 _posicion = Vector2.Zero;
        public static Vector2 Posicion
        {
            get { return _posicion; }
            set{
                _posicion = new Vector2(
                    //restringimos x e y, entre un valor m�ximo(x e y) y 
                    //m�ximo(tama�o del mundo menos anchura o la altura).
                    //con esto evitamos salir de los ejes del mundo.
                    MathHelper.Clamp(value.X,_tama�oMundo.X, _tama�oMundo.Width - PosicionVistaX),
                    MathHelper.Clamp(value.Y, _tama�oMundo.Y,_tama�oMundo.Height - PosicionVistaY));
                
            }
        }
        private static Rectangle _tama�oMundo = new Rectangle(0, 0, 0, 0);
        public static Rectangle Tama�oMundo
        {
            get { return _tama�oMundo; }
            set { _tama�oMundo = value; }
        }
        //vector que representa el n�mero de p�xeles a la derecha y hacia abajo desde
        //la posici�n que  est�n cubiertos por el �rea de visualizaci�n.
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
