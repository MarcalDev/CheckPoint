using CheckPointBase.Context;
using CheckPointBase.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Data.Common
{
    public class CoreRepository<T> : ICoreRepository<T> where T : CoreEntity, new()
    {
        protected CheckPointContext _dbContext;
        public CheckPointContext DbContext
        {
            get { return _dbContext; }
            set { _dbContext = value; }
        }

        public void Delete(T model)
        {
            try
            {
                var rs = _dbContext.Conexao.Delete(model);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAll(List<T> model)
        {
            try
            {
                _dbContext.Conexao.BeginTransaction();

                foreach (var _model in model)
                {
                    var rs = _dbContext.Conexao.Delete(_model);

                    if (rs == 0)
                        throw new Exception("Erro ao excluir registro");
                }
                _dbContext.Conexao.Commit();
            }
            catch (Exception ex)
            {
                _dbContext.Conexao.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAll()
        {
            _dbContext.Conexao.DeleteAll<T>();
        }

        public void DeleteById(Guid? Id)
        {
            try
            {
                var rs = _dbContext.Conexao.Delete<T>(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return _dbContext.Conexao.Table<T>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<T> GetUsers()
        {
            try
            {
                return _dbContext.Conexao.Table<T>().ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<T> GetAllAtivos()
        {
            try
            {
                return _dbContext.Conexao.Table<T>().Where(t => t.Ativo == 1).ToList();
            }
            catch(Exception ex);
        }

        public T GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public DateTime? GetLastUpdate()
        {
            throw new NotImplementedException();
        }

        public void InsertOrReplace(T Model)
        {
            throw new NotImplementedException();
        }

        public void InsertOrReplace(List<T> Model)
        {
            throw new NotImplementedException();
        }

        public void Update(T model)
        {
            throw new NotImplementedException();
        }
    }
}
