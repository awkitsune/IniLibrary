using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IniLibrary
{
    public class IniFile
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(
            string Section,
            string Key,
            string Value,
            string FilePath
        );

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(
            string Section,
            string Key,
            string Default,
            StringBuilder RetVal,
            int Size,
            string FilePath
        );
        /// <summary>
        /// Constructor for IniFile class
        /// </summary>
        /// <param name="IniPath">Path to INI file</param>
        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }
        /// <summary>
        /// Returns value from ini file by key and section
        /// </summary>
        /// <param name="Key">Ini file key</param>
        /// <param name="Section">Ini file section</param>
        /// <returns></returns>
        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }
        /// <summary>
        /// Writes value to ini file by key and section
        /// </summary>
        /// <param name="Key">Ini file key</param>
        /// <param name="Value">Writing value</param>
        /// <param name="Section">Ini file section</param>
        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }
        /// <summary>
        /// Removes value from ini file by key and section
        /// </summary>
        /// <param name="Key">Ini file key</param>
        /// <param name="Section">Ini file section</param>
        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }
        /// <summary>
        /// Checks if key from ini file exists by key and section
        /// </summary>
        /// <param name="Key">Ini file key</param>
        /// <param name="Section">Ini file section</param>
        /// <returns></returns>
        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}
