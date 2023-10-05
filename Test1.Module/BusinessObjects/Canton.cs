using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Test1.Module.BusinessObjects
{
    public class Canton : XPObject
    {
        public Canton(Session session) : base(session)
        { }


        string nombre;
        Provincia provincia;

        [Association]
        public Provincia Provincia
        {
            get => provincia;
            set => SetPropertyValue(nameof(Provincia), ref provincia, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Nombre
        {
            get => nombre;
            set => SetPropertyValue(nameof(Nombre), ref nombre, value);
        }


    }
}
