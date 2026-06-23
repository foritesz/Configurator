using BackEnd.Services.MongoDb;
using DTO;
using DTO.PagedResultModel;
using DTO.TanksModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TanksController : ControllerBase
{
    private readonly MongoDbContext _db;

    public TanksController(MongoDbContext db)
    {
        _db = db;
    }

    // GET api/tanks?search=...&page=1&pageSize=20
    [HttpGet]
    public async Task<ActionResult<PagedResult<FrontEndData>>> GetTanks(
    [FromQuery] string? search,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20)
    {
        try
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 20;

            /*var filter = string.IsNullOrWhiteSpace(search)
                ? Builders<TankData>.Filter.Empty
                : Builders<TankData>.Filter.Or(
                    Builders<TankData>.Filter.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(search, "i")),
                    Builders<TankData>.Filter.Regex(x => x.Type, new MongoDB.Bson.BsonRegularExpression(search, "i"))
                );*/

            var safeSearch = Regex.Escape((search ?? "").Trim());

            var filter = string.IsNullOrWhiteSpace(safeSearch)
                ? Builders<TankData>.Filter.Empty
                : Builders<TankData>.Filter.Regex(
                    x => x.Name,
                    new MongoDB.Bson.BsonRegularExpression("^" + safeSearch, "i"));

            var totalCount = (int)await _db.Tanks.CountDocumentsAsync(filter);

            var items = await _db.Tanks
                .Find(filter)
                .SortBy(t => t.TankId)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var result = new PagedResult<TankData>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = ex.Message,
                inner = ex.InnerException?.Message,
                type = ex.GetType().FullName
            });
        }
    }

    [HttpGet("test")]
    public async Task<ActionResult> Test()
    {
        try
        {
            var one = await _db.Tanks.Find(Builders<TankData>.Filter.Empty).FirstOrDefaultAsync();
            return Ok(one);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = ex.Message,
                inner = ex.InnerException?.Message,
                type = ex.GetType().FullName
            });
        }
    }

    // GET api/tanks/1234
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FrontEndData>> GetById([FromRoute] int id)
    {
        var tank = await _db.Tanks.Find(t => t.TankId == id).FirstOrDefaultAsync();
        return tank is null ? NotFound() : Ok(tank);
    }

    [HttpGet("debug")]
    public async Task<ActionResult> Debug()
    {
        try
        {
            var ids = await _db.Tanks
                .Find(Builders<TankData>.Filter.Empty)
                .SortBy(t => t.TankId)
                .Project(t => t.TankId)
                .ToListAsync();

            var okIds = new List<int>();
            var bad = new List<object>();

            foreach (var id in ids)
            {
                try
                {
                    var tank = await _db.Tanks.Find(t => t.TankId == id).FirstOrDefaultAsync();
                    okIds.Add(id);
                }
                catch (Exception ex)
                {
                    bad.Add(new
                    {
                        id,
                        message = ex.Message,
                        inner = ex.InnerException?.Message,
                        type = ex.GetType().FullName
                    });
                }
            }

            return Ok(new
            {
                okCount = okIds.Count,
                badCount = bad.Count,
                bad
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = ex.Message,
                inner = ex.InnerException?.Message,
                type = ex.GetType().FullName
            });
        }
    }
}
