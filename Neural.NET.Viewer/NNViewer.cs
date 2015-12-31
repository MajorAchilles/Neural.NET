using Neural.NET.Visualization;
using System.Drawing;
using System.Windows.Forms;

namespace Neural.NET.Viewer
{
    public partial class NNViewer : Form
    {
        private NeuralNetwork network;
        private Visualizer visualizer;

        public NNViewer()
        {
            NeuralNetworkOptions nno = new NeuralNetworkOptions();
            nno.InputLayerSize = 5;
            nno.OutputLayerSize = 2;
            nno.HiddenLayerSizes = new int[] { 3,4,5, 7, 8, 20, 4 };
            nno.ActivationFunction = ActivationFunction.HyperbolicTangent;
            network = new NeuralNetwork(nno);
            InitializeComponent();
        }

        private void NNViewer_Load(object sender, System.EventArgs e)
        {
            VisualizationOptions vo = new VisualizationOptions();
            vo.Width = pictureBox1.Width;
            vo.Height = pictureBox1.Height;
            vo.InputLayerPrimaryColor = Color.PaleVioletRed;
            vo.OutputLayerPrimaryColor = Color.PaleGreen;
            vo.HiddenLayerPrimaryColor = Color.SteelBlue;
            vo.BackgroundColor = Color.PowderBlue;
            vo.ShowValues = true;
            vo.ValueFontColor = Color.Black;
            visualizer = new Visualizer(network, vo);
            pictureBox1.Image = visualizer.Draw();
            network.Train(new double[] { 2, 4, 7 }, new double[] { 2, 2 }, 10);
            pictureBox1.Image = visualizer.Draw();
        }
    }
}