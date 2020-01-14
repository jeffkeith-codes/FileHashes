using System;
using System.IO; 
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.ComponentModel;
using System.Security.Cryptography;

namespace sha256sum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string fileName; 
        public string FileName 
        { 
            get { return fileName; }
            set
            {
                fileName = value;
                if (FileName != null)
                {
                    OnPropertyChanged(nameof(FileName));
                    OnPropertyChanged(nameof(SHA512Hash));
                    OnPropertyChanged(nameof(SHA384Hash));
                    OnPropertyChanged(nameof(SHA256Hash));
                }
            }
        }
        public string SHA512Hash { get => Hasher("SHA512"); }
        public string SHA384Hash { get => Hasher("SHA384"); }
        public string SHA256Hash { get => Hasher("SHA256"); }

        private string Hasher(string whichHash)
        {
            if (fileName == null)
                return "Choose a file";

            using ( FileStream fileToHash = new FileStream(FileName, FileMode.Open))
            {
                string outputHash = ""; 
                //try
                //{
                    SHA512Managed sha512 = new SHA512Managed();
                    SHA384Managed sha384 = new SHA384Managed();
                    SHA256Managed sha256 = new SHA256Managed();
                    switch (whichHash)
                    {
                        case "SHA512":
                            outputHash = HexStringOfByteArray(sha512.ComputeHash(fileToHash));
                            break; 
                        case "SHA384":
                            outputHash = HexStringOfByteArray(sha384.ComputeHash(fileToHash));
                            break; 
                        case "SHA256":
                            outputHash = HexStringOfByteArray(sha256.ComputeHash(fileToHash));
                            break; 
                    }
                //}
                //finally
                //{
                //    GC.Collect();
                //    GC.WaitForPendingFinalizers();
                //}
                return outputHash;
            }
        }

        private static string HexStringOfByteArray(byte[] byteArray)
        {
            StringBuilder stringBuilder = new StringBuilder(); 
            for (int i = 0; i< byteArray.Length; i++)
                stringBuilder.Append(byteArray[i].ToString("X2"));
            return stringBuilder.ToString(); 
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseFileClick(object sender, RoutedEventArgs e)
        {
            var openfileDialog = new OpenFileDialog
            {
                Filter = "Any File Type|*.*"
            };

            var dialogResult = openfileDialog.ShowDialog();
            if (dialogResult == true)
            {
                FileName = openfileDialog.FileName;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
