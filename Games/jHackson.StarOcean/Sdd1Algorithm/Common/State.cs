// <copyright file="State.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.SDD1Algorithm.Common
{
    public class State
    {
        public byte CodeNum { get; set; }

        public byte NextIfLPS { get; set; }

        public byte NextIfMPS { get; set; }
    }
}