using System; // Basic system functions
using System.Media; // Audio playback
using System.Threading; // Threading for delays
using System.Collections.Generic; // Generic collections
using System.Linq; // LINQ for querying



// Code Attribution:
// - Code samples and snippets from: https://stackoverflow.com/
// - C# tutorials and language features from: https://learn.microsoft.com/en-us/dotnet/csharp/
// - Practical examples of chatbot logic in C#: https://www.c-sharpcorner.com/
// - Guidance on building .NET applications: https://dotnet.microsoft.com/
// - C# syntax reference and best practices from: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/



namespace chatbot
{
    class Program
    {
        private static string userAlias = "Cyber Cadet"; // Default user nickname
        private static string userInterest = ""; // Stores user's preferred topic
        private static string lastKeyword = ""; // Tracks the last topic discussed
        private static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>(); // Stores responses by keyword
        private static Dictionary<string, List<string>> usedResponses = new Dictionary<string, List<string>>(); // Tracks used responses to avoid repetition
        private static Random random = new Random(); // Random number generator

        // Delegate for selecting responses dynamically
        delegate string ResponseSelector(string keyword);

        static void Main(string[] args)
        {
            // Set the console window title
            Console.Title = "Cybersecurity Awareness Assistant";

            // Set background color to black and clear the console
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            // Play an audio greeting at startup
            PlayStartupGreeting();

            // Show the ASCII logo
            PrintAsciiArtLogo();
            PrintDivider();

            // Get the user's name
            userAlias = RequestUserName();

            // Ask about the user's mood
            AskUserMood();

            // Initialize keyword responses and tracking
            InitializeKeywordResponses();
            InitializeUsedResponses();

            // Show divider and learning topics menu
            PrintDivider();
            DisplayLearningTopics();

            // Start the main interaction loop
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n>>> "); // Prompt user for input
                Console.ResetColor();

                string userInput = Console.ReadLine();

                // Check for empty input
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Input cannot be empty. Please try again.");
                    Console.ResetColor();
                    continue;
                }

                // Check for exit command
                if (userInput.ToLower().Trim() == "exit")
                {
                    ShowTypingEffect($"\nLogging off... Stay vigilant in the Cyber Space, {userAlias}.", 30);
                    break;
                }

                // Process the user's input
                ProcessUserInput(userInput);
            }
        }

        // Play an audio greeting file
        static void PlayStartupGreeting()
        {
            try
            {
                string soundPath = @"Assets\greeting.wav"; // Path to audio file
                SoundPlayer greetingPlayer = new SoundPlayer(soundPath);
                greetingPlayer.Load();
                greetingPlayer.PlaySync();
            }
            catch (Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Audio Error] " + error.Message); // Display error if audio fails
                Console.ResetColor();
            }
        }

        // Display ASCII art and logo
        static void PrintAsciiArtLogo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string asciiLogo = @"

 
                                        
      .; ;; ;.      
     xX: ;; :Xx     
  ::+x:;;XX:;;x+::  
..;;+X:x.;; x:X+;;..
     ;X; ;; ;X;     
    .::X.;; $;;     
      .+.;; x.      
       ;.;; +       
       ; :: :       
         ;;         
         :;                            
                                                             
     _____)  _____    _____   )   ___      _____) 
   /        (, /   ) (, /  | (__/_____)  /        
  /   ___     /__ /    /---|   /         )__      
 /     / ) ) /   \_ ) /    |_ /        /          
