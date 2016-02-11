/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISE�O Y ESPICIFICACI�N DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERN�NDEZ VILLANUEVA
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
        /// Indica que el bot�n est� siendo pulsado.
        /// </summary>
        internal bool _pulsacionActiva;
        /// <summary>
        /// Indica si un bot�n puede ser pulsado.
        /// </summary>
        private bool _botonDesactivado = false;
        /// <summary>
        /// Modifica y obtiene el valor booleano que indica si un bot�n puede ser pulsado.
        /// </summary>
        public bool _BotonDesactivado
        {
            get { return _botonDesactivado; }
            set { _botonDesactivado = value; }
        }
        
        #region TEXTURAS y POSICIONES
        /// <summary>
        /// Textura asociada al bot�n en estado activo.
        /// </summary>
        internal Texture2D _texturaBotonActivo;
        /// <summary>
        /// Ubicaci�n t�ctil del dispositivo de pantalla,este puede ser nulo.
        /// </summary>
        private static TouchLocation? _touchLocation;
        /// <summary>
        /// Obtiene y permite modificar la ubicaci�n t�ctil.
        /// </summary>
        public static TouchLocation? _TouchLocation
        {
            get { return Botones._touchLocation; }
            set { Botones._touchLocation = value; }
        }


        
        /// <summary>
        /// Inidica los l�mites de la pantalla del Bot�n desde el eje superior izquierdo.
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
        /// Posici�n en pantalla de la parte superior izquierda de la esquina del bot�n.
        /// </summary>
        internal Vector2 _PosicionSuperiorIzquierdaBoton
        {
            get { return _PosicionProximaOrigen - _PosicionOrigen; }
            set { _PosicionProximaOrigen = value + _PosicionOrigen; }
        }
        /// <summary>
        /// Posicion  del bot�n.
        /// </summary>
        internal Vector2 _posicion = Vector2.Zero;
        /// <summary>
        /// Devuelve la posici�n de origen del bot�n.
        /// </summary>
        public Vector2 _PosicionOrigen { get; set; }
        /// <summary>
        /// La localizaci�n en pantalla donde el origen de la textura ser� dibujada/situada. 
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
        /// El centro de la textura.(Las texturas de bot�n activo y desactivado deben ser del mismo tama�o).
        /// Suele usarse como posici�n de origen.
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
        ///  Posici�n en la que se dibuja el boton respecto al origen.
        /// </summary>
        public Vector2 _PosicionDibujar
        {
            get { return _PosicionProximaOrigen - _PosicionOrigen; }
        }
        /// <summary>
        /// Textura asociada al bot�n cuando cambia de estado.
        /// </summary>
        private Texture2D _texturaBotonEstado2 = null;
        /// <summary>
        /// Devuelve y modifica la textura asociada al bot�n cuando cambia de estado.
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
        /// Estado inicial del bot�n.
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
        /// Color aplicado cuando se est� dibujando el bot�n(en los tres estados).
        /// </summary>
        protected Color _ColorBoton
        {
            get
            {   //Si el color est� desactivado,devuelve el valor del bot�n en estado desactivado.
                Color color;
                if (_BotonDesactivado == true)
                    color = _ColorBotonDesactivado.Value;
                else
                {
                    //Comprueba si se debe llamar a GameComponent.Update, si es as�,comprueba que el bot�n est� pulsado.
                    //Si est� pulsado devuelve el color del bot�n en estado pulsado(se comprueba s� no es nulo
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
        /// Color aplicado cuando se est� dibujando el bot�n en estado activo(el bot�n est� pulsado).
        /// </summary>
        public Color? _ColorBotonApretado { get; set; }
        /// <summary>
        /// Color aplicado cuando se est� dibujando el bot�n en un estado desactivado.
        /// </summary>
        public Color? _ColorBotonDesactivado { get; set; }
        /// <summary>
        /// Color del bot�n en estado normal.
        /// </summary>
        private Color _colorBoton;
        /// <summary>
        /// Color del bot�n por defecto
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
        /// Indica cuando el bot�n puede ser arrastrado.
        /// </summary>
        public bool _PermitirArrastre { get; set; }

        /// <summary>
        /// Se utiliza en botones con mango de deslizamiento/ l�mites de arrastre (por lo general un eje).
        /// </summary>
        public Rectangle _RestriccionesDesplazamiento { get; set; }
       #endregion
       #region EVENTOS
        public event EventHandler Click;
        /// <summary>
        /// Provoca el evento Click si alg�n oyente se adjunta.
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
        /// Provoca el evento TouchDown si alg�n oyente se adjunta.
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
        /// Establece indicadores v�lidos cuando el bot�n es pulsado.
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
        /// Lanza el evento cuando un elemento cambia de estado o posici�n. 
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
        /// Inicializa un bot�n.
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
           
                //Obtenemos el rect�ngulo del "touchLocation".
                Rectangle? rectangulo = TenerContactoRectangulo();

                //Si el bot�n est� desactivado no hace falta que se compruebe en cada momento si se pulsar� ese bot�n.
                if (!_BotonDesactivado)
                {
                    //COMPARA AMBOS RECT�NGULOS(TOUCHDOWN Y EL BOT�N) Y SI COINCIDEN LLAMA AL EVENTO.
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
                    //LIBERA EL BOT�N UNA VEZ PULSADO
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
        /// Comprueba el estado del bot�n(apretado) y crea un 
        /// peque�o rect�ngulo en torno a la posici�n de contacto si est� disponible
        /// </summary>
        /// <returns>Peque�o rect�ngulo en torno a la posici�n de contacto</returns>
        protected Rectangle? TenerContactoRectangulo()
        {
            Rectangle? contactoRectangulo = null;



            if (_TouchLocation != null && _TouchLocation.Value.State != TouchLocationState.Invalid)
            {

                //Creamos un rectangulo con las coordenadas x,y de la ubicaci�n t�ctil y de anchura y largura 10.
                //Si ponemos mucho m�s de 10 puede que al apretar toquemos m�s de un bot�n.
                contactoRectangulo = new Rectangle((int)_touchLocation.Value.Position.X - 3,
                                            (int)_touchLocation.Value.Position.Y - 3,
                                            10, 10);
            }
            return contactoRectangulo;
        }
        
        /// <summary>
        ///Se le llama en cada fotograma  para comprobar si el bot�n se ha pulsado.(s�lo una vez)
        /// </summary>
        /// <param name="rectangulo">Peque�o rect�ngulo en torno a la posici�n de contacto si est� disponible</param>
        /// <returns>Devuelve un booleano para comprobar si se ha pulsado el bot�n</returns>
        internal bool ComprobarTouchDown(Rectangle? rectangulo)
        {
            //Comprobamos si el rect�ngulo del bot�n hace intersecci�n con el rect�ngulo de la pulsaci�n t�ctil.
            bool pulsarBoton = false;
            if (!_pulsacionActiva == true && rectangulo != null)
            {
                pulsarBoton = _LimitesPantallaBoton.Intersects(rectangulo.Value);
            }

            return pulsarBoton;
        }

        /// <summary>
        /// Se le llama cada cuadro para comprobar si a�n est� 'presionado'
        /// </summary>
        /// <param name="rectangulo">Peque�o rect�ngulo en torno a la posici�n de contacto si est� disponible</param>
        /// <returns>True si todav�a est� presionado. False en caso contrario</returns>
        private bool CompruebaSiAunEstaPresionado(Rectangle? rectangulo)
        {
            bool estaPresionado = false;
            if (_pulsacionActiva)
                if (rectangulo == null)
                   estaPresionado = true;

            return estaPresionado;
        }
        /// <summary>
        /// Se le llama en  cada fotograma  para comprobar si  est� el bot�n pulsado.
        /// </summary>
        /// <param name="rectangulo">Peque�o rect�ngulo en torno a la posici�n de contacto si est� disponible</param>
        /// <returns>Devuelve un booleano para comprobar si esta siendo pulsado el bot�n</returns>
        private bool ComprobarSiEstaPulsado(Rectangle? rectangulo)
        {
            bool estaPulsado = false;

            if (rectangulo != null)
                //si el rectangulo coincide con los limites del bot�n, este est� pulsado.
                estaPulsado = _LimitesPantallaBoton.Intersects(rectangulo.Value);

            return estaPulsado;
        }
        /// <summary>
        ///Realiza acciones para desbloquear el bot�n.
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
        /// Reinicia los indicadores cuando no est� siendo apretado.
        /// </summary>
        protected virtual void ReiniciarIndicadores()
        {
            if (_pulsacionActiva)
                _pulsacionActiva = false;         
        }
       
       #endregion

       
   }
}
