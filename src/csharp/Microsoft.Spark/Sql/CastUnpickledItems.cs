﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Spark.Sql
{
    /// <summary>
    /// Cast UnpickledItems.
    /// </summary>
    internal static class CastUnpickledItems
    {
        /// <summary>
        /// If unpickledItems contains ArrayList, cast it to typed array.
        /// </summary>
        /// <param name="unpickledItems">Unpickled objects.</param>
        /// <returns>Unpickled objects after casting.</returns>
        public static object[] Cast(object unpickledItems)
        {
            var castUnpickledItems = new List<object>();
            foreach (object obj in (object[])unpickledItems)
            {
                castUnpickledItems.Add(
                    (obj.GetType() == typeof(RowConstructor)) ?
                    obj : CastHelper(obj as object[]));
            }

            return castUnpickledItems.ToArray();
        }

        /// <summary>
        /// Helper function to cast unpickled objects as needed.
        /// </summary>
        /// <param name="obj">Unpickled objects.</param>
        /// <returns>Original object or cast object</returns>
        public static object CastHelper(object[] obj)
        {
            var convertedObj = new List<object>();
            foreach (object o in obj)
            {
                convertedObj.Add(
                    (o != null && o is ArrayList arrayList) ?
                    TypeConverter(arrayList) : o);
            }

            return convertedObj.ToArray();
        }

        /// <summary>
        /// Cast arraylist to typed array.
        /// </summary>
        /// <param name="arrayList">ArrayList to be converted.</param>
        /// <returns>Typed array after casting.</returns>
        public static object TypeConverter(ArrayList arrayList)
        {
            Type type = arrayList[0].GetType();
            return type switch
            {
                _ when type == typeof(ArrayList) => CastArray(arrayList),
                _ => arrayList.ToArray(type)
            };
        }

        /// <summary>
        /// Cast simple array and array of arrays.
        /// </summary>
        /// <param name="arrayList">ArrayList to be converted.</param>
        /// <returns>Typed array after casting.</returns>
        public static object CastArray(ArrayList arrayList)
        {
            var convertedArray = new ArrayList();
            foreach (ArrayList al in arrayList)
            {
                convertedArray.Add(TypeConverter(al));
            }

            return convertedArray.ToArray(convertedArray[0].GetType());
        }
    }
}
