namespace Neural.NET
{
    /// <summary>
    ///
    /// </summary>
    public class InputLayer
    {
        /// <summary>
        /// Gets or sets the neurons.
        /// </summary>
        /// <value>
        /// The neurons.
        /// </value>
        public InputNeuron[] Neurons { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Layer" /> class.
        /// </summary>
        /// <param name="size">The size of the layer.</param>
        public InputLayer(int size)
        {
            this.Neurons = new InputNeuron[size];
            for (int i = 0; i < size; i++)
            {
                this.Neurons[i] = new InputNeuron();
            }
        }

        public int Size
        {
            get
            {
                return Neurons.Length;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class OutputLayer
    {
        /// <summary>
        /// Gets or sets the neurons.
        /// </summary>
        /// <value>
        /// The neurons.
        /// </value>
        public OutputNeuron[] Neurons { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Layer" /> class.
        /// </summary>
        /// <param name="size">The size of the layer.</param>
        public OutputLayer(int size)
        {
            this.Neurons = new OutputNeuron[size];
            for (int i = 0; i < size; i++)
            {
                this.Neurons[i] = new OutputNeuron();
            }
        }

        public int Size
        {
            get
            {
                return Neurons.Length;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class HiddenLayer
    {
        /// <summary>
        /// Gets or sets the neurons.
        /// </summary>
        /// <value>
        /// The neurons.
        /// </value>
        public HiddenNeuron[] Neurons { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Layer" /> class.
        /// </summary>
        /// <param name="size">The size of the layer.</param>
        public HiddenLayer(int size)
        {
            this.Neurons = new HiddenNeuron[size];
            for (int i = 0; i < size; i++)
            {
                this.Neurons[i] = new HiddenNeuron();
            }
        }

        public int Size
        {
            get
            {
                return Neurons.Length;
            }
        }
    }
}