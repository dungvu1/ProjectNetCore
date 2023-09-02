using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace DOAN.Models
{
    public  class MD5WEB
    {
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0;i< bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public static bool IsValidPhoneNumber(int phoneNumber)
        {
            string phoneNumberString = phoneNumber.ToString();

            // Định dạng chuỗi biểu thức chính quy
            string pattern = @"^0\d{9}$";

            // Kiểm tra chuỗi số điện thoại với biểu thức chính quy
            return Regex.IsMatch(phoneNumberString, pattern);
        }
         public static bool IsGmail(string email)
        {
            // Kiểm tra xem địa chỉ email có đuôi là "@gmail.com" hay không
            return email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
        }
    }
}
