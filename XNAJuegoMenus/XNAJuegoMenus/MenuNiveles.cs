/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using XNAProyecto.Menus;
using XNAProyecto.ComponentesXNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAProyecto.Recursos;
namespace XNAJuegoMenus
{
    class MenuNiveles:Pantalla
    {
        private BotonFijo _botonAnimaciones,_botonScrollingMapa,_botonScrollingH;
        public static bool _menuNivelesEstado = false;
        public MenuNiveles() : base("Menu niveles") { _menuNivelesEstado = true; }

       

        #region METODOS COMUNES
        protected override void SalirMenu()
       {
           _menuNivelesEstado = false;
           ControlMenus.NuevaPantalla(new MenuInicio());
           _botonAnimaciones.Dispose();
           _botonScrollingMapa.Dispose();
           _botonScrollingH.Dispose();
       }

       public override void Draw(GameTime gameTime)
       {
          
           SpriteBatch spriteBatch = ControlMenus.SpriteBatch;
           if (_menuNivelesEstado == true)
           {
               spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
               spriteBatch.Draw(ImagenesR._Instancia.Textura("fondoNiveles"), ControlMenus.game.GraphicsDevice.Viewport.Bounds,
                   null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1.0f);
               spriteBatch.End();
           }
           base.Draw(gameTime);
       }
     
       public override void LoadContent()
       {
           _botonAnimaciones = new BotonFijo(ControlMenus.game, "_botonAnimacion", ImagenesR._Instancia.Textura("bAnim"));
           _botonAnimaciones._PosicionOrigen = _botonAnimaciones._CentroTextura;
           _botonAnimaciones._PosicionProximaOrigen = new Vector2(240, 220);
           _botonAnimaciones._ColorBotonNormal = Color.White;
           _botonAnimaciones._ColorBotonApretado = Color.Green;
           _botonAnimaciones.Initialize();
           _botonAnimaciones.Click += botonAnimacionesClick;

           _botonScrollingMapa = new BotonFijo(ControlMenus.game, "_botonMapa", ImagenesR._Instancia.Textura("bScrollM"));
           _botonScrollingMapa._PosicionOrigen = _botonScrollingMapa._CentroTextura;
           _botonScrollingMapa._PosicionProximaOrigen = new Vector2(240, 420);
           _botonScrollingMapa._ColorBotonNormal = Color.White;
           _botonScrollingMapa._ColorBotonApretado = Color.Green;
           _botonScrollingMapa.Initialize();
           _botonScrollingMapa.Click += botonMapaClick;

           _botonScrollingH = new BotonFijo(ControlMenus.game, "_botonScrollH", ImagenesR._Instancia.Textura("bScrollH"));
           _botonScrollingH._PosicionOrigen = _botonScrollingH._CentroTextura;
           _botonScrollingH._PosicionProximaOrigen = new Vector2(240, 620);
           _botonScrollingH._ColorBotonNormal = Color.White;
           _botonScrollingH._ColorBotonApretado = Color.Green;
           _botonScrollingH.Initialize();
           _botonScrollingH.Click += botonScrollHClick;    
           base.LoadContent();
       }
       #endregion
       #region EVENTOS
       public void botonAnimacionesClick(object sender, EventArgs events)
       {
           _menuNivelesEstado = false;
         
           ControlMenus.NuevaPantalla(new MenuAnimaciones());
          _botonAnimaciones.Dispose();
          _botonScrollingMapa.Dispose();
          _botonScrollingH.Dispose();
       }
       public void botonMapaClick(object sender, EventArgs events)
       {
           _menuNivelesEstado = false;

           ScrollingMapa scrollingM = new ScrollingMapa();
           ControlMenus.NuevaPantalla(scrollingM);
           _botonAnimaciones.Dispose();
           _botonScrollingMapa.Dispose();
           _botonScrollingH.Dispose();
       }

       public void botonScrollHClick(object sender, EventArgs events)
       {
           _menuNivelesEstado = false;
           ScrollingHorizontal scrollingH = new ScrollingHorizontal();
           ControlMenus.NuevaPantalla(scrollingH);
           _botonAnimaciones.Dispose();
           _botonScrollingMapa.Dispose();
           _botonScrollingH.Dispose();
       }
       #endregion

       
    }
    
}
