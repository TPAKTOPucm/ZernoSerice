using System.Collections.Generic;
using Zerno.Models;

namespace Zerno.Data
{
    public interface IGrainStorage
    {
        int CountProducts();
        int CountUsers();

        ICollection<Product> GetProducts();
        ICollection<Product> GetProducts(int index, int count);

        ICollection<Request> GetRequests();
        ICollection<Request> GetRequestsByUserId(int userId);
        ICollection<Request> GetRequestsByProductId(int productId);

        ICollection<User> GetUsers();
        ICollection<User> GetUsers(int index, int count);

        Product GetProductById(int productId);
        Request GetRequestById(int requestId);
        User GetUserById(int userId);

        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

        void CreateRequest(Request request);
        void UpdateRequest(Request request);
        void DeleteRequest(Request request);

        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
