using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;


namespace MyClassLibrary.Database
{
    public static class DatabaseTools
    {
        public static void AddToDbDataParameterList<T_ParameterType, T_DbParameter>(String parameter, ICollection<T_ParameterType> parameterList, ICollection<T_DbParameter> dbParameterList, Boolean incrementingParameter)
    where T_DbParameter : IDbDataParameter, new()
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            Int32 counter = 0;

            foreach (T_ParameterType parameterValue in parameterList)
            {
                T_DbParameter dbParameter = new T_DbParameter();

                if (incrementingParameter)
                {
                    dbParameter.ParameterName = parameter + counter;

                    counter += 1;
                }
                else
                {
                    dbParameter.ParameterName = parameter;
                }

                dbParameter.Value = parameterValue.ToString();

                dbParameterList.Add(dbParameter);
            }
        }

        public static String CreateConcantinatedFieldString<T>(IDataReader dataReader, List<T> fieldIndexList, String concatinateString)
        {
            Int32 convertedIndexNumber;

            String fieldStringValue;

            String result = null;

            StringBuilder stringBuilder = null;

            foreach (T indexNumber in fieldIndexList)
            {
                convertedIndexNumber = Convert.ToInt32(indexNumber);

                if (!dataReader.IsDBNull(convertedIndexNumber))
                {
                    fieldStringValue = dataReader[convertedIndexNumber].ToString().Trim();

                    if (stringBuilder == null)
                    {
                        stringBuilder = new StringBuilder();
                        stringBuilder.Append(fieldStringValue);
                    }
                    else
                    {
                        stringBuilder.Append(concatinateString + fieldStringValue);
                    }
                }
            }

            if (stringBuilder != null)
            {
                result = stringBuilder.ToString();
            }

            return result;
        }

        public static String CreateCSVString<T>(ICollection<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            StringBuilder stringBuilder = null;
            
            foreach (T value in list)
            {
                if (stringBuilder == null)
                {
                    stringBuilder = new StringBuilder(list.Count * 50);
                    stringBuilder.Append(value.ToString());
                }
                else
                {
                    stringBuilder.Append("," + value.ToString());
                }
            }

            return stringBuilder.ToString();
        }

        public static String CreateCSVStringSingleQuotes<T>(ICollection<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            StringBuilder stringBuilder = null;

            foreach (T value in list)
            {
                if (stringBuilder == null)
                {
                    stringBuilder = new StringBuilder(list.Count * 50);
                    stringBuilder.Append("'" + value.ToString() + "'");
                }
                else
                {
                    stringBuilder.Append("," + "'" + value.ToString() + "'");
                }
            }

            return stringBuilder.ToString();
        }

        public static String CreateParameterString(String parameter, Int32 parameterCount, Boolean incrementingParameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            if (parameterCount < 1)
            {
                throw new ArgumentOutOfRangeException("parameterCount");
            }

            StringBuilder stringBuilder = null;

            for (Int32 counter = 0; counter < parameterCount; counter += 1)
            {
                String newParameter;

                if (incrementingParameter)
                {
                    newParameter = parameter + counter;
                }
                else
                {
                    newParameter = parameter;
                }

                if (stringBuilder == null)
                {
                    stringBuilder = new StringBuilder();
                    stringBuilder.Append(newParameter);
                }
                else
                {
                    stringBuilder.Append("," + newParameter);
                }
            }

            return stringBuilder.ToString();
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.