/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISE�O Y ESPICIFICACI�N DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERN�NDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
namespace XNAProyecto.ComponentesXNA
{
    public class BotonDeslizante : Botones
    {
        
        /// <summary>
        /// Constructor de "BotonDeslizante".
        /// </summary>
        /// <param name="game">Juego</param>
        /// <param name="nombreRecurso">Nombre del recurso</param>
        /// <param name="texturaActiva">Textura del bot�n por defecto</param>
        public BotonDeslizante(Game game, string nombreRecurso, Texture2D texturaActiva) 
            : base(game, nombreRecurso, texturaActiva) { }
        /// <summary>
        ///Establece indicadores v�lidos cuando el bot�n es pulsado.
        /// Lanza el evento PulsarTouchDown, que es cuando el dedo toca la pantalla en un elemento y este se desplaza
        /// </summary>
        protected override void LanzarTouchDown()
        {
            PulsarTouchDown(EventArgs.Empty);
            _pulsacionActiva = true;
            if (_PermitirArrastre == true)
                _arrastreActivo = true;
        }


       

        /// <summary>
        /// Reinicia los indicadores cuando no est� siendo apretado.
        /// </summary>
        protected override void ReiniciarIndicadores()
        {
            if (_pulsacionActiva)
            {
                _arrastreActivo = false;
                _pulsacionActiva = false;

                if (_arrastreActivo)

                    _arrastreActivo = false;
            }

        }
        /// <summary>
        /// Si el bot�n es posible arrastrarlo (bot�n delizantes), a continuaci�n, realiza los c�lculos necesarios
        /// para actualizar la posici�n del bot�n.Cambiando las coordenadas de la psoici�n proxima desde el origen.
        /// </summary>
        /// <param name="rectangulo">Rectangulo de posici�n</param>
        private void CalcularArrastreBoton(Rectangle rectangulo)
        {
            if (_pulsacionActiva == true)
            {
                //Calcula la coordenada x de la nueva posici�n del bot�n devolviendo rectangulo.Center.X.
                //Devuelve left si rectangulo.Center.X<RestriccionesDesplazamiento.Left
                //y right rectangulo.Center.X>RestriccionesDesplazamiento.Right.(para no pasarse de la barra de arrastre.)
                float x = MathHelper.Clamp(rectangulo.Center.X, _RestriccionesDesplazamiento.Left,
                                              _RestriccionesDesplazamiento.Right);
                //Calcula la coordenada y de la nueva posici�n del bot�n devolviendo rectangulo.Center.Y.
                //Devuelve Top si rectangulo.Center.Y<RestriccionesDesplazamiento.Top
                //y Bottom rectangulo.Center.Y>RestriccionesDesplazamiento.Bottom.(para no pasarse de la barra de arrastre.)
                float y = MathHelper.Clamp(rectangulo.Center.Y, _RestriccionesDesplazamiento.Top,
                                              _RestriccionesDesplazamiento.Bottom);

                _PosicionProximaOrigen = new Vector2(x, y);
            }
            else
                _arrastreActivo = false;
        }
        /// <summary>
        /// Dibuja el bot�n.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            try
            {
                _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);


                _spriteBatch.Draw(_texturaBotonActivo, _PosicionDibujar, null, _ColorBoton);

                _spriteBatch.End();
            }
            catch (ArgumentNullException ex)
            {
                System.Diagnostics.Debug.WriteLine("Excepci�n encontrada al intentar dibujar la  textura asociada al bot�n" + this._NombreActivo + "   \n" + ex.Message);
                throw ex;
            }
            catch (NullReferenceException ex)
            {
                System.Diagnostics.Debug.WriteLine("Excepci�n encontrada al intentar dibujar la  textura asociada al bot�n" + this._NombreActivo + "   \n" + ex.Message);
                throw ex;
            }
            base.Draw(gameTime);
        }
        /// <summary>
        /// Actualiza el juego,calcula si un bot�n ha sido deslizado,calculando el rect�ngulo correspondiente.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //CASO BOTON DESLIZANTE
            if (_arrastreActivo == true)
            {
                Rectangle? rectangulo = TenerContactoRectangulo();
                CalcularArrastreBoton(rectangulo.GetValueOrDefault());
            }
        }
    }
}
