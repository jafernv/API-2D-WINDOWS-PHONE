/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XNAProyecto.ComponentesXNA;

namespace XNAProyecto.Tiles
{
    public class MapaTiles : Componentes
    {
        #region PROPIEDADES Y VARIABLES
        private Game game;
        private SpriteBatch spriteBatch;
        private int _anchoMapa = 0;
        private int _largoMapa = 0;
        private int _anchoTile = 0;
        private  int _largoTile = 0;
        
        public int _numeroFilasTileSinColision = 0; 
        public int _numeroColumnasTileSinColision = 0;
        public int _numeroFilasTileConColision = 0;
        public int _numeroColumnasTileConColision = 0;
        private int nTileSeparacion = 0;//indica donde empiezan los tiles con colision y sin colision.

        public int NTileSeparacion
        {
            get { return nTileSeparacion; }   
        }
        private Texture2D _texturaConColision;
        private Texture2D _texturaSinColision;
        private bool _mapaIniciado=false;//Se ha llamado a la acción Inicio
        private bool _mapaGenerado = false;//Se ha generado el mapa
        private List<Rectangle> tiles = new List<Rectangle>();

        public List<Rectangle> Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }
       
        static private int[,] mapa;

        public  int ObtenerTile(int x, int y)
        {
            if ((x >= 0) && (x < _anchoMapa) &&
                (y >= 0) && (y < _largoMapa))
            {
                return mapa[x, y];
            }
            else
                return 0;

        }
        
        public int ObtenerMapaX(int x)
        {
            return x / _anchoTile;
        }
        public int ObtenerMapaY(int y)
        {
            return y / _largoTile;
        }
        public Rectangle ObtenerRectanguloTile(int x, int y)
        {
            return Scrolling.Transform(new Rectangle(
                x * _anchoTile,
                y * _largoTile,
                _anchoTile,
                _largoTile));
        }
        public bool tileConColision(Vector2 tile) 
         {
                int x= ObtenerMapaX((int)tile.X);
                int y =ObtenerMapaY((int)tile.Y);
                 //Miramos si el tile es con o sin colisión.
                int tileIndice = ObtenerTile(x, y);
                //tile no existe,está en la frontera.
                if (tileIndice == 0)
                    return false;
                return tileIndice >= NTileSeparacion;
         }
        
        #endregion
        public MapaTiles(Game game,int AnchoMapa,int LargoMapa,int AnchoTile,int LargoTile):base(game,"Mapa") {
            this.game = game;
            if (AnchoMapa > 0)
                this._anchoMapa = AnchoMapa;
            else
                System.Diagnostics.Debug.WriteLine("El ancho del mapa debe ser mayor que cero");
            if(LargoMapa>0)
            this._largoMapa = LargoMapa;
            else
                System.Diagnostics.Debug.WriteLine("El largo del mapa debe ser mayor que cero");
            if (AnchoTile > 0)
                this._anchoTile = AnchoTile;
            else
            {
                System.Diagnostics.Debug.WriteLine("El ancho del tile debe ser mayor que cero");
                throw new Exception("El ancho del tile no puede ser cero");
            }
            if (LargoTile > 0)
                this._largoTile = LargoTile;
            else{
                System.Diagnostics.Debug.WriteLine("El largo del tile debe ser mayor que cero");
                throw new Exception("El largo del tile no puede ser cero");
            }
          
            mapa = new int[_anchoMapa, _largoMapa];
            Scrolling.TamañoMundo = new Rectangle(0, 0, _anchoMapa * _anchoTile, _largoMapa * _largoTile);
           
        }
        #region METODOS
        public void Iniciar(Texture2D TexturaConColision,int NumeroFilasConColision,int NumeroColumnasConColision,
            Texture2D TexturaSinColision,int NumeroFilasSinColision,int NumeroColumnasSinColision) {
            if (TexturaConColision != null)
                this._texturaConColision = TexturaConColision;
            else
                System.Diagnostics.Debug.WriteLine("La textura con colisiones no puede ser nula");
            if (TexturaSinColision != null)
                this._texturaSinColision = TexturaSinColision;
            else
                System.Diagnostics.Debug.WriteLine("La textura sin colisiones no puede ser nula");

            if (NumeroColumnasSinColision > 0)
                this._numeroColumnasTileSinColision = NumeroColumnasSinColision;
            else
                System.Diagnostics.Debug.WriteLine("El número de columnas sin colisión debe ser mayor que cero");
            if (NumeroFilasSinColision > 0)
                this._numeroFilasTileSinColision = NumeroFilasSinColision;
            else
                System.Diagnostics.Debug.WriteLine("El número de filas sin colisión debe ser mayor que cero");
            if (NumeroColumnasConColision > 0)
                this._numeroColumnasTileConColision = NumeroColumnasConColision;
            else
                System.Diagnostics.Debug.WriteLine("El número de columnas con colisión debe ser mayor que cero");
            if (NumeroFilasConColision > 0)
                this._numeroFilasTileConColision = NumeroFilasConColision;
            else
                System.Diagnostics.Debug.WriteLine("El número de filas con colisión debe ser mayor que cero");

            tiles.Clear();

            for (int columna = 0; columna < _numeroColumnasTileConColision; columna++)
            {
                for (int fila = 0; fila < _numeroFilasTileConColision; fila++)
                {
                    tiles.Add(new Rectangle(columna * _anchoTile,fila * _largoTile, _anchoTile, _largoTile));
                    nTileSeparacion++;
                    System.Diagnostics.Debug.WriteLine(NTileSeparacion);
                }
            }
           
            for (int columna = 0; columna < _numeroColumnasTileSinColision; columna++)
            {
                for (int fila = 0; fila < _numeroFilasTileSinColision; fila++)
                {
                    tiles.Add(new Rectangle(columna * _anchoTile,fila * _largoTile , _anchoTile, _largoTile));
                  
                }
            }
            this._mapaIniciado = true;
            }

