using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Example.Models
{
    /// <summary>
    /// Gets all main attributes of file
    /// </summary>
    class DuplicateFile
    {
        /// <summary>
        /// Name of file
        /// </summary>
        public string fileName { get; set; }

        /// <summary>
        /// File's path
        /// </summary>
        public string filePath { get; set; }

        /// <summary>
        /// File's size
        /// </summary>
        public double fileLength { get; set; }
    }
}
