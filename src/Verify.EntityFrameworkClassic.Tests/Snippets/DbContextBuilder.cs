﻿using System.Threading.Tasks;
using EfLocalDb;

// LocalDb is used to make the sample simpler.
// Replace with a real DbContext
public static class DbContextBuilder
{
    static DbContextBuilder()
    {
        sqlInstance = new SqlInstance<SampleDbContext>(
            buildTemplate: CreateDb,
            constructInstance: connection => new SampleDbContext(connection),
            instanceSuffix:"Classic");
    }

    static SqlInstance<SampleDbContext> sqlInstance;

    static async Task CreateDb(SampleDbContext context)
    {
        await context.CreateOnExistingDb();

        var company1 = new Company
        {
            Id = 1,
            Content = "Company1"
        };
        var employee1 = new Employee
        {
            Id = 2,
            CompanyId = company1.Id,
            Content = "Employee1",
            Age = 25
        };
        var employee2 = new Employee
        {
            Id = 3,
            CompanyId = company1.Id,
            Content = "Employee2",
            Age = 31
        };
        var company2 = new Company
        {
            Id = 4,
            Content = "Company2"
        };
        var employee4 = new Employee
        {
            Id = 5,
            CompanyId = company2.Id,
            Content = "Employee4",
            Age = 34
        };
        var company3 = new Company
        {
            Id = 6,
            Content = "Company3"
        };
        var company4 = new Company
        {
            Id = 7,
            Content = "Company4"
        };
        context.Companies.Add(company1);
        context.Companies.Add(company2);
        context.Companies.Add(company3);
        context.Companies.Add(company4);
        context.Employees.Add(employee1);
        context.Employees.Add(employee2);
        context.Employees.Add(employee4);
        await context.SaveChangesAsync();
    }

    public static Task<SqlDatabase<SampleDbContext>> GetDatabase(string suffix)
    {
        return sqlInstance.Build(suffix);
    }
}