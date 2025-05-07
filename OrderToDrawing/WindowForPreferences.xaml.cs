using System.Windows;

namespace OrderToDrawing
{
    /// <summary>
    /// Interaktionslogik für WindowForPreferences.xaml
    /// </summary>
    public partial class WindowForPreferences : Window
    {

        public WindowForPreferences()
        {
            InitializeComponent();
            Preferences MyPreferences = new Preferences
            {
                CSVOutputPath = Properties.Settings.Default.CSVOutputPath,
                ConnectionString = Properties.Settings.Default.ConnectionString,
                JsonSearchingPath = Properties.Settings.Default.JsonSearchingPath,
                DWGOutputPath = Properties.Settings.Default.DWGOutputPath,
                DWGOrderCopyPath = Properties.Settings.Default.DWGOrderCopyPath,
                DWGSearchingPath = Properties.Settings.Default.DWGSearchingPath,
                PDSSearchingPath = Properties.Settings.Default.PDSSearchingPath,
                SerialBlocks = Properties.Settings.Default.SerialBlocks,
                SerialAtts = Properties.Settings.Default.SerialAtts,
                ScriptFile2 = Properties.Settings.Default.ScriptFile2,
                ScriptFile = Properties.Settings.Default.ScriptFile,
                ErrorPath = Properties.Settings.Default.ErrorPath,
                LogFileMode = Properties.Settings.Default.LogFileMode,
                Plot = Properties.Settings.Default.Plot,
                HeaderMode = Properties.Settings.Default.HeaderMode,
                NoPDSArtikel = Properties.Settings.Default.NoPDSArtikel,
                IgnoreArtikel = Properties.Settings.Default.IgnoreArtikel,
                NamingRule = Properties.Settings.Default.NamingRule,
                NamingRuleIgnore = Properties.Settings.Default.NamingRuleIgnore,
                ConfigPath = Properties.Settings.Default.ConfigPath,
                ObjectIdentificatorAsBlock = Properties.Settings.Default.ObjectIdentificatorAsBlock
            };
            DataContext = MyPreferences;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow MW = new MainWindow();
            //MW.StartButton.IsEnabled = true;
            //MW.PrefButton.IsEnabled = true;
            Close();

        }
    }
}
