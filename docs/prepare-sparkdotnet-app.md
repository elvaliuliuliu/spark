### Submit .NET for Apache Spark Batch Jobs via Azure CLI Synapse Tool
This documentation provides instructions on how to create and publish your .NET for Apache Spark application before submitting it via Azure CLI Synapse Tool.

#### Prepare your environment
You need to set up some prerequisite dependencies before you begin writing your app. Please see the documentation [here](https://docs.microsoft.com/en-us/dotnet/spark/tutorials/get-started#prepare-your-environment) to set up your environment.

#### Create and write your .NET for Apache Spark application

- Create a console app with .NET Core CLI
```
dotnet new console -o mySparkApp
cd <your application folder>
```

- Write your .NET for Apache Spark application
To use .NET for Apache Spark in an app, install the Microsoft.Spark package. You can check releases [here](https://github.com/dotnet/spark/releases). Then code your app and add the data files. Please see more detailed instructions [here](https://docs.microsoft.com/en-us/dotnet/spark/tutorials/get-started#write-a-net-for-apache-spark-app).

#### Publish and zip up your .NET for Apache Spark application

- Publish your .NET for Apache Spark app as self-contained
```
cd <your application folder>
dotnet publish -f netcoreapp3.1 -r ubuntu.16.04-x64
```

- Produce <your application>.zip for the published files
```
cd <your application folder>\bin\Debug\netcoreapp3.1\ubuntu.16.04-x64\publish
```

Run the following command to zip up the published files on **Linux**:
```
zip -r <your application>.zip
```

Run the following commands to zip up the published files on **Windows**:
```
7z a <your application>.zip
```

Now you are ready to submit your application via Azure CLI Synapse Tool!


