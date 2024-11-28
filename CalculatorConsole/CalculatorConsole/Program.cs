namespace CalculatorConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter First Number: ");
            if (!double.TryParse(Console.ReadLine(), out double num1))
            {
                Console.WriteLine("Error: Invalid number format!");
                return;
            }
            Console.WriteLine("Enter Second Number: ");
            if (!double.TryParse(Console.ReadLine(), out double num2))
            {
                Console.WriteLine("Error: Invalid number format!");
                return;
            }
            Console.WriteLine("Enter Operation");
            string operation = Console.ReadLine();
            Calculator calculator = new Calculator(num1, num2, operation);
            double result;
            switch (operation)
            {
                case "+":
                    calculator.Sum();
                    break;
                case "-":
                    calculator.Min();
                    break;
                case "*":
                    calculator.Multiply();
                    break;
                case "/":
                    if (num2 == 0)
                    {
                        Console.WriteLine("Cannot Divide By Zero");
                    }
                    else
                    {
                        calculator.Divide();
                        
                    }
                    break;
                default:
                    Console.WriteLine("This Operation Is Not Valide");
                    break;
                     
            }
            Console.WriteLine("Do you want to perform another operation? (yes/no)");
            string continueChoice = Console.ReadLine()?.ToLower();
            if (continueChoice != "yes")
            {
                
            }
        }
        }
        public class Calculator
        {
            public double FirstNumber { get; set; }
            public double SecondNumber { get; set; }
            public string Operation { get; set; }
            public Calculator()
            {

            }
            public Calculator(double firstNumber , double secondNumber , string operation)
            {
                FirstNumber = firstNumber;
                SecondNumber = secondNumber;
                Operation = operation;
            }
            public void Sum()
            {
                double result = FirstNumber + SecondNumber;
                Console.WriteLine($"Result: {FirstNumber} + {SecondNumber} = {result}");
            }
            public void Min()
            {
                double result = FirstNumber - SecondNumber;
                Console.WriteLine($"Result: {FirstNumber} - {SecondNumber} = {result}");
            }
            public void Multiply()
            {
                double result = FirstNumber * SecondNumber;
                Console.WriteLine($"Result: {FirstNumber} * {SecondNumber} = {result}");
            }
            public void Divide() 
            { if (SecondNumber != 0)
            {
                double result = FirstNumber / SecondNumber;
                Console.WriteLine($"Result: {FirstNumber} / {SecondNumber} = {result}");
            }
            else
            {
                Console.WriteLine("Cannot Divide By Zero");
            }
            }
        }
    }
