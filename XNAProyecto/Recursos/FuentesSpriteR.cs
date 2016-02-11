/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAProyecto.Recursos
{
    public class FuentesSpriteR : ConjuntoRecursos
    {
        /// <summary>
        /// Constructor de las fuentes de sprite.
        /// </summary>
        /// <param name="game"></param>
        public FuentesSpriteR(Game game) : base( game) { }

        #region PROPIEDADES Y VARIABLES
        /// <summary>
        /// Instancia de "FuentesSpriteR" usando el patrón Singleton.
        /// </summary>
        private static FuentesSpriteR _fuentesR = null;
        /// <summary>
        /// Devuekve la instancia de  "FuentesSpriteR" usando el patrón Singleton.
        /// </summary>
        public static FuentesSpriteR _Instancia
        {
            get
            {
                return _fuentesR;
            }
        }
        /// <summary>
        /// Ruta de las fuentes de sprite.
        /// </summary>
        private static string _rutaFuentesContent = "Fuentes/";
        /// <summary>
        /// Devuelve y permite modificar la ruta de las fuentes de sprites.Por defecto es : "Fuentes"
        /// </summary>
        public static string _RutaFuentesContent
        {
            get { return FuentesSpriteR._rutaFuentesContent; }
            set { FuentesSpriteR._rutaFuentesContent = value; }
        }
        /// <summary>
        /// Diccionario de las fuenes de sprite.
        /// </summary>
        protected Dictionary<string, SpriteFont> _diccionarioFuentes;
        #endregion
        #region INITIALIZE

        /// <summary>
        /// Inicializa el contenedor de imágenes.
        /// </summary>
        /// <param name="game"></param>
        internal new static void Initialize(Game game)
        {
            _fuentesR = new FuentesSpriteR(game);
            _fuentesR._diccionarioFuentes = new Dictionary<string, SpriteFont>();

            game.Components.Add(_fuentesR);

        }


        #endregion
        #region  METODOS LOAD


        /// <summary>
        /// Carga la fuente en el contenedor de fuentes ,dándole un alias.
        /// </summary>
        /// <param name="nombreArchivo">Nombre real dela archivo asociado.</param>
        /// <param name="alias">Alias . Es un id único.</param>
        public static void CargarFuente(string nombreArchivo, string alias)
        {
            if (_fuentesR != null)
            try
            {
                if (!_fuentesR._diccionarioFuentes.ContainsKey(alias))
                {
                    SpriteFont sprite = _fuentesR.Game.Content.Load<SpriteFont>(ConjuntoRecursos._RutaNivel + _RutaFuentesContent + nombreArchivo);
                    _fuentesR._diccionarioFuentes.Add(alias, sprite);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Imposible cargar la  fuente  con el alias : {0}", alias));
                }

            }
            catch (Microsoft.Xna.Framework.Content.ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine("Excepción encontrada al intentar cargar la fuente " + alias + "   \n" + ex.Message);
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

        /// <summary>
        /// Indexador. Devuelve la instancia de la fuente por el alias.
        /// </summary>
        public SpriteFont Fuente(string alias)
        {

            if (_fuentesR._diccionarioFuentes.ContainsKey(alias))
            {

                return _fuentesR._diccionarioFuentes[alias];

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Imposible cargar la  fuente  con el alias : {0}", alias));
                return null;
            }   
        }
    }

}
