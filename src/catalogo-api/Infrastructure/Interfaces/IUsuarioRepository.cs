using CatalogoApi.Domain.Entities;

namespace CatalogoApi.Infrastructure.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    bool EmailJaCadastrado(string email, Guid? excluirId = null);
}
