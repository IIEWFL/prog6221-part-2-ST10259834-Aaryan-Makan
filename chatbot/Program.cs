using System;
using System.Media;
using System.Threading;



// Catalin Cimpanu (2019). Microsoft: Using multi-factor authentication blocks 99.9% of account hacks.
// [online] ZDNET. Available at: https://www.zdnet.com/article/microsoft-using-multi-factor-authentication-blocks-99-9-of-account-hacks/?utm 
// [Accessed 23 Apr. 2025].

// Federal Trade Commission (2022). How to Recognize and Avoid Phishing Scams.
// [online] Consumer Information. Available at: https://consumer.ftc.gov/articles/how-recognize-and-avoid-phishing-scams.

// Staysafeonline.org. (2024). Online Safety and Privacy Resources - National Cybersecurity Alliance.
// [online] Available at: https://www.staysafeonline.org/resources/online-safety-and-privacy.

// w3schools (2022). C# Tutorial (C Sharp).
// [online] www.w3schools.com. Available at: https://www.w3schools.com/cs/index.php.

// 262588213843476 (2018). Typewriter effect for C# console - https://gfycat.com/ObeseFancyAvians.
// [online] Gist. Available at: https://gist.github.com/joshschmelzle/610451c749dd14bb777a?utm 
// [Accessed 23 Apr. 2025].

// Image to ASCII: Free ASCII Art Converter. (n.d.). Image to ASCII: Free ASCII Art Converter.
// [online] Available at: https://www.asciiart.eu/image-to-ascii.

// Elevenlabs (2024). ElevenLabs - Generative AI Text to Speech & Voice Cloning.
// [online] elevenlabs.io. Available at: https://elevenlabs.io/.


namespace chatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the console window title
            Console.Title = " Cybersecurity Awareness Assistant";

            // Set the background color and clear the console
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            // Play an audio greeting when the app starts
            PlayStartupGreeting();

            // Show the ASCII logo
            PrintAsciiArtLogo();
            PrintDivider();

            // Ask for and get the user's name
            string userAlias = RequestUserName();

            // Ask how the user is feeling
            AskUserMood();

            // Show divider and the options menu
            PrintDivider();
            DisplayLearningTopics();

            // Begin chatbot interaction loop
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n>>> "); // Prompt for input
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
                    ShowTypingEffect($"\n  Logging off... Stay vigilant in the Cyber Space, {userAlias}.", 30);
                    break;
                }

                // Handle user input
                ProcessUserSelection(userInput);
            }
        }

        // Play an audio file for greeting
        static void PlayStartupGreeting()
        {
            try
            {
                string soundPath = @"Assets\greeting.wav";
                SoundPlayer greetingPlayer = new SoundPlayer(soundPath);
                greetingPlayer.Load();
                greetingPlayer.PlaySync();
            }
            catch (Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Audio Error] " + error.Message);
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
            Console.WriteLine(asciiLogo);
            Console.ResetColor();
        }

        // Print a visual divider line
        static void PrintDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n" + new string('═', 65) + "\n");
            Console.ResetColor();
        }

        // Ask user for their name
        static string RequestUserName()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Enter your name: ");
            string enteredName = Console.ReadLine();
            Console.ResetColor();

            // If name is empty, assign default name
            if (string.IsNullOrWhiteSpace(enteredName)) enteredName = "Cyber Cadet";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nWelcome aboard, {enteredName}!");
            Console.ResetColor();

            return enteredName;
        }

        // Ask the user how they are doing
        static void AskUserMood()
        {
            ShowTypingEffect("How are you doing today?", 40);

            Console.ForegroundColor = ConsoleColor.Cyan;
            string moodInput = Console.ReadLine().ToLower();
            Console.ResetColor();

            // Respond to different moods
            if (moodInput.Contains("good") || moodInput.Contains("great") || moodInput.Contains("fine"))
                ShowTypingEffect(" That's great to hear! Let's dive in!");
            else if (moodInput.Contains("bad") || moodInput.Contains("not good"))
                ShowTypingEffect("Sorry to hear that. I'm here to make your day better!");
            else
                ShowTypingEffect("Got it! Let's keep moving forward into cyberspace!");
        }

        // Display available topics for the user to choose
        static void DisplayLearningTopics()
        {
            ShowTypingEffect(" Select a topic to begin learning:\n", 30);
            ShowTypingEffect("  1️ Password Safety");
            ShowTypingEffect("  2️ Phishing Scams");
            ShowTypingEffect("  3️ Safe Browsing Tips");
            ShowTypingEffect("   Type 'exit' to end the session.");
        }

        // Process user's topic selection
        static void ProcessUserSelection(string choice)
        {
            choice = choice.ToLower();

            if (choice.Contains("1") || choice.Contains("password"))
            {
                ShowTypingEffect(" Tip: Using strong, unique passwords is one of the best ways to protect your online accounts. A good password should be at least 12 characters long and include a mix of uppercase letters, lowercase letters, numbers, and special symbols. Avoid using common words or personal information like your name or birthday. Also, it’s important not to reuse passwords across different accounts—if one account gets compromised, others could be at risk too.\r\nFor extra protection, enable two-factor authentication (2FA) wherever possible. This adds an extra step when logging in, like entering a code sent to your phone or generated by an authentication app. Even if someone manages to get your password, they won’t be able to access your account without that second factor. Combining strong passwords with 2FA makes it much harder for attackers to break into your accounts.\r\n");
            }
            else if (choice.Contains("2") || choice.Contains("phishing"))
            {
                ShowTypingEffect(" Tip: Phishing is a common trick cybercriminals use to steal your personal information. These scams often come in the form of emails or messages that seem urgent—like saying your account will be locked or that you've won something. They try to get you to click on a link or open an attachment, which can lead to malware or fake websites designed to steal your login details. It's best to avoid clicking on anything if the message seems even a little suspicious.\r\nTo stay safe, always take a moment to verify who the message is really from. Even if it looks like it’s from a trusted source, double-check the email address or contact the sender directly through a known and trusted method. If something feels off, trust your instincts. Being cautious with emails and messages can protect you from falling victim to phishing scams.\r\n");
            }
            else if (choice.Contains("3") || choice.Contains("browsing"))
            {
                ShowTypingEffect("Tip: When you're browsing the internet, it's important to stay on secure websites—look for URLs that start with HTTPS instead of just HTTP. That little “S” means the site encrypts the data you send, making it harder for hackers to intercept your information. It’s a small detail that makes a big difference in keeping your personal info safe.\r\nAlso, be careful with pop-ups and flashy ads, especially the ones that seem too good to be true or ask you to click quickly. These can sometimes lead to harmful sites or even download malware onto your device. To stay protected, always keep your browser and antivirus software up to date. Updates often include fixes for security vulnerabilities, so running the latest version helps block threats before they reach you.\r\n");
            }
            else
            {
                ShowTypingEffect(" I didn’t catch that. Try selecting 1, 2, or 3");
            }
        }

        // Simulate a typewriter text effect
        static void ShowTypingEffect(string message, int delay = 30)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char ch in message)
            {
                Console.Write(ch);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}