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



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.