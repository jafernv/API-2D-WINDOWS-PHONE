/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAProyecto.ComponentesXNA
{
    public class PresentacionFija : Presentacion
    {
        public PresentacionFija(Game game, string nombreRecurso):base(game,nombreRecurso){    
           
        }
        public override void reinicializarMarcadores()
        {  
            base.reinicializarMarcadores();
        }
        public void botonDiapositivaAnterior(object sender, EventArgs e) {
            if (this.nDiapositiva > 0)
            {
                this.nDiapositiva--;
                this._texturaADibujar = this._presentacion.ElementAt(nDiapositiva)._imagen;
            }
        }
        public void botonDiapositivaSiguiente(object sender, EventArgs e)
        {
            if (this.nDiapositiva < this._presentacion.Count - 1)
            {
                this.nDiapositiva++;
                this._texturaADibujar = this._presentacion.ElementAt(nDiapositiva)._imagen;
                
            }
            else
                this._finPresentacion = true;

            
        }
        public void agregarDiapositiva(Texture2D textura)
        {
            if (_presentacion != null)
            {
                _conjunto con;
                con._imagen = textura;
                con._tiempoT = 0;
                this._presentacion.Add(con);
            }
            else
                System.Diagnostics.Debug.WriteLine("La presentación es nula  " + this._NombreActivo);
        }
    }
}
