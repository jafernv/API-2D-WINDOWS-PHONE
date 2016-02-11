/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using XNAProyecto.ComponentesXNA;

namespace XNAProyecto.Menus
{
    public class AdministradorPantallas : DrawableGameComponent
    {
       
        public SpriteBatch spriteBatch;
        public Game game;
        public TouchLocation? _touchLocation;
        EstadoPadTouchPanel entrada = new EstadoPadTouchPanel(); 
        List<TiposP> pantallas = new List<TiposP>();
        List<TiposP> pantallasListaActualizar = new List<TiposP>();  
      
  
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

       
        public AdministradorPantallas(Game game)
            : base(game)
        {
            this.game = game;
            TouchPanel.EnabledGestures = GestureType.None;//control de gestos lo activamos después.
        }

        #region METODOS COMUNES


       
        public override void Initialize()
        {
            cargarCosasComunes();
            base.Initialize();        
        }
        public virtual void cargarCosasComunes() {}
        protected override void LoadContent()
        {      
            
            ContentManager content = Game.Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (TiposP screen in pantallas)
            {
                screen.LoadContent();
            }
        }
        protected override void UnloadContent()
        {         
            foreach (TiposP screen in pantallas)
            {
                screen.UnloadContent();
            }
        }


        #endregion
       
        public override void Update(GameTime gameTime)
        {
            bool focoPantalla = !Game.IsActive;
            
            // Actualizamos la pantalla tactil y el pad.
            entrada.Update();

            // Vaciamos la lista de pantallas a actualizar anteriores.
            pantallasListaActualizar.Clear();
            //Añadimos todas las pantallas.
            foreach (TiposP pantalla in pantallas)
                pantallasListaActualizar.Add(pantalla);
         
            while (pantallasListaActualizar.Count > 0)
            {
                // A partir de ahora usamos la lista normal,quitando la pantalla anterior.
                //(es la primera a actualizar,la qe esta en activo)
                TiposP pantalla = pantallasListaActualizar[pantallasListaActualizar.Count - 1];
                pantallasListaActualizar.RemoveAt(pantallasListaActualizar.Count - 1);
                pantalla.Update(gameTime, false);
                //Si la pantalla está activa.
                if (pantalla.Estado == EstadoP.activo){
                  //Si es la primerapantalla, activamos panel tactil.
                    if (!focoPantalla) {
                        pantalla.BackBoton(entrada);
                        pantalla.ControlTactil(gameTime, entrada);
                        focoPantalla = true;} }
                //CONTROL TACTIL PARA BOTONES
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
            }

        }



        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black); //Pantalla de fondo negro
            foreach (TiposP pantalla in pantallas)
            {
                if (pantalla.Estado == EstadoP.activo)
                pantalla.Draw(gameTime);
            }
        }


        
        #region Public Methods


        public void NuevaPantalla(TiposP pantalla)
        {
            
            pantalla.ControlMenus = this;
            pantalla.LoadContent();
            pantallas.Add(pantalla);
            TouchPanel.EnabledGestures = pantalla.GestureType;
        }


        /// <summary>
        /// Elimina una pantalla.Se usa en el evento BackBoton automáticamente.
        /// </summary>
        public void EliminarPantalla(TiposP pantalla)
        {
            pantalla.UnloadContent();
            pantallas.Remove(pantalla);
            pantallasListaActualizar.Remove(pantalla);
            if (pantallas.Count > 0){
                TouchPanel.EnabledGestures = pantallas[pantallas.Count - 1].GestureType;
                //miramos el tipo de pulsación del usuario.
            }
        }

        #endregion
       
    }
}
