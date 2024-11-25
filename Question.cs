namespace TestGrader
{
    class Question
    {
        public Dictionary<string, int> multipleChoiceMap = new Dictionary<string, int>();

        private readonly int correctChoiceIndex;
        public string question;
        public int questionNumber;
        public bool correct;

        public Question(int questionNumber, string correctChoice)
        {
            multipleChoiceMap["a"] = 0;
            multipleChoiceMap["b"] = 1;
            multipleChoiceMap["c"] = 2;
            multipleChoiceMap["d"] = 3;

            this.questionNumber = questionNumber;
            Random r = new();
            correctChoiceIndex = multipleChoiceMap[correctChoice];
            int num1 = r.Next(1, 10);
            int num2 = r.Next(1, 10);
            int answer = num1 + num2;
            int[] choices = new int[4];
            choices[correctChoiceIndex] = answer;

            for (int i = 0; i < 4; i++)
            {
                int newChoice = r.Next(1, 20);
                if (i != correctChoiceIndex)
                {
                    choices[i] = newChoice == answer ? newChoice + 1 : newChoice;
                }
            }

            question = GenerateQuestion(questionNumber, num1, num2, choices);

        }


        /// Generates a question with a random number and 3 random choices
        public static string GenerateQuestion(int questionNumber, int num1, int num2, int[] choices)
        {

            return $"Question {questionNumber}: What is {num1} + {num2}?\n" +
                $"a) {choices[0]}\n" +
                $"b) {choices[1]}\n" +
                $"c) {choices[2]}\n" +
                $"d) {choices[3]}\n";

        }


        public bool CheckAnswer(string choice)
        {
            correct = multipleChoiceMap[choice] == correctChoiceIndex;
            return correct;
        }


    }
}