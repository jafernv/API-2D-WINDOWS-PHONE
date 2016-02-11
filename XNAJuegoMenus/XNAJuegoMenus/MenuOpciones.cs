/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework;
using XNAProyecto.Menus;
using Microsoft.Xna.Framework.Graphics;
using XNAProyecto.Recursos;
using XNAProyecto.ComponentesXNA;
using System;
using Microsoft.Xna.Framework.Media;

namespace XNAJuegoMenus
{
    public class MenuOpciones : Pantalla
    {
       public static bool _menuOpcionesEstado=false;
       private bool _efectosSonidosPausados = false;
       private BotonFijo _botonEfecto1, _botonEfecto2, _botonEfecto3, _botonEfecto4;
       private BotonFijo _botonMusica1, _botonMusica2, _botonMusica3;
       private BotonFijo _botonSilencio;
       private BotonDeslizante _botonDeslizante1, _botonDeslizante2, _botonDeslizante3, _botonDeslizante4;
        public MenuOpciones(): base("Menu Opciones")
            
        {
            _menuOpcionesEstado = true;
        }

        protected override void SalirMenu()
        {
            _menuOpcionesEstado = false;
            ControlMenus.NuevaPantalla(new MenuInicio());


            _botonEfecto1.Dispose();
            _botonEfecto2.Dispose();
            _botonEfecto3.Dispose();
            _botonEfecto4.Dispose();
            _botonMusica1.Dispose();
            _botonMusica2.Dispose();
            _botonMusica3.Dispose();
            _botonDeslizante1.Dispose();
            _botonDeslizante2.Dispose();
            _botonDeslizante3.Dispose();
            _botonDeslizante4.Dispose();
            _botonSilencio.Dispose();
            _botonSilencio._botonUsaUpdate = false;
        }
      
       public override void Draw(GameTime gameTime)
       {
           SpriteBatch spriteBatch = ControlMenus.SpriteBatch;
         
           if (_menuOpcionesEstado == true)
           {
               spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
               spriteBatch.Draw(ImagenesR._Instancia.Textura("fondo"), ControlMenus.game.GraphicsDevice.Viewport.Bounds, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1.0f);
               spriteBatch.DrawString(FuentesSpriteR._Instancia.Fuente("fuente"), "Sound Effect 1", new Vector2(150, 6),
                   Color.GreenYellow);
               spriteBatch.DrawString(FuentesSpriteR._Instancia.Fuente("fuente"), "Song  y volumen", new Vector2(140, 130),
                   Color.GreenYellow);
               spriteBatch.DrawString(FuentesSpriteR._Instancia.Fuente("fuente"), "Sound Effect 2 (pitch,volumen)", new Vector2(90, 350),
                     Color.GreenYellow);
               spriteBatch.DrawString(FuentesSpriteR._Instancia.Fuente("fuente"), "Silencio y Volumen General",
                    new Vector2(90, 620), Color.GreenYellow);
               spriteBatch.Draw(ImagenesR._Instancia.Textura("sliderBarra"), new Vector2(92, 280), Color.White);
               spriteBatch.Draw(ImagenesR._Instancia.Textura("sliderBarra"), new Vector2(92, 500), Color.White);
               spriteBatch.Draw(ImagenesR._Instancia.Textura("sliderBarra"), new Vector2(92, 580), Color.White);
               spriteBatch.Draw(ImagenesR._Instancia.Textura("sliderBarra"), new Vector2(35, 720), Color.White);
           
               spriteBatch.End();
           }
           base.Draw(gameTime);
       }

