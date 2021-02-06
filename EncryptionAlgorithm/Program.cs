using System;
using System.Security.Cryptography;
using System.Text;
using CommandLine;
using System.IO;

namespace EncryptionAlgorithm
{
    class Program
    {
        public class Options
        {
            [Value(0, HelpText = "The input file to operate on.", Required = true)]
            public string Input { get; set; }

            [Option('e', "encrypt", Group = "mode")]
            public bool Encrypt { get; set; }

            [Option('d', "decrypt", Group = "mode")]
            public bool Decrypt { get; set; }

            [Option('k', "key", Required = true)]
            public string Key { get; set; }

            [Option('o', "out", HelpText = "Output file. If not specified output to stdout")]
            public string Out { get; set; }
        }

        static byte[] sbox = { 2, 15, 6, 13, 0, 1, 7, 14, 8, 3, 5, 10, 4, 11, 9, 12}; //maps 4 bit values to different 4 bit values
        static byte[] pbox = { 7, 2, 3, 0, 6, 1, 4, 5 }; // maps 8 bit positions to 8 different bit positions

        static MD5 md5 = MD5.Create();

        static void Main(string[] args) {
            Options options = new Options();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(parsed => options = parsed)
                .WithNotParsed(error => System.Environment.Exit(1));

            byte[] key = ExpandKey(options.Key);
            byte[] inputData, outputData;

            try {
                string filepath = Path.GetFullPath(options.Input);
                inputData = System.IO.File.ReadAllBytes(filepath);
            } catch (Exception e) {
                Console.WriteLine("Unable to read file {0}\n{1}", options.Input, e.Message);
                return;
            }

            outputData = options.Decrypt ? Decrypt(inputData, key) : Encrypt(inputData, key);

            if(options.Out != null) {
                try {
                    string filepath = Path.GetFullPath(options.Out);
                    System.IO.File.WriteAllBytes(filepath, outputData);
                } catch (Exception e) {
                    Console.WriteLine("Failed to write to file {0}\n{1}", options.Out, e.Message);
                    return;
                }
            } else {
                Console.WriteLine("Output:\n{0}", Encoding.ASCII.GetString(outputData));
            }
        }

        static byte[] ExpandKey(string key) {
            return md5.ComputeHash(Encoding.ASCII.GetBytes(key));
        }

        static byte[] Encrypt(byte[] data, byte[] key) {
            foreach(byte subkey in key) {
                for(int i = 0; i < data.Length; i++) {
                    byte xored = (byte)(data[i] ^ subkey);
                    data[i] = DoRound(xored);
                }
            }
            return data;
        }

        static byte[] Decrypt(byte[] data, byte[] key) {
            byte[] reversedKey = (byte[])key.Clone();
            Array.Reverse(reversedKey);
            foreach(byte subkey in reversedKey) {
                for(int i = 0; i < data.Length; i++) {
                    byte roundData = ReverseRound(data[i]);
                    data[i] = (byte)(roundData ^ subkey);
                }
            }
            return data;
        }

        static byte DoRound(byte data) {
            byte sdata1 = sbox[(byte)(data>>4 & 0xF)];
            byte sdata2 = sbox[(byte)(data & 0xF)];
            byte pdata = (byte)((sdata1 << 4) | sdata2);
            byte result = 0;
            for(int i = 0; i < 8; i++) {
                result |= (byte)((pdata >> i & 1) << pbox[i]);
            }
            return result;
        }

        static byte ReverseRound(byte data) {
            byte pdata = 0;
            for(int i = 0; i < 8; i++) {
                pdata |= (byte)((data >> pbox[i] & 1) << i);
            }
            // it'd probably be faster to have the reverse lookup table precooked instead of finding the index for every entry.
            int sdata1 = Array.IndexOf(sbox, (byte)(pdata >> 4 & 0xF));
            int sdata2 = Array.IndexOf(sbox, (byte)(pdata & 0xF));
            return (byte)((sdata1 << 4) | sdata2);
        }
    }
}
