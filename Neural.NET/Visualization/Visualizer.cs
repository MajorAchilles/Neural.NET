using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Neural.NET.Visualization
{
    /// <summary>
    /// Provide functionality to draw the neural network.
    /// </summary>
    public class Visualizer
    {
        /// <summary>
        /// The border width
        /// </summary>
        public const int BorderWidth = 0;

        /// <summary>
        /// The padding factor
        /// </summary>
        public const float PaddingFactor = 0.2F;

        /// <summary>
        /// The effective width
        /// </summary>
        private int effectiveWidth;

        /// <summary>
        /// The effective height
        /// </summary>
        private int effectiveHeight;

        /// <summary>
        /// The column count
        /// </summary>
        private int columnCount;

        /// <summary>
        /// The column width
        /// </summary>
        private float columnWidth;

        /// <summary>
        /// The input layer data
        /// </summary>
        private LayerDrawingData inputLayerData;

        /// <summary>
        /// The output layer data
        /// </summary>
        private LayerDrawingData outputLayerData;

        /// <summary>
        /// The hidden layer data
        /// </summary>
        private LayerDrawingData[] hiddenLayerData;

        /// <summary>
        /// The visualization options
        /// </summary>
        private VisualizationOptions visualizationOptions;

        /// <summary>
        /// The neural network
        /// </summary>
        private NeuralNetwork neuralNetwork;

        /// <summary>
        /// Initializes a new instance of the <see cref="Visualizer" /> class.
        /// </summary>
        /// <param name="neuralNetwork">The neural network.</param>
        /// <param name="visualizationOptions">The visualization options.</param>
        public Visualizer(NeuralNetwork neuralNetwork, VisualizationOptions visualizationOptions)
        {
            this.neuralNetwork = neuralNetwork;
            this.visualizationOptions = visualizationOptions;
            CalculateValues();
        }

        /// <summary>
        /// Calculates the values.
        /// </summary>
        private void CalculateValues()
        {
            effectiveWidth = visualizationOptions.Width - (BorderWidth * 2);
            effectiveHeight = visualizationOptions.Height - (BorderWidth * 2);

            columnCount = neuralNetwork.Options.HiddenLayerSizes.Length + 2;
            columnWidth = effectiveWidth / columnCount;

            inputLayerData = new LayerDrawingData();
            inputLayerData.rowCount = neuralNetwork.InputLayer.Size;
            inputLayerData.rowHeight = effectiveHeight / inputLayerData.rowCount;
            if (inputLayerData.rowHeight < columnWidth)
            {
                inputLayerData.paddingVertical = inputLayerData.rowHeight * PaddingFactor;
                inputLayerData.neuronLength = inputLayerData.rowHeight - inputLayerData.paddingVertical * 2;
                inputLayerData.paddingHorizontal = (columnWidth - inputLayerData.neuronLength) / 2;
            }
            else
            {
                inputLayerData.paddingHorizontal = columnWidth * PaddingFactor;
                inputLayerData.neuronLength = columnWidth - inputLayerData.paddingHorizontal * 2;
                inputLayerData.paddingVertical = (inputLayerData.rowHeight - inputLayerData.neuronLength) / 2;
            }

            outputLayerData = new LayerDrawingData();
            outputLayerData.rowCount = neuralNetwork.OutputLayer.Size;
            outputLayerData.rowHeight = effectiveHeight / outputLayerData.rowCount;
            if (outputLayerData.rowHeight < columnWidth)
            {
                outputLayerData.paddingVertical = outputLayerData.rowHeight * PaddingFactor;
                outputLayerData.neuronLength = outputLayerData.rowHeight - outputLayerData.paddingVertical * 2;
                outputLayerData.paddingHorizontal = (columnWidth - outputLayerData.neuronLength) / 2;
            }
            else
            {
                outputLayerData.paddingHorizontal = columnWidth * PaddingFactor;
                outputLayerData.neuronLength = columnWidth - outputLayerData.paddingHorizontal * 2;
                outputLayerData.paddingVertical = (outputLayerData.rowHeight - outputLayerData.neuronLength) / 2;
            }

            hiddenLayerData = new LayerDrawingData[neuralNetwork.HiddenLayers.Length];
            for (int i = 0; i < neuralNetwork.HiddenLayers.Length; i++)
            {
                hiddenLayerData[i].rowCount = neuralNetwork.HiddenLayers[i].Size;
                hiddenLayerData[i].rowHeight = effectiveHeight / hiddenLayerData[i].rowCount;
                if (hiddenLayerData[i].rowHeight < columnWidth)
                {
                    hiddenLayerData[i].paddingVertical = hiddenLayerData[i].rowHeight * PaddingFactor;
                    hiddenLayerData[i].neuronLength = hiddenLayerData[i].rowHeight - hiddenLayerData[i].paddingVertical * 2;
                    hiddenLayerData[i].paddingHorizontal = (columnWidth - hiddenLayerData[i].neuronLength) / 2;
                }
                else
                {
                    hiddenLayerData[i].paddingHorizontal = columnWidth * PaddingFactor;
                    hiddenLayerData[i].neuronLength = columnWidth - hiddenLayerData[i].paddingHorizontal * 2;
                    hiddenLayerData[i].paddingVertical = (hiddenLayerData[i].rowHeight - hiddenLayerData[i].neuronLength) / 2;
                }
            }
        }

        /// <summary>
        /// Draws the neural network to a bitmap.
        /// </summary>
        /// <returns></returns>
        public Bitmap Draw()
        {
            Bitmap bitmap = new Bitmap(visualizationOptions.Width, visualizationOptions.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(visualizationOptions.BackgroundColor);
            //DrawBorders(width, height, g);

            DrawSynapses(g);
            DrawNeurons(g);
            if (visualizationOptions.ShowValues)
                DrawValues(g);
            return bitmap;
        }

        /// <summary>
        /// Draws the neurons.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawNeurons(Graphics g)
        {
            //TODO: Change calculations to count for tall layers;
            float x = BorderWidth; //Column start
            float y = BorderWidth; //Row start

            #region Input Layer

            x += inputLayerData.paddingHorizontal; //Neuron start
            Brush inputBrush = new SolidBrush(visualizationOptions.InputLayerPrimaryColor);
            foreach (Neuron neuron in neuralNetwork.InputLayer.Neurons)
            {
                y += inputLayerData.paddingVertical;
                g.FillEllipse(inputBrush, new RectangleF(x, y, inputLayerData.neuronLength, inputLayerData.neuronLength));
                y += inputLayerData.neuronLength + inputLayerData.paddingVertical;
            }
            x += inputLayerData.neuronLength + inputLayerData.paddingHorizontal; //Next column
            //g.DrawLine(Pens.Black, new PointF(x, 0), new PointF(x, height));

            #endregion Input Layer

            #region Hidden layers

            int i = 0;
            Brush hiddenBrush = new SolidBrush(visualizationOptions.HiddenLayerPrimaryColor);
            foreach (HiddenLayer layer in neuralNetwork.HiddenLayers)
            {
                x += hiddenLayerData[i].paddingHorizontal; //Neuron start
                y = BorderWidth;
                foreach (Neuron neuron in layer.Neurons)
                {
                    y += hiddenLayerData[i].paddingVertical;
                    g.FillEllipse(hiddenBrush, new RectangleF(x, y, hiddenLayerData[i].neuronLength, hiddenLayerData[i].neuronLength));
                    y += hiddenLayerData[i].neuronLength + hiddenLayerData[i].paddingVertical; ;
                }

                x += hiddenLayerData[i].neuronLength + hiddenLayerData[i].paddingHorizontal; //Next Column
                //g.DrawLine(Pens.Black, new PointF(x, 0), new PointF(x, height));
                i++;
            }

            #endregion Hidden layers

            #region Output layer

            x += outputLayerData.paddingHorizontal; //Neuron start
            y = BorderWidth;
            Brush outputBrush = new SolidBrush(visualizationOptions.OutputLayerPrimaryColor);
            foreach (Neuron neuron in neuralNetwork.OutputLayer.Neurons)
            {
                y += outputLayerData.paddingVertical;
                g.FillEllipse(outputBrush, new RectangleF(x, y, outputLayerData.neuronLength, outputLayerData.neuronLength));
                y += outputLayerData.neuronLength + outputLayerData.paddingVertical;
            }
            x += outputLayerData.neuronLength + outputLayerData.paddingHorizontal; //Border
                                                                                   //g.DrawLine(Pens.Black, new PointF(x, 0), new PointF(x, height));

            #endregion Output layer
        }

        /// <summary>
        /// Draws the synapses.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawSynapses(Graphics g)
        {
            float x = BorderWidth; //Column start
            float y = BorderWidth; //Row start

            #region Input Layer

            float xCur = x + columnWidth / 2;
            foreach (InputNeuron neuron in neuralNetwork.InputLayer.Neurons)
            {
                float yCur = y + inputLayerData.rowHeight / 2;
                float xNext = (x + columnWidth) + columnWidth / 2;
                float yNext = BorderWidth;
                foreach (Synapse synapse in neuron.OutSynapses)
                {
                    yNext += hiddenLayerData[0].rowHeight / 2;
                    if (synapse.Weight != 0)
                    {
                        Pen pen = new Pen(Color.Black, Convert.ToSingle(synapse.Weight * 10));
                        g.DrawLine(pen, xCur, yCur, xNext, yNext);
                    }
                    yNext += hiddenLayerData[0].rowHeight / 2;
                }
                y += inputLayerData.rowHeight;
            }
            x += columnWidth; //Next column

            #endregion Input Layer

            #region Hidden layers

            int hiddenLayerCount = neuralNetwork.HiddenLayers.Length;
            for (int i = 0; i < hiddenLayerCount - 1; i++)
            {
                y = BorderWidth;
                xCur = x + columnWidth / 2;
                foreach (HiddenNeuron neuron in neuralNetwork.HiddenLayers[i].Neurons)
                {
                    float yCur = y + hiddenLayerData[i].rowHeight / 2;
                    float xNext = (x + columnWidth) + columnWidth / 2;
                    float yNext = BorderWidth;
                    foreach (Synapse synapse in neuron.OutSynapses)
                    {
                        yNext += hiddenLayerData[i + 1].rowHeight / 2;
                        if (synapse.Weight != 0)
                        {
                            Pen pen = new Pen(Color.Black, Convert.ToSingle(synapse.Weight * 10));
                            g.DrawLine(pen, xCur, yCur, xNext, yNext);
                        }
                        yNext += hiddenLayerData[i + 1].rowHeight / 2;
                    }
                    y += hiddenLayerData[i].rowHeight;
                }
                x += columnWidth;
            }

            #endregion Hidden layers

            #region Output Layer

            y = BorderWidth;
            xCur = x + columnWidth / 2;
            HiddenLayer lastHiddenLayer = neuralNetwork.HiddenLayers[neuralNetwork.Options.HiddenLayerSizes.Length - 1];
            foreach (HiddenNeuron neuron in lastHiddenLayer.Neurons)
            {
                float yCur = y + hiddenLayerData[hiddenLayerData.Length - 1].rowHeight / 2;
                float xNext = (x + columnWidth) + columnWidth / 2;
                float yNext = BorderWidth;
                foreach (Synapse synapse in neuron.OutSynapses)
                {
                    yNext += outputLayerData.rowHeight / 2;
                    if (synapse.Weight != 0)
                    {
                        Pen pen = new Pen(Color.Black, Convert.ToSingle(synapse.Weight * 10));
                        g.DrawLine(pen, xCur, yCur, xNext, yNext);
                    }
                    yNext += outputLayerData.rowHeight / 2;
                }
                y += hiddenLayerData[hiddenLayerData.Length - 1].rowHeight;
            }
            x += columnWidth; //Next column

            #endregion Output Layer
        }

        /// <summary>
        /// Draws the synapses.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawValues(Graphics g)
        {
            float x = BorderWidth; //Column start
            float y = BorderWidth; //Row start

            #region Input Layer

            float xCur = x + columnWidth / 2;
            foreach (InputNeuron neuron in neuralNetwork.InputLayer.Neurons)
            {
                float yCur = y + inputLayerData.rowHeight / 2;
                DrawString(g, neuron, xCur, yCur);
                y += inputLayerData.rowHeight;
            }
            x += columnWidth; //Next column

            #endregion Input Layer

            #region Hidden layers

            for (int i = 0; i < neuralNetwork.HiddenLayers.Length; i++)
            {
                y = BorderWidth;
                xCur = x + columnWidth / 2;
                foreach (HiddenNeuron neuron in neuralNetwork.HiddenLayers[i].Neurons)
                {
                    float yCur = y + hiddenLayerData[i].rowHeight / 2;
                    DrawString(g, neuron, xCur, yCur);
                    y += hiddenLayerData[i].rowHeight;
                }
                x += columnWidth;
            }

            #endregion Hidden layers

            #region Output Layer

            y = BorderWidth;
            xCur = x + columnWidth / 2;
            foreach (OutputNeuron neuron in neuralNetwork.OutputLayer.Neurons)
            {
                float yCur = y + outputLayerData.rowHeight / 2;
                DrawString(g, neuron, xCur, yCur);
                y += outputLayerData.rowHeight;
            }
            x += columnWidth; //Next column

            #endregion Output Layer
        }

        /// <summary>
        /// Draws the string.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="neuron">The neuron.</param>
        /// <param name="xCur">The x current.</param>
        /// <param name="yCur">The y current.</param>
        private void DrawString(Graphics g, Neuron neuron, float xCur, float yCur)
        {
            g.DrawString(Math.Round(neuron.Value, 3, MidpointRounding.AwayFromZero).ToString(), new Font("Arial", 10F), new SolidBrush(visualizationOptions.ValueFontColor), new PointF(xCur - 10, yCur - 5));
        }

        /// <summary>
        /// Draws the borders.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="g">The g.</param>
        private static void DrawBorders(int width, int height, Graphics g)
        {
            g.DrawLine(Pens.Black, new Point(BorderWidth, 0), new Point(BorderWidth, height)); //BORDER TOP
            g.DrawLine(Pens.Black, new Point(0, BorderWidth), new Point(width, BorderWidth)); //BORDER LEFT
            g.DrawLine(Pens.Black, new Point(width - BorderWidth, 0), new Point(width - BorderWidth, height)); //BORDER TOP
            g.DrawLine(Pens.Black, new Point(0, height - BorderWidth), new Point(width, height - BorderWidth)); //BORDER TOP
        }
    }
}