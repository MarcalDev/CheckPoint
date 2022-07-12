using CheckPointBase.Data.Common;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Data.Interface
{
    public interface IRelatorioRepository : ICoreRepository<Relatorio>
    {
        Relatorio GetRelatorioById(Guid relatorioId);
        Relatorio GetRelatorioByDate(DateTime data, int usuarioId);
        List<Relatorio> GetRelatorios(Guid UsuarioId);
        bool InsertOrReplaceRelatorio(Relatorio relatorio);
        bool UpdateSaldoRelatorio(Guid relatorioId, TimeSpan saldo);
    }
}
