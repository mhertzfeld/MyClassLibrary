using MyClassLibrary;
using System;
using System.ComponentModel;


namespace MyClassLibrary.Process
{
    public abstract class ProcessWorkerBase
        : ProcessBase, IProcessWorker, IDisposable
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
                MyTools.DisposeObject(backgroundWorker);

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
            MyTools.DisposeObject(backgroundWorker);
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



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.