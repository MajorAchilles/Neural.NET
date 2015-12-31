using System;
using System.Collections.Generic;

namespace Neural.NET
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Neuron
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the activation function.
        /// </summary>
        /// <value>
        /// The activation function.
        /// </value>
        public ActivationFunction ActivationFunction { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Neural.NET.Neuron" />
    public class InputNeuron : Neuron
    {
        /// <summary>
        /// The out synapses
        /// </summary>
        private List<Synapse> outSynapses = new List<Synapse>();

        /// <summary>
        /// Gets or sets the out synapses.
        /// </summary>
        /// <value>
        /// The out synapses.
        /// </value>
        public List<Synapse> OutSynapses
        {
            get
            {
                return outSynapses;
            }

            set
            {
                outSynapses = value;
            }
        }

        /// <summary>
        /// Propagates the value.
        /// </summary>
        /// <returns></returns>
        public void Propagate()
        {
            foreach (Synapse synapse in outSynapses)
            {
                synapse.Value = this.Value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Neural.NET.Neuron" />
    public class OutputNeuron : Neuron
    {
        /// <summary>
        /// The in synapses
        /// </summary>
        List<Synapse> inSynapses = new List<Synapse>();

        /// <summary>
        /// Gets or sets the in synapses.
        /// </summary>
        /// <value>
        /// The in synapses.
        /// </value>
        public List<Synapse> InSynapses
        {
            get
            {
                return inSynapses;
            }

            set
            {
                inSynapses = value;
            }
        }

        /// <summary>
        /// Adds the values.
        /// </summary>
        /// <returns></returns>
        public virtual double AddInputs()
        {
            Value = 0;
            foreach (Synapse synapse in InSynapses)
            {
                Value += synapse.Value * synapse.Weight;
            }
            return Value;
        }

        /// <summary>
        /// Applies the activation function.
        /// </summary>
        /// <param name="activationFunction">The activation function.</param>
        /// <returns></returns>
        public double Activate(ActivationFunction activationFunction)
        {
            switch(activationFunction)
            {
                case ActivationFunction.HeavisideStep:
                    return HeavisideStep(Value);
                case ActivationFunction.HyperbolicTangent:
                    return HyperbolicTangent(Value);
                case ActivationFunction.Identity:
                    return Identity(Value);
                case ActivationFunction.LogisticSigmoid:
                    return LogisticSigmoid(Value);
                case ActivationFunction.Softmax:
                    return LogisticSigmoid(Value);
            }
            return Identity(Value);
        }

        /// <summary>
        /// Logistics the sigmoid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private double LogisticSigmoid(double value)
        {
            return Value = 1 / (1 + Math.Exp(-value));
        }

        /// <summary>
        /// Hyperbolics the tangent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private double HyperbolicTangent(double value)
        {
            return Value = (Math.Exp(value) - Math.Exp(-value)) / (Math.Exp(value) + Math.Exp(-value));
        }

        /// <summary>
        /// Heavisides the step.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private double HeavisideStep(double value)
        {
            return Value = Convert.ToDouble(value >= 0);
        }

        /// <summary>
        /// Softmaxes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private double Softmax(double value)
        {
            return Value = value;
        }

        /// <summary>
        /// Identities the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private double Identity(double value)
        {
            return Value = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Neural.NET.OutputNeuron" />
    public class HiddenNeuron : OutputNeuron
    {
        /// <summary>
        /// Gets or sets the bias.
        /// </summary>
        /// <value>
        /// The bias.
        /// </value>
        public double Bias { get; set; }

        /// <summary>
        /// The out synapses
        /// </summary>
        private List<Synapse> outSynapses = new List<Synapse>();

        /// <summary>
        /// Gets or sets the out synapses.
        /// </summary>
        /// <value>
        /// The out synapses.
        /// </value>
        public List<Synapse> OutSynapses
        {
            get
            {
                return outSynapses;
            }

            set
            {
                outSynapses = value;
            }
        }

        /// <summary>
        /// Adds the values.
        /// </summary>
        /// <returns></returns>
        public override double AddInputs()
        {
            Value = Bias;
            foreach (Synapse synapse in InSynapses)
            {
                Value += synapse.Value * synapse.Weight;
            }
            return Value;
        }

        /// <summary>
        /// Activates this instance.
        /// </summary>
        public void Propagate()
        {
            foreach (Synapse synapse in OutSynapses)
            {
                synapse.Value = Value;
            }
        }
    }
}