using CheckPointBase.Context;
using CheckPointBase.Data.Common;
using CheckPointBase.Data.Interface;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Data.Repository
{
    public class PontoRepository : CoreRepository<Ponto>, IPontoRepository
    {
        public PontoRepository()
        {
            DbContext = CheckPointContext.Current;
        }
        public List<Ponto> GetPontosByRelatorio(int relatorioId, int usuarioId)
        {
            try
            {
                var pontos = _dbContext.Conexao.Query<Ponto>("SELECT * FROM PONTO WHERE RELATORIO_ID = ? AND USUARIO_ID = ?", relatorioId, usuarioId);
                
                return pontos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ponto GetPontoById(int pontoId)
        {
            try
            {
                var ponto = _dbContext.Conexao.FindWithQuery<Ponto>("SELECT * FROM PONTO WHERE ID_PONTO = ?", pontoId);

                return ponto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertOrReplacePonto(Ponto ponto)
        {
            try
            {
                var _novoPonto = new PontoRepository();
                if (ponto == null)
                    return false;
                else
                {
                    _novoPonto.DbContext.Conexao.InsertOrReplace(ponto);
                    return true;
                }
            } catch(Exception e)
            {
                throw e;
            }
        }

        public List<Ponto> ListaPontosRecentes(Usuario usuarioId)
        {
            try
            {
                var pontos = _dbContext.Conexao.Query<Ponto>("SELECT * FROM PONTO WHERE USUARIO_ID = ?", usuarioId);
                
                return pontos;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Ponto> ListaTodosPontos()
        {
            try
            {
                var pontos = _dbContext.Conexao.Query<Ponto>("SELECT * FROM PONTO");

                return pontos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
