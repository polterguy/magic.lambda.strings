﻿/*
 * Magic, Copyright(c) Thomas Hansen 2019 - thomas@gaiasoul.com
 * Licensed as Affero GPL unless an explicitly proprietary license has been obtained.
 */

using System;
using System.Linq;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;

namespace magic.lambda.strings
{
    [Slot(Name = "contains")]
    public class Contains : ISlot
    {
        public void Signal(ISignaler signaler, Node input)
        {
            if (input.Children.Count() != 1)
                throw new ApplicationException("[contains] must be given exactly one argument that contains value to look for");

            signaler.Signal("eval", input);

            input.Value = input.GetEx<string>().Contains(input.Children.First().GetEx<string>());
        }
    }
}
