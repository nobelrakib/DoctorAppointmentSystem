using Autofac;
using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Repositories;
using DoctorAppointmentSystem.Core.Service;
using DoctorAppointmentSystem.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core
{
    public class DoctorAppointmentModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public DoctorAppointmentModule(string connectionStringName, string migrationAssemblyName)
        {

            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DoctorAppointmentContext>()
                   .WithParameter("connectionString", _connectionString)
                   .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                   .InstancePerLifetimeScope();

            //builder.RegisterType<BillingContext>().As<IBillingContext>()
            //       .WithParameter("connectionString", _connectionString)
            //       .WithParameter("migrationAssemblyName", _migrationAssemblyName)
            //       .InstancePerLifetimeScope();

            builder.RegisterType<DoctorAppointmentUnitOfWork>().As<IDoctorAppointmentUnitOfWork>()
                   .WithParameter("connectionString", _connectionString)
                   .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                   .InstancePerLifetimeScope();

            builder.RegisterType<DoctorRepository>().As<IDoctorRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DoctorService>().As<IDoctorService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DepartmentService>().As<IDepartmentService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FileService>()
               .InstancePerLifetimeScope();

            builder.RegisterType<DrugRepository>().As<IDrugRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<DrugService>().As<IDrugService>()
               .InstancePerLifetimeScope();

            builder.RegisterType<AppointmentRepository>().As<IAppointmentRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<AppointmentService>().As<IAppointmentService>()
               .InstancePerLifetimeScope();

            builder.RegisterType<AWSS3BucketHelper>().As<IAWSS3BucketHelper>()
               .InstancePerLifetimeScope();

            builder.RegisterType<DynamodbAppointmentService>().As<IDynamodbAppointmentService>()
               .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
