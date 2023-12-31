﻿using Microsoft.EntityFrameworkCore;
using Zerno.Data;
using Zerno.Models;

namespace Zerno.Services
{
    public class GrainService : IGrainStorage
    {
        private readonly GrainContext _context;
        public GrainService(GrainContext context)
        {
            _context = context;
        }
        public int CountProducts() => _context.Products.Count();

        public int CountUsers() => _context.Users.Count();

        public void CreateProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public void CreateRequest(Request request)
        {
            _context.Add(request);
            _context.SaveChanges();
        }

        public void CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }

        public void DeleteRequest(Request request)
        {
            _context.Remove(request);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }

        public Product GetProductById(int productId) => _context.Products.Include(p => p.Requests).Include(p => p.Dealer).Where(p => p.Id == productId).FirstOrDefault();

        public ICollection<Product> GetProducts() => _context.Products.Include(p => p.Requests).Include(p => p.Dealer).ToList();

        public ICollection<Product> GetProducts(int index, int count) => _context.Products.Include(p => p.Requests).Include(p => p.Dealer).Skip(index).Take(count).ToList();

        public Request GetRequestById(int requestId) => _context.Requests.Include(r => r.Product).Include(r => r.Wanter).Where(r => r.Id == requestId).FirstOrDefault();

        public ICollection<Request> GetRequests() => _context.Requests.Include(r => r.Product).Include(r => r.Wanter).ToList();

        public ICollection<Request> GetRequestsByProductId(int productId) => _context.Requests.Include(r => r.Wanter).Include(r => r.Product).Where(r => r.ProductId == productId).ToList();

        public ICollection<Request> GetRequestsByUserId(int userId) => _context.Requests.Include(r => r.Wanter).Include(r => r.Product).Where(r => r.WanterId == userId).ToList();

        public User GetUserById(int userId) => _context.Users.Include(u => u.Products).Include(u => u.BuyRequests).Where(u => u.Id == userId).FirstOrDefault();

        public ICollection<User> GetUsers() => _context.Users.Include(u => u.Products).Include(u => u.BuyRequests).ToList();

        public ICollection<User> GetUsers(int index, int count) => _context.Users.Include(u => u.Products).Include(u => u.BuyRequests).Skip(index).Take(count).ToList();

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
