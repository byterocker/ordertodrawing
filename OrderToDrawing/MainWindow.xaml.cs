<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.ComponentModel;
using LaunchAutoCAD;
using Autodesk.AutoCAD;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.AutoCAD.Interop;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Configuration;
=======
﻿using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using LaunchAutoCAD;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderToDrawing.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Linq;
using TC_JsonConverter;
using OrderToDrawing.Models;
>>>>>>> a25158f (Initial commit)

namespace OrderToDrawing
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int eventcounter;
        readonly DispatcherTimer myTimer = new DispatcherTimer();
        AcadConnection acadConnection = new AcadConnection();
        string[] blockNames;
        string[] noPDSNames;
        string[] ignoreArtikelNames;
        string[] serialAtts;
<<<<<<< HEAD
        bool isThreadRunning=false;
        bool isTimeToStop=false;
        Log myLog;
        Log systemLog;
        
=======
        bool isThreadRunning = false;
        bool isTimeToStop = false;
        Log myLog;
        Log myLog2;
        Log systemLog;

>>>>>>> a25158f (Initial commit)



        public MainWindow()
        {
            try
            {
<<<<<<< HEAD
                
                InitializeComponent();
                if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() == 2)
                {
                    Properties.Settings.Default.SettingsKey = "PROD100";
                    SettingsKeyTextBox.Text= "PROD100";
                    Properties.Settings.Default.Reload();
                } 
=======
                //var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;

                InitializeComponent();
                /*
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                if (File.Exists()) 
                {
                    ConfigurationManager.RefreshSection("configuration");
                    Properties.Settings.Default.Reload();
                }
                */
                Properties.Settings.Default.SettingsKey = "PROD";
                SettingsKeyTextBox.Text = "PROD";
                Properties.Settings.Default.Reload();

                if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() == 2)
                {
                    Properties.Settings.Default.SettingsKey = "TEST";
                    SettingsKeyTextBox.Text = "TEST";
                    Properties.Settings.Default.Reload();
                }

                /*
>>>>>>> a25158f (Initial commit)
                else if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() == 3)
                {
                    Properties.Settings.Default.SettingsKey = "PROD100MIG";
                    SettingsKeyTextBox.Text = "PROD100MIG";
                    Properties.Settings.Default.Reload();
                }
                else if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 3)
                {
                    string instancekey = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count().ToString();
                    Properties.Settings.Default.SettingsKey = instancekey;
                    SettingsKeyTextBox.Text = instancekey;
                    Properties.Settings.Default.Reload();
                }
<<<<<<< HEAD
                eventcounter = Properties.Settings.Default.EventCounter;
                TotalCSV.Text = eventcounter.ToString();
                TotalCSV.Refresh();
                myTimer.Interval = TimeSpan.FromSeconds(10);
                myTimer.Tick += MainProcess;
                CancelButton.IsEnabled = false;
                systemLog = new Log();
                systemLog.Open(@Properties.Settings.Default.ErrorPath, "System_Log");
=======
                */
                eventcounter = Properties.Settings.Default.EventCounter;
                TotalCSV.Text = eventcounter.ToString();
                TotalCSV.Refresh();
                myTimer.Interval = TimeSpan.FromSeconds(150);
                myTimer.Tick += MainProcess;
                CancelButton.IsEnabled = false;
                systemLog = new Log();
                systemLog.Open(@Properties.Settings.Default.ErrorPath, "System_Log_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
>>>>>>> a25158f (Initial commit)
                systemLog.LogMsg("System initialized");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
<<<<<<< HEAD
            
=======

>>>>>>> a25158f (Initial commit)

        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            try
            {
                systemLog.LogMsg("Start Button pressed");
                blockNames = Properties.Settings.Default.SerialBlocks.Split(',');
                serialAtts = Properties.Settings.Default.SerialAtts.Split(',');
                if (Properties.Settings.Default.NoPDSArtikel != null)
                {
                    noPDSNames = Properties.Settings.Default.NoPDSArtikel.Split(',');
                    noPDSNames = Array.ConvertAll(noPDSNames, d => d.ToUpper());
                }
                if (Properties.Settings.Default.IgnoreArtikel != null)
                {
                    ignoreArtikelNames = Properties.Settings.Default.IgnoreArtikel.Split(',');
                    ignoreArtikelNames = Array.ConvertAll(ignoreArtikelNames, d => d.ToUpper());
                }
                blockNames = Array.ConvertAll(blockNames, d => d.ToUpper());
                serialAtts = Array.ConvertAll(serialAtts, d => d.ToUpper());
                PrefButton.IsEnabled = false;
<<<<<<< HEAD
                StartButton.IsEnabled = false;
                CancelButton.IsEnabled = true;
                isTimeToStop = false;
                systemLog.LogMsg("starting Dispatcher-Timer...");
=======
                ReloadConfigButton.IsEnabled = false;
                StartButton.IsEnabled = false;
                CancelButton.IsEnabled = true;
                isTimeToStop = false;
                systemLog.LogMsg("starting Dispatcher-Timer...150 seconds");
>>>>>>> a25158f (Initial commit)
                myTimer.Start();
            }
            catch (Exception ex)
            {
                systemLog.LogMsg("Failure on Button_Start\n\t\t" + ex);
            }
<<<<<<< HEAD
            
            
            
=======



>>>>>>> a25158f (Initial commit)
        }
        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            try
            {
                systemLog.LogMsg("Stop Button pressed");
                isTimeToStop = true;
                CancelButton.Content = "Waiting...";
            }
            catch (Exception ex)
            {
                systemLog.LogMsg("Failure on Button_Stop\n\t\t" + ex);
            }

        }

        private void MainProcess(object sender, EventArgs e)
        {
            try
<<<<<<< HEAD
            {
=======
            {                
>>>>>>> a25158f (Initial commit)
                systemLog.LogMsg("Dispatcher Timer is executing the MainProcess");
                if (isThreadRunning == false)
                {
                    if (isTimeToStop == false)
                    {
                        systemLog.LogMsg("Thread is not running and it is not time to stop => try to start Thread...");

                        try
                        {
                            Thread t = new Thread(SearchforCSV);
                            t.SetApartmentState(ApartmentState.STA);
                            t.Start();
                        }
                        catch (Exception ex)
                        {
                            systemLog.LogMsg("Failure on starting the Thread\n\t\t" + ex);
                        }

                    }
                    else
                    {
                        systemLog.LogMsg("Thread is not running and it is time to stop the dispatcher...");
                        myTimer.Stop();
                        PrefButton.IsEnabled = true;
<<<<<<< HEAD
=======

                        ReloadConfigButton.IsEnabled = true;
>>>>>>> a25158f (Initial commit)
                        StartButton.IsEnabled = true;
                        CancelButton.IsEnabled = false;
                        CancelButton.Content = "Stop";
                    }
                }
                else
                {
                    systemLog.LogMsg("Thread is running in Background");
                }
            }
            catch (Exception ex)
            {
                systemLog.LogMsg("Failure on MainProcess\n\t\t" + ex);
            }
<<<<<<< HEAD
            
            
        }

=======


        }

        private IEnumerable<ACAD_SerialNumber> getUnprocessedOrders()
        {
            try
            {
                systemLog.LogMsg($"Connection String: {Properties.Settings.Default.ConnectionString}");

                // DbContextOptionsBuilder zum Konfigurieren der Verbindungszeichenfolge
                var optionsBuilder = new DbContextOptionsBuilder<MW_TC_LN_InterfaceContext>();
                optionsBuilder.UseSqlServer(Properties.Settings.Default.ConnectionString);

                // DbContext mit Optionen erstellen
                using (var context = new MW_TC_LN_InterfaceContext(optionsBuilder.Options))
                {
                    // EF Core-Methode zum Abrufen unverarbeiteter Bestellungen aufrufen
                    var orders = context.GetUnprocessedOrders();

                    systemLog.LogMsg($"Orders: {orders.Count()}");
                    List<ACAD_SerialNumber> orders2 = orders.ToList();

                    var orders_no_dwg = from p in orders2
                                        where !p.Drawing_No.IsMatch(Properties.Settings.Default.NamingRuleIgnore, '?', '*') && p.Drawing_No.IsMatch(Properties.Settings.Default.NamingRule, '?', '*') && p.Drawing_No == null
                                        select p;


                    var orders3 = from p in orders2
                                  where !p.Drawing_No.IsMatch(Properties.Settings.Default.NamingRuleIgnore, '?', '*') && p.Drawing_No.IsMatch(Properties.Settings.Default.NamingRule, '?', '*') && p.Drawing_No != null
                                  select p;
                    foreach (var item in orders3)
                    {
                        item.PO_Number = item.PO_Number.Trim();
                        item.PO_Position = item.PO_Position.Trim();
                        item.SerialNumber = item.SerialNumber.Trim();
                        item.SO_Position = item.SO_Position.Trim();
                        item.Drawing_No = item.Drawing_No.Trim();
                        item.PO_Sequence = item.PO_Sequence.Trim();

                    }
                    foreach (var item in orders_no_dwg)
                    {
                        Log errorLog = new Log();
                        errorLog.Open(@Properties.Settings.Default.ErrorPath, Path.GetFileNameWithoutExtension(item.PO_Number));
                        errorLog.LogMsg($"{item.PO_Number}-{item.PO_Position} has no Drawing_Number! => Skipping");
                        errorLog.Close();
                    }
                    systemLog.LogMsg(orders2.Count().ToString());
                    return orders3;
                }
            }
            catch (Exception ex)
            {
                systemLog.LogMsg("Failure on SQL Reading\n\t\t" + ex);
                return null;
            }
        }

        private void MarkInSQL(List<ACAD_SerialNumber> liste, string status)
        {
            try
            {
                // DbContextOptionsBuilder zum Konfigurieren der Verbindungszeichenfolge
                var optionsBuilder = new DbContextOptionsBuilder<MW_TC_LN_InterfaceContext>();
                optionsBuilder.UseSqlServer(Properties.Settings.Default.ConnectionString);

                // DbContext mit Optionen erstellen
                using (var context = new MW_TC_LN_InterfaceContext(optionsBuilder.Options))
                {
                    // Aufrufen der EF Core-Methode zum Markieren von Bestellungen
                    context.MarkOrdersWithStatus(liste, status);
                }
            }
            catch (Exception ex)
            {
                myLog.LogMsg("Failure on MarkAsDone\n\t\t" + ex);
            }
        }

        private void SearchAndMoveTCRequestsDWG()
        {
            try
            {
                // DbContextOptionsBuilder zum Konfigurieren der Verbindungszeichenfolge
                var optionsBuilder = new DbContextOptionsBuilder<MW_TC_LN_InterfaceContext>();
                optionsBuilder.UseSqlServer(Properties.Settings.Default.ConnectionString);

                // DbContext mit Optionen erstellen
                using (var context = new MW_TC_LN_InterfaceContext(optionsBuilder.Options))
                {
                    var todo = context.GetNewTCRequests();

                    if (!todo.Any())
                    {
                        Debug.WriteLine("No new TC Requests");
                    }

                    foreach (var item in todo)
                    {
                        myLog2 = new Log();
                        myLog2.Delete(@Properties.Settings.Default.ErrorPath, item.PO_Number + "_" + item.Drawing_Number + "_ImportToTC");
                        myLog2.Open(@Properties.Settings.Default.ErrorPath, item.PO_Number + "_" + item.Drawing_Number + "_ImportToTC");
                        Debug.WriteLine($"Trying to find DWG {item.Drawing_Number}, because it needs to be imported for {item.PO_Number}");
                        myLog2.LogMsg($"Trying to find DWG {item.Drawing_Number}, because it needs to be imported for {item.PO_Number}");
                        var dwgpath = FindDWGPathforTC(item);

                        if (dwgpath == null)
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"Found no DWG for {item.Drawing_Number}");
                            continue;
                        }

                        try
                        {
                            myLog2.LogMsg($"Trying to copy DWG - {dwgpath}");
                            File.Copy(dwgpath, @"\\wipfil32\Process_WorkingDir\01-zum_bearbeiten_TC\import_without_pds\" + item.Drawing_Number + ".dwg");
                        }
                        catch (Exception ex)
                        {
                            myLog2.LogMsg($"Error while copying - {dwgpath}");
                        }

                        myLog2.LogMsg($"Marking SQL Entry as done");
                        try
                        {
                            // Rufen Sie die EF Core-Methode zum Markieren von TC-Anforderungen als erledigt auf
                            context.MarkTCRequestAsDone(item.PO_Number, item.Drawing_Number);
                            myLog2.LogMsg($"Successfully marked as done: {item.PO_Number} - {item.Drawing_Number}");
                        }
                        catch (Exception ex)
                        {
                            myLog2.LogMsg($"Error while updating status in database: {ex.Message}");
                        }
                        myLog2.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                systemLog.LogMsg("Failure on MarkAsDone\n\t\t" + ex);
                systemLog.LogMsg(ex.StackTrace);
            }
        }
>>>>>>> a25158f (Initial commit)
        private void SearchforCSV()
        {
            try
            {
                isThreadRunning = true;
<<<<<<< HEAD
                systemLog.LogMsg("Searching for CSV Files");
=======
                systemLog.LogMsg("Searching for new Entrys");
>>>>>>> a25158f (Initial commit)
                // This path is a directory
                /*
                string[] fileEntries = Directory.GetFiles(@Properties.Settings.Default.CSVSearchingPath, Properties.Settings.Default.NamingRule + "*.csv");
                var files = Directory.GetFiles(@Properties.Settings.Default.CSVSearchingPath)
                            .Where(fn => !Path.GetFileName(fn).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                            .ToArray();
<<<<<<< HEAD
                */
                string[] fileEntries = (from p in Directory.GetFiles(@Properties.Settings.Default.CSVSearchingPath)
                                        where !Path.GetFileName(p).IsMatch(Properties.Settings.Default.NamingRuleIgnore, '?', '*') && Path.GetFileName(p).IsMatch(Properties.Settings.Default.NamingRule, '?', '*')
                                        select p).ToArray();
                int filesInFolder = fileEntries.Length;
                systemLog.LogMsg($"{fileEntries.Length} CSV's to process...");
                foreach (string fileName in fileEntries)
                {
                    string workingName = Path.ChangeExtension(fileName, ".lockedcsv");
=======
                */                
                SearchAndMoveTCRequestsDWG();
                
                var newOrders = getUnprocessedOrders().ToList();
                List<List<ACAD_SerialNumber>> sortedNewOrdersbyPO = newOrders.SeperateByPO_Number<List<ACAD_SerialNumber>>();
                /*
                string[] fileEntries = (from p in Directory.GetFiles(@Properties.Settings.Default.CSVSearchingPath)
                                        where !Path.GetFileName(p).IsMatch(Properties.Settings.Default.NamingRuleIgnore, '?', '*') && Path.GetFileName(p).IsMatch(Properties.Settings.Default.NamingRule, '?', '*')
                                        select p).ToArray();
                */
                int filesInFolder = sortedNewOrdersbyPO.Count();
                systemLog.LogMsg($"{filesInFolder} CSV's to process...");
                foreach (List<ACAD_SerialNumber> order in sortedNewOrdersbyPO)
                {

                    //string workingName = Path.ChangeExtension(order.First().PO_Number, ".lockedcsv");
>>>>>>> a25158f (Initial commit)
                    if (isTimeToStop == true) break;
                    if (Properties.Settings.Default.LogFileMode == true)
                    {
                        myLog = new Log();
<<<<<<< HEAD
                        myLog.Open(@Properties.Settings.Default.ErrorPath, Path.GetFileNameWithoutExtension(fileName));
                    }
                    CSVNumber.Refresh_txtbox(filesInFolder.ToString());
=======
                        myLog.Delete(@Properties.Settings.Default.ErrorPath, Path.GetFileNameWithoutExtension(order.First().PO_Number));
                        myLog.Open(@Properties.Settings.Default.ErrorPath, Path.GetFileNameWithoutExtension(order.First().PO_Number));
                    }
                    CSVNumber.Refresh_txtbox(filesInFolder.ToString());
                    /*
>>>>>>> a25158f (Initial commit)
                    try
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"CSV wird von {fileName} auf {workingName} umbenannt.");
                        File.Move(fileName, workingName);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
<<<<<<< HEAD
                    ProcessFile(workingName, fileEntries.Length);
=======
                    */
                    ProcessOrder(order, order.First().PO_Number, filesInFolder);
>>>>>>> a25158f (Initial commit)
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Process File Function completed, closing log");
                    if (Properties.Settings.Default.LogFileMode == true)
                        myLog.Close();
                    filesInFolder--;
                }
<<<<<<< HEAD
                
=======

>>>>>>> a25158f (Initial commit)
            }
            catch (Exception ex)
            {
                systemLog.LogMsg("Failure on searching for CSV Files\n\t\t" + ex);
            }
            finally
            {
                systemLog.LogMsg("Finished. Check if AutoCad is running, if yes => closing");
                //if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Closing the AutoCad Connection");
                acadConnection.Quit();
                // Anzeige:
                CSVNumber.Refresh_txtbox("0");
                CSV_File.Refresh_txtbox("");
                lbActualCSV.Refresh_listbox(null);
                ProgressBar.Refresh_progressbar(0);
                CurrentDWG.Refresh_txtbox("");
                lbActualDrawing.Refresh_listbox(null);
                isThreadRunning = false;
                //Anzeige Ende */
                systemLog.LogMsg("Thread completed");
            }

        }

<<<<<<< HEAD
        private void ProcessFile(string name, int generalDrawings)
=======
        private void ProcessOrder(List<ACAD_SerialNumber> myCSVData, string name, int generalDrawings)
>>>>>>> a25158f (Initial commit)
        {
            try
            {
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Processing {name}");
<<<<<<< HEAD
                List<OrderObject> myCSVData = ReadmyCSVData(name);
                if (myCSVData == null) return;
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Successfully read the CSV");
                List<List<OrderObject>> ToProcess = myCSVData.Seperate<List<OrderObject>>();
=======
                //List<OrderObject> myCSVData = ReadmyCSVData(name);
                if (myCSVData == null) return;
                //if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Successfully read the CSV");
                List<List<ACAD_SerialNumber>> ToProcess = myCSVData.Seperate<List<ACAD_SerialNumber>>();
>>>>>>> a25158f (Initial commit)
                if (ToProcess == null) return;
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Successfully seperated the CSV");
                int CSVLength = ToProcess.Count;
                int Progress = 0;
                // Anzeige:
                CSV_File.Refresh_txtbox(name);
                lbActualCSV.Refresh_listbox(myCSVData);
                ProgressBar.SetMaximum(CSVLength);
                ProgressBar.Refresh_progressbar(Progress);
                //Anzeige Ende
<<<<<<< HEAD
                foreach (List<OrderObject> Drawing in ToProcess)
                {
                    // Anzeige:
                    ProgressBar.Refresh_progressbar(++Progress);
                    CurrentDWG.Refresh_txtbox(Drawing[0].AnlagenReferenz);
                    lbActualDrawing.Refresh_listbox(Drawing);
                    //Anzeige Ende
                    string newDWGname = (@Properties.Settings.Default.DWGOutputPath + Drawing[0].OrderId + "~" + Drawing[0].AnlagenReferenz + "~" + Drawing[0].AnlagenSequenz + "~Bestellskizze");
                    string newDWGname2 = (@Properties.Settings.Default.DWGOrderCopyPath + Drawing[0].OrderId + "~" + Drawing[0].AnlagenReferenz + "~" + Drawing[0].AnlagenSequenz + "~Bestellskizze");
=======
                foreach (List<ACAD_SerialNumber> Drawing in ToProcess)
                {
                    // Anzeige:
                    ProgressBar.Refresh_progressbar(++Progress);
                    CurrentDWG.Refresh_txtbox(Drawing[0].PO_Number);
                    lbActualDrawing.Refresh_listbox(Drawing);
                    //Anzeige Ende
                    string newDWGname = (@Properties.Settings.Default.DWGOutputPath + Drawing[0].PO_Number + "~" + Drawing[0].Drawing_No + "~" + Drawing[0].SO_Position + "~Bestellskizze~COIN");
                    string newDWGname2 = (@Properties.Settings.Default.DWGOrderCopyPath + Drawing[0].PO_Number + "~" + Drawing[0].Drawing_No + "~" + Drawing[0].SO_Position + "~Bestellskizze~COIN");
>>>>>>> a25158f (Initial commit)

                    if (File.Exists(newDWGname + ".dwg"))
                    {
                        try
                        {
                            FileInfo fInfo = new FileInfo(newDWGname + ".dwg")
                            {
                                IsReadOnly = false
                            };
                            File.Delete(newDWGname + ".dwg");
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Deleted {newDWGname}.dwg");
                        }
                        catch (Exception ex)
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error while deleting old DWG {newDWGname}, skipping this dwg\n\t\t" + ex);
                            continue;
                        }
                    }
                    else
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"No existing DWG {newDWGname}");
                    }
                    if (File.Exists(newDWGname + ".pdf"))
                    {
                        try
                        {
                            FileInfo fInfo = new FileInfo(newDWGname + ".pdf")
                            {
                                IsReadOnly = false
                            };
                            File.Delete(newDWGname + ".pdf");
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Deleted {newDWGname}.pdf");
                        }
                        catch (Exception ex)
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error while deleting old PDF {newDWGname}\n\t\t" + ex);
<<<<<<< HEAD
                            
=======

>>>>>>> a25158f (Initial commit)
                        }
                    }
                    else
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"No existing DWG {newDWGname}");
                    }
                    if (File.Exists(newDWGname2 + ".dwg"))
                    {
                        try
                        {
                            FileInfo fInfo = new FileInfo(newDWGname2 + ".dwg");
                            fInfo.IsReadOnly = false;
                            File.Delete(newDWGname2 + ".dwg");
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Deleted {newDWGname2}.dwg");
                        }
                        catch (Exception ex)
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error while deleting old DWG {newDWGname2}, skipping this dwg\n\t\t" + ex);
                            continue;
                        }
                    }
                    else
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"No existing DWG {newDWGname2}");
                    }

                    //Fall 3 - DWG wird gnoriert
