using Api_clientes.Domain;
using Api_clientes.Infraestructure.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using PdfSharp.Pdf;


namespace Api_clientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;
        public ClientesController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("pdf")]
        public IActionResult Get([FromHeader] string gia)
        {
            try
            {
                var response = _mediator.Send(new GetListGiaQuery.ByGia(gia)).GetAwaiter().GetResult();
                var cliente = response.Cliente;
                string pdfPath = GeneratePdf(cliente);

                SmtpClient client = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("artiaga2727.da@gmail.com", "elwimplwgphtfsrz"),
                    EnableSsl = true,
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("artiaga2727.da@gmail.com"),
                    Subject = "Asunto del correo",
                    Body = "Cuerpo del correo"
                };

                mailMessage.Attachments.Add(new Attachment(pdfPath));

                mailMessage.To.Add(cliente.correo);

                client.Send(mailMessage);

                

                return Json(cliente);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"Error al enviar el correo electrónico: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                }
                return StatusCode(500, "Error al enviar el correo electrónico.");
            }
        }

        private string GeneratePdf(Cliente cliente)
        {
            //string pdfPath = Path.Combine(Path.GetTempPath(), cliente.nom_cli+".pdf");

            string pdfPath = Path.Combine(Path.GetTempPath(), cliente.nom_cli + ".pdf");
            System.Threading.Thread.Sleep(100);

            if (System.IO.File.Exists(pdfPath))
            {
                System.IO.File.Delete(pdfPath);
            }


            using (var document = new PdfDocument())
            {
                PdfPage page = document.AddPage();

                document.Save(pdfPath);
                document.Close();
            }
            
            return pdfPath;
        }
    }
}
