using KeeprFinalPJ.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using KeeprFinalPJ.Models;
using System.Security.Claims;

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
      var x = new User();
      return Ok(_repo.GetAll());
    }
    [HttpGet("{id}")]
    public ActionResult<Vault> GetOne(string id)
    {
      return _repo.GetbyVaultId(id);
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