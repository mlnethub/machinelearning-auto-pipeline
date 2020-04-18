﻿// <copyright file="OptionBuilder.cs" company="BigMiao">
// Copyright (c) BigMiao. All rights reserved.
// </copyright>

using MLNet.Sweeper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLNet.AutoPipeline
{
    internal abstract class OptionBuilder<TOption>
        where TOption : class
    {
        public IValueGenerator[] ValueGenerators => this.GetParameterAttributes().Select(kv => kv.Value.ValueGenerator).ToArray();

        public TOption CreateDefaultOption()
        {
            var assem = typeof(TOption).Assembly;
            var option = assem.CreateInstance(typeof(TOption).FullName) as TOption;

            // set up fields
            var fields = this.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var field in fields)
            {
                var value = field.GetValue(this);
                option.GetType().GetField(field.Name)?.SetValue(option, value);
            }

            return option;
        }

        public TOption BuildOption(ParameterSet parameters)
        {
            var option = this.CreateDefaultOption();
            foreach (var param in parameters)
            {
                var value = param.RawValue;
                typeof(TOption).GetField(param.Name)?.SetValue(option, value);
            }

            return option;
        }

        private Dictionary<string, ParameterAttribute> GetParameterAttributes()
        {
            var paramaters = this.GetType().GetFields()
                     .Where(x => Attribute.GetCustomAttribute(x, typeof(ParameterAttribute)) != null);

            var paramatersDictionary = new Dictionary<string, ParameterAttribute>();
            foreach (var param in paramaters)
            {
                paramatersDictionary.Add(param.Name, Attribute.GetCustomAttribute(param, typeof(ParameterAttribute)) as ParameterAttribute);
            }

            return paramatersDictionary;
        }
    }
}