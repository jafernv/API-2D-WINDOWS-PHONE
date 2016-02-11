/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using XNAProyecto.Recursos;
using Microsoft.Xna.Framework.Media;
namespace XNAProyecto.ComponentesXNA
{
    public class MusicaReproductor
    {
        /// <summary>
        /// Indica si un juego está en silencio.
        /// </summary>
        internal static bool _silencio = false;

        /// <summary>
        /// Reproduce una canción según el alias de la canción.
        /// </summary>
        /// <param name="alias">Alias de la canción</param>
        public static void ReproducirCancion(string alias)
        {
           if(_silencio==false){
               if (CancionesR._Instancia.Cancion(alias) != null)
               {
                   
                       // Paramos la antigua canción, si está en ejecución.
                       if (MediaPlayer.State != MediaState.Stopped)              
                           MediaPlayer.Stop();

                       //Repetimos la canción.
                       MediaPlayer.IsRepeating = true;
                   //Reproduce la canción.
                       MediaPlayer.Play(CancionesR._Instancia.Cancion(alias));
               } 
   
            }
        }


        /// <summary>
        /// Reproduce una canción según el alias de la canción.
        /// </summary>
        /// <param name="alias">Alias de la canción</param>
       /// <param name="volumen">Volumen de la canción</param>
        public static void ReproducirCancion(string alias,float volumen)
        {
            if (_silencio == false)
            {
                if (CancionesR._Instancia.Cancion(alias) != null)
                {

                    // Paramos la antigua canción, si está en ejecución.
                    if (MediaPlayer.State != MediaState.Stopped)
                        MediaPlayer.Stop();

                    //Repetimos la canción.
                    MediaPlayer.IsRepeating = true;
                    //Volumen de la canción.
                    MediaPlayer.Volume = volumen;
                    //Reproduce la canción.
                    MediaPlayer.Play(CancionesR._Instancia.Cancion(alias));
                }

            }
        }
        /// <summary>
        /// Para la canción.
        /// </summary>
        public static void PararCancion()
        {

            // Paramos la canción, si no está en ejecución.
            if (MediaPlayer.State != MediaState.Stopped)
                MediaPlayer.Stop();

        }
        /// <summary>
        /// Pausa la canción.
        /// </summary>
        public static void PausarCancion() {

            // Pausamos la  canción, si está en ejecución.
            if (MediaPlayer.State == MediaState.Playing) 
                MediaPlayer.Pause();                                 
        }
        /// <summary>
        /// Reanuda la canción.
        /// </summary>
        public static void ReanudarCancion() {
            // Reanudamos la  canción, si está en pausa.
                if (MediaPlayer.State == MediaState.Paused)
                    MediaPlayer.Resume();
        }
        /// <summary>
        /// Evento que silencia o reanuda el MediaPlayer y los efectos de sonido  según el estado .
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
        /// Permite controlar el volumen de las canciones mediante un botón deslizante.
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
        /// Permite controlar el volumen general mediante un botón deslizante.
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