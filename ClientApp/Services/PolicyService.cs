using InsuranceApi.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace ClientApp.Services
{
    public interface IPolicyDtoService
    {
        Task Add(PolicyDto employee);
        Task Delete(int id);
        Task<List<PolicyDto>> GetAll();
        Task<PolicyDto> GetById(int id);
        Task Update(PolicyDto employee);
    }

    public class PolicyDtoService : IPolicyDtoService
    {
        private readonly HttpClient httpClient;
        public PolicyDtoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<PolicyDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<PolicyDto>>("PolicyContoller");
        }

        public async Task<PolicyDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<PolicyDto>($"PolicyContoller/{id}");
        }

        public async Task Add(PolicyDto employee)
        {
            await httpClient.PostAsJsonAsync<PolicyDto>("PolicyContoller", employee);
        }

        public async Task Delete(int id)
        {
            await httpClient.DeleteAsync($"PolicyContoller/{id}");
        }

        public async Task Update(PolicyDto employee)
        {
            await httpClient.PutAsJsonAsync<PolicyDto>("PolicyContoller", employee);
        }
    }
}
