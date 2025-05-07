using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
=======
using System.Diagnostics;
using System.IO;
>>>>>>> a25158f (Initial commit)

namespace OrderToDrawing
{
    class Log
    {
        public string SavingPath { get; set; }

        StreamWriter myWriter;

        public Log()
        {

        }
<<<<<<< HEAD
=======
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
>>>>>>> a25158f (Initial commit)
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
<<<<<<< HEAD
            
        }
        public void LogMsg(string Msg)
        {
=======

        }
        public void LogMsg(string Msg)
        {
            Debug.WriteLine(Msg);
>>>>>>> a25158f (Initial commit)
            try
            {
                if (myWriter != null)
                {
                    myWriter.WriteLine($"{DateTime.Now.ToLongTimeString()}" + "---" + Msg);
                    myWriter.Flush();
                }
                //else
<<<<<<< HEAD
                    //MessageBox.Show("There is no LogFile Streamwriter!");
=======
                //MessageBox.Show("There is no LogFile Streamwriter!");
>>>>>>> a25158f (Initial commit)
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
<<<<<<< HEAD
            
=======

>>>>>>> a25158f (Initial commit)
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
<<<<<<< HEAD
                    //MessageBox.Show("There is no LogFile Streamwriter!");
            }
            catch (Exception  ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            
            
=======
                //MessageBox.Show("There is no LogFile Streamwriter!");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }


>>>>>>> a25158f (Initial commit)
        }

    }
}
