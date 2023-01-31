using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
	public interface IChildReadOnlyRestDataProvider<T> where T : class
	{
		int Count(string parentId);
		IEnumerable<T> GetCurrentList(string parentId, out string previousList, out string nextList, IFilter filter = null);
		IEnumerable<T> GetAll(string parentId, IFilter filter = null);
		IEnumerable<T> GetAllWithoutParent(IFilter filter = null);
		T GetByID(string parentId, string id);
	}

	public interface IChildRestDataProvider<T> : IChildReadOnlyRestDataProvider<T> where T : class
	{
		T Create(T entity, string parentId);
		T Update(T entity, string parentId, string id);
		bool Delete(string parentId, string id);
	}

}
