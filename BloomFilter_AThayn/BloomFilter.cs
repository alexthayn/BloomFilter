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
        static int MAX = 999999999;
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
                _wordBitmap[CalculateWordHash(line)] = true;
            }
        }

        //Calculate a has for any string given
        public int CalculateWordHash(String word)
        {
            word = word.ToLower();
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(word);
            byte[] hash = md5.ComputeHash(inputBytes);

            int hashInt = Math.Abs(BitConverter.ToInt32(hash, 0));
            hashInt = hashInt % MAX;
            return hashInt;
        }

        //Check to see if string is in the dictionary
        public bool CheckWordInDictionary(String word)
        {
            int hash = CalculateWordHash(word);

            if (_wordBitmap[hash] == true)
                return true;
            return false;
        }
    }
}
