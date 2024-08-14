using CategoriasMvc.Models;

namespace CategoriasMvc.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoViewModel>> GetAll(string token);
        Task<ProdutoViewModel> GetById(int id, string token);
        Task<ProdutoViewModel> Create(ProdutoViewModel model, string token);
        Task<bool> Update(int id, ProdutoViewModel model, string token);
        Task<bool> DeleteById(int id, string token);
    }
}
