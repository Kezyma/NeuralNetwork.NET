﻿using System;
using NeuralNetworkNET.Cuda.Helpers;
using NeuralNetworkNET.Helpers;

namespace NeuralNetworkNET.Cuda.APIs
{
    /// <summary>
    /// A static class for some additional settings for the neural networks produced by the library
    /// </summary>
    public static class NeuralNetworkGpuPreferences
    {
        private static ProcessingMode _ProcessingMode = ProcessingMode.Cpu;

        /// <summary>
        /// Gets or sets the desired processing mode to perform the neural network operations
        /// </summary>
        public static ProcessingMode ProcessingMode
        {
            get => _ProcessingMode;
            set
            {
                if (_ProcessingMode != value)
                {
                    switch (value)
                    {
                        case ProcessingMode.Cpu:
                            MatrixServiceProvider.ResetInjections();
                            break;
                        case ProcessingMode.Gpu:
                            MatrixServiceProvider.SetupInjections(
                                MatrixGpuExtensions.Multiply,
                                MatrixGpuExtensions.TransposeAndMultiply,
                                MatrixGpuExtensions.MultiplyAndActivation,
                                MatrixGpuExtensions.Activation,
                                MatrixGpuExtensions.HalfSquaredDifference,
                                MatrixGpuExtensions.InPlaceSubtractAndHadamardProductWithActivationPrime,
                                MatrixGpuExtensions.InPlaceActivationPrimeAndHadamardProduct);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(value), value, null);
                    }
                    _ProcessingMode = value;
                }
            }
        }
    }
}
