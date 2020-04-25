﻿// <copyright file="AutoEstimatorExtension.cs" company="BigMiao">
// Copyright (c) BigMiao. All rights reserved.
// </copyright>

using Microsoft.ML;
using Microsoft.ML.Data;
using MLNet.Sweeper;
using System;

namespace MLNet.AutoPipeline.AutoEstimatorExtension
{
    public static class AutoEstimatorExtension
    {
        public static AutoEstimatorNodeChain
            Append<TLastTrain, TNewTrain, TOption>(
                this EstimatorChain<TLastTrain> estimatorChain,
                Func<TOption, IEstimator<TNewTrain>> estimatorBuilder,
                OptionBuilder<TOption> parameters,
                TransformerScope scope = TransformerScope.Everything)
            where TLastTrain : class, ITransformer
            where TNewTrain : class, ITransformer
            where TOption : class
        {
            var autoEstimator = new EstimatorBuilder<TNewTrain, TOption>(estimatorBuilder, parameters, scope);

            var estimators = estimatorChain.GetType().GetField("_estimators", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(estimatorChain) as IEstimator<ITransformer>[];
            var scopes = estimatorChain.GetType().GetField("_scopes", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(estimatorChain) as TransformerScope[];

            return new AutoEstimatorNodeChain(estimators, scopes)
                       .Append(autoEstimator);
        }

        public static AutoEstimatorNodeChain
            Append<TNewTrain, TOption>(
                this IEstimator<ITransformer> estimator,
                Func<TOption, IEstimator<TNewTrain>> estimatorBuilder,
                OptionBuilder<TOption> parameters,
                TransformerScope scope = TransformerScope.Everything)
            where TNewTrain : class, ITransformer
            where TOption : class
        {
            var autoEstimator = new EstimatorBuilder<TNewTrain, TOption>(estimatorBuilder, parameters);

            return new AutoEstimatorNodeChain(new IEstimator<ITransformer>[] { estimator }, new TransformerScope[] { TransformerScope.Everything })
                       .Append(autoEstimator);
        }
    }
}
