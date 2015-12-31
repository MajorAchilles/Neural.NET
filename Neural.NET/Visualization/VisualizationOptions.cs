using System.Drawing;

namespace Neural.NET.Visualization
{
    /// <summary>
    ///
    /// </summary>
    public class VisualizationOptions
    {
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { set; get; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { set; get; }

        /// <summary>
        /// Gets or sets a value indicating whether [show values].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show values]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowValues { set; get; }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        public Color BackgroundColor { set; get; }

        /// <summary>
        /// Gets or sets the color of the value font.
        /// </summary>
        /// <value>
        /// The color of the value font.
        /// </value>
        public Color ValueFontColor { set; get; }

        /// <summary>
        /// Gets or sets the color of the input layer primary.
        /// </summary>
        /// <value>
        /// The color of the input layer primary.
        /// </value>
        public Color InputLayerPrimaryColor { set; get; }

        /// <summary>
        /// Gets or sets the color of the hidden layer primary.
        /// </summary>
        /// <value>
        /// The color of the hidden layer primary.
        /// </value>
        public Color HiddenLayerPrimaryColor { set; get; }

        /// <summary>
        /// Gets or sets the color of the output layer primary.
        /// </summary>
        /// <value>
        /// The color of the output layer primary.
        /// </value>
        public Color OutputLayerPrimaryColor { set; get; }
    }
}