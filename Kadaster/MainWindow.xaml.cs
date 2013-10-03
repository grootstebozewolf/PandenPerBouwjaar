using System.Windows;
using ESRI.ArcGIS.Client;
using System.Diagnostics;
using ESRI.ArcGIS.Client.Symbols;
using System.Windows.Media;

namespace Kadaster
{

    public partial class MainWindow : Window
    {
        private DynamicMapServiceLayer bagLayer;
        private LayerDrawingOptionsCollection layerDrawingOptionsCollection;
        public MainWindow()
        {
            // License setting and ArcGIS Runtime initialization is done in Application.xaml.cs.
            InitializeComponent();

        var dynamicLayerInfoCollection = new DynamicLayerInfoCollection();
            var dynamicLayerInfo = new DynamicLayerInfo();
            dynamicLayerInfoCollection.Add(dynamicLayerInfo);

            var layerDrawingOptionsCollection = new LayerDrawingOptionsCollection();
            var defaultDrawingOptions = new LayerDrawingOptions();
            var simpleRender = new SimpleRenderer();
            var classBreaksRenderer = new ClassBreaksRenderer { Field="Bouwjaar" };
            var defaultFillSymbol = new SimpleFillSymbol { BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 199, 147, 118)), Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 227, 201, 187)) };
            var brownFillSymbol = new SimpleFillSymbol { BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 184, 146, 133)), Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 148, 102, 86)) };
            var brownClassBreakInfo = new ClassBreakInfo { MinimumValue = double.MinValue, MaximumValue = 1800, Symbol = brownFillSymbol, Label="Voor 1800" };
            var darkbrownFillSymbol = new SimpleFillSymbol { BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 106, 73, 62)), Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 169, 142, 126)) };
            var darkbrownClassBreakInfo = new ClassBreakInfo { MinimumValue = 1800, MaximumValue = 1950, Symbol = darkbrownFillSymbol, Label = "1800-1900" };
            var graybrownFillSymbol = new SimpleFillSymbol { BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 109, 89, 84)), Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 170, 150, 145)) };
            var graybrownClassBreakInfo = new ClassBreakInfo { MinimumValue = 1950, MaximumValue = 1970, Symbol = graybrownFillSymbol, Label = "1950-1970" };
            var grayFillSymbol = new SimpleFillSymbol { BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 117, 117, 117)), Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 173, 173, 173)) };
            var grayClassBreakInfo = new ClassBreakInfo { MinimumValue = 1970, MaximumValue = 1990, Symbol = grayFillSymbol, Label = "1970-1990" };
            var darkgrayFillSymbol = new SimpleFillSymbol { BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 80, 80, 80)), Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 117, 117, 117)) };
            var darkgrayClassBreakInfo = new ClassBreakInfo { MinimumValue = 1990, MaximumValue = 2005, Symbol = darkgrayFillSymbol, Label = "1990-2005" };
            var blackFillSymbol = new SimpleFillSymbol { BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 0, 0)), Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 80, 80, 80)) };
            var blackClassBreakInfo = new ClassBreakInfo { MinimumValue = 2005, MaximumValue = 2013, Symbol = blackFillSymbol, Label = "2005-2013" };
            simpleRender.Symbol = defaultFillSymbol;
            //classBreaksRenderer.Classes.Add(brownClassBreakInfo);
            classBreaksRenderer.Classes.Add(darkbrownClassBreakInfo);
            classBreaksRenderer.Classes.Add(graybrownClassBreakInfo);
            classBreaksRenderer.Classes.Add(grayClassBreakInfo);
            classBreaksRenderer.Classes.Add(darkgrayClassBreakInfo);
            classBreaksRenderer.Classes.Add(blackClassBreakInfo);
//            classBreaksRenderer.DefaultSymbol = defaultFillSymbol;
            defaultDrawingOptions.LayerID = 4;
            defaultDrawingOptions.Renderer = classBreaksRenderer;
            layerDrawingOptionsCollection.Add(defaultDrawingOptions);
            bagLayer = new ArcGISDynamicMapServiceLayer { Url = @"http://services.arcgisonline.nl/arcgis/rest/services/Basisregistraties/BAG/MapServer", VisibleLayers = new int[] { 4 }, LayerDrawingOptions = layerDrawingOptionsCollection, ShowLegend=true, ID = "BAG" };
            _map.Layers.Add(bagLayer);
            bagLayer.Initialized += new System.EventHandler<System.EventArgs>(bagLayer_Initialized);
        }

        void bagLayer_Initialized(object sender, System.EventArgs e)
        {
            Debug.WriteLine("Bag Layer Initialized");

        }

        private void _map_ExtentChanged(object sender, ExtentEventArgs e)
        {
            Debug.WriteLine(_map.Extent);
        }
    }
}
