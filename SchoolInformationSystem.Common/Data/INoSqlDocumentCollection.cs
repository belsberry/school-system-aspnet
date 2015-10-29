using System.Linq;

namespace SchoolInformationSystem.Common.Data
{
    public interface INoSqlDocumentCollection<T> where T : class
    {
        IQueryable<T> Collection { get; }
    }
}