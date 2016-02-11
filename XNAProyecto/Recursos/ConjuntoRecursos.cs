/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework;
namespace XNAProyecto.Recursos
{

    public abstract class ConjuntoRecursos : GameComponent
    {
        /// <summary>
        /// Constructor del conjunto de recursos.
        /// </summary>
        /// <param name="game"></param>
        public ConjuntoRecursos(Game game) : base(game) {
        
        }
        #region VARIABLES Y PROPIEDADES
        /// <summary>
        /// Ruta para cargar los niveles de un juego.
        /// </summary>
        private static string _rutaNivel="";
        /// <summary>
        /// Obtiene y modifica la ruta de nivel de un juego.Por defecto es "".
        /// </summary>
        public static string _RutaNivel
        {
            get { return ConjuntoRecursos._rutaNivel; }
            set { ConjuntoRecursos._rutaNivel = value; }
        }  
      
        #endregion
       
        #region METODOS
        /// <summary>
        /// Inicializa todas las clases hijas de los recursos.
        /// </summary>
        /// <param name="game"></param>
        public static void Initialize(Game game) { 
            EfectosSonidoR.Initialize(game);
            CancionesR.Initialize(game);
            ImagenesR.Initialize(game);
            FuentesSpriteR.Initialize(game);
        }
        /// <summary>
        /// Libera los recursos del juego,sin destruir los diccionarios.
        /// </summary>
        public static void LiberarRecursos() {
            EfectosSonidoR.liberarRecursosEfectosSonido();
            CancionesR.liberarRecursosCanciones();
            ImagenesR.liberarRecursosImagenes();
        }

        #endregion

    }
}
