<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
=======
﻿using System.Windows;
>>>>>>> a25158f (Initial commit)

namespace OrderToDrawing
{
    /// <summary>
    /// Interaktionslogik für WindowForPreferences.xaml
    /// </summary>
    public partial class WindowForPreferences : Window
    {
<<<<<<< HEAD
        
=======

>>>>>>> a25158f (Initial commit)
        public WindowForPreferences()
        {
            InitializeComponent();
            Preferences MyPreferences = new Preferences
            {
                CSVOutputPath = Properties.Settings.Default.CSVOutputPath,
<<<<<<< HEAD
                CSVSearchingPath = Properties.Settings.Default.CSVSearchingPath,
=======
                ConnectionString = Properties.Settings.Default.ConnectionString,
                JsonSearchingPath = Properties.Settings.Default.JsonSearchingPath,
>>>>>>> a25158f (Initial commit)
                DWGOutputPath = Properties.Settings.Default.DWGOutputPath,
                DWGOrderCopyPath = Properties.Settings.Default.DWGOrderCopyPath,
                DWGSearchingPath = Properties.Settings.Default.DWGSearchingPath,
                PDSSearchingPath = Properties.Settings.Default.PDSSearchingPath,
                SerialBlocks = Properties.Settings.Default.SerialBlocks,
                SerialAtts = Properties.Settings.Default.SerialAtts,
                ScriptFile2 = Properties.Settings.Default.ScriptFile2,
                ScriptFile = Properties.Settings.Default.ScriptFile,
                ErrorPath = Properties.Settings.Default.ErrorPath,
<<<<<<< HEAD
                LogFileMode= Properties.Settings.Default.LogFileMode,
                Plot= Properties.Settings.Default.Plot,
=======
                LogFileMode = Properties.Settings.Default.LogFileMode,
                Plot = Properties.Settings.Default.Plot,
>>>>>>> a25158f (Initial commit)
                HeaderMode = Properties.Settings.Default.HeaderMode,
                NoPDSArtikel = Properties.Settings.Default.NoPDSArtikel,
                IgnoreArtikel = Properties.Settings.Default.IgnoreArtikel,
                NamingRule = Properties.Settings.Default.NamingRule,
                NamingRuleIgnore = Properties.Settings.Default.NamingRuleIgnore,
<<<<<<< HEAD
                ConfigPath = Properties.Settings.Default.ConfigPath
=======
                ConfigPath = Properties.Settings.Default.ConfigPath,
                ObjectIdentificatorAsBlock = Properties.Settings.Default.ObjectIdentificatorAsBlock
>>>>>>> a25158f (Initial commit)
            };
            DataContext = MyPreferences;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow MW = new MainWindow();
            //MW.StartButton.IsEnabled = true;
            //MW.PrefButton.IsEnabled = true;
            Close();
<<<<<<< HEAD
            
=======

>>>>>>> a25158f (Initial commit)
        }
    }
}
