// <copyright file="Sdd1Helper.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.SDD1Algorithm.Common
{
    using System.Collections.Generic;

    public static class Sdd1Helper
    {
        public static List<State> GetEvolutionTable()
        {
            return new List<State>()
            {
                new State { CodeNum = 0, NextIfMPS = 25, NextIfLPS = 25 },
                new State { CodeNum = 0, NextIfMPS = 2, NextIfLPS = 1 },
                new State { CodeNum = 0, NextIfMPS = 3, NextIfLPS = 1 },
                new State { CodeNum = 0, NextIfMPS = 4, NextIfLPS = 2 },
                new State { CodeNum = 0, NextIfMPS = 5, NextIfLPS = 3 },
                new State { CodeNum = 1, NextIfMPS = 6, NextIfLPS = 4 },
                new State { CodeNum = 1, NextIfMPS = 7, NextIfLPS = 5 },
                new State { CodeNum = 1, NextIfMPS = 8, NextIfLPS = 6 },
                new State { CodeNum = 1, NextIfMPS = 9, NextIfLPS = 7 },
                new State { CodeNum = 2, NextIfMPS = 10, NextIfLPS = 8 },
                new State { CodeNum = 2, NextIfMPS = 11, NextIfLPS = 9 },
                new State { CodeNum = 2, NextIfMPS = 12, NextIfLPS = 10 },
                new State { CodeNum = 2, NextIfMPS = 13, NextIfLPS = 11 },
                new State { CodeNum = 3, NextIfMPS = 14, NextIfLPS = 12 },
                new State { CodeNum = 3, NextIfMPS = 15, NextIfLPS = 13 },
                new State { CodeNum = 3, NextIfMPS = 16, NextIfLPS = 14 },
                new State { CodeNum = 3, NextIfMPS = 17, NextIfLPS = 15 },
                new State { CodeNum = 4, NextIfMPS = 18, NextIfLPS = 16 },
                new State { CodeNum = 4, NextIfMPS = 19, NextIfLPS = 17 },
                new State { CodeNum = 5, NextIfMPS = 20, NextIfLPS = 18 },
                new State { CodeNum = 5, NextIfMPS = 21, NextIfLPS = 19 },
                new State { CodeNum = 6, NextIfMPS = 22, NextIfLPS = 20 },
                new State { CodeNum = 6, NextIfMPS = 23, NextIfLPS = 21 },
                new State { CodeNum = 7, NextIfMPS = 24, NextIfLPS = 22 },
                new State { CodeNum = 7, NextIfMPS = 24, NextIfLPS = 23 },
                new State { CodeNum = 0, NextIfMPS = 26, NextIfLPS = 1 },
                new State { CodeNum = 1, NextIfMPS = 27, NextIfLPS = 2 },
                new State { CodeNum = 2, NextIfMPS = 28, NextIfLPS = 4 },
                new State { CodeNum = 3, NextIfMPS = 29, NextIfLPS = 8 },
                new State { CodeNum = 4, NextIfMPS = 30, NextIfLPS = 12 },
                new State { CodeNum = 5, NextIfMPS = 31, NextIfLPS = 16 },
                new State { CodeNum = 6, NextIfMPS = 32, NextIfLPS = 18 },
                new State { CodeNum = 7, NextIfMPS = 24, NextIfLPS = 22 },
            };
        }
    }
}