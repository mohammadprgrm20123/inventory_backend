﻿using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Migrations;

public class WebApiMigration
{
    public static void Main(string connectionString)
    {
        CreateDatabase(connectionString!);

        using var serviceProvider = CreateServices(connectionString!);
        using var scope = serviceProvider.CreateScope();

        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    private static ServiceProvider CreateServices(string connectionString)
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(
                    connectionString)
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    }

    private static void CreateDatabase(string connectionString)
    {
        var databaseName = GetDatabaseName(connectionString);
        var masterConnectionString =
            ChangeDatabaseName(connectionString, "master");
        var commandScript =
            $"if db_id(N'{databaseName}') is null create database [{databaseName}]";

        using var connection = new SqlConnection(masterConnectionString);
        using var command = new SqlCommand(commandScript, connection);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    private static string GetDatabaseName(string connectionString)
    {
        return new SqlConnectionStringBuilder(connectionString)
            .InitialCatalog;
    }

    private static string ChangeDatabaseName(string connectionString,
        string databaseName)
    {
        var csb = new SqlConnectionStringBuilder(connectionString)
        {
            InitialCatalog = databaseName
        };
        return csb.ConnectionString;
    }
}