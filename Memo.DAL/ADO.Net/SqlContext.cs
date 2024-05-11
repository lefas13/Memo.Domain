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
        Type = SelectT(selectType);

        SqlCommand selectPlanting = new("SELECT * FROM Plant", _connection);
        Planting = SelectP(selectPlanting);

        SqlCommand selectHarvest = new("SELECT * FROM Harvest", _connection);
        Harvest = SelectH(selectHarvest);

        SqlCommand selectVegetable = new("SELECT * FROM Vegetable", _connection);
        Vegetable = SelectV(selectVegetable);

        _connection.Close();
    }

    private List<Vegetable> SelectV(SqlCommand command)
    {
        List<Vegetable> entities = new List<Vegetable>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Vegetable entity = new();
                PropertyInfo[] properties = typeof(Vegetable).GetProperties();

                for (int i = 0; i < properties.Length; i++)
                {
                    if (reader.GetName(i).Contains("ID") && i != 0)
                    {
                        int id = Convert.ToInt32(reader.GetValue(i));
                        if (entity is Vegetable)
                        {
                            if (reader.GetName(i).Contains("Type"))
                            {
                                List<Domain.Type> types = Type;
                                foreach (Domain.Type type in types)
                                {
                                    if (type.Id == id)
                                    properties[i].SetValue(entity, type);
                                }
                            }
                            if (reader.GetName(i).Contains("Plant"))
                            {
                                List<Planting> plantings = Planting;
                                foreach (Planting planting in plantings)
                                {
                                    if (planting.Id == id)
                                    properties[i].SetValue(entity, planting);
                                }
                            }
                            if (reader.GetName(i).Contains("Harvest"))
                            {
                                List<Harvest> harvests = Harvest;
                                foreach (Harvest harvest in harvests)
                                {
                                    if (harvest.Id == id)
                                    properties[i].SetValue(entity, harvest);
                                }
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

    private List<Planting> SelectP(SqlCommand command)
    {
        List<Planting> entities = new List<Planting>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Planting entity = new();
                PropertyInfo[] properties = typeof(Planting).GetProperties();

                for (int i = 0; i < properties.Length; i++)
                {
                    if (reader.GetName(i).Contains("ID") && i != 0)
                    {
                        int id = Convert.ToInt32(reader.GetValue(i));
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

    private List<Memo.Domain.Type> SelectT(SqlCommand command)
    {
        List<Memo.Domain.Type> entities = new List<Memo.Domain.Type>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Memo.Domain.Type entity = new();
                PropertyInfo[] properties = typeof(Memo.Domain.Type).GetProperties();

                for (int i = 0; i < properties.Length; i++)
                {
                    if (reader.GetName(i).Contains("ID") && i != 0)
                    {
                        int id = Convert.ToInt32(reader.GetValue(i));
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

    private List<Harvest> SelectH(SqlCommand command)
    {
        List<Harvest> entities = new List<Harvest>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Harvest entity = new();
                PropertyInfo[] properties = typeof(Harvest).GetProperties();

                for (int i = 0; i < properties.Length; i++)
                {
                    if (reader.GetName(i).Contains("ID") && i != 0)
                    {
                        int id = Convert.ToInt32(reader.GetValue(i));
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

    private void InsertIntoVegetable()
    {
        string vegetableStr = "";
        foreach (Vegetable vegetable in Vegetable)
        {
            List<Domain.Type> types = Type;
            List<Planting> plants = Planting;
            List<Harvest> harvests = Harvest;
            foreach (Domain.Type type in types)
            {
                if (type.TypeV == vegetable.Type?.TypeV)
                    vegetable.Type = type;
            }
            foreach (Planting planting in plants)
            { 
                if (planting.PlantingTime == vegetable.Planting?.PlantingTime) 
                vegetable.Planting = planting;
            }
            foreach (Harvest harvest in harvests)
            {
                if (harvest.HarvestTime == vegetable.Harvest?.HarvestTime)
                vegetable.Harvest = harvest;
            }
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

