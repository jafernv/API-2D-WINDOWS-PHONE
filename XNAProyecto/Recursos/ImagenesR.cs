/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNAProyecto.Recursos
{
    public class ImagenesR : ConjuntoRecursos
    {
        /// <summary>
        /// Constructor de imagenesR.
        /// </summary>
        /// <param name="game"></param>
        public ImagenesR(Game game): base(game) {
        
        }
       
        #region PROPIEDADES Y VARIABLES
        /// <summary>
        /// Instancia de "ImagenesR" usando el patrón Singleton.
        /// </summary>
        private static ImagenesR _imagenesR = null;
        /// <summary>
        /// Devuelve la instancia de  "ImagenesR" usando el patrón Singleton.
        /// </summary>
        public static ImagenesR _Instancia
        {
            get
            {
                return _imagenesR;
            }
        }
        /// <summary>
        /// Ruta de las imágenes.
        /// </summary>
        private static string _rutaImagenesContent = "Imagenes/";
        /// <summary>
        /// Devuelve y permite modificar la ruta de imágenes.Por defecto es : "Imagenes"
        /// </summary>
        public static string _RutaImagenesContent
        {
            get { return ImagenesR._rutaImagenesContent; }
            set { ImagenesR._rutaImagenesContent = value; }
        }
        /// <summary>
        /// Diccionario de imágenes.
        /// </summary>
        protected Dictionary<string, Texture2D> _diccionarioTexturas;
        #endregion
        #region INITIALIZE

        /// <summary>
        /// Inicializa el contenedor de imágenes.
        /// </summary>
        /// <param name="game"></param>
        internal new static void Initialize(Game game)
        {
            _imagenesR = new ImagenesR(game);
            _imagenesR._diccionarioTexturas = new Dictionary<string, Texture2D>();
           
            game.Components.Add(_imagenesR);

        }


        #endregion
        #region  METODOS LOAD


        /// <summary>
        /// Carga la imagen en el contenedor de imágenes ,dándole un alias.
        /// </summary>
        /// <param name="nombreArchivo">Nombre real dela archivo asociado.</param>
        /// <param name="alias">Alias . Es un id único.</param>
        public static void CargarImagen(string nombreArchivo, string alias)
        {
            if (_imagenesR != null)
            try
            {
                if (!_imagenesR._diccionarioTexturas.ContainsKey(alias))
                {
                    Texture2D textura = _imagenesR.Game.Content.Load<Texture2D>(ConjuntoRecursos._RutaNivel + _RutaImagenesContent + nombreArchivo);
                    _imagenesR._diccionarioTexturas.Add(alias, textura);
                  
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Imposible cargar la  textura  con el alias : {0}", alias));
                }

            }
            catch (Microsoft.Xna.Framework.Content.ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine("Excepción encontrada al intentar cargar la textura " + alias + "   \n" + ex.Message);
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
        /// Indexador. Devuelve la instancia de imagen por el alias.
        /// </summary>
        public Texture2D Textura(string alias)
        {
            
                if (_imagenesR._diccionarioTexturas.ContainsKey(alias))
                {
                    return _imagenesR._diccionarioTexturas[alias];
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Imposible cargar la  textura  con el alias  : {0}", alias));
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
                    if (_diccionarioTexturas!=null)
                    foreach (var item in _diccionarioTexturas)
                    {
                        item.Value.Dispose();
                    }
                    _diccionarioTexturas.Clear();
                    _diccionarioTexturas = null;
                    System.Diagnostics.Debug.WriteLine("Llamo a dispose de imágenes");
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        /// <summary>
        /// Libera las imágenes ,no hace falta volver a inicializar.
        /// </summary>

        public static void liberarRecursosImagenes()
        {
            try
            {
                foreach (var item in _imagenesR._diccionarioTexturas)
                {
                    item.Value.Dispose();
                }
                _imagenesR._diccionarioTexturas.Clear();
                System.Diagnostics.Debug.WriteLine("Libero todos los recursos de la biblioteca de imágenes.");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error al liberar los recursos de la biblioteca de imágenes " + e);
                throw e;
            }
        }
        #endregion
    }
}
