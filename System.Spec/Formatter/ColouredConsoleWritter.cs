// Author:
//       alex.falkowski <alexrfalkowski@gmail.com>
//
//  Copyright (c) 2013 alex.falkowski
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace System.Spec.Formatter
{
    [Serializable]
    public class ColouredConsoleWritter : IConsoleWritter
    {
        public void WriteInformationLine(string value)
        {
            Console.WriteLine(value);
        }

        public void WriteErrorLine(string value)
        {
            WriteWithColour(ConsoleColor.Red, () => Console.WriteLine(value));
        }

        public void WriteSuccessLine(string value)
        {
            WriteWithColour(ConsoleColor.Green, () => Console.WriteLine(value));
        }

        public void WriteInformation(string value)
        {
            Console.Write(value);
        }
        
        public void WriteError(string value)
        {
            WriteWithColour(ConsoleColor.Red, () => Console.Write(value));
        }
        
        public void WriteSuccess(string value)
        {
            WriteWithColour(ConsoleColor.Green, () => Console.Write(value));
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        private static void WriteWithColour(ConsoleColor colour, Action action)
        {
            var originalColour = Console.ForegroundColor;
            
            try {
                Console.ForegroundColor = colour;
                action();
            } finally {
                Console.ForegroundColor = originalColour;
            }
        }
    }
}