using System;
using System.Security.Cryptography;
using System.Text;

namespace MD5Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "Hello World!";
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, source);

                if (VerifyMd5Hash(md5Hash, source, hash))
                {
                    Console.WriteLine("Los hash coinciden.");
                }
                else
                {
                    Console.WriteLine("Los hash NO coinciden.");
                }
				

            }
        }
		
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
	}
}