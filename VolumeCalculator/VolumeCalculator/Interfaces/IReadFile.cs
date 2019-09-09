using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeCalculator.Interfaces
{
    /// <summary>
    /// Interface for reading file.
    /// </summary>
    public interface IReadFile
    {
        List<int> Read(string path);
        bool PathIsValid(string path);
    }
}
