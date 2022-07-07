using CheckPointBase.Data.Common;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Data.Interface
{
    public interface IUsuarioRepository : ICoreRepository<Usuario>
    {
        Usuario GetUsuarioById(int usuarioId);

        Usuario GetUsuarioByEmail(string email);

        Usuario GetUsuarioByLogin(string email, string senha);

        bool InsertOrReplaceUsuario(Usuario usuario);

    }
}
