using System.Linq;

namespace SchoolInformationSystem.Common.Data
{
    public class MongoDBNoSqlDocumentCollection<T> : INoSqlDocumentCollection<T> where T :class
    {
        private IQueryable<T> _collection;

        public MongoDBNoSqlDocumentCollection(IQueryable<T> collection)
        {
            _collection = collection;
        }

        public IQueryable<T> Collection
        {
            get
            {
                return _collection;
            }
        }
    }
}