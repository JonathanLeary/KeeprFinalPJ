using System.Data;
using System.Collections.Generic;
using Dapper;
using KeeprFinalPJ.Models;



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
    public VaultKeep GetbyVaultKeepId(string vaultkeepId)
    {
      string query = "SELECT * FROM vaultkeeps WHERE id = @vaultkeepId";
      return _db.QueryFirstOrDefault<VaultKeep>(query, new { vaultkeepId });
    }
    public VaultKeep CreateVaultKeep(VaultKeep VaultKeep)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO vaultkeeps (userId, vaultId, keepId )
      VALUES ( @CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();
      ", VaultKeep);
      return VaultKeep;
    }
  }
}