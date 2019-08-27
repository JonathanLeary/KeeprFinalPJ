using System.Data;
using System.Collections.Generic;
using Dapper;
using KeeprFinalPJ.Models;



namespace KeeprFinalPJ.Repositories
{
  public class VaultRepository
  {
    readonly IDbConnection _db;
    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }
    public IEnumerable<Vault> GetAll()
    {
      return _db.Query<Vault>("SELECT * FROM vaults;");
    }
    public IEnumerable<Vault> GetVaultbyCreator(string id)
    {
      string query = "SELECT * FROM keeps WHERE id = @id";
      return _db.Query<Vault>(query, new { id });
    }
    public Vault GetbyVaultId(string id)
    {
      string query = "SELECT * FROM vaults WHERE id = @id";
      return _db.QueryFirstOrDefault<Vault>(query, new { id });
    }
    public Vault CreateVault(Vault Vault)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO vaults (name, description, userId)
      VALUES (@Name, @Description, @UserId);
      SELECT LAST_INSERT_ID();
      ", Vault);
      Vault.Id = id;
      return Vault;
    }
    public bool RemoveVault(string vaultId)
    {
      var success = _db.Execute("DELETE FROM vaults WHERE id = @vaultId", new { vaultId });
      return success > 0;
    }
  }
}