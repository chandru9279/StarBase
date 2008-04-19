using System;
using System.Collections.Generic;
using System.Text;

namespace DIComplete
{
    enum TransactionType
    {
        Invalid,
        Credit,
        Debit,
        Int,
        Cmt,
        Rate
    }

    public class InputTransaction : IComparable<InputTransaction>
    {
        public InputTransaction()
        {
        }
        internal TransactionType type;
        internal DateTime date;
        internal double amount;
        public string Type
        {
            get
            {
                switch (type)
                {
                    case TransactionType.Cmt:
                        return "Cmt";

                    case TransactionType.Credit:
                        return "Credit";

                    case TransactionType.Debit:
                        return "Debit";

                    case TransactionType.Int:
                        return "Int";

                    case TransactionType.Invalid:
                        return "Invalid";

                    case TransactionType.Rate:
                        return "Rate";
                }
                return "Invalid";
            }
            set
            {
                switch (value)
                {
                    case "Cmt":
                        type = TransactionType.Cmt;
                        break;
                    case "Credit":
                        type = TransactionType.Credit;
                        break;
                    case "Debit":
                        type = TransactionType.Debit;
                        break;
                    case "Int":
                        type = TransactionType.Int;
                        break;
                    case "Invalid":
                        type = TransactionType.Invalid;
                        break;
                    case "Rate":
                        type = TransactionType.Rate;
                        break;
                    default:
                        type = TransactionType.Invalid;
                        break;
                }
            }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public int CompareTo(InputTransaction transaction)
        {
            return date.CompareTo(transaction.date);
        }
    }