<<<<<<< HEAD
                    if (ignoreArtikelNames != null && ignoreArtikelNames.Any(x => Drawing[0].ArtikelCode.ToUpper().IsMatch(x, '?', '*')))
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Ignoring {Drawing[0].ArtikelCode}, because it matches to the IgnoreArtikel");
=======
                    if (ignoreArtikelNames != null && ignoreArtikelNames.Any(x => Drawing[0].Item_Id.ToUpper().IsMatch(x, '?', '*')))
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Ignoring {Drawing[0].Item_Id}, because it matches to the IgnoreArtikel");
>>>>>>> a25158f (Initial commit)
                        continue;
                    }


<<<<<<< HEAD
                    if (noPDSNames != null && noPDSNames.Any(x => Drawing[0].ArtikelCode.ToUpper().IsMatch(x, '?', '*')))
                    {
                        // Fall 2: DWG wird direkt übergeben
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"The drawing {Drawing[0].AnlagenReferenz} has no attributes to change");
                        string dwgpath = FindDWGinDir(Drawing[0].AnlagenReferenz);
=======
                    if (noPDSNames != null && noPDSNames.Any(x => Drawing[0].Item_Id.ToUpper().IsMatch(x, '?', '*')))
                    {
                        // Fall 2: DWG wird direkt übergeben
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"The drawing {Drawing[0].Drawing_No} has no attributes to change");
                        string dwgpath = FindDWGinDir(Drawing[0].Drawing_No);
