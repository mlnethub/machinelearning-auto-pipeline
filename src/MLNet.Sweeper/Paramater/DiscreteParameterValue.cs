﻿// <copyright file="StringParameterValue.cs" company="BigMiao">
// Copyright (c) BigMiao. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace MLNet.Sweeper
{
    public sealed class DiscreteParameterValue : IParameterValue
    {
        private readonly string _name;
        private readonly object _value;

        public string Name => this._name;

        public string ValueText => this._value.ToString();

        public object Value => this._value;

        public object RawValue => this._value;

        public DiscreteParameterValue(string name, object value)
        {
            this._name = name;
            this._value = value;
        }

        public bool Equals(IParameterValue other)
        {
            return this.Equals((object)other);
        }

        public override bool Equals(object obj)
        {
            var spv = obj as DiscreteParameterValue;
            return spv != null && this.Name == spv.Name && this.ValueText == spv.ValueText;
        }
    }
}