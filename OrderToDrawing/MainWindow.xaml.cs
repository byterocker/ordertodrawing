using System;
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
        bool isThreadRunning=false;
        bool isTimeToStop=false;
        Log myLog;
        Log systemLog;
        



        public MainWindow()
        {
            try
            {
                
                InitializeComponent();
                if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() == 2)
                {
                    Properties.Settings.Default.SettingsKey = "PROD100";
                    SettingsKeyTextBox.Text= "PROD100";
                    Properties.Settings.Default.Reload();
                } 
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
                eventcounter = Properties.Settings.Default.EventCounter;
                TotalCSV.Text = eventcounter.ToString();
                TotalCSV.Refresh();
                myTimer.Interval = TimeSpan.FromSeconds(10);
                myTimer.Tick += MainProcess;
                CancelButton.IsEnabled = false;
                systemLog = new Log();
                systemLog.Open(@Properties.Settings.Default.ErrorPath, "System_Log");
                systemLog.LogMsg("System initialized");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

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
                StartButton.IsEnabled = false;
                CancelButton.IsEnabled = true;
                isTimeToStop = false;
                systemLog.LogMsg("starting Dispatcher-Timer...");
                myTimer.Start();
            }
            catch (Exception ex)
            {
                systemLog.LogMsg("Failure on Button_Start\n\t\t" + ex);
            }
            
            
            
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
            {
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
            
            
        }

        private void SearchforCSV()
        {
            try
            {
                isThreadRunning = true;
                systemLog.LogMsg("Searching for CSV Files");
                // This path is a directory
                /*
                string[] fileEntries = Directory.GetFiles(@Properties.Settings.Default.CSVSearchingPath, Properties.Settings.Default.NamingRule + "*.csv");
                var files = Directory.GetFiles(@Properties.Settings.Default.CSVSearchingPath)
                            .Where(fn => !Path.GetFileName(fn).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                            .ToArray();
                */
                string[] fileEntries = (from p in Directory.GetFiles(@Properties.Settings.Default.CSVSearchingPath)
                                        where !Path.GetFileName(p).IsMatch(Properties.Settings.Default.NamingRuleIgnore, '?', '*') && Path.GetFileName(p).IsMatch(Properties.Settings.Default.NamingRule, '?', '*')
                                        select p).ToArray();
                int filesInFolder = fileEntries.Length;
                systemLog.LogMsg($"{fileEntries.Length} CSV's to process...");
                foreach (string fileName in fileEntries)
                {
                    string workingName = Path.ChangeExtension(fileName, ".lockedcsv");
                    if (isTimeToStop == true) break;
                    if (Properties.Settings.Default.LogFileMode == true)
                    {
                        myLog = new Log();
                        myLog.Open(@Properties.Settings.Default.ErrorPath, Path.GetFileNameWithoutExtension(fileName));
                    }
                    CSVNumber.Refresh_txtbox(filesInFolder.ToString());
                    try
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"CSV wird von {fileName} auf {workingName} umbenannt.");
                        File.Move(fileName, workingName);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    ProcessFile(workingName, fileEntries.Length);
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Process File Function completed, closing log");
                    if (Properties.Settings.Default.LogFileMode == true)
                        myLog.Close();
                    filesInFolder--;
                }
                
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

        private void ProcessFile(string name, int generalDrawings)
        {
            try
            {
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Processing {name}");
                List<OrderObject> myCSVData = ReadmyCSVData(name);
                if (myCSVData == null) return;
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Successfully read the CSV");
                List<List<OrderObject>> ToProcess = myCSVData.Seperate<List<OrderObject>>();
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
                foreach (List<OrderObject> Drawing in ToProcess)
                {
                    // Anzeige:
                    ProgressBar.Refresh_progressbar(++Progress);
                    CurrentDWG.Refresh_txtbox(Drawing[0].AnlagenReferenz);
                    lbActualDrawing.Refresh_listbox(Drawing);
                    //Anzeige Ende
                    string newDWGname = (@Properties.Settings.Default.DWGOutputPath + Drawing[0].OrderId + "~" + Drawing[0].AnlagenReferenz + "~" + Drawing[0].AnlagenSequenz + "~Bestellskizze");
                    string newDWGname2 = (@Properties.Settings.Default.DWGOrderCopyPath + Drawing[0].OrderId + "~" + Drawing[0].AnlagenReferenz + "~" + Drawing[0].AnlagenSequenz + "~Bestellskizze");

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
                    if (ignoreArtikelNames != null && ignoreArtikelNames.Any(x => Drawing[0].ArtikelCode.ToUpper().IsMatch(x, '?', '*')))
                    {
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Ignoring {Drawing[0].ArtikelCode}, because it matches to the IgnoreArtikel");
                        continue;
                    }


                    if (noPDSNames != null && noPDSNames.Any(x => Drawing[0].ArtikelCode.ToUpper().IsMatch(x, '?', '*')))
                    {
                        // Fall 2: DWG wird direkt übergeben
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"The drawing {Drawing[0].AnlagenReferenz} has no attributes to change");
                        string dwgpath = FindDWGinDir(Drawing[0].AnlagenReferenz);
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
                        string dwgpath = FindDWGPath(Drawing[0].AnlagenReferenz);
                        if (dwgpath == null) continue;
                        AcadApplication AcadApp = acadConnection.AcadApp;
                        if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Successfully started a AutoCad Session");
                        if (AcadApp != null)
                        {
                            //Autocad wurde gestartet und es geht los
                            AcadDocument oDoc = AcadApp.Documents.Open(dwgpath, true);
                            if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Opened the drawing in AutoCad");
                            oDoc.Activate();
                            ProcessInAutoCad(oDoc, Drawing, newDWGname2, newDWGname);
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
                }
                MoveCSV(name);
                eventcounter++;
                TotalCSV.Refresh_txtbox(eventcounter.ToString());
                Properties.Settings.Default.EventCounter = eventcounter;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                myLog.LogMsg("Failure on ProcessFile\n\t\t" + ex);
            }
            
            
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
                    } 
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
        private string FindDWGinDir (string dwgname)
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
            
            
        }

        private void ProcessInAutoCad(AcadDocument oDoc, List<OrderObject> Drawing, string newFname, string pdfName)
        {
            try
            {
                if (@Properties.Settings.Default.ScriptFile != null)
                {
                    oDoc.SendCommand("filedia" + (char)13 + "0" + (char)13);
                    oDoc.SendCommand("_.script" + (char)13 + @Properties.Settings.Default.ScriptFile + (char)13);
                    if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg("Sent the script\n\t\t" + Properties.Settings.Default.ScriptFile);
                }

                // Verwenden von LINQ, um nur die relevanten Blöcke zu filtern
                var relevantBlocks = oDoc.ModelSpace.OfType<AcadBlockReference>()
                    .Where(block => blockNames.Any(x => block.EffectiveName.ToUpper().IsMatch(x, '?', '*')));

                foreach (AcadEntity ent in relevantBlocks)
                {
                    AcadBlockReference block = ent as AcadBlockReference;
                    if (block == null)
                    {
                        continue;
                    }

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
                oDoc.SaveAs(newFname); 
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Saved as {newFname}.dwg");
                
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.LogFileMode == true) myLog.LogMsg($"Error in Autocad - File : {newFname}.dwg\n\t\t" + ex);
            }
            finally
            {
                oDoc.Close();
            }
            
        }
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
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowForPreferences win2 = new WindowForPreferences();
            //PrefButton.IsEnabled = false;
            //StartButton.IsEnabled = false;
            win2.ShowDialog();
        }
        
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            systemLog.Close();
            base.OnClosing(e);            
        }
        
        private void MoveCSV (string myName)
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.SettingsKey = SettingsKeyTextBox.Text;
            Properties.Settings.Default.Reload();
        }
    }
}

