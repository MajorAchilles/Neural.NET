namespace Neural.NET
{
    /// <summary>
    /// The Activation function to be used by the neuron.
    /// </summary>
    public enum ActivationFunction
    {
        Identity,
        HyperbolicTangent,
        HeavisideStep,
        LogisticSigmoid,
        Softmax
    }
}