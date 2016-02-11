/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System.Xml.Linq;
using System.Xml;
using Microsoft.Xna.Framework;
using System;

namespace XNAProyecto.Recursos
{
    public  class XMLDatos
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
        /// Nombre de cada dato del XML(alias).
        /// </summary>
        private string _alias = "Alias";
        public string _Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }
        /// <summary>
        /// Nombre real del dato.(el de la ruta)
        /// </summary>
        private string _ruta = "Ruta";
        public string _Ruta
        {
            get { return _ruta; }
            set { _ruta = value; }
        }
       
     
        private string _tipoDato="Tipo";

        public string _TipoDato
        {
            get { return _tipoDato; }
            set { _tipoDato = value; }
        }
        #endregion
          public XMLDatos(Game game, string rutaDocumento, string nombreSeparacion)
        {

            try
            {
                this._documento = XDocument.Load(game.Content.RootDirectory + "/" + rutaDocumento);
                this._nombreSep = nombreSeparacion;
               
                this._game = game;  
            }
            catch (XmlException ex)
            {
                System.Diagnostics.Debug.WriteLine("La ruta  no es correcta: " + rutaDocumento + "  " + ex);
                throw ex;
            }

        }

          public  void CargarDatos() {
              var datos = _documento.Document.Descendants(_nombreSep);

              foreach (var datosDados in datos)
              {
                  try
                  {
                      string nombre = datosDados.Attribute(_Alias).Value;
                      string datoRuta = datosDados.Attribute(_Ruta).Value;   
                      if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(datoRuta)){
                          string tipoDato = datosDados.Attribute(_TipoDato).Value.ToLower();
                          switch (tipoDato)
                          {

                              case "cancion":
                                  CancionesR.CargarCancion(datoRuta, nombre);
                                  break;
                              case "fuente":
                                  FuentesSpriteR.CargarFuente(datoRuta, nombre);
                                  break;
                              case "imagen":
                                  ImagenesR.CargarImagen(datoRuta, nombre);
                                  break;
                              case "efecto sonido":
                                  goto case "efectosonido";
                              case "efectosonido":
                                  EfectosSonidoR.CargarEfectoSonido(datoRuta, nombre);
                                  break;
                              default:
                                  System.Diagnostics.Debug.WriteLine("El tipo de dato no existe: " + tipoDato);
                                  break;

                          }//switch
                       }//if
                         else
                         System.Diagnostics.Debug.WriteLine("El nombre o ruta del XML de datos no puede ser nula o vacía");  
                
                    }
                  catch (NullReferenceException ex)
                  {
                      System.Diagnostics.Debug.WriteLine("Valor nulo en la animación : " + datosDados.Attribute(_Alias).Value + "no existe");
                      throw (ex);
                  }
                  catch (FormatException ex)
                  {
                      System.Diagnostics.Debug.WriteLine("Los valores deben ser enteros para la longitud,anchura, filas y columnas");
                      throw (ex);
                  }
              }
          }
    
    }
}
