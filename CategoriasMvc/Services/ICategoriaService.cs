using CategoriasMvc.Models;

namespace CategoriasMvc.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaViewModel>> GetCategorias();
        Task<CategoriaViewModel> GetCategoriaById(int id);
        Task<CategoriaViewModel> Create(CategoriaViewModel categoria);
        Task<bool> Update(int id, CategoriaViewModel categoria);
        Task<bool> Delete(int id);
    }
}
