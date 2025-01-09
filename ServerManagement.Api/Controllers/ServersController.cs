using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerManagement.Api.Models;
using ServerManagement.Models;

namespace ServerManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ServersController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServers()
        {
            try
            {
                var allServers = await dbContext.Servers.ToListAsync();
                return Ok(allServers);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");

            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAllServers(int id)
        {

            try
            {
                var server = await dbContext.Servers.FindAsync(id);

                if (server is null)
                {
                    return NotFound();
                }

                return Ok(server);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddServer(ServersDto serversDto)
        {

            try
            {
                if (serversDto == null)
                    return BadRequest();

                var serverEntity = new Server
                {
                    IsOnline = serversDto.IsOnline,
                    City = serversDto.City,
                    Name = serversDto.Name
                };

                await dbContext.Servers.AddAsync(serverEntity);
                await dbContext.SaveChangesAsync();

                return Ok(serverEntity);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new server record");
            }

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateServer(int id, ServersDto serversDto)
        {
            try
            {
                var server = await dbContext.Servers.FindAsync(id);

                if (server is null)
                {
                    return NotFound();
                }

                server.Name = serversDto.Name;
                server.City = serversDto.City;
                server.IsOnline = serversDto.IsOnline;

                await dbContext.SaveChangesAsync();

                return Ok(server);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteServer(int id)
        {
            var server = await dbContext.Servers.FindAsync(id);

            if (server is null)
            {
                return NotFound();
            }

            dbContext.Servers.Remove(server);
            await dbContext.SaveChangesAsync();

            return Ok();

        }
    }
}
