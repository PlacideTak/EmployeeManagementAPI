using WebApplication1.Services;
using WebApplication1.Models;
using WebApplication1.Repositories;
using FluentAssertions;
using Moq;

namespace EmployeeManagement.Tests
{
    public class EmployeeServiceTests
    {

        private readonly Mock<IEmployeeRepository> _repoMock;
        private readonly EmployeeService _service;

        public EmployeeServiceTests()
        {
            _repoMock = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_Should_Call_Repository()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@test.com"
            };

            _repoMock
                .Setup(r => r.AddAsync(employee))
                .Returns(Task.CompletedTask);

            // Act
            await _service.CreateAsync(employee);

            // Assert
            _repoMock.Verify(r => r.AddAsync(employee), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Employee()
        {
            // Arrange
            var employee = new Employee { Id = 1, FirstName = "John" };

            _repoMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(employee);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }


        [Fact]
        public async Task UpdateEmployeeAsync_Should_Return_False_When_NotFound()
        {
            var employee = new Employee { Id = 99 };

            _repoMock.Setup(r => r.GetByIdAsync(99))
                     .ReturnsAsync((Employee?)null);

            var result = await _service.UpdateEmployeeAsync(employee);

            result.Should().BeFalse();
            _repoMock.Verify(r => r.Update(It.IsAny<Employee>()), Times.Never);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_Should_Delete_When_Exists()
        {
            // Arrange
            var employee = new Employee { Id = 1, FirstName = "John" };

            _repoMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(employee);

            _repoMock.Setup(r => r.Delete(employee));

            // Act
            var result = await _service.DeleteEmployeeAsync(employee.Id);

            // Assert
            result.Should().BeTrue();
            _repoMock.Verify(r => r.Delete(employee), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_Should_Return_False_When_NotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(99))
                     .ReturnsAsync((Employee?)null);

            // Act
            var result = await _service.DeleteEmployeeAsync(99);

            // Assert
            result.Should().BeFalse();
            _repoMock.Verify(r => r.Delete(It.IsAny<Employee>()), Times.Never);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}
