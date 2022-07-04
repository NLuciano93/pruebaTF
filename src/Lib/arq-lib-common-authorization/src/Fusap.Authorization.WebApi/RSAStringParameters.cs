using System;
using System.Security.Cryptography;

// ReSharper disable InconsistentNaming

namespace Fusap.Common.Authorization.WebApi
{
#pragma warning disable S101 // Types should be named in PascalCase
    public class RSAStringParameters
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public string Modulus { get; set; } = default!;

        public string Exponent { get; set; } = default!;

        public string? P { get; set; }

        public string? Q { get; set; }

        public string? DP { get; set; }

        public string? DQ { get; set; }

        public string? InverseQ { get; set; }

        public string? D { get; set; }

        public RSAParameters ToRSAParameters()
        {
            var parameters = new RSAParameters
            {
                Modulus = Convert.FromBase64String(Modulus),
                Exponent = Convert.FromBase64String(Exponent)
            };

            if (!string.IsNullOrEmpty(P))
            {
                parameters.P = Convert.FromBase64String(P);
            }
            if (!string.IsNullOrEmpty(Q))
            {
                parameters.Q = Convert.FromBase64String(Q);
            }
            if (!string.IsNullOrEmpty(DP))
            {
                parameters.DP = Convert.FromBase64String(DP);
            }
            if (!string.IsNullOrEmpty(DQ))
            {
                parameters.DQ = Convert.FromBase64String(DQ);
            }
            if (!string.IsNullOrEmpty(InverseQ))
            {
                parameters.InverseQ = Convert.FromBase64String(InverseQ);
            }
            if (!string.IsNullOrEmpty(D))
            {
                parameters.D = Convert.FromBase64String(D);
            }

            return parameters;
        }

        public static implicit operator RSAParameters(RSAStringParameters input)
        {
            return input.ToRSAParameters();
        }
    }
}
