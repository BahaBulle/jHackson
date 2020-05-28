// <copyright file="ITableElementParam.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.TableElements
{
    public interface ITableElementParam
    {
        int NbBytes { get; set; }

        int Position { get; set; }

        string Value { get; set; }
    }
}