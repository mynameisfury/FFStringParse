using System;
using System.Collections.Generic;
using System.Text;

namespace StringParse
{
    class ParseFunctions
    {

        //todo: write better comments
         public static List<Transaction> ParseTransaction(string csv)
        {
            var csvArray = csv.Split("\n");
            var transactionList = new List<Transaction>();
            DateTime startDate = DateTime.Parse("2/1/2018");
            //will prolly want to split this off into its own function, for readibility/modularity
            for (var i = 0; i <csvArray.Length; i++)
            {
                var splitArray = csvArray[i].Split(",");
                Transaction transaction = new Transaction();
                string name = splitArray[0];
                var splitNames = name.Trim().Split(" ");
                transaction.FirstName = splitNames[0].ToUpper();
                //transaction.MiddleName = splitNames[1];
                transaction.LastName = splitNames[splitNames.Length - 1].ToUpper();
                transaction.TransactionNumber = int.Parse(splitArray[1]);
                var transactionAmount = splitArray[2];
                if (splitArray[3] != string.Empty)
                {
                    transaction.TransactionDate = startDate.AddDays(-int.Parse(splitArray[3]));

                }
                else
                {
                    transaction.TransactionDate = DateTime.Today;
                    //should actually be startdate
                }

                if (transactionAmount.Contains("("))
                {
                    //transactionAmount.Replace("(", " ");
                    //transactionAmount.Replace(")", string.Empty); this is literally the worst solution ive ever come up witht this is so ugly i hate it
                    var amountArray = transactionAmount.Split('(', ')');
                    var realAmount = amountArray[1].ToString();
                    var newAmount = float.Parse(realAmount);
                    transaction.TransactionAmount = -newAmount;

                }
                else
                {
                    transaction.TransactionAmount = float.Parse(splitArray[2]);

                }

                transactionList.Add(transaction);
            }
            return transactionList;
            //this function splits the array by line divider, and maps that split array to the base object values

        }



        public static string GetTransactionTotals(string csv)
        {
            //grabs total value of all transactions provided
            var transactions = ParseTransaction(csv);
            float total = 0;
            foreach (var transaction in transactions)
            {
                total += transaction.TransactionAmount;
            }
            if (total < 0)
            {
                string value = Math.Abs(total).ToString();
                return "(" + value + ")";
            }
            else
            {
                return total.ToString();
            }
        }


        public static List<Transaction> GetTransactionsStringFiltered(string csv, string filter)
        {
            //filters by a xgiven character set. could probably be improved by a broader search funtionality
            var transactions = ParseTransaction(csv);
            var returnedTransactions = new List<Transaction>();
            foreach (var transaction in transactions)
            {
                // (transaction.MiddleName.Contains(filter) || do things with middle names once we figure that out
                if (transaction.FirstName.Contains(filter.ToUpper()) ||  transaction.LastName.Contains(filter.ToUpper()))
                {
                    returnedTransactions.Add(transaction);
                }
            }
            return returnedTransactions;
        }

        public static List<Transaction> GetTransactionsDateFiltered(string csv, string filter)
        {
            //filters by a given date, returning everything before and including the given date to filter by
            var filterDate = DateTime.Parse(filter);
            var transactions = ParseTransaction(csv);
            var returnedTransactions = new List<Transaction>();
            foreach (var transaction in transactions)
            {
                if (transaction.TransactionDate <= filterDate)
                {
                    returnedTransactions.Add(transaction);
                }
            }
            return returnedTransactions;
        }

    }
}
