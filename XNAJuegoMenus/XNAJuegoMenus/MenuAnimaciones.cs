/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework;
using XNAProyecto.ComponentesXNA;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System;
using XNAProyecto.Menus;
using XNAProyecto.Recursos;
namespace XNAJuegoMenus
{
    public class MenuAnimaciones : Pantalla
    {
       
        public TouchLocation? _touchLocation;
        public Dictionary<string, Animacion> definicionAn { get; set; }
        protected string AnimationKey { get; set; }
        protected string AnimationKey2 { get; set; }
        protected string AnimationKey3 { get; set; }
        protected string AnimationKey4 { get; set; }
        protected BotonFijo _botonEscala, _botonOrientacion;
        private float _escala = 1.5f ;
        private float _orientacion=0f;
        private bool _cambioEscala=false;
        public static  bool _menuAnimacionesEstado=false;
        public MenuAnimaciones()
            : base("Menu animaciones")
        {    
            this.AnimationKey = "golpear";
            this.AnimationKey2 = "disparo";
            this.AnimationKey3 = "disparo2";
             this.AnimationKey4 = "golpeado";
             _menuAnimacionesEstado = true;
        }

        #region METODOS COMUNES
        public override void  Update(GameTime gameTime,bool coveredByOtherScreen)
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
            if ((!string.IsNullOrEmpty(AnimationKey)) && (!string.IsNullOrEmpty(AnimationKey2))
                && (!string.IsNullOrEmpty(AnimationKey3)) && (!string.IsNullOrEmpty(AnimationKey4)))
            {
                definicionAn[AnimationKey].Update(gameTime, true);
                definicionAn[AnimationKey2].Update(gameTime, true);
                definicionAn[AnimationKey3].Update(gameTime, true);
                definicionAn[AnimationKey4].Update(gameTime, true);
            }
            base.Update(gameTime,coveredByOtherScreen);

          
        }   
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ControlMenus.SpriteBatch;
            ControlMenus.GraphicsDevice.Clear(Color.Teal);
            if (_menuAnimacionesEstado == true)
            {
                if ((!string.IsNullOrEmpty(AnimationKey)) && (!string.IsNullOrEmpty(AnimationKey2)) &&
                    (!string.IsNullOrEmpty(AnimationKey3)) && (!string.IsNullOrEmpty(AnimationKey4)))
                {
                    spriteBatch.Begin();
                    
                    if (_touchLocation != null && _touchLocation.Value.State != TouchLocationState.Invalid)
                       definicionAn[AnimationKey2].Draw(spriteBatch, _touchLocation.Value.Position, _escala, _orientacion, SpriteEffects.None);
                    definicionAn[AnimationKey].Draw(spriteBatch, new Vector2(240, 90), 2.5f, _orientacion, SpriteEffects.FlipHorizontally);
                    definicionAn[AnimationKey2].Draw(spriteBatch, new Vector2(200, 285), _escala, 0f, SpriteEffects.FlipVertically);
                    definicionAn[AnimationKey3].Draw(spriteBatch, new Vector2(200, 405), 3f, 0f, SpriteEffects.None);
                    definicionAn[AnimationKey4].Draw(spriteBatch, new Vector2(285, 405), 3f, 0f, SpriteEffects.FlipHorizontally);
                    spriteBatch.End();
                }
            }
            base.Draw(gameTime);


            
        }
        public override void LoadContent()
        {
            this.definicionAn = Inicio.xmlAnimaciones._DiccAnimaciones;
            if ((!string.IsNullOrEmpty(AnimationKey)) && (!string.IsNullOrEmpty(AnimationKey2)) &&
                    (!string.IsNullOrEmpty(AnimationKey3)) && (!string.IsNullOrEmpty(AnimationKey4)))
            {
                definicionAn[AnimationKey].IndiceInicio(0);
                definicionAn[AnimationKey2].IndiceInicio(0);
                definicionAn[AnimationKey3].IndiceInicio(0);
                definicionAn[AnimationKey4].IndiceInicio(0);
            }
            _botonEscala = new BotonFijo(ControlMenus.game, "_botonEscala", ImagenesR._Instancia.Textura("escala"));
            _botonOrientacion = new BotonFijo(ControlMenus.game, "_botonOrientacion", ImagenesR._Instancia.Textura("orientacion"));

            _botonOrientacion._PosicionOrigen = _botonOrientacion._CentroTextura;
            _botonEscala._PosicionOrigen = _botonEscala._CentroTextura;

            _botonEscala._PosicionProximaOrigen = new Vector2(100, 285);
            _botonOrientacion._PosicionProximaOrigen = new Vector2(100, 100);

            _botonOrientacion.TouchDown += Orientacion;
            _botonEscala.TouchDown += Escala;      
            base.LoadContent();
        }
        protected override void SalirMenu()
        {
            _menuAnimacionesEstado = false;
            ControlMenus.NuevaPantalla(new MenuInicio());
            _botonEscala.Dispose();
            _botonOrientacion.Dispose();
        }
        #endregion
        #region EVENTOS
        private void Escala(object sender, EventArgs e)
        {

            if (_escala == 0.5f)
                _cambioEscala = false;
            if (_escala == 4.5f)
                _cambioEscala = true;

            if (_cambioEscala == false)
                _escala = _escala + 0.5f;
            else
                _escala = _escala - 0.5f;


        }
        private void Orientacion(object sender, EventArgs e)
        {
            _orientacion = _orientacion - 0.5f;


        }
        #endregion
    }
}
