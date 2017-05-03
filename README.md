# VidyoTokenGenerator
C# .NET portable implementation of a Vidyo token generator

This project implements a token generator for Vidyo provision tokens in a portable .NET class library. It is based on the sample C# token generator provided by Vidyo (https://github.com/Vidyo/generateToken-c-sharp).

Since System.Security.Cryptography is not available in .NET portable I used PCL Crypto (https://www.nuget.org/packages/pclcrypto) for the implementation of the HMAC.
