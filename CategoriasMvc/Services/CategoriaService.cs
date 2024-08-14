using CategoriasMvc.Models;
using System.Text;
using System.Text.Json;

namespace CategoriasMvc.Services
{
    public class CategoriaService : ICategoriaService
    {
        private const string apiEndpoint = "/api/1/categorias/";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _httpClient;

        private CategoriaViewModel categoriaVM;
        private IEnumerable<CategoriaViewModel> categoriasVM;

        public CategoriaService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<CategoriaViewModel> Create(CategoriaViewModel categoriaVM)
        {
            var client = _httpClient.CreateClient("CategoriasApi");
            var categoria = JsonSerializer.Serialize(categoriaVM);
            StringContent content = new StringContent(categoria, Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoriaVM = await JsonSerializer
                        .DeserializeAsync<CategoriaViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }

                return categoriaVM;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var client = _httpClient.CreateClient("CategoriasApi");
            using (var response = await client.DeleteAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else return false;
            }
        }

        public async Task<CategoriaViewModel> GetCategoriaById(int id)
        {

            var client = _httpClient.CreateClient("CategoriasApi");

            using (var response = await client.GetAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoriaVM = await JsonSerializer
                                   .DeserializeAsync<CategoriaViewModel>
                                   (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return categoriaVM;
        }

        public async Task<IEnumerable<CategoriaViewModel>> GetCategorias()
        {
            var client = _httpClient.CreateClient("CategoriasApi");
            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoriasVM = await JsonSerializer
                                   .DeserializeAsync<IEnumerable<CategoriaViewModel>>
                                   (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return categoriasVM;
        }

        public async Task<bool> Update(int id, CategoriaViewModel categoria)
        {
            var client = _httpClient.CreateClient("CategoriasApi");
            using (var response = await client.PutAsJsonAsync(apiEndpoint + id, categoria))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
