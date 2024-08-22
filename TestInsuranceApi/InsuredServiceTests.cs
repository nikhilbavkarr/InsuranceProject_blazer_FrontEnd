using InsuranceApi.Data;
using InsuranceApi.DTOs;
using InsuranceApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestInsuranceApi
{
    [TestClass]
    public class InsuredServiceTests
    {
        private FnfProjectContext context;
        private IInsuredService service;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FnfProjectContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            context = new FnfProjectContext(options);
            service = new InsuredService(context);
        }

        [TestMethod]
        public async Task Add_ShouldAddInsured()
        {
            var insuredDto = new InsuredDto
            {
                InsuredId = 1,
                PolicyHolderId = 1001,
                Name = "John Doe",
                Dob = DateTime.Now.AddYears(-30),
                Gender = "M",
                RegistrationDate = DateTime.Now
            };

            await service.Add(insuredDto);

            var insured = await context.Insureds.FirstOrDefaultAsync(i => i.InsuredId == insuredDto.InsuredId);
            Assert.IsNotNull(insured);
            Assert.AreEqual(insuredDto.Name, insured.Name);
        }

        [TestMethod]
        public async Task Delete_ShouldRemoveInsured()
        {
            var insured = new Insured
            {
                InsuredId = 1,
                PolicyHolderId = 1001,
                Name = "John Doe",
                Dob = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Gender = "M",
                RegistrationDate = DateOnly.FromDateTime(DateTime.Now)
            };

            context.Insureds.Add(insured);
            await context.SaveChangesAsync();

            await service.Delete(insured.InsuredId);

            var result = await context.Insureds.FindAsync(insured.InsuredId);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnAllInsureds()
        {
            var insured1 = new Insured { InsuredId = 1, Name = "John Doe" };
            var insured2 = new Insured { InsuredId = 2, Name = "Jane Smith" };

            context.Insureds.AddRange(insured1, insured2);
            await context.SaveChangesAsync();

            var result = await service.GetAll();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnInsured()
        {
            var insured = new Insured { InsuredId = 1, Name = "John Doe" };
            context.Insureds.Add(insured);
            await context.SaveChangesAsync();

            var result = await service.GetById(insured.InsuredId);
            Assert.IsNotNull(result);
            Assert.AreEqual(insured.Name, result.Name);
        }

        [TestMethod]
        public async Task Update_ShouldUpdateInsured()
        {
            var insured = new Insured
            {
                InsuredId = 1,
                PolicyHolderId = 1001,
                Name = "John Doe",
                Dob = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Gender = "M",
                RegistrationDate = DateOnly.FromDateTime(DateTime.Now)
            };

            context.Insureds.Add(insured);
            await context.SaveChangesAsync();

            var updatedDto = new InsuredDto
            {
                InsuredId = 1,
                PolicyHolderId = 1001,
                Name = "John Updated",
                Dob = DateTime.Now.AddYears(-35),
                Gender = "M",
                RegistrationDate = DateTime.Now.AddDays(1)
            };

            await service.Update(updatedDto);

            var updatedInsured = await context.Insureds.FindAsync(updatedDto.InsuredId);
            Assert.IsNotNull(updatedInsured);
            Assert.AreEqual(updatedDto.Name, updatedInsured.Name);
        }
    }
}