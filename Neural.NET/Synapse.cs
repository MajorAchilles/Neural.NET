namespace Neural.NET
{
    public class Synapse
    {
        /// <summary>
        /// The value carried by the synapse.
        /// </summary>
        private double value;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value
        {
            get
            {
                return value * Weight;
            }

            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public double Weight { get; set; }
    }
}