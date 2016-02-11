/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using XNAProyecto.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAProyecto.Recursos;
using XNAProyecto.ComponentesXNA;
namespace XNAJuegoMenus
{
   public class MenuInicio : Pantalla
    {
        private BotonFijo _botonOpciones, _botonPantallas;
        public static bool _menuInicioEstado=false;
       public MenuInicio() : base("Menu Inicio") {

           _menuInicioEstado = true;
          
       }
       #region METODOS COMUNES

       protected override void SalirMenu()
       {
           _menuInicioEstado = false;
           ControlMenus.Game.Exit();
       }

       public override void Draw(GameTime gameTime)
       {
           SpriteBatch spriteBatch = ControlMenus.SpriteBatch;

           if (_menuInicioEstado == true)
           {
               spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
               spriteBatch.Draw(ImagenesR._Instancia.Textura("fondoNiv"), ControlMenus.game.GraphicsDevice.Viewport.Bounds,
                   null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1.0f);
               spriteBatch.End();
           }
           base.Draw(gameTime);
       }
     
       public override void LoadContent()
       {
           _botonOpciones = new BotonFijo(ControlMenus.game, "_botonOpciones", ImagenesR._Instancia.Textura("bOpciones"));
           _botonOpciones._PosicionOrigen = _botonOpciones._CentroTextura;
           _botonOpciones._PosicionProximaOrigen = new Vector2(240, 500);
           _botonOpciones._ColorBotonNormal = Color.White;
           _botonOpciones._ColorBotonApretado = Color.Green;
           _botonOpciones.Initialize();
           _botonOpciones.Click += botonOpcionesTouchDown;//USAR EL EVENTO CLICK NO TOUCHDOWN

           _botonPantallas = new BotonFijo(ControlMenus.game, "_botonPantallas", ImagenesR._Instancia.Textura("bNiveles"));
           _botonPantallas._PosicionOrigen = _botonPantallas._CentroTextura;
           _botonPantallas._PosicionProximaOrigen = new Vector2(240, 250);
           _botonPantallas._ColorBotonNormal = Color.White;
           _botonPantallas._ColorBotonApretado = Color.Green;
           _botonPantallas.Initialize();
           _botonPantallas.Click += botonNivelesTouchDown;
           base.LoadContent();
       }
       #endregion
       #region EVENTOS
       public void botonOpcionesTouchDown(object sender, EventArgs events)
       {
           _menuInicioEstado = false;
          ControlMenus.NuevaPantalla(new MenuOpciones());
          _botonOpciones.Dispose();
          _botonPantallas.Dispose();
       }
       public void botonNivelesTouchDown(object sender, EventArgs events)
       {
           _menuInicioEstado = false;      
           ControlMenus.NuevaPantalla(new MenuNiveles());
           _botonOpciones.Dispose();
           _botonPantallas.Dispose();
       }
       #endregion
    }
}