>>>>>>> a25158f (Initial commit)
                        if (dwgpath == null) continue;
                        try
                        {
                            File.Copy(dwgpath, newDWGname + ".dwg");
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Copied the dwg to {newDWGname}");
                        }
                        catch (Exception ex)
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error while copying the NoPDS Drawing {dwgpath} to {newDWGname}\n\t\t" + ex);
                        }
                    }
                    else
                    {
                        // Fall 1: Autocad vergibt CSV und speichert neue DWG
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("This drawing has attributes to change");
<<<<<<< HEAD
                        string dwgpath = FindDWGPath(Drawing[0].AnlagenReferenz);
                        if (dwgpath == null) continue;
=======
                        string dwgpath = FindDWGPath(Drawing[0].Drawing_No);

                        // Überspringen, wenn kein DWG oder kein Json vorhanden ist
                        if (dwgpath == null)
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found no DWG for {Drawing[0].Drawing_No}");
                            continue;
                        }
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Searching for Json in {Properties.Settings.Default.JsonSearchingPath} with dwgname = {Drawing[0].Drawing_No}");
                        List<string> JsonfileEntries = null;
                        if (Drawing[0].Status == "change")
                        {
                            var directory = new DirectoryInfo(@Properties.Settings.Default.JsonSearchingPath);
                            JsonfileEntries = (from f in directory.GetFiles()
                                         where f.Name.ToUpper().StartsWith(Drawing[0].Drawing_No + "~") && f.Name.ToUpper().EndsWith(".JSON")
                                         && f.LastWriteTime > Drawing[0].Timestamp
                                         select f.FullName).ToList();
                            if (JsonfileEntries != null)
                            {
                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Orderstatus is change an there where {JsonfileEntries.Count()} which are newer than the given timestamp {Drawing[0].Timestamp}");
                            }
                            else
                            {
                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Orderstatus is change an there where no Jsons which are newer than the given timestamp {Drawing[0].Timestamp}");
                            }


                        }
                        else
                        {
                            JsonfileEntries = Directory.GetFiles(@Properties.Settings.Default.JsonSearchingPath, Drawing[0].Drawing_No + "~*.json").ToList();
                        }
                        
                        if (!JsonfileEntries.Any())
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found no Json for {Drawing[0].Drawing_No} in {@Properties.Settings.Default.JsonSearchingPath}");
                            continue;
                        }
                        else
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"There are Jsons => do not skip");
                        }

                        List<CT4Modul> objectGroups = GenerateObjectGroup(Drawing[0].Drawing_No, Drawing[0].PO_Number, JsonfileEntries);

