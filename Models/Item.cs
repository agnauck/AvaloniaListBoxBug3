using System;
using System.Text;

namespace AvaloniaListBoxBug.Models
{
    using ReactiveUI;

    public class EvenItem : Item
    {
        public EvenItem(int id) : base(id){}
    }
    
    public class UnEvenItem : Item
    {
        public UnEvenItem(int id) : base(id){}
    }

    public class Item : ReactiveObject
    {
        public Item(int id)
        {
            _id = id;
            Message = GenerateMessage();
        }

        private int _id;
        public int Id => _id;

        public string Name { get; set; }

        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

        private string GenerateMessage(
            int minWords = 3, int maxWords = 20,
            int minSentences = 1, int maxSentences = 10)
        {
            var words = new[]
            {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            };

            var rand = new Random();
            int numSentences = rand.Next(maxSentences - minSentences) + minSentences + 1;
            int numWords = rand.Next(maxWords - minWords) + minWords + 1;

            var result = new StringBuilder();

            for (int s = 0; s < numSentences; s++)
            {
                for (int w = 0; w < numWords; w++)
                {
                    if (w > 0)
                    {
                        result.Append(" ");
                    }

                    result.Append(words[rand.Next(words.Length)]);
                }

                result.Append(". ");
            }
            return result.ToString();

        }
    }
}
