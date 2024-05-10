using System.Reflection;
using System.Data.SqlClient;
using Memo.DAL.Interfaces;
using Memo.Domain.Models;
using Memo.Domain;
using System.Configuration;

namespace Memo.DAL.ADO.Net;

public class SqlContext : IDbContext
{
    public List<Vegetable> Vegetable { get; set; } = [];
    public List<Domain.Type> Type { get; set; } = [];
    public List<Planting> Planting { get; set; } = [];
    public List<Harvest> Harvest { get; set; } = [];

    private readonly string _dbSettings;

    private readonly SqlConnection _connection;

    public SqlContext()
    {
        _dbSettings = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        _connection = new SqlConnection(_dbSettings);
        SelectAll();
    }

    private void SelectAll()
    {
        _connection.Open();

        SqlCommand selectType = new("SELECT * FROM Type", _connection);
        Type = Select<Domain.Type>(selectType);

        SqlCommand selectPlanting = new("SELECT * FROM Plant", _connection);
        Planting = Select<Planting>(selectPlanting);

        SqlCommand selectHarvest = new("SELECT * FROM Harvest", _connection);
        Harvest = Select<Harvest>(selectHarvest);

        SqlCommand selectVegetable = new("SELECT * FROM Vegetable", _connection);
        Vegetable = Select<Vegetable>(selectVegetable);

        _connection.Close();
    }

    private List<T> Select<T>(SqlCommand command) where T : new()
    {
        List<T> entities = [];
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                T entity = new();
                PropertyInfo[] properties = typeof(T).GetProperties();

                for (int i = 0; i < properties.Length; i++)
                {
                    if (reader.GetName(i).Contains("ID") && i != 0)
                    {
                        int id = Convert.ToInt32(reader.GetValue(i));
                        if (entity is Vegetable)
                        {
                            if (reader.GetName(i).Contains("Type"))
                            {
                                Domain.Type type = Type.Find(x => x.Id == id)!;
                                properties[i].SetValue(entity, type);
                            }
                            if (reader.GetName(i).Contains("Plant"))
                            {
                                Planting planting = Planting.Find(x => x.Id == id)!;
                                properties[i].SetValue(entity, planting);
                            }
                            if (reader.GetName(i).Contains("Harvest"))
                            {
                                Harvest harvest = Harvest.Find(x => x.Id == id)!;
                                properties[i].SetValue(entity, harvest);
                            }
                        }
                        else
                        {
                            properties[i].SetValue(entity, null);
                        }
                    }
                    else
                    {
                        properties[i].SetValue(entity, Convert.ChangeType(reader.GetValue(i),
                                                   properties[i].PropertyType));
                    }
                }
                entities.Add(entity);
            }
        }
        return entities;
    }


    private void DeleteAll()
    {
        SqlCommand deleteVegetable = new("DELETE FROM Vegetable", _connection);
        deleteVegetable.ExecuteNonQuery();
    }


    private void InsertIntoType()
    {
        string typeStr = "";
        foreach (Domain.Type type in Type)
        {
            typeStr += type + ", ";
        }
        typeStr = typeStr.Remove(typeStr.Length - 2);

        SqlCommand insertType = new("INSERT INTO Type (TypeID, [Name]) " +
                                         "VALUES " + typeStr, _connection);
        insertType.ExecuteNonQuery();
    }
    private void InsertIntoPlanting()
    {
        string plantingStr = "";
        foreach (Planting planting in Planting)
        {
            plantingStr += planting + ", ";
        }
        plantingStr = plantingStr.Remove(plantingStr.Length - 2);

        SqlCommand insertPlanting = new("INSERT INTO Plant (PlantID, PlantingDate) " +
                                         "VALUES " + plantingStr, _connection);
        insertPlanting.ExecuteNonQuery();
    }
    private void InsertIntoHarvest()
    {
        string harvestStr = "";
        foreach (Harvest harvest in Harvest)
        {
            harvestStr += harvest + ", ";
        }
        harvestStr = harvestStr.Remove(harvestStr.Length - 2);

        SqlCommand insertHarvest = new("INSERT INTO Harvest (HarvestID, HarvestTime) " +
                                         "VALUES " + harvestStr, _connection);
        insertHarvest.ExecuteNonQuery();
    }
    private void InsertIntoVegetable()
    {
        string vegetableStr = "";
        foreach (Vegetable vegetable in Vegetable)
        {
            vegetable.Type = Type.Find(s => s.TypeV == vegetable.Type?.TypeV);
            vegetable.Planting = Planting.Find(s => s.PlantingTime == vegetable.Planting?.PlantingTime);
            vegetable.Harvest = Harvest.Find(s => s.HarvestTime == vegetable.Harvest?.HarvestTime);
            vegetableStr += vegetable + ", ";
        }
        vegetableStr = vegetableStr.Remove(vegetableStr.Length - 2);

        SqlCommand insertVegetable = new("INSERT INTO Vegetable ([Name], TypeID, Height, PlantingID, HarvestID) " +
                                         "VALUES " + vegetableStr, _connection);
        insertVegetable.ExecuteNonQuery();
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

