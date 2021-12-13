using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;


namespace Task5
{
    class FileWorker
    {
        public static string[] FileReader(string path)
        {
            string[] _textLines;
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("File path is empty");
            }
            if (File.Exists(path))
            {
                _textLines = File.ReadAllLines(path);
            }
            else
            {
                throw new Exception("File not found");
            }
            if (_textLines.Length < 1)
            {
                throw new Exception("File is empty");
            }
            return _textLines;
        }

        public static void FileWriter (string [] input, string path)
        {
            File.WriteAllLines(path + "_outcome", input);
        }
    }
}
