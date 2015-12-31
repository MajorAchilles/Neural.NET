using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neural.NET
{
    /// <summary>
    /// 
    /// </summary>
    public class NeuralNetworkOptions
    {
        /// <summary>
        /// Gets or sets the size of the input layer.
        /// </summary>
        /// <value>
        /// The size of the input layer.
        /// </value>
        public int InputLayerSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the output layer.
        /// </summary>
        /// <value>
        /// The size of the output layer.
        /// </value>
        public int OutputLayerSize { get; set; }

        /// <summary>
        /// Gets or sets the hidden layer sizes.
        /// </summary>
        /// <value>
        /// The hidden layer sizes.
        /// </value>
        public int[] HiddenLayerSizes { get; set; }

        /// <summary>
        /// Gets or sets the activation function.
        /// </summary>
        /// <value>
        /// The activation function.
        /// </value>
        public ActivationFunction ActivationFunction { get; set; }
    }
}
