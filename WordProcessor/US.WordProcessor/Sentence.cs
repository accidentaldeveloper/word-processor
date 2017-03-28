using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace US.WordProcessor
{
    public class Sentence
       : IEnumerable<string>
    {
        public Sentence(string source)
        {
            Source = source.Trim();
        }

        public string Source { get; }

        public IEnumerator<string> GetEnumerator()
        {
            return Source.Split(' ')
               .ToList()
               .GetEnumerator();
        }

        public override string ToString()
        {
            return Source;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}