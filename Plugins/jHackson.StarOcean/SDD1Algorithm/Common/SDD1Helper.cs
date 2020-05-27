using System.Collections.Generic;

namespace jHackson.StarOcean.SDD1Algorithm.Common
{
    public static class SDD1Helper
    {
        public static List<State> GetEvolutionTable()
        {
            return new List<State>() {
                new State{ code_num = 0, nextIfMPS = 25, nextIfLPS = 25},
                new State{ code_num = 0, nextIfMPS =  2, nextIfLPS =  1},
                new State{ code_num = 0, nextIfMPS =  3, nextIfLPS =  1},
                new State{ code_num = 0, nextIfMPS =  4, nextIfLPS =  2},
                new State{ code_num = 0, nextIfMPS =  5, nextIfLPS =  3},
                new State{ code_num = 1, nextIfMPS =  6, nextIfLPS =  4},
                new State{ code_num = 1, nextIfMPS =  7, nextIfLPS =  5},
                new State{ code_num = 1, nextIfMPS =  8, nextIfLPS =  6},
                new State{ code_num = 1, nextIfMPS =  9, nextIfLPS =  7},
                new State{ code_num = 2, nextIfMPS = 10, nextIfLPS =  8},
                new State{ code_num = 2, nextIfMPS = 11, nextIfLPS =  9},
                new State{ code_num = 2, nextIfMPS = 12, nextIfLPS = 10},
                new State{ code_num = 2, nextIfMPS = 13, nextIfLPS = 11},
                new State{ code_num = 3, nextIfMPS = 14, nextIfLPS = 12},
                new State{ code_num = 3, nextIfMPS = 15, nextIfLPS = 13},
                new State{ code_num = 3, nextIfMPS = 16, nextIfLPS = 14},
                new State{ code_num = 3, nextIfMPS = 17, nextIfLPS = 15},
                new State{ code_num = 4, nextIfMPS = 18, nextIfLPS = 16},
                new State{ code_num = 4, nextIfMPS = 19, nextIfLPS = 17},
                new State{ code_num = 5, nextIfMPS = 20, nextIfLPS = 18},
                new State{ code_num = 5, nextIfMPS = 21, nextIfLPS = 19},
                new State{ code_num = 6, nextIfMPS = 22, nextIfLPS = 20},
                new State{ code_num = 6, nextIfMPS = 23, nextIfLPS = 21},
                new State{ code_num = 7, nextIfMPS = 24, nextIfLPS = 22},
                new State{ code_num = 7, nextIfMPS = 24, nextIfLPS = 23},
                new State{ code_num = 0, nextIfMPS = 26, nextIfLPS =  1},
                new State{ code_num = 1, nextIfMPS = 27, nextIfLPS =  2},
                new State{ code_num = 2, nextIfMPS = 28, nextIfLPS =  4},
                new State{ code_num = 3, nextIfMPS = 29, nextIfLPS =  8},
                new State{ code_num = 4, nextIfMPS = 30, nextIfLPS = 12},
                new State{ code_num = 5, nextIfMPS = 31, nextIfLPS = 16},
                new State{ code_num = 6, nextIfMPS = 32, nextIfLPS = 18},
                new State{ code_num = 7, nextIfMPS = 24, nextIfLPS = 22}
            };
        }
    }
}