using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Test1.Module.BusinessObjects
{
    [NavigationItem("Parámetros")]
    [DevExpress.ExpressApp.DC.XafDefaultProperty("Descripcion")]
    public class TipoEmpleado : XPObject
    {
        public TipoEmpleado(Session session) : base(session) { }


        bool esActivo;
        string cargo;
        string descripcion;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]

        [VisibleInLookupListView(true)]
        public string Descripcion
        {
            get => descripcion;
            set => SetPropertyValue(nameof(Descripcion), ref descripcion, value);
        }

        [VisibleInLookupListView(true)]
        public string Cargo
        {
            get => cargo;
            set => SetPropertyValue(nameof(Cargo), ref cargo, value);
        }


        public bool EsActivo
        {
            get => esActivo;
            set => SetPropertyValue(nameof(EsActivo), ref esActivo, value);
        }
    }
}
