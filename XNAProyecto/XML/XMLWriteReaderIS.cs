/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml;

namespace XNAProyecto.Recursos
{
    public class XMLWriteReaderIS
    {
        private IsolatedStorageFileStream _isoStream;
        private XmlWriterSettings _propiedadesW = new XmlWriterSettings();
        private XmlReaderSettings _propiedadesR=new XmlReaderSettings();

        public XmlReaderSettings PropiedadesR
        {
            get { return _propiedadesR; }
            set { _propiedadesR = value; }
        }
        public XmlWriterSettings PropiedadesW
        {
            get { return _propiedadesW; }
            set { _propiedadesW = value; }
        }
        private XmlWriter _xmlW;

        public XmlWriter XmlWriter
        {
            get { return _xmlW; }
            set { _xmlW = value; }
        }
        private XmlReader _xmlR;

        public XmlReader XmlReader
        {
            get { return _xmlR; }
            set { _xmlR = value; }
        }
        public void ModoEscritura(string nombreArchivo,string rutaArchivo) {
            try
            {
                
                if (!string.IsNullOrEmpty(nombreArchivo) && IsolatedStorageC.IsolatedStorage != null)
                {
                    this._isoStream = new IsolatedStorageFileStream(rutaArchivo + "/" + nombreArchivo, FileMode.Create, IsolatedStorageC.IsolatedStorage);
                    PropiedadesW.Indent = true;
                    _xmlW = XmlWriter.Create(_isoStream, PropiedadesW);
                }
                else
                
                    System.Diagnostics.Debug.WriteLine("Inicializa el isolatedStorage de la clase isolatedStorageC o escribe un nombre correcto para " + nombreArchivo);
                  
                
            
            
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
                System.Diagnostics.Debug.WriteLine("El archivo ya existe" + nombreArchivo);
                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine("Tipo de dato no correcto" + nombreArchivo);
                throw ex;
            }
        }
        public void CerrarXMLWriter() {
            if (_xmlW != null)
            {
                _xmlW.Flush();
                _xmlW.Close();
                _isoStream.Flush();
                _isoStream.Close();
            }
        }
        public void ModoLectura(string nombreArchivo,string rutaArchivo){
            try
            {

                if (!string.IsNullOrEmpty(nombreArchivo) && IsolatedStorageC.IsolatedStorage != null)
                {
                    this._isoStream = IsolatedStorageC.IsolatedStorage.OpenFile(rutaArchivo+"/"+nombreArchivo, FileMode.Open);
                    PropiedadesR.IgnoreComments = true;
                    _xmlR = XmlReader.Create(_isoStream, PropiedadesR);
                 
                }
                else

                    System.Diagnostics.Debug.WriteLine("Inicializa el isolatedStorage de la clase isolatedStorageC o escribe un nombre correcto para " + nombreArchivo);




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
                System.Diagnostics.Debug.WriteLine("El archivo no existe" + nombreArchivo);
                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine("Tipo de dato no correcto" + nombreArchivo);
                throw ex;
            }
        }
        public void CerrarXMLReader() {
            if (_xmlR != null)
            
                _xmlR.Close();
            _isoStream.Flush();
            _isoStream.Close();
            
        }
    }
}
