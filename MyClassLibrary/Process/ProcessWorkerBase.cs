using MyClassLibrary;
using System;
using System.ComponentModel;


namespace MyClassLibrary.Process
{
    public abstract class ProcessWorkerBase
        : Process.ProcessBase, Process.IProcessWorker, System.IDisposable
    {
        //FIELDS
        protected BackgroundWorker backgroundWorker;


        //INITIALIZE
        public ProcessWorkerBase()
        {
            backgroundWorker = null;
        }


        //FINALIZE
        ~ProcessWorkerBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposeManagedResources)
        {
            if (!disposed)
            {
                MyUtilities.DisposeObject(backgroundWorker);

                if (disposeManagedResources)
                {
                    backgroundWorker = null;
                }
            }

            disposed = true;
        }


        //EVENTS
        protected virtual void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!ProcessExecution())
            {
                Error = true;
            }
        }

        protected virtual void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MyUtilities.DisposeObject(backgroundWorker);
            backgroundWorker = null;
            
            OnProcessComplete(new ProcessCompleteEventArgs(Error));
        }


        //METHODS
        public abstract Boolean ProcessExecution();

        public virtual void RunWorker()
        {
            if (IsBusy)
            {
                throw new Exception("The ProcessWorker IsBusy.");
            }

            ResetProcess();

            isBusy = true;

            ExecuteWorker();
        }


        //FUNCTIONS
        protected virtual void ExecuteWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            backgroundWorker.RunWorkerAsync();
        }
    }
}