>>>>>>> a25158f (Initial commit)
                        AcadApplication AcadApp = acadConnection.AcadApp;
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Successfully started a AutoCad Session");
                        if (AcadApp != null)
                        {
                            //Autocad wurde gestartet und es geht los
                            AcadDocument oDoc = AcadApp.Documents.Open(dwgpath, true);
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Opened the drawing in AutoCad");
                            oDoc.Activate();
<<<<<<< HEAD
                            ProcessInAutoCad(oDoc, Drawing, newDWGname2, newDWGname);
=======
                            ProcessInAutoCad(oDoc, Drawing, objectGroups, newDWGname2, newDWGname);
>>>>>>> a25158f (Initial commit)
                            try
                            {
                                File.Copy(newDWGname2 + ".dwg", newDWGname + ".dwg");
                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Copied the dwg to {newDWGname}");
                            }
                            catch (Exception ex)
                            {
                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error while copying the Drawing {newDWGname2} to {newDWGname}\n\t\t" + ex);
                            }

                        }
                    }
<<<<<<< HEAD
                }
                MoveCSV(name);
=======
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Marking the Drawing {Drawing[0].Drawing_No} as done in database");
                    MarkInSQL(Drawing, "done");
                }

                //MoveCSV(name);
>>>>>>> a25158f (Initial commit)
                eventcounter++;
                TotalCSV.Refresh_txtbox(eventcounter.ToString());
                Properties.Settings.Default.EventCounter = eventcounter;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                myLog.LogMsg("Failure on ProcessFile\n\t\t" + ex);
<<<<<<< HEAD
            }
            
            
