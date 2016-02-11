/*
 * PROYECTO FIN DE CARRERA ITIG 2012-2013
 * DISEÑO Y ESPICIFICACIÓN DE UNA API PARA EL DESARROLLO DE VIDEOJUEGOS EN 2D PARA WINDOWS PHONE 7
 * AUTOR: JAVIER FERNÁNDEZ VILLANUEVA
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace XNAProyecto.Menus
{
    public enum EstadoP
    {
        activo,
        desactivado,
    }
    public abstract class TiposP
    {

        EstadoP estado = EstadoP.activo;
        public EstadoP Estado
        {
            get { return estado; }
            internal set { estado = value; }
        }

        
        GestureType gesture = GestureType.None;
        public GestureType GestureType
        {
            get { return gesture; }
            internal set
            {
                gesture = value;
            }
        }
        AdministradorPantallas controlmenus;
        public AdministradorPantallas ControlMenus
        {
            get { return controlmenus; }
            internal set { controlmenus = value; }
        }


      
        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
        public virtual void Update(GameTime gameTime, bool oculto)
        {
            if (oculto)
                   
                    estado = EstadoP.desactivado;            
            else  
                    estado = EstadoP.activo;    
        }

        public virtual void ControlTactil(GameTime time, EstadoPadTouchPanel estado) { }
        public virtual void BackBoton( EstadoPadTouchPanel input) { }

        public virtual void Draw(GameTime gameTime) { }

        public void EliminarPantalla()
        {          
                ControlMenus.EliminarPantalla(this); 
        }


     

       
    }
}
