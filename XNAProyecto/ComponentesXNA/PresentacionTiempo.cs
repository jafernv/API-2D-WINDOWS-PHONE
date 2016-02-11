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
  
    public  class PresentacionTiempo : Presentacion
    {
        public PresentacionTiempo(Game game, string nombreRecurso):base(game,nombreRecurso){    
            _inicio = false; 
        }
        
        
        public void agregarDiapositiva(Texture2D textura){
            if (_presentacion != null)
            {
                _conjunto con;
                con._imagen = textura;
                con._tiempoT = this._TiempoTransicionGeneral;
                this._presentacion.Add(con);
            }
            else
                System.Diagnostics.Debug.WriteLine("La presentación es nula  "+ this._NombreActivo);
        }
        public void agregarDiapositiva(Texture2D textura,float tiempo) {
            if (this._presentacion != null)
            {
                _conjunto con;
                con._imagen = textura;
                if (tiempo >= 0)
                {
                    con._tiempoT = tiempo;
                    this._presentacion.Add(con);
                }
                else
                    System.Diagnostics.Debug.WriteLine("El tiempo tiene que ser mayor que 0 en  "+this._NombreActivo);
                
            }
            else
                System.Diagnostics.Debug.WriteLine("La presentación es nula " + this._NombreActivo);
        }
        public override void reinicializarMarcadores() {
            this._inicio = false;
            base.reinicializarMarcadores();
        }


     
        /// <summary>
        /// Encargado de medir el tiempo entre imagenes
        /// </summary>
        /// <returns>booleano</returns>
        protected bool Esperar( GameTime gameTime,float tiempo)
        {
            //Primero coge el tiempo inicial (tiempo total jugado).
            if (_inicio == false)
            {
                timeEsperaInicial = (float)gameTime.TotalGameTime.TotalSeconds;
                _inicio = true;  
                return true;
            }
            //Va comparando si el tiempo inicial más el tiempo de espera es menor que el tiempo total jugado hasta ahora
            else if (timeEsperaInicial + tiempo <= (float)gameTime.TotalGameTime.TotalSeconds)
                return false;
            else {
                return true; } }
        #region VARIABLES Y PROPIEDADES
        
     
        protected float _tiempoTransicionGeneral = 2f;
        public float _TiempoTransicionGeneral
        {
            get { return _tiempoTransicionGeneral; }
            set { if(value>=0)_tiempoTransicionGeneral = value; }
        }
        

       
        
        internal bool _inicio;//usado solo en la accion "esperar",interiormente
        internal float timeEsperaInicial;
        #endregion
        #region METODOS GAME
        public override void Update(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
           //mientras haya diapositivas a presentar,el juego espera el número de segundos indicado por cada diapositiva.
            if ((this.nDiapositiva < _presentacion.Count) && (Esperar(gameTime, _presentacion.ElementAt(nDiapositiva)._tiempoT) == false))
                {

                    this._inicio = false;
                    //pasa a la siguiente diapositiva para dibujarla.
                    if (this.nDiapositiva + 1 < this._presentacion.Count)
                        this._texturaADibujar = this._presentacion.ElementAt(nDiapositiva + 1)._imagen;
                    this.nDiapositiva++;
                }
            
 
            base.Update(gameTime);
        }
       
       
       
        
        #endregion

        
    }
}
