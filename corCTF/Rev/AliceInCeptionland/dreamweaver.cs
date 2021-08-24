using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

 public class Dream
{        
    private static readonly string[] rm;
    private static readonly ulong xm;
    public static string Encode(string plaintext)
    {
        string text = string.Empty;
        if (!string.IsNullOrEmpty(plaintext))
        {
            string text2 = string.Join("/", plaintext.Select(new Func<char, string>(Dream.func3_3)).ToArray<string>());
            byte[] array = new byte[]
            {
                (byte)((byte)Dream.xm & 255UL),
                (byte)(Dream.xm >> 8 & 255UL),
                (byte)(Dream.xm >> 16 & 255UL),
                (byte)(Dream.xm >> 24 & 255UL),
                (byte)(Dream.xm >> 32 & 255UL),
                (byte)(Dream.xm >> 40 & 255UL),
                (byte)(Dream.xm >> 48 & 255UL),
                (byte)(Dream.xm >> 56 & 255UL)
            };
            for (int i = 0; i < text2.Length; i++)
            {
                text += string.Format("{0:x2}", (byte)text2[i] ^ array[i % array.Length]);
            }
        }
        
    
            text = string.Join<char>("", text.ToArray<char>());
        
        return text;
    }
    
    public static byte[] Decrypt(byte[] ciphertext, string secret)
    {
        byte[] iv = null;
        byte[] key = null;
        using (MD5 md = MD5.Create())
        {
            iv = md.ComputeHash(Encoding.ASCII.GetBytes(secret));
        }
        using (SHA256 sha = SHA256.Create())
        {
            key = sha.ComputeHash(Encoding.ASCII.GetBytes(secret));
        }
        byte[] result;
        using (Aes aes = Aes.Create())
        {
            aes.IV = iv;
            aes.Key = key;
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(ciphertext, 0, ciphertext.Length);
                    cryptoStream.FlushFinalBlock();
                    result = memoryStream.ToArray();
                }
            }
        }
        return result;
    }
    
    private static byte ror(byte v, byte s)
    {
        int b = (int)s % 8;
        return (byte)((int)v << (int)b | v >> (int)(8 - b));
    }

    private static byte rol(byte v, byte s)
    {
        int b = (int) s % 8;
        return (byte)(v >> (int)b | (int)v << (int)(8 - b));
    }

    static Dream()
    {
        string[] array = new string[256];
        array[4] = "\u000f";
        array[5] = "\u0005\u0006\u0005\u0005\u0006";
        array[6] = "\u001d\u001d\u001d\u001d\u001d";
        array[7] = "\u0015\u0015\u0015\u0016\u0016";
        array[8] = "nmmnmn";
        array[9] = "ffff";
        array[10] = "~}}~";
        array[11] = "uvvu";
        array[12] = "\0";
        array[13] = "FFFF";
        array[14] = "^]]^";
        array[15] = "UVVU";
        array[36] = "\f\u000f\f\u000f\f\f";
        array[37] = "\u0004\a\u0004\u0004\a\u0004";
        array[38] = "\u001f\u001c\u001c\u001c\u001c";
        array[39] = "\u0014\u0014\u0014\u0014\u0017";
        array[40] = "ol";
        array[41] = "gg";
        array[42] = "||\u007f|";
        array[43] = "twtt";
        array[44] = "OL";
        array[45] = "GG";
        array[46] = "\\\\_\\";
        array[47] = "TWTT";
        array[68] = "\0";
        array[69] = "\0";
        array[70] = "\u001c\u001c\u001f\u001f\u001f";
        array[71] = "\u0017\u0017\u0017\u0014\u0014\u0014";
        array[72] = "olll";
        array[73] = "dggg";
        array[74] = "|\u007f|";
        array[75] = "wwtt";
        array[76] = "OLLL";
        array[77] = "DGGG";
        array[78] = "\\_\\";
        array[79] = "WWTT";
        array[100] = "\0";
        array[101] = "\u0005\u0006\u0005\u0006\u0005";
        array[102] = "\u001d\u001d\u001d\u001e\u001e";
        array[103] = "\u0016\u0015\u0016\u0015\u0016\u0015";
        array[104] = "nmnm";
        array[105] = "fef";
        array[106] = "}}}";
        array[107] = "\0";
        array[108] = "NMNM";
        array[109] = "FEF";
        array[110] = "]]]";
        array[111] = "\0";
        array[132] = "\n\n\n\n\n";
        array[133] = "\u0001\u0001\u0002\u0002\u0001\u0001";
        array[134] = "\u001a\u001a\u001a\u001a\u0019";
        array[135] = "\0";
        array[136] = "ijj";
        array[137] = "babb";
        array[138] = "y";
        array[139] = "\0";
        array[140] = "IJJ";
        array[141] = "BABB";
        array[142] = "Y";
        array[143] = "\0";
        array[164] = "\0";
        array[165] = "\0\u0003\u0003\u0003\u0003\0";
        array[166] = "\u001b\u001b\u001b\u001b\u001b";
        array[167] = "\u0010\u0013\u0013\u0013\u0010";
        array[168] = "k";
        array[169] = "``";
        array[170] = "{{x";
        array[171] = "\0";
        array[172] = "K";
        array[173] = "@@";
        array[174] = "[[X";
        array[175] = "\0";
        array[196] = "\b\v\b\b\b";
        array[197] = "\0\u0003\0\u0003\0\u0003";
        array[198] = "\u001b\u0018\u0018\u0018\u0018";
        array[199] = "\0";
        array[200] = "hhkh";
        array[201] = "c`";
        array[202] = "xxx{";
        array[203] = "\0";
        array[204] = "HHKH";
        array[205] = "C@";
        array[206] = "XXX[";
        array[207] = "\0";
        array[228] = "\t\n\n\n\n\t";
        array[229] = "\u0002\u0001\u0001\u0002\u0001";
        array[230] = "\u001a\u001a\u0019\u0019\u0019";
        array[231] = "\u0011\u0011\u0012\u0012\u0011\u0011";
        array[232] = "jji";
        array[233] = "bbb";
        array[234] = "yzz";
        array[235] = "qqrrqr";
        array[236] = "JJI";
        array[237] = "BBB";
        array[238] = "YZZ";
        Dream.rm = array;
        Dream.xm = 1056017893861212352UL;
    }
    
    internal static char rol_l(char c)
    {
        return (char)Dream.rol((byte)c, 3);
    }

    internal static char rol_r(char c)
    {
        return (char)Dream.ror((byte)c, 5);
    }

    internal static string xor(char c, string x)
    {
        return new string((from y in x
        select (char)((byte)y ^ (byte)c)).ToArray<char>());
    }
    
        internal static string func3_3(char c)
    {
        return string.Join("", new string[]
        {
            Dream.xor(c, new string(Dream.rm[(int)Dream.rol_l(c)].Select(new Func<char, char>(Dream.rol_r)).ToArray<char>()))
        });
    }
}

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string finalEncoding = "3c3cf1df89fe832aefcc22fc82017cd57bef01df54235e21414122d78a9d88cfef3cf10c829ee32ae4ef01dfa1951cd51b7b22fc82433ef7ef418cdf8a9d802101ef64f9a495268fef18d52882324f217b1bd64b82017cd57bef01df255288f7593922712c958029e7efccdf081f8808a6efd5287595f821482822f6cb95f821cceff4695495268fefe72ad7821a67ae0060ad";
            string guess = ""; // our initial guess is nothing
            do
            {
                for (int i = 0x20; i < 0x7f; i++) // only bruteforce printable ascii characters
                {
                    string encoded = Dream.Encode((guess + (char)i)); // encoded string for guess
                    if (finalEncoding.StartsWith(encoded)) // if it's a match, we have the right character
                    {
                        guess += (char)i; // add the character to the successes to build string
                        break; // break from loop for optimization
                    }
                }
                Console.WriteLine("Guess: " + guess); // print guess
            } while (!Dream.Encode(guess).Equals(finalEncoding)); // keep going until the entire string is found
            Console.WriteLine("Final: " + guess); // print the solution
        }
    }
}