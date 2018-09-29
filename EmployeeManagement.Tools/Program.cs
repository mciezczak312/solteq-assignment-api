using System;
using System.Collections.Generic;
using EmployeesManagement.Core.Entitites;
using EmployeesManagement.Infrastructure.Data;
using EmployeesManagement.Infrastructure.Repositories;

namespace EmployeeManagement.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContext ctx = new DbContext("server=localhost;port=3306;user=root;password=root;database=solteq");
            SalaryRepository repo = new SalaryRepository(ctx);
            var rand = new Random();


            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    var salary = new Salary
                    {
                        EmployeeId = i + 1,
                        Amount = Math.Round(rand.NextDouble() * (7000 - 1000) + 1000, 2)
                    };

                    salary.FromDate = new DateTime(2018, j+1, 2);
                    if (j == 11)
                    {
                        salary.ToDate = new DateTime(2019, 1, 1);
                    }
                    else
                    {
                        salary.ToDate = new DateTime(2018, j + 2, 1);
                    }

                    repo.Insert(salary);
                }

                
            }

            

            Console.WriteLine("Hello World!");
        }
    }
}
