using DapperExampleApp;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Linq;
using System.Data;
using Dapper;

public class Dappository
{
    private const string connStr = @"Data Source=.; Initial Catalog=DapperTestDb; Integrated Security=false; User Id = admin; Password = admin";

    private IDbConnection connection = null;
    private IDbConnection Connection
    {
        get
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection = new SqlConnection(connStr);
            }
            return connection;
        }
    }

    public ICollection<WeaponType> GetWeaponTypes()
    {
        return Connection.Query<WeaponType>("SELECT * FROM WeaponTypes").AsList();
    }

    public WeaponType GetWeaponTypeById(int id)
    {
        return Connection.QueryFirst<WeaponType>("SELECT * FROM WeaponTypes WHERE Id = @Id", new { Id = id });
    }

    public void AddWeaponType(WeaponType weaponType)
    {
        Connection.Execute("INSERT INTO WeaponTypes (Title) VALUES (@Title)", new { Title = weaponType.Title });
    }

    public ICollection<Weapon> GetWeapons() 
    {
        string sql = "SELECT * FROM Weapons JOIN WeaponTypes ON Weapons.WeaponType_Id = WeaponTypes.Id";
        var weapons = Connection.Query<Weapon, WeaponType, Weapon>(sql, (weapon, type) => { weapon.Type = type; return weapon; }, splitOn: "Id").AsList();
        return weapons;
    }

    public ICollection<Unit> GetUnits()
    {
        string sql = "SELECT * FROM Units JOIN Weapons ON Units.Weapon_Id = Weapons.Id JOIN WeaponTypes ON Weapons.WeaponType_Id = WeaponTypes.Id";
        var units = Connection.Query<Unit, Weapon, WeaponType, Unit>(sql, (unit, weapon, type) => { weapon.Type = type; unit.Weapon = weapon; return unit; }, splitOn: "Id, Id").AsList();
        return units;
    }

    public ICollection<Army> GetArmies()
    {
        string sql = "SELECT * FROM Armys JOIN Units ON Armys.Id = Units.Army_Id JOIN Weapons ON Units.Weapon_Id = Weapons.Id JOIN WeaponTypes ON Weapons.WeaponType_Id = WeaponTypes.Id";

        Dictionary<int, Army> armies = new Dictionary<int, Army>();

        var rote = Connection.Query<Army, Unit, Weapon, WeaponType, Army>(sql, (army, unit, weapon, type) =>
        {
            weapon.Type = type; unit.Weapon = weapon; if (armies.ContainsKey(army.Id))
            {
                army = armies[army.Id];
            }
            else
            {
                armies.Add(army.Id, army);
                army.Units = new List<Unit>();

            } army.Units.Add(unit); return army; }, splitOn: "Id, Id, Id").Distinct().AsList();
        return rote;
    }

}

