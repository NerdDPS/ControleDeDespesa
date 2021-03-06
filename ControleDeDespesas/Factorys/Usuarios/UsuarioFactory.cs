﻿using Factorys.Ninject;
using Modelos;
using Modelos.ViewModels;

using Persistencia.DAO;
using Factorys.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Factorys
{
    public static class UsuarioFactory
    {


        private static CentroDeCustoDAO ccDAO = Injections.CentroDeCustoInject();

        /// <summary>
        /// Cria uma usuário com base em UsuarioModelView
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ccDAO"></param>
        /// <returns></returns>

        public static CadastroDeUsuario GeraUsuario(UsuarioModelView model)
        {
           
            CadastroDeUsuario usuario = new CadastroDeUsuario() {
                CentroDeCusto = ccDAO.GetByCodigo(model.CentroDeCusto),
                Cpf = model.Cpf,
                Email = model.Email,
                Id = model.Id,
                IsAdmin= model.IsAdmin,                
                Login = model.Login,
                Nome = model.Nome,
                Senha = model.Senha,
                Role = model.Role
            };

            return usuario;
        }

        /// <summary>
        /// devolve uma lista de usuários com base em uma lista de UsuarioModelView
        /// </summary>
        /// <param name="listModel"></param>
        /// <param name="ccDAO"></param>
        /// <returns></returns>
        public static IList<CadastroDeUsuario> GeraListaUsuario(IList<UsuarioModelView> listModel)
        {
            IList<CadastroDeUsuario> listaUsuario = new List<CadastroDeUsuario>();

            foreach (var x in listModel)
            {
                listaUsuario.Add(UsuarioFactory.GeraUsuario(x));
            }

            return listaUsuario;

        }


        public static UsuarioModelView GetModelView (CadastroDeUsuario model)
        {
            UsuarioModelView modelView = new UsuarioModelView()
            {
                Id = model.Id,                
                Cpf = model.Cpf,
                Email = model.Email,
                IsAdmin = model.IsAdmin,                
                Login = model.Login,
                Nome = model.Nome,
                Senha = model.Senha,
                Role =model.Role
            };
            if (model.CentroDeCusto != null)
            {
                modelView.CentroDeCusto = model.CentroDeCusto.Codigo;
            }
            

            return modelView;
        }

    }
}
