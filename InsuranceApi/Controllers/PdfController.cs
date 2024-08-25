using InsuranceApi.DTOs;
using InsuranceApi.Services;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using iText.Layout.Properties;
using iText.Kernel.Colors;


namespace InsuranceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IPolicyHolderService _policyHolderService;
        private readonly IPolicyService _policyService;

        public PdfController(IPolicyHolderService policyHolderService, IPolicyService policyService)
        {
            _policyHolderService = policyHolderService;
            _policyService = policyService;
        }

        [HttpGet("GeneratePolicyPdf")]
        public async Task<IActionResult> GeneratePolicyPdf(int policyHolderId, int policyId)
        {
            try
            {
                var policyHolder = await _policyHolderService.GetById(policyHolderId);
                var policy = await _policyService.GetById(policyId);

                if (policyHolder == null)
                {
                    return NotFound("Policy holder not found.");
                }

                if (policy == null)
                {
                    return NotFound("Policy not found.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    var pdfWriter = new PdfWriter(memoryStream);
                    var pdfDocument = new PdfDocument(pdfWriter);
                    var document = new Document(pdfDocument);

                    // Add title
                    document.Add(new Paragraph("Certificate of Insurance").SetBold().SetFontSize(20).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    document.Add(new Paragraph(" ")); // Spacer

                    // Create table for Policy Information
                    var policyInfoTable = new Table(2);
                    policyInfoTable.AddCell("Policy Number");
                    policyInfoTable.AddCell(policy.PolicyNumber);
                    policyInfoTable.AddCell("Date");
                    policyInfoTable.AddCell(policy.StartDate.ToShortDateString());

                    document.Add(new Paragraph("Policy Information").SetBold().SetBackgroundColor(iText.Kernel.Colors.ColorConstants.YELLOW));
                    document.Add(policyInfoTable);

                    document.Add(new Paragraph(" ")); // Spacer

                    // Create table for General Information
                    var generalInfoTable = new Table(2);
                    generalInfoTable.AddCell("Policy Initial Owner / Applicant");
                    generalInfoTable.AddCell(policyHolder.Name);
                    generalInfoTable.AddCell("Gender of Applicant");
                    generalInfoTable.AddCell("Option 2"); // Example Placeholder
                    generalInfoTable.AddCell("Civil Status of Applicant");
                    generalInfoTable.AddCell("Option 2"); // Example Placeholder
                    generalInfoTable.AddCell("Residence Address of Applicant");
                    generalInfoTable.AddCell(policyHolder.Address);
                    generalInfoTable.AddCell("Birthday of Applicant");
                    generalInfoTable.AddCell("November 8, 1956"); // Example Placeholder
                    generalInfoTable.AddCell("Age of the Applicant at Issuance of Policy");
                    generalInfoTable.AddCell("Vivamus in felis eu sapien cursus vestibulum."); // Example Placeholder

                    document.Add(new Paragraph("General Information").SetBold().SetBackgroundColor(iText.Kernel.Colors.ColorConstants.YELLOW));
                    document.Add(generalInfoTable);

                    document.Add(new Paragraph(" ")); // Spacer

                    // Create table for insured details (as per the example image)
                    var insuredInfoTable = new Table(2);
                    insuredInfoTable.AddCell("Name of Life Insured");
                    insuredInfoTable.AddCell(policyHolder.Name); // Example Placeholder
                    insuredInfoTable.AddCell("Gender of Insured");
                    insuredInfoTable.AddCell("Option 2"); // Example Placeholder
                    insuredInfoTable.AddCell("Civil Status of Insured");
                    insuredInfoTable.AddCell("Option 2"); // Example Placeholder
                    insuredInfoTable.AddCell("Residence Address of Insured");
                    insuredInfoTable.AddCell(policyHolder.Address);
                    insuredInfoTable.AddCell("Birthday of Insured");
                    insuredInfoTable.AddCell("November 8, 1956"); // Example Placeholder
                    insuredInfoTable.AddCell("Age of the Insured at Issuance of Policy");
                    insuredInfoTable.AddCell("Vivamus in felis eu sapien cursus vestibulum."); // Example Placeholder

                    document.Add(new Paragraph("Information of the Person's Life Insured").SetBold().SetBackgroundColor(iText.Kernel.Colors.ColorConstants.YELLOW));
                    document.Add(insuredInfoTable);

                    document.Close();

                    var fileContent = memoryStream.ToArray();

                    // Return the PDF file with appropriate content disposition
                    return File(fileContent, "application/pdf", "PolicyDetails.pdf");
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

