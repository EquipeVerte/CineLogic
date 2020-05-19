using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Models.Hashing
{
    public class Hash
    {
        public byte[] Salt { get; set; }
        public int Iterations { get; set; }
        public byte[] HashedString { get; set; }

        public Hash()
        {
        }

        public Hash(byte[] salt, int iterations, byte[] hashedString)
        {
            Salt = salt;
            Iterations = iterations;
            HashedString = hashedString;
        }
    }
}