using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TruongDinhQuan_QuanLyThuVien.Models
{
    public class MUONTRASACH
    {
        [Key]
        public int IdMuon { get; set; }
        public int IdND { get; set; }

        //[ForeignKey("IdS")]
        public int IdS { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayTra { get; set; }
        public string? Muon { get; set; }
        public string? Tra { get; set; }


        //connect to user and book table
        [ForeignKey("IdND")]
        public NGUOIDUNG NGUOIDUNG { get; set; }

        [ForeignKey("IdS")]
        public SACH SACH { get; set; }
    }
}
