﻿// <copyright file="FastForestBinaryTrainerSweepableOptions.cs" company="BigMiao">
// Copyright (c) BigMiao. All rights reserved.
// </copyright>

using Microsoft.ML.Trainers.FastTree;

namespace MLNet.AutoPipeline
{
    public class FastForestBinaryTrainerSweepableOptions : SweepableOption<FastForestBinaryTrainer.Options>
    {
        public static FastForestBinaryTrainerSweepableOptions Default = new FastForestBinaryTrainerSweepableOptions();

        [Parameter]
        public Parameter<string> LabelColumnName = CreateFromSingleValue("Label");

        [Parameter]
        public Parameter<string> FeatureColumnName = CreateFromSingleValue("Features");

        [Parameter]
        public Parameter<string> ExampleWeightColumnName = CreateFromSingleValue<string>(default);

        [Parameter]
        public Parameter<int> NumberOfLeaves = CreateInt32Parameter(10, 1000, true);

        [Parameter]
        public Parameter<int> NumberOfTrees = CreateInt32Parameter(1, 1000, true);

        [Parameter]
        public Parameter<int> MinimumExampleCountPerLeaf = CreateInt32Parameter(1, 100, true);
    }
}
