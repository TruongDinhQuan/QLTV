using System.ComponentModel.DataAnnotations;

namespace TruongDinhQuan_QuanLyThuVien.Models
{
    public class NGUOIDUNG
    {
        [Key]
        public int IdUser { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public string? TenDaydu { get; set; }
        public string? Email { get; set; }
        public string? VaiTro { get; set; }
    }
}
