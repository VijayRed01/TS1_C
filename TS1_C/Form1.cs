using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
//using Vlc.DotNet.Wpf;
using Vlc.DotNet.Forms;
using System.Threading;
using Vlc.DotNet.Core;
using System.Diagnostics;
using IronOcr;
using Tesseract;
using WMPLib;
using Microsoft.WindowsAPICodePack.Shell;
//using LibVLCSharp;
//using LibVLCSharp.Shared;

namespace TS1_C
{


    public partial class Form1 : Form
    {
       
        bool buttonClicked = false;
        List<string> text_data_list = new List<string>();

        Decimal value;

        string selectedpath;
        string selectedpath2;

        List<String> myObjList4 = new List<String>();
        List<String> myObjList5 = new List<String>();
        private VlcControl control;

        TimeSpan hello = new TimeSpan();

        private bool test_play;

        public Form1()
        {
            InitializeComponent();
        }

 


        private void Form1_Load(object sender, EventArgs e)
        {
            this.trackBar1.ValueChanged += new EventHandler(this.trackBar1_ValueChanged);

            control = new VlcControl();
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            var libDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
            control.BeginInit();
            control.VlcLibDirectory = libDirectory;
            control.Dock = DockStyle.Fill;
            control.EndInit();
            panel1.Controls.Add(control);
            listBox1.MouseDoubleClick += new MouseEventHandler(listBox1_MouseDoubleClick);
            button8.Click += new EventHandler(this.button8_Click);
     
        }
        

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                selectedpath2 = folderDlg.SelectedPath;
                textBox2.Text = folderDlg.SelectedPath;
                // Environment.SpecialFolder root = folderDlg.RootFolder;
            }

            listBox1.Items.Clear();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string[] array1 = Directory.GetFiles(selectedpath2,"*.ASF");

            foreach (string name in array1)
            {
                string[] words = name.Split('\\');
                string lastItem = words[words.Length - 1];
                listBox1.Items.Add(lastItem);
                
               // Console.WriteLine(name);
            }

        }

        void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
          

            
                ////////////////////////////////////////////////////////////load media///////////////////////////////////////////////
                string chosen = listBox1.SelectedItem.ToString();
                string final = selectedpath2 + "\\" + chosen;

                control.SetMedia(new Uri(final).AbsoluteUri);
                control.Audio.Volume = 0;
                control.Update();
                ////////////////////////////////////////////////////////////calculate trackbar length///////////////////////////////////////////////
                var player = new WindowsMediaPlayer();
                var clip = player.newMedia(final);
                string vOut = clip.duration.ToString();

                long howdy = Convert.ToInt64(clip.duration);

                string vOut2 = vOut.Replace(".", "");
                int x = Int32.Parse(vOut2);
                int x_result = x * 10;

               trackBar1.Maximum = x_result;
                trackBar1.Minimum = 0;

                Console.WriteLine("Track Bar Maximum : " + trackBar1.Maximum);
                Console.WriteLine("Track Bar Maximum : " + trackBar1.Minimum);
                Console.WriteLine("Track Tick Frequency : " + trackBar1.TickFrequency);

                button9.PerformClick();
                System.Threading.Thread.Sleep(1000);
                button8.PerformClick();
        

            trackBar1.Value = 0;

            Time_update(0);
        

        }


   

        private void button8_Click(object sender, EventArgs e)
        {
           
            control.Pause();
          
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            control.Play();

     
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
          
                Time_update(trackBar1.Value);
          

        }

        void Time_update(long hold_time2)
        {
            System.Threading.Thread.Sleep(100);
            control.Invoke((MethodInvoker)(() => control.Time = (int)hold_time2));
         
            System.Threading.Thread.Sleep(100);

        }


      
        private void button10_Click(object sender, EventArgs e)
        {
            trackBar1.Value += 40; // fixed value for the sample video
        }

        private void button11_Click(object sender, EventArgs e)
        {
            trackBar1.Value -= 40; // fixed value for the sample video
        }
    }
}
