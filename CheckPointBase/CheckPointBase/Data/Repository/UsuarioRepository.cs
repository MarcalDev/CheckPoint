using CheckPointBase.Context;
using CheckPointBase.Data.Common;
using CheckPointBase.Data.Interface;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Data.Repository
{
    public class UsuarioRepository : CoreRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository()
        {
            DbContext = CheckPointContext.Current;
        }

        public Usuario GetUsuarioById(int usuarioId)
        {
            try
            {
                var usuario = _dbContext.Conexao.FindWithQuery<Usuario>("SELECT * FROM USUARIO WHERE ID_USUARIO = ?",usuarioId);

                return usuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            try
            {
                var usuario = _dbContext.Conexao.FindWithQuery<Usuario>("SELECT * FROM USUARIO WHERE EMAIL = ?", email);

                return usuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Usuario GetUsuarioByLogin(string email, string senha)
        {
            try
            {
                var usuario = _dbContext.Conexao.FindWithQuery<Usuario>("SELECT * FROM USUARIO WHERE EMAIL = ? AND SENHA = ?", email, senha);

                return usuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool InsertOrReplaceUsuario(Usuario usuario)
        {
            try
            {
                var _novoUsuario = new UsuarioRepository();
                if (usuario == null)
                    return false;
                else
                {
                    _novoUsuario.DbContext.Conexao.InsertOrReplace(usuario);
                    return true;
                }
            } catch (Exception e)
            {
                throw e;
            }
        }

        



    }
}
