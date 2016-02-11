/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAProyecto.ComponentesXNA
{
   
    public class BotonFijo : Botones
    {
        /// <summary>
        /// Constructor de "BotonFijo".
        /// </summary>
        /// <param name="game">Juego</param>
        /// <param name="nombreRecurso">Nombre del recurso</param>
        /// <param name="texturaActiva">Textura del botón por defecto</param>
        public BotonFijo(Game game, string nombreRecurso, Texture2D texturaActiva)
            : base(game, nombreRecurso,texturaActiva) {
               
        }

     
        /// <summary>
        /// Dibuja el botón.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (_botonUsaUpdate == true)
            {
                try
                {
                    _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

                    if ((_CambioEstadoTextura == false))
                    {
                        _spriteBatch.Draw(_texturaBotonActivo, _PosicionDibujar, _ColorBoton);


                    }
                    else
                        _spriteBatch.Draw(_TexturaBotonEstado2, _PosicionDibujar, _ColorBoton);

                    _spriteBatch.End();
                }
                catch (ArgumentNullException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Excepción encontrada al intentar dibujar la  textura asociada al botón" + this._NombreActivo + "   \n" + ex.Message);
                    throw ex;
                }
                catch (NullReferenceException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Excepción encontrada al intentar dibujar la  textura asociada al botón" + this._NombreActivo + "   \n" + ex.Message);
                    throw ex;
                }
                base.Draw(gameTime);
            }
        }
    }
}
