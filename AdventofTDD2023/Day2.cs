namespace AdventofTDD2023
{
    public class Day2
    {
        public int GetAnswer(string inputText,int exceptRedCubes,int exceptGreenCubes,int exceptBlueCubes)
        {
            if (string.IsNullOrEmpty(inputText))
                throw new ArgumentException($"The parameter '{nameof(inputText)}' can not be null or empty.");

            foreach (var game in inputText.Split(new char[] { '\r', '\n' }))
            {

            }

            return 0;
        }
    }
}