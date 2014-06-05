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