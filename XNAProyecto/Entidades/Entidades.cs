/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System.Collections.Generic;
using XNAProyecto.ComponentesXNA;
using Microsoft.Xna.Framework;

namespace XNAProyecto.Entidades
{
   public class Entidades : GameComponent
    {

       private Dictionary<string, Componentes> listaComponentes;
       private Game _game;


       public Entidades(Game game) : base(game) {
           this.listaComponentes = new Dictionary<string, Componentes>();
           this._game = game;
       }
       
       public void AddComponente(Componentes componente) {
           this.listaComponentes.Add(componente._NombreActivo,componente);
       }
    }
}
