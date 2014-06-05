using System;


namespace MyClassLibrary.Process
{
    public interface IProcessWorker
        : Process.IProcess
    {
        //METHODS
        Boolean ProcessExecution();

        void RunWorker();
    }
}