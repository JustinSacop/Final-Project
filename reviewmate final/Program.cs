using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Reviewmate
{
    private List<(string Question, string Answer)> questions;
    private Dictionary<string, int> questionIncorrectCount = new Dictionary<string, int>();
    private List<string> incorrectAnswers = new List<string>();
    private int score = 0;
    private int highestScore = 0;
    private const string filename = "questions.txt";
    private Random random = new Random();

    public Reviewmate()
    {
        LoadQuestions();
    }

    private void LoadQuestions()
    {
        questions = new List<(string Question, string Answer)>
        {
          ("IPO Stands for?", "Input Process Output"),
          ("PDLC Stands for?", "Program Development Life Cycle"),
          ("UML Stands for?", "Unified Modeling Language"),
          ("CLR Stands for?", "Common Language Runtime"),
          ("IL Stands for?", "Intermediate Language"),
          ("JIT Stands for?", "Just In Time"),
          ("IDE Stands for?", "Integrated Development Environment"),
          ("GUI Stands for?", "Graphic User Interface"),
          ("CSS Stands for?", "Cascading Style Sheet"),
          ("HTML Stands for?", "HyperText Markup Language")
        };

        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 2)
                {
                    questions.Add((parts[0].Trim(), parts[1].Trim()));
                }
            }
        }
    }

    private void SaveQuestionsToFile()
    {
        List<string> lines = questions.Select(q => $"{q.Question}|{q.Answer}").ToList();
        File.WriteAllLines(filename, lines);
    }

    private void AddQuestion()
    {
        Console.Write("Enter the new question: ");
        string question = Console.ReadLine().Trim();

        Console.Write("Enter the answer: ");
        string answer = Console.ReadLine().Trim();

        questions.Add((question, answer));
        SaveQuestionsToFile(); // Save the new list of questions
    }
    private void DisplayTutorial()
    {
        Console.WriteLine("Welcome to Reviewmate tutorial!");
        Console.WriteLine("Reviewmate is your personalized reviewer console edition.");
        Console.WriteLine("This program is designed to help you review and reinforce your knowledge through a simple quiz game. Let's get started!");

        Console.WriteLine("1. **Main Menu:**");
        Console.WriteLine("-You are presented with a main menu upon starting Reviewmate.");
        Console.WriteLine("-Options:");
        Console.WriteLine("-Press **1** to start a new game.");
        Console.WriteLine("-Press **2** to exit the program.");
        Console.WriteLine("-Press **3** to add a new question to the quiz pool.");
        Console.WriteLine("-Press **4** to review frequently missed questions.");
        Console.WriteLine("-Press **5** to Reviewmate tutorial.");

        Console.WriteLine("2. **Starting a New Game:**");
        Console.WriteLine("-Choose option **1** to start a new game.");
        Console.WriteLine("-Answer the questions presented to you.");
        Console.WriteLine("-Your score and the correct answer will be displayed after each question.");
        Console.WriteLine("-At the end of the game, you can choose to play again or return to the main menu.");

        Console.WriteLine("3. **Exiting the Program:**");
        Console.WriteLine("-Choose option **2** to exit Reviewmate at any time.");

        Console.WriteLine("4. **Adding a New Question:**");
        Console.WriteLine("-Choose option **3** to add a new question to the quiz pool.");
        Console.WriteLine("-Enter the new question and its correct answer when prompted.");
        Console.WriteLine("-The new question will be saved, and you can see it in future games.");

        Console.WriteLine("5. **Reviewing Frequently Missed Questions:**");
        Console.WriteLine("-Choose option **4** to review questions you frequently answered incorrectly.");
        Console.WriteLine("-The program will display the most frequently missed questions and the number of times you missed them.");

        Console.WriteLine("6. **Game Over:**");
        Console.WriteLine("-After finishing a game, your current score and highest score will be displayed.");
        Console.WriteLine("-If you had incorrect answers, you can choose to review them.");

        Console.WriteLine("7. **Reviewing Incorrect Answers:**");
        Console.WriteLine("-If you choose to review incorrect answers, they will be displayed.");

        Console.WriteLine("8. **Returning to the Main Menu:**");
        Console.WriteLine("-After completing an action (starting a game, adding a question, or reviewing questions), press any key to return to the main menu.");

        Console.WriteLine("9. **Exiting Reviewmate:**");
        Console.WriteLine("-To exit Reviewmate, choose option **2** from the main menu.");
        Console.WriteLine("-A thank you message will be displayed, and the program will exit.");

        Console.WriteLine("Now that you're familiar with the Reviewmate console edition,");
        Console.WriteLine("feel free to explore the quiz, add new questions, and improve your knowledge. Enjoy learning with Reviewmate!.");

    }
    private void DisplayMainMenu()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("**********************************************");
        sb.AppendLine("*               REVIEWMATE                   *");
        sb.AppendLine("* Your Personalized Reviewer Console Edition *");
        sb.AppendLine("**********************************************");
        sb.AppendLine("*                                            *");
        sb.AppendLine("*   1. Game Tutorial                         *");
        sb.AppendLine("*   2. Start Game                            *");
        sb.AppendLine("*   3. Add a new question                    *");
        sb.AppendLine("*   4. Review frequently missed questions    *");
        sb.AppendLine("*   5. Exit                                  *");
        sb.AppendLine("*                                            *");
        sb.AppendLine("**********************************************");
        sb.AppendLine("Enter your choice (1-5):");

        Console.WriteLine(sb.ToString());
    }

    private void DisplayScore()
    {
        Console.WriteLine($"Your current score is: {score}");
        Console.WriteLine($"Highest score: {highestScore}");
    }

    private void ReviewFrequentlyMissedQuestions()
    {
        // Displaying the most frequently incorrect questions
        if (questionIncorrectCount.Any())
        {
            var sortedIncorrectCounts = questionIncorrectCount.OrderByDescending(kvp => kvp.Value);
            Console.WriteLine("\nMost Frequently Missed Questions:");
            foreach (var kvp in sortedIncorrectCounts)
            {
                Console.WriteLine($"Question: {kvp.Key} - Missed: {kvp.Value} time(s)");
            }
        }
        else
        {
            Console.WriteLine("\nNo missed questions to review.");
        }
    }

    public void StartGame()
    {
        incorrectAnswers.Clear(); // Clear the list of incorrect answers
        score = 0; // Reset score on game start
        Console.Clear();
        Console.WriteLine("Welcome to Reviewmate your Personalized Reviewer Console Edition!");

        while (true)
        {
            int index = random.Next(questions.Count);
            var ques = questions[index];

            Console.WriteLine(ques.Question);
            string userAnswer = Console.ReadLine().Trim();

            if (string.Equals(userAnswer, ques.Answer, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Correct!");
                score++;
            }
            else
            {
                Console.WriteLine($"Incorrect! The correct answer is: {ques.Answer}");
                incorrectAnswers.Add($"{ques.Question} Correct answer: {ques.Answer}");
                if (questionIncorrectCount.ContainsKey(ques.Question))
                {
                    questionIncorrectCount[ques.Question]++;
                }
                else
                {
                    questionIncorrectCount[ques.Question] = 1;
                }
            }

            DisplayScore();

            Console.WriteLine("\nWould you like to play again? (yes/no)");
            string playAgain = Console.ReadLine().Trim().ToLower();
            if (playAgain == "quit")
            {
                break; // Break out of the loop to return to the main menu
            }
            else if (playAgain != "yes")
            {
                highestScore = Math.Max(highestScore, score);
                Console.WriteLine("Returning to the main menu.");
                break; // Break out of the loop to return to the main menu
            }
        }
        // After game has ended, show  score and incorrect answers before returning to the main menu
        Console.WriteLine("\nGame Over!");
        DisplayScore();
        if (incorrectAnswers.Any())
        {
            Console.WriteLine("\nYou had some incorrect answers. Would you like to review them? (yes/no)");
            string reviewChoice = Console.ReadLine().Trim().ToLower();
            if (reviewChoice == "yes")
            {
                ReviewIncorrectAnswers();
            }
        }
    }

    private void ReviewIncorrectAnswers()
    {
        Console.WriteLine("\nIncorrect Answers Review:");
        foreach (string incorrect in incorrectAnswers)
        {
            Console.WriteLine(incorrect);
        }
    }

    public static void Main(string[] args)
    {
        Reviewmate game = new Reviewmate();
        bool running = true;

        while (running)
        {
            Console.Clear();
            game.DisplayMainMenu();
            string choice = Console.ReadLine().Trim();

            switch (choice)
            {
                case "1":
                    game.DisplayTutorial();
                    break;
                case "2":
                    game.StartGame();
                    break;
                case "3":
                    game.AddQuestion();
                    break;
                case "4":
                    game.ReviewFrequentlyMissedQuestions();
                    break;
                case "5":
                    Console.WriteLine("Thank you for using Reviewmate! Exiting...");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid selection, please try again.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }
        }
    }
}