using System;


namespace MyClassLibrary.Process
{
    public interface IProcess
    {
        //EVENT HANDLERS
        event EventHandler<ProcessCompleteEventArgs> InitializationComplete;

        event EventHandler<ProcessCompleteEventArgs> ProcessComplete;


        //PROPERTIES
        Boolean Completed
        {
            get;
        }

        Boolean Error
        {
            get;
        }

        Boolean Initialized
        {
            get;
        }

        Boolean IsBusy
        {
            get;
        }
    }
}
