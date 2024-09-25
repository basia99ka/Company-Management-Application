using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using BaseLibrary.Contracts;

namespace ServerLibrary.Repositories.Implementation
{
    public class BranchRepository : IGenericRepositoryInterface<Branch>
    {
        private readonly AppDbContext _appDbContext;

        public BranchRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var team = await _appDbContext.Branches.FindAsync(id);
            if (team is null) return NotFound();

            _appDbContext.Branches.Remove(team);
            await Commit();
            return Success();
        }

        public async Task<List<Branch>> GetAll() => await _appDbContext.
            Branches.AsNoTracking()
            .Include(d => d.Department)
            .ToListAsync();

        public async Task<Branch> GetById(int id) => await _appDbContext.Branches.FindAsync(id);


        public async Task<GeneralResponse> Insert(Branch item)
        {
            if (!await ChceckName(item.Name!)) return new GeneralResponse(false, "Team already added!");
            _appDbContext.Branches.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Branch item)
        {
            var team = await _appDbContext.Branches.FindAsync(item.Id);
            if (team is null) return NotFound();

            team.Name = item.Name;
            team.DepartmentId = item.DepartmentId;
            //team.Department.Name= item.Department.Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Team not found");
        private static GeneralResponse Success() => new(true, "Process completed");

        private async Task Commit() => await _appDbContext.SaveChangesAsync();
        private async Task<bool> ChceckName(string name)
        {
            var item = await _appDbContext.Branches.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

    }
}
