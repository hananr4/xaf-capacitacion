using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Test1.Module.BusinessObjects
{

    public enum ActividadPrioridad
    {
        Alta,
        Media,
        Baja
    }

    public class Tarea : BaseObject
    {
        public Tarea(Session session) : base(session) { }


        Persona persona;
        ActividadPrioridad prioridad;
        string descripcion;


        [Association("Persona-Tareas")]
        public Persona Persona
        {
            get => persona;
            set => SetPropertyValue(nameof(Persona), ref persona, value);
        }
        [RuleRequiredField]
        public string Descripcion
        {
            get => descripcion;
            set => SetPropertyValue(nameof(Descripcion), ref descripcion, value);
        }


        public ActividadPrioridad Prioridad
        {
            get => prioridad;
            set => SetPropertyValue(nameof(Prioridad), ref prioridad, value);
        }
    }
}
