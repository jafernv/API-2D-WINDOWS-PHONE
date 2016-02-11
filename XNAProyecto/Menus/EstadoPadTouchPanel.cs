/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace XNAProyecto.Menus
{
    public class EstadoPadTouchPanel
    {


        public  GamePadState ActualGamePadState;//Tecla pulsada
        public  GamePadState AnteriorGamePadStates;//la última tecla pulsada
        public  List<GestureSample> pantallaGesture = new List<GestureSample>();//Pantalla tactil
        public TouchCollection estadoPantallaTactil;
        /// <summary>
        /// Devuelve el estado del pad al controlador de menús.
        /// </summary>
        public EstadoPadTouchPanel()
        {      
            ActualGamePadState = new GamePadState();
            AnteriorGamePadStates = new GamePadState();}

        /// <summary>
        /// Actualiza el pad y la pantalla táctil.
        /// </summary>
        public void Update()
        {
           
                AnteriorGamePadStates = ActualGamePadState;
                ActualGamePadState = GamePad.GetState(PlayerIndex.One);
           
            estadoPantallaTactil = TouchPanel.GetState();
            pantallaGesture.Clear();//limpiamos la lista de la pantalla táctil
            while (TouchPanel.IsGestureAvailable)
            {
                pantallaGesture.Add(TouchPanel.ReadGesture());//lee las pulsaciones de la pantalla
            }

        }

        /// <summary>
        ///Comprueba si se pulsa el pad entre cambios del juego.
        /// </summary>
        public bool BotonPulsado(Buttons boton)//buttons son el pad(enum)
        {
                return (ActualGamePadState.IsButtonDown(boton) &&
                        AnteriorGamePadStates.IsButtonUp(boton));    
        }
    }
}
