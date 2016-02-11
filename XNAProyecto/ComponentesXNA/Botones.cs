/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace XNAProyecto.ComponentesXNA
{   
    /// <summary>
    /// Clase abstracta que se encarga de los botones.
    /// </summary>
    public abstract class Botones: Componentes
   {
       /// <summary>
       /// Constructor de Botones.
       /// </summary>
       /// <param name="game"></param>
       /// <param name="nombreRecurso"></param>
       /// <param name="texturaActiva"></param>
       public Botones(Game game, string nombreRecurso, Texture2D texturaActiva )
           :base(game,nombreRecurso)
       {
           this._texturaBotonActivo = texturaActiva;
           this._ColorBotonApretado = Color.Green;
           this._ColorBotonDesactivado = Color.Gray;
           this._ColorBotonNormal= Color.Beige;
          
           game.Components.Add(this);
       }


    
       #region VARIABLES Y PROPIEDADES
        /// <summary>
        /// SpriteBatch de Botones.
        /// </summary>
       internal SpriteBatch _spriteBatch;

       public bool _botonUsaUpdate = true;
        /// <summary>
        /// Indica que el botón está siendo pulsado.
        /// </summary>
        internal bool _pulsacionActiva;
        /// <summary>
        /// Indica si un botón puede ser pulsado.
        /// </summary>
        private bool _botonDesactivado = false;
        /// <summary>
        /// Modifica y obtiene el valor booleano que indica si un botón puede ser pulsado.
        /// </summary>
        public bool _BotonDesactivado
        {
            get { return _botonDesactivado; }
            set { _botonDesactivado = value; }
        }
        
        #region TEXTURAS y POSICIONES
        /// <summary>
        /// Textura asociada al botón en estado activo.
        /// </summary>
        internal Texture2D _texturaBotonActivo;
        /// <summary>
        /// Ubicación táctil del dispositivo de pantalla,este puede ser nulo.
        /// </summary>
        private static TouchLocation? _touchLocation;
        /// <summary>
        /// Obtiene y permite modificar la ubicación táctil.
        /// </summary>
        public static TouchLocation? _TouchLocation
        {
            get { return Botones._touchLocation; }
            set { Botones._touchLocation = value; }
        }


        
        /// <summary>
        /// Inidica los límites de la pantalla del Botón desde el eje superior izquierdo.
        /// </summary>
        internal Rectangle _LimitesPantallaBoton
        {
            get
            {
                return new Rectangle(
                    (int)_PosicionSuperiorIzquierdaBoton.X,
                    (int)_PosicionSuperiorIzquierdaBoton.Y,
                    (int)_texturaBotonActivo.Width,
                    (int)_texturaBotonActivo.Height);
            }
        }
        /// <summary>
        /// Posición en pantalla de la parte superior izquierda de la esquina del botón.
        /// </summary>
        internal Vector2 _PosicionSuperiorIzquierdaBoton
        {
            get { return _PosicionProximaOrigen - _PosicionOrigen; }
            set { _PosicionProximaOrigen = value + _PosicionOrigen; }
        }
        /// <summary>
        /// Posicion  del botón.
        /// </summary>
        internal Vector2 _posicion = Vector2.Zero;
        /// <summary>
        /// Devuelve la posición de origen del botón.
        /// </summary>
        public Vector2 _PosicionOrigen { get; set; }
        /// <summary>
        /// La localización en pantalla donde el origen de la textura será dibujada/situada. 
        /// </summary>
        public Vector2 _PosicionProximaOrigen
        {
            get { return _posicion; }
            set
            {
                if (_posicion != value)
                {
                    _posicion = value;
                   PulsarPosicionCambiada(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// El centro de la textura.(Las texturas de botón activo y desactivado deben ser del mismo tamaño).
        /// Suele usarse como posición de origen.
        /// Usamos un try para no devolver un null.
        /// </summary>
        public Vector2 _CentroTextura
        {
            get
            {
                try
                {
                    return new Vector2((float)(_texturaBotonActivo.Width / 2), (float)(_texturaBotonActivo.Height / 2));
                }
                catch (NullReferenceException ex)
                {
                    System.Diagnostics.Debug.WriteLine("La imagen " + this._NombreActivo + " es nula");
                    throw ex;
                }
            }


        }
        /// <summary>
        ///  Posición en la que se dibuja el boton respecto al origen.
        /// </summary>
        public Vector2 _PosicionDibujar
        {
            get { return _PosicionProximaOrigen - _PosicionOrigen; }
        }
        /// <summary>
        /// Textura asociada al botón cuando cambia de estado.
        /// </summary>
        private Texture2D _texturaBotonEstado2 = null;
        /// <summary>
        /// Devuelve y modifica la textura asociada al botón cuando cambia de estado.
        /// </summary>
        public Texture2D _TexturaBotonEstado2
        {
            get { return _texturaBotonEstado2; }
            set { _texturaBotonEstado2 = value; }
        }
        /// <summary>
        /// Referencia para cambiar el estado ,cuando se permite cambiar de estado(_CambioEstado).
        /// </summary>
        private bool _cambioEstadoTextura = false;
        /// <summary>
        /// Estado inicial del botón.
        /// </summary>
        private bool _cambioEstado = false;
        /// <summary>
        /// Permite cambiar el estado inicial.
        /// </summary>
        public bool _CambioEstado
        {
            get { return _cambioEstado; }
            set { if (_cambioEstado == false)
                _cambioEstadoTextura = false;
                _cambioEstado = value; }
        }
        
        /// <summary>
        /// Cambia el estado internamente.
        /// </summary>
        protected bool _CambioEstadoTextura
        {
            get { return _cambioEstadoTextura; }
             set
            {
                if (_texturaBotonEstado2 != null)
                    _cambioEstadoTextura = value;
            }
        }
        #endregion
        #region COLORES
        /// <summary>
        /// Color aplicado cuando se está dibujando el botón(en los tres estados).
        /// </summary>
        protected Color _ColorBoton
        {
            get
            {   //Si el color está desactivado,devuelve el valor del botón en estado desactivado.
                Color color;
                if (_BotonDesactivado == true)
                    color = _ColorBotonDesactivado.Value;
                else
                {
                    //Comprueba si se debe llamar a GameComponent.Update, si es así,comprueba que el botón está pulsado.
                    //Si está pulsado devuelve el color del botón en estado pulsado(se comprueba sí no es nulo
                    //,ya que puede serlo ),sino devuelve el color original.
                    color = (Enabled == true ?
                        (_pulsacionActiva == true ? _ColorBotonApretado ?? _colorBoton : _colorBoton)
                        : _colorBoton);
                }

                return color;
            }
            set
            { _colorBoton = value;}
        }
        /// <summary>
        /// Color aplicado cuando se está dibujando el botón en estado activo(el botón está pulsado).
        /// </summary>
        public Color? _ColorBotonApretado { get; set; }
        /// <summary>
        /// Color aplicado cuando se está dibujando el botón en un estado desactivado.
        /// </summary>
        public Color? _ColorBotonDesactivado { get; set; }
        /// <summary>
        /// Color del botón en estado normal.
        /// </summary>
        private Color _colorBoton;
        /// <summary>
        /// Color del botón por defecto
        /// </summary>
        public Color _ColorBotonNormal
        {
            get { return _colorBoton; }
            set { _colorBoton = value; }
        }
        #endregion
        //BOTONES DELIZANTES
        internal bool _arrastreActivo;
        /// <summary>
        /// Indica cuando el botón puede ser arrastrado.
        /// </summary>
        public bool _PermitirArrastre { get; set; }

        /// <summary>
        /// Se utiliza en botones con mango de deslizamiento/ límites de arrastre (por lo general un eje).
        /// </summary>
        public Rectangle _RestriccionesDesplazamiento { get; set; }
       #endregion
       #region EVENTOS
        public event EventHandler Click;
        /// <summary>
        /// Provoca el evento Click si algún oyente se adjunta.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void PulsarClick(EventArgs e)
        {
            if (Click != null)
            {
                Click.Invoke(this, e);
            }
        }
        /// <summary>
        /// Provoca el evento TouchDown si algún oyente se adjunta.
        /// </summary>
        public event EventHandler TouchDown;
        /// <summary>
        /// Lanza el evento cuando un dedo toca la pantalla en un elemento y este se desplaza. 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void PulsarTouchDown(EventArgs e)
        {
            if (TouchDown != null)
            {
                TouchDown.Invoke(this, e);
            }
        }

        /// <summary>
        /// Establece indicadores válidos cuando el botón es pulsado.
        /// Lanza el evento PulsarTouchDown, que es cuando el dedo toca la pantalla en un elemento y este se desplaza
        /// </summary>
        protected virtual void LanzarTouchDown()
        {
            PulsarTouchDown(EventArgs.Empty);
            _pulsacionActiva = true;
            

        }
        /// <summary>
        /// Evento para botones deslizantes
        /// </summary>
        public event EventHandler PosicionCambiada;
        /// <summary>
        /// Lanza el evento cuando un elemento cambia de estado o posición. 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void PulsarPosicionCambiada(EventArgs e)
        {
            if (PosicionCambiada != null)
            {
                PosicionCambiada.Invoke(this, e);
            }
        }
        #endregion Events
       #region METODOS COMUNES(UPDATE,INITIALIZE....)
        /// <summary>
        /// Inicializa un botón.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            _spriteBatch = new SpriteBatch(GraphicsDevice);      
        }


        /// <summary>
        /// Cada "frame" se comprueba si es pulsado , liberado o arrastrado
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
           
                //Obtenemos el rectángulo del "touchLocation".
                Rectangle? rectangulo = TenerContactoRectangulo();

                //Si el botón está desactivado no hace falta que se compruebe en cada momento si se pulsará ese botón.
                if (!_BotonDesactivado)
                {
                    //COMPARA AMBOS RECTÁNGULOS(TOUCHDOWN Y EL BOTÓN) Y SI COINCIDEN LLAMA AL EVENTO.
                    if (ComprobarTouchDown(rectangulo) == true)
                    {
                        LanzarTouchDown();
                        if (_CambioEstado == true)
                        {
                            if (_CambioEstadoTextura == false)
                                _CambioEstadoTextura = true;
                            else
                                _CambioEstadoTextura = false;
                        }
                    }
                    //LIBERA EL BOTÓN UNA VEZ PULSADO
                    else if (_pulsacionActiva == true && CompruebaSiAunEstaPresionado(rectangulo))
                        LiberarBoton();

                        //CASO CUANDO NO SE PULSA NADA,Y SE REINICIA TODOS LOS INDICADORES
                    else if (!ComprobarSiEstaPulsado(rectangulo))

                        ReiniciarIndicadores();

                }

                base.Update(gameTime);
            
        }
        protected override void Dispose(bool disposing)
        {
           
            base.Dispose(disposing);
        }
       #endregion
       #region METODOS PRIVADOS

        /// <summary>
        /// Comprueba el estado del botón(apretado) y crea un 
        /// pequeño rectángulo en torno a la posición de contacto si está disponible
        /// </summary>
        /// <returns>Pequeño rectángulo en torno a la posición de contacto</returns>
        protected Rectangle? TenerContactoRectangulo()
        {
            Rectangle? contactoRectangulo = null;



            if (_TouchLocation != null && _TouchLocation.Value.State != TouchLocationState.Invalid)
            {

                //Creamos un rectangulo con las coordenadas x,y de la ubicación táctil y de anchura y largura 10.
                //Si ponemos mucho más de 10 puede que al apretar toquemos más de un botón.
                contactoRectangulo = new Rectangle((int)_touchLocation.Value.Position.X - 3,
                                            (int)_touchLocation.Value.Position.Y - 3,
                                            10, 10);
            }
            return contactoRectangulo;
        }
        
        /// <summary>
        ///Se le llama en cada fotograma  para comprobar si el botón se ha pulsado.(sólo una vez)
        /// </summary>
        /// <param name="rectangulo">Pequeño rectángulo en torno a la posición de contacto si está disponible</param>
        /// <returns>Devuelve un booleano para comprobar si se ha pulsado el botón</returns>
        internal bool ComprobarTouchDown(Rectangle? rectangulo)
        {
            //Comprobamos si el rectángulo del botón hace intersección con el rectángulo de la pulsación táctil.
            bool pulsarBoton = false;
            if (!_pulsacionActiva == true && rectangulo != null)
            {
                pulsarBoton = _LimitesPantallaBoton.Intersects(rectangulo.Value);
            }

            return pulsarBoton;
        }

        /// <summary>
        /// Se le llama cada cuadro para comprobar si aún está 'presionado'
        /// </summary>
        /// <param name="rectangulo">Pequeño rectángulo en torno a la posición de contacto si está disponible</param>
        /// <returns>True si todavía está presionado. False en caso contrario</returns>
        private bool CompruebaSiAunEstaPresionado(Rectangle? rectangulo)
        {
            bool estaPresionado = false;
            if (_pulsacionActiva)
                if (rectangulo == null)
                   estaPresionado = true;

            return estaPresionado;
        }
        /// <summary>
        /// Se le llama en  cada fotograma  para comprobar si  está el botón pulsado.
        /// </summary>
        /// <param name="rectangulo">Pequeño rectángulo en torno a la posición de contacto si está disponible</param>
        /// <returns>Devuelve un booleano para comprobar si esta siendo pulsado el botón</returns>
        private bool ComprobarSiEstaPulsado(Rectangle? rectangulo)
        {
            bool estaPulsado = false;

            if (rectangulo != null)
                //si el rectangulo coincide con los limites del botón, este está pulsado.
                estaPulsado = _LimitesPantallaBoton.Intersects(rectangulo.Value);

            return estaPulsado;
        }
        /// <summary>
        ///Realiza acciones para desbloquear el botón.
        /// </summary>
        private void LiberarBoton()
        {
            if (_pulsacionActiva)
            {
                ReiniciarIndicadores();
                PulsarClick(EventArgs.Empty);
            }
        }
       
        /// <summary>
        /// Reinicia los indicadores cuando no está siendo apretado.
        /// </summary>
        protected virtual void ReiniciarIndicadores()
        {
            if (_pulsacionActiva)
                _pulsacionActiva = false;         
        }
       
       #endregion

       
   }
}
