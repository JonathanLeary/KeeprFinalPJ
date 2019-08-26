using KeeprFinalPJ.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using KeeprFinalPJ.Models;
using System.Security.Claims;

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
    [HttpGet]
    public ActionResult<IEnumerable<VaultKeep>> Get()
    {
      var x = new User();
      return Ok(_repo.GetAll());
    }
    [HttpPost]
    public ActionResult<VaultKeep> Create([FromBody]VaultKeep newVaultKeep)
    {
      var id = HttpContext.User.FindFirstValue("Id");
      return Ok(_repo.CreateVaultKeep(newVaultKeep));
    }
  }
}
