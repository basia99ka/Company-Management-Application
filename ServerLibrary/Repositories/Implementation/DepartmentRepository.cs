using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using BaseLibrary.Contracts;

namespace ServerLibrary.Repositories.Implementation
{
    public class DepartmentRepository : IGenericRepositoryInterface<Department>
    {
        private readonly AppDbContext _appDbContext;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var department = await _appDbContext.Departments.FindAsync(id);
            if (department is null) return NotFound();

            _appDbContext.Departments.Remove(department);
            await Commit();
            return Success();
        }

        public async Task<List<Department>> GetAll() => await _appDbContext.Departments.ToListAsync();

        public async Task<Department> GetById(int id) => await _appDbContext.Departments.FindAsync(id);


        public async Task<GeneralResponse> Insert(Department item)
        {
            if (!await ChceckName(item.Name!)) return new GeneralResponse(false, "Department already added!");
            _appDbContext.Departments.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Department item)
        {
            var department = await _appDbContext.Departments.FindAsync(item.Id);
            if (department is null) return NotFound();

            department.Name = item.Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Department not found");
        private static GeneralResponse Success() => new(true, "Process completed");

        private async Task Commit() => await _appDbContext.SaveChangesAsync();
        private async Task<bool> ChceckName(string name)
        {
            var item = await _appDbContext.Departments.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

    }
}
