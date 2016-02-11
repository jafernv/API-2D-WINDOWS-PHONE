/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using XNAProyecto.Menus;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XNAProyecto.Recursos;
using XNAProyecto.Jugador;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using XNAProyecto.Tiles;
using XNAProyecto.ComponentesXNA;
using System;
namespace XNAJuegoMenus
{
    public class ScrollingHorizontal : Pantalla
    {

        public static bool _mapaTilesIniciado = false;
        private Texture2D _imagenStick, _imagenStickPerimetro;
        private Vector2 _posicionMandoVirtual = Vector2.Zero, _posicionMandoVirtualPerimetro = Vector2.Zero;
        private Vector2 _posicionAnteriorTouchPanel;
        private MandoVirtual _mandoVirtual;
        private MapaTiles mapaTiles;

        private Jugador _jugador;
        private BotonFijo _botonSaltar;
        private GameTime gameTime;
        public ScrollingHorizontal()
            : base("Scrolling Horizontal")
        {
            _mapaTilesIniciado = true;
            Scrolling.PosicionVistaX = 480;
            Scrolling.PosicionVistaY = 800;
        }
        #region METODOS COMUNES
        public override void BackBoton(EstadoPadTouchPanel input)
        {

            if (input.BotonPulsado(Buttons.Back))
            {
                _mapaTilesIniciado = false;
                _jugador.Dispose();
                _botonSaltar.Dispose();
                ControlMenus.NuevaPantalla(new MenuInicio());
                Scrolling.Posicion = Vector2.Zero;
            }
            base.BackBoton(input);
        }

        public override void Update(GameTime gameTime, bool oculto)
        {
            _jugador.MoverJugadorHorizontalmente(_mandoVirtual, gameTime, this.mapaTiles);

            //actualiza la posición del mando respecto al radio.
            float radio = _imagenStick.Width / 2 + 42;
            _posicionMandoVirtual = new Vector2(77, 95) + (_mandoVirtual.MandoVirtualPosicionNormalizado * radio);

            this.gameTime = gameTime;
            base.Update(gameTime, oculto);
        }


        public override void ControlTactil(GameTime time, EstadoPadTouchPanel estado)
        {
            _mandoVirtual.EstadoPanel(estado);

            if (estado.estadoPantallaTactil.Count > 0)
            {
                foreach (TouchLocation touch in estado.estadoPantallaTactil)
                {
                    _posicionAnteriorTouchPanel = touch.Position;
                }
            }
            this.gameTime = time;

        }
        public override void Draw(GameTime gameTime)
        {

            SpriteBatch spriteBatch = ControlMenus.SpriteBatch;
            if (_mapaTilesIniciado == true)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                this.mapaTiles.Draw(gameTime);
                spriteBatch.Draw(_imagenStick, _posicionMandoVirtual, Color.White);
                spriteBatch.Draw(_imagenStickPerimetro, _posicionMandoVirtualPerimetro, Color.White);
                spriteBatch.End();

            }
            base.Draw(gameTime);
        }

        public override void LoadContent()
        {

            _imagenStick = ImagenesR._Instancia.Textura("mandoV");
            _imagenStickPerimetro = ImagenesR._Instancia.Textura("mandoVB");
            _posicionMandoVirtual = new Vector2(77, 95);
            _posicionMandoVirtualPerimetro = new Vector2(59, 78);
            MandoVirtual.rectanguloToqueMandoVirtual = new Rectangle(0, 0, ControlMenus.game.GraphicsDevice.Viewport.Width / 2 - 55, ControlMenus.game.GraphicsDevice.Viewport.Height / 3 - 55);
            _mandoVirtual = new MandoVirtual();
            this._jugador = new Jugador(ControlMenus.game, ImagenesR._Instancia.Textura("jugadorH"), new Rectangle(40, 40, 80, 120), new Vector2(185, 40));
            this._jugador.Initialize();

            this.mapaTiles = new MapaTiles(ControlMenus.game, 11, 45, 45, 45);
            this.mapaTiles.Iniciar(ImagenesR._Instancia.Textura("gridCCH"), 2, 4,
                ImagenesR._Instancia.Textura("gridSCH"), 2, 4);
            this.mapaTiles.GenerarMapaHorizontal();
            this.mapaTiles.Initialize();

            _botonSaltar = new BotonFijo(ControlMenus.game, "_botonSaltar", ImagenesR._Instancia.Textura("play"));
            _botonSaltar._PosicionOrigen = _botonSaltar._CentroTextura;
            _botonSaltar._PosicionProximaOrigen = new Vector2(85, 720);
            _botonSaltar._ColorBotonNormal = Color.White;
            _botonSaltar._ColorBotonApretado = Color.Green;
            _botonSaltar.Initialize();
             _botonSaltar.Click += botonSaltar;
            base.LoadContent();
        }
        #endregion
        public void botonSaltar(object sender, EventArgs e)
        {
            
               _jugador.Salto(gameTime,this.mapaTiles);
            
        }
    }
}
