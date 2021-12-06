﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Advent.Utilities
{
    public abstract class FileSelectionParsingConsole<T> : FileSelectionConsole
    {
        private IList<ConsoleOption> options;

        public FileSelectionParsingConsole()
            : base()
        {
        }

        protected sealed override void Execute(string file)
        {
            var data = ParseData(file);
            Timer.Monitor(() =>
            {
                Execute(data);
            });
        }

        protected abstract void Execute(T data);

        protected T ParseData(string fileName)
        {
            try
            {
                var input = System.IO.File.ReadAllLines(fileName);

                return DeserializeData(input);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected abstract T DeserializeData(IList<string> input);
    }
}