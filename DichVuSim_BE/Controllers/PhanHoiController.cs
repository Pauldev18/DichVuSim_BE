using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DichVuSim_BE.Models;
using DichVuSim_BE.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace DichVuSim_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanHoiController : ControllerBase
    {
        private readonly VMchargerDBContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public PhanHoiController(VMchargerDBContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/PhanHoi
        [HttpGet("getAllPhanHoi")]
        public async Task<ActionResult<IEnumerable<PhanHoiDTO>>> GetPhanHois()
        {
            var phanHois = await _context.PhanHois.ToListAsync();
            return phanHois.Select(ph => new PhanHoiDTO
            {
                MaPhanHoi = ph.MaPhanHoi,
                SenderId = ph.SenderId,
                ReceiverId = ph.ReceiverId,
                NoiDungPhanHoi = ph.NoiDungPhanHoi,
                NgayTao = ph.NgayTao,
                NgayCapNhat = ph.NgayCapNhat
            }).ToList();
        }

        // GET: api/PhanHoi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhanHoiDTO>> GetPhanHoi(int id)
        {
            var ph = await _context.PhanHois.FindAsync(id);

            if (ph == null)
            {
                return NotFound();
            }

            var phanHoiDTO = new PhanHoiDTO
            {
                MaPhanHoi = ph.MaPhanHoi,
                SenderId = ph.SenderId,
                ReceiverId = ph.ReceiverId,
                NoiDungPhanHoi = ph.NoiDungPhanHoi,
                NgayTao = ph.NgayTao,
                NgayCapNhat = ph.NgayCapNhat
            };

            return phanHoiDTO;
        }

        // GET: api/PhanHoi/chat/{userId1}/{userId2}
        [HttpGet("chat/{userId1}/{userId2}")]
        public async Task<ActionResult<IEnumerable<PhanHoiDTO>>> GetChatHistory(int userId1, int userId2)
        {
            var phanHois = await _context.PhanHois
                .Where(ph => (ph.SenderId == userId1 && ph.ReceiverId == userId2) || (ph.SenderId == userId2 && ph.ReceiverId == userId1))
                .OrderBy(ph => ph.NgayTao)
                .ToListAsync();

            var userIds = phanHois.Select(ph => ph.SenderId).Union(phanHois.Select(ph => ph.ReceiverId)).Distinct().ToList();
            var users = await _context.NguoiDungs
                .Where(u => userIds.Contains(u.MaNguoiDung))
                .ToDictionaryAsync(u => u.MaNguoiDung, u => u.HoTen);

            return phanHois.Select(ph => new PhanHoiDTO
            {
                MaPhanHoi = ph.MaPhanHoi,
                SenderId = ph.SenderId,
                ReceiverId = ph.ReceiverId,
                SenderName = users[ph.SenderId], // Gán tên người gửi
                ReceiverName = users[ph.ReceiverId], // Gán tên người nhận
                NoiDungPhanHoi = ph.NoiDungPhanHoi,
                NgayTao = ph.NgayTao,
                NgayCapNhat = ph.NgayCapNhat
            }).ToList();
        }

        // PUT: api/PhanHoi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhanHoi(int id, PhanHoiDTO phanHoiDTO)
        {
            if (id != phanHoiDTO.MaPhanHoi)
            {
                return BadRequest();
            }

            var phanHoi = await _context.PhanHois.FindAsync(id);
            if (phanHoi == null)
            {
                return NotFound();
            }

            phanHoi.SenderId = phanHoiDTO.SenderId;
            phanHoi.ReceiverId = phanHoiDTO.ReceiverId;
            phanHoi.NoiDungPhanHoi = phanHoiDTO.NoiDungPhanHoi;
            phanHoi.NgayTao = phanHoiDTO.NgayTao;
            phanHoi.NgayCapNhat = phanHoiDTO.NgayCapNhat;

            _context.Entry(phanHoi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", phanHoiDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhanHoiExists(id))
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
        [HttpPost]
        public async Task<ActionResult<PhanHoiDTO>> PostPhanHoi(PhanHoiDTO phanHoiDTO)
        {
            var phanHoi = new PhanHoi
            {
                SenderId = phanHoiDTO.SenderId,
                ReceiverId = phanHoiDTO.ReceiverId,
                NoiDungPhanHoi = phanHoiDTO.NoiDungPhanHoi,
                NgayTao = phanHoiDTO.NgayTao,
                NgayCapNhat = phanHoiDTO.NgayCapNhat
            };

            _context.PhanHois.Add(phanHoi);
            await _context.SaveChangesAsync();
            phanHoiDTO.MaPhanHoi = phanHoi.MaPhanHoi;

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", phanHoiDTO);

            return CreatedAtAction("GetPhanHoi", new { id = phanHoiDTO.MaPhanHoi }, phanHoiDTO);
        }

        // DELETE: api/PhanHoi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhanHoi(int id)
        {
            var phanHoi = await _context.PhanHois.FindAsync(id);
            if (phanHoi == null)
            {
                return NotFound();
            }

            _context.PhanHois.Remove(phanHoi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhanHoiExists(int id)
        {
            return _context.PhanHois.Any(e => e.MaPhanHoi == id);
        }
    }
}