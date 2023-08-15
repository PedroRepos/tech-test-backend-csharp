using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Produto.Controllers.ControllersBlobStorage
{

    /// <summary>
    /// Controladora incorporada com os métodos que realizam requisições HTTP para o servidor | Embedded controller with the methods that perform HTTP requests to the server.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ArquivosProdutoControllers : ControllerBase
    {
        private readonly string _connetionString;
        private readonly string _containerName;

        public ArquivosProdutoControllers(IConfiguration configuration)
        {
            _connetionString = configuration.GetValue<string>("BlobConnectionString");
            _containerName = configuration.GetValue<string>("BlobContainerName");
        }

        /// <summary>
        /// Realiza o Post criando uma nova informação no BlobStorage| Performs the Post by creating a new information in the BlobStorage
        /// </summary>
        /// <param name="produto"></param>
        /// <returns>StatusCode</returns>
        [HttpPost("Upload")]
        public IActionResult UploadArquivo(IFormFile arquivo)
        {
            //BLOB = Binary Large Object
            BlobContainerClient container = new(_connetionString, _containerName);
            BlobClient blob = container.GetBlobClient(arquivo.FileName);

            using var data = arquivo.OpenReadStream();
            blob.Upload(data, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = arquivo.ContentType }
            });

            return Ok(blob.Uri.ToString());
        }

        /// <summary>
        /// Realiza o download do arquivo no storage | Download the file to storage
        /// </summary>
        /// <returns>Lista de Produtos</returns>
        [HttpGet("Download/{nome}")]
        public IActionResult DownloadArquivo(string nome)
        {
            BlobContainerClient container = new(_connetionString, _containerName);
            BlobClient blob = container.GetBlobClient(nome);

            if (!blob.Exists())
                return BadRequest();

            var retorno = blob.DownloadContent();
            return File(retorno.Value.Content.ToArray(), retorno.Value.Details.ContentType, blob.Name);

        }

        /// <summary>
        /// Apaga o arquivo no storage | Delete file from storage
        /// </summary>
        /// <returns>Lista de Produtos</returns>
        [HttpDelete("Apagar/{nome}")]
        public IActionResult DeletarArquivo(string nome)
        {
            BlobContainerClient container = new(_connetionString, _containerName);
            BlobClient blob = container.GetBlobClient(nome);

            blob.DeleteIfExists();
            return NoContent();

        }

        /// <summary>
        /// Lista os conteúdos do Storage | List the contents of the Storage
        /// </summary>
        /// <returns>Lista de Produtos</returns>
        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            List<BlobDto> blobsDto = new List<BlobDto>();
            BlobContainerClient container = new(_connetionString, _containerName);

            foreach (var blob in container.GetBlobs())
            {
                blobsDto.Add(new BlobDto
                {
                    Nome = blob.Name,
                    Tipo = blob.Properties.ContentType,
                    Uri = container.Uri.AbsoluteUri + "/" + blob.Name
                });
            }

            return Ok(blobsDto);

        }

    }
}

