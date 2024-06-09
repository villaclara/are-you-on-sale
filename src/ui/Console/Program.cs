// See https://aka.ms/new-console-template for more information
using Core.Interfaces;
using Core.Repository.Interfaces;
using Core.Repository.Services;

Console.WriteLine("Hello, World!");


IProductRepository repo;
ProductRepository productRepository;

IApplicationDBContext db;