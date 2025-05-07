using System;
using System.Diagnostics;
using System.IO;

namespace OrderToDrawing
{
    class Log
    {
        public string SavingPath { get; set; }

        StreamWriter myWriter;

        public Log()
        {

        }
        public void Delete(string path, string forFile)
        {
            try
            {
                SavingPath = path + forFile + ".log";
                if (File.Exists(@SavingPath))
                {
                    File.Delete(@SavingPath);
                }
            }
            catch (Exception)
            {
            }

        }
        public void Open(string path, string forFile)
        {
            try
            {
                SavingPath = path + forFile + ".log";
                /*
                if (File.Exists(SavingPath))
                    try
                    {
                        FileInfo fInfo = new FileInfo(SavingPath);
                        fInfo.IsReadOnly = false;
                        File.Delete(SavingPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while deleting the old log" + ex);
                        SavingPath = null;
                        return;
                    }
                */
                myWriter = File.AppendText(SavingPath);
                myWriter.Write("\r\nLog Entry : ");
                myWriter.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                myWriter.WriteLine("  :");
                myWriter.WriteLine($"  :{forFile}");
                myWriter.WriteLine("-------------------------------");
                myWriter.Flush();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

        }
        public void LogMsg(string Msg)
        {
            Debug.WriteLine(Msg);
            try
            {
                if (myWriter != null)
                {
                    myWriter.WriteLine($"{DateTime.Now.ToLongTimeString()}" + "---" + Msg);
                    myWriter.Flush();
                }
                //else
                //MessageBox.Show("There is no LogFile Streamwriter!");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

        }
        public void Close()
        {
            try
            {
                if (myWriter != null)
                {
                    myWriter.WriteLine("-------------------------------");
                    myWriter.Close();
                    myWriter.Dispose();
                    myWriter = null;
                }
                //else
                //MessageBox.Show("There is no LogFile Streamwriter!");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }


        }

    }
}
