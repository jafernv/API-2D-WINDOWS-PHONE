/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework;
namespace XNAProyecto.Tiles
{
    public class Colisiones{

        private static float _espacioScrolling = 3f;
        public static float EspacioScrolling
        {
            get { return _espacioScrolling; }
            set { _espacioScrolling = value; }
        }

        public static void ColisionarConBordes(){
            //Posiciones x e y del jugador.
            float x = Jugador.Jugador._posicion.X;
            float y = Jugador.Jugador._posicion.Y;
            x = MathHelper.Clamp(x, 0, Scrolling.TamañoMundo.Right - Jugador.Jugador._textura.Width);
           //coge el valor x entre 0 y el extremo superior derecho del mundo menos el ancho del jugador.
            y = MathHelper.Clamp( y,0,Scrolling.TamañoMundo.Bottom - Jugador.Jugador._textura.Height);
            //coge el valor y entre 0 y el extremo inferior derecho del mundo menos el largo del jugador.
            Jugador.Jugador._posicion = new Vector2(x, y); //devuelv ela nueva posición.
           
        }
        public  static void scrollingMapaMover(GameTime gameTime, Vector2 posicionMando){
            float tiempoJuego = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Con la posición del mando, dada por el Vector2 de la acción vemos el sentido de movimiento.
            //IZQUIERDA
            if ((posicionMando.X < 0)&&(Jugador.Jugador.RectanguloJugadorTransform.X < Jugador.Jugador._scrollJugador.X)){
                Scrolling.CambiarPosicionScroll((new Vector2(posicionMando.X, 0) * Jugador.Jugador._velocidadJugador * tiempoJuego) * EspacioScrolling);
            }
            //DERECHA
            if ((posicionMando.X > 0)&&(Jugador.Jugador.RectanguloJugadorTransform.Right > Jugador.Jugador._scrollJugador.Right)){
                Scrolling.CambiarPosicionScroll((new Vector2(posicionMando.X, 0) * Jugador.Jugador._velocidadJugador * tiempoJuego) * EspacioScrolling);
            }
            //ARRIBA
            if ((posicionMando.Y < 0)&&(Jugador.Jugador.RectanguloJugadorTransform.Y < Jugador.Jugador._scrollJugador.Y)){
                Scrolling.CambiarPosicionScroll((new Vector2(0, posicionMando.Y) * Jugador.Jugador._velocidadJugador * tiempoJuego) * EspacioScrolling);
            }
            //ABAJO
            if ((posicionMando.Y > 0)&&(Jugador.Jugador.RectanguloJugadorTransform.Bottom > Jugador.Jugador._scrollJugador.Bottom)){
                Scrolling.CambiarPosicionScroll((new Vector2(0, posicionMando.Y) * Jugador.Jugador._velocidadJugador * tiempoJuego) * EspacioScrolling);
            }
        }
        public static Vector2 ColisionesConTiles(GameTime gameTime,Vector2 posicionTemporal,MapaTiles mapa){
            float tiempoJuego = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 posicionHorizontalFutura = (new Vector2(posicionTemporal.X, 0) * Jugador.Jugador._velocidadJugador * tiempoJuego) + Jugador.Jugador._posicion;
            Vector2 posicionVerticalFutura = (new Vector2(0, posicionTemporal.Y) * Jugador.Jugador._velocidadJugador * tiempoJuego)+Jugador.Jugador._posicion;

            Rectangle rectanguloHorizontal = new Rectangle((int)posicionHorizontalFutura.X,(int) Jugador.Jugador._posicion.Y,
                Jugador.Jugador._textura.Width,Jugador.Jugador._textura.Height);

            Rectangle rectanguloVertical = new Rectangle((int)Jugador.Jugador._posicion.X,(int)posicionVerticalFutura.Y,
              Jugador.Jugador._textura.Width, Jugador.Jugador._textura.Height);

            //dependiendo del movimiento miramos el eje x e y (4 posiciones)
            int ejeXNegativo = -1;
            int ejeXPositivo = -1;
            int ejeYNegativo = -1;
            int ejeYPositivo = -1;

            if (posicionTemporal.X < 0){
                 ejeXNegativo = (int)rectanguloHorizontal.Left;
                 ejeXPositivo = (int)Jugador.Jugador.RectanguloJugador.Left;}
            else{
                 ejeXNegativo = (int)Jugador.Jugador.RectanguloJugador.Right;
                 ejeXPositivo = (int)rectanguloHorizontal.Right;}

            if (posicionTemporal.Y < 0){
                 ejeYNegativo = (int)rectanguloVertical.Top;
                 ejeYPositivo = (int)Jugador.Jugador.RectanguloJugador.Top;}
            else{
                 ejeYNegativo = (int)Jugador.Jugador.RectanguloJugador.Bottom;
                 ejeYPositivo = (int)rectanguloVertical.Bottom; }
            //Las colisones entre rectangulo se hacen por pixeles.
                for (int x = ejeXNegativo; x < ejeXPositivo; x++)
                {
                    for (int y = 0; y < Jugador.Jugador._textura.Height; y++)
                    {
                        if (mapa.tileConColision(new Vector2(x, posicionHorizontalFutura.Y + y)))
                            posicionTemporal.X = 0;}}
                
                for (int y = ejeYNegativo; y < ejeYPositivo; y++)
                {
                    for (int x = 0; x < Jugador.Jugador._textura.Width; x++)
                    {
                        if (mapa.tileConColision( new Vector2(posicionVerticalFutura.X + x, y)))
                            posicionTemporal.Y = 0;}}

            return posicionTemporal;
        }
        
    }
}
