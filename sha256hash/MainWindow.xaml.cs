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
        private string fileName = ""; 
        public string FileName 
        { 
            get { return fileName; }
            set
            {
                fileName = value;
                if (FileName != null)
                {
                    OnPropertyChanged("FileName");
                    OnPropertyChanged("SHA512Hash");
                    OnPropertyChanged("SHA384Hash");
                    OnPropertyChanged("SHA256Hash");
                }
            }
        }

        public string SHA512Hash
        {
            get
            {
                using (SHA512Managed sha512 = new SHA512Managed())
                {
                    using (FileStream fileToHash = new FileStream(FileName, FileMode.Open))
                    {
                        byte[] hashValue = sha512.ComputeHash(fileToHash);
                        return HexStringOfByteArray(hashValue);
                    }
                }
            }
        }

        public string SHA384Hash
        {
            get
            {
                using (SHA384Managed sha384 = new SHA384Managed())
                {
                    using (FileStream fileToHash = new FileStream(FileName, FileMode.Open))
                    {
                        byte[] hashValue = sha384.ComputeHash(fileToHash);
                        return HexStringOfByteArray(hashValue);
                    }
                }
            }
        }

        public string SHA256Hash
        {
            get
            {
                using (SHA256Managed sha256 = new SHA256Managed())
                {
                    using (FileStream fileToHash = new FileStream(FileName, FileMode.Open))
                    {
                        byte[] hashValue = sha256.ComputeHash(fileToHash);
                        return HexStringOfByteArray(hashValue);
                    }
                }
            }
        }

        private string HexStringOfByteArray(byte[] byteArray)
        {
            StringBuilder stringBuilder = new StringBuilder(); 
            for (int i = 0; i< byteArray.Length; i++)
            {
                // stringBuilder.Append(String.Format("X2", byteArray[i]));
                stringBuilder.Append(byteArray[i].ToString("X2"));
            }
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
