using Microsoft.EntityFrameworkCore;
using YouthProtection.Models;
using YouthProtectionApi.DataBase;
using YouthProtectionApi.Models;

namespace YouthProtectionApi.Repositories
{
    public interface IAttendanceRepository
    {
        Task<AttendanceModel> AddAttendance(AttendanceModel attendance);
        Task<AttendanceModel> GetAttendanceById(long id);
        Task UpdateAttendance(AttendanceModel attendance);

    }

    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly DataContext _context;

        public AttendanceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AttendanceModel> AddAttendance(AttendanceModel attendance)
        {
            _context.TB_ATTENDANCES.Add(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<AttendanceModel> GetAttendanceById(long id)
        {
            return await _context.TB_ATTENDANCES.FindAsync(id);
        }

        public async Task UpdateAttendance(AttendanceModel attendance)
        {
            _context.TB_ATTENDANCES.Update(attendance);
            await _context.SaveChangesAsync();
        }


    }
}
