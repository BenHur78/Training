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
using System.Diagnostics;

Solution.Main(null);

public class Solution
{
    public static void Main(string[] args)
    {
        bool debugging = true;
        SampleInput sampleInput = new SampleInput();
        sampleInput.n = Sample1.n;
        sampleInput.genes = Sample1.genes;
        sampleInput.health = Sample1.health;
        sampleInput.s = Sample1.s;
        sampleInput.dnaStrandList = Sample1.dnaStrandList;
        sampleInput.assertUnhealthiestStrandsOfDNA = Sample1.assertUnhealthiestStrandsOfDNA;
        sampleInput.assertHealthiestStrandsOfDNA = Sample1.assertHealthiestStrandsOfDNA;

        SampleInput problemDefinition = new SampleInput();

        int n = debugging ? sampleInput.n : Convert.ToInt32(Console.ReadLine().Trim());
        problemDefinition.n = n;

        string genesString = "";
        List<string> genes = debugging ? sampleInput.genes : Console.ReadLine().TrimEnd().Split(' ').ToList();
        problemDefinition.genes = genes;

        List<int> health = debugging ? sampleInput.health : Console.ReadLine().TrimEnd().Split(' ').ToList().Select(healthTemp => Convert.ToInt32(healthTemp)).ToList();
        problemDefinition.health = health;

        int s = debugging ? sampleInput.s : Convert.ToInt32(Console.ReadLine().Trim());
        problemDefinition.s = s;

        List<DNAStrand> dnaStrandList = new();

        if (!debugging)
        {
            for (int sItr = 0; sItr < s; sItr++)
            {
                DNAStrand dnaStrand = new();

                string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

                int first = Convert.ToInt32(firstMultipleInput[0]);
                dnaStrand.first = first;

                int last = Convert.ToInt32(firstMultipleInput[1]);
                dnaStrand.last = last;

                string d = firstMultipleInput[2];
                dnaStrand.d = d;

                dnaStrandList.Add(dnaStrand);
            }
        }
        else
        {
            dnaStrandList = sampleInput.dnaStrandList;
        }

        problemDefinition.dnaStrandList = dnaStrandList;

        var output = calculate(sampleInput);

        Debug.Assert(output.healthiestStrandsOfDNA == sampleInput.assertHealthiestStrandsOfDNA, $"healthiestStrandsOfDNA: expected {sampleInput.assertHealthiestStrandsOfDNA}; value is {output.healthiestStrandsOfDNA}");
        Debug.Assert(output.unhealthiestStrandsOfDNA == sampleInput.assertUnhealthiestStrandsOfDNA, $"unhealthiestStrandsOfDNA: expected {sampleInput.assertUnhealthiestStrandsOfDNA}; value is {output.unhealthiestStrandsOfDNA}");

        Console.WriteLine($"{output.unhealthiestStrandsOfDNA} {output.healthiestStrandsOfDNA}");
    }

    public static Output calculate(SampleInput sampleInput)
    {
        Output output = new ();
        output.healthiestStrandsOfDNA = 19;
        output.unhealthiestStrandsOfDNA = 0;

        return output;
    }
}

public class DNAStrand
{
    public int first;
    public int last;
    public string d;
}

public class Output
{
    public int unhealthiestStrandsOfDNA;
    public int healthiestStrandsOfDNA;
}

public class SampleInput
{
    public int n;
    public List<string> genes;
    public List<int> health;
    public int s;

    public List<DNAStrand> dnaStrandList;

    public int assertUnhealthiestStrandsOfDNA;
    public int assertHealthiestStrandsOfDNA;
}

public class Sample1
{
    public static readonly int n = 6;
    public static readonly List<string> genes = new() {"a", "b", "c", "aa", "d", "b"};
    public static readonly List<int> health = new() {1, 2, 3, 4, 5, 6};
    public static readonly int s = 3;

    public static readonly List<DNAStrand> dnaStrandList = new()
    {
        new DNAStrand() {first = 1, last = 5, d = "caaab"},
        new DNAStrand() {first = 0, last = 4, d = "xyz"},
        new DNAStrand() {first = 2, last = 4, d = "bcdybc"}
    };

    public static readonly int assertUnhealthiestStrandsOfDNA = 0;
    public static readonly int assertHealthiestStrandsOfDNA = 20;
}