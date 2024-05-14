using Memo.DAL.Interfaces;
using Memo.Domain;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Memo.Domain.Models;

namespace Memo.DAL.ADO.Net;

public class SqlContextLinq : IDbContext
{
    private readonly SqlConnection _connection;

    public List<Vegetable> Vegetable { get; set; } = [];
    public List<Domain.Type> Type { get; set; } = [];
    public List<Planting> Planting { get; set; } = [];
    public List<Harvest> Harvest { get; set; } = [];

    private readonly string _dbSettings;

    private readonly DataTable _dtVegetable;
    private readonly DataTable _dtType;
    private readonly DataTable _dtPlanting;
    private readonly DataTable _dtHarvest;

    public SqlContextLinq()
    {
        string dbSettings = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        _connection = new SqlConnection(dbSettings);


        SqlDataAdapter _adapterVegetable = new("SELECT * FROM Vegetable", _connection);
        SqlDataAdapter _adapterType = new("SELECT * FROM Type", _connection);
        SqlDataAdapter _adapterPlanting = new("SELECT * FROM Plant", _connection);
        SqlDataAdapter _adapterHarvest = new("SELECT * FROM Harvest", _connection);

        _dtVegetable = new();
        _dtType = new();
        _dtPlanting = new();
        _dtHarvest = new();

        _connection.Open();

        _adapterVegetable.Fill(_dtVegetable);
        _adapterType.Fill(_dtType);
        _adapterPlanting.Fill(_dtPlanting);
        _adapterHarvest.Fill(_dtHarvest);

        _connection.Close();

        SelectVegetable();
    }

    private void SelectType()
    {
        var queryTypes = _dtType.AsEnumerable().Select(types => new
        {
            Id = types.Field<int>("TypeID"),
            TypeV = types.Field<string>("Name"),
        });

        Type = [];
        foreach (var query in queryTypes)
        {
            if (query.TypeV != null)
            {
                Type.Add(new Domain.Type
                {
                    Id = query.Id,
                    TypeV = query.TypeV,
                });
            }
        }
    }

    private void SelectPlanting()
    {
        var queryPlantings = _dtPlanting.AsEnumerable().Select(plantings => new
        {
            Id = plantings.Field<int>("PlantID"),
            PlantingTime = plantings.Field<DateTime>("PlantingDate"),
        });

        Planting = [];
        foreach (var query in queryPlantings)
        {
            if (query.PlantingTime != null)
            {
                Planting.Add(new Planting
                {
                    Id = query.Id,
                    PlantingTime = Convert.ToDateTime(query.PlantingTime),
                });
            }
        }
    }

    private void SelectHarvest()
    {
        var queryHarvests = _dtHarvest.AsEnumerable().Select(harvests => new
        {
            Id = harvests.Field<int>("HarvestID"),
            HarvestTime = harvests.Field<int>("HarvestTime"),
        });

        Harvest = [];
        foreach (var query in queryHarvests)
        {
            if (query.HarvestTime != 0)
            {
                Harvest.Add(new Harvest
                {
                    Id = query.Id,
                    HarvestTime = query.HarvestTime,
                });
            }
        }
    }

    private void SelectVegetable()
    {
        SelectType();
        SelectPlanting();
        SelectHarvest();

        var queryVegetable = _dtVegetable.AsEnumerable().Select(vegetable => new
        {
            Id = vegetable.Field<int>("VegetableID"),
            Name = vegetable.Field<string>("Name"),
            Type = vegetable.Field<int>("TypeID"),
            HeightSm = vegetable.Field<decimal>("Height"),
            Planting = vegetable.Field<int>("PlantingID"),
            Harvest = vegetable.Field<int>("HarvestID")
        });

        Vegetable = [];
        foreach (var query in queryVegetable)
        {
            if (query.Name != null)
            {
                Domain.Type qType = Type.Find(x => x.Id == query.Type)!;
                Planting qPlanting = Planting.Find(x => x.Id == query.Planting)!;
                Harvest qHarvest = Harvest.Find(x => x.Id == query.Harvest)!;
                Vegetable.Add(new Vegetable
                {
                    Id = query.Id,
                    Name = query.Name,
                    Type = qType,
                    HeightSm = Convert.ToDouble(query.HeightSm),
                    Planting = qPlanting,
                    Harvest = qHarvest,
                });
            }
        }
    }