        public void GenerarMapaAleatorio() {
            if (_mapaIniciado == false)
                throw new Exception("Inicie el mapa llamando a la acción 'Iniciar'");
            else {
                Random random1 = new Random();
                Random random2 = new Random();
                 int sinColisionTile;
                 int conColisionTile;
                for (int x = 0; x < _anchoMapa; x++) {
                    for (int y = 0; y < _largoMapa; y++){
                        //CUBRIMOS TODO DE BALDOSAS SIN COLISIÓN.
                        sinColisionTile = random1.Next(0, nTileSeparacion);
                        mapa[x, y] = sinColisionTile;
                        
                        //ponemos colisiones aleatorias
                        if (random2.Next(0, 20) > 16)
                        {
                            conColisionTile = random2.Next(NTileSeparacion, tiles.Count - 1);
                            mapa[x, y] = conColisionTile;
                        }
                    }}//cierres for
                this._mapaGenerado = true;
            }
        }
        public void GenerarMapaHorizontal() {
            if (_mapaIniciado == false)
                throw new Exception("Inicie el mapa llamando a la acción 'Iniciar'");
            else
            {
                Random random1 = new Random();
                Random random2 = new Random();
                int sinColisionTile;
                int conColisionTile;
                for (int x = 0; x < _anchoMapa; x++){
                    for (int y = 0; y < _largoMapa; y++){
                        //CUBRIMOS TODO DE BALDOSAS SIN COLISIÓN.
                        sinColisionTile = random1.Next(0,NTileSeparacion);
                        mapa[x, y] = sinColisionTile;
                    }}
                for (int y = 0; y < _largoMapa; y++)
                {

                    conColisionTile = random2.Next(10, 14);
                    mapa[0, y] = conColisionTile;
                    conColisionTile = random2.Next(10, 14);
                    mapa[1, y] = conColisionTile;
                    conColisionTile = random2.Next(10, 14);
                    mapa[2, y] = conColisionTile;
                    conColisionTile = random2.Next(8, 10);
                    mapa[3, y] = conColisionTile;
                    mapa[4, y] = 5;
                }
                for (int y = 0; y < _largoMapa; y++)
                {
                    if (random2.Next(0, 20) > 12)
                    {
                        conColisionTile = random2.Next(14, 16);
                        mapa[6, y] = conColisionTile;
                    }
                    if (random2.Next(0, 20) > 12)
                    {
                        conColisionTile = random2.Next(14, 16);
                        mapa[7, y] = conColisionTile;
                    }
                    
                  
                }
                this._mapaGenerado = true;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            if (_mapaGenerado == false || _mapaIniciado == false)
                throw new Exception("Inicie y genere el mapa para poder dibujarlo");
            else
            {

                //Obtenemos las posiciones del scrolling para dibujar sólo la parte de la pantalla correspondiente
                int inicioX = ObtenerMapaX((int)Scrolling.Posicion.X);
                int startY = ObtenerMapaY((int)Scrolling.Posicion.Y);

                int finalX = ObtenerMapaX((int)Scrolling.Posicion.X + (int)Scrolling.PosicionVistaX);
                int finalY = ObtenerMapaY((int)Scrolling.Posicion.Y + (int)Scrolling.PosicionVistaY);
                try
                {
                    for (int ancho = inicioX; ancho <= finalX; ancho++)
                        for (int alto = startY; alto <= finalY; alto++)
                        {
                            
                                spriteBatch.Begin();
                                if (ObtenerTile(ancho, alto) < nTileSeparacion)
                                    spriteBatch.Draw(
                                        _texturaSinColision,
                                        ObtenerRectanguloTile(ancho, alto),
                                        tiles[ObtenerTile(ancho, alto)],
                                        Color.White);
                                else
                                    spriteBatch.Draw(
                                 _texturaConColision,
                                 ObtenerRectanguloTile(ancho, alto),
                                 tiles[ObtenerTile(ancho, alto)],
                                 Color.White);
                                spriteBatch.End();
                            
                        }
                }

                catch (NullReferenceException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Las imagenes son nulas o el mapa no esta inicializado(llamar a 'Initialize')");
                    throw ex;
                }
                catch (ArgumentNullException ex) {
                    System.Diagnostics.Debug.WriteLine("Las imagenes no son correctas o el mapa no esta inicializado(llamar a 'Initialize')");
                    throw ex;
                }
            }
            base.Draw(gameTime);
        }
        public override void  Initialize()
        {

 	     base.Initialize();
         spriteBatch = new SpriteBatch(GraphicsDevice);     
        }
       
        #endregion
    }
}
