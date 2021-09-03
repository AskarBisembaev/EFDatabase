using System;
using System.Linq;
using EFDatabase.Models;

namespace EFDatabase
{
	class Program
	{
		static void Main(string[] args)
		{
            var context = new NorthwindContext();
            var categories = context.Categories.ToList();

            var products = context.Products.ToList();

            var result = categories.GroupJoin(
                        products,
                        t => t.CategoryId, 
                        pl => pl.CategoryId, 
                        (category, product) => new  
                        {
                            CName = category.CategoryName,
                            CID = category.CategoryId,
                            CDescription = category.Description,
                        PName = product.Select(p => p.ProductName),
                        });

            foreach (var category in result)
            {
                 Console.WriteLine($"Category ID - {category.CID}, \nCategory Name - {category.CName}, \n{category.CDescription}" +
                     $"\nProducts from this category:");
                foreach (string product in category.PName)
                {
                    Console.WriteLine($"\t{product}");
                }
                Console.WriteLine();
            }
            
            var employees = context.Employees;
            Console.WriteLine($"Table {employees.EntityType.Name} content: \n");
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeId} {employee.LastName} {employee.FirstName}");
            }
        }
	}
}
