using System;
using System.Collections.Generic;

namespace US.WordProcessor.Internal
{
   internal class Dictionary
   {
      private static readonly Dictionary<string, Definition> KnownWords
         = new Dictionary<string, Definition>(StringComparer.CurrentCultureIgnoreCase)
            {
               // pronouns

               {"susan", new Definition(WordType.ProperNoun, "Susan", "")},
               {"susans", new Definition(WordType.ProperNoun, "Susan", "s")},
               {"susan's", new Definition(WordType.ProperNoun, "Susan", "s")},
               {"susans'", new Definition(WordType.ProperNoun, "Susan", "s")},

               {"barry", new Definition(WordType.ProperNoun, "Barry", "")},
               {"barrys", new Definition(WordType.ProperNoun, "Barry", "s")},
               {"barry's", new Definition(WordType.ProperNoun, "Barry", "s")},
               {"barrys'", new Definition(WordType.ProperNoun, "Barry", "s")},

               // nouns

               {"hat", new Definition(WordType.Noun, "Hat", "")},
               {"airplane", new Definition(WordType.Noun, "Airplane", "")},
               {"airplanes", new Definition(WordType.Noun, "Airplane", "s")},
               {"airplane's", new Definition(WordType.Noun, "Airplane", "s")},
               {"airplanes'", new Definition(WordType.Noun, "Airplane", "s")},

               // contractions
               {"isnt", new Definition(WordType.Contraction, "isn't", "")},
               {"isn't'", new Definition(WordType.Contraction, "isn't", "")},
               {"wont", new Definition(WordType.Contraction, "won't", "")},
               {"won't", new Definition(WordType.Contraction, "won't", "")},
               {"doesn't", new Definition(WordType.Contraction, "doesn't", "")},
               {"doesnt", new Definition(WordType.Contraction, "doesn't", "")},
            };
      
      public Definition Define(string word)
      {
         return KnownWords.TryGetValue(word, out var definition)
            ? definition
            : new Definition(WordType.NotAvailable, word, "");
      }      
   }
}