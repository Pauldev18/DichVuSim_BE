using System;
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
    public class DichVuNguoiDungController : ControllerBase
    {
        private readonly VMchargerDBContext _context;

        public DichVuNguoiDungController(VMchargerDBContext context)
        {
            _context = context;
        }

        // GET: api/DichVuNguoiDung
        [HttpGet("getAllDichVuNguoiDung")]
        public async Task<ActionResult<IEnumerable<DichVuNguoiDungDTO>>> GetDichVuNguoiDungs()
        {
            var dichVuNguoiDungs = await _context.DichVuNguoiDungs.ToListAsync();
            return dichVuNguoiDungs.Select(dv => new DichVuNguoiDungDTO
            {
                MaDichVu = dv.MaDichVu,
                MaNguoiDung = dv.MaNguoiDung,
                LoaiDichVu = dv.LoaiDichVu,
                TrangThaiDichVu = dv.TrangThaiDichVu,
                NgayTao = dv.NgayTao,
                NgayCapNhat = dv.NgayCapNhat
            }).ToList();
        }

        // GET: api/DichVuNguoiDung/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DichVuNguoiDungDTO>> GetDichVuNguoiDung(int id)
        {
            var dichVuNguoiDung = await _context.DichVuNguoiDungs.FindAsync(id);

            if (dichVuNguoiDung == null)
            {
                return NotFound();
            }

            var dichVuNguoiDungDTO = new DichVuNguoiDungDTO
            {
                MaDichVu = dichVuNguoiDung.MaDichVu,
                MaNguoiDung = dichVuNguoiDung.MaNguoiDung,
                LoaiDichVu = dichVuNguoiDung.LoaiDichVu,
                TrangThaiDichVu = dichVuNguoiDung.TrangThaiDichVu,
                NgayTao = dichVuNguoiDung.NgayTao,
                NgayCapNhat = dichVuNguoiDung.NgayCapNhat
            };

            return dichVuNguoiDungDTO;
        }

        // PUT: api/DichVuNguoiDung/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDichVuNguoiDung(int id, DichVuNguoiDungDTO dichVuNguoiDungDTO)
        {
            if (id != dichVuNguoiDungDTO.MaDichVu)
            {
                return BadRequest();
            }

            var dichVuNguoiDung = await _context.DichVuNguoiDungs.FindAsync(id);
            if (dichVuNguoiDung == null)
            {
                return NotFound();
            }

            dichVuNguoiDung.MaNguoiDung = dichVuNguoiDungDTO.MaNguoiDung;
            dichVuNguoiDung.LoaiDichVu = dichVuNguoiDungDTO.LoaiDichVu;
            dichVuNguoiDung.TrangThaiDichVu = dichVuNguoiDungDTO.TrangThaiDichVu;
            dichVuNguoiDung.NgayTao = dichVuNguoiDungDTO.NgayTao;
            dichVuNguoiDung.NgayCapNhat = dichVuNguoiDungDTO.NgayCapNhat;

            _context.Entry(dichVuNguoiDung).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DichVuNguoiDungExists(id))
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

        // POST: api/DichVuNguoiDung
        [HttpPost]
        public async Task<ActionResult<DichVuNguoiDungDTO>> PostDichVuNguoiDung(DichVuNguoiDungDTO dichVuNguoiDungDTO)
        {
            var dichVuNguoiDung = new DichVuNguoiDung
            {
                MaNguoiDung = dichVuNguoiDungDTO.MaNguoiDung,
                LoaiDichVu = dichVuNguoiDungDTO.LoaiDichVu,
                TrangThaiDichVu = dichVuNguoiDungDTO.TrangThaiDichVu,
                NgayTao = dichVuNguoiDungDTO.NgayTao,
                NgayCapNhat = dichVuNguoiDungDTO.NgayCapNhat
            };

            _context.DichVuNguoiDungs.Add(dichVuNguoiDung);
            await _context.SaveChangesAsync();

            dichVuNguoiDungDTO.MaDichVu = dichVuNguoiDung.MaDichVu;

            return CreatedAtAction("GetDichVuNguoiDung", new { id = dichVuNguoiDungDTO.MaDichVu }, dichVuNguoiDungDTO);
        }

        // DELETE: api/DichVuNguoiDung/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDichVuNguoiDung(int id)
        {
            var dichVuNguoiDung = await _context.DichVuNguoiDungs.FindAsync(id);
            if (dichVuNguoiDung == null)
            {
                return NotFound();
            }

            _context.DichVuNguoiDungs.Remove(dichVuNguoiDung);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DichVuNguoiDungExists(int id)
        {
            return _context.DichVuNguoiDungs.Any(e => e.MaDichVu == id);
        }
    }
}
