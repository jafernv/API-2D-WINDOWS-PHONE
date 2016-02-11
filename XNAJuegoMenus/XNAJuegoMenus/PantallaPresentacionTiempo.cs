/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using XNAProyecto.Recursos;
using XNAProyecto.ComponentesXNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using XNAProyecto.Menus;
namespace XNAJuegoMenus
{
    public class PantallaPresentacionTiempo : DrawableGameComponent
    {
        private Game _game;
        private BotonFijo _boton;
        private PresentacionTiempo _presentacion;
        private SpriteBatch _spriteBatch;
        public TouchLocation? _touchLocation;
        internal bool _pantallaPresentacionActiva;
        
        public PantallaPresentacionTiempo(Game game)
            : base(game)
        {
            this._game = game;
        }
        /// <summary>
        /// Inicia los elementos de pantalla
        /// depende del orden de constructores para renderizar la pantalla
        /// </summary>
        public override void Initialize()
        {

            base.Initialize();
            GraphicsDevice.Clear(Color.Black);
            _presentacion = new PresentacionTiempo(_game, "pantallaPresentacion");
            _presentacion.agregarDiapositiva(ImagenesR._Instancia.Textura("fondo1"), 2.5f);
            _presentacion.agregarDiapositiva(ImagenesR._Instancia.Textura("fondo2"), 1.5f);
            _presentacion.agregarDiapositiva(ImagenesR._Instancia.Textura("fondo3"), 3f);
            _presentacion._SaltarTransicion = true;

            _spriteBatch = new SpriteBatch(GraphicsDevice);


            _boton = new BotonFijo(this._game, "_boton", ImagenesR._Instancia.Textura("play"));
            _boton._PosicionOrigen = _boton._CentroTextura;
            _boton._PosicionProximaOrigen = new Vector2(45, 45);
            _boton._ColorBotonNormal = Color.OrangeRed;
            _boton._ColorBotonApretado = Color.Green;
            _boton.Initialize();
            _presentacion.Initialize();

            _boton.TouchDown += _presentacion.botonSaltarPresentacion;
        }

        public override void Update(GameTime gameTime)
        {

            TouchCollection touches = TouchPanel.GetState();
            if (touches.Count == 1)
            {
                _touchLocation = touches[0];
                Botones._TouchLocation = _touchLocation;
            }
            else
            {
                _touchLocation = null;
                Botones._TouchLocation = _touchLocation;
            }
            if (_presentacion.FinPresentacion == true)
            {
                _boton.Dispose();
                this._pantallaPresentacionActiva = false;
                this.Dispose(false);
                MenuInicio._menuInicioEstado = true;
            }
            base.Update(gameTime);
        }

    }
}
