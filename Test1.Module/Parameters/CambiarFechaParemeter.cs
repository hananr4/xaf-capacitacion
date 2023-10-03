using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Module.Parameters
{
    [DomainComponent]
    public class CambiarFechaParemeter: NonPersistentBaseObject
    {
        public CambiarFechaParemeter()        {        }

        public CambiarFechaParemeter(Guid oid) : base(oid)        {        }

        DateTime fechaNacimiento;
        string comentario;


        public DateTime FechaNacimiento
        {
            get => fechaNacimiento;
            set
            {
                if (fechaNacimiento == value)
                    return;
                fechaNacimiento = value;
                OnPropertyChanged(nameof(FechaNacimiento));
            }
        }

        public string Comentario
        {
            get => comentario;
            set
            {
                if (comentario == value)
                    return;
                comentario = value;
                OnPropertyChanged(nameof(Comentario));
            }
        }
    }
}
