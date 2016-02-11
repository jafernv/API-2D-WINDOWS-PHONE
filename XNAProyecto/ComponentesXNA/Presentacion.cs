/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAProyecto.ComponentesXNA
{
    internal struct _conjunto
    {
        internal Texture2D _imagen;
        internal float _tiempoT;
    };
      
    public abstract class Presentacion : Componentes
    {
        public Presentacion(Game game, string nombreRecurso)
            : base(game, nombreRecurso)
        {
            this._presentacion = new List<_conjunto>();
            game.Components.Add(this);
        }
        #region VARIABLES Y PROPIEDADES
        protected SpriteBatch _spriteBatch;
        internal List<_conjunto> _presentacion;
        protected bool _saltarTransicion = false;
        public bool _SaltarTransicion
        {
            get { return _saltarTransicion; }
            set { _saltarTransicion = value; }
        }
         protected bool _finPresentacion = false;
         public bool FinPresentacion
         {
             get { return _finPresentacion; }
         }
         protected Texture2D _texturaADibujar;
         protected int nDiapositiva = 0;
        #endregion

        #region METODOS
         public void botonSaltarPresentacion(object sender, EventArgs e)
         {
             if (_saltarTransicion == true)
                 _finPresentacion = true;

         }
         public virtual void reinicializarMarcadores()
         {
             this.nDiapositiva = 0;
             this._finPresentacion = false;
            
         }
         public override void Initialize()
         {
             base.Initialize();
             _spriteBatch = new SpriteBatch(GraphicsDevice);
             //La primera textura a dibujar es la 0
             if ((_presentacion != null) && (_presentacion.Count != 0))
                 _texturaADibujar = _presentacion.ElementAt(nDiapositiva)._imagen;
             else
                 System.Diagnostics.Debug.WriteLine("Inicie la presentación con alguna imagen  " + this._NombreActivo);
         }
         public override void Draw(GameTime gameTime)
         {
             try
             {
                 _spriteBatch.Begin();
                 GraphicsDevice.Clear(Color.Black);
                 if (_texturaADibujar != null)
                     _spriteBatch.Draw(this._texturaADibujar, Vector2.Zero, Color.White);
                 
                 _spriteBatch.End();
             }
             catch (ArgumentNullException ex)
             {
                 System.Diagnostics.Debug.WriteLine("Excepción encontrada al intentar dibujar la  textura asociada " + this._NombreActivo + "   \n" + this._texturaADibujar + "   \n" + ex.Message);
                 throw ex;
             }
             catch (NullReferenceException ex)
             {
                 System.Diagnostics.Debug.WriteLine("Excepción encontrada al intentar dibujar la  textura asociada " + this._NombreActivo + "   \n" + ex.Message);
                 throw ex;
             }
             base.Draw(gameTime);
         }
         public override void Update(GameTime gameTime)
         {
             if (_SaltarTransicion == true)
             {
                 //SALIR AL PULSAR TECLA "<-"
                 if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed))
                 {
                     this._finPresentacion = true;
                     Dispose();
                 }

             }
             //Si es la última diapositiva,la presentación finaliza.
             if (this.nDiapositiva >= _presentacion.Count)
                 this._finPresentacion = true;
             if (_finPresentacion != false)
               Dispose();
             base.Update(gameTime);
         }
         #endregion
    }
}
