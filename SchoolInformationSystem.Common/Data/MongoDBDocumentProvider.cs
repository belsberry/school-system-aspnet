using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Bson.Serialization;

namespace SchoolInformationSystem.Common.Data
{
    public class MongoDBDocumentProvider : IDocumentProvider
    {
        private MongoClient _client;
        private string _connectionString;
        private Dictionary<Type, string> _collections;
        private IMongoDatabase _database;
        
        private IServiceProvider _serviceProvider = null;

        public MongoDBDocumentProvider(string connectionString)
        {
            _connectionString = connectionString;
            _collections = new Dictionary<Type, string>();
        }
        
        public void SetServiceProvider(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        public void Connect()
        {
            _client = new MongoClient(_connectionString);
        }

        public void CreateCollectionIfNotExists(Type type, string collectionName)
        {
            return;
        }

        public void CreateDatabaseIfNotExists(string databaseName)
        {
            return;//No need since the lib does this for us
        }
        
        private delegate object Inject();
        
        private class Injector
        {
            Type _type;
            IServiceProvider _provider;
            public Injector(Type typeToCreate, IServiceProvider provider)
            {
                _type = typeToCreate;
                _provider = provider;
            }
            public object GetInstance()
            {
                return _provider.GetService(_type);
            }
        }
        private Delegate CreateInjectorDelegate(Type typeToInject)
        {
            Injector inj = new Injector(typeToInject, _serviceProvider);
            MethodInfo mi = typeof(Injector).GetMethod("GetInstance");
            return Delegate.CreateDelegate(typeof(Inject), inj, mi, false);
        }
        
        public void RegisterCollectionWithType(Type type, string collectionName)
        {
            if(!_collections.ContainsKey(type))
            {
                _collections.Add(type, collectionName);
            }
            if(_serviceProvider != null)
            {
                BsonClassMap map = new BsonClassMap(type);
                map.AutoMap();
                Delegate getValue = CreateInjectorDelegate(type);
                map.MapCreator(getValue);
                BsonClassMap.RegisterClassMap(map);
            }
        }

        public void UseDatabase(string databaseName)
        {
            _database = _client.GetDatabase(databaseName);
        }

        public void CreateCollectionIfNotExists<T>(string collectionName) where T : class, IHaveId
        {
            return; //Don't need to do this
        }

        public T CreateDocument<T>(T document) where T : class, IHaveId
        {
            if (_collections.ContainsKey(typeof(T)))
            {
                Console.WriteLine("in create");
                string colName = _collections[typeof(T)];
                Console.WriteLine(colName);
                _database.GetCollection<T>(colName)
                    .InsertOneAsync(document)
                    .Wait();
                Console.WriteLine("Complete");
                return document;
            }
            else
            {
                throw new InvalidOperationException("No Existing collection for the type of document");
            }
        }

        public INoSqlDocumentCollection<T> GetCollection<T>() where T :class, IHaveId
        {
            if (_collections.ContainsKey(typeof(T)))
            {
                string colName = _collections[typeof(T)];
                return new MongoDBNoSqlDocumentCollection<T>(_database.GetCollection<T>(colName).AsQueryable<T>());
            }
            else
            {
                throw new InvalidOperationException("No Existing collection for the type of document");
            }
        }

        public T UpdateDocument<T>(T document) where T : class, IHaveId
        {
            IMongoCollection<T> collection = GetCollectionByType<T>();
            if (collection != null)
            {
                FilterDefinition<T> def = Builders<T>.Filter.Eq("_id", document._id);
                collection
                    .ReplaceOneAsync(def, document)
                    .Wait();
                return document;
            }
            else
            {
                throw new InvalidOperationException("No Existing collection for the type of document");
            }
        }
        
        
        public void UpdateSubDocumentWithID<T, TProperty>(Expression<Func<T, TProperty>> subCollectionSelector, object subDocumentID, object updates, object documentID = null) where T : class, IHaveId
        {
            var body = subCollectionSelector.Body as MemberExpression;
            var propertyInfo = body.Member as PropertyInfo;
            
            FilterDefinition<T> filter = null;
            
            if(documentID != null)
            {
                filter = Builders<T>.Filter
                    .And(Builders<T>.Filter.Eq(String.Format("{0}._id", propertyInfo.Name), subDocumentID), Builders<T>.Filter.Eq("_id", documentID));  
            }
            else
            {
                filter = Builders<T>.Filter.Eq(String.Format("{0}._id", propertyInfo.Name), subDocumentID);
            }
            var valuesToUpdate = updates.GetType().GetProperties();
            UpdateDefinition<T> update = null;
            foreach(PropertyInfo info in valuesToUpdate)
            {
                string propertySelector = String.Format("{0}.$.{1}", propertyInfo, info.Name);
                object value = info.GetValue(updates, null);
                if(update == null)
                {
                    update = Builders<T>.Update.Set(propertySelector, value);
                }
                else
                {
                    update = update.Set(propertySelector, value);
                }    
            }
            IMongoCollection<T> collection = GetCollectionByType<T>();
            if(collection != null)
            {
                collection.UpdateOneAsync(filter, update).Wait();    
            }
        }
        
        private IMongoCollection<T> GetCollectionByType<T>()
        {
            if(_collections.ContainsKey(typeof(T)))
            {
                string colName = _collections[typeof(T)];
                return _database.GetCollection<T>(colName);    
            }
            else
            {
                return null;
            }
        }
    }
}