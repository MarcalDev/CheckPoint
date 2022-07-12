using CheckPointBase.Context;
using CheckPointBase.Data.Common;
using CheckPointBase.Data.Interface;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Data.Repository
{
    public class RelatorioRepository : CoreRepository<Relatorio>, IRelatorioRepository
    {
        public RelatorioRepository()
        {
            DbContext = CheckPointContext.Current;
        }
        public Relatorio GetRelatorioByDate(DateTime data, int usuarioId)
        {
            try
            {
                var relatorio = _dbContext.Conexao.FindWithQuery<Relatorio>("SELECT * FROM RELATORIO WHERE DT_RELATORIO = ? AND USUARIO_ID = ?", data, usuarioId);

                return relatorio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Relatorio GetRelatorioById(Guid relatorioId)
        {
            try
            {
                var relatorio = _dbContext.Conexao.FindWithQuery<Relatorio>("SELECT * FROM RELATORIO WHERE Id = ?", relatorioId);

                return relatorio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Relatorio> GetRelatorios(Guid UsuarioId)
        {
            try
            {
                var relatorios = _dbContext.Conexao.Query<Relatorio>("SELECT * FROM RELATORIO WHERE USUARIO_ID = ?", UsuarioId);

                return relatorios;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertOrReplaceRelatorio(Relatorio relatorio)
        {
            try
            {
                var _novoRelatorio = new RelatorioRepository();
                if (relatorio == null)
                    return false;
                else
                {
                    _novoRelatorio.DbContext.Conexao.InsertOrReplace(relatorio);
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateSaldoRelatorio(Guid relatorioId, TimeSpan saldo)
        {
            try
            {
                _dbContext.Conexao.FindWithQuery<Ponto>("UPDATE RELATORIO SET SALDO = ? WHERE Id = ?", saldo, relatorioId);
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
