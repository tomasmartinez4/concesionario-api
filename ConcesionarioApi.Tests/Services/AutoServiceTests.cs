using AutoMapper;
using ConcesionarioApi.DTOs;
using ConcesionarioApi.Interfaces;
using ConcesionarioApi.Models;
using ConcesionarioApi.Profiles;
using ConcesionarioApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcesionarioApi.Tests.Services
{

    [TestClass]
    public class AutoServiceTests
    {
        private Mock<IAutoRepository> _repoMock;
        private IMapper _mapper;
        private AutoService _service;

        [TestInitialize]
        public void setup()
        {
            // 1) Mock del repositorio
            _repoMock = new Mock<IAutoRepository>();

            // 2)COnfig Automapper

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoProfile>());
            _mapper = config.CreateMapper();

            // 3) Creamos el servicio y le inyectamos el repo mock y el mapper
            _service = new AutoService(_repoMock.Object, _mapper);

        }

        [TestMethod]
        public async Task GetAllAsync_WhenCalled_ReturnsAllDtos()
        {
            // Arrange: simulamos que el repo devuelve dos entidades Auto
            var entities = new List<Auto>
            {
                new Auto { Id = 1, Marca = "Toyota", Modelo = "Corolla", Anio = 2021, Precio = 20000 },
                new Auto { Id = 2, Marca = "Ford",   Modelo = "Fiesta",  Anio = 2020, Precio = 15000 }
            };
            _repoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(entities);

            // Act
            var result = (await _service.GetAllAsync()).ToList();

            // Assert: el servicio llama al repo y devuelve esas dos entidades
            _repoMock
                .Verify(r => r.GetAllAsync(), Times.Once());
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(dto => dto.Marca == "Toyota" && dto.Id == 1));
            Assert.IsTrue(result.Any(dto => dto.Modelo == "Fiesta" && dto.Id == 2));
        }

        [TestMethod]
        public async Task GetByIdAsync_WhenNotFound_ReturnsNull()
        {
            //Arrange
            _repoMock.Setup(r => r.GetByIdAsync(42))
                .ReturnsAsync((Auto?)null);

            //Act
            var dto = await _service.GetByIdAsync(42);

            //Assert
            Assert.IsNull(dto);
            _repoMock.Verify(r => r.GetByIdAsync(42), Times.Once());
        }

        [TestMethod]
        public async Task DeleteAsync_WhenCalled_InvokesRepoDelete()
        {
            //Arrange
            var id = 55;

            //Act
            await _service.DeleteAsync(id);

            //Assert
            _repoMock.Verify(r => r.DeleteAsync(id), Times.Once());
        }

        [TestMethod]
        public async Task UpdateAsync_WhenFound_MapsAndCallsRepo()
        {

            //Arrange
            var existing = new Auto { Id = 5, Marca = "Ford", Modelo = "Mondeo", Anio = 2019, Precio = 25000 };

            _repoMock.Setup(r => r.GetByIdAsync(5))
                .ReturnsAsync(existing);

            var updateDto = new UpdateAutoDto
            {
                Marca = "Ford",
                Modelo = "Focus",
                Anio = 2021,
                Precio = 20000
            };

            //Act
            await _service.UpdateAsync(5, updateDto);

            //Assert, llamar a UpdateASync con la misma entidad modificada
            Assert.AreEqual("Focus", existing.Modelo);
            Assert.AreEqual(20000, existing.Precio);
            _repoMock.Verify(r => r.UpdateAsync(existing), Times.Once);

        }

        [TestMethod]
        public async Task CreateAsync_WhenCalled_InvokesRepoAddAndReturnsDto()
        {
            // Arrange
            var dto = new CreateAutoDto { Marca = "Mazda", Modelo = "3", Anio = 2022, Precio = 18000 };
            Auto? captured = null;
            _repoMock
              .Setup(r => r.AddAsync(It.IsAny<Auto>()))
              .Callback<Auto>(a => captured = a)
              .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Auto>()), Times.Once);
            Assert.IsNotNull(captured);
            Assert.AreEqual("Mazda", captured!.Marca);
            Assert.AreEqual("3", captured.Modelo);
            Assert.AreEqual(2022, captured.Anio);
            Assert.AreEqual(18000m, captured.Precio);

            //  devuelve el DTO mapeado
            Assert.AreEqual(captured.Marca, result.Marca);
            Assert.AreEqual(captured.Modelo, result.Modelo);
            Assert.AreEqual(captured.Anio, result.Anio);
            Assert.AreEqual(captured.Precio, result.Precio);
        }


    }

}
