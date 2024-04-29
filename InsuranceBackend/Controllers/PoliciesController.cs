using InsuranceBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceBackend.Controllers
{
    [Route("api/policies")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly IInsuranceContext _context;
        public PoliciesController(IInsuranceContext context)=>_context=context;

        [HttpGet]
        public async Task<IEnumerable<Policy>> getPolicies()
        {
            return await _context.Policies.ToListAsync();
        }

        [HttpGet("{id}",Name ="GetPolicyById")]
        public async Task<IActionResult> GetPolicyById(int id)
        {
            var policy=await _context.Policies.FirstOrDefaultAsync(x=>x.User_id==id);

            return Ok(policy);

        }

        [HttpGet("all/{userId}", Name = "GetPoliciesByUserId")]
        public async Task<IActionResult> GetPoliciesByUserId(int userId)
        {
            var policies = await _context.Policies.Where(x => x.User_id == userId).ToListAsync();

            return Ok(policies);
        }

    }
}
