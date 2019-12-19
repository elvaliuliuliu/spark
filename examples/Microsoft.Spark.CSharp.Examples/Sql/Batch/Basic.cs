// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
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
                    new StructField("id", new StringType())
                });
            Func<Column, Column> udf = Udf<int>(
                r => new GenericRow(new object[] { r + 100 }), schema);

            
            Func<Column, Column> workingudf = Udf<int, int>(
                r => r + 100);

            Func<Column, Column> udfarray = Udf<int, int[]>(
                r => new int[] { r, r });


            

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


            /*Console.WriteLine("This is testing return object udf-------------------------------------------------------");
            var udfDf3 = df.Select(udfobject(df["id"]).As("udf_col"));
            udfDf3.PrintSchema();
            udfDf3.Show();*/

            /*Console.WriteLine("This is testing return array udf-------------------------------------------------------");
            var udfDf4 = df.Select(udfarray(df["id"]).As("udf_col"));
            udfDf4.PrintSchema();
            udfDf4.Show();*/

            Console.WriteLine("This is testing return row udf-------------------------------------------------------");
            var udfDf = df.Select(udf(df["id"]).As("udf_col"));
            udfDf.PrintSchema();
            udfDf.Show();


            // udfDf.Select("udf_col.*").Show();
            // udfDf.Select(udfDf["udf_col.col1"], udfDf["udf_col.col2"]).Show();



            spark.Stop();

            

            /*SparkSession spark = SparkSession
                .Builder()
                .AppName(".NET Spark SQL basic example")
                .Config("spark.some.config.option", "some-value")
                .GetOrCreate();

            // Need to explicitly specify the schema since pickling vs. arrow formatting
            // will return different types. Pickling will turn longs into ints if the values fit.
            // Same as the "age INT, name STRING" DDL-format string.
            var inputSchema = new StructType(new[]
            {
                new StructField("age", new IntegerType()),
                new StructField("name", new StringType())
            });
            DataFrame df = spark.Read().Schema(inputSchema).Json(args[0]);

            Spark.Sql.Types.StructType schema = df.Schema();
            Console.WriteLine(schema.SimpleString);

            IEnumerable<Row> rows = df.Collect();
            foreach (Row row in rows)
            {
                Console.WriteLine(row);
            }

            df.Show();

            df.PrintSchema();

            df.Select("name", "age", "age", "name").Show();

            df.Select(df["name"], df["age"] + 1).Show();

            df.Filter(df["age"] > 21).Show();

            df.GroupBy("age")
                .Agg(Avg(df["age"]), Avg(df["age"]), CountDistinct(df["age"], df["age"]))
                .Show();

            df.CreateOrReplaceTempView("people");

            // Registering Udf for SQL expression.
            DataFrame sqlDf = spark.Sql("SELECT * FROM people");
            sqlDf.Show();

            spark.Udf().Register<int?, string, string>(
                "my_udf",
                (age, name) => name + " with " + ((age.HasValue) ? age.Value.ToString() : "null"));

            sqlDf = spark.Sql("SELECT my_udf(*) FROM people");
            sqlDf.Show();

            // c
            Func<Column, Column, Column> addition =
                Udf<string, Row> ((str) => new GenericRow(new object[] {1}));
            df.Select(addition(df["age"], df["name"])).Show();

            // Chaining example:
            Func<Column, Column> addition2 = Udf<string, string>(str => $"hello {str}!");
            df.Select(addition2(addition(df["age"], df["name"]))).Show();

            // Multiple UDF example:
            df.Select(addition(df["age"], df["name"]), addition2(df["name"])).Show();

            // UDF return type as array.
            Func<Column, Column> udfArray =
                Udf<string, string[]>((str) => new string[] { str, str + str });
            df.Select(Explode(udfArray(df["name"]))).Show();

            // UDF return type as map.
            Func<Column, Column> udfMap =
                Udf<string, IDictionary<string, string[]>>(
                    (str) => new Dictionary<string, string[]> { { str, new[] { str, str } } });
            df.Select(udfMap(df["name"]).As("UdfMap")).Show(truncate: 50);

            // Joins.
            DataFrame joinedDf = df.Join(df, "name");
            joinedDf.Show();

            DataFrame joinedDf2 = df.Join(df, new[] { "name", "age" });
            joinedDf2.Show();

            DataFrame joinedDf3 = df.Join(df, df["name"] == df["name"], "outer");
            joinedDf3.Show();

            spark.Stop();*/
        }
        private static int Counter(ArrayList all_emails)
        {
            return all_emails.Count;
        }
    }
}
