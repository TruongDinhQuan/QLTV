using System.ComponentModel.DataAnnotations;

namespace TruongDinhQuan_QuanLyThuVien.Models
{
    public class LOAI
    {
        [Key]
        public int LoaiID { get; set; }
        public string? TenLoai { get; set; }
    }
}
