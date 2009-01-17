using System;

namespace Shelf.Support.Searching
{
    public class NoStemming : IStemming
    {
        public string StemWord(string word)
        {
            return word;
        }
    }
}