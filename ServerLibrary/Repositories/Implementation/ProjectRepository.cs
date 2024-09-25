using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using BaseLibrary.Contracts;

namespace ServerLibrary.Repositories.Implementation
{
    public class ProjectRepository : IGenericRepositoryInterface<Project>
    {
        private readonly AppDbContext _appDbContext;

        public ProjectRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var project = await _appDbContext.Projects.FindAsync(id);
            if (project is null) return NotFound();

            _appDbContext.Projects.Remove(project);
            await Commit();
            return Success();
        }

        public async Task<List<Project>> GetAll() =>
          await _appDbContext
            .Projects.AsNoTracking()
           .Include(b => b.Branch)
           .ToListAsync();

       

        public async Task<Project> GetById(int id) => await _appDbContext.Projects.FindAsync(id);


        public async Task<GeneralResponse> Insert(Project item)
        {
            if (!await ChceckName(item.Name!)) return new GeneralResponse(false, "Project already added!");
            _appDbContext.Projects.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Project item)
        {
            var project = await _appDbContext.Projects.FindAsync(item.Id);
            if (project is null) return NotFound();

            project.Name = item.Name;
            project.BranchId = item.BranchId;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Project not found");
        private static GeneralResponse Success() => new(true, "Process completed");

        private async Task Commit() => await _appDbContext.SaveChangesAsync();
        private async Task<bool> ChceckName(string name)
        {
            var item = await _appDbContext.Projects.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

    }
}