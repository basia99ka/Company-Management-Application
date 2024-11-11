using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BaseLibrary.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : GenericController<Branch>
    {
        private readonly IGenericRepositoryInterface<Branch> _genericRepositoryInterface;
        private readonly ILogger<BranchController> _logger;


        public BranchController(IGenericRepositoryInterface<Branch> genericRepositoryInterface,
            ILogger<BranchController> logger)
            : base(genericRepositoryInterface, logger)
        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }
    }

}