       public override void LoadContent()
       {
           _botonEfecto1 = new BotonFijo(ControlMenus.game, "_botonEfecto1", ImagenesR._Instancia.Textura("play"));
           _botonEfecto2 = new BotonFijo(ControlMenus.game, "_botonEfecto2", ImagenesR._Instancia.Textura("play"));
           _botonEfecto3 = new BotonFijo(ControlMenus.game, "_botonEfecto3", ImagenesR._Instancia.Textura("pause"));
           _botonEfecto4 = new BotonFijo(ControlMenus.game, "_botonEfecto4", ImagenesR._Instancia.Textura("stop"));
           _botonMusica1 = new BotonFijo(ControlMenus.game, "_botonMusica1", ImagenesR._Instancia.Textura("play"));
           _botonMusica2 = new BotonFijo(ControlMenus.game, "_botonMusica2", ImagenesR._Instancia.Textura("pause"));
           _botonMusica3 = new BotonFijo(ControlMenus.game, "_botonMusica3", ImagenesR._Instancia.Textura("stop"));
            if (_botonSilencio == null)
                _botonSilencio = new BotonFijo(ControlMenus.game, "_botonSilencio", ImagenesR._Instancia.Textura("sin silencio"));
            _botonDeslizante1 = new BotonDeslizante(ControlMenus.game, "_botonDeslizante1", ImagenesR._Instancia.Textura("sliderBoton"));
            _botonDeslizante2 = new BotonDeslizante(ControlMenus.game, "_botonDeslizante2", ImagenesR._Instancia.Textura("sliderBoton"));
            _botonDeslizante3 = new BotonDeslizante(ControlMenus.game, "_botonDeslizante3", ImagenesR._Instancia.Textura("sliderBoton"));
            _botonDeslizante4 = new BotonDeslizante(ControlMenus.game, "_botonDeslizante4", ImagenesR._Instancia.Textura("sliderBoton"));



           _botonEfecto1._PosicionOrigen = _botonEfecto1._CentroTextura;
           _botonEfecto2._PosicionOrigen = _botonEfecto2._CentroTextura;
           _botonEfecto3._PosicionOrigen = _botonEfecto3._CentroTextura;
           _botonEfecto4._PosicionOrigen = _botonEfecto4._CentroTextura;
           _botonMusica1._PosicionOrigen = _botonMusica1._CentroTextura;
           _botonMusica2._PosicionOrigen = _botonMusica2._CentroTextura;
           _botonMusica3._PosicionOrigen = _botonMusica3._CentroTextura;
           _botonSilencio._PosicionOrigen = _botonSilencio._CentroTextura;
           _botonDeslizante1._PosicionOrigen = _botonDeslizante1._CentroTextura;
           _botonDeslizante2._PosicionOrigen = _botonDeslizante2._CentroTextura;
           _botonDeslizante3._PosicionOrigen = _botonDeslizante3._CentroTextura;
           _botonDeslizante4._PosicionOrigen = _botonDeslizante4._CentroTextura;


           _botonEfecto1._PosicionProximaOrigen = new Vector2(112, 85);
           _botonEfecto2._PosicionProximaOrigen = new Vector2(112, 430);
           _botonEfecto3._PosicionProximaOrigen = new Vector2(242, 430);
           _botonEfecto4._PosicionProximaOrigen = new Vector2(372, 430);
           _botonMusica1._PosicionProximaOrigen = new Vector2(112, 210);
           _botonMusica2._PosicionProximaOrigen = new Vector2(242, 210);
           _botonMusica3._PosicionProximaOrigen = new Vector2(372, 210);


           _botonSilencio._PosicionProximaOrigen = new Vector2(436, 730);
           _botonSilencio._CambioEstado = true;
           _botonSilencio._TexturaBotonEstado2 = ImagenesR._Instancia.Textura("silencio");


           _botonDeslizante1._PosicionProximaOrigen = new Vector2(265, 280);
           _botonDeslizante1._RestriccionesDesplazamiento = new Rectangle(120,
               (int)_botonDeslizante1._PosicionProximaOrigen.Y, 300, 0);
           _botonDeslizante1._PermitirArrastre = true;

           _botonDeslizante2._PosicionProximaOrigen = new Vector2(265, 500);
           _botonDeslizante2._RestriccionesDesplazamiento = new Rectangle(120,
               (int)_botonDeslizante2._PosicionProximaOrigen.Y, 300, 0);
           _botonDeslizante2._PermitirArrastre = true;

           _botonDeslizante3._PosicionProximaOrigen = new Vector2(265, 580);
           _botonDeslizante3._RestriccionesDesplazamiento = new Rectangle(120,
              (int)_botonDeslizante3._PosicionProximaOrigen.Y, 300, 0);
           _botonDeslizante3._PermitirArrastre = true;

           _botonDeslizante4._PosicionProximaOrigen = new Vector2(210, 720);
           _botonDeslizante4._RestriccionesDesplazamiento = new Rectangle(60,
              (int)_botonDeslizante4._PosicionProximaOrigen.Y, 300, 0);
           _botonDeslizante4._PermitirArrastre = true;

           _botonEfecto1.Click += EfectoSonido1Play;
           _botonEfecto2.Click += EfectoSonido2Play;
           _botonEfecto3.Click += EfectoSonido2Pause;
           _botonEfecto4.Click += EfectoSonido2Stop;

           _botonDeslizante1.PosicionCambiada += MusicaReproductor.CambioVolumenMusica;
           _botonDeslizante2.PosicionCambiada += Pitch;
           _botonDeslizante3.PosicionCambiada += EfectosSonidoReproductor.CambioVolumenEfectosSonido;
           _botonDeslizante4.PosicionCambiada += MusicaReproductor.CambioVolumenGeneral;

           _botonMusica1.Click += MusicaPlay;
           _botonMusica2.Click += MusicaPause;
           _botonMusica3.Click += MusicaStop;
           _botonSilencio._botonUsaUpdate = true;
           _botonSilencio.Click += MusicaReproductor.PonerSilencio;
           base.LoadContent();
       }

