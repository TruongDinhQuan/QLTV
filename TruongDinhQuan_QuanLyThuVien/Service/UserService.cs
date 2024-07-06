using Microsoft.EntityFrameworkCore;
using TruongDinhQuan_QuanLyThuVien.Data;
using TruongDinhQuan_QuanLyThuVien.Models;

namespace TruongDinhQuan_QuanLyThuVien.Service
{
    public class UserService : IUserService
    {
        private readonly QLTVDbcontext _dbContext;


        public UserService(QLTVDbcontext dbContext)
        {
            _dbContext = dbContext;
        }

        //Lấy danh sách sách
        public async Task<List<SACH>> GetBookListAsync(int? id)
        {
            if (id == 0)
            {
                var result = await _dbContext.SACH
                    .OrderByDescending(book => book.IDS)
                    .ToListAsync();
                return result;
            }
            else
            {
                var result = await _dbContext.SACH
                    .Where(book => book.IDS == id)
                    .OrderByDescending(book => book.IDS)
                    .ToListAsync();
                return result;
            }
        }

        //Tạo sách mới
        public async Task AddNewBookAsync(SACH book)
        {
            _dbContext.SACH.Add(book);
            await _dbContext.SaveChangesAsync();
        }

        //cập nhật thông tin sách
        public async Task UpdateNewBookAsync(SACH book, int id)
        {
            var resultUpdate = await _dbContext.SACH.FindAsync(id);
            if (resultUpdate != null)
            {
                resultUpdate.TenSach = book.TenSach;
                resultUpdate.TacGia = book.TacGia;
                resultUpdate.NXB = book.NXB;
                resultUpdate.NgayXuatBan = book.NgayXuatBan;
                resultUpdate.LoaiID = book.LoaiID;
                resultUpdate.MoTa = book.MoTa;
                resultUpdate.CoSan = book.CoSan;
                resultUpdate.HinhAnh = book.HinhAnh;  // Ensure this line is updating the Image
                await _dbContext.SaveChangesAsync();
            }
        }


        //Xóa sách
        public async Task DeleteNewBookAsync(int id)
        {
            var book = await _dbContext.SACH.FindAsync(id);
            if (book != null)
            {
                // Remove the book
                _dbContext.SACH.Remove(book);

                // Save changes to the database (including cascading delete)
                await _dbContext.SaveChangesAsync();
            }
        }

        //lấy sách bằng ID
        public async Task<SACH> GetBookByIdAsync(int id)
        {
            var bookbyid = await _dbContext.SACH.FindAsync(id);
            return bookbyid;
        }


        //Lấy danh sách loại sách
        public async Task<List<LOAI>> GetCategoryListAsync()
        {
            var result = await _dbContext.LOAI
                .OrderByDescending(Category => Category.LoaiID).ToListAsync();
            return result;
        }

        //Lấy danh sách người dùng
        public async Task<List<NGUOIDUNG>> GetUserListAsync()
        {
            var result = await _dbContext.NGUOIDUNG
                .OrderByDescending(Users => Users.IdUser).ToListAsync();
            return result;
        }


        //Tạo người dùng

        public async Task AddNewUserAsync(NGUOIDUNG users)
        {
            var existingUser = await _dbContext.NGUOIDUNG.FirstOrDefaultAsync(u => u.TenDangNhap == users.TenDangNhap);

            if (existingUser != null)
            {
            }
            else
            {
                _dbContext.NGUOIDUNG.Add(users);
                await _dbContext.SaveChangesAsync();
            }
        }

        //Cập nhật thông tin người dùng
        public async Task UpdateNewUserAsync(NGUOIDUNG users, int id)
        {
            var result_update = await _dbContext.NGUOIDUNG.FindAsync(id);
            if (result_update != null)
            {

                await _dbContext.SaveChangesAsync();

            }
        }

        public async Task<bool> UpdateUserByUserNameAsync(NGUOIDUNG updatedUser, string userName)
        {
            var existingUser = await _dbContext.NGUOIDUNG
                                             .FirstOrDefaultAsync(u => u.TenDangNhap == userName);
            if (existingUser != null)
            {
                existingUser.TenDangNhap = updatedUser.TenDangNhap;
                existingUser.MatKhau = updatedUser.MatKhau;
                existingUser.TenDaydu = updatedUser.TenDaydu;
                existingUser.Email = updatedUser.Email;
                // Cập nhật các thuộc tính khác nếu cần

                await _dbContext.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
                return true;
            }
            return false;
        }


