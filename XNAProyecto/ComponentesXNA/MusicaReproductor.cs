/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISE�O Y ESPICIFICACI�N DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERN�NDEZ VILLANUEVA
 */
using System;
using XNAProyecto.Recursos;
using Microsoft.Xna.Framework.Media;
namespace XNAProyecto.ComponentesXNA
{
    public class MusicaReproductor
    {
        /// <summary>
        /// Indica si un juego est� en silencio.
        /// </summary>
        internal static bool _silencio = false;

        /// <summary>
        /// Reproduce una canci�n seg�n el alias de la canci�n.
        /// </summary>
        /// <param name="alias">Alias de la canci�n</param>
        public static void ReproducirCancion(string alias)
        {
           if(_silencio==false){
               if (CancionesR._Instancia.Cancion(alias) != null)
               {
                   
                       // Paramos la antigua canci�n, si est� en ejecuci�n.
                       if (MediaPlayer.State != MediaState.Stopped)              
                           MediaPlayer.Stop();

                       //Repetimos la canci�n.
                       MediaPlayer.IsRepeating = true;
                   //Reproduce la canci�n.
                       MediaPlayer.Play(CancionesR._Instancia.Cancion(alias));
               } 
   
            }
        }


        /// <summary>
        /// Reproduce una canci�n seg�n el alias de la canci�n.
        /// </summary>
        /// <param name="alias">Alias de la canci�n</param>
       /// <param name="volumen">Volumen de la canci�n</param>
        public static void ReproducirCancion(string alias,float volumen)
        {
            if (_silencio == false)
            {
                if (CancionesR._Instancia.Cancion(alias) != null)
                {

                    // Paramos la antigua canci�n, si est� en ejecuci�n.
                    if (MediaPlayer.State != MediaState.Stopped)
                        MediaPlayer.Stop();

                    //Repetimos la canci�n.
                    MediaPlayer.IsRepeating = true;
                    //Volumen de la canci�n.
                    MediaPlayer.Volume = volumen;
                    //Reproduce la canci�n.
                    MediaPlayer.Play(CancionesR._Instancia.Cancion(alias));
                }

            }
        }
        /// <summary>
        /// Para la canci�n.
        /// </summary>
        public static void PararCancion()
        {

            // Paramos la canci�n, si no est� en ejecuci�n.
            if (MediaPlayer.State != MediaState.Stopped)
                MediaPlayer.Stop();

        }
        /// <summary>
        /// Pausa la canci�n.
        /// </summary>
        public static void PausarCancion() {

            // Pausamos la  canci�n, si est� en ejecuci�n.
            if (MediaPlayer.State == MediaState.Playing) 
                MediaPlayer.Pause();                                 
        }
        /// <summary>
        /// Reanuda la canci�n.
        /// </summary>
        public static void ReanudarCancion() {
            // Reanudamos la  canci�n, si est� en pausa.
                if (MediaPlayer.State == MediaState.Paused)
                    MediaPlayer.Resume();
        }
        /// <summary>
        /// Evento que silencia o reanuda el MediaPlayer y los efectos de sonido  seg�n el estado .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PonerSilencio(object sender, EventArgs e)
        {
            if (MusicaReproductor._silencio == false)
            {
                //Silenciamos el mediaPlayer y todos los efectos de sonido.
                MusicaReproductor._silencio = true;
                MusicaReproductor.PausarCancion();
                MediaPlayer.IsMuted = true;
                EfectosSonidoReproductor.PausarReanudarTodosEfectosSonido(false);

            }
            else
            {
                //Reanudamos el mediaPlayer y todos los efectos de sonido.
                MusicaReproductor._silencio = false;
                MusicaReproductor.ReanudarCancion();
                MediaPlayer.IsMuted = false;
                EfectosSonidoReproductor.PausarReanudarTodosEfectosSonido(true);
            }
        }
        /// <summary>
        /// Permite controlar el volumen de las canciones mediante un bot�n deslizante.
        /// Usado para eventos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CambioVolumenMusica(object sender, EventArgs e)
        {
            Botones boton = sender as BotonDeslizante;
            //Obtenemos el valor de desplazamiento entre 0 y 1.
            float valor = (boton._PosicionProximaOrigen.X - (float)boton._RestriccionesDesplazamiento.Left) /
                (float)boton._RestriccionesDesplazamiento.Width;
            CancionesR._VolumenMusica = valor;

        }
        /// <summary>
        /// Permite controlar el volumen general mediante un bot�n deslizante.
        /// Usado en eventos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CambioVolumenGeneral(object sender, EventArgs e)
        {
            Botones boton = sender as BotonDeslizante;
            //Obtenemos el valor de desplazamiento entre 0 y 1.
            float valor = (boton._PosicionProximaOrigen.X - (float)boton._RestriccionesDesplazamiento.Left) /
                (float)boton._RestriccionesDesplazamiento.Width;
            CancionesR._VolumenGeneral = valor;

        }
    }

}