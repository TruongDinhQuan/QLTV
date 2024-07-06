using System.ComponentModel.DataAnnotations;

namespace TruongDinhQuan_QuanLyThuVien.Models
{
    public class SACH
    {
        [Key]
        public int IDS { get; set; }
        public string? TenSach { get; set; } = string.Empty;
        public string? TacGia { get; set; } = string.Empty;
        public string? NXB { get; set; } = string.Empty;
        public DateTime NgayXuatBan { get; set; }
        public int? LoaiID { get; set; }
        public string? MoTa { get; set; } = string.Empty;
        public string? HinhAnh { get; set; } = string.Empty;
        public bool CoSan { get; set; }

    }
}
