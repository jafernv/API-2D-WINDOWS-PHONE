
/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNAProyecto.ComponentesXNA
{
    public class Animacion : Componentes
    {
        #region CONSTRUCTORES

        public Animacion(Game game, string nombreRecurso, Texture2D imagen, Point dimensionFrame, Point dimensionImagen)
            : base(game, nombreRecurso)
        {
            _textura = imagen;
            _dimensionFrame = dimensionFrame;
            _dimensionImagen = dimensionImagen;
            this.Activo = true;
        }

        #endregion

        #region VARIABLES Y PROPIEDADES


        public Texture2D _textura;
        public Point _dimensionImagen;
        public Point _dimensionFrame;
        public Point _frameActual;
        
        private TimeSpan _ultimoCambioTimeSpan;
        /// <summary>
        /// Indica el tiempo que pasa entre frame y frame-->su velocidad.
        /// </summary>
        private TimeSpan _cambioTransicion = TimeSpan.Zero;
        private int _ultimoSubFrame=-1;

        private bool dibujarAhora = false;
        /// <summary>
        /// Final de la animacion.
        /// </summary>
        public int FinalFrame
        {
            get{
                return _dimensionImagen.X * _dimensionImagen.Y; }
        }


        public int IndiceFrame
        {
            get
            {
                return _dimensionImagen.X * _frameActual.Y + _frameActual.X;
            }
            set
            {
                if (value >= _dimensionImagen.X * _dimensionImagen.Y + 1)
                {
                    System.Diagnostics.Debug.WriteLine("Indice incorrecto");
                    throw new InvalidOperationException("El indice del frame no es correcto");
                }

                _frameActual.Y = value / _dimensionImagen.X;
                _frameActual.X = value % _dimensionImagen.X;
            }
        }

        public bool Activo { get ;  set; }


        #endregion
     
        #region METODOS


        public void Update(GameTime gameTime, bool dibujarlo){
            //Sí la animación está activa y no coincide el tick del juego con el tick de la animación
            if (Activo && gameTime.TotalGameTime != _ultimoCambioTimeSpan)
            {
                // Comprueba si el intervalo de tiempo entre frames está  definido.
                if (_cambioTransicion != TimeSpan.Zero)
                {
                    // Si no está definido espera al siguiente tick.
                    if (_ultimoCambioTimeSpan + _cambioTransicion > gameTime.TotalGameTime)
                    {return;} }

                //hacemos coincidir los ticks del juego y de la animación.
                //Comprobamos si el frame coincide con el final de linea.
                _ultimoCambioTimeSpan = gameTime.TotalGameTime;
                if (IndiceFrame >= FinalFrame){
                    //resetea la animación
                    IndiceFrame = 0; }
                else {
                    // ORDEN DE DIBUJAR-->SÓLO SI SE PERMITE POR EL PROGRAMADOR.
                    if (dibujarlo==true){           
                            // No avanzamos los frames hasta la primera orden de dibujado.
                            if (dibujarAhora)
                            {
                                _frameActual.X++;
                                if (_frameActual.X >= _dimensionImagen.X) {
                                    _frameActual.X = 0;
                                    _frameActual.Y++;}
                                if (_frameActual.Y >= _dimensionImagen.Y)
                                    _frameActual.Y = 0;

                                if (_ultimoSubFrame != -1)
                                    _ultimoSubFrame = -1;
                  }}}}}
      
        public void Draw(SpriteBatch spriteBatch, Vector2 posicion, float escalaDibujado,float rotacion, SpriteEffects spriteEffect)
        {
            dibujarAhora = true;
            try
            {
                spriteBatch.Draw(_textura, posicion,
                    new Rectangle(_dimensionFrame.X * _frameActual.X, _dimensionFrame.Y * _frameActual.Y, _dimensionFrame.X, _dimensionFrame.Y),
                    Color.White, rotacion, Vector2.Zero, escalaDibujado, spriteEffect, 0);
            }
            catch (InvalidOperationException e){
                System.Diagnostics.Debug.WriteLine("Debe inicializar el spriteBatch(usar begin/end)");
                throw e;
            }
            catch (ArgumentNullException e)
            {
                System.Diagnostics.Debug.WriteLine("El nombre de la textura no es correcto o no está cargada en memoria");
                throw e;
            }
        }

        /// <summary>
        /// Dibuja la animación en el indice dicho.
        /// </summary>
        /// <param name="indice">Frame index to play the animation from.</param>
        public void IndiceInicio(int indice)
        {
            IndiceFrame = indice;   
            dibujarAhora = false;
        }

        public void CambioEntreIntervalos(TimeSpan interval)
        {
            _cambioTransicion = interval;
        }


        #endregion
    }
}
