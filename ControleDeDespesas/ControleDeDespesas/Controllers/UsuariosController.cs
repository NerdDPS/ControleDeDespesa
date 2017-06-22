﻿using ControleDeDespesas.DAO;
using ControleDeDespesas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace ControleDeDespesas.Controllers
{
    public class UsuariosController : Controller
    {
        private UsuariosDAO usuarioDAO;

        public UsuariosController (UsuariosDAO user)
        {
            this.usuarioDAO = user;

            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("local", "CadastroDeUsuario", "Id", "Login", true);
            }
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(CadastroDeUsuario usuario)
        {
            if (ModelState.IsValid)
            {
                //usuarioDAO.Adiciona(usuario);
                try
                {
                    WebSecurity.CreateUserAndAccount(usuario.Login, usuario.Senha, new {                                                         
                                                                                         Nome = usuario.Nome                                                       
                                                                                        ,Email= usuario.Email
                                                                                        ,IsAdmin = usuario.IsAdmin
                                                                                        ,Cpf = usuario.Cpf
                                                                                        ,CentroDeCusto = usuario.CentroDeCusto
                                                                                        }
                                                        , false);
                    
                }catch(MembershipCreateUserException ex)
                {
                    return View("Erro");
                }
            }
            return View();

        }
    }
}