       #region EVENTOS
       private void EfectoSonido1Play(object sender, EventArgs e)
       {
           EfectosSonidoReproductor.ReproducirEfectoSonido("trueno");

       }
       private void EfectoSonido2Play(object sender, EventArgs e)
       {
           EfectosSonidoReproductor.ReproducirEfectoSonido("truenoR", true);

       }
       private void EfectoSonido2Pause(object sender, EventArgs e)
       {

           //PARA PAUSAR UN SÓLO EFECTO
           //if (EfectosSonidoR._Instancia.EfectoSonido("truenoR").State == SoundState.Playing)
           //    EfectosSonidoReproductor.PausarEfectoSonido("truenoR");
           //else
           //    if (EfectosSonidoR._Instancia.EfectoSonido("truenoR").State == SoundState.Paused)
           //        EfectosSonidoReproductor.ReanudarEfectoSonido("truenoR");
           if (_efectosSonidosPausados == false)
           {
               EfectosSonidoReproductor.PausarReanudarTodosEfectosSonido(false);
               _efectosSonidosPausados = true;
           }
           else
           {
               EfectosSonidoReproductor.PausarReanudarTodosEfectosSonido(true);
               _efectosSonidosPausados = false;
           }

       }
       private void EfectoSonido2Stop(object sender, EventArgs e)
       {
           EfectosSonidoReproductor.PararTodosEfectosSonido();

       }
       private void MusicaPlay(object sender, EventArgs e)
       {
           MusicaReproductor.ReproducirCancion("music");
       }
       private void MusicaPause(object sender, EventArgs e)
       {
           if (MediaPlayer.State == MediaState.Playing)
               MusicaReproductor.PausarCancion();
           else
               if (MediaPlayer.State == MediaState.Paused)
                   MusicaReproductor.ReanudarCancion();
       }
       private void MusicaStop(object sender, EventArgs e)
       {
           MusicaReproductor.PararCancion();
       }
       private void Pitch(object sender, EventArgs e)
       {
           Botones boton = sender as BotonDeslizante;
           float valor = (boton._PosicionProximaOrigen.X - (float)boton._RestriccionesDesplazamiento.Left) /
               (float)boton._RestriccionesDesplazamiento.Width;
           //EfectosSonidoR._Instancia.EfectoSonido("trueno").Pitch = valor;
           //EfectosSonidoR._Instancia.EfectoSonido("truenoR").Pitch = valor; 
           EfectosSonidoReproductor.PitchEfectoSonido("trueno", valor);
           EfectosSonidoReproductor.PitchEfectoSonido("truenoR", valor);
       }
       #endregion
    }
}