        //Xóa người dùng
        public async Task DeleteNewUserAsync(int id)
        {
            var user = await _dbContext.NGUOIDUNG.FindAsync(id);

            if (user == null)
            {
                throw new Exception("Người dùng không tồn tại");
            }

            bool hasBorrowedBooks = await _dbContext.MUONTRASACH.AnyAsync(mts => mts.IdND == id);

            if (hasBorrowedBooks)
            {
                throw new InvalidOperationException("Người dùng này đã mượn sách và không thể xóa.");
            }
            else
            {
                _dbContext.NGUOIDUNG.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }


        //Lấy người dùng bằng ID
        public async Task<NGUOIDUNG> GetUserByIdAsync(int id)
        {
            var bookbyid = await _dbContext.NGUOIDUNG.FindAsync(id);
            return bookbyid;
        }


        //lấy thông tin người dùng bằng tên
        public async Task<NGUOIDUNG> GetUserByUserNameAsync(string userName)
        {
            return await _dbContext.NGUOIDUNG
                .FirstOrDefaultAsync(u => u.TenDangNhap == userName);
        }


        //Hiển thị sách bằng thuộc tính CoSan
        public async Task<List<SACH>> GetBookListAvailableAsync()
        {
            var result = await _dbContext.SACH
                .Where(Book => Book.CoSan == true)
                .OrderByDescending(Book => Book.IDS)
                .ToListAsync();
            return result;
        }


        //Mượn sách
        public async Task AddBookLoanAsync(MUONTRASACH BorrowingRecord, String Username, int BookId)
        {
            var getinfoUser = await _dbContext.NGUOIDUNG.FirstOrDefaultAsync(u => u.TenDangNhap == Username);
            var getinfoBook = await _dbContext.SACH.FirstOrDefaultAsync(b => b.IDS == BookId);
            // Set BorrowedDate to the current date
            BorrowingRecord.NgayMuon = DateTime.Today;

            var newRecord = new MUONTRASACH
            {
                IdND = getinfoUser.IdUser,
                IdS = getinfoBook.IDS,
                NgayMuon = BorrowingRecord.NgayMuon,
                NgayTra = BorrowingRecord.NgayTra,
                Muon = "mượn sách",
            };

            _dbContext.MUONTRASACH.Add(newRecord);
            await _dbContext.SaveChangesAsync();
        }

        //Lấy danh sách mượn sách
        public async Task<List<MUONTRASACH>> GetLoanBookListAsync(String Username)
        {
            if (Username == "admin")
            {
                var result = await _dbContext.MUONTRASACH
                .Where(record => record.Muon == "mượn sách")
                .Include(record => record.SACH)
                .Include(record => record.NGUOIDUNG)
                .OrderByDescending(record => record.IdMuon)
                .ToListAsync();
                return result;
            }
            else
            {
                var getinfoUser = await _dbContext.NGUOIDUNG.FirstOrDefaultAsync(u => u.TenDangNhap == Username);
                var result = await _dbContext.MUONTRASACH
                .Where(record => record.IdND == getinfoUser.IdUser && record.Muon == "mượn sách")
                .OrderByDescending(record => record.IdMuon)
                .Include(record => record.SACH)
                .Include(record => record.NGUOIDUNG)
                .ToListAsync();
                return result;
            }
        }

        //Xác nhận cho mượn sách
        public async Task UpdateReturnedDateAsync(MUONTRASACH Book, int id)
        {
            var result_update = await _dbContext.MUONTRASACH.FindAsync(id);
            if (result_update != null)
            {
                result_update.Muon = "Kết thúc";
                await _dbContext.SaveChangesAsync();
            }
        }

        //trả sách//
        public async Task UpdateReturnedAsync(int id, string status)
        {
            var result_update = await _dbContext.MUONTRASACH.FindAsync(id);
            if (result_update != null)
            {
                result_update.Tra = status;
                await _dbContext.SaveChangesAsync();
            }
        }

        //Xác nhận đã trả sách của Admin
                public async Task<List<MUONTRASACH>> GetLoanBookListttAsync(int userId)
                {
                    return await _dbContext.MUONTRASACH
                        .Include(m => m.SACH) // Include the related SACH entity
                        .Where(m => m.Muon == "Kết thúc" && (m.Tra == null || m.Tra != "Đã trả") && m.IdND == userId)
                        .ToListAsync();
                }

        public async Task<List<MUONTRASACH>> GetAllLoanRequestsAsync()
        {
            return await _dbContext.MUONTRASACH
                .Include(m => m.SACH)
                .Include(m => m.NGUOIDUNG)
                .Where(m => m.Tra == "Đang chờ xác nhận")
                .ToListAsync();
        }


        public async Task<int?> GetUserIdByNameAsync(string userName)
        {
            var user = await _dbContext.NGUOIDUNG
                                     .Where(u => u.TenDangNhap == userName)
                                     .FirstOrDefaultAsync();
            return user?.IdUser;
        }
        public async Task RequestReturnAsync(int id)
        {
            var result_update = await _dbContext.MUONTRASACH.FindAsync(id);
            if (result_update != null)
            {
                result_update.Tra = "Đang chờ xác nhận";
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task ConfirmReturnAsync(int id)
        {
            var result_update = await _dbContext.MUONTRASACH.FindAsync(id);
            if (result_update != null)
            {
                result_update.Tra = "Đã trả";
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Dictionary<string, int>> GetLoanStatisticsAsync()
        {
            var loans = await _dbContext.MUONTRASACH
                .Include(m => m.SACH)
                .Include(m => m.NGUOIDUNG)
                .ToListAsync();

            var statistics = loans
                .GroupBy(l => l.NGUOIDUNG.TenDangNhap)  // Nhóm theo tên người dùng
                .ToDictionary(g => g.Key, g => g.Count());  // Đếm số lần mỗi người dùng mượn sách

            return statistics;
        }
    }
}
