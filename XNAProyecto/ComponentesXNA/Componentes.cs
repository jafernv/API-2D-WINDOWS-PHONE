/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework;

namespace XNAProyecto.ComponentesXNA
{
    public abstract class Componentes :DrawableGameComponent
    {
        #region VARIABLES Y PROPIEDADES

        
        /// <summary>
        /// Es el nombre que tendra la textura.
        /// </summary>
        private string _nombreActivo;
        /// <summary>
        /// Devuelve y permite modificar el nombre del activo.
        /// </summary>
        public string _NombreActivo
        {
            get { return _nombreActivo; }
            set { _nombreActivo = value; }
        }
        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Crea un componente.
        /// Para crear componentes,debes llamar a  "AgregarComponente" del namespace "Entidades". 
        /// </summary>
        internal Componentes(Game game,string nombre): base(game)
        {
            _NombreActivo = nombre;
        } 

        #endregion

       

       
       
    }
}
