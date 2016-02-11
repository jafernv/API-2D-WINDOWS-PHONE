/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
#region USING
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
#endregion

namespace XNAProyecto.Recursos
{
    public class EfectosSonidoR : ConjuntoRecursos
    {
        /// <summary>
        /// Constructor de los efectos de sonido.
        /// </summary>
        /// <param name="game"></param>
        private EfectosSonidoR(Game game)
            : base(game) { }


        #region VARIABLES Y PROPIEDADES


        /// <summary>
        /// Instancia de "EfectosSonidoR" usando el patrón Singleton.
        /// </summary>
        static EfectosSonidoR _efectosSonidoR = null;
        /// <summary>
        /// Devuelve la instancia de  "EfectosSonidoR" usando el patrón Singleton.
        /// </summary>
        public static EfectosSonidoR _Instancia
        {
            get
            {
                return _efectosSonidoR;
            }
        }
        /// <summary>
        /// Ruta de los efectos de sonido.
        /// </summary>
        private static string _rutaEfectosSonidoContent = "Efectos Sonido/";
        /// <summary>
        /// Devuelve y permite modificar la ruta de los efectos de sonido.Por defecto es : "Efectos Sonido"
        /// </summary>
        public static string _RutaEfectosSonidoContent
        {
            get { return EfectosSonidoR._rutaEfectosSonidoContent; }
            set { EfectosSonidoR._rutaEfectosSonidoContent = value; }
        }
        /// <summary>
        /// Modifica o obtiene el master volumen de los efectos de sonido. 1.0f es el máximo de volumen.
        /// </summary>
        public static float _VolumenEfectosSonido
        {
            get { return SoundEffect.MasterVolume; }
            set
            {
                SoundEffect.MasterVolume = value;         
            }
        }
        /// <summary>
        /// Diccionario de efectos de sonido.
        /// </summary>
        private Dictionary<string, SoundEffectInstance> _diccionarioEfectosSonido;
        /// <summary>
        ///Permite obtener el diccionario de sonidos.
        /// </summary>
        public Dictionary<string, SoundEffectInstance> _DiccionarioEfectosSonido
        {
            get { return _diccionarioEfectosSonido; }
           
        }

     
     


        #endregion

       #region INITIALIZE

       /// <summary>
        /// Inicializa el contenedor de Efectos de Sonido.
        /// </summary>
        /// <param name="game"></param>
        internal new  static void Initialize(Game game)
        {
            _efectosSonidoR = new EfectosSonidoR(game);
            _efectosSonidoR._diccionarioEfectosSonido = new Dictionary<string, SoundEffectInstance>();
            game.Components.Add(_efectosSonidoR);
        }


        #endregion

        #region  METODOS LOAD


        /// <summary>
        /// Carga un efecto de sonido  en el contenedor de efectos de sonido ,dándole un alias.
        /// </summary>
        /// <param name="nombreArchivo">Nombre real dela archivo asociado.</param>
        /// <param name="alias">Alias . Es un id único.</param>
        public static void CargarEfectoSonido(string nombreArchivo, string alias)
        {
            if (_efectosSonidoR!=null)
            try
            {
                if (!_efectosSonidoR._diccionarioEfectosSonido.ContainsKey(alias))
                {
                    SoundEffect soundEffect = _efectosSonidoR.Game.Content.Load<SoundEffect>(ConjuntoRecursos._RutaNivel+_RutaEfectosSonidoContent + nombreArchivo);
                    SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
                    _efectosSonidoR._diccionarioEfectosSonido.Add(alias, soundEffectInstance);
                }
                else {
                    System.Diagnostics.Debug.WriteLine(string.Format("Imposible cargar el efecto de sonido  con el alias : {0}", alias));
                }

            }
            catch (Microsoft.Xna.Framework.Content.ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine("Excepción encontrada al intentar cargar el efecto de sonido " + alias + "   \n" + ex.Message);
                throw ex;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("Imposible cargar el recurso: " + alias + "   "+  e);
            }
            catch (ObjectDisposedException e)
            {
                throw new Exception("Imposible cargar el recurso,objeto desechado: " + alias + "   " + e);
            }
        }

        #endregion

      


        /// <summary>
        /// Indexador. Devuelve la instancia de sonido por el alias.
        /// </summary>
        public SoundEffectInstance EfectoSonido(string alias)
        {

            if (_efectosSonidoR._diccionarioEfectosSonido.ContainsKey(alias))
            {
                return _efectosSonidoR._diccionarioEfectosSonido[alias];
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Imposible cargar el efecto de sonido  con el alias : {0}", alias));
                return null;
            }
        }

       


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
                    if (_diccionarioEfectosSonido!=null)
                    foreach (var item in _diccionarioEfectosSonido)
                    {
                        item.Value.Dispose();
                    }
                    _diccionarioEfectosSonido.Clear();
                    _diccionarioEfectosSonido = null;
                    System.Diagnostics.Debug.WriteLine("Llamo a dispose de efectos de sonido");
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        /// <summary>
        /// Libera los efectos de sonido,no hace falta volver a inicializar.
        /// </summary>
       public static void liberarRecursosEfectosSonido(){
           try
           {
               foreach (var item in _efectosSonidoR._diccionarioEfectosSonido)
               {
                   item.Value.Dispose();
               }
               _efectosSonidoR._diccionarioEfectosSonido.Clear();
               System.Diagnostics.Debug.WriteLine("Libero todos los recursos de la biblioteca de canciones.");
           }
           catch (Exception e){
               System.Diagnostics.Debug.WriteLine("Error al liberar los recursos de la biblioteca de canciones  "+ e);
               throw e;
           }
       }
        #endregion
    }
}
