using Microsoft.EntityFrameworkCore;
using TruongDinhQuan_QuanLyThuVien.Models;

namespace TruongDinhQuan_QuanLyThuVien.Data
{
    public class QLTVDbcontext : DbContext
    {
        public QLTVDbcontext(DbContextOptions<QLTVDbcontext> options) : base(options)
        {

        }
        public DbSet<SACH> SACH { get; set; }
        public DbSet<NGUOIDUNG> NGUOIDUNG { get; set; }
        public DbSet<LOAI> LOAI { get; set; }
        public DbSet<MUONTRASACH> MUONTRASACH { get; set; }
    }
}
