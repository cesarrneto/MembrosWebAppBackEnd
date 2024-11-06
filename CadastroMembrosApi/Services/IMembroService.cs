using CadastroMembrosApi.Models;

namespace CadastroMembrosApi.Services;

public interface IMembroService
{
    Task<IEnumerable<Membro>> GetMembros();
    Task<Membro> GetById(int id);
    Task<IEnumerable<Membro>> GetMembroByName(string name);
    Task CreateMembro(Membro membro);
    Task UptadeMembro(Membro membro);
    Task DeleteMembro(Membro membro);
}
