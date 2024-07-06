using System.Threading.Tasks;
using TruongDinhQuan_QuanLyThuVien.Models;

namespace TruongDinhQuan_QuanLyThuVien.Service
{
    public interface IUserService
    {
        Task AddBookLoanAsync(MUONTRASACH BorrowingRecord, string Username, int BookId);
        Task AddNewBookAsync(SACH book);
        Task AddNewUserAsync(NGUOIDUNG users);
        Task DeleteNewBookAsync(int id);
        Task DeleteNewUserAsync(int id);
        Task<SACH> GetBookByIdAsync(int id);
        Task<List<SACH>> GetBookListAsync(int? id);
        Task<List<SACH>> GetBookListAvailableAsync();
        Task<List<LOAI>> GetCategoryListAsync();
        Task<List<MUONTRASACH>> GetLoanBookListAsync(string Username);
        Task<NGUOIDUNG> GetUserByIdAsync(int id);
        Task<List<NGUOIDUNG>> GetUserListAsync();
        Task UpdateNewBookAsync(SACH book, int id);
        Task UpdateNewUserAsync(NGUOIDUNG users, int id);
        Task UpdateReturnedDateAsync(MUONTRASACH Book, int id);
        Task UpdateReturnedAsync(int id, string status);
        Task<int?> GetUserIdByNameAsync(string userName);
        Task<List<MUONTRASACH>> GetLoanBookListttAsync(int userId);
        Task<List<MUONTRASACH>> GetAllLoanRequestsAsync();
        Task RequestReturnAsync(int id);
        Task ConfirmReturnAsync(int id);
        Task<NGUOIDUNG> GetUserByUserNameAsync(string userName);
        Task<Dictionary<string, int>> GetLoanStatisticsAsync();

        Task<bool> UpdateUserByUserNameAsync(NGUOIDUNG updatedUser, string userName);

    }
}