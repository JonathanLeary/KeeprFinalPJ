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
    public IEnumerable<Keep> GetbyVaultId(int VaultId, string UserId)
    {
      string query = @"
      SELECT * FROM vaultkeeps vk
      INNER JOIN keeps k ON k.id = vk.KeepId
      WHERE(VaultId = @VaultId AND vk.UserId = @UserId)
      ";
      return _db.Query<Keep>(query, new { VaultId, UserId });
    }
    public VaultKeep CreateVaultKeep(VaultKeep VaultKeep)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO vaultkeeps (userId, vaultId, keepId )
      VALUES ( @UserId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();
      ", VaultKeep);
      VaultKeep.Id = id;
      return VaultKeep;
    }
  }
}