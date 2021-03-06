﻿using System;
using System.Diagnostics;
using System.Reflection;


namespace MyClassLibrary
{
    public static class MyTrace
    {
        public static void WriteMethodError(MethodBase methodBase)
        {
            Trace.WriteLine("An error occured within '" + String.Format("{0}.{1}.{2}()", methodBase.ReflectedType.Namespace, methodBase.ReflectedType.Name, methodBase.Name) + ".");
        }
    }
}