using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DichVuSim_BE.DTO;
using DichVuSim_BE.Models;
using DichVuSim_BE.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DichVuSim_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoiCuocController : ControllerBase
    {
        private readonly VMchargerDBContext _context;

        public GoiCuocController(VMchargerDBContext context)
        {
            _context = context;
        }

        // GET: api/GoiCuoc
        [HttpGet("getAllGoiCuoc")]
        public async Task<ActionResult<IEnumerable<GoiCuocDTO>>> GetGoiCuocs()
        {
            var goiCuocs = await _context.GoiCuocs.ToListAsync();
            return goiCuocs.Select(gc => new GoiCuocDTO
            {
                MaGoiCuoc = gc.MaGoiCuoc,
                TenGoiCuoc = gc.TenGoiCuoc,
                MoTa = gc.MoTa,
                Gia = gc.Gia,
                ThoiHanSuDung = gc.ThoiHanSuDung,
                NgayTao = gc.NgayTao,
                NgayCapNhat = gc.NgayCapNhat
            }).ToList();
        }

        // GET: api/GoiCuoc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GoiCuocDTO>> GetGoiCuoc(int id)
        {
            var goiCuoc = await _context.GoiCuocs.FindAsync(id);

            if (goiCuoc == null)
            {
                return NotFound();
            }

            var goiCuocDTO = new GoiCuocDTO
            {
                MaGoiCuoc = goiCuoc.MaGoiCuoc,
                TenGoiCuoc = goiCuoc.TenGoiCuoc,
                MoTa = goiCuoc.MoTa,
                Gia = goiCuoc.Gia,
                ThoiHanSuDung = goiCuoc.ThoiHanSuDung,
                NgayTao = goiCuoc.NgayTao,
                NgayCapNhat = goiCuoc.NgayCapNhat
            };

            return goiCuocDTO;
        }

        // PUT: api/GoiCuoc/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoiCuoc(int id, GoiCuocDTO goiCuocDTO)
        {
            if (id != goiCuocDTO.MaGoiCuoc)
            {
                return BadRequest();
            }

            var goiCuoc = await _context.GoiCuocs.FindAsync(id);
            if (goiCuoc == null)
            {
                return NotFound();
            }

            goiCuoc.TenGoiCuoc = goiCuocDTO.TenGoiCuoc;
            goiCuoc.MoTa = goiCuocDTO.MoTa;
            goiCuoc.Gia = goiCuocDTO.Gia;
            goiCuoc.ThoiHanSuDung = goiCuocDTO.ThoiHanSuDung;
            goiCuoc.NgayTao = goiCuocDTO.NgayTao;
            goiCuoc.NgayCapNhat = goiCuocDTO.NgayCapNhat;

            _context.Entry(goiCuoc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoiCuocExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GoiCuoc
        [HttpPost]
        public async Task<ActionResult<GoiCuocDTO>> PostGoiCuoc(GoiCuocDTO goiCuocDTO)
        {
            var goiCuoc = new GoiCuoc
            {
                TenGoiCuoc = goiCuocDTO.TenGoiCuoc,
                MoTa = goiCuocDTO.MoTa,
                Gia = goiCuocDTO.Gia,
                ThoiHanSuDung = goiCuocDTO.ThoiHanSuDung,
                NgayTao = goiCuocDTO.NgayTao,
                NgayCapNhat = goiCuocDTO.NgayCapNhat
            };

            _context.GoiCuocs.Add(goiCuoc);
            await _context.SaveChangesAsync();

            goiCuocDTO.MaGoiCuoc = goiCuoc.MaGoiCuoc;

            return CreatedAtAction("GetGoiCuoc", new { id = goiCuocDTO.MaGoiCuoc }, goiCuocDTO);
        }

        // DELETE: api/GoiCuoc/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoiCuoc(int id)
        {
            var goiCuoc = await _context.GoiCuocs.FindAsync(id);
            if (goiCuoc == null)
            {
                return NotFound();
            }

            _context.GoiCuocs.Remove(goiCuoc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GoiCuocExists(int id)
        {
            return _context.GoiCuocs.Any(e => e.MaGoiCuoc == id);
        }
    }
}
