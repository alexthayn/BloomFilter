using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter_AThayn
{
    class BloomFilter
    {
        //Max number of values in bitmap
        static int MAX = 9999999;
        bool[] _wordBitmap = new bool[MAX];

        //Initialize bloom filter with the worlist.txt file
        public BloomFilter()
        {
            Console.WriteLine("Loading dictionary bitmap with values...");
            ReadDictionary();
        }

        //Read in the dictionary and store hashes in bitmap
        private void ReadDictionary()
        {
            System.IO.StreamReader wordlist = new System.IO.StreamReader("wordlist.txt");
            String line;

            while((line = wordlist.ReadLine()) != null)
            {
                int[] hashValues = CalculateWordHash(line);
                _wordBitmap[hashValues[0]] = true;
                _wordBitmap[hashValues[1]] = true;
                _wordBitmap[hashValues[2]] = true;
                _wordBitmap[hashValues[3]] = true;
            }
        }

        //Calculate two hash values for any string given
        public int[] CalculateWordHash(String word)
        {
            word = word.ToLower();
            MD5 md5 = MD5.Create();
            SHA256 sHA256 = SHA256.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(word);
            byte[] md5Hash = md5.ComputeHash(inputBytes);
            byte[] sha256Hash = sHA256.ComputeHash(inputBytes);

            //Split md5 byte array into two segments
            byte[] firstHash = md5Hash.Take((md5Hash.Length + 1) / 2).ToArray();
            byte[] secondHash = md5Hash.Skip((md5Hash.Length + 1) / 2).ToArray();

            //Split sha256 byte array into two sub arrays
            byte[] thirdHash = sha256Hash.Take((sha256Hash.Length + 1) / 2).ToArray();
            byte[] fourthHash = sha256Hash.Skip((sha256Hash.Length + 1) / 2).ToArray();

            //Set four values to return for each string passed in
            int firstValue = Math.Abs(BitConverter.ToInt32(firstHash, 0)) % MAX;
            int secondvalue = Math.Abs(BitConverter.ToInt32(secondHash, 0)) % MAX;
            int thirdValue = Math.Abs(BitConverter.ToInt32(thirdHash, 0)) % (MAX/4);
            int fouthValue = Math.Abs(BitConverter.ToInt32(fourthHash, 0)) % (MAX/8);

            return new int[] { firstValue, secondvalue, thirdValue, fouthValue };
        }

        //Check to see if string is in the dictionary
        public bool CheckWordInDictionary(String word)
        {
            int [] hash = CalculateWordHash(word);

            if (_wordBitmap[hash[0]] == true && _wordBitmap[hash[1]] == true &&
                _wordBitmap[hash[2]] == true && _wordBitmap[3] == true)
                return true;
            return false;
        }
    }
}
