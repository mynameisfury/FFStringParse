using System;

namespace StringParse
{
    class Program
    {
        static void Main(string[] args)
        {
            //parse csv into an object: give it fields:
            //firstname, middlename, lastname, (int) transaction number, (float) transactionamount, (datetime) transaxtiondate

            Console.WriteLine("Hello World!");
            var csv = "greg R hopper,0123654,24.25,255\n  Sam Smith,000126,(24.25),421\n maximus WHITE  ,000025,(12),\n Bill Masters,000526,6.5,11\n Frank   Berg,000527,6.75,1";
            ParseFunctions.ParseTransaction(csv);
            var total = ParseFunctions.GetTransactionTotals(csv);
            ParseFunctions.GetTransactionsStringFiltered(csv, "m");
            ParseFunctions.GetTransactionsDateFiltered(csv, "12/28/2019");
            Console.WriteLine(total);

        }
    }
}
