/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISE�O Y ESPICIFICACI�N DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERN�NDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;


namespace XNAProyecto.Menus
{

    public abstract class Pantalla : TiposP
    {
        string titulo;
        
        public Pantalla(string nombre)
        {
            // pantalla t�ctil activamos las pulsaciones para su estado.
            GestureType = GestureType.Tap;
            this.titulo = nombre;
        }

        /// <summary>
        /// Controla la salida con el bot�n "<-",usado solo por el gestor de pantallas.
        /// </summary>
        public override void BackBoton(EstadoPadTouchPanel input)
        {  
            if (input.BotonPulsado(Buttons.Back))
            {
                SalirMenu();
            }  
        }

        /// <summary>
        /// Salir del men�.
        /// </summary>
        protected virtual void SalirMenu()
        {
            EliminarPantalla();
        }
     
    }
}
