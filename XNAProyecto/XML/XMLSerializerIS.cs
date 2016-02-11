/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using System;
namespace XNAProyecto.Recursos
{
    public class XMLSerializerIS
    {
        private XmlSerializer xmlSerializer=null;
        private Dictionary<string, string> dicc = new Dictionary<string, string>();
        public XmlSerializer _xmlSerializer
        {
            get { return xmlSerializer; }
            set { xmlSerializer = value; }
        }
        public XMLSerializerIS(Type type) {
            this.xmlSerializer = new XmlSerializer(type);    
        }
        /// <summary>
        /// Guarda en un xml serializer los datos, para que funcione hay que inicializar el xmlSerializer.
        /// </summary>
        /// <param name="nombreArchivoXMLSerializer"></param>
        /// <param name="rutaArchivoXMLSerializer"></param>
        /// <param name="o"></param>
        public void GuardarXMLSerialize(string nombreArchivoXMLSerializer,string rutaArchivoXMLSerializer,object o) {
            IsolatedStorageFileStream ISFstream=null;
            XmlWriter xmlWriter = null;
            try
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;//usamos sangría.
                //Abrimos o creamos el archivo
                ISFstream = IsolatedStorageC.IsolatedStorage.OpenFile(rutaArchivoXMLSerializer + "/" + nombreArchivoXMLSerializer, FileMode.Create);
                xmlWriter = XmlWriter.Create(ISFstream, xmlWriterSettings);

                if ((xmlSerializer != null) && (o != null))
                {
                    xmlSerializer.Serialize(xmlWriter, o);
                    System.Diagnostics.Debug.WriteLine("XML GUARDADO" + nombreArchivoXMLSerializer);
                }
                else
                    System.Diagnostics.Debug.WriteLine("Inicializa el '_xmlSerializer' con el tipo de dato(colección genérica)");

            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (ObjectDisposedException ex)
            {
                System.Diagnostics.Debug.WriteLine("Inicialize el almacenamiento aislado estático 'IsolatedStorage'");
                throw ex;
            }
            catch (IsolatedStorageException ex)
            {
                System.Diagnostics.Debug.WriteLine("El archivo ya existe" + nombreArchivoXMLSerializer);
                throw ex;
            }
            catch (InvalidOperationException ex) {
                System.Diagnostics.Debug.WriteLine("Tipo de dato no correcto" + nombreArchivoXMLSerializer);
                throw ex;
            }
            finally {
                if (ISFstream != null)
                {
                    ISFstream.Flush();
                    ISFstream.Close();
                }
                if (xmlWriter != null) {
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
         
            }
        }
        public object CargarXMLSerialize(string nombreArchivoXMLSerializer, string rutaArchivoXMLSerializer)
        {
             IsolatedStorageFileStream ISFstream=null;
            try
            {

                ISFstream = IsolatedStorageC.IsolatedStorage.OpenFile(rutaArchivoXMLSerializer + "/" + nombreArchivoXMLSerializer, FileMode.Open);
                if (xmlSerializer != null)
                    return xmlSerializer.Deserialize(ISFstream);
                else
                {
                    System.Diagnostics.Debug.WriteLine("Inicailiza el '_xmlSerializer' con el tipo de dato(colección genérica)");
                    return null;
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (ObjectDisposedException ex)
            {
                System.Diagnostics.Debug.WriteLine("Inicialize el almacenamiento aislado estático 'IsolatedStorage'");
                throw ex;
            }
            catch (IsolatedStorageException ex)
            {
                System.Diagnostics.Debug.WriteLine("El archivo no existe" + nombreArchivoXMLSerializer);
                throw ex;
            }
            finally
            {
                if (ISFstream != null)
                {
                    ISFstream.Flush();
                    ISFstream.Close();
                }
               

            }
        }




    }
}
