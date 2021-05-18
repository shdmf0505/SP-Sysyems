﻿using System;
using System.Collections.Generic;

namespace Micube.SmartMES.Commons.Gerber.Core.Primitives
{
    public class GCodeCommand
    {
        public int errors = 0;
        public string originalline = string.Empty;

        public List<char> charcommands = new List<char>();
        public List<double> numbercommands = new List<double>();

        public void Decode(string D, GerberNumberFormat GNF)
        {
            originalline = D;
            int idx = 0;
            string numberstring = "";

            while (idx < D.Length)
            {
                if (char.IsLetter(D[idx]))
                {
                    if (numberstring.Length > 0)
                    {
                        string[] r = numberstring.Split(',');

                        try
                        {
                            for (int i = 0; i < r.Length; i++)
                            {
                                if (Gerber.TryParseDouble(r[i], out double N))
                                {
                                    numbercommands.Add(N);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("failed to parse {0}", numberstring);
                            errors++;
                        }

                        numberstring = "";
                    }

                    charcommands.Add(D[idx]);
                }
                else
                {
                    if (char.IsNumber(D[idx]) || D[idx] == '-' || D[idx] == '.' || D[idx] == ',')
                    {
                        numberstring += D[idx];
                    }
                    else
                    {
                        charcommands.Add(D[idx]);
                    }
                }

                idx++;
            }
            if (numberstring.Length > 0)
            {
                string[] r = numberstring.Split(',');

                try
                {
                    for (int i = 0; i < r.Length; i++)
                    {   
                        if (Gerber.TryParseDouble(r[i], out double N))
                        {
                            numbercommands.Add(N);
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("failed to parse {0}", numberstring);
                    errors++;
                }

            }
        }

        public void Print()
        {
            Console.Write("chars: ");
            for (int i = 0; i < charcommands.Count; i++)
            {
                Console.Write(charcommands[i].ToString() + " ");
            }
            Console.WriteLine("");
            Console.Write("numbers: ");
            for (int i = 0; i < numbercommands.Count; i++)
            {
                Console.Write(numbercommands[i].ToString() + " ");
            }
            Console.WriteLine("");
        }

        internal double GetNumber(char p)
        {
            for (int i = 0; i < charcommands.Count; i++)
            {
                if (charcommands[i] == p)
                {
                    return numbercommands[i];
                }
            }

            return 0;
        }
    };
}
