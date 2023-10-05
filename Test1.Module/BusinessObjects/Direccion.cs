using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Test1.Module.BusinessObjects
{
    public class Direccion : BaseObject
    {
        public Direccion(Session session) : base(session) { }


        Canton canton;
        Provincia provincia;
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

        [ImmediatePostData]
        public Provincia Provincia
        {
            get => provincia;
            set => SetPropertyValue(nameof(Provincia), ref provincia, value);
        }

        [DataSourceCriteria("Provincia.Oid == '@this.Provincia.Oid'")]
        public Canton Canton
        {
            get => canton;
            set => SetPropertyValue(nameof(Canton), ref canton, value);
        }
    }
}
