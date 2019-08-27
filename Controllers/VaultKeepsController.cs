using KeeprFinalPJ.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using KeeprFinalPJ.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace KeeprFinalPJ.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepRepository _repo;

    public VaultKeepsController(VaultKeepRepository repo)
    {
      _repo = repo;
    }
    [Authorize]
    [HttpPost]
    public ActionResult<VaultKeep> Create([FromBody]VaultKeep newVaultKeep)
    {
      newVaultKeep.CreatorId = HttpContext.User.FindFirstValue("Id");
      return Ok(_repo.CreateVaultKeep(newVaultKeep));
    }
    [HttpGet]
    public ActionResult<VaultKeep> GetOne(int id)
    {
      return _repo.GetVaultKeepbyVaultId(id);
    }
  }
}
