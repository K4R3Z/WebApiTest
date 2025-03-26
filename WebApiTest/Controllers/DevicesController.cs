using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebApiTest.Context;
using WebApiTest.Models;
namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("api/Devices")]
    [Authorize]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceContext _context;

        public DevicesController(DeviceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddDevice(int id, [FromBody] Device device)
        {
            if (id != device.Id)
                return BadRequest();

            if (await _context.Devices.AnyAsync(d => d.Id == id))
                return Conflict();

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDevices), new { id = device.Id }, device);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return NotFound();

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("type/{type}")]
        public async Task<IActionResult> DeleteDevicesByType(string type)
        {
            var devices = await _context.Devices
                .Where(d => d.Type == type)
                .ToListAsync();

            if (!devices.Any()) return NotFound();

            _context.Devices.RemoveRange(devices);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
