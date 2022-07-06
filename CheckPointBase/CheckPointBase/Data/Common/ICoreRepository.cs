using CheckPointBase.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Data.Common
{
    public interface ICoreRepository<T> where T : CoreEntity
    {
        T GetById(Guid Id);
        void InsertOrReplace(T Model);
        void InsertOrReplace(List<T> Model);
        List<T> GetAll();
        
        DateTime? GetLastUpdate();
        void DeleteById(Guid? Id);
        void Delete(T Model);
        void DeleteAll(List<T> Model);
        void DeleteAll();
        void Update(T model);
        List<T> GetAllAtivos();
    }
}
