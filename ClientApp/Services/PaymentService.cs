﻿using InsuranceApi.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace ClientApp.Services
{
    public interface IPaymentDtoService
    {
        Task Add(PaymentDto employee);
        Task DeleteById(int id);
        Task<List<PaymentDto>> GetAll();
        Task<PaymentDto> GetById(int id);
        Task Update(PaymentDto employee);
    }

    public class PaymentDtoService : IPaymentDtoService
    {
        private readonly HttpClient httpClient;
        public PaymentDtoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<PaymentDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<PaymentDto>>("Payment");
        }

        public async Task<PaymentDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<PaymentDto>($"Payment/{id}");
        }

        public async Task Add(PaymentDto employee)
        {
            await httpClient.PostAsJsonAsync<PaymentDto>("Payment", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"Payment/{id}");
        }

        public async Task Update(PaymentDto employee)
        {
            await httpClient.PutAsJsonAsync<PaymentDto>("Payment", employee);
        }
    }
}
