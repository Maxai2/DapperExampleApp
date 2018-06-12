using DapperExampleApp;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Linq;
using System.Data;
using Dapper;
using Dapper.FastCrud;
using Dapper.Mapper;

public class DappositoryCRUD
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
        return Connection.Find<WeaponType>().AsList();
    }

    public WeaponType GetWeaponTypeById(int id)
    {
        return Connection.Get<WeaponType>(new WeaponType { Id = id });
    }

    public void AddWeaponType(WeaponType weaponType)
    {
        Connection.Insert<WeaponType>(weaponType);
    }

    public ICollection<Weapon> GetWeapons() 
    {
        string sql = "SELECT * FROM Weapons JOIN WeaponTypes ON Weapons.WeaponType_Id = WeaponTypes.Id";
        var weapons = Connection.Query<Weapon, WeaponType>(sql).AsList();
        return weapons;
    }

    public ICollection<Unit> GetUnits()
    {
        string sql = "SELECT * FROM Units JOIN Weapons ON Units.Weapon_Id = Weapons.Id JOIN WeaponTypes ON Weapons.WeaponType_Id = WeaponTypes.Id";
        var units = Connection.Query<Unit, Weapon, WeaponType>(sql).AsList();
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

