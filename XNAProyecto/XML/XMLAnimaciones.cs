/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System.Xml.Linq;
using System.Xml;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using XNAProyecto.ComponentesXNA;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace XNAProyecto.Recursos
{
    /// <summary>
    /// Carga las animaciones definidas en un xml,devolviendo para ello un diccionario de animaciones.
    /// </summary>
    public class XMLAnimaciones
    {
        #region VARIABLES
        private Game _game;
        /// <summary>
        /// Nombre del documento XML.
        /// </summary>
        private XDocument _documento;
        /// <summary>
        /// Nombre con el que separas cada animación dentro del XML.
        /// </summary>
        private XName _nombreSep;//separa cada animación en el XML.

        //RECURSOS INDIVIDUALES
        /// <summary>
        /// Nombre de cada animación del XML.
        /// </summary>
        private string _aliasAnimacion = "Alias";
        public string _AliasAnimacion
        {
            get { return _aliasAnimacion; }
            set { _aliasAnimacion = value; }
        }
        /// <summary>
        /// Nombre que se le da a cada textura dentro del XML.
        /// </summary>
        private string _nombreTextura = "NombreTextura";
        public string _NombreTextura
        {
            get { return _nombreTextura; }
            set { _nombreTextura = value; }
        }

        /// <summary>
        /// Diccionario con todas las animaciones de un XML.
        /// </summary>
        private Dictionary<string, Animacion> _diccAnimaciones;
        public Dictionary<string, Animacion> _DiccAnimaciones
        {
            get { return _diccAnimaciones; }
            set { _diccAnimaciones = value; }
        }

        private string _anchoFrame = "AnchoFrame", _largoFrame = "LargoFrame";
        public string _LargoFrame
        {
            get { return _largoFrame; }
            set { _largoFrame = value; }
        }
        public string _AnchoFrame
        {
            get { return _anchoFrame; }
            set { _anchoFrame = value; }
        }

        private string _nFilas = "NFilas", _nColumnas = "NColumnas";
        public string _NColumnas
        {
            get { return _nColumnas; }
            set { _nColumnas = value; }
        }
        public string _NFilas
        {
            get { return _nFilas; }
            set { _nFilas = value; }
        }

        /// <summary>
        /// Velocidad de los frames.//en el xml es un double.
        /// </summary>
        private string _velocidadFrame = "Velocidad";
        public string _VelocidadFrame
        {
            get { return _velocidadFrame; }
            set { _velocidadFrame = value; }
        }
 
        #endregion

        //ej de ruta-->Animaciones/Animaciones.xml
        public XMLAnimaciones(Game game, string rutaDocumento, string nombreSeparacion)
        {

            try
            {
                this._documento = XDocument.Load(game.Content.RootDirectory + "/" + rutaDocumento);
                this._nombreSep = nombreSeparacion;
                this._DiccAnimaciones = new Dictionary<string, Animacion>();
                this._game = game;
            }
            catch (XmlException ex)
            {
                System.Diagnostics.Debug.WriteLine("La ruta de la animación no es correcta: " + rutaDocumento + "  " + ex);
                throw ex;
            }

        }
        public void cargarAnimacion()
        {
            var datos = _documento.Document.Descendants(_nombreSep);
            
            foreach (var datosAnimacion in datos)
            {
                // Nombre de la animacion,para añadirla al final al diccionario
                string nombreAnimacion = datosAnimacion.Attribute(_AliasAnimacion).Value;
             
                //Añadimos la textura,está debe ser cargada por el usuario.
                try{
                Texture2D textura =  ImagenesR._Instancia.Textura(datosAnimacion.Attribute(_nombreTextura).Value);          
                //Dimension de la imagen
                Point dimensionImagen = new Point();
                dimensionImagen.X = int.Parse(datosAnimacion.Attribute(_NColumnas).Value);
                dimensionImagen.Y = int.Parse(datosAnimacion.Attribute(_NFilas).Value);
                //Obtenemos el tamaño del frame(ancho y alto). 
                Point dimensionFrame = new Point();
                dimensionFrame.X = int.Parse(datosAnimacion.Attribute(_AnchoFrame).Value);
                dimensionFrame.Y = int.Parse(datosAnimacion.Attribute(_LargoFrame).Value);

                Animacion animacion = new Animacion(_game,_AliasAnimacion, textura, dimensionFrame, dimensionImagen);

                //ATRIBUTOS OPCIONALES
                if (datosAnimacion.Attribute(_VelocidadFrame) != null)
                {      
                     animacion.CambioEntreIntervalos(TimeSpan.FromMilliseconds(
                   double.Parse(datosAnimacion.Attribute(_VelocidadFrame).Value)));
                }
                    _DiccAnimaciones.Add(nombreAnimacion, animacion);
                  
                }
                catch(NullReferenceException ex){
                    System.Diagnostics.Debug.WriteLine("Valor nulo en la animación : " + datosAnimacion.Attribute(_AliasAnimacion).Value + "no existe");
                throw(ex);
                }
                catch (FormatException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Los valores deben ser enteros para la longitud,anchura, filas y columnas");
                    throw (ex);
                }
            }
        }//cargarAnimacion
    }
}

