using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BaseLibrary.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : GenericController<Employee>
    {
        private readonly IGenericRepositoryInterface<Employee> _genericRepositoryInterface;
        private readonly ILogger<EmployeeController> _logger;


        public EmployeeController(IGenericRepositoryInterface<Employee> genericRepositoryInterface, ILogger<EmployeeController> logger)
            : base(genericRepositoryInterface, logger)
        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }
    }
}
