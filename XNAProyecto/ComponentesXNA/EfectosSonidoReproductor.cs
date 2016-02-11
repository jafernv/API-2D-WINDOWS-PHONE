/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using XNAProyecto.Recursos;
using Microsoft.Xna.Framework.Audio;
using System;
namespace XNAProyecto.ComponentesXNA
{
    public class EfectosSonidoReproductor
    {
       
        /// <summary>
        /// Reproduce un efecto de sonido.
        /// </summary>
        /// <param name="alias">Alias</param>
        public static void ReproducirEfectoSonido(string alias)
        {
            //Si existe y no está en silencio,tocamos el efecto de sonido.
            if ((MusicaReproductor._silencio==false)&& (EfectosSonidoR._Instancia.EfectoSonido(alias)!=null))
            {
               
                EfectosSonidoR._Instancia.EfectoSonido(alias).Play();
            }
        }
        /// <summary>
        /// Reproduce un efecto de sonido.
        /// </summary>
        /// <param name="alias">Alias</param>
        /// <param name="repetir">Indica si el efecto debe ser repetido. </param>
        public static void ReproducirEfectoSonido(string alias, bool repetir)
        {
            //Si existe y no está en silencio,tocamos el efecto de sonido.
            if ((MusicaReproductor._silencio == false) && (EfectosSonidoR._Instancia.EfectoSonido(alias) != null))
            {
                if (EfectosSonidoR._Instancia.EfectoSonido(alias).IsLooped != repetir)
                {
                    EfectosSonidoR._Instancia.EfectoSonido(alias).IsLooped = repetir;
                }

                EfectosSonidoR._Instancia.EfectoSonido(alias).Play();
            }
        }
        /// <summary>
        /// Reproduce un efecto de sonido.
        /// </summary>
        /// <param name="alias">Alias</param>
        /// <param name="repetir">Indica si el efecto debe ser repetido.</param>
        /// <param name="volumen">Volumen del efecto de sonido.</param>
        public static void ReproducirEfectoSonido(string alias, bool repetir, float volumen)
        {
            //Si existe y no está en silencio,tocamos el efecto de sonido.
            if ((MusicaReproductor._silencio == false) && (EfectosSonidoR._Instancia.EfectoSonido(alias) != null))
            {
                if (EfectosSonidoR._Instancia.EfectoSonido(alias).IsLooped != repetir)
                {
                    EfectosSonidoR._Instancia.EfectoSonido(alias).IsLooped = repetir;
                }

                EfectosSonidoR._Instancia.EfectoSonido(alias).Volume = volumen;
                EfectosSonidoR._Instancia.EfectoSonido(alias).Play();
            }
        }
        
        /// <summary>
        /// Pausa un efecto de sonido.
        /// </summary>
        /// <param name="alias"></param>
        public static void PausarEfectoSonido(string alias)
        {

            if (EfectosSonidoR._Instancia.EfectoSonido(alias) != null   &&
                EfectosSonidoR._Instancia.EfectoSonido(alias).State==SoundState.Playing)
            {
                
                EfectosSonidoR._Instancia.EfectoSonido(alias).Pause();
            }
        }
        /// <summary>
        /// Reanuda un efecto de sonido que estaba en pausa
        /// </summary>
        /// <param name="alias"></param>
        public static void ReanudarEfectoSonido(string alias)
        {

            if (EfectosSonidoR._Instancia.EfectoSonido(alias) != null &&
                EfectosSonidoR._Instancia.EfectoSonido(alias).State == SoundState.Paused)
            {

                EfectosSonidoR._Instancia.EfectoSonido(alias).Resume();
            }
        }
        /// <summary>
        /// Para un efecto de sonido.
        /// </summary>
        /// <param name="alias">Alias.</param>
        public static void PararEfectoSonido(string alias)
        {

            if (EfectosSonidoR._Instancia.EfectoSonido(alias) != null)
            {
                EfectosSonidoR._Instancia.EfectoSonido(alias).Stop();
            }
        }

        /// <summary>
        /// Para todos los efectos de sonido.
        /// </summary>
        public static void PararTodosEfectosSonido()
        {
            if (EfectosSonidoR._Instancia._DiccionarioEfectosSonido != null)
            {
                foreach (SoundEffectInstance sonido in EfectosSonidoR._Instancia._DiccionarioEfectosSonido.Values)
                {
                    if (sonido.State != SoundState.Stopped)

                        sonido.Stop();

                }
            }
        }
        /// <summary>
        /// Define la panorámica para un efecto de sonido.
        /// </summary>
        /// <param name="alias">Alias</param>
        /// <param name="pan">panorámica</param>
        public static void PanEfectoSonido(string alias,float pan) {
            try{
            //Si existe 
            if ( EfectosSonidoR._Instancia.EfectoSonido(alias) != null)
            {
                EfectosSonidoR._Instancia.EfectoSonido(alias).Pan = pan;
            }
             }
            catch (ArgumentOutOfRangeException ex) {
                System.Diagnostics.Debug.WriteLine("El pan tiene que estar entre valores 0 y 1");
                throw ex;
            }
        }
        /// <summary>
        /// Define el ajuste de paso para un efecto de sonido.
        /// </summary>
        /// <param name="alias">Nombre del efecto de sonido</param>
        /// <param name="pitch">ajuste de paso</param>
        public static void PitchEfectoSonido(string alias, float pitch)
        {
            try
            {
                //Si existe 
                if (EfectosSonidoR._Instancia.EfectoSonido(alias) != null)
                {
                    EfectosSonidoR._Instancia.EfectoSonido(alias).Pitch = pitch;
                }
            }
            catch (ArgumentOutOfRangeException ex) {
                System.Diagnostics.Debug.WriteLine("El pitch tiene que estar entre valores 0 y 1");
                throw ex;
            }
        }
        /// <summary>
        ///Pausa o reanuda todos los efectos de sonido.
        /// </summary>
        /// <param name="pausar">True para reanudar todos los efectos de sonido.
        /// False para pausar todos los efectos de sonido.</param>
        public static void PausarReanudarTodosEfectosSonido(bool pausar)
        {
            if (EfectosSonidoR._Instancia._DiccionarioEfectosSonido != null)
            {
                //Escogemos un estado según la opción pausar
                SoundState estado = pausar ? SoundState.Paused : SoundState.Playing;
                //Comparamos el estado con todos los sonidos del  diccionario 
                foreach (SoundEffectInstance sonido in EfectosSonidoR._Instancia._DiccionarioEfectosSonido.Values)
                {
                    //Si coincide,pausa o reanuda el estado del efecto de sonido
                    if (sonido.State == estado)
                    {
                        if (pausar)
                            sonido.Resume();
                        else
                            sonido.Pause();
                    }
                }
            }
        }
        /// <summary>
        /// Permite controlar el volumen de todos  los efectos de sonido mediante un botón deslizante.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CambioVolumenEfectosSonido(object sender, EventArgs e)
        {
            Botones boton = sender as BotonDeslizante;
            float valor = (boton._PosicionProximaOrigen.X - (float)boton._RestriccionesDesplazamiento.Left) /
                (float)boton._RestriccionesDesplazamiento.Width;
                EfectosSonidoR._VolumenEfectosSonido = valor;
           
        }
    }
}
