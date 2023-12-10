namespace AdventofTDD2023
{
    public class Day9
    {
        public int GetAnswerOfPart1(string intputData)
        {
            var histories = GetHistoryValues(intputData);
            var answer = 0;
            foreach (var history in histories)
            {
                var subSequence = GenerateSubSequences(history);
                for (var i = subSequence.Count - 1; i > 0; i--)
                {
                    var lastIndex = subSequence[i].Count - 1;
                    subSequence[i - 1].Add(subSequence[i - 1][subSequence[i - 1].Count - 1] + subSequence[i][lastIndex]);
                }
                answer += subSequence[0][subSequence[0].Count - 1];
            }
            return answer;
        }

        public int GetAnswerOfPart2(string inputData)
        {
            var answer = 0;
            foreach (var history in GetHistoryValues(inputData))
            {
                var subSequence = GenerateSubSequences(history);
                for (var i = subSequence.Count - 1; i > 0; i--)
                {
                    subSequence[i - 1].Insert(0, subSequence[i - 1][0] - subSequence[i][0]);
                }
                answer += subSequence[0][0];
            }

            return answer;
        }


        private List<List<int>> GenerateSubSequences(List<int> sequence)
        {
            var subSequence = new List<List<int>>() { sequence };
            var current = subSequence.Last();
            while (!current.All(x => x.Equals(0)))
            {
                var newSequence = new List<int>();
                for (var i = 1; i < current.Count; i++)
                {
                    newSequence.Add(current[i] - current[i - 1]);
                }

                subSequence.Add(newSequence);
                current = subSequence.Last();
            }
            return subSequence;
        }

        public IList<List<int>> GetHistoryValues(string inputData)
        {
            if (string.IsNullOrEmpty(inputData))
                throw new ArgumentException("Argument can not be null or empty.");

            var values = new List<List<int>>();

            foreach (var line in inputData.Split("\r\n".ToCharArray()))
            {
                if (string.IsNullOrEmpty(line.Trim())) continue;
                var lineData = new List<int>();
                foreach (var item in line.Split(" ".ToCharArray()))
                {
                    if (string.IsNullOrEmpty(item)) continue;

                    if (int.TryParse(item, out var @value))
                        lineData.Add(value);
                    else
                        throw new InvalidCastException($"Can cast the value '{item}' to int.");
                }
                values.Add(lineData);
            }

            return values;
        }
    }
}