=======
                //MarkInSQL(myCSVData, ex.InnerException.ToString());
            }


        }

        private List<CT4Modul> GenerateObjectGroup(string dwgName, string PO_Number, List<string> fileEntries)
        {
            try
            {
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Check if Json exists...");
                //string jsonName = @Properties.Settings.Default.JsonSearchingPath + dwgName + "_updated.json";
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Searching for Json in {Properties.Settings.Default.JsonSearchingPath} with dwgname = {dwgName}");
                if (!fileEntries.Any())
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"fileEntries disappeared??");

                }
                int actualMbId = 0;
                int actualMbRev = 0;
                string actualFileName = "";
                foreach (var item in fileEntries)
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"{item}");

                    string fileName = Path.GetFileNameWithoutExtension(item);
                    var jsonNameData = fileName.Split('~');
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Analyzing newest updated json MB ID={jsonNameData[1]} MB Rev={jsonNameData[2]} - {item}");

                    int.TryParse(Regex.Replace(jsonNameData[1], "[A-Za-z ]", ""), out int mBiD);
                    int.TryParse(jsonNameData[2], out int mBRev);
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Analyzing newest updated json MB ID={mBiD} MB Rev={mBRev} - {item}");

                    if (mBiD != 0 && mBiD >= actualMbId && mBRev != 0 && mBRev >= actualMbRev)
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Setting {item} to actual");
                        actualMbId = mBiD;
                        actualMbRev = mBRev;
                        actualFileName = item;
                    }
                    else if (string.IsNullOrWhiteSpace(actualFileName))
                    {
                        actualFileName = item;
                    }

                }
                string jsonName = actualFileName;

                if (string.IsNullOrWhiteSpace(jsonName))
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found no Json for after analysing found files {dwgName}");
                    return null;
                }
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found a Json {jsonName}");

                //Search for Json in "DONE" - Workaround for AcadHandle
                string jsonName2 = @"\\wiptcr05\sbomi\done\" + dwgName + ".json";
                CT4MB jsonObject2 = null;
                if (File.Exists(jsonName2))
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found Json in DONE for after analysing found files {dwgName} - just for acadHandle - {jsonName2}");
                    string json2 = File.ReadAllText(@jsonName2, Encoding.UTF8);
                    jsonObject2 = JsonConvert.DeserializeObject<CT4MB>(json2);

                }
                else
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found no Json in DONE for after analysing found files {dwgName} - just for acadHandle - {jsonName2}");
                }

                //

                string json = File.ReadAllText(jsonName, Encoding.UTF8);
                CT4MB jsonObject = JsonConvert.DeserializeObject<CT4MB>(json);
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Successfully read and deserialized the Json");
                List<CT4Modul> resultForLN = new List<CT4Modul>();
                List<CT4Modul> resultForAcad = new List<CT4Modul>();

                var objectgroups = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("U", "MD0000252"),  //Kabelkanal
                    new KeyValuePair<string, string>("F", "MD0000249"),  //Eckabdeckleiste
                    new KeyValuePair<string, string>("E", "MD0000250"),  //Sesselleiste
                    new KeyValuePair<string, string>("D", "MD0000251"),  //obere Abdeckleiste
                    new KeyValuePair<string, string>("B", "MD0000266"),  //Klima
                    new KeyValuePair<string, string>("A", "MD0000267"), //Konvektor
                    new KeyValuePair<string, string>("C", "MD0000268") //Heizlüfter
                };
                var objectgroupsWithAcad = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("P", "MD0000212,MD0000243,MD0000332,MD0000329,MD0000330,MD0000331,MD0000602,MD0000603,MD0000607"),   //Paneele
                    new KeyValuePair<string, string>("T", "MD0000246")   //Trennwände
                };
                foreach (var objectgroup in objectgroups)
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"ObjectGroup:{objectgroup.Key} - {objectgroup.Value}");
                    var matchID = objectgroup.Value.Split(',');
                    var entrysToInspect = jsonObject.FindAll(x => matchID.Any(y => y.Equals(x.GenericObjectID)));
                    if (entrysToInspect != null)
                    {
                        var separatedEntrysToInspect = entrysToInspect.Seperate<TcObject>();

                        for (int i = 0; i < separatedEntrysToInspect.Count(); i++)
                        {
                            string Group_Identificator = objectgroup.Key + (i + 1);

                            CT4Modul module = separatedEntrysToInspect[i].First() as CT4Modul;
                            module.Group_Identificator = Group_Identificator;
                            resultForLN.Add(module);
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"The Module ObjectName:{module.ObjectName} with AcadHandle={module.AcadHandle} | FindNo={module.FindNo} | ObjectID={module.ObjectID} | GenericObjectID={module.GenericObjectID} gets the Group_Identificator {Group_Identificator}");
                        }
                    }
                }
                foreach (var objectgroup in objectgroupsWithAcad)
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"ObjectGroup:{objectgroup.Key} - {objectgroup.Value}");
                    var matchID = objectgroup.Value.Split(',');
                    var entrysToInspect = jsonObject.FindAll(x => matchID.Any(y => y.Equals(x.GenericObjectID)));
                    if (entrysToInspect != null)
                    {
                        var separatedEntrysToInspect = entrysToInspect.Seperate<TcObject>();

                        for (int i = 0; i < separatedEntrysToInspect.Count(); i++)
                        {
                            string Group_Identificator = objectgroup.Key + (i + 1);

                            CT4Modul module = separatedEntrysToInspect[i].First() as CT4Modul;
                            module.Group_Identificator = Group_Identificator;
                            resultForLN.Add(module);
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"The Module ObjectName:{module.ObjectName} with AcadHandle={module.AcadHandle} | FindNo={module.FindNo} | ObjectID={module.ObjectID} | GenericObjectID={module.GenericObjectID} gets the Group_Identificator {Group_Identificator}");
                        }
                        foreach (var item in entrysToInspect)
                        {
                            CT4Modul module = item as CT4Modul;
                            CT4Modul singleObjectGroup = resultForLN.Find(x => x.ObjectID == item.ObjectID);
                            //if (string.IsNullOrEmpty(module.AcadHandle) && jsonObject2 != null)   jsonObject
                            if (jsonObject2 != null)
                            {
                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"AcadHandle is not written in UpdatedJson for Module with ID {module.ObjectID}. Searching in DONE-Json");

                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Parent = {module.Parent.ObjectName}");

                                var foundObjectForAcadHandles = jsonObject2.FindAll(x => x.GenericObjectID == module.GenericObjectID && module.Coordinates == (x as CT4Modul).Coordinates && (x as CT4Modul).VariantRules.Equals(module.VariantRules) ); //&& x.Parent != null && x.Parent.Type == ObjectType.CT4Cabin && (x.Parent as CT4Modul).Coordinates == (module.Parent as CT4Modul).Coordinates
                                foreach (var foundObjectForAcadHandle in foundObjectForAcadHandles)
                                {
                                    if (foundObjectForAcadHandle.Parent != null && module.Parent != null && foundObjectForAcadHandle.Parent.Coordinates == module.Parent.Coordinates)
                                    {
                                        if (Properties.Settings.Default.LogFileMode == true && string.IsNullOrEmpty(module.AcadHandle)) myLog.LogMsg($"Set AcadHandle from {module.AcadHandle} to {foundObjectForAcadHandle.AcadHandle}");
                                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Set AcadHandle to {foundObjectForAcadHandle.AcadHandle}");
                                        module.AcadHandle = foundObjectForAcadHandle.AcadHandle;
                                    }
                                        
                                }
                            }
                            if (singleObjectGroup != null)
                            {
                                module.Group_Identificator = singleObjectGroup.Group_Identificator;
                                resultForAcad.Add(module);
                            }

                        }
                    }
                }
                //var entrysToInspect = jsonObject.FindAll(x => x.GenericObjectID == "MD0000212" || x.GenericObjectID == "MD0000243" || x.GenericObjectID == "MD0000332" || x.GenericObjectID == "MD0000329" || x.GenericObjectID == "MD0000331" || x.GenericObjectID == "MD0000330");
                /*
                foreach (var item in entrysToInspect)
                {
                    MessageBox.Show($"{item.ObjectName},{item.ObjectID},{item.GenericObjectID},{item.AcadHandle}");
                }
                */
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Generated the ObjectGroups. Now push to SQL.\n\t\t");
                if (resultForLN != null)
                {
                    // DbContextOptionsBuilder zum Konfigurieren der Verbindungszeichenfolge
                    var optionsBuilder = new DbContextOptionsBuilder<MW_TC_LN_InterfaceContext>();
                    optionsBuilder.UseSqlServer(Properties.Settings.Default.ConnectionString);

                    // DbContext mit Optionen erstellen
                    using (var context = new MW_TC_LN_InterfaceContext(optionsBuilder.Options))
                    {
                        var foundEntrys = context.GetGroup_Identificators(dwgName, PO_Number);

                        if (Properties.Settings.Default.LogFileMode == true)
                            myLog.LogMsg($"Searching for Entries with matching drawing no ({dwgName}) and po number ({PO_Number})");

                        // Vorhandene Einträge als ungültig markieren
                        context.MarkGroup_IdentificatorsAsInvalid(foundEntrys);

                        foreach (var item in foundEntrys)
                        {
                            if (Properties.Settings.Default.LogFileMode == true)
                                myLog.LogMsg($"Found {item.ACAD_DrawingNo}-{item.PO_Number}-{item.Group_Identificator}-{item.TC_SolutionID}");
                        }

                        foreach (CT4Modul element in resultForLN)
                        {
                            if (Properties.Settings.Default.LogFileMode == true)
                                myLog.LogMsg($"handling {element.Group_Identificator}-{element.ObjectID}");

                            ACAD_GroupIdentificator existingEntry = null;

                            if (foundEntrys.Any())
                            {
                                existingEntry = context.FindGroup_Identificator(element.Group_Identificator, foundEntrys);
                            }

                            if (existingEntry != null)
                            {
                                if (Properties.Settings.Default.LogFileMode == true)
                                    myLog.LogMsg($"Found existing Entry => update");

                                context.UpdateGroup_Identificator(existingEntry, element.ObjectID);
                            }
                            else
                            {
                                if (Properties.Settings.Default.LogFileMode == true)
                                    myLog.LogMsg($"No existing Entry => create");

                                ACAD_GroupIdentificator newEntry = new ACAD_GroupIdentificator()
                                {
                                    ACAD_DrawingNo = jsonObject.DesignNo,
                                    Group_Identificator = element.Group_Identificator,
                                    TC_SolutionID = element.ObjectID,
                                    created = DateTime.Now,
                                    modified = DateTime.Now,
                                    Status = "valid",
                                    PO_Number = PO_Number
                                };

                                context.AddGroup_Identificator(newEntry);
                            }
                        }
                    }

                    if (Properties.Settings.Default.LogFileMode == true)
                        myLog.LogMsg($"Pushed successfully in Database\n\t\t");
                }

                return resultForAcad;
            }
            catch (Exception ex)
            {
                myLog.LogMsg("Failure on GenerateObjectGroup\n\t\t" + ex);
                return null;
            }
        }
        private string FindDWGPathforTC(ACAD_TC_Imports entry)
        {
            string fname = entry.Drawing_Number;   
            
            
            //MessageBox.Show(fname);
            string pdsname = @"\\wipfil32\Process_WorkingDir\02-tagesgeschaeft\" + fname + ".pds";
            if (File.Exists(pdsname))
            {
                try
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"Found a .pds file {pdsname}");
                    string pdstext = System.IO.File.ReadAllText(@pdsname);
                    var values = pdstext.Split(',');
                    string dwgpath = values[1];
                    dwgpath = dwgpath.Replace("\r\n", "");
                    dwgpath = dwgpath.Replace("'", "");
                    dwgpath = dwgpath.Replace("\"", "");
                    while (dwgpath.EndsWith(" "))
                    {
                        dwgpath = dwgpath.Remove(dwgpath.Length - 1);
                    }
                    if (File.Exists(dwgpath))
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"Found the dwg {dwgpath}");
                        return dwgpath;
                    }
                    else
                    {
                        myLog2.LogMsg($"");
                        if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"Das in dem PDS File deklarierte DWG existiert nicht - {dwgpath}, searching for DWG");
                        try
                        {
                            string[] dwgpaths = Directory.GetFiles(@"\\wipfil32\2D_Zeichnungen\", fname + ".dwg", SearchOption.AllDirectories);
                            if (dwgpaths != null && dwgpaths.Length != 0)
                            {
                                if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"Found the dwg {dwgpaths[0]}");
                                return dwgpaths[0];
                            }
                            else
                            {
                                if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"Found no dwg in \\wipfil32\\2D_Zeichnungen");
                                return null;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"Error while searching \n\t\t" + ex);
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"Error while reading the {pdsname}\n\t\t" + ex);
                    return null;
                }
            }
            else
            {
                if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"There is no PDS File {pdsname}, searching for DWG in \\wipfil32\\2D_Zeichnungen");
                try
                {
                    string[] dwgpaths = Directory.GetFiles(@"\\wipfil32\2D_Zeichnungen\", fname + ".dwg", SearchOption.AllDirectories);
                    if (dwgpaths != null && dwgpaths.Length != 0)
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"Found the dwg {dwgpaths[0]}");
                        return dwgpaths[0];
                    }
                    else
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"Found no dwg in \\wipfil32\\2D_Zeichnungen");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog2.LogMsg($"Error while searching \n\t\t" + ex);
                    return null;
                }
            }
