using CadastroMembrosApi.Context;
using CadastroMembrosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroMembrosApi.Services
{
    public class MembrosService : IMembroService
    {

        private readonly AppDbContext _appDbContext;

        public MembrosService(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task<IEnumerable<Membro>> GetMembros()
        {
            try
            {
                return await _appDbContext.Membros.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Membro> GetById(int id)
        {
            try
            {
                var membro = await _appDbContext.Membros.FindAsync(id);

                return membro;
            }
            catch
            {
                throw;
            }

        }

        public async Task<IEnumerable<Membro>> GetMembroByName(string name)
        {
            try
            {
                IEnumerable<Membro> membros;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    membros = await _appDbContext.Membros.Where(n => n.NomeMembro.Contains(name)).ToListAsync();
                }

                else
                {
                    membros = await GetMembros();
                }

                return membros;
            }
            catch
            {
                throw;
            }
        }

        public async Task CreateMembro(Membro membro)
        {
            try
            {
                _appDbContext.Membros.Add(membro);
                await _appDbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task UptadeMembro(Membro membro)
        {
            try
            {
                _appDbContext.Entry(membro).State = EntityState.Modified;
                await _appDbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteMembro(Membro membro)
        {
            try
            {
                _appDbContext.Membros.Remove(membro);
                await _appDbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

    }
}