    public struct Input
    {
        internal double principal;
        internal double startRate;
        internal DateTime startDate;
        internal DateTime endDate;
        internal List<InputTransaction> transactions;
        public double Principal
        {
            get { return principal; }
            set { principal = value; }
        }
        public double StartRate
        {
            get { return startRate; }
            set { startRate = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
    }

    struct OutputTransaction
    {
        internal TransactionType type;
        internal DateTime date;
        internal double initialPrincipal;
        internal double balancePrincipal;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public string Type
        {            
            get 
            {
                switch (type)
                {
                    case TransactionType.Cmt:
                        return "Cmt";                        

                    case TransactionType.Credit:
                        return "Credit";
                        
                    case TransactionType.Debit:
                        return "Debit";
                        
                    case TransactionType.Int:
                        return "Int";
                       
                    case TransactionType.Invalid:
                        return "Invalid";
                        
                    case TransactionType.Rate:
                        return "Rate";                        
                }
                return "Invalid";
            }
            set { type = TransactionType.Invalid;}
        }
        public double InitialPrincipal
        {
            get { return initialPrincipal; }
            set { initialPrincipal = value; }
        }
        public double BalancePrincipal
        {
            get { return balancePrincipal; }
            set { balancePrincipal = value; }
        }
    }

    class InterimInterest
    {
        internal DateTime startDate;
        internal DateTime endDate;
        internal double principal;
        internal double rate;
        internal double amount;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public double Principal
        {
            get { return principal; }
            set { principal = value; }
        }
        public double Rate
        {
            get { return rate; }
            set { rate = value; }
        }
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }

    struct Output
    {
        internal double balancePrincipal;
        internal List<OutputTransaction> transactions;
        internal List<InterimInterest> interimInterests;
    }

    class MainProgram
    {
        const int InterestFactor = 365;
        const int CommitmentInterest = 2;
        /*
        static InputTransaction GetInputTransaction()
        {
            InputTransaction transaction = new InputTransaction();

            Console.Write("Enter transaction type (Credit - C, Debit - D, Rate - R, Done - <Enter>) : ");
            string type = Console.ReadLine();
            if (string.Compare(type, "C", true) == 0)
            {
                transaction.type = TransactionType.Credit;
            }
            else if (string.Compare(type, "D", true) == 0)
            {
                transaction.type = TransactionType.Debit;
            }
            else if (string.Compare(type, "R", true) == 0)
            {
                transaction.type = TransactionType.Rate;
            }
            else
            {
                transaction.type = TransactionType.Invalid;
                return transaction;
            }

            if (transaction.type == TransactionType.Rate)
            {
                Console.Write("Enter new daily interest rate (%) : ");
                transaction.amount = double.Parse(Console.ReadLine());

                Console.Write("Enter rate change date (d/m/yyyy) : ");
                transaction.date = DateTime.Parse(Console.ReadLine());
            }
            else
            {
                Console.Write("Enter transaction amount (Rs.) : ");
                transaction.amount = double.Parse(Console.ReadLine());

                Console.Write("Enter transaction date (d/m/yyyy) : ");
                transaction.date = DateTime.Parse(Console.ReadLine());
            }

            Console.WriteLine();

            return transaction;
        }
        */

        /*static Input GetInput()
        {
            Input input = new Input();

            Console.Write("Enter principal (Rs.) : ");
            input.principal = double.Parse(Console.ReadLine());

            Console.Write("Enter initial daily interest rate (%) : ");
            input.startRate = double.Parse(Console.ReadLine());

            Console.Write("Enter start date (d/m/yyyy) : ");
            input.startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter end date (d/m/yyyy) : ");
            input.endDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Enter transaction details:");
            Console.WriteLine();

            input.transactions = new List<InputTransaction>();
            bool isDone = false;
            while (!isDone)
            {
                InputTransaction transaction = GetInputTransaction();
                if (transaction.type != TransactionType.Invalid)
                {
                    input.transactions.Add(transaction);
                }
                else
                {
                    isDone = true;
                }
            }

            return input;
        }*/

        static double CalculateInterest(double principal,
                                        double rate,
                                        double days)
        {
            return ((principal * rate * days) / (100 * InterestFactor));
        }

        static InterimInterest ComputeInterimInterest(double principal,
                                                 double rate,
                                                 DateTime startDate,
                                                 DateTime endDate)
        {
            InterimInterest interest = new InterimInterest();
            interest.principal = principal;
            interest.rate = rate;
            interest.startDate = startDate;
            interest.endDate = endDate;
            interest.amount = CalculateInterest(principal, rate,
                                                (endDate - startDate).Days + 1);

            return interest;
        }

        static DateTime GetFirstDayOfMonth(DateTime date)
        {
            DateTime firstDate = date;
            firstDate = firstDate.AddDays(-(firstDate.Day - 1));
            return firstDate;
        }

        static DateTime GetLastDayOfMonth(DateTime date)
        {
            DateTime lastDate = date;
            lastDate = lastDate.AddMonths(1);
            lastDate = lastDate.AddDays(-(lastDate.Day));
            return lastDate;
        }

        public static bool IsLastDayOfMonth(DateTime date)
        {
            return (date == GetLastDayOfMonth(date));
        }

        static List<InterimInterest> ComputeMonthlyInterimInterests(
            double principal, double rate, DateTime startDate, DateTime endDate)
        {
            List<InterimInterest> interests = new List<InterimInterest>();
            DateTime currentStartDate = startDate;
            DateTime currentEndDate = GetLastDayOfMonth(startDate);
            double currentPrincipal = principal;

            while (currentEndDate < endDate)
            {
                interests.Add(ComputeInterimInterest(
                    currentPrincipal, rate, currentStartDate, currentEndDate));

                currentStartDate = currentEndDate.AddDays(1);
                currentEndDate = GetLastDayOfMonth(currentStartDate);
                currentPrincipal += interests[interests.Count - 1].amount;
            }

            interests.Add(ComputeInterimInterest(
                currentPrincipal, rate, currentStartDate, endDate));

            return interests;
        }

        public static bool IsSameMonthYear(DateTime date1, DateTime date2)
        {
            return (date1.Month == date2.Month &&
                    date1.Year == date2.Year);
        }

        static OutputTransaction GetCommitmentInterest(double principal, DateTime date)
        {
            OutputTransaction transaction = new OutputTransaction();
            transaction.type = TransactionType.Cmt;
            transaction.date = date;
            transaction.initialPrincipal = principal;
            transaction.balancePrincipal = principal + CommitmentInterest;
            return transaction;
        }

        static void RoundOffInterest(ref OutputTransaction transaction)
        {
            double amount = transaction.balancePrincipal - transaction.initialPrincipal;
            amount = Math.Round(amount);
            transaction.balancePrincipal = transaction.initialPrincipal + amount;
        }

        static List<OutputTransaction> ComputeMonthlyInterests(
            double principal, DateTime stopDate,
            List<InterimInterest> interests, ref int currentIndex)
        {
            DateTime currentDate = interests[currentIndex].endDate;
            List<OutputTransaction> transactions = new List<OutputTransaction>();
            OutputTransaction transaction = new OutputTransaction();
            double currentPrincipal = principal;

            while (!IsSameMonthYear(currentDate, stopDate) &&
                   currentIndex < interests.Count)
            {
                if (transaction.type == TransactionType.Invalid)
                {
                    transaction.type = TransactionType.Int;
                    transaction.date = currentDate;
                    transaction.initialPrincipal = currentPrincipal;
                    transaction.balancePrincipal = currentPrincipal;
                }

                currentDate = interests[currentIndex].endDate;
                transaction.balancePrincipal += interests[currentIndex++].amount;

                if (IsLastDayOfMonth(currentDate))
                {
                    transaction.date = GetLastDayOfMonth(currentDate);
                    RoundOffInterest(ref transaction);
                    transactions.Add(transaction);
                    currentPrincipal = transaction.balancePrincipal;

                    transactions.Add(GetCommitmentInterest(currentPrincipal, currentDate));
                    currentPrincipal = transactions[transactions.Count - 1].balancePrincipal;

                    transaction.type = TransactionType.Invalid;
                }

                if (currentIndex < interests.Count)
                {
                    currentDate = interests[currentIndex].endDate;
                }
            }
            return transactions;
        }

        static OutputTransaction ComputeTransaction(double principal,
                                                    TransactionType type,
                                                    double amount,
                                                    DateTime date)
        {
            OutputTransaction transaction = new OutputTransaction();
            transaction.type = type;
            transaction.date = date;
            transaction.initialPrincipal = principal;
            transaction.balancePrincipal = principal +
                ((type == TransactionType.Credit) ? -amount : amount);

            return transaction;
        }

        static Output ComputeOutput(Input input)
        {
            Output output = new Output();
            double currentBalance = input.principal;
            DateTime currentDate = input.startDate;
            double currentRate = input.startRate;
            int currentInterestIndex = 0;

            output.transactions = new List<OutputTransaction>();
            output.interimInterests = new List<InterimInterest>();
            foreach (InputTransaction inputTransaction in input.transactions)
            {
                DateTime inputDate = inputTransaction.date.AddDays(-1);

                List<InterimInterest> interests = ComputeMonthlyInterimInterests(
                    currentBalance, currentRate, currentDate, inputDate);
                output.interimInterests.AddRange(interests);

                output.transactions.AddRange(ComputeMonthlyInterests(
                    currentBalance, inputTransaction.date, output.interimInterests,
                    ref currentInterestIndex));

                if (output.transactions.Count > 0)
                {
                    currentBalance =
                        output.transactions[output.transactions.Count - 1].balancePrincipal;
                }

                if (inputTransaction.type == TransactionType.Rate)
                {
                    currentRate = inputTransaction.amount;
                }
                else
                {
                    OutputTransaction outputTransaction = ComputeTransaction(currentBalance,
                        inputTransaction.type, inputTransaction.amount, inputTransaction.date);
                    output.transactions.Add(outputTransaction);
                    currentBalance = outputTransaction.balancePrincipal;
                }

                currentDate = inputTransaction.date;
            }

            List<InterimInterest> endInterests = ComputeMonthlyInterimInterests(
                currentBalance, currentRate, currentDate, input.endDate);
            output.interimInterests.AddRange(endInterests);

            output.transactions.AddRange(ComputeMonthlyInterests(
                currentBalance, input.endDate, output.interimInterests,
                ref currentInterestIndex));

            output.balancePrincipal =
                output.transactions[output.transactions.Count - 1].balancePrincipal;

            return output;
        }
        /*
        static void DisplayOutput(Output output)
        {
            Console.WriteLine();
            Console.WriteLine("Output:");
            Console.WriteLine("~~~~~~~");
            Console.WriteLine();

            Console.WriteLine("Transactions:");
            Console.WriteLine("~~~~~~~~~~~~~");
            Console.WriteLine();
            Console.WriteLine("Date\t\tOpening\t\t\tAmount\t\tType\tClosing");
            Console.WriteLine("~~~~\t\t~~~~~~~\t\t\t~~~~~~\t\t~~~~\t~~~~~~~");

            DateTime previousDate = DateTime.MinValue;
            foreach (OutputTransaction transaction in output.transactions)
            {
                if (previousDate != DateTime.MinValue &&
                    !IsSameMonthYear(previousDate, transaction.date))
                {
                    Console.WriteLine();
                }

                Console.WriteLine("{0:d}\t{1:c}\t{2:c}\t{3}\t{4:c}",
                    transaction.date,
                    transaction.initialPrincipal,
                    transaction.balancePrincipal - transaction.initialPrincipal,
                    transaction.type,
                    transaction.balancePrincipal);

                previousDate = transaction.date;
            }
            Console.WriteLine();
            Console.WriteLine("Balance : {0:c}", output.balancePrincipal);

            Console.WriteLine();
            Console.WriteLine("Interests:");
            Console.WriteLine("~~~~~~~~~~");
            Console.WriteLine();
            Console.WriteLine("Start\t\tEnd\t\tBalance\t\t\tRate\tInterest");
            Console.WriteLine("~~~~~\t\t~~~\t\t~~~~~~~\t\t\t~~~~\t~~~~~~~~");

            foreach (InterimInterest interest in output.interimInterests)
            {
                Console.WriteLine("{0:d}\t{1:d}\t{2:c}\t{3}\t{4:c}",
                    interest.startDate,
                    interest.endDate,
                    interest.principal,
                    interest.rate,
                    interest.amount);

                if (IsLastDayOfMonth(interest.endDate))
                {
                    Console.WriteLine();
                }
            }
        }
        */

       
        /*static void Main(string [] args)*/

        public static Output execMain(Input input)
        {
            //Input input = GetInput();
            input.transactions.Sort();
            Output output = ComputeOutput(input);
            return output;
            //DisplayOutput(output);

            //Console.Read();
        }
        
    }
    
}
