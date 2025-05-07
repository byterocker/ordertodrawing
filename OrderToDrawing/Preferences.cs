<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
=======
﻿using System.ComponentModel;
>>>>>>> a25158f (Initial commit)
using System.IO;
using System.Windows;

namespace OrderToDrawing
{
    public class Preferences : INotifyPropertyChanged
    {
        private string namingRule;
        private string namingRuleIgnore;
<<<<<<< HEAD
        private string csvsearchingpath;
=======
        private string connectionString;
        private string jsonSearchingPath;
>>>>>>> a25158f (Initial commit)
        private string pdssearchingpath;
        private string dwgsearchingpath;
        private string dwgoutputPath;
        private string dwgordercopypath;
        private string csvoutputPath;
        private string serialBlocks;
        private string noPDSArtikel;
        private string serialAtts;
        private string scriptfile;
        private string scriptfile2;
<<<<<<< HEAD
        private string configPath;
        private string errorpath;
        private string ignoreArtikel;
=======
        private string errorpath;
        private string configPath;
        private string ignoreArtikel;
        private bool objectIdentificatorAsBlock;
>>>>>>> a25158f (Initial commit)
        private bool logfileMode;
        private bool plot;
        private bool headerMode;

        public string NamingRuleIgnore
        {
            get { return namingRuleIgnore; }
            set
            {
                if (value != null)
                {
                    namingRuleIgnore = value.TrimEnd('"');
                    Properties.Settings.Default.NamingRuleIgnore = namingRuleIgnore;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("namingRuleIgnore");
                }

            }
        }
        public string NamingRule
        {
            get { return namingRule; }
            set
            {
                if (value != null)
                {
                    namingRule = value.TrimEnd('"');
                    Properties.Settings.Default.NamingRule = namingRule;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("NamingRule");
                }

            }
        }
        public bool HeaderMode
        {
            get { return headerMode; }
            set
            {
                headerMode = value;
                Properties.Settings.Default.HeaderMode = headerMode;
                Properties.Settings.Default.Save();
                OnPropertyChanged("LogFileMode");

            }
        }
        public bool Plot
        {
            get { return plot; }
            set
            {
                plot = value;
                Properties.Settings.Default.Plot = plot;
                Properties.Settings.Default.Save();
                OnPropertyChanged("Plot");

            }
        }
        public bool LogFileMode
        {
            get { return logfileMode; }
            set
            {
                logfileMode = value;
                Properties.Settings.Default.LogFileMode = logfileMode;
                Properties.Settings.Default.Save();
                OnPropertyChanged("LogFileMode");
<<<<<<< HEAD
                
            }
        }
        public string CSVSearchingPath
        {
            get { return csvsearchingpath; }
=======

            }
        }
        public bool ObjectIdentificatorAsBlock
        {
            get { return objectIdentificatorAsBlock; }
            set
            {
                objectIdentificatorAsBlock = value;
                Properties.Settings.Default.ObjectIdentificatorAsBlock = objectIdentificatorAsBlock;
                Properties.Settings.Default.Save();
                OnPropertyChanged("ObjectIdentificatorAsBlock");

            }
        }
        public string ConnectionString
        {
            get { return connectionString; }
            set
            {
                if (value != null)
                {
                    connectionString = value;
                    Properties.Settings.Default.ConnectionString = value;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("ConnectionString");
                }
            }
        }
        public string JsonSearchingPath
        {
            get { return jsonSearchingPath; }
>>>>>>> a25158f (Initial commit)
            set
            {
                if (value != null)
                {
                    if (!value.EndsWith("\\"))
<<<<<<< HEAD
                        csvsearchingpath = value.TrimEnd('"') + "\\";
                    else
                        csvsearchingpath = value.TrimEnd('"');
                    if (!Directory.Exists(csvsearchingpath))
                    {
                        MessageBox.Show($"Der Ordner {csvsearchingpath} existiert nicht");
                        csvsearchingpath = Properties.Settings.Default.CSVSearchingPath;
                    }
                    else
                    {
                        Properties.Settings.Default.CSVSearchingPath = csvsearchingpath;
                        Properties.Settings.Default.Save();
                        OnPropertyChanged("CSVSearchingPath");
=======
                        jsonSearchingPath = value.TrimEnd('"') + "\\";
                    else
                        jsonSearchingPath = value.TrimEnd('"');
                    if (!Directory.Exists(jsonSearchingPath))
                    {
                        MessageBox.Show($"Der Ordner {jsonSearchingPath} existiert nicht");
                        jsonSearchingPath = Properties.Settings.Default.JsonSearchingPath;
                    }
                    else
                    {
                        Properties.Settings.Default.JsonSearchingPath = jsonSearchingPath;
                        Properties.Settings.Default.Save();
                        OnPropertyChanged("JsonSearchingPath");
>>>>>>> a25158f (Initial commit)
                    }
                }
            }
        }
<<<<<<< HEAD
        public string ConfigPath
        {
            get { return configPath; }
            set
            {
                if (value == null)
                    configPath = null;
                else if (!value.EndsWith(".config"))
                    configPath = value.TrimEnd('"') + ".config";
                else
                    configPath = value.TrimEnd('"');
                if (!(configPath == null) && !File.Exists(configPath))
                {
                    MessageBox.Show($"Das Config {configPath} existiert nicht");
                    configPath = Properties.Settings.Default.ConfigPath;
                }
                else
                {
                    Properties.Settings.Default.ConfigPath = configPath;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("ConfigPath");
                }
            }
        }
=======
>>>>>>> a25158f (Initial commit)
        public string DWGSearchingPath
        {
            get { return dwgsearchingpath; }
            set
            {
                if (value != null)
                {
                    if (!value.EndsWith("\\"))
                        dwgsearchingpath = value.TrimEnd('"') + "\\";
                    else
                        dwgsearchingpath = value.TrimEnd('"');
                    if (!Directory.Exists(dwgsearchingpath))
                    {
                        MessageBox.Show($"Der Ordner {dwgsearchingpath} existiert nicht");
                        dwgsearchingpath = Properties.Settings.Default.DWGSearchingPath;
                    }
                    else
                    {
                        Properties.Settings.Default.DWGSearchingPath = dwgsearchingpath;
                        Properties.Settings.Default.Save();
                        OnPropertyChanged("DWGSearchingPath");
                    }
                }
            }
        }
        public string PDSSearchingPath
        {
            get { return pdssearchingpath; }
            set
            {
                if (value != null)
                {
                    if (!value.EndsWith("\\"))
                        pdssearchingpath = value.TrimEnd('"') + "\\";
                    else
                        pdssearchingpath = value.TrimEnd('"');
                    if (!Directory.Exists(pdssearchingpath))
                    {
                        MessageBox.Show("Der Ordner {pdssearchingpath} existiert nicht");
                        pdssearchingpath = Properties.Settings.Default.PDSSearchingPath;
                    }
                    else
                    {
                        Properties.Settings.Default.PDSSearchingPath = pdssearchingpath;
                        Properties.Settings.Default.Save();
                        OnPropertyChanged("PDSSearchingPath");
                    }
                }
            }
        }
        public string CSVOutputPath
        {
            get { return csvoutputPath; }
            set
            {
                if (value != null)
                {
                    if (!value.EndsWith("\\"))
                        csvoutputPath = value.TrimEnd('"') + "\\";
                    else
                        csvoutputPath = value.TrimEnd('"');
                    if (!Directory.Exists(csvoutputPath))
                    {
                        MessageBox.Show($"Der Ordner {csvoutputPath} existiert nicht");
                        csvoutputPath = Properties.Settings.Default.CSVOutputPath;
                    }
                    else
                    {
                        Properties.Settings.Default.CSVOutputPath = csvoutputPath;
                        Properties.Settings.Default.Save();
                        OnPropertyChanged("CSVOutputPath");
                    }
                }
            }
        }
        public string DWGOutputPath
        {
            get { return dwgoutputPath; }
<<<<<<< HEAD
            set 
=======
            set
>>>>>>> a25158f (Initial commit)
            {
                if (value != null)
                {
                    if (!value.EndsWith("\\"))
                        dwgoutputPath = value.TrimEnd('"') + "\\";
                    else
                        dwgoutputPath = value.TrimEnd('"');
                    if (!Directory.Exists(dwgoutputPath))
                    {
                        MessageBox.Show($"Der Ordner {dwgoutputPath} existiert nicht");
                        dwgoutputPath = Properties.Settings.Default.DWGOutputPath;
                    }
                    else
                    {
                        Properties.Settings.Default.DWGOutputPath = dwgoutputPath;
                        Properties.Settings.Default.Save();
                        OnPropertyChanged("DWGOutputPath");
                    }
                }
            }
        }
        public string DWGOrderCopyPath
        {
            get { return dwgordercopypath; }
            set
            {
                if (value != null)
                {
                    if (!value.EndsWith("\\"))
                        dwgordercopypath = value.TrimEnd('"') + "\\";
                    else
                        dwgordercopypath = value.TrimEnd('"');
                    if (!Directory.Exists(dwgordercopypath))
                    {
                        MessageBox.Show($"Der Ordner {dwgordercopypath} existiert nicht");
                        dwgordercopypath = Properties.Settings.Default.DWGOrderCopyPath;
                    }
                    else
                    {
                        Properties.Settings.Default.DWGOrderCopyPath = dwgordercopypath;
                        Properties.Settings.Default.Save();
                        OnPropertyChanged("DWGOrderCopyPath");
                    }
                }
            }
        }
        public string ErrorPath
        {
            get { return errorpath; }
            set
            {
                if (value != null)
                {
                    if (!value.EndsWith("\\"))
                        errorpath = value.TrimEnd('"') + "\\";
                    else
                        errorpath = value.TrimEnd('"');
                    if (!Directory.Exists(errorpath))
                    {
                        MessageBox.Show($"Der Ordner {errorpath} existiert nicht");
                        errorpath = Properties.Settings.Default.ErrorPath;
                    }
                    else
                    {
                        Properties.Settings.Default.ErrorPath = errorpath;
                        Properties.Settings.Default.Save();
                        OnPropertyChanged("ErrorPath");
                    }
                }
            }
        }
        public string NoPDSArtikel
        {
            get { return noPDSArtikel; }
            set
            {
<<<<<<< HEAD
                if (value!= null)
=======
                if (value != null)
>>>>>>> a25158f (Initial commit)
                {
                    noPDSArtikel = value.TrimEnd('"');
                    Properties.Settings.Default.NoPDSArtikel = noPDSArtikel;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("NoPDSArtikel");
                }
<<<<<<< HEAD
                
=======

>>>>>>> a25158f (Initial commit)
            }
        }
        public string IgnoreArtikel
        {
            get { return ignoreArtikel; }
            set
            {
<<<<<<< HEAD
                if (value!= null)
=======
                if (value != null)
>>>>>>> a25158f (Initial commit)
                {
                    ignoreArtikel = value.TrimEnd('"');
                    Properties.Settings.Default.IgnoreArtikel = ignoreArtikel;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("IgnoreArtikel");
                }
<<<<<<< HEAD
                
=======

>>>>>>> a25158f (Initial commit)
            }
        }
        public string SerialBlocks
        {
            get { return serialBlocks; }
            set
            {
                if (value != null)
                {
                    serialBlocks = value.TrimEnd('"');
                    Properties.Settings.Default.SerialBlocks = serialBlocks;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("SerialBlocks");
                }
            }
        }
        public string SerialAtts
        {
            get { return serialAtts; }
            set
            {
                if (value != null)
                {
                    serialAtts = value.TrimEnd('"');
                    Properties.Settings.Default.SerialAtts = serialAtts;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("SerialAtts");
                }
            }
        }
        public string ScriptFile
        {
            get { return scriptfile; }
            set
            {
                if (value == null)
                    scriptfile = null;
                else if (!value.EndsWith(".scr"))
                    scriptfile = value.TrimEnd('"') + ".scr";
                else
                    scriptfile = value.TrimEnd('"');
                if (!(scriptfile == null) && !File.Exists(scriptfile))
                {
                    MessageBox.Show($"Das ScriptFile {scriptfile} existiert nicht");
                    scriptfile = Properties.Settings.Default.ScriptFile;
                }
                else
                {
                    Properties.Settings.Default.ScriptFile = scriptfile;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("ScriptFile");
                }
            }
        }
        public string ScriptFile2
        {
            get { return scriptfile2; }
            set
            {
                if (value == null)
                    scriptfile2 = null;
                else if (!value.EndsWith(".scr"))
                    scriptfile2 = value.TrimEnd('"') + ".scr";
                else
                    scriptfile2 = value.TrimEnd('"');
                if (!(scriptfile2 == null) && !File.Exists(scriptfile2))
                {
<<<<<<< HEAD
                    MessageBox.Show($"Das ScriptFile {scriptfile} existiert nicht");
=======
                    MessageBox.Show($"Das ScriptFile {scriptfile2} existiert nicht");
>>>>>>> a25158f (Initial commit)
                    scriptfile2 = Properties.Settings.Default.ScriptFile2;
                }
                else
                {
                    Properties.Settings.Default.ScriptFile2 = scriptfile2;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("ScriptFile2");
                }
            }
        }

<<<<<<< HEAD
=======
        public string ConfigPath
        {
            get { return configPath; }
            set
            {
                if (value == null)
                    configPath = null;
                else if (!value.EndsWith(".config"))
                    configPath = value.TrimEnd('"') + ".config";
                else
                    configPath = value.TrimEnd('"');
                if (!(configPath == null) && !File.Exists(configPath))
                {
                    MessageBox.Show($"Das Config {configPath} existiert nicht");
                    configPath = Properties.Settings.Default.ConfigPath;
                }
                else
                {
                    Properties.Settings.Default.ConfigPath = configPath;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("ConfigPath");
                }
            }
        }

>>>>>>> a25158f (Initial commit)
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
<<<<<<< HEAD
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
            
=======
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

>>>>>>> a25158f (Initial commit)
    }
}
