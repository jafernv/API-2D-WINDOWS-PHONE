/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISE�O Y ESPICIFICACI�N DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERN�NDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using XNAProyecto.Menus;

namespace XNAProyecto.Jugador
{

    public class MandoVirtual{

        // Posiciones del toque fisico enla pantalla
        private static Vector2 posicion;
        //Posici�n del mando.
        public static Vector2? PosicionMandoVirtual { get; private set; }
        public static Rectangle rectanguloToqueMandoVirtual;
        public  Vector2 MandoVirtualPosicionNormalizado
        {
            get
            {
                // si no hay toque en pantalla,pone el stick en la posici�n cero.
                if (!PosicionMandoVirtual.HasValue)
                    return Vector2.Zero;
                
                // Calcula el vector escalado desde la posici�n detoque hasta el centro
                Vector2 vectorEscalado = (posicion - PosicionMandoVirtual.Value);

                //Normalizamos el vector si la distancia es muy grande.
                if (vectorEscalado.LengthSquared() > 1f)
                    vectorEscalado.Normalize();
      
                return vectorEscalado;
            }
        }
  
        public  void  EstadoPanel(EstadoPadTouchPanel entrada)
        {
           
            TouchLocation? toque = null;
            TouchCollection touchCollection = entrada.estadoPantallaTactil;

            //Examinamos toda la colecci�n de toques y cogemos el �ltimo.
            foreach (TouchLocation touch in touchCollection)   
                    toque = touch;
            
            // Si tiene un valor
            if (toque.HasValue)
            {
                //Si hay toque,el rectangulo que controla la posici�n del stick debe contener la posici�n del toque.
                if (rectanguloToqueMandoVirtual.Contains((int)toque.Value.Position.X, (int)toque.Value.Position.Y))
                {
                    // Miramos si el mando est� en el centro,si no est� entonces nuestra posici�n es el valor del toque.
                    if (!PosicionMandoVirtual.HasValue)
                        PosicionMandoVirtual = toque.Value.Position;
                    // guardamos la posici�n del toque.
                    posicion = toque.Value.Position;
                }
            }
            else
                //No hay toque,pues posicion del mando nula.
                PosicionMandoVirtual = null;
               
            }

    }
}
