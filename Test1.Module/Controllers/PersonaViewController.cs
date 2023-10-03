using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test1.Module.BusinessObjects;
using Test1.Module.Parameters;

namespace Test1.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class PersonaViewController : ViewController
    {
        PopupWindowShowAction CambiarFechaNacimiento;
        
        SimpleAction DarBajaEmpleadoAction;

        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public PersonaViewController()
        {
            InitializeComponent();
            DarBajaEmpleadoAction = new SimpleAction(this, "DarBajaEmpleadoAction", "Filters");
            DarBajaEmpleadoAction.Execute += DarBajaEmpleadoAction_Execute;
            DarBajaEmpleadoAction.TargetObjectType = typeof(BusinessObjects.Persona);
            DarBajaEmpleadoAction.TargetObjectsCriteria = "EsEmpleado = True";
            DarBajaEmpleadoAction.Caption = "Dar de baja";
            DarBajaEmpleadoAction.ImageName = "BO_Customer";
            DarBajaEmpleadoAction.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            DarBajaEmpleadoAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

            CambiarFechaNacimiento = new PopupWindowShowAction(this, "CambiarFechaNacimiento", "View");
            CambiarFechaNacimiento.Execute += CambiarFechaNacimiento_Execute;
            CambiarFechaNacimiento.CustomizePopupWindowParams += CambiarFechaNacimiento_CustomizePopupWindowParams;
            CambiarFechaNacimiento.TargetObjectType = typeof(BusinessObjects.Persona);
            CambiarFechaNacimiento.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
        }
        private void CambiarFechaNacimiento_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var par= e.PopupWindowViewCurrentObject as CambiarFechaParemeter;
            if (par == null)
            {
                throw new UserFriendlyException("Seleccione una persona");
            }

            var per = View.CurrentObject as Persona;

            per.FechaNacimiento = par.FechaNacimiento;

            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }
        private void CambiarFechaNacimiento_CustomizePopupWindowParams(object sender, 
                CustomizePopupWindowParamsEventArgs e)
        {
            var persona = this.View.CurrentObject as Persona;

            if (persona == null)
            {
                throw new UserFriendlyException("Seleccione una persona");
            }
            var par = new CambiarFechaParemeter();
            var os = Application.CreateObjectSpace(typeof(CambiarFechaParemeter));
            var view = Application.CreateDetailView(os, par);
            e.View = view;
        }
        private void DarBajaEmpleadoAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var persona = this.View.CurrentObject as Persona;

            if (persona == null)
            {
                throw new UserFriendlyException("Seleccione una persona");
            }

            if (persona.EsEmpleado == false)
            {
                throw new UserFriendlyException("No es empleado");
            }
            persona.EsEmpleado = false;
            persona.Save();
            View.ObjectSpace.CommitChanges();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}

