using System;
using System.Linq.Expressions;

namespace SchoolInformationSystem.Common.Data
{
    public interface IDocumentProvider
    {

        void CreateCollectionIfNotExists<T>(string collectionName) where T :  class, IHaveId;

        INoSqlDocumentCollection<T> GetCollection<T>() where T : class, IHaveId;

        void CreateCollectionIfNotExists(Type type, string collectionName);

        void RegisterCollectionWithType(Type type, string collectionName);

        void CreateDatabaseIfNotExists(string databaseName);

        void UseDatabase(string databaseName);

        void Connect();

        T UpdateDocument<T>(T document) where T : class, IHaveId;

        T CreateDocument<T>(T document) where T : class, IHaveId;
        void UpdateSubDocumentWithID<T, TProperty>(Expression<Func<T, TProperty>> subCollectionSelector, 
            object subDocumentID, object updates, object documentID = null) where T : class, IHaveId;
            
        void SetServiceProvider(IServiceProvider provider);

    }
}