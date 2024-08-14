using CategoriasMvc.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CategoriasMvc.Services
{
    public class ProdutoService : IProdutoService
    {
        private const string apiEndpoint = "/api/1/produtos/";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _httpClient;
        private ProdutoViewModel produtoVM;
        private IEnumerable<ProdutoViewModel> produtosVM;

        public ProdutoService(IHttpClientFactory httpClient)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _httpClient = httpClient;
        }

        public async Task<ProdutoViewModel> Create(ProdutoViewModel produtos, string token)
        {
            var client = _httpClient.CreateClient("ProdutosApi");
            PutTokenInHeaderAuthorization(token, client);
            var produto = JsonSerializer.Serialize(produtos);
            StringContent content = new StringContent(produto, Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    produtos = await JsonSerializer
                        .DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }

                return produtos;
            }
        }

        public async Task<bool> DeleteById(int id, string token)
        {
            var client = _httpClient.CreateClient("ProdutosApi");
            PutTokenInHeaderAuthorization(token, client);
            using (var response = await client.DeleteAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else return false;
            }
        }

        public async Task<IEnumerable<ProdutoViewModel>> GetAll(string token)
        {
            var client = _httpClient.CreateClient("ProdutosApi");
            PutTokenInHeaderAuthorization(token, client);
            using (var response = await client.GetAsync(apiEndpoint))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    produtosVM = await JsonSerializer
                                   .DeserializeAsync<IEnumerable<ProdutoViewModel>>
                                   (apiResponse, _options);
                }
                else
                {
                    return null;
                }                
            }
            return produtosVM;
        }

        public async Task<ProdutoViewModel> GetById(int id, string token)
        {
            var client = _httpClient.CreateClient("ProdutosApi");
            PutTokenInHeaderAuthorization(token, client);
            using (var response = await client.GetAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    produtoVM = await JsonSerializer
                                  .DeserializeAsync<ProdutoViewModel>
                                  (apiResponse, _options);

                }
                else
                {
                    return null;
                }
            }
            return produtoVM;
        }

        public async Task<bool> Update(int id, ProdutoViewModel produto, string token)
        {
            var client = _httpClient.CreateClient("ProdutosApi");
            PutTokenInHeaderAuthorization(token, client);
            using (var response = await client.PutAsJsonAsync(apiEndpoint + id, produto))
            {
                if( response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }

        }

        private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
    