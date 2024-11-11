using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BaseLibrary.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : GenericController<Department>
    {
        private readonly IGenericRepositoryInterface<Department> _genericRepositoryInterface;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IGenericRepositoryInterface<Department> genericRepositoryInterface, ILogger<DepartmentController> logger)
            : base(genericRepositoryInterface, logger)
        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }
    }

}
