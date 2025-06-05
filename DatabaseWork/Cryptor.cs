using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork
{
    public static class Cryptor
    {
        public static string HashPasswordSHA512(string password)
        {
            byte[] b_password = Encoding.UTF8.GetBytes(password);
            using var cryptor = SHA512.Create();

            byte[] crypted_password = cryptor.ComputeHash(b_password);

            return Convert.ToBase64String(crypted_password);
        }
    }
}
