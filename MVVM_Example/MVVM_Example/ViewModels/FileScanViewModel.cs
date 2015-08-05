using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using MVVM_Example.Commands;
using MVVM_Example.Models;
using MVVM_Example.Services;
using MVVM_Example.Views;

namespace MVVM_Example.ViewModels
{
    /// <summary>
    /// Manages the window to scan files
    /// </summary>
    class FileScanViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The default constructor
        /// </summary>
        public FileScanViewModel(String path)
        {
            this.worker = new BackgroundWorker();
            this.worker.DoWork += this.DoWork;
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.ProgressChanged += this.ProgressChanged;
            this.worker.RunWorkerCompleted += this.BackgroundWorker_RunWorkerCompleted;
            fileScanner = new FileScannerService();
            fileScanner.SetDirectoryPath(path);
            fileScanner.SetCurrentIteration(0);
            fileScanner.SetDuplicateFileCount(0);
            this.duplicateFiles = new List<String>();
            this.canExecuteStart = false;
            this.canExecuteCancel = true;
            this.startScan = new RelayCommand(StartBackgroundWorker, param => this.canExecuteStart);
            this.cancelScan = new RelayCommand(CancelBackgroundWorker, param => this.canExecuteCancel);
            this.worker.RunWorkerAsync();
            this.progressVisibility = Visibility.Visible;
        }

        /// <summary>
        /// Gets the list of duplicate files
        /// </summary>
        List<String> duplicateFiles;

        /// <summary>
        /// To work with background tasks
        /// </summary>
        BackgroundWorker worker;

        /// <summary>
        /// Command to start scanning files
        /// </summary>
        ICommand startScan;

        /// <summary>
        /// Command to cancel scanning files
        /// </summary>
        ICommand cancelScan;

        /// <summary>
        /// Manages the current progress percentage in progress bar
        /// </summary>
        int currentProgress;

        /// <summary>
        /// Shows the current progress percentage in progress bar
        /// </summary>
        String progressText;

        /// <summary>
        /// Folder's path
        /// </summary>
        String directory;

        /// <summary>
        /// Service to scan files 
        /// </summary>
        FileScannerService fileScanner;

        /// <summary>
        /// Allows scanning files
        /// </summary>
        bool canExecuteStart;

        /// <summary>
        /// Allows cancelling to scan the files
        /// </summary>
        bool canExecuteCancel;

        /// <summary>
        /// Visibility for progress bar
        /// </summary>
        Visibility progressVisibility;

        /// <summary>
        /// Getter and setter for progress visibility field
        /// </summary>
        public Visibility ProgressVisibility
        {
            get
            {
                return this.progressVisibility;
            }

            set
            {
                if (this.progressVisibility == value)
                {
                    return;
                }

                this.progressVisibility = value;
                OnPropertyChanged("ProgressVisibility");
            }
        }

        /// <summary>
        /// Getter and setter for can execute start field
        /// </summary>
        public bool CanExecuteStart
        {
            get
            {
                return this.canExecuteStart;
            }

            set
            {
                if (this.canExecuteStart == value)
                {
                    return;
                }

                this.canExecuteStart = value;
                OnPropertyChanged("CanExecuteStart");
            }
        }

        /// <summary>
        /// Getter and setter for can execute cancel field
        /// </summary>
        public bool CanExecuteCancel
        {
            get
            {
                return this.canExecuteCancel;
            }

            set
            {
                if (this.canExecuteCancel == value)
                {
                    return;
                }

                this.canExecuteCancel = value;
                OnPropertyChanged("CanExecuteCancel");
            }
        }

        /// <summary>
        /// Getter and setter for directory field
        /// </summary>
        public String Directory
        {
            get
            {
                return this.directory;
            }
            set
            {
                this.directory = value;
                OnPropertyChanged("Directory");
            }
        }

        /// <summary>
        /// Event for changing the vales in UI window
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

