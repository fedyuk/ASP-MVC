using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Example.Models;

namespace MVVM_Example.Services
{
    /// <summary>
    /// Scans the current folder to find the duplicate files 
    /// </summary>
    class FileScannerService
    {
        /// <summary>
        /// Current folder's path to scan files
        /// </summary>
        protected String directoryPath;

        /// <summary>
        /// Collection of all nested files
        /// </summary>
        protected List<DuplicateFile> nestedFiles;

        /// <summary>
        /// Collection of collections files that are grouped by file size
        /// </summary>
        protected List<List<DuplicateFile>> duplicateFilesByLength;

        /// <summary>
        /// Collection of all files with the same data content
        /// </summary>
        protected HashSet<DuplicateFile> duplicateFilesByContent;

        /// <summary>
        /// Count of files with the same size
        /// </summary>
        protected long duplicateFileCount;

        /// <summary>
        /// total count of iterations or count of files with the same size
        /// </summary>
        protected long totalIterations;

        /// <summary>
        /// Current iteration of scanning files
        /// </summary>
        protected long currentIteration;

        /// <summary>
        /// Count of all nested files in folder
        /// </summary>
        protected long nestedFileCount;

        /// <summary>
        /// Defaul constructor to initialize filescanner object
        /// </summary>
        public FileScannerService()
        {
            this.nestedFiles = new List<DuplicateFile>();
            this.duplicateFilesByLength = new List<List<DuplicateFile>>();
            this.duplicateFileCount = 0;
            this.totalIterations = 0;
            this.currentIteration = 0;
            this.nestedFileCount = 0;
            this.duplicateFilesByContent = new HashSet<DuplicateFile>();
        }

        /// <summary>
        /// Method returns all nested files in current directory
        /// </summary>
        public List<DuplicateFile> GetNestedFiles(DirectoryInfo dinfo, BackgroundWorker backgroundWorker, System.ComponentModel.DoWorkEventArgs e)
        {
            List<DuplicateFile> files = new List<DuplicateFile>();
            List<DuplicateFile> nestedFiles = new List<DuplicateFile>();
            try
            {
                nestedFiles.AddRange(dinfo.GetFiles().Select(s => this.ConvertFileInfo(s)));
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (Exception)
            {
            }
            foreach (var d in dinfo.GetDirectories())
            {
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return nestedFiles;
                }
                try
                {
                    nestedFiles.AddRange(GetNestedFiles(d, backgroundWorker, e));
                }
                catch (UnauthorizedAccessException)
                {
                }
                catch (Exception)
                {
                }
            }

            return nestedFiles;
        }

        /// <summary>
        /// Method returns all files that have a same size
        /// </summary>
        public List<DuplicateFile> GetDuplicateFilesByLength(BackgroundWorker backgroundWorker, System.ComponentModel.DoWorkEventArgs e)
        {
            var directoryInfo = new DirectoryInfo(this.directoryPath);
            var files = this.GetNestedFiles(directoryInfo, backgroundWorker, e)
                            .GroupBy(i => i.fileLength)
                            .Where(g => g.Count() > 1)
                            .SelectMany(list => list)
                            .ToList<DuplicateFile>();
            this.duplicateFileCount = files.Count;
            if (backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return files.ToList<DuplicateFile>();
            }
            return files.ToList<DuplicateFile>();
        }

        /// <summary>
        /// Method converts a type FileInfo to a type DuplicateFile
        /// </summary>
        private DuplicateFile ConvertFileInfo(FileInfo finfo)
        {
            return new DuplicateFile
            {
                fileName = finfo.Name,
                filePath = finfo.FullName,
                fileLength = finfo.Length
            };
        }


