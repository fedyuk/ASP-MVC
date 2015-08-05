using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows.Forms;
using MVVM_Example.Commands;
using MVVM_Example.Views;

namespace MVVM_Example.ViewModels
{
    /// <summary>
    /// Manages the window to select a path
    /// </summary>
    class SelectPathViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor for crrent view model
        /// </summary>
        public SelectPathViewModel()
        {
            path = "Folder's path";
            canExecuteOpenDialog = true;
            canExecuteScan = false;
            openDialog = new RelayCommand(GetPath, param => this.canExecuteOpenDialog);
            scan = new RelayCommand(ScanPath, param => this.canExecuteScan);
        }

        /// <summary>
        /// Folder's path
        /// </summary>
        String path;

        /// <summary>
        /// Command to open dialog for getting the path
        /// </summary>
        ICommand openDialog;

        /// <summary>
        /// Command to show a file scan window for getting the path
        /// </summary>
        ICommand scan;

        /// <summary>
        /// Allows to open a file dialg
        /// </summary>
        bool canExecuteOpenDialog;

        /// <summary>
        /// Allows to show a file scan window
        /// </summary>
        bool canExecuteScan;

        /// <summary>
        /// Getter and setter for  can execute open dialog field
        /// </summary>
        public bool CanExecuteOpenDialog
        {
            get
            {
                return this.canExecuteOpenDialog;
            }

            set
            {
                if (this.canExecuteOpenDialog == value)
                {
                    return;
                }

                this.canExecuteOpenDialog = value;
            }
        }

        /// <summary>
        /// Getter and setter for  can execute scan field
        /// </summary>
        public bool CanExecuteScan
        {
            get
            {
                return this.canExecuteScan;
            }

            set
            {
                if (this.canExecuteScan == value)
                {
                    return;
                }

                this.canExecuteScan = value;
            }
        }

        /// <summary>
        /// Getter and setter for  path field
        /// </summary>
        public String Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
                OnPropertyChanged("Path");
            }
        }

        /// <summary>
        /// Getter and setter for  open dialog command
        /// </summary>
        public ICommand OpenDialog
        {
            get
            {
                return this.openDialog;
            }
            set
            {
                this.openDialog = value;
            }
        }

        /// <summary>
        /// Getter and setter for scan command
        /// </summary>
        public ICommand Scan
        {
            get
            {
                return this.scan;
            }
            set
            {
                this.scan = value;
            }
        }

        /// <summary>
        /// Opens and gets the folder's path
        /// </summary>
        public void GetPath(object obj)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Select folder to find duplicate files.";
            folderDialog.ShowNewFolderButton = false;

            DialogResult result = folderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.path=folderDialog.SelectedPath;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Path"));
                }
                canExecuteScan = true;
 
            }
        }

        /// <summary>
        /// Open the file scan window
        /// </summary>
        public void ScanPath(object obj)
        {
            FileScanView view = new FileScanView();
            FileScanViewModel scanViewModel = new FileScanViewModel(Path);
            view.DataContext = scanViewModel;
            scanViewModel.Directory = Path;
            System.Windows.Application.Current.MainWindow.Close();
            view.ShowDialog();
        }

        /// <summary>
        /// Event to change the values in UI window
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Changes the values in UI window
        /// </summary>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