>>>>>>> a25158f (Initial commit)
        }
        private string FindDWGPath(string fname)
        {
            //MessageBox.Show(fname);
            string pdsname = @Properties.Settings.Default.PDSSearchingPath + fname + ".pds";
            if (File.Exists(pdsname))
            {
                try
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found a .pds file {pdsname}");
                    string pdstext = System.IO.File.ReadAllText(@pdsname);
                    var values = pdstext.Split(',');
                    string dwgpath = values[1];
                    dwgpath = dwgpath.Replace("\r\n", "");
                    dwgpath = dwgpath.Replace("'", "");
                    dwgpath = dwgpath.Replace("\"", "");
                    while (dwgpath.EndsWith(" "))
                    {
                        dwgpath = dwgpath.Remove(dwgpath.Length - 1);
                    }
                    if (File.Exists(dwgpath))
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found the dwg {dwgpath}");
                        return dwgpath;
<<<<<<< HEAD
                    } 
=======
                    }
>>>>>>> a25158f (Initial commit)
                    else
                    {
                        myLog.LogMsg($"");
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Das in dem PDS File deklarierte DWG existiert nicht - {dwgpath}, searching for DWG");
                        try
                        {
                            string[] dwgpaths = Directory.GetFiles(@Properties.Settings.Default.DWGSearchingPath, fname + ".dwg", SearchOption.AllDirectories);
                            if (dwgpaths != null && dwgpaths.Length != 0)
                            {
                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found the dwg {dwgpaths[0]}");
                                return dwgpaths[0];
                            }
                            else
                            {
                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found no dwg in {Properties.Settings.Default.DWGSearchingPath}");
                                return null;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error while searching \n\t\t" + ex);
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error while reading the {pdsname}\n\t\t" + ex);
                    return null;
                }
            }
            else
            {
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"There is no PDS File {pdsname}, searching for DWG in {Properties.Settings.Default.DWGSearchingPath}");
                try
                {
                    string[] dwgpaths = Directory.GetFiles(@Properties.Settings.Default.DWGSearchingPath, fname + ".dwg", SearchOption.AllDirectories);
                    if (dwgpaths != null && dwgpaths.Length != 0)
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found the dwg {dwgpaths[0]}");
                        return dwgpaths[0];
                    }
                    else
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found no dwg in {Properties.Settings.Default.DWGSearchingPath}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error while searching \n\t\t" + ex);
                    return null;
                }
            }
        }
<<<<<<< HEAD
        private string FindDWGinDir (string dwgname)
=======
        private string FindDWGinDir(string dwgname)
>>>>>>> a25158f (Initial commit)
        {
            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Searching for DWG in {Properties.Settings.Default.DWGSearchingPath}");
            try
            {
                string[] dwgpaths = Directory.GetFiles(@Properties.Settings.Default.DWGSearchingPath, dwgname + ".dwg", SearchOption.AllDirectories);
                if (dwgpaths != null && dwgpaths.Length != 0)
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found the dwg {dwgpaths[0]}");
                    return dwgpaths[0];
                }
                else
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found no dwg in {Properties.Settings.Default.DWGSearchingPath}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error while searching \n\t\t" + ex);
                return null;
            }
<<<<<<< HEAD
            
            
        }

        private void ProcessInAutoCad(AcadDocument oDoc, List<OrderObject> Drawing, string newFname, string pdfName)
