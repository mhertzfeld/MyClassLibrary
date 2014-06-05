using System;


namespace MyClassLibrary.Process
{
    public abstract class ProcessBase
        : Process.IProcess
    {
        //EVENT HANDLERS
        public event EventHandler<ProcessCompleteEventArgs> InitializationComplete;

        public event EventHandler<ProcessCompleteEventArgs> ProcessComplete;


        //FIELDS
        protected Boolean completed;

        protected Boolean disposed;

        protected Boolean error;

        protected Boolean initialized;

        protected Boolean isBusy;


        //PROPERTIES
        public virtual Boolean Completed
        {
            get { return completed; }

            protected set { completed = value; }
        }

        public virtual Boolean Error
        {
            get { return error; }

            protected set { error = value; }
        }

        public virtual Boolean Initialized
        {
            get { return initialized; }

            protected set { initialized = value; }
        }

        public virtual Boolean IsBusy
        {
            get { return isBusy; }

            protected set { isBusy = value; }
        }

       
        //INITILAIZE
        public ProcessBase()
        {
            completed = false;

            disposed = false;

            error = false;

            initialized = true;

            isBusy = false;
        }



        //FUNCTIONS
        protected virtual void OnInitializationComplete(ProcessCompleteEventArgs e)
        {
            error = e.Error;

            initialized = true;

            isBusy = false;

            InitializationComplete.SafeTrigger(this, e);
        }

        protected virtual void OnProcessComplete(ProcessCompleteEventArgs e)
        {
            error = e.Error;

            completed = true;

            isBusy = false;

            ProcessComplete.SafeTrigger(this, e);
        }

        protected virtual void ResetProcess()
        {
            completed = false;

            error = false;
        }
    }
}