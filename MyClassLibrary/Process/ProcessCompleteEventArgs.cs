using System;


namespace MyClassLibrary.Process
{
    public class ProcessCompleteEventArgs 
        : System.EventArgs
    {
        //FIELDS
        protected Boolean error;

        protected Exception exception;

        protected String message;


        //PROPERTIES
        public Boolean Error
        {
            get { return error; }

            set { error = value; }
        }

        public Exception Exception
        {
            get { return exception; }

            set
            {
                if (value == null)
                {
                    throw new PropertySetToDefaultException("Exception");
                }

                exception = value;
            }
        }

        public String Message
        {
            get { return message; }

            set
            {
                if (value == null)
                {
                    throw new PropertySetToDefaultException("Message");
                }

                message = value;
            }
        }


        //INITIALIZE
        public ProcessCompleteEventArgs()
        {
            error = false;

            exception = null;

            message = null;
        }

        public ProcessCompleteEventArgs(Boolean Error)
        {
            this.Error = Error;

            exception = null;

            message = null;
        }

        public ProcessCompleteEventArgs(Boolean Error, String Message)
        {
            this.Error = Error;

            exception = null;

            this.Message = Message;
        }

        public ProcessCompleteEventArgs(Boolean Error, Exception Exception)
        {
            this.Error = Error;

            this.Exception = Exception;

            message = null;
        }

        public ProcessCompleteEventArgs(Boolean Error, String Message, Exception Exception)
        {
            this.Error = Error;

            this.Exception = Exception;

            this.Message = Message;
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.
