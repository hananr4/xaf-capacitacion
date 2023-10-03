using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Test1.Module.BusinessObjects
{
    public class Direccion : BaseObject
    {
        public Direccion(Session session) : base(session) { }


        string interseccion;
        string numero;
        string calle;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Calle
        {
            get => calle;
            set => SetPropertyValue(nameof(Calle), ref calle, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Numero
        {
            get => numero;
            set => SetPropertyValue(nameof(Numero), ref numero, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Interseccion
        {
            get => interseccion;
            set => SetPropertyValue(nameof(Interseccion), ref interseccion, value);
        }


    }
}
