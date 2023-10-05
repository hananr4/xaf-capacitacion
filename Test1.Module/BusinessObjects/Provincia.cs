using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Test1.Module.BusinessObjects
{
    [NavigationItem("Parámetros")]
    public class Provincia : XPObject
    {
        public Provincia(Session session) : base(session)
        { }


        string nombre;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Nombre
        {
            get => nombre;
            set => SetPropertyValue(nameof(Nombre), ref nombre, value);
        }

        [Association, Aggregated]
        public XPCollection<Canton> Cantones
        {
            get
            {
                return GetCollection<Canton>(nameof(Cantones));
            }
        }

    }
}
