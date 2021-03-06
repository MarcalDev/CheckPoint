using CheckPointBase.Data.Common;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Data.Interface
{
    public interface IPontoRepository : ICoreRepository<Ponto>
    {
        Ponto GetPontoById(int pontoId);
        List<Ponto> GetPontosByRelatorio(Guid relatorioId);
        List<Ponto> ListaPontosRecentes(Usuario usuarioId);
        bool InsertOrReplacePonto(Ponto ponto);
        List<Ponto> ListaTodosPontos(Guid usuarioId);
        Ponto GetLastPonto(Guid usuarioId);
        bool SetPontoFinalizado(Guid pontoId, DateTime dataFim, string localFim);
    }
}
