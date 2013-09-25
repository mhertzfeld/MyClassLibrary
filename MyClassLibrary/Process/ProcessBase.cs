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



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.