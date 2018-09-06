using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter_AThayn
{
    class Program
    {
        static void Main(string[] args)
        {
            BloomFilter bloomFilter = new BloomFilter();
            String userInput;
            String continueProgram;

            do
            {
                System.Console.WriteLine("Enter a word to check: ");
                userInput = Console.ReadLine();
                if (bloomFilter.CheckWordInDictionary(userInput) == true)
                    Console.WriteLine(userInput + " is contained in the loaded word list.");
                else
                    Console.WriteLine(userInput + " is not contained in the loaded word list.");

                Console.WriteLine("Would you like to check another word? (Y/N)");
                continueProgram = Console.ReadLine();
            } while (continueProgram.ToLower().StartsWith("y"));

        }
    }
}
