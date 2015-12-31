using System.Drawing;

namespace Neural.NET.Visualization
{
    /// <summary>
    ///
    /// </summary>
    internal abstract class VisualizationComponent
    {
        /// <summary>
        /// The left top
        /// </summary>
        private PointF left;

        /// <summary>
        /// The right bottom
        /// </summary>
        private PointF right;

        /// <summary>
        /// The size
        /// </summary>
        private SizeF size;

        /// <summary>
        /// Gets or sets the left top.
        /// </summary>
        /// <value>
        /// The left top.
        /// </value>
        public PointF Left
        {
            get
            {
                return left;
            }

            set
            {
                left = value;
            }
        }

        /// <summary>
        /// Gets or sets the right bottom.
        /// </summary>
        /// <value>
        /// The right bottom.
        /// </value>
        public PointF Right
        {
            get
            {
                return right;
            }

            set
            {
                right = value;
            }
        }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public SizeF Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        /// <returns></returns>
        public abstract Bitmap Draw();

        /// <summary>
        /// Draws this instance.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="primaryColor">Color of the primary.</param>
        /// <param name="secondaryColor">Color of the secondary.</param>
        /// <returns></returns>
        public abstract void Draw(Graphics g, Color primaryColor, Color secondaryColor);
    }
}