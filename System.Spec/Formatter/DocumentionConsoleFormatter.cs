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
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using System.Spec.Properties;

    [Serializable]
    public class DocumentionConsoleFormatter : ConsoleFormatterBase
    {
        private IConsoleWritter writter;
        
        public DocumentionConsoleFormatter(IConsoleWritter writter) : base(writter)
        {
            this.writter = writter;
        }

        public override void WriteInformation(string message)
        {
            this.writter.WriteLine();
            this.writter.WriteInformationLine(message);
        }

        public override void WriteSuccess(ExampleResult example)
        {
            this.writter.WriteSuccessLine(string.Concat(StringHelper.DoubleSpace, example.Reason));
        }

        public override void WriteError(ExampleResult example)
        {
            var message = string.Format(CultureInfo.CurrentCulture,
                                        Resources.DocumentationConsoleFormatterErrorMessage,
                                        example.Reason);
            this.writter.WriteErrorLine(string.Concat(StringHelper.DoubleSpace, message));
        }
    }
}