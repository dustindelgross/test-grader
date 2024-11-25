namespace TestGrader
{
    class Program
    {

        static void Main()
        {

            bool submitted = false;
            string[] solutionKey = File.ReadAllLines("solutionFile.txt");
            string[] gradedAnswers = new string[10];
            Question[] questions = new Question[10];

            for (int i = 1; i <= 10; i++)
            {
                Question q = new(i, solutionKey[i - 1][^1].ToString().ToLower());
                questions[i - 1] = q;

                Console.WriteLine(q.question);
                Console.WriteLine("Enter your choice (a, b, c, or d): ");
                string choice = Console.ReadLine()!.ToLower();

                gradedAnswers[i - 1] = $"Question {q.questionNumber}: ";
                gradedAnswers[i - 1] += q.CheckAnswer(choice) ? "Correct!" : "Incorrect.";

            }

            ShowGradedAnswers(gradedAnswers);

            while (!submitted)
            {
                ShowGradedAnswers(gradedAnswers);

                if (questions.Any(q => !q.correct))
                {
                    PromptReview(questions, gradedAnswers);
                }
                else
                {
                    Console.WriteLine("You got all the questions correct!");
                }

                Console.WriteLine("Ready to submit your answers? (y/n) ");
                string submit = Console.ReadLine()!.ToLower();
                if (submit == "y")
                {
                    submitted = true;
                }
            }

            int score = GradeTest(questions, gradedAnswers);

            switch (score)
            {
                case 10:
                    Console.WriteLine("Congratulations! You got a perfect score!");
                    break;
                case >= 7:
                    Console.WriteLine("Great job! You passed the test!");
                    break;
                default:
                    Console.WriteLine("You did not pass the test. Better luck next time!");
                    break;
            }
            
            Console.WriteLine($"You answered {score} out of 10 questions correctly.");

        }


        static void ShowGradedAnswers(string[] gradedAnswers)
        {
            Console.WriteLine("Graded Answers: ");
            foreach (string answer in gradedAnswers)
            {
                Console.WriteLine(answer);
            }
        }

        static int GradeTest(Question[] questions, string[] gradedAnswers)
        {
            int score = 0;
            foreach (Question q in questions)
            {
                if (q.correct)
                {
                    score++;
                }
            }
            return score;
        }

        static void PromptQuestion(Question q, string[] gradedAnswers)
        {
            Console.WriteLine(q.question);
            Console.WriteLine("Enter your choice (a, b, c, or d): ");
            string choice = Console.ReadLine()!.ToLower();
            q.CheckAnswer(choice);
            gradedAnswers[q.questionNumber - 1] = $"{q.questionNumber}: ";
            gradedAnswers[q.questionNumber - 1] += q.CheckAnswer(choice) ? "Correct!" : "Incorrect!";
        }

        static void PromptReview(Question[] questions, string[] gradedAnswers)
        {
            Console.WriteLine("Would you like to review the answers you missed? (y/n) ");
            string review = Console.ReadLine()!.ToLower();
            if (review == "y")
            {
                questions.ToList().ForEach(q =>
                {
                    if (!q.correct)
                    {
                        PromptQuestion(q, gradedAnswers);
                    }
                });
                ShowGradedAnswers(gradedAnswers);
            }
        }

    }

}