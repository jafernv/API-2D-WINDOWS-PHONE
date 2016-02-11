/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using System.IO;
using System.IO.IsolatedStorage;
namespace XNAProyecto.Recursos
{
    public class IsolatedStorageC
    {
        private static IsolatedStorageFile _isolatedStorage;

        public static IsolatedStorageFile IsolatedStorage
        {
            get { return IsolatedStorageC._isolatedStorage; }            
        }
        public IsolatedStorageC() {
            _isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
        }
        public void CrearDirectorio(string nombre)
        {
            try
            {
                if (!string.IsNullOrEmpty(nombre) && !IsolatedStorage.DirectoryExists(nombre))
                {
                    IsolatedStorage.CreateDirectory(nombre);
                }
                else
                    System.Diagnostics.Debug.WriteLine("El directorio del 'IsolatedStorage' existe o no es correcto " + nombre);
            }
            catch (ObjectDisposedException ex) {
                System.Diagnostics.Debug.WriteLine("Inicialize el directorio aislado estático 'IsolatedStorage'  " + nombre);
                throw ex;
            }
           
        }
        public void EliminarDirectorio(string nombre)
        {
            try
            {
                if (!string.IsNullOrEmpty(nombre) && IsolatedStorage.DirectoryExists(nombre))
                {
                    IsolatedStorage.DeleteDirectory(nombre);
                }
                else
                    System.Diagnostics.Debug.WriteLine("El directorio del 'IsolatedStorage' no existe o no es correcto " + nombre);
            }
            catch (ObjectDisposedException ex)
            {
                System.Diagnostics.Debug.WriteLine("Inicialize el directorio aislado estático 'IsolatedStorage'  " + nombre);
                throw ex;
            }

        }
        /// <summary>
        /// Crea el archivo en el directorio raiz.
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public void CrearArchivo(string nombreArchivo) {
            CrearArchivo(nombreArchivo,"");
        }
        public void CrearArchivo(string nombreArchivo,string rutaArchivo)
        {

            try
            {
                if (!string.IsNullOrEmpty(nombreArchivo) && IsolatedStorage.DirectoryExists(rutaArchivo))
                {
                    StreamWriter str = new StreamWriter(new IsolatedStorageFileStream(rutaArchivo + "/" + nombreArchivo, FileMode.CreateNew, IsolatedStorage));
                    str.Flush();
                    str.Close();

                }
                else
                    System.Diagnostics.Debug.WriteLine("El archivo del 'IsolatedStorage' no es correcto " + nombreArchivo);
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
            catch (IsolatedStorageException ex) {
                System.Diagnostics.Debug.WriteLine("El archivo ya existe"+ nombreArchivo);
                throw ex;
            }
        }
        /// <summary>
        /// Elimina un archivo del directorio raiz
        /// </summary>
        /// <param name="nombreArchivo"></param>
        public void EliminarArchivo(string nombreArchivo) {
            EliminarArchivo(nombreArchivo,"");
        }
        public void EliminarArchivo(string nombreArchivo, string rutaArchivo) {
            try
            {
                if (!string.IsNullOrEmpty(nombreArchivo) && IsolatedStorage.DirectoryExists(rutaArchivo))
                {
                    if (ExisteArchivo(rutaArchivo, nombreArchivo))
                        IsolatedStorage.DeleteFile(rutaArchivo + "/" + nombreArchivo);
                    else
                        System.Diagnostics.Debug.WriteLine("El archivo del 'IsolatedStorage' no existe " + nombreArchivo);
                }
                else
                    System.Diagnostics.Debug.WriteLine("El archivo del 'IsolatedStorage' no es correcto " + nombreArchivo);
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
        }
        public bool ExisteArchivo(string nombreArchivo, string rutaArchivo) {
            
            return IsolatedStorage.FileExists(rutaArchivo + "/" + nombreArchivo);    
            
        }
        
    }
}
