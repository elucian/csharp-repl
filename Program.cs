using System;
using System.Linq;

public class Program
{
    // A helper method to perform the arithmetic calculation.
    private static double Calculate(double num1, string op, double num2)
    {
        // Use a switch statement to handle the four basic operations.
        switch (op)
        {
            case "+":
                return num1 + num2;
            case "-":
                return num1 - num2;
            case "*":
                return num1 * num2;
            case "/":
                // Check for division by zero before performing the operation.
                if (num2 == 0)
                {
                    throw new DivideByZeroException("Error: Division by zero is not allowed.");
                }
                return num1 / num2;
            default:
                // Throw an ArgumentException if the operator is not recognized.
                throw new ArgumentException($"Error: Invalid operator '{op}'. Supported operators are +, -, *, /.");
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("Simple C# Math REPL (Read-Eval-Print Loop)");
        Console.WriteLine("Enter expressions like: 10 + 5, 4 * 2.5");
        Console.WriteLine("Type 'exit' or 'quit' to close the app.");
        Console.WriteLine("------------------------------------------");

        // The main loop for the REPL
        while (true)
        {
            Console.Write("\n=> ");
            // Read user input and ensure it's not null, then trim whitespace.
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                continue; // Skip if input is just empty space
            }

            // Check for exit commands (case-insensitive)
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting REPL. Goodbye!");
                break;
            }

            try
            {
                // Split the input by space, removing any empty entries from extra spaces.
                string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // The expected format is always 3 parts: Number1 Operator Number2
                if (parts.Length != 3)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Invalid format. Please use: <number1> <operator> <number2> (e.g., 10 + 5)");
                    Console.ResetColor();
                    continue;
                }

                // 1. Attempt to parse the first and third parts as double numbers.
                if (!double.TryParse(parts[0], out double num1) || !double.TryParse(parts[2], out double num2))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid number input. Please ensure '{parts[0]}' and '{parts[2]}' are valid numbers.");
                    Console.ResetColor();
                    continue;
                }

                // 2. Extract the operator (the middle part).
                string op = parts[1];

                // 3. Calculate the result.
                double result = Calculate(num1, op, num2);

                // Display the result in green for emphasis.
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Result: {result}");
                Console.ResetColor();
                
                // --- Example Use Cases (for user reference, not part of REPL logic) ---
                // 1. Addition: 10 + 5
                // 2. Subtraction: 25 - 12
                // 3. Multiplication: 4.5 * 3
                // 4. Division: 100 / 4
                // --------------------------------------------------------------------

            }
            // Catch specific calculation errors
            catch (DivideByZeroException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            // Catch general calculation errors (like invalid operator)
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            // Catch any unexpected system errors
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}