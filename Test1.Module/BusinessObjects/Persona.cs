using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Module.BusinessObjects
{
    [DefaultClassOptions]
    [RuleCriteria("CargasFamiliares>=0")]
    
    [Appearance("PersonaEmpleado", Criteria = "EsEmpleado = true", TargetItems = "Identificacion, Nombre",
        BackColor = "Yellow", 
        FontColor = "Red" )]

    [Appearance("PersonaEmpleado2", Criteria = "!EsEmpleado", TargetItems = "*",
        FontColor = "Blue" )]


    [Appearance("PersonaEmpleado3", Criteria = "!EsEmpleado", TargetItems = "Sueldo",
        Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]


    [Appearance("PersonaEmpleado4", Criteria = "EsNuevoRegistro", TargetItems = "NombreCompleto",
        Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]

    [VisibleInReports(false)]
    [VisibleInDashboards(false)]
    public class Persona : XPObject
    {
        public Persona(Session session) : base(session)        { }


        TipoEmpleado tipoEmpleado;
        decimal sueldo;
        bool esEmpleado;
        Direccion direccionDomicilio;
        string apellido;
        int cargasFamiliares;
        DateTime fechaNacimiento;
        string nombre;
        string identificacion;

        [Size(13)]
        [RuleRequiredField]
        [RuleUniqueValue(CustomMessageTemplate = "Ya existe una persona con esa cédula")]
        public string Identificacion
        {
            get => identificacion;
            set => SetPropertyValue(nameof(Identificacion), ref identificacion, value);
        }

        [Size(50)]
        [RuleRequiredField]
        public string Apellido
        {
            get => apellido;
            set => SetPropertyValue(nameof(Apellido), ref apellido, value);
        }

        [Size(50)]
        [RuleRequiredField]
        public string Nombre
        {
            get => nombre;
            set => SetPropertyValue(nameof(Nombre), ref nombre, value);
        }



        [FetchOnly]
        [Persistent("NombreCompleto")]
        public string NombreCompleto
        {
            get;
            internal set;
        }

        public DateTime FechaNacimiento
        {
            get => fechaNacimiento;
            set => SetPropertyValue(nameof(FechaNacimiento), ref fechaNacimiento, value);
        }

        [VisibleInDetailView(false)]
        [VisibleInListView(true)]
        public int Edad => DateTime.Today.Year - this.FechaNacimiento.Year;

        [Persistent]
        public bool EsEmpleado
        {
            get => esEmpleado;
            internal set => SetPropertyValue(nameof(EsEmpleado), ref esEmpleado, value);
        }

        public int CargasFamiliares
        {
            get => cargasFamiliares;
            set => SetPropertyValue(nameof(CargasFamiliares), ref cargasFamiliares, value);
        }

        [Aggregated]
        public Direccion DireccionDomicilio
        {
            get => direccionDomicilio;
            set => SetPropertyValue(nameof(DireccionDomicilio), ref direccionDomicilio, value);
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(true)]
        public decimal Sueldo
        {
            get => sueldo;
            set => SetPropertyValue(nameof(Sueldo), ref sueldo, value);
        }

        //[Browsable(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public bool EsNuevoRegistro => this.Session.IsNewObject(this);

        [DataSourceCriteria("EsActivo")]
        public TipoEmpleado TipoEmpleado
        {
            get => tipoEmpleado;
            set => SetPropertyValue(nameof(TipoEmpleado), ref tipoEmpleado, value);
        }

        [Association("Persona-Tareas"), DevExpress.Xpo.Aggregated]
        public XPCollection<Tarea> Tareas
        {
            get
            {
                return GetCollection<Tarea>(nameof(Tareas));
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.FechaNacimiento = DateTime.Today;
            this.DireccionDomicilio = new Direccion(this.Session);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!(this.Identificacion.Length == 13 || this.Identificacion.Length == 10))
                throw new UserFriendlyException("La identificación debe tener 13 caracteres");
        }

        [Action(Caption = "Dar ingreso a empleado", ConfirmationMessage = "Seguro que desea dar el ingreso de empleado", AutoCommit = true )]
        public void DarIngresoEmpleado()
        {
            if (this.EsEmpleado)
            {
                throw new UserFriendlyException("La persona ya es empleado");
            }
                
            this.EsEmpleado = true;
            this.Save();
        }
        

    }
}
