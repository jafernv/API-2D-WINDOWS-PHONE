/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
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
            // pantalla táctil activamos las pulsaciones para su estado.
            GestureType = GestureType.Tap;
            this.titulo = nombre;
        }

        /// <summary>
        /// Controla la salida con el botón "<-",usado solo por el gestor de pantallas.
        /// </summary>
        public override void BackBoton(EstadoPadTouchPanel input)
        {  
            if (input.BotonPulsado(Buttons.Back))
            {
                SalirMenu();
            }  
        }

        /// <summary>
        /// Salir del menú.
        /// </summary>
        protected virtual void SalirMenu()
        {
            EliminarPantalla();
        }
     
    }
}
