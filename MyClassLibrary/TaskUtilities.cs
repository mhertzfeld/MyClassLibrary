using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MyClassLibrary
{
    public static class TaskUtilities
    {
        public static Boolean ProcessTaskListResults(List<Task<Boolean>> taskList)
        {
            Boolean taskListResult = true;

            foreach (Task<Boolean> task in taskList)
            {
                Boolean taskResult = task.Result;

                task.Dispose();

                if (!taskResult)
                { taskListResult = false; }
            }

            return taskListResult;
        }
    }
}