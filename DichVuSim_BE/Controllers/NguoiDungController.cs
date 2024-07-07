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
    public class NguoiDungController : ControllerBase
    {
        private readonly VMchargerDBContext _context;

        public NguoiDungController(VMchargerDBContext context)
        {
            _context = context;
        }

        // GET: api/NguoiDung
        [HttpGet("getAllNguoiDung")]
        public async Task<ActionResult<IEnumerable<NguoiDungDTO>>> GetNguoiDungs()
        {
            var nguoiDungs = await _context.NguoiDungs.ToListAsync();
            return nguoiDungs.Select(nd => new NguoiDungDTO
            {
                MaNguoiDung = nd.MaNguoiDung,
                SoDienThoai = nd.SoDienThoai,
                MatKhau = nd.MatKhau,
                Email = nd.Email,
                HoTen = nd.HoTen,
                SoDu = nd.SoDu,
                VaiTro = nd.VaiTro,
                NgayTao = nd.NgayTao,
                NgayCapNhat = nd.NgayCapNhat
            }).ToList();
        }

        // GET: api/NguoiDung/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NguoiDungDTO>> GetNguoiDung(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);

            if (nguoiDung == null)
            {
                return NotFound();
            }

            var nguoiDungDTO = new NguoiDungDTO
            {
                MaNguoiDung = nguoiDung.MaNguoiDung,
                SoDienThoai = nguoiDung.SoDienThoai,
                MatKhau = nguoiDung.MatKhau,
                Email = nguoiDung.Email,
                HoTen = nguoiDung.HoTen,
                SoDu = nguoiDung.SoDu,
                VaiTro = nguoiDung.VaiTro,
                NgayTao = nguoiDung.NgayTao,
                NgayCapNhat = nguoiDung.NgayCapNhat
            };

            return nguoiDungDTO;
        }

        // PUT: api/NguoiDung/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNguoiDung(int id, NguoiDungDTO nguoiDungDTO)
        {
            if (id != nguoiDungDTO.MaNguoiDung)
            {
                return BadRequest();
            }

            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            nguoiDung.SoDienThoai = nguoiDungDTO.SoDienThoai;
            nguoiDung.MatKhau = nguoiDungDTO.MatKhau;
            nguoiDung.Email = nguoiDungDTO.Email;
            nguoiDung.HoTen = nguoiDungDTO.HoTen;
            nguoiDung.SoDu = nguoiDungDTO.SoDu;
            nguoiDung.VaiTro = nguoiDungDTO.VaiTro;
            nguoiDung.NgayTao = nguoiDungDTO.NgayTao;
            nguoiDung.NgayCapNhat = nguoiDungDTO.NgayCapNhat;

            _context.Entry(nguoiDung).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NguoiDungExists(id))
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

        // POST: api/NguoiDung
        [HttpPost]
        public async Task<ActionResult<NguoiDungDTO>> PostNguoiDung(NguoiDungDTO nguoiDungDTO)
        {
            var nguoiDung = new NguoiDung
            {
                SoDienThoai = nguoiDungDTO.SoDienThoai,
                MatKhau = nguoiDungDTO.MatKhau,
                Email = nguoiDungDTO.Email,
                HoTen = nguoiDungDTO.HoTen,
                SoDu = nguoiDungDTO.SoDu,
                VaiTro = nguoiDungDTO.VaiTro,
                NgayTao = nguoiDungDTO.NgayTao,
                NgayCapNhat = nguoiDungDTO.NgayCapNhat
            };

            _context.NguoiDungs.Add(nguoiDung);
            await _context.SaveChangesAsync();

            nguoiDungDTO.MaNguoiDung = nguoiDung.MaNguoiDung;

            return CreatedAtAction("GetNguoiDung", new { id = nguoiDungDTO.MaNguoiDung }, nguoiDungDTO);
        }

        // DELETE: api/NguoiDung/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNguoiDung(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            _context.NguoiDungs.Remove(nguoiDung);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NguoiDungExists(int id)
        {
            return _context.NguoiDungs.Any(e => e.MaNguoiDung == id);
        }
    }
}
