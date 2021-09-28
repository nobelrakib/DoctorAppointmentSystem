using Autofac.Extras.Moq;
using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Core.Repositories;
using DoctorAppointmentSystem.Core.Service;
using DoctorAppointmentSystem.Core.UnitOfWork;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DoctorAppointmentSystem.Tests.Services
{
    public class DepartmentServiceTests
    {
        private IDepartmentService _departmentService;
        private AutoMock _mock;
        private Mock<IDepartmentRepository> _departmentRepositoryMock;
        private Mock<IDoctorAppointmentUnitOfWork> _doctorAppointmentUnitOfWorkMock;

        [OneTimeSetUp]
        public void ClassSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void ClassCleanUp()
        {
            _mock?.Dispose();
        }

        [SetUp]
        public void TestSetup()
        {
            _departmentRepositoryMock = _mock.Mock<IDepartmentRepository>();
            _doctorAppointmentUnitOfWorkMock = _mock.Mock<IDoctorAppointmentUnitOfWork>();
            _departmentService = _mock.Create<DepartmentService>();
        }

        [TearDown]
        public void TestCleanUp()
        {
            _departmentRepositoryMock.Reset();
            _doctorAppointmentUnitOfWorkMock.Reset();
        }

        [Test, Category("Add Department Unit Test")]
        public void AddNewDepartment_InvalidDepartment_ThrowsNullReferenceException()
        {
            // Arrange
            Department department = null;

            // Act & Assert
            Should.Throw<InvalidOperationException>(() => _departmentService.AddNewDepartment(department));
        }

        [Test, Category("Add Department Unit Test")]
        public void AddNewDepartment_ValidDepartment_DepartmentSuccessfullyInserted()
        {
            // Arrange
            Department department = new Department {
               Name="medicine",
               Description="good department"
            };

            _doctorAppointmentUnitOfWorkMock.Setup(x => x.DepartmentRepository).Returns(_departmentRepositoryMock.Object);
            _departmentRepositoryMock.Setup(x => x.Add(It.Is<Department>(y => y == department))).Verifiable();
            _doctorAppointmentUnitOfWorkMock.Setup(x => x.Save()).Verifiable();

            //Act
            _departmentService.AddNewDepartment(department);

            //Assert
            _departmentRepositoryMock.VerifyAll();
            _doctorAppointmentUnitOfWorkMock.VerifyAll();
        }

        [Test,Category("Delete Department Unit Test")]
        public void DeleteDepartment_WhenDepartmentIdGiven_DeleteDepartment()
        {
            // Arrange
            var id = 1;
            Department department = new Department
            {
                Id=1,
                Name = "medicine",
                Description = "good department"
            };

            _doctorAppointmentUnitOfWorkMock.Setup(x => x.DepartmentRepository).Returns(_departmentRepositoryMock.Object);
            _departmentRepositoryMock.Setup(x => x.GetById(id)).Returns(department).Verifiable();
            _departmentRepositoryMock.Setup(x => x.Remove(department)).Verifiable();
            _doctorAppointmentUnitOfWorkMock.Setup(x => x.Save()).Verifiable();

            //Act
            _departmentService.DeleteDepartment(id);

            //Assert
            _departmentRepositoryMock.VerifyAll();
            _doctorAppointmentUnitOfWorkMock.VerifyAll();
        }

        [Test, Category("Edit Department Unit Test")]
        public void EditBill_WhenUpdatedBillGiven_BillUpdatedSuccessfully()
        {

            // Arrange
            Department department = new Department
            {
                Id = 1,
                Name = "medicine",
                Description = "good department"
            };

            Department oldDepartment = new Department
            {
                Id = 1,
                Name = "Nurology",
                Description = "best department"
            };


            _doctorAppointmentUnitOfWorkMock.Setup(x => x.DepartmentRepository).Returns(_departmentRepositoryMock.Object);
            _departmentRepositoryMock.Setup(x => x.GetById(department.Id)).Returns(oldDepartment).Verifiable();
            _doctorAppointmentUnitOfWorkMock.Setup(x => x.Save()).Verifiable();

            // Act
            _departmentService.EditDepartment(department);

            //Assert
            _doctorAppointmentUnitOfWorkMock.VerifyAll();
            _departmentRepositoryMock.VerifyAll();
        }

        [Test, Category("Edit Department Unit Test")]
        public void Editdepartment_IfdepartmentNotExists_ThrowsNullReferenceException()
        {

            // Arrange
            Department department = new Department
            {
                Id = 1,
                Name = "medicine",
                Description = "good department"
            };
            Department olddepartment = null;

            _doctorAppointmentUnitOfWorkMock.Setup(x => x.DepartmentRepository).Returns(_departmentRepositoryMock.Object);
            _departmentRepositoryMock.Setup(x => x.GetById(department.Id)).Returns(olddepartment).Verifiable();

            // Act & Assert
            Should.Throw<InvalidOperationException>(() => _departmentService.EditDepartment(department));
        }

        [Test, Category("Get department Unit test")]
        public void Getdepartment_WhendepartmentIdGiven_Returnsdepartment()
        {
            //Arrange
            Department department = new Department
            {
                Id = 1,
                Name = "medicine",
                Description = "good department"
            };
            int departmentId = 1;

            _doctorAppointmentUnitOfWorkMock.Setup(x => x.DepartmentRepository).Returns(_departmentRepositoryMock.Object);
            _departmentRepositoryMock.Setup(x => x.GetById(departmentId)).Returns(department);

            //Act 
            var actualResult = _departmentService.GetDepartment(departmentId);
            actualResult.ShouldBe(department);

            // Assert
            _departmentRepositoryMock.VerifyAll();
            _doctorAppointmentUnitOfWorkMock.VerifyAll();
        }

        [Test, Category("Get Department Unit test")]
        public void GetDepartment_ReturnsListOfDepartments()
        {
            //Arrange
            var total = 20;
            var totalfiltered = 10;
            var pageIndex = 1;
            var pageSize = 10;
            var searchText = "";
            IEnumerable<Department> departments = new List<Department>
            {
                new Department
                {
                    Id = 1,
                    Name = "medicine",
                    Description = "good department"
                },
                new Department
                {
                    Id = 2,
                    Name = "nurology",
                    Description = "best department"
                }
            };

            _doctorAppointmentUnitOfWorkMock.Setup(x => x.DepartmentRepository).Returns(_departmentRepositoryMock.Object);
            _departmentRepositoryMock.Setup(x => x.Get(
            out total,
            out totalfiltered,
            It.IsAny<Expression<Func<Department,bool>>>(),
            It.IsAny<Func<IQueryable<Department>, IOrderedQueryable<Department>>>(),
            "", pageIndex, pageSize, true)).Returns(departments);

            //Act 
            _departmentService.GetDepartments(pageIndex, pageSize, searchText, out total, out totalfiltered);

            // Assert
            _departmentRepositoryMock.VerifyAll();
            _doctorAppointmentUnitOfWorkMock.VerifyAll();
        }
    }
}
