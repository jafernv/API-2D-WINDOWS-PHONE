/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAProyecto.Recursos;
namespace XNAJuegoMenus
{

    public class Juego : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;  
        Inicio inicio;
        private PantallaPresentacionTiempo _pantP;
        private static bool _iniciado = false;
        
        public Juego()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ConjuntoRecursos.Initialize(this);
            
            // La velocidad de fotogramas predeterminada para Windows Phone es de 30 fps.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Amplía la duración de la batería con bloqueo.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
            this.graphics.ApplyChanges();
            _pantP = new PantallaPresentacionTiempo(this);
            _pantP._pantallaPresentacionActiva = true; 
        }
        protected override void Initialize()
        {
            DatosPresentacionAnimada();
            GraphicsDevice.Clear(Color.Black);
            base.Initialize();
            _pantP.Initialize();
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_pantP._pantallaPresentacionActiva == true)
                _pantP.Update(gameTime);
            else
            if(_iniciado==false)
            {
                inicio = new Inicio(this);
                Components.Add(inicio);
                inicio.NuevaPantalla(new MenuInicio());
                _iniciado = true;
            }
        }
        public void DatosPresentacionAnimada()
        {
            ImagenesR.CargarImagen("playButton", "play");
            ImagenesR.CargarImagen("fondoPresentacion1", "fondo1");
            ImagenesR.CargarImagen("fondoPresentacion2", "fondo2");
            ImagenesR.CargarImagen("fondoPresentacion3", "fondo3");
            ImagenesR.CargarImagen("fondoNegro", "fondoNegro");
            ImagenesR.CargarImagen("boton_anterior", "bAnterior");
            ImagenesR.CargarImagen("boton_siguiente", "bSiguiente");
        }
        static class Program{
            static void Main(){
                using (Juego game = new Juego()){
                    
                    game.Run();} }}
    }


    public class Inicio : XNAProyecto.Menus.AdministradorPantallas {
        public static XMLDatos xmlDatosInicio;
        public static XMLAnimaciones xmlAnimaciones;
        public Inicio(Game game): base(game) {
            
        }

        public override void cargarCosasComunes()
        {
            if(game!=null){

                ImagenesR.CargarImagen("fondo", "fondo");     
                ImagenesR.CargarImagen("pauseButton", "pause");
                ImagenesR.CargarImagen("stopButton", "stop");
                ImagenesR.CargarImagen("sin silencio", "sin silencio");
                ImagenesR.CargarImagen("silencio", "silencio");
                ImagenesR.CargarImagen("botonDeslizante", "sliderBoton");
                ImagenesR.CargarImagen("barraDeslizante", "sliderBarra");
                ImagenesR.CargarImagen("Inicio_opciones", "bOpciones");
                ImagenesR.CargarImagen("Inicio_niveles", "bNiveles");
                // FuentesSpriteR.CargarFuente("GameFont", "fuente");
                //CancionesR.CargarCancion("tormenta", "music");
                //EfectosSonidoR.CargarEfectoSonido("truenoEfecto", "trueno");
                //EfectosSonidoR.CargarEfectoSonido("truenoEfectoR", "truenoR");

                ImagenesR.CargarImagen("Niveles_anim", "bAnim");
                ImagenesR.CargarImagen("NivelesScrollH", "bScrollH");
                ImagenesR.CargarImagen("NivelesScrollMapa", "bScrollM");
                ImagenesR.CargarImagen("fondoNiv", "fondoNiv");
                ImagenesR.CargarImagen("fondoNiveles", "fondoNiveles");
                ImagenesR.CargarImagen("animacionP", "animacionP");
                xmlDatosInicio = new XMLDatos(game, "Datos.xml", "Definition");
                xmlDatosInicio.CargarDatos();
                xmlAnimaciones = new XMLAnimaciones(game, "Imagenes/PruebasAnimaciones.xml", "Definition");
                xmlAnimaciones.cargarAnimacion();
                
                ImagenesR.CargarImagen("escala", "escala");
                ImagenesR.CargarImagen("orientacion", "orientacion");

                ImagenesR.CargarImagen("mandoVirtual", "mandoV");
                ImagenesR.CargarImagen("mandoVirtualBase", "mandoVB");
                ImagenesR.CargarImagen("gridConColisiones", "gridCC");
                ImagenesR.CargarImagen("gridSinColisiones", "gridSC");
                ImagenesR.CargarImagen("gridSinColisionesH", "gridSCH");
                ImagenesR.CargarImagen("gridConColisionesH", "gridCCH");
                ImagenesR.CargarImagen("jugador", "jugador");
                ImagenesR.CargarImagen("jugadorH", "jugadorH");
            }
            base.cargarCosasComunes();
        }
       
    }

}
