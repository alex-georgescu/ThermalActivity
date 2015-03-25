using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;


namespace ThermalActivity
{
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty SolarScoresProperty = DependencyProperty.Register("SolarScores", typeof(ObservableCollection<uint>), typeof(MainWindow), new PropertyMetadata(null));
        public ObservableCollection<uint> SolarScores
        {
            get { return (ObservableCollection<uint>)GetValue(SolarScoresProperty); }
            set { SetValue(SolarScoresProperty, value); }
        }

        

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            
            GenerateSolarScores();
        }


        public async void GenerateSolarScores()
        {
            // input data for the HeatMap was taken from Test 2 (within the requirements)

            uint t = 3;
            uint n = 4;
            uint[] sunData = { 2, 3, 2, 1, 4, 4, 2, 0, 3, 4, 1, 1, 2, 3, 4, 4 };

            Sun sun = new Sun(sunData);

            await sun.EvaluateSunSurface();
            SolarScores = sun.HeatMap;
        }
    }
}