        /// <summary>
        /// Method returns collection of collections of files that have a same size
        /// </summary>
        public List<List<DuplicateFile>> TakeDuplicateFilesByLength(BackgroundWorker backgroundWorker, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                List<DuplicateFile> files = new List<DuplicateFile>();
                DuplicateFile previousDuplicateFile = new DuplicateFile();
                bool isFirstIteration = true;
                foreach (DuplicateFile file in GetNestedFiles())
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return duplicateFilesByLength;
                    }
                    if (previousDuplicateFile.fileLength != file.fileLength && !(isFirstIteration))
                    {
                        List<DuplicateFile> insertedFiles = new List<DuplicateFile>(files);
                        this.duplicateFilesByLength.Add(insertedFiles);
                        files.Clear();
                        files.Add(file);
                    }
                    else
                    {
                        files.Add(file);
                    }
                    previousDuplicateFile = file;
                    isFirstIteration = false;
                }
                duplicateFilesByLength.Add(files);
                return duplicateFilesByLength;
            }
            catch (Exception)
            {
                return duplicateFilesByLength;
            }
        }

        /// <summary>
        /// Method returns true if current files are equal
        /// </summary>
        private bool CompareFiles(string firstFile, string secondFile, BackgroundWorker backgroundWorker, System.ComponentModel.DoWorkEventArgs e)
        {
            int firstFileByte;
            int secondFilebyte;
            FileStream firstFileStream = null;
            FileStream secondFileStream = null;
            try
            {
                firstFileStream = new FileStream(firstFile, FileMode.Open);
                secondFileStream = new FileStream(secondFile, FileMode.Open);
                do
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return false;
                    }
                    firstFileByte = firstFileStream.ReadByte();
                    secondFilebyte = secondFileStream.ReadByte();
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return true;
                    }
                }
                while ((firstFileByte == secondFilebyte) && (firstFileByte != -1));
                firstFileStream.Close();
                secondFileStream.Close();
                return ((firstFileByte - secondFilebyte) == 0);
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (firstFileStream != null)
                {
                    firstFileStream.Close();
                }
                if (secondFileStream != null)
                {
                    secondFileStream.Close();
                }
            }
        }

        /// <summary>
        /// Method returns files that have a same content
        /// </summary>
        public HashSet<DuplicateFile> GetDuplicateFilesByContent(BackgroundWorker backgroundWorker, System.ComponentModel.DoWorkEventArgs e)
        {
            currentIteration++;
            double percentage = 0;
            bool isComparedFilesByLength = false;
            HashSet<DuplicateFile> duplicateFilesByContent = new HashSet<DuplicateFile>();
            foreach (List<DuplicateFile> file in GetDuplicateFilesByLength())
            {
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return duplicateFilesByContent;
                }
                for (int i = 0; i < file.Count; i++)
                {
                    percentage = (double)currentIteration / GetDuplicateFileCount() * 100;
                    backgroundWorker.ReportProgress(Convert.ToInt32(percentage), file[i].filePath + "(" + Convert.ToInt32(file[i].fileLength / 1024) + "Kb)");
                    currentIteration++;
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return duplicateFilesByContent;
                    }
                    if ((i + 2 == file.Count) && (!isComparedFilesByLength))
                    {
                        if (CompareFiles(file[i].filePath, file[i + 1].filePath, backgroundWorker, e))
                        {
                            duplicateFilesByContent.Add(file[i]);
                            duplicateFilesByContent.Add(file[i + 1]);
                            if (backgroundWorker.CancellationPending)
                            {
                                e.Cancel = true;
                                return duplicateFilesByContent;
                            }
                        }
                        isComparedFilesByLength = true;
                    }
                    else if ((!isComparedFilesByLength) && ((i + 1) < file.Count))
                    {
                        if (CompareFiles(file[i].filePath, file[i + 1].filePath, backgroundWorker, e))
                        {
                            duplicateFilesByContent.Add(file[i]);
                            duplicateFilesByContent.Add(file[i + 1]);
                            if (backgroundWorker.CancellationPending)
                            {
                                e.Cancel = true;
                                return duplicateFilesByContent;
                            }
                        }
                    }

                }
                isComparedFilesByLength = false;
            }
            return duplicateFilesByContent;
        }

        /// <summary>
        /// Returns folder's path to scan duplicate files
        /// </summary>
        public String GetDirectoryPath()
        {
            return directoryPath;
        }

        /// <summary>
        /// Sets folder's path
        /// </summary>
        public void SetDirectoryPath(String path)
        {
            this.directoryPath = path;
        }

        /// <summary>
        /// Returns all nested files in folder
        /// </summary>
        public List<DuplicateFile> GetNestedFiles()
        {
            return this.nestedFiles;
        }

        /// <summary>
        /// Sets all nested files in folder
        /// </summary>
        public void SetNestedFiles(List<DuplicateFile> files)
        {
            this.nestedFiles = files;
        }

        /// <summary>
        /// Returns files with the same size
        /// </summary>
        public List<List<DuplicateFile>> GetDuplicateFilesByLength()
        {
            return this.duplicateFilesByLength;
        }

        /// <summary>
        /// Sets files with the same data
        /// </summary>
        public void SetDuplicateFilesByLength(List<List<DuplicateFile>> duplicateFiles)
        {
            this.duplicateFilesByLength = duplicateFiles;
        }

        /// <summary>
        /// Returns count of duplicate files with the same size
        /// </summary>
        public long GetDuplicateFileCount()
        {
            return duplicateFileCount;
        }

        /// <summary>
        /// Sets the count of files with the same size
        /// </summary>
        public void SetDuplicateFileCount(long fileCount)
        {
            this.duplicateFileCount = fileCount;
        }

        /// <summary>
        /// Returns the total count of iterations to show progress result in percentages
        /// </summary>
        public long GetTotalIterations()
        {
            return this.totalIterations;
        }

        /// <summary>
        /// Sets the total count of iterations to show progress result in percentages
        /// </summary>
        public void SetTotalIterations(long totalIterations)
        {
            this.totalIterations = totalIterations;
        }

        /// <summary>
        /// Returns the current iteration of file scanning
        /// </summary>
        public long GetCurrentIteration()
        {
            return this.currentIteration;
        }

        /// <summary>
        /// Sets the current iteration of file scanning
        /// </summary>
        public void SetCurrentIteration(long currentIteration)
        {
            this.currentIteration = currentIteration;
        }

        /// <summary>
        /// Returns the count of all nested files in folder
        /// </summary>
        public long GetNestedFileCount()
        {
            return nestedFileCount;
        }

        /// <summary>
        /// Sets the count of all nested files in folder
        /// </summary>
        public void SetNestedFileCount(long nestedFileCount)
        {
            this.nestedFileCount = nestedFileCount;
        }

        /// <summary>
        /// Returns all files with the same data content
        /// </summary>
        public HashSet<DuplicateFile> GetDuplicateFilesByContent()
        {
            return this.duplicateFilesByContent;
        }

        /// <summary>
        /// Sets all files with the same data content
        /// </summary>
        public void SetDuplicateFilesByContent(HashSet<DuplicateFile> duplicateFiles)
        {
            this.duplicateFilesByContent = duplicateFiles;
        }

        /// <summary>
        /// Clears collection of files with the same data content
        /// </summary>
        public void ClearDuplicateFilesByContent()
        {
            this.duplicateFilesByContent.Clear();
        }

        /// <summary>
        /// Clears the collection of files with the same size
        /// </summary>
        public void ClearDuplicateFilesByLength()
        {
            this.duplicateFilesByLength.Clear();
        }
    }
}
