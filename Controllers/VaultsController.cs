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
  public class VaultsController : ControllerBase
  {
    readonly VaultRepository _repo;
    public VaultsController(VaultRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Vault>> Get()
    {
      return Ok(_repo.GetAll());
    }
    [HttpGet("user")]
    public ActionResult<IEnumerable<Vault>> GetAll(string UserId)
    {
      UserId = HttpContext.User.FindFirstValue("Id");
      return Ok(_repo.GetVaultbyCreator(UserId));

    }

    [HttpGet("{id}")]
    public ActionResult<Vault> Get(string id)
    {
      try
      {
        return Ok(_repo.GetbyVaultId(id));
      }
      catch
      {
        return BadRequest("Fuck you");
      }
    }

    [HttpPost]
    public ActionResult<Vault> Create([FromBody]Vault newVault)
    {
      return _repo.CreateVault(newVault);
    }

    [HttpDelete("{id}")]
    public ActionResult<Vault> Delete(string Id)
    {
      bool wasSuccessful = _repo.RemoveVault(Id);
      if (wasSuccessful)
      {
        return Ok();
      }
      return BadRequest();
    }


  }
}