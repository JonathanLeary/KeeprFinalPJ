using System.Data;
using System.Collections.Generic;
using Dapper;
using KeeprFinalPJ.Models;
using System;

namespace KeeprFinalPJ.Repositories
{
  public class VaultKeepRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepRepository(IDbConnection db)
    {
      _db = db;
    }
    public IEnumerable<VaultKeep> GetAll()
    {
      return _db.Query<VaultKeep>("SELECT * FROM vaultkeeps;");
    }
    public VaultKeep GetVaultKeepbyVaultId(int VaultId)
    {
      try
      {
        return _db.QuerySingle<VaultKeep>(@"SELECT * FROM vaultkeeps WHERE id = @VaultId", new { VaultId });
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
    public VaultKeep CreateVaultKeep(VaultKeep VaultKeep)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO vaultkeeps (userId, vaultId, keepId )
      VALUES ( @CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();
      ", VaultKeep);
      VaultKeep.Id = id;
      return VaultKeep;
    }
  }
}