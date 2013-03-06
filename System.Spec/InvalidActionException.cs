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
    using System.Collections;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidActionException : Exception, ISerializable
    {
        private IDictionary data;
        private string message;
        private string stackTrace;

        public InvalidActionException(Exception exception)
        {
            this.data = exception.Data;
            this.HelpLink = exception.HelpLink;
            this.message = exception.Message;
            this.Source = exception.Source;
            this.stackTrace = exception.StackTrace;
        }

        protected InvalidActionException(SerializationInfo info, StreamingContext context)
        {
            this.data = (IDictionary)info.GetValue("data", typeof(IDictionary));
            this.HelpLink = info.GetString("helpLink");
            this.message = info.GetString("message");
            this.Source = info.GetString("source");
            this.stackTrace = info.GetString("stackTrace");
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