=======


        }

        private void ProcessInAutoCad(AcadDocument oDoc, List<ACAD_SerialNumber> Drawing, List<CT4Modul> objectGroups, string newFname, string pdfName)
>>>>>>> a25158f (Initial commit)
        {
            try
            {
                if (@Properties.Settings.Default.ScriptFile != null)
                {
                    oDoc.SendCommand("filedia" + (char)13 + "0" + (char)13);
                    oDoc.SendCommand("_.script" + (char)13 + @Properties.Settings.Default.ScriptFile + (char)13);
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Sent the script\n\t\t" + Properties.Settings.Default.ScriptFile);
                }
<<<<<<< HEAD
=======
                try
                {
                    if (objectGroups != null)
                    {

                        string blockPath = @"\\lkw-walter.com\Data\CONTAINEX\Autocad\PARTLIBRARY\groupidentificator.dwg";
                        if (Properties.Settings.Default.ObjectIdentificatorAsBlock)
                        {
                            foreach (var item in objectGroups)
                            {
                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Searching Handle {item.AcadHandle} in Blockspace");
                                try
                                {
                                    var foundByHandle = oDoc.Blocks.Database.HandleToObject(item.AcadHandle) as AcadBlockReference;
                                    if (foundByHandle != null)
                                    {
                                        if (foundByHandle != null)
                                        {
                                            // Überprüfen, ob der Block bereits existiert
                                            AcadBlock blockDef;
                                            blockDef = oDoc.Blocks.Item(foundByHandle.EffectiveName);
                                            double[] insertionPoint = (double[])foundByHandle.InsertionPoint;
                                            double angle = foundByHandle.Rotation;
                                            double cosAngle = Math.Cos(angle);
                                            double sinAngle = Math.Sin(angle);

                                            // Berechnung der BoundingBox der Blockdefinition
                                            double[] blockMinCoords = new double[] { double.MaxValue, double.MaxValue, double.MaxValue };
                                            double[] blockMaxCoords = new double[] { double.MinValue, double.MinValue, double.MinValue };

                                            foreach (AcadEntity entity in blockDef)
                                            {
                                                Debug.WriteLine($"{entity.ObjectName} - {entity.Layer}");
                                                // Nur Blockreferenzen, Linien, Kreise und Attribute berücksichtigen
                                                if (entity is AcadBlockReference || entity is AcadLine || entity is AcadCircle || entity is AcadPolyline || entity is AcadLWPolyline)
                                                {
                                                    // Überprüfen, ob das Objekt auf dem Layer "Elektroverr" liegt
                                                    if (entity.Layer == "ROHBAU")
                                                    {
                                                        Debug.WriteLine("\tTRUE");
                                                        object entityMinPoint = new object[3];
                                                        object entityMaxPoint = new object[3];
                                                        entity.GetBoundingBox(out entityMinPoint, out entityMaxPoint);

                                                        double[] entityMinCoords = (double[])entityMinPoint;
                                                        double[] entityMaxCoords = (double[])entityMaxPoint;

                                                        for (int i = 0; i < 3; i++)
                                                        {
                                                            if (entityMinCoords[i] < blockMinCoords[i])
                                                                blockMinCoords[i] = entityMinCoords[i];
                                                            if (entityMaxCoords[i] > blockMaxCoords[i])
                                                                blockMaxCoords[i] = entityMaxCoords[i];
                                                        }
                                                    }
                                                }
                                            }

                                            double[] blockMidPoint = new double[]
                                            {
                                            (blockMinCoords[0] + blockMaxCoords[0]) / 2,
                                            //(blockMinCoords[1] + blockMaxCoords[1]) / 2,
                                            blockMinCoords[1],
                                            (blockMinCoords[2] + blockMaxCoords[2]) / 2
                                            };
                                            Debug.WriteLine($"Definition Middlep = {blockMidPoint[0].ToString()}, {blockMidPoint[1].ToString()}, {blockMidPoint[2].ToString()}");

                                            double rotatedOffsetX = blockMidPoint[0] * cosAngle - blockMidPoint[1] * sinAngle;
                                            double rotatedOffsetY = blockMidPoint[0] * sinAngle + blockMidPoint[1] * cosAngle;

                                            var finalInsertionPoint = new double[]
                                            {
                                            insertionPoint[0] + rotatedOffsetX,
                                            insertionPoint[1] + rotatedOffsetY,
                                            insertionPoint[2]
                                            };                                            

                                            // Erstelle einen neuen Block mit der gleichen Drehung und einem Offset
                                            var newBlock = oDoc.ModelSpace.InsertBlock(finalInsertionPoint, blockPath, 1, 1, 1, foundByHandle.Rotation) as AcadBlockReference;
                                            newBlock.Layer = "GROUP_IDENTIFICATOR";
                                            // Setze das Attribut in den neuen Block
                                            var atts_ = newBlock.GetAttributes() as object[];
                                            foreach (AcadAttributeReference att in atts_)
                                            {
                                                if (att.TagString == "GROUPID")
                                                {
                                                    att.TextString = item.Group_Identificator;
                                                    break;
                                                }
                                            }


                                        }
                                    }
                                    else
                                    {
                                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Handle {item.AcadHandle} not found in Blockspace");
                                    }
                                }
                                catch (Exception ex)
                                {

                                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error in Searching Handle\n\t\t" + ex);
                                }
                            }
                        } else
                        {
                            foreach (var item in objectGroups)
                            {
                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Searching Handle {item.AcadHandle} in Blockspace");
                                try
                                {
                                    var foundByHandle = oDoc.Blocks.Database.HandleToObject(item.AcadHandle) as AcadBlockReference;
                                    if (foundByHandle != null)
                                    {
                                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found {foundByHandle.EffectiveName}");


                                        var atts_ = foundByHandle.GetAttributes() as object[];

                                        foreach (var attribute in atts_.OfType<AcadAttributeReference>())
                                        {
                                            if (attribute.TagString == "034_PANISO")
                                            {
                                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"\tWriting Group_Identificator {item.Group_Identificator}");
                                                attribute.TextString = item.Group_Identificator;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Handle {item.AcadHandle} not found in Blockspace");
                                    }
                                }
                                catch (Exception ex)
                                {

                                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error in Searching Handle\n\t\t" + ex);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error in Loop for writing the Object Group in Acad Handle\n\t\t" + ex);
                }
>>>>>>> a25158f (Initial commit)

                // Verwenden von LINQ, um nur die relevanten Blöcke zu filtern
                var relevantBlocks = oDoc.ModelSpace.OfType<AcadBlockReference>()
                    .Where(block => blockNames.Any(x => block.EffectiveName.ToUpper().IsMatch(x, '?', '*')));

<<<<<<< HEAD
=======

>>>>>>> a25158f (Initial commit)
                foreach (AcadEntity ent in relevantBlocks)
                {
                    AcadBlockReference block = ent as AcadBlockReference;
                    if (block == null)
                    {
                        continue;
                    }
<<<<<<< HEAD

=======
>>>>>>> a25158f (Initial commit)
                    {
                        //if (!block.Name.Equals(Properties.Settings.Default.SerialBlocks, StringComparison.CurrentCultureIgnoreCase)) continue;
                        if (!blockNames.Any(x => block.EffectiveName.ToUpper().IsMatch(x, '?', '*'))) continue;
                        var atts = block.GetAttributes() as object[];
                        if (atts == null) continue;
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Blockname {block.EffectiveName} matches the pattern and has attributes");
                        foreach (var attribute in atts.OfType<AcadAttributeReference>())
                        {
                            //if (!attribute.TagString.Equals(Properties.Settings.Default.SerialAtts, StringComparison.CurrentCultureIgnoreCase)) continue;
                            if (!serialAtts.Any(x => attribute.TagString.Equals(x))) continue;

<<<<<<< HEAD
                            List<OrderObject> results = Drawing.FindAll(x => x.AcadNumber.Equals(attribute.TextString));
                            string result = null;
                            int resultsCount = results.Count;
                            if (resultsCount == 1)
                            {
                                result = results[0].Seriennummer;
                            }
                            else if (resultsCount > 1)
                            {
                                results.Sort();
                                string firstResult = results[0].Seriennummer;
                                string lastResult = results.Last().Seriennummer;
=======
                            List<ACAD_SerialNumber> results = Drawing.FindAll(x => x.PO_Sequence.Equals(attribute.TextString));
                            string result = null;
                            int resultsCount = results.Count;
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Found {resultsCount} Serial Numbers for this frame with Pseudo Serial {attribute.TextString}");
                            if (resultsCount == 1)
                            {
                                result = results[0].SerialNumber;
                            }
                            else if (resultsCount > 1)
                            {
                                //results.Sort();
                                results.Sort((x, y) => x.SerialNumber.CompareTo(y.SerialNumber));
                                string firstResult = results[0].SerialNumber;
                                string lastResult = results.Last().SerialNumber;
>>>>>>> a25158f (Initial commit)
                                if (int.TryParse(firstResult, out int firstResultInt) && int.TryParse(lastResult, out int lastResultInt))
                                {
                                    int diff = lastResultInt - firstResultInt + 1;
                                    if (resultsCount == diff)
                                        result = firstResult + "-" + lastResult;
                                    else
                                    {
                                        result = "#error#";
                                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error on Snr difference Check. Calculated difference {diff.ToString()}. Counted difference {resultsCount.ToString()} ");

                                    }
                                }
                            }
                            if (result == null)
                                break;
                            string oldatt = attribute.TextString;
                            attribute.TextString = result;
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Wrote the Snr {result} in the block {block.EffectiveName} with the PSnr {oldatt}");
                            break;
                        }
                    }
                }
                if (Properties.Settings.Default.Plot == true)
                {
                    oDoc.SendCommand("'tilemode" + (char)13 + "0" + (char)13 + "_-plot" + (char)13 + "_n" + (char)13 + (char)13 + "PageLayoutCtx2DtoPdf" + (char)13 + "DWG To PDF.pc3" + (char)13 + "PageLayoutCtx2DtoPdf" + (char)13 + pdfName + ".pdf" + (char)13 + "_n" + (char)13 + "_y" + (char)13);
                }
                if (@Properties.Settings.Default.ScriptFile != null)
                {
                    oDoc.SendCommand("filedia" + (char)13 + "0" + (char)13);
                    oDoc.SendCommand("_.script" + (char)13 + @Properties.Settings.Default.ScriptFile2 + (char)13);
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Sent the END script\n\t\t" + Properties.Settings.Default.ScriptFile2);
                }
<<<<<<< HEAD
                oDoc.SaveAs(newFname); 
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Saved as {newFname}.dwg");
                
=======
                oDoc.SaveAs(newFname);
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Saved as {newFname}.dwg");

>>>>>>> a25158f (Initial commit)
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error in Autocad - File : {newFname}.dwg\n\t\t" + ex);
            }
            finally
            {
                oDoc.Close();
            }
<<<<<<< HEAD
            
        }
=======

        }

>>>>>>> a25158f (Initial commit)
        private List<OrderObject> ReadmyCSVData(string csvpath)
        {
            try
            {
                List<OrderObject> csvList = new List<OrderObject>();
                using (StreamReader reader = new StreamReader(File.OpenRead(@csvpath)))
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        i++;
                        var line = reader.ReadLine();
                        if (i == 1 && Properties.Settings.Default.HeaderMode == true)
                        {
                            Console.WriteLine("Header (LineNumber {0}): {1}", i, line);
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("storing Line (Linenumber {0}): {1}", i, line);
                            var values = line.Split(';');
                            csvList.Add(new OrderObject()
                            {
                                OrderId = values[0],
                                Position = values[1],
                                ArtikelCode = values[2],
                                AnlagenReferenz = values[3],
                                AnlagenSequenz = values[4],
                                AcadNumber = values[5],
                                Seriennummer = values[6]
                            });
                        }
                    }
                }
                return csvList;
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Error while reading the csv\n\t\t" + ex);
                return null;
            }
<<<<<<< HEAD
            
=======

>>>>>>> a25158f (Initial commit)
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowForPreferences win2 = new WindowForPreferences();
            //PrefButton.IsEnabled = false;
            //StartButton.IsEnabled = false;
            win2.ShowDialog();
        }
<<<<<<< HEAD
        
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            systemLog.Close();
            base.OnClosing(e);            
        }
        
        private void MoveCSV (string myName)
=======

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            systemLog.Close();
            base.OnClosing(e);
        }

        private void MoveCSV(string myName)
