using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;

namespace SchoolInformationSystem.Common.Data
{
    /// <summary>
    /// NoSql document context.  Any property marked as IQueryable will be considered as a collection.
    /// </summary>

    public abstract class DocumentContext
    {
        private IDocumentProvider _provider;
        private string _databaseName;
        

        public DocumentContext(IDocumentProvider provider, 
            IServiceProvider serviceProvider = null, 
            string databaseName = "")
        {
            
            _provider = provider;
            if(serviceProvider != null)
            {
                _provider.SetServiceProvider(serviceProvider);
            }
            _databaseName = databaseName;
            ResolveMissingDatabaseName();
            SetupDatabase();

        }

        public void Create<T>(T document) where T : class, IHaveId
        {
            _provider.CreateDocument<T>(document);
        }

        public void Update<T>(T document) where T : class, IHaveId
        {
            _provider.UpdateDocument<T>(document);
        }
        
        public void UpdateSubDocumentWithID<T, TProperty>(Expression<Func<T, TProperty>> subCollectionSelector, object subDocumentID, object updates, object documentID = null) where T : class, IHaveId
        {
            _provider.UpdateSubDocumentWithID(subCollectionSelector, subDocumentID, updates, documentID);
        }

        private void ResolveMissingDatabaseName()
        {
            if (String.IsNullOrWhiteSpace(_databaseName))
            {
                _databaseName = this.GetType().Name;
            }
        }

        private void SetupDatabase()
        {
            try
            {
                
                _provider.Connect();
                _provider.CreateDatabaseIfNotExists(_databaseName);
                _provider.UseDatabase(_databaseName);
                
                IEnumerable<PropertyInfo> collectionTypeProperties = this.GetType()
                    .GetProperties().Where(x => typeof(IQueryable<>).IsAssignableFrom(x.PropertyType.GetGenericTypeDefinition()));
    
                foreach (PropertyInfo info in collectionTypeProperties)
                {
                    string collectionName = info.Name;
                    Type collectionType = info.PropertyType.GetGenericArguments()[0];
                    if (typeof(IHaveId).IsAssignableFrom(collectionType))
                    {
                        _provider.CreateCollectionIfNotExists(collectionType, collectionName);
                        _provider.RegisterCollectionWithType(collectionType, collectionName);
                        object genericCollection = typeof(IDocumentProvider)
                            .GetMethod("GetCollection")
                            .MakeGenericMethod(collectionType)
                            .Invoke(_provider, new object[] { });
                        object value = genericCollection.GetType().GetProperty("Collection").GetValue(genericCollection, new object[] { });
    
                        info.SetValue(this, value);
                    }
                    else
                    {
                        throw new InvalidOperationException("IQueryable type must be assignable from IHaveId");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}