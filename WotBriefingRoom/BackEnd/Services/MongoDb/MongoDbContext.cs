//using BackEnd.Models.BonusModels;
using BackEnd.Models.MongoDB;
using DTO.TanksModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using BonusModels.BonusModels;

namespace BackEnd.Services.MongoDb
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> options, IMongoClient client)
        {
            var s = options.Value;
            _database = client.GetDatabase(s.DatabaseName);
        }

        public IMongoCollection<TankData> Tanks => _database.GetCollection<TankData>("tanks");
        public IMongoCollection<Consumables> VehicleConsumables => _database.GetCollection<Consumables>("consumables");
        public IMongoCollection<CrewSkills> Skills => _database.GetCollection<CrewSkills>("crewskills");
        public IMongoCollection<Equipments> VehicleEquipments => _database.GetCollection<Equipments>("equipments");
        public IMongoCollection<FiledModefication> ModificationsTrees => _database.GetCollection<FiledModefication>("fieldModification");

    }
}

