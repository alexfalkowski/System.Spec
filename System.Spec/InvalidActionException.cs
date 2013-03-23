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

namespace System.Spec
{
    using Collections;
    using Runtime.Serialization;

    [Serializable]
    public sealed class InvalidActionException : Exception
    {
        private readonly IDictionary data;
        private readonly string message;
        private readonly string stackTrace;

        public InvalidActionException(Exception exception)
        {
            data = exception.Data;
            HelpLink = exception.HelpLink;
            message = exception.Message;
            Source = exception.Source;
            stackTrace = exception.StackTrace;
        }

        private InvalidActionException(SerializationInfo info, StreamingContext context)
        {
            data = (IDictionary)info.GetValue("data", typeof(IDictionary));
            HelpLink = info.GetString("helpLink");
            message = info.GetString("message");
            Source = info.GetString("source");
            stackTrace = info.GetString("stackTrace");
        }

        public override IDictionary Data {
            get {
                return data;
            }
        }

        public override string Message {
            get {
                return message;
            }
        }

        public override string StackTrace {
            get {
                return stackTrace;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context) 
        {
            info.AddValue("data", data, typeof(IDictionary));
            info.AddValue("helpLink", HelpLink, typeof(string));
            info.AddValue("message", message, typeof(string));
            info.AddValue("source", Source, typeof(string));
            info.AddValue("stackTrace", stackTrace, typeof(string));
        }
    }
}