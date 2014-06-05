using System;



namespace MyClassLibrary
{
    public static class EventHandlerExtensions
    {
        public static void SafeTrigger<T_EventArgs>(this EventHandler<T_EventArgs> eventToTrigger, Object sender, T_EventArgs eventArgs)
            where T_EventArgs : EventArgs
        {
            if (eventToTrigger != null)
            {
                eventToTrigger(sender, eventArgs);
            }
        }

        public static T_ReturnType SafeTrigger<T_EventArgs, T_ReturnType>(this EventHandler<T_EventArgs> eventToTrigger, Object sender, T_EventArgs eventArgs, Func<T_EventArgs, T_ReturnType> retrieveDataFunction)
            where T_EventArgs : EventArgs
        {
            if (retrieveDataFunction == null)
            {
                throw new ArgumentNullException("retrieveDataFunction");
            }

            if (eventToTrigger != null)
            {
                eventToTrigger(sender, eventArgs);
                T_ReturnType returnData = retrieveDataFunction(eventArgs);
                return returnData;
            }
            else
            {
                return default(T_ReturnType);
            }
        }
    }
}