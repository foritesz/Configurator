using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Consumables = BonusModels.BonusModels.Consumables;
using Equipments = BonusModels.BonusModels.Equipments;
using CrewSkills = BonusModels.BonusModels.CrewSkills;
using FiledModefication = BonusModels.BonusModels.FiledModefication;
using BackEnd.Services.MongoDb;

namespace BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BonusesController : ControllerBase
{
    private readonly MongoDbContext _db;

    public BonusesController(MongoDbContext db)
    {
        _db = db;
    }

    // -------- CONSUMABLES --------
    [HttpGet("consumables")]
    public async Task<ActionResult<IEnumerable<Consumables>>> GetConsumables()
    {
        var list = await _db.VehicleConsumables.Find(_ => true).ToListAsync();
        return Ok(list);
    }

    [HttpGet("consumables/{id:int}")]
    public async Task<ActionResult<Consumables>> GetConsumableById(int id)
    {
        var item = await _db.VehicleConsumables.Find(c => c.Id == id).FirstOrDefaultAsync();
        return item is null ? NotFound() : Ok(item);
    }

    // -------- EQUIPMENTS --------
    [HttpGet("equipments")]
    public async Task<ActionResult<IEnumerable<Equipments>>> GetEquipments()
    {
        var list = await _db.VehicleEquipments.Find(_ => true).ToListAsync();
        return Ok(list);
    }

    [HttpGet("equipments/{compositeId}")]
    public async Task<ActionResult<Equipments>> GetEquipmentById(string compositeId)
    {
        var item = await _db.VehicleEquipments.Find(e => e._Id == compositeId).FirstOrDefaultAsync();
        return item is null ? NotFound() : Ok(item);
    }

    // -------- CREW SKILLS --------
    [HttpGet("crewskills")]
    public async Task<ActionResult<IEnumerable<CrewSkills>>> GetCrewSkills()
    {
        var list = await _db.Skills.Find(_ => true).ToListAsync();
        return Ok(list);
    }

    [HttpGet("crewskills/{id:int}")]
    public async Task<ActionResult<CrewSkills>> GetCrewSkillById(int id)
    {
        var item = await _db.Skills.Find(c => c.Id == id).FirstOrDefaultAsync();
        return item is null ? NotFound() : Ok(item);
    }

    // -------- FIELD MODIFICATIONS --------
    [HttpGet("fieldmods")]
    public async Task<ActionResult<IEnumerable<FiledModefication>>> GetFieldMods()
    {
        var list = await _db.ModificationsTrees.Find(_ => true).ToListAsync();
        return Ok(list);
    }

    [HttpGet("fieldmods/{id}")]
    public async Task<ActionResult<FiledModefication>> GetFieldModById(string id)
    {
        var item = await _db.ModificationsTrees.Find(fm => fm.Id == id).FirstOrDefaultAsync();
        return item is null ? NotFound() : Ok(item);
    }
}
