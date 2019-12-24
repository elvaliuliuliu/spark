// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Spark.Sql;
using Microsoft.Spark.Sql.Types;
using static Microsoft.Spark.Sql.Functions;

namespace Microsoft.Spark.Examples.Sql.Batch
{
    /// <summary>
    /// A simple example demonstrating basic Spark SQL features.
    /// </summary>
    internal sealed class Basic : IExample
    {
        public void Run(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine(
                    "Usage: Basic <path to SPARK_HOME/examples/src/main/resources/people.json>");
                Environment.Exit(1);
            }



            var spark = SparkSession.Builder().GetOrCreate();
            var df = spark.Range(0, 5);

            var schema = new StructType(new[]
                {
                    new StructField("id", new IntegerType())
                });
            Func<Column, Column> udfrow = Udf<int>(
                r => new GenericRow(new object[] { r + 100 }), schema);

            /*Func<Column, Column> workingudf = Udf<int, int>(
                r => r + 100);

            Func<Column, Column> udfarray = Udf<int, int[]>(
                r => new int[] { r, r });*/




            /*Console.WriteLine("This is testing return string working --------------------------------------");
            var udfDf2 = df.Select(workingudf(df["id"]).As("udf_col2"));
            udfDf2.PrintSchema();
            udfDf2.Show();*/

            /*Console.WriteLine("This is testing taking row type --------------------------------------");
            Func<Column, Column> udfRow = Udf<Row, string>(
                (row) =>
                {
                    string city = row.GetAs<string>("city");
                    string state = row.GetAs<string>("state");
                    return $"{city},{state}";
                });
            var udfDf3 = df.Select(udfRow(df["info"]).As("udf_col"));
            udfDf3.PrintSchema();
            udfDf3.Show();*/

            /*Console.WriteLine("This is testing return array udf-------------------------------------------------------");
            var udfDf4 = df.Select(udfarray(df["id"]).As("udf_col"));
            udfDf4.PrintSchema();
            udfDf4.Show();*/

            Console.WriteLine("This is testing return row udf test test-------------------------------------------------------");
            var udfDf = df.Select(udfrow(df["id"]).As("udf_col"));
            udfDf.PrintSchema();
            // udfDf.Show();
            Row[] rows = udfDf.Collect().ToArray();




            // udfDf.Select("udf_col.*").Show();
            // udfDf.Select(udfDf["udf_col.col1"], udfDf["udf_col.col2"]).Show();



            spark.Stop();
        }
    }
}
