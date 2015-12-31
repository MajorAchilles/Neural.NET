using System;
using System.Linq;

namespace Neural.NET
{
    /// <summary>
    /// Provides functionality to simulate a neural network.
    /// </summary>
    public class NeuralNetwork
    {
        /// <summary>
        /// Random number generator;
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public NeuralNetworkOptions Options { get; set; }

        /// <summary>
        /// The input layer
        /// </summary>
        private InputLayer inputLayer;

        /// <summary>
        /// The output layer
        /// </summary>
        private OutputLayer outputLayer;

        /// <summary>
        /// The hidden layer
        /// </summary>
        private HiddenLayer[] hiddenLayers;

        /// <summary>
        /// Gets the input layer.
        /// </summary>
        /// <value>
        /// The input layer.
        /// </value>
        public InputLayer InputLayer
        {
            get
            {
                return inputLayer;
            }
        }

        /// <summary>
        /// Gets the output layer.
        /// </summary>
        /// <value>
        /// The output layer.
        /// </value>
        public OutputLayer OutputLayer
        {
            get
            {
                return outputLayer;
            }
        }

        /// <summary>
        /// Gets the hidden layer.
        /// </summary>
        /// <value>
        /// The hidden layer.
        /// </value>
        public HiddenLayer[] HiddenLayers
        {
            get
            {
                return hiddenLayers;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NeuralNetwork"/> class.
        /// </summary>
        /// <param name="inputLayerSize">Size of the input layer.</param>
        /// <param name="outputLayerSize">Size of the output layer.</param>
        /// <param name="hiddenLayerSize">Size of the hidden layer.</param>
        public NeuralNetwork(NeuralNetworkOptions options)
        {
            Options = options;

            CreateLayers();
            ConnectLayers();
        }

        /// <summary>
        /// Creates the layers.
        /// </summary>
        private void CreateLayers()
        {
            inputLayer = new InputLayer(Options.InputLayerSize);
            outputLayer = new OutputLayer(Options.OutputLayerSize);
            hiddenLayers = new HiddenLayer[Options.HiddenLayerSizes.Count()];

            for (int i = 0; i < Options.HiddenLayerSizes.Count(); i++) //Initialize every hidden layer
                hiddenLayers[i] = new HiddenLayer(Options.HiddenLayerSizes[i]);
        }

        /// <summary>
        /// Connects the layers.
        /// </summary>
        private void ConnectLayers()
        {
            for (int i = 0; i < Options.InputLayerSize; i++) //Each neuron in input layer
            {
                InputNeuron leftNeuron = InputLayer.Neurons[i];

                for (int j = 0; j < Options.HiddenLayerSizes[0]; j++) //each neuron in first hidden layer
                {
                    Synapse synapse = new Synapse();
                    synapse.Weight = ((double)random.Next(-10, 11)) / 10;
                    leftNeuron.OutSynapses.Add(synapse);
                    hiddenLayers[0].Neurons[j].InSynapses.Add(synapse);
                }
            }

            for (int i = 0; i < Options.HiddenLayerSizes.Count() - 1; i++) //Each hidden layer, except the last one
            {
                for (int j = 0; j < Options.HiddenLayerSizes[i]; j++) //Each neuron
                {
                    HiddenNeuron leftNeuron = hiddenLayers[i].Neurons[j];

                    for (int k = 0; k < Options.HiddenLayerSizes[i + 1]; k++) //Each neuron in right side
                    {
                        Synapse synapse = new Synapse();
                        synapse.Weight = ((double)random.Next(-10, 11)) / 10;
                        leftNeuron.OutSynapses.Add(synapse);
                        hiddenLayers[i + 1].Neurons[k].InSynapses.Add(synapse);
                    }
                }
            }

            int lasthiddenLayerSize = Options.HiddenLayerSizes[Options.HiddenLayerSizes.Count() - 1];

            for (int i = 0; i < lasthiddenLayerSize; i++) //Each neuron in last hidden layer
            {
                HiddenNeuron leftNeuron = hiddenLayers[hiddenLayers.Length - 1].Neurons[i];

                for (int j = 0; j < Options.OutputLayerSize; j++) //each neuron in output layer
                {
                    Synapse synapse = new Synapse();
                    synapse.Weight = ((double)random.Next(-10, 11)) / 10;
                    leftNeuron.OutSynapses.Add(synapse);
                    OutputLayer.Neurons[j].InSynapses.Add(synapse);
                }
            }
        }

        /// <summary>
        /// Trains the instance using the specified input and output
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        /// <param name="epochs">The epochs.</param>
        public void Train(double[] input, double[] output, int epochs)
        {
            for (int counter = 0; counter < epochs; counter++)
            {
                for (int i = 0; i < input.Length && i < Options.InputLayerSize; i++)
                {
                    inputLayer.Neurons[i].Value = input[i];
                }

                foreach (InputNeuron neuron in InputLayer.Neurons)
                {
                    neuron.Propagate();
                }

                foreach (HiddenLayer hiddenLayer in hiddenLayers)
                {
                    foreach (HiddenNeuron neuron in hiddenLayer.Neurons)
                    {
                        neuron.AddInputs();
                        neuron.Activate(Options.ActivationFunction);
                        neuron.Propagate();
                    }
                }

                foreach (OutputNeuron neuron in outputLayer.Neurons)
                {
                    neuron.AddInputs();
                    neuron.Activate(Options.ActivationFunction);
                }
            }
        }
    }
}