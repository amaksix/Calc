using System;
using System.Collections.Generic;
using System.Linq;

namespace Task5
{
    delegate decimal NextStep(string input);

    public class Calc
    {
        Dictionary<char, Func<decimal, decimal, decimal>> _op = new Dictionary<char, Func<decimal, decimal, decimal>>();

        public Calc()
        {
            _op.Add('+', (a, b) => a + b);
            _op.Add('-', (a, b) => a - b);
            _op.Add('*', (a, b) => a * b);
            _op.Add('/', Divide); //zero exception
        }

        public decimal Calculate(string input)
        {
            return CountBrackets(input);
        }

        public string[] Calculate(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                try
                {
                    decimal temp = CountBrackets(input[i]);
                    input[i] += "  =  " + temp.ToString();
                }
                catch (Exception e)
                {
                    input[i] += "  =  " + e.Message;
                }
            }
            return input;
        }

        private decimal CountBrackets(string input)
        {
            List<int> leftBracketsID = new List<int>();
            List<int> rightBracketsID = new List<int>();
            GetBracketsID(input, ref leftBracketsID, ref rightBracketsID);

            if (leftBracketsID.Count != rightBracketsID.Count)
            {
                throw new Exception("Brackets ammount are not equal");
            }

            if (leftBracketsID.Count == 0)
            {
                return CountPluses(input);
            }

            for (int i = leftBracketsID.Count - 1; i > -1; i--)
            {
                for (int j = 0; j < rightBracketsID.Count; j++)
                {
                    if (leftBracketsID[i] < rightBracketsID[j])
                    {
                        string substring = input.Substring(leftBracketsID[i] + 1, rightBracketsID[j] - leftBracketsID[i] - 1);
                        input = input.Remove(leftBracketsID[i], rightBracketsID[j] - leftBracketsID[i] + 1);
                        decimal partResult = CountPluses(substring);
                        input = input.Insert(leftBracketsID[i], partResult.ToString());
                        GetBracketsID(input, ref leftBracketsID, ref rightBracketsID);
                        j = rightBracketsID.Count;
                    }
                }
            }
            return CountPluses(input);
        }

        private void GetBracketsID(string input, ref List<int> leftBracketsID, ref List<int> rightBracketsID)
        {
            leftBracketsID.Clear();
            rightBracketsID.Clear();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    leftBracketsID.Add(i);
                }
                if (input[i] == ')')
                {
                    rightBracketsID.Add(i);
                }
            }
        }

        private decimal CountPluses(string input)
        {
            decimal result = 0;
            var splited = input.Split('+');
            result = Count('+', splited, CountMinuses);
            return result;
        }

        private decimal CountMinuses(string input)
        {
            decimal result = 0;
            var splited = input.Split('-');
            if (splited[0].Length < 1)
            {
                splited[1] = '-' + splited[1];
                int indexToRemove = 0;
                splited = splited.Where((source, index) => index != indexToRemove).ToArray();
            }
            for (int i = 0; i < splited.Length; i++)
            {
                if ((splited[i][splited[i].Length - 1] == '*') || (splited[i][splited[i].Length - 1] == '/'))
                {
                    splited[i + 1] = splited[i] + '-' + splited[i + 1];
                    int indexToRemove = i;
                    splited = splited.Where((source, index) => index != indexToRemove).ToArray();
                    i--;
                }
            }

            result = Count('-', splited, CountMultiplies);
            return result;
        }

        private decimal CountMultiplies(string input)
        {
            decimal result = 0;
            var splited = input.Split('*');
            result = Count('*', splited, CountDividers);
            return result;
        }

        private decimal CountDividers(string input)
        {
            decimal result = 0;
            var splited = input.Split('/');
            result = Count('/', splited, Parsing);
            return result;
        }

        private decimal Parsing(string input)
        {
            decimal result;
            bool succes = decimal.TryParse(input, out result);
            if (!succes)
            {
                throw new Exception("Invalid data");
            }
            return result;
        }

        private decimal Count(char sign, string[] splited, NextStep nextStep)
        {
            decimal result = 0;
            if (splited.Length == 1)
                return nextStep(splited[0]);

            for (int i = 0; i < splited.Length; i++)
            {
                decimal var1, var2;
                if (i == 0)
                {
                    var1 = nextStep(splited[i]);
                    var2 = nextStep(splited[i + 1]);
                    result = _op[sign](var1, var2);
                    i++;
                }
                else
                {
                    var1 = nextStep(splited[i]);
                    result = _op[sign](result, var1);
                }
            }
            return result;
        }

        private decimal Divide(decimal a, decimal b)
        {
            if (b == 0)
            {
                throw new Exception("Dividing by zero is not allowed");
            }

            return a / b;
        }

    }
}