>>>>>>> a25158f (Initial commit)
        {
            try
            {
                string myNameCsv = Path.ChangeExtension(myName, ".csv");
                string newCSVName = @Properties.Settings.Default.CSVOutputPath + Path.GetFileName(myNameCsv);
                string errorCSVName = @Properties.Settings.Default.ErrorPath + Path.GetFileName(myNameCsv);
                try
                {
                    File.Move(myName, newCSVName);
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Moved {myName} to {newCSVName}");
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error while moving the CSV {myName} to {newCSVName}\n\t\t" + ex);
                    if (File.Exists(newCSVName))
                        try
                        {
                            FileInfo fInfo = new FileInfo(newCSVName);
                            fInfo.IsReadOnly = false;
                            File.Delete(newCSVName);
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Deleted {newCSVName}");
                            File.Move(myName, newCSVName);
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Moved {myName} to {newCSVName}");
                        }
                        catch (Exception ex1)
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Error while deleting old CSV\n\t\t" + ex1);
                            try
                            {
                                File.Move(myName, errorCSVName);
                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Moved {myName} to {errorCSVName}");

                            }
                            catch (Exception ex2)
                            {
                                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Error while moving the CSV to the ErrorPath\n\t\t" + ex2);
                                return;
                            }

                        }
                    else
                        try
                        {
                            File.Move(myName, errorCSVName);
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Moved {myName} to {errorCSVName}");

                        }
                        catch (Exception ex2)
                        {
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Error while moving the CSV to the ErrorPath" + ex2);
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                myLog.LogMsg("Failure on MoveCSV\n\t\t" + ex);
            }

        }

<<<<<<< HEAD
=======
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.SettingsKey = SettingsKeyTextBox.Text;
            Properties.Settings.Default.Reload();
        }

>>>>>>> a25158f (Initial commit)
        private void ReloadConfigButton_Click(object sender, RoutedEventArgs e)
        {
            var appConfigPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            string restoreConfigPath = Properties.Settings.Default.ConfigPath;
            try
            {
                File.Delete(appConfigPath);
                File.Copy(restoreConfigPath, appConfigPath);
                Properties.Settings.Default.Reload();
            }
            catch (Exception)
            {
                MessageBox.Show("Config Datei konnte nicht überschrieben werden\n" + appConfigPath + "\n" + restoreConfigPath);
            }
        }
<<<<<<< HEAD

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.SettingsKey = SettingsKeyTextBox.Text;
            Properties.Settings.Default.Reload();
        }
=======
>>>>>>> a25158f (Initial commit)
    }
}

