using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using BaseLibrary.Contracts;

namespace ServerLibrary.Repositories.Implementation
{
    public class EmployeeRepository : IGenericRepositoryInterface<Employee>
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);
            if (employee is null) return NotFound();

            _appDbContext.Employees.Remove(employee);
            await Commit();
            return Success();
        }

        public async Task<List<Employee>> GetAll()
        {
            var employees = await _appDbContext.
                 Employees.AsNoTracking()
                .Include(b => b.Branch)
                .ToListAsync();
            return employees;
        }
      

        public async Task<Employee> GetById(int id) => await _appDbContext.Employees.FindAsync(id);


        public async Task<GeneralResponse> Insert(Employee item)
        {
            if (!await ChceckName(item.Name!)) return new GeneralResponse(false, "Employee already added!");
            _appDbContext.Employees.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Employee employee)
        {
            var findUser = await _appDbContext.Employees.FirstOrDefaultAsync(e=>e.Id==employee.Id);
            if (findUser is null) return new GeneralResponse(false, "Employee does not exist");

            findUser.Name = employee.Name;
            findUser.Address = employee.Address;
            findUser.TelephoneNumber = employee.TelephoneNumber;
            findUser.BranchId = employee.BranchId;
            findUser.FileName = employee.FileName;
            findUser.JobName = employee.JobName;
            findUser.Other = employee.Other;

            await _appDbContext.SaveChangesAsync();
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Employee not found");
        private static GeneralResponse Success() => new(true, "Process completed");

        private async Task Commit() => await _appDbContext.SaveChangesAsync();
        private async Task<bool> ChceckName(string name)
        {
            var item = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

    }
}