        /// <summary>
        /// Getter and setter for duplicate files field
        /// </summary>
        public List<String> DuplicateFiles
        {
            get
            {
                return this.duplicateFiles;
            }
            set
            {
                if (this.duplicateFiles != value)
                {
                    this.duplicateFiles = value;
                    this.OnPropertyChanged("DuplicateFiles");
                }
            }
        }

        /// <summary>
        /// Getter and setter for start scan command
        /// </summary>
        public ICommand StartScan
        {
            get { return this.startScan; }
        }

        /// <summary>
        /// Getter and setter for cancel scan command
        /// </summary>
        public ICommand CancelScan
        {
            get { return this.cancelScan; }
        }

        /// <summary>
        /// Getter and setter for current progress field
        /// </summary>
        public int CurrentProgress
        {
            get { return this.currentProgress; }
            set
            {
                if (this.currentProgress != value)
                {
                    this.currentProgress = value;
                    this.OnPropertyChanged("CurrentProgress");
                }
            }
        }

        /// <summary>
        /// Getter and setter for progress text field
        /// </summary>
        public String ProgressText
        {
            get { return this.progressText; }
            set
            {
                if (this.progressText != value)
                {
                    this.progressText = value;
                    this.OnPropertyChanged("ProgressText");
                }
            }
        }

        /// <summary>
        /// the worker's main job to find duplicate files
        /// </summary>
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            worker.ReportProgress(0);
            var files = fileScanner.GetDuplicateFilesByLength(worker, e).ToList();
            if (files == null)
            {
                e.Cancel = true;
                return;
            }
            fileScanner.SetNestedFiles(files.ToList());
            fileScanner.SetTotalIterations(Convert.ToInt64(files.Count));
            var duplicateFilesByLength = fileScanner.TakeDuplicateFilesByLength(worker, e);
            HashSet<DuplicateFile> duplicateFilesByContent = fileScanner.GetDuplicateFilesByContent(worker, e);
            fileScanner.SetDuplicateFilesByContent(duplicateFilesByContent);
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// Finishes the worker job
        /// </summary>
        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.duplicateFiles.Clear();
            OnPropertyChanged("DuplicateFiles");
            foreach (DuplicateFile file in fileScanner.GetDuplicateFilesByContent())
            {
                duplicateFiles.Add(file.filePath + "(" + Convert.ToInt32(file.fileLength / 1024) + "Kb)");
            }
            this.canExecuteCancel = false;
            this.canExecuteStart = true;
            this.currentProgress = 0;
            OnPropertyChanged("CurrentProgress");
            this.progressText = "";
            OnPropertyChanged("ProgressText");
            this.progressVisibility = Visibility.Hidden;
            OnPropertyChanged("ProgressVisibility");
            this.fileScanner.SetCurrentIteration(0);
            this.fileScanner.SetDuplicateFileCount(0);
            this.fileScanner.ClearDuplicateFilesByContent();
            this.fileScanner.ClearDuplicateFilesByLength();
            DuplicateFilesView view = new DuplicateFilesView();
            DuplicateFilesViewModel duplicateFilesViewModel = new DuplicateFilesViewModel(duplicateFiles);
            view.DataContext = duplicateFilesViewModel;
            view.Show();
            
        }

        /// <summary>
        /// Changes the progress bar value
        /// </summary>
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.CurrentProgress = e.ProgressPercentage;
            this.ProgressText = (e.ProgressPercentage.ToString() + "%");
            this.duplicateFiles.Add((String)e.UserState);
        }

        /// <summary>
        /// Starts the worker's job
        /// </summary>
        public void StartBackgroundWorker(object obj)
        {
            this.canExecuteCancel = true;
            this.canExecuteStart = false;
            this.progressVisibility = Visibility.Visible;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ProgressVisibility"));
            }
            this.worker.RunWorkerAsync();
        }

        /// <summary>
        /// Cancels the worker's job
        /// </summary>
        public void CancelBackgroundWorker(object obj)
        {
            this.worker.CancelAsync();
        }
    }
}
