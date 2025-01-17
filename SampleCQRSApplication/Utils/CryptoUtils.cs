﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Utils
{
    public interface ISHAUtils
    {
        string GetHash(string input);
        bool VerifyHash(string input, string hash);
    }

    public class SHAUtils : ISHAUtils
    {
        public string GetHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                var sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        // Verify a hash against a string.
        public bool VerifyHash(string input, string hash)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Hash the input.
                var hashOfInput = GetHash(input);

                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                return comparer.Compare(hashOfInput, hash) == 0;
            }
        }
    }
}
