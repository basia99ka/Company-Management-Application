using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BaseLibrary.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : GenericController<Project>
    {
        private readonly IGenericRepositoryInterface<Project> _genericRepositoryInterface;
        private readonly ILogger<ProjectController> _logger;


        public ProjectController(IGenericRepositoryInterface<Project> genericRepositoryInterface, ILogger<ProjectController> logger)
            : base(genericRepositoryInterface, logger)
        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;

        }
    }
}
