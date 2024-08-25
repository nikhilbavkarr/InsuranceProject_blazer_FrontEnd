namespace ClientApp.Services
{
    public interface IPdfService
    {
        Task<byte[]> GeneratePolicyPdfAsync(int policyHolderId, int policyId);
    }
    public class PdfService : IPdfService
    {
        private readonly HttpClient _httpClient;

        public PdfService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> GeneratePolicyPdfAsync(int policyHolderId, int policyId)
        {
            var requestUri = $"api/pdf/GeneratePolicyPdf?policyHolderId={policyHolderId}&policyId={policyId}";
            var response = await _httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
