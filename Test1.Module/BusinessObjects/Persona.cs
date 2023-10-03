﻿using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Module.BusinessObjects
{
    [DefaultClassOptions]
    [RuleCriteria("CargasFamiliares>=0")]
    public class Persona : XPObject
    {
        public Persona(Session session) : base(session)        { }

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

        public DateTime FechaNacimiento
        {
            get => fechaNacimiento;
            set => SetPropertyValue(nameof(FechaNacimiento), ref fechaNacimiento, value);
        }

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


        [Association("Persona-Tareas"), Aggregated]
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