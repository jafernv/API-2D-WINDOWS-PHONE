/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Audio;

namespace XNAProyecto.Recursos
{
    public class CancionesR : ConjuntoRecursos
    {
        /// <summary>
        /// Constructor de "CancionesR"
        /// </summary>
        /// <param name="game"></param>
        private CancionesR(Game game)
            : base(game) { }

        #region VARIABLES Y PROPIEDADES


        /// <summary>
        /// Instancia de "CancionesR" usando el patrón Singleton.
        /// </summary>
        private  static CancionesR _cancionesR = null;
        /// <summary>
        /// Devuelve la instancia de  "CancionesR" usando el patrón Singleton.
        /// </summary>
        public static CancionesR _Instancia
        {
            get
            {
                return _cancionesR;
            }
        }
        /// <summary>
        /// Ruta de las canciones.
        /// </summary>
        private static string _rutaMusicaContent = "Canciones/";
        /// <summary>
        /// Devuelve o modifica la ruta de las canciones. Por defecto es "Canciones".
        /// </summary>
        public static string _RutaMusicaContent
        {
            get { return CancionesR._rutaMusicaContent; }
            set { CancionesR._rutaMusicaContent = value; }
        }
        /// <summary>
        /// Modifica o obtiene el volumen de la música. 1.0f es el máximo de volumen.
        /// </summary>
        public static float _VolumenMusica
        {
            get { return MediaPlayer.Volume; }
            set { MediaPlayer.Volume = value;    
            }
        }
        /// <summary>
        /// Modifica o obtiene el volumen de la música y de los efectos de sonido. 1.0f es el máximo de volumen.
        /// </summary>
        public static float _VolumenGeneral
        {
            get { return MediaPlayer.Volume; }
            set
            {
                MediaPlayer.Volume = value;
                SoundEffect.MasterVolume = value; 
            }
        }
        /// <summary>
        /// Diccionario de canciones.
        /// </summary>
      protected Dictionary<string, Song> _diccionarioMusica;

        
        #endregion
        #region INITIALIZE

      /// <summary>
      /// Inicializa el contenedor de canciones.
      /// </summary>
      /// <param name="game"></param>
      internal new static void Initialize(Game game)
      {
          _cancionesR = new CancionesR(game);     
          _cancionesR._diccionarioMusica = new Dictionary<string, Song>();
          game.Components.Add(_cancionesR);

      }


      #endregion
        #region METODOS LOAD
      /// <summary>
      /// Carga una canción en el contenedor de canciones,dándole un alias.
      /// </summary>
      /// <param name="nombreArchivo">Nombre real dela archivo asociado.</param>
      /// <param name="alias">Alias . Es un id único.</param>
      public static void CargarCancion(string nombreArchivo, string alias)
      {
          if (_cancionesR!=null)
          try
          {
              Song song = _cancionesR.Game.Content.Load<Song>(ConjuntoRecursos._RutaNivel+_rutaMusicaContent + nombreArchivo);

              if (!_cancionesR._diccionarioMusica.ContainsKey(alias))
              {
                  _cancionesR._diccionarioMusica.Add(alias, song);
              }
              else
              {
                  System.Diagnostics.Debug.WriteLine(string.Format("Imposible cargar la  canción con el alias : {0}, ya que este existe", alias));
              }
          }
          catch (Microsoft.Xna.Framework.Content.ContentLoadException ex)
          {
              System.Diagnostics.Debug.WriteLine("Excepción encontrada al intentar cargar la canción " + alias + "   \n" + ex.Message);
              throw ex;
          }
          catch (ArgumentNullException e)
          {
              throw new Exception("Imposible cargar el recurso: " + alias + "   " + e);
          }
          catch (ObjectDisposedException e)
          {
              throw new Exception("Imposible cargar el recurso,objeto desechado: " + alias + "   " + e);
          }
      }
        #endregion
        #region METODOS DISPOSAL


      /// <summary>
      /// Limpia el componente al hacer Dispose.
      /// </summary>
      protected override void Dispose(bool disposing)
      {
          try
          {
              if (disposing)
              {
                  if (_diccionarioMusica!=null)
                  foreach (var item in _diccionarioMusica)
                  {
                      item.Value.Dispose();
                  }
                  _diccionarioMusica.Clear();
                  _diccionarioMusica = null;
                  System.Diagnostics.Debug.WriteLine("Llamo a dispose de canciones");
              }
          }
          finally
          {
              base.Dispose(disposing);
          }
      }
       /// <summary>
       /// Libera las canciones ,no hace falta volver a inicializar.
       /// </summary>
 
      public static void liberarRecursosCanciones()
      {
          try
          {
              foreach (var item in _cancionesR._diccionarioMusica)
              {
                  item.Value.Dispose();
              }
              _cancionesR._diccionarioMusica.Clear();
              System.Diagnostics.Debug.WriteLine("Libero todos los recursos de la biblioteca de canciones.");
          }
          catch (Exception e)
          {
              System.Diagnostics.Debug.WriteLine("Error al liberar los recursos de la biblioteca de canciones  " + e);
              throw e;
          }
      }
      #endregion
      /// <summary>
      /// Indexador. Devuelve la instancia de la canción por el alias.
      /// </summary>
      public Song Cancion(string alias)
      {

          if (_cancionesR._diccionarioMusica.ContainsKey(alias))
          {
              return _cancionesR._diccionarioMusica[alias];
          }
          else
          {
              System.Diagnostics.Debug.WriteLine(string.Format("Imposible cargar la canción  con el alias : {0}", alias));
              return null;
          }

      }
    }
}