(____ /   (_/      (_/       (______) (_____)     

       
Hi, I am Grace, your friendly Cybersecurity Awareness Assistant!
I am here to help you learn about cybersecurity and stay safe online! 
";
            Console.WriteLine(asciiLogo); // Display the ASCII art
            Console.ResetColor();
        }

        // Print a visual divider line
        static void PrintDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n" + new string('═', 65) + "\n"); // Draw divider line
            Console.ResetColor();
        }

        // Ask user for their name
        static string RequestUserName()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Enter your name: "); // Prompt for name
            string enteredName = Console.ReadLine();
            Console.ResetColor();

            if (string.IsNullOrWhiteSpace(enteredName)) enteredName = "Cyber Cadet"; // Set default if empty
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nWelcome aboard, {enteredName}!"); // Welcome message
            Console.ResetColor();

            return enteredName;
        }

        // Ask the user how they are doing
        static void AskUserMood()
        {
            ShowTypingEffect("How are you doing today?", 40); // Prompt for mood with typing effect

            Console.ForegroundColor = ConsoleColor.Cyan;
            string moodInput = Console.ReadLine().ToLower(); // Get user mood
            Console.ResetColor();

            // Respond based on detected mood
            if (moodInput.Contains("good") || moodInput.Contains("great") || moodInput.Contains("fine"))
                ShowTypingEffect("That's great to hear! Let's dive in!");
            else if (moodInput.Contains("bad") || moodInput.Contains("not good"))
                ShowTypingEffect("Sorry to hear that. I'm here to make your day better with some tips!");
            else if (moodInput.Contains("worried"))
                ShowTypingEffect("I can sense you're worried. Let's tackle that together with some advice!");
            else if (moodInput.Contains("frustrated"))
                ShowTypingEffect("I understand that can be frustrating. I'm here to help simplify things for you!");
            else if (moodInput.Contains("curious"))
                ShowTypingEffect("Great to see your curiosity! I’ve got lots of tips to share!");
            else
                ShowTypingEffect("Got it! Let's keep moving forward into cyberspace!");
        }

        // Initialize keyword responses with multiple options
        static void InitializeKeywordResponses()
        {
            keywordResponses["password"] = new List<string>
            {
                "Using strong, unique passwords is key to protecting your accounts. Aim for at least 12 characters with a mix of letters, numbers, and symbols. Avoid personal info like your name or birthday!",
                "A good password habit is to never reuse passwords across sites. If one gets hacked, others stay safe. Consider using a password manager to generate and store complex passwords.",
                "Boost security with two-factor authentication (2FA). It adds a second step, like a code from your phone, making it nearly impossible for hackers to break in even with your password.",
                "Change your passwords every few months to reduce the risk of them being compromised over time. Use a mix of random characters for maximum strength.",
                "Avoid writing passwords down where others can find them. Instead, rely on a secure digital password manager to keep them safe and accessible only to you."
            };
            keywordResponses["scam"] = new List<string>
            {
                "Watch out for phishing scams! Emails asking for personal info are a red flag—verify the sender by checking the email address carefully.",
                "Scammers often pose as trusted organizations. Always contact the company directly using a known phone number or website to confirm suspicious messages.",
                "If an offer seems too good to be true, it probably is. Avoid clicking links in unexpected texts or emails to prevent malware installation.",
                "Be cautious of urgent messages claiming your account is at risk. Legitimate organizations rarely ask for sensitive info via email—call them to verify.",
                "Look for spelling or grammar errors in messages, a common sign of phishing. Legit companies usually maintain professional communication."
            };
            keywordResponses["privacy"] = new List<string>
            {
                "Protect your privacy by reviewing app permissions. Only allow access to what’s necessary, like camera or location, to limit data exposure.",
                "Use HTTPS websites to encrypt your data. That 'S' in the URL ensures your browsing is secure from interception by hackers.",
                "Consider clearing your browser cookies regularly to reduce tracking of your online habits and protect your personal information.",
                "Adjust your social media privacy settings to control who can see your posts and personal details—limit access to friends only if possible.",
                "Use a VPN when on public Wi-Fi to encrypt your internet connection and prevent others from snooping on your online activities."
            };
        }

        // Initialize tracking for used responses
        static void InitializeUsedResponses()
        {
            foreach (var keyword in keywordResponses.Keys) // For each keyword
            {
                usedResponses[keyword] = new List<string>(); // Initialize empty list for used responses
            }
        }

        // Display available learning topics
        static void DisplayLearningTopics()
        {
            ShowTypingEffect("Select a topic to begin learning:\n", 30); // Display menu with effect
            ShowTypingEffect("1️ Password Safety");
            ShowTypingEffect("2️ Phishing Scams");
            ShowTypingEffect("3️ Safe Browsing Tips");
            ShowTypingEffect("Type 'exit' to end the session.");
            ShowTypingEffect("Or ask about 'password', 'scam', or 'privacy' for more info!");
        }

        // Process user input with flow, memory, and sentiment
        static void ProcessUserInput(string input)
        {
            input = input.ToLower(); // Convert input to lowercase

            // Detect sentiment in input
            bool sentimentDetected = false;
            string sentimentResponse = "";
            if (input.Contains("worried"))
            {
                sentimentDetected = true;
                sentimentResponse = "It’s completely understandable to feel that way. Let’s address your concerns.";
            }
            else if (input.Contains("frustrated"))
            {
                sentimentDetected = true;
                sentimentResponse = "I understand that can be frustrating. Let’s break this down together.";
            }
            else if (input.Contains("curious"))
            {
                sentimentDetected = true;
                sentimentResponse = "I love your curiosity! Let’s explore that topic further.";
            }

            // Map related terms to keywords
            string keyword = "";
            if (input.Contains("password") || input.Contains("passwords"))
                keyword = "password";
            else if (input.Contains("scam") || input.Contains("phishing"))
                keyword = "scam";
            else if (input.Contains("privacy"))
                keyword = "privacy";

            // Set user interest if mentioned
            if (input.Contains("interested") && (input.Contains("password") || input.Contains("scam") || input.Contains("privacy")))
            {
                userInterest = keyword;
                ShowTypingEffect($"Great! I'll remember that you're interested in {userInterest}. It's a crucial part of staying safe online!");
                lastKeyword = userInterest;
                return;
            }

            // Handle follow-ups or confusion
            if (input.Contains("more") || input.Contains("explain") || input.Contains("confused"))
            {
                // Check if the input contains a keyword, and if so, override lastKeyword
                string topicToExpand = lastKeyword;
                if (!string.IsNullOrEmpty(keyword)) // Override with new keyword if present
                {
                    topicToExpand = keyword;
                    lastKeyword = keyword; // Update lastKeyword to the new topic
                }
                else if (string.IsNullOrEmpty(topicToExpand))
                {
                    if (!string.IsNullOrEmpty(userInterest))
                        topicToExpand = userInterest;
                    else
                    {
                        ShowTypingEffect("Could you let me know what topic you'd like more details on?");
                        return;
                    }
                }

                ResponseSelector selector = GetRandomResponse;
                string response = selector(topicToExpand);
                if (sentimentDetected)
                    ShowTypingEffect($"{sentimentResponse} Let me expand on {topicToExpand}: {response}");
                else
                    ShowTypingEffect($"Let me expand on {topicToExpand}: {response}");
                return;
            }

            // Keyword recognition and random responses
            if (!string.IsNullOrEmpty(keyword))
            {
                lastKeyword = keyword;
                if (string.IsNullOrEmpty(userInterest))
                    userInterest = keyword; // Set userInterest for better flow

                ResponseSelector selector = GetRandomResponse;
                string response = selector(keyword);
                if (sentimentDetected)
                    ShowTypingEffect($"{sentimentResponse} {response}");
                else
                    ShowTypingEffect(response);

                if (!string.IsNullOrEmpty(userInterest) && keyword == userInterest)
                    ShowTypingEffect($"Since you're into {userInterest}, you might also check your account security settings!");
                return;
            }
            else if (input.Contains("1"))
            {
                lastKeyword = "password";
                if (string.IsNullOrEmpty(userInterest))
                    userInterest = "password";
                ResponseSelector selector = GetRandomResponse;
                ShowTypingEffect(selector("password"));
                return;
            }
            else if (input.Contains("2"))
            {
                lastKeyword = "scam";
                if (string.IsNullOrEmpty(userInterest))
                    userInterest = "scam";
                ResponseSelector selector = GetRandomResponse;
                ShowTypingEffect(selector("scam"));
                return;
            }
            else if (input.Contains("3") || input.Contains("browsing"))
            {
                lastKeyword = "browsing";
                ShowTypingEffect("Tip: Use HTTPS sites to encrypt your data and keep your browser updated to patch security holes. Avoid clicking suspicious pop-ups!");
                return;
            }

            // Default error handling
            ShowTypingEffect("I'm not sure I understand. Can you try rephrasing or selecting 1, 2, or 3?");
        }

        // Get a random response without repetition until all are used
        static string GetRandomResponse(string keyword)
        {
            if (!keywordResponses.ContainsKey(keyword))
                return "No response available for this topic.";

            var responses = keywordResponses[keyword];
            var used = usedResponses[keyword];

            // Reset used responses if all have been used
            if (used.Count == responses.Count)
                used.Clear();

            // Get remaining unused responses
            var availableResponses = responses.Where(r => !used.Contains(r)).ToList();
            if (availableResponses.Count == 0)
                return "No new responses available for this topic.";

            // Select a random unused response
            string selectedResponse = availableResponses[random.Next(availableResponses.Count)];
            used.Add(selectedResponse);

            return selectedResponse;
        }

        // Simulate a typewriter text effect
        static void ShowTypingEffect(string message, int delay = 30)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char ch in message) // Print each character with a delay
            {
                Console.Write(ch);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}