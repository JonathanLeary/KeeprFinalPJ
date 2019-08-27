using KeeprFinalPJ.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using KeeprFinalPJ.Models;
using System.Security.Claims;
using System;

namespace KeeprFinalPJ.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class KeepsController : ControllerBase
  {
    private readonly KeepRepository _repo;
    public KeepsController(KeepRepository repo)
    {
      _repo = repo;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      return Ok(_repo.GetAll());
    }
    [HttpGet("user")]
    public ActionResult<IEnumerable<Keep>> GetAll()
    {
      var id = HttpContext.User.FindFirstValue("Id");
      return Ok(_repo.GetKeepbyCreator(id));

    }
    [HttpGet("{id}")]
    public ActionResult<Keep> GetOne(int id)
    {
      return _repo.GetKeepbyId(id);
    }
    [HttpPost]
    public ActionResult<Keep> Create([FromBody]Keep newKeep)
    {
      return _repo.CreateKeep(newKeep);
    }
    [HttpDelete("{id}")]
    public ActionResult<Vault> Delete(string Id)
    {
      bool wasSuccessful = _repo.RemoveKeep(Id);
      if (wasSuccessful)
      {
        return Ok();
      }
      return BadRequest();
    }

  }

  internal class AthenticateAttribute : Attribute
  {
  }
}
