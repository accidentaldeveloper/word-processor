using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace US.WordProcessor.Tests
{
    [TestClass]
    public class CorrectionsTests
    {
        [TestMethod]
        public void ProperNounsSuffixedWithSNeedAnAposBeforeTheSCorrect()
        {
            var p = new Paragraph("Susan owns a hat. It is Susan's hat.");
            var c = CorrectionFactory.CreateCorrectionFinder()
               .Find(p)
               .ToList();

            Assert.AreEqual(0, c.Count);
        }

        [TestMethod]
        public void ProperNounsSuffixedWithSNeedAnAposBeforeTheSIncorrect()
        {
            var p = new Paragraph("Susan owns a hat. It is Susans hat.");
            var c = CorrectionFactory.CreateCorrectionFinder()
               .Find(p)
               .Single();

            Assert.AreEqual(CorrectionType.OwnershipByAProperNoun, c.Type);
            Assert.AreEqual("It is Susans hat", c.Sentence);
            Assert.AreEqual("Susans", c.Word);
        }

        [TestMethod]
        public void ProperNounsPrecededByIsNeedAnAposBeforeTheSCorrect()
        {
            var p = new Paragraph("Barry owns a car. The car is Barry's.");
            var c = CorrectionFactory.CreateCorrectionFinder()
               .Find(p)
               .ToList();

            Assert.AreEqual(0, c.Count);
        }

        [TestMethod]
        public void ProperNounsPrecededByIsNeedAnAposBeforeTheSIncorrect()
        {
            var p = new Paragraph("Barry owns a car. The car is Barrys.");
            var c = CorrectionFactory.CreateCorrectionFinder()
               .Find(p)
               .Single();

            Assert.AreEqual(CorrectionType.OwnershipByAProperNoun, c.Type);
            Assert.AreEqual("The car is Barrys", c.Sentence);
            Assert.AreEqual("Barrys", c.Word);
        }

        [TestMethod]
        public void ContractionsNeedAnApostropheCorrect()
        {
            var p = new Paragraph("Barry doesn't own a car. The car isn't Barry's.");
            var c = CorrectionFactory.CreateCorrectionFinder()
               .Find(p)
               .ToList();

            Assert.AreEqual(0, c.Count);
        }

        [TestMethod]
        public void ContractionsNeedAnApostropheIncorrect()
        {
            var p = new Paragraph("Barry doesnt own a car. The car isnt Barry's.");
            var c = CorrectionFactory.CreateCorrectionFinder()
               .Find(p)
               .ToList();

            var doesntCorrection = c[0];
            Assert.AreEqual(CorrectionType.MissingContractionApostrophe, doesntCorrection.Type);
            Assert.AreEqual("Barry doesnt own a car", doesntCorrection.Sentence);
            Assert.AreEqual("doesnt", doesntCorrection.Word);

            var isntCorrection = c[1];
            Assert.AreEqual(CorrectionType.MissingContractionApostrophe, isntCorrection.Type);
            Assert.AreEqual("The car isnt Barry's", isntCorrection.Sentence);
            Assert.AreEqual("isnt", isntCorrection.Word);
        }

        [TestMethod]
        public void RegularNounsDoNotNeedAnApostropheCorrect()
        {
            var p = new Paragraph("Look at those airplanes over there.");
            var c = CorrectionFactory.CreateCorrectionFinder()
               .Find(p)
               .ToList();

            Assert.AreEqual(0, c.Count);
        }

        [TestMethod]
        public void RegularNounsDoNotNeedAnApostropheIncorrect()
        {
            var p = new Paragraph("Look at those airplane's over there.");
            var c = CorrectionFactory.CreateCorrectionFinder()
               .Find(p)
               .Single();

            Assert.AreEqual(CorrectionType.IncorrectNounApostrophe, c.Type);
            Assert.AreEqual("Look at those airplane's over there", c.Sentence);
            Assert.AreEqual("airplane's", c.Word);
        }
    }
}
