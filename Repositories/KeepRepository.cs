using System.Data;
using System.Collections.Generic;
using Dapper;
using KeeprFinalPJ.Models;
using System;

namespace KeeprFinalPJ.Repositories
{
  public class KeepRepository
  {
    readonly IDbConnection _db;
    public KeepRepository(IDbConnection db)
    {
      _db = db;
    }
    public IEnumerable<Keep> GetAll()
    {
      return _db.Query<Keep>("SELECT * FROM keeps;");
    }
    public IEnumerable<Keep> GetKeepbyCreator(string id)
    {
      string query = "SELECT * FROM keeps WHERE id = @id";
      return _db.Query<Keep>(query, new { id });
    }
    public Keep GetKeepbyId(int id)
    {
      try
      {
        return _db.QuerySingle<Keep>(@"SELECT * FROM keeps WHERE id = @id", new { id });
      }
      catch (Exception e)
      {
        throw new ConstraintException(e.Message);
      }

    }
    public Keep CreateKeep(Keep keep)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO keeps (name, img, description)
      VALUES (@Name, @Image, @Description);
      SELECT LAST_INSERT_ID();
      ", keep);
      return keep;
    }
    public bool RemoveKeep(string keepId)
    {
      var success = _db.Execute("DELETE FROM keeps WHERE id = @keepId", new { keepId });
      return success > 0;
    }
  }
}