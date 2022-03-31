using System.Linq.Expressions;

namespace WarungKuApp.Interfaces;

public interface IKatalogService<T> where T : class
{
     Task<List<T>> GetAll();
     Task<List<T>> Get(int limit, int offset, string keyword);
     Task<T?> Get(int id);

     Task<T?> Get(Expression<Func<T, bool>> func);
}