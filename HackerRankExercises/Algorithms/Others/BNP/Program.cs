using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;



class Result
{

    /*
     * Complete the 'budgetShopping' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER_ARRAY bundleQuantities
     *  3. INTEGER_ARRAY bundleCosts
     */

    public static int budgetShopping1(int budget, List<int> bundleQuantities, List<int> bundleCosts)
    {
        int numberOfBundles = bundleQuantities.Count;
        int[,] dp = new int[numberOfBundles + 1, budget + 1];

        for (int i = 1; i <= numberOfBundles; i++)
        {
            int cost = bundleCosts[i - 1];
            int quantity = bundleQuantities[i - 1];

            for (int j = 1; j <= budget; j++)
            {
                if (j >= cost)
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - cost] + quantity);
                }
                else
                {
                    dp[i, j] = dp[i - 1, j];
                }
            }
        }

        return dp[numberOfBundles, budget];
    }

    public static int budgetShopping2(int budget, List<int> bundleQuantities, List<int> bundleCosts)
    {
        int maxNotebooks = 0;

        for (int i = 0; i < bundleQuantities.Count; i++)
        {
            int notebooks = 0;
            int remainingBudget = budget;

            while (remainingBudget >= bundleCosts[i])
            {
                notebooks += bundleQuantities[i];
                remainingBudget -= bundleCosts[i];
            }

            if (notebooks > maxNotebooks)
            {
                maxNotebooks = notebooks;
            }
        }

        return maxNotebooks;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int budget = Convert.ToInt32(Console.ReadLine().Trim());

        int bundleQuantitiesCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> bundleQuantities = new List<int>();

        for (int i = 0; i < bundleQuantitiesCount; i++)
        {
            int bundleQuantitiesItem = Convert.ToInt32(Console.ReadLine().Trim());
            bundleQuantities.Add(bundleQuantitiesItem);
        }

        int bundleCostsCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> bundleCosts = new List<int>();

        for (int i = 0; i < bundleCostsCount; i++)
        {
            int bundleCostsItem = Convert.ToInt32(Console.ReadLine().Trim());
            bundleCosts.Add(bundleCostsItem);
        }

        budget = 50;
        int[] bundleQuantitiesArray1 = { 20, 19 }; //The number of notebooks in each bundle
        int[] bundleCostsArray1 = { 24, 20 }; // the cost of each bundle
        bundleQuantities = bundleQuantitiesArray1.ToList();
        bundleCosts = bundleCostsArray1.ToList();
        int result = Result.budgetShopping2(budget, bundleQuantities, bundleCosts);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}