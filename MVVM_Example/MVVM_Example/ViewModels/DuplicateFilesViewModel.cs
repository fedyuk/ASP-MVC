using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Example.ViewModels
{
    /// <summary>
    /// Manages the window to show duplicate files list
    /// </summary>
    class DuplicateFilesViewModel
    {
        /// <summary>
        /// Default Constructor 
        /// </summary>
        public DuplicateFilesViewModel(List<String> files)
        {
            this.duplicateFiles = files;
            this.totalCount = "Total duplicate files: " + files.Count();
        }

        /// <summary>
        /// Gets the duplicate files from file scanner service
        /// </summary>
        List<String> duplicateFiles;
        /// <summary>
        /// Gets the total count of duplicate files
        /// </summary>
        String totalCount;

        /// <summary>
        /// Setter and getter for totalCount field
        /// </summary>
        public String TotalCount
        {
            get
            {
                return this.totalCount;
            }
            set
            {
                if (this.totalCount != value)
                {
                    this.totalCount = value;
                    this.OnPropertyChanged("TotalCount");
                }
            }
        }

        /// <summary>
        /// Setter and getter for duplicateFiles field
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
        /// Changes the values in UI window
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
