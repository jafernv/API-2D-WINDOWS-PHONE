/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAProyecto.ComponentesXNA;
using XNAProyecto.Tiles;
using System;

namespace XNAProyecto.Jugador
{
   public  class Jugador : Componentes
    {
     
       private Game _game;
       public static Texture2D _textura;
       private SpriteBatch _spriteBatch;
       public static Vector2 _posicion=Vector2.Zero;
       public static float _velocidadJugador = 10f;
       public static Rectangle _scrollJugador=new Rectangle(0,0,0,0);
       public static Rectangle RectanguloJugador
       {
           get
           {
               return new Rectangle(
                   (int)_posicion.X,
                   (int)_posicion.Y,
                   _textura.Width,
                   _textura.Height);
           }
       }
       public static Rectangle RectanguloJugadorTransform
       {
           get
           {
               return Scrolling.Transform(RectanguloJugador);
           }
       }
       public bool permitirSaltar = false;
       public static int saltoMaximo = 200;
       public Jugador(Game game, Texture2D textura, Rectangle scrollJugador, Vector2 posOrigen)
           : base(game, "Jugador")
       {
          
           this._game = game;
           _textura = textura;
           _scrollJugador = scrollJugador;
           _posicion =posOrigen;
           
           game.Components.Add(this);
       }
       public Vector2 CentroJugadorDibujado
       {
           get{return Scrolling.Transform(_posicion);}
       }
       public override void Draw(GameTime gameTime)
       {
           _spriteBatch.Begin();
           //Al jugador hay que dibujarlo con un transform.
           _spriteBatch.Draw(_textura, CentroJugadorDibujado, Color.White);
           _spriteBatch.End();
           base.Draw(gameTime);
       }
       
       public override void Initialize()
       {
           
           base.Initialize();
           _spriteBatch = new SpriteBatch(GraphicsDevice);  
       }

       public void MoverJugador(MandoVirtual mandoVirtual,GameTime gameTime,MapaTiles mapa)
       {
           Vector2 mandoPosicion  = mandoVirtual.MandoVirtualPosicionNormalizado;

           if (mandoPosicion != Vector2.Zero)
           {
               Vector2 tmp = mandoPosicion * _velocidadJugador;
               
               Colisiones.ColisionarConBordes();//chocar con bordes
               Colisiones.scrollingMapaMover(gameTime, tmp);//scrolling camara
               tmp = Colisiones.ColisionesConTiles(gameTime, tmp, mapa);//mirar colisiones
              _posicion += tmp;}
           
       }
       public void MoverJugadorHorizontalmente(MandoVirtual mandoVirtual, GameTime gameTime, MapaTiles mapa)
       {
           this.permitirSaltar = true;
           Vector2 mandoPosicion = mandoVirtual.MandoVirtualPosicionNormalizado;

           if (mandoPosicion != Vector2.Zero)
           {
               Vector2 tmp = new Vector2(0, mandoPosicion.Y * _velocidadJugador);
             
               Colisiones.ColisionarConBordes();//chocar con bordes
               Colisiones.scrollingMapaMover(gameTime, tmp);//scrolling camara
               tmp = Colisiones.ColisionesConTiles(gameTime, tmp, mapa);//mirar colisiones
               _posicion += tmp;
           }

       }

       public void Salto( GameTime gameTime, MapaTiles mapa)
       {
           if (permitirSaltar != false)
           {
               int i = 0;
               Vector2 tmp = new Vector2(15, 0);
               if ((i != saltoMaximo)&&(tmp==new Vector2(15,0)))
               {
                   Colisiones.ColisionarConBordes();//chocar con bordes
                   Colisiones.scrollingMapaMover(gameTime, tmp);//scrolling camara
                   tmp = Colisiones.ColisionesConTiles(gameTime, tmp, mapa);//mirar colisiones
                   _posicion += tmp;
                   i = i + 15;
               }

              
           }

       }
       }
    }

