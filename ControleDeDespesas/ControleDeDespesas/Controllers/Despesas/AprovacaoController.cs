﻿using Modelos.ViewModels;
using BuildMenu;
using Interfaces;
using Modelos;
using Persistencia.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using ControleDeDespesas.Controllers.Filters;

namespace ControleDeDespesas.Controllers
{
    [AutorizacaoFilter]
    public class AprovacaoController : Controller, ISetMenu
    {
        private DespesasDAO despesasDAO;
        private UsuariosDAO usuarioDAO;
        private CentroDeCustoDAO ccDAO;

        public AprovacaoController(DespesasDAO dep, UsuariosDAO user, CentroDeCustoDAO cc)
        {
            this.despesasDAO = dep;
            this.usuarioDAO = user;
            this.ccDAO = cc;
            BuildMenu();
        }

        public void BuildMenu()
        {
            MakeMenu.Add("Despesas", "Index", "Aprovacao", "Home", Role.User);
        }

        // GET: Aprovacao
        public ActionResult Index()
        {

            var model = new AprovacaoModelView();

            //Recupera a session para o cadasrtro de usuário
            CadastroDeUsuario usuario = (CadastroDeUsuario)Session["Usuario"];

            //Retorna todos os Centros de Custos de aprovação do usuário                        
            IList<CentroDeCusto> CCAutorizados = ccDAO.GetByAprovador(usuario);

            

            //Recupera a Lista das Despesas pendentes pra aprovação 
            ViewBag.UnApprovedRDV = despesasDAO.GetDespesasUnApproved(CCAutorizados);           

            return View(model);
        }

        /// <summary>
        /// Realiza a aprovação de uma despesa 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Aprovar(int id) //<-- Aqui nós poderiamos solicitar o motivo pelo AprovacaoModelView Motivo
        {
            //Recupera a session para o cadasrtro de usuário
            CadastroDeUsuario usuario = (CadastroDeUsuario)Session["Usuario"];

            //Aprova o item da Despesa
            despesasDAO.AprovarDespesa(id, usuario);
            
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AprovarAll(int id)
        {

            //Recupera a session para o cadasrtro de usuário
            CadastroDeUsuario usuario = (CadastroDeUsuario)Session["Usuario"];

            //Pega todas as Depesas 
            var despesas = despesasDAO.GetDespesas(id);

            //Aprova em massa as Despesas
            despesasDAO.AprovaDespesa(despesas, usuario);

            //Aprova as Despesas em massa

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Realiza a Reprovação de uma despesa 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Reprovar(int id, FormCollection form)
        {
            //Motivo da reprovação da Despesa
            string motivo = form["Motivo"];

            //Recupera a session para o cadasrtro de usuário
            CadastroDeUsuario usuario = (CadastroDeUsuario)Session["Usuario"];


            despesasDAO.ReprovarDespesa(id, usuario, motivo);      
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ReprovarAll(int id, FormCollection form)
        {
            //Motivo da reprovação da Despesa
            string motivo = form["Motivo"];


            //Recupera a session para o cadasrtro de usuário
            CadastroDeUsuario usuario = (CadastroDeUsuario)Session["Usuario"];

            //Paga todas as despesas desse Id
            IList<Despesas> despesas = despesasDAO.GetDespesas(id);


            //despesasDAO.ReprovarDespesa(usuario,motivo, despesas);
            despesasDAO.ReprovarDespesa(despesas,usuario, motivo);
            
            return RedirectToAction("Index");
        }


    }
}