    private void DeleteAll()
    {
        SqlDataAdapter adapter = new()
        {
            DeleteCommand = new("DELETE FROM Vegetable", _connection)
        };
        adapter.DeleteCommand.ExecuteNonQuery();
        _dtVegetable.Clear();
        adapter.Update(_dtVegetable);
        _dtVegetable.AcceptChanges();

        adapter = new()
        {
            DeleteCommand = new("DELETE FROM Type", _connection)
        };
        adapter.DeleteCommand.ExecuteNonQuery();
        _dtType.Clear();
        adapter.Update(_dtType);
        _dtType.AcceptChanges();

        adapter = new()
        {
            DeleteCommand = new("DELETE FROM Plant", _connection)
        };
        adapter.DeleteCommand.ExecuteNonQuery();
        _dtPlanting.Clear();
        adapter.Update(_dtPlanting);
        _dtPlanting.AcceptChanges();

        adapter = new()
        {
            DeleteCommand = new("DELETE FROM Harvest", _connection)
        };
        adapter.DeleteCommand.ExecuteNonQuery();
        _dtHarvest.Clear();
        adapter.Update(_dtHarvest);
        _dtHarvest.AcceptChanges();
    }


    private void InsertIntoType()
    {
        foreach (Domain.Type type in Type)
        {
            DataRow row = _dtType.NewRow();
            row["[Name]"] = type.TypeV;

            _dtType.Rows.Add(row);
        }
        SqlDataAdapter adapter = new()
        {
            InsertCommand = new("INSERT INTO Type ([Name]) " +
                                            "VALUES (@Name)", _connection)
        };
        adapter.InsertCommand.Parameters.Add("@Name", SqlDbType.VarChar, 50, "[Name]");

        adapter.Update(_dtType);
        _dtType.AcceptChanges();
    }

    private void InsertIntoPlanting()
    {
        foreach (Planting planting in Planting)
        {
            DataRow row = _dtPlanting.NewRow();

            row["PlantingDate"] = planting.PlantingTime;

            _dtPlanting.Rows.Add(row);

        }

        SqlDataAdapter adapter = new()
        {
            InsertCommand = new("INSERT INTO Plant (PlantingDate) " +
            "VALUES (@Date)", _connection),

        };

        adapter.InsertCommand.Parameters.Add("@Date", SqlDbType.Date, 100, "PlantingDate");

        adapter.Update(_dtPlanting);
        _dtPlanting.AcceptChanges();
    }

    private void InsertIntoHarvest()
    {
        foreach (Harvest harvest in Harvest)
        {
            DataRow row = _dtHarvest.NewRow();

            row["HarvestTime"] = harvest.HarvestTime;

            _dtHarvest.Rows.Add(row);

        }

        SqlDataAdapter adapter = new()
        {
            InsertCommand = new("INSERT INTO Harvest (HarvestTime) " +
            "VALUES (@Time)", _connection),

        };

        adapter.InsertCommand.Parameters.Add("@Time", SqlDbType.Int, 10, "HarvestTime");

        adapter.Update(_dtHarvest);
        _dtHarvest.AcceptChanges();
    }

    private void InsertIntoVegetable()
    {
        foreach (Vegetable vegetable in Vegetable)
        {
            vegetable.Type = Type.Find(s => s.TypeV == vegetable.Type?.TypeV);
            vegetable.Planting = Planting.Find(s => s.PlantingTime == vegetable.Planting?.PlantingTime);
            vegetable.Harvest = Harvest.Find(s => s.HarvestTime == vegetable.Harvest?.HarvestTime);

            DataRow row = _dtVegetable.NewRow();
            row["Name"] = vegetable.Name;
            row["TypeID"] = vegetable.Type?.Id;
            row["Height"] = vegetable.HeightSm;
            row["PlantingID"] = vegetable.Planting?.Id;
            row["HarvestID"] = vegetable.Harvest?.Id;

            _dtVegetable.Rows.Add(row);
        }

        SqlDataAdapter adapter = new()
        {
            InsertCommand = new("INSERT INTO Vegetable (Name, TypeID, Height, PlantingID, HarvestID) " +
                                "VALUES (@Name, @TypeID, @HeightSm, @PlantingID, @HarvestID)", _connection)
        };


        adapter.InsertCommand.Parameters.Add("@Name", SqlDbType.VarChar, 50, "Name");
        adapter.InsertCommand.Parameters.Add("@TypeID", SqlDbType.Int, 4, "TypeID");
        adapter.InsertCommand.Parameters.Add("@HeightSm", SqlDbType.Decimal, 18, "Height");
        adapter.InsertCommand.Parameters.Add("@PlantingID", SqlDbType.Int, 4, "PlantingID");
        adapter.InsertCommand.Parameters.Add("@HarvestID", SqlDbType.Int, 4, "HarvestID");

        adapter.Update(_dtVegetable);
        _dtVegetable.AcceptChanges();
    }

    private void UpdateAll()
    {
        DeleteAll();
        InsertIntoVegetable();
    }

    public void SaveChanges()
    {
        _connection.Open();

        UpdateAll();

        _connection.Close();
    }
}
