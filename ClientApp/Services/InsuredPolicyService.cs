﻿using InsuranceApi.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace ClientApp.Services
{
    public interface IInsuredPolicyDtoService
    {
        Task Add(InsuredPolicyDto employee);
        Task DeleteById(int id);
        Task<List<InsuredPolicyDto>> GetAll();
        Task<InsuredPolicyDto> GetById(int id);
        Task Update(InsuredPolicyDto employee);
    }

    public class InsuredPolicyDtoService : IInsuredPolicyDtoService
    {
        private readonly HttpClient httpClient;
        public InsuredPolicyDtoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<InsuredPolicyDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<InsuredPolicyDto>>("InsuredPolicyContoller");
        }

        public async Task<InsuredPolicyDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<InsuredPolicyDto>($"InsuredPolicyContoller/{id}");
        }

        public async Task Add(InsuredPolicyDto employee)
        {
            await httpClient.PostAsJsonAsync<InsuredPolicyDto>("InsuredPolicy", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"InsuredPolicyContoller/{id}");
        }

        public async Task Update(InsuredPolicyDto employee)
        {
            await httpClient.PutAsJsonAsync<InsuredPolicyDto>("InsuredPolicyContoller", employee);
        }
    }
}
