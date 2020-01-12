using System;

namespace Project_4_Trung_Nguyen
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to RPN calculator");
            printMenu();
        }

        static public int DisplayMenu()
        {
            Console.WriteLine("1. Simple calculator");
            Console.WriteLine("2. Postfix notation calculator");
            Console.WriteLine("3. Convert from infix to postfix notation ");
            Console.WriteLine("4. Exit");
            Console.Write("Please select an option: ");
            int result = int.Parse(Console.ReadLine());            
            return result;
        }
        static void printMenu()
        {
           
            
            string expressionInput;
            bool resume = true;
            while (resume)
            {

                int userInput = 0;
                userInput = DisplayMenu();
                
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine("This option will available soon\n");
                        break;
                    case 2:
                        Console.Write("Please enter postfix notation expression(ex: 2 3 6 * -): ");
                        expressionInput = Console.ReadLine();
                        ExceptionPrintOut(expressionInput);
                        Console.WriteLine("Result: {0}\n",PostfixCalculator(expressionInput));
                        break;
                    case 3:
                        Console.Write("please enter an infix notation expression(exp: 2 * 3 - 6): ");
                        expressionInput = Console.ReadLine();
                        ExceptionPrintOut(expressionInput);
                        Console.WriteLine(InfixToPostfix(expressionInput));
                        break;
                    case 4:
                        resume = false;                        
                        break;
                    default:
                        break;
                }
            }

        }

        static void ExceptionPrintOut(string operation)
        {
            try
            {
                ValidOperationInput(operation);
                
            }
            catch (InvalidInputException ex)
            {

                Console.WriteLine(ex.Message);
            }
            
        }

        static void ValidOperationInput(string Operation)
        {
            int numCount = 0;
            int opCount = 0;
            double num;
            if (string.IsNullOrEmpty(Operation))
            {
                throw new InvalidInputException("No input given");
            }
            foreach (var item in Operation.Split(' '))
            {
                if (!string.IsNullOrEmpty(Operation)&&double.TryParse(item, out num))
                {
                    numCount += 1;
                }
                else
                {
                    opCount += 1;
                }
            }

            if (opCount == numCount)
            {
                throw new InvalidInputException("Too many operators");
            }
            else if (opCount < numCount - 1)
            {
                throw new InvalidInputException("Not enough operator");
            }            
            
        }

        static double PostfixCalculator(string oprationInput)
        {
            Stack<double> numStack = new Stack<double>(10);
            double num;
            foreach (var item in oprationInput.Split(' '))
            {
                if (double.TryParse(item,out num))
                {
                    numStack.push(num);
                }
                else
                {
                    //2-3*(7+5) ->2 3 7 5 - * +
                    //according to the func below: (5-7)*3+2
                    double num1 = numStack.pop();
                    double num2 = numStack.pop();
                    double result = Calculate(num1, num2, item);
                    numStack.push(result);
                }
            }
            return numStack.pop();
        }

        static int Precedence(string opInput)
        {
            if (opInput=="^")
            {
                return 4;
            }
            else if (opInput=="*" || opInput=="/")
            {
                return 3;
            }
            else
            {
                return 2;
            }
        }

        static string Associativity(string opInput)
        {
            if (opInput=="^")
            {
                return "Right";
            }
            return "Left";
        }

        static string InfixToPostfix(string operation)
        {
            string postfixOutput = "";
            Stack<string> operatorStack = new Stack<string>(10);
            double num;
            foreach (var item in operation.Split(' '))
            {
                if (double.TryParse(item, out num))
                {
                    if (postfixOutput=="")
                    {
                        postfixOutput = item;
                    }
                    else
                    {
                        postfixOutput += " " + item;
                    }
                    
                   
                }
                else if (item == "(")
                {
                    operatorStack.push(item);
                }
                else if (item == ")")
                {
                    while (!operatorStack.isEmpty() && operatorStack.Peek() != "(")
                    {
                        postfixOutput += " " + operatorStack.pop();
                    }
                }
                else
                {
                    while(!operatorStack.isEmpty() && operatorStack.Peek() != "(" && (Precedence(operatorStack.Peek())>Precedence(item) || Precedence(operatorStack.Peek())==Precedence(item) &&Associativity(item)=="left"))
                    {
                        postfixOutput += " " + operatorStack.pop();
                    }
                    operatorStack.push(item);
                }
            }
            while (!operatorStack.isEmpty())
            {
                postfixOutput += " " + operatorStack.pop();
            }
            return postfixOutput;
        }


        static double Calculate(double num1, double num2, string operatorInput)
        {
            if (operatorInput == "+")
            {
                return num1 + num2;
            }
            else if (operatorInput == "-")
            {
                return num1 - num2;
            }
            else if (operatorInput == "*")
            {
                return num1 * num2;
            }
            else if (operatorInput == "/")
            {
                return num1 / num2;
            }
            else if (operatorInput == "^")
            {
                return Math.Pow(num1, num2);
            }
            else
            {
                return 0;
            }
        }


    }
}
