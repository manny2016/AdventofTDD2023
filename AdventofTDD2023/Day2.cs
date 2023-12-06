namespace AdventofTDD2023
{
    public class Day2
    {
        public int GetAnswer1(string inputText, int limitedRedCubes, int limitedGreenCubes, int limitedBlueCubes)
        {
            if (string.IsNullOrEmpty(inputText))
                throw new ArgumentException($"The parameter '{nameof(inputText)}' can not be null or empty.");

            var lines = inputText.Split(new char[] { '\r', '\n' }).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            var answer = 0;
            for (var i = 1; i <= lines.Length; i++)
            {
                var gameInfo = lines[i - 1].Split(":");

                var possible = gameInfo[1].Split(';').Select(y =>
                     y.Split(',')
                           .Select(x => new
                           {
                               Color = x.Trim().Split(' ')[1],
                               Count = int.Parse(x.Trim().Split(' ')[0])
                           }).Any(x =>
                           {
                               switch (x.Color.ToLower())
                               {
                                   case "blue":
                                       return x.Count > limitedBlueCubes;
                                   case "red":
                                       return x.Count > limitedRedCubes;
                                   case "green":
                                       return x.Count > limitedGreenCubes;
                                   default:
                                       return false;
                               }
                           })
                 );

                if (possible.All(x => !x))
                    answer += i;
            }

            return answer;
        }


        public int GetAnswer2(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
                throw new ArgumentException($"The parameter '{nameof(inputText)}' can not be null or empty.");
            var lines = inputText.Split(new char[] { '\r', '\n' }).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            var answer = 0;
            for (var i = 0; i < lines.Length; i++)
            {
                var gameInfo = lines[i].Split(":");
                var items = gameInfo[1].Split(';').Select(y =>
                    y.Split(',')
                    .Select(x => new
                    {
                        Color = x.Trim().Split(' ')[1],
                        Count = int.Parse(x.Trim().Split(' ')[0])
                    }));
                var red = int.MinValue;
                var blue = int.MinValue;
                var green = int.MinValue;

                foreach (var item in items)
                {
                    foreach (var xx in item)
                    {
                        switch (xx.Color)
                        {
                            case "blue":
                                blue = Math.Max(blue, xx.Count);
                                break;
                            case "red":
                                red = Math.Max(red, xx.Count);
                                break;
                            case "green":
                                green = Math.Max(green, xx.Count);
                                break;
                        }
                    }
                }
                answer += red * blue * green;
            }
            return answer;
        }
    }
}