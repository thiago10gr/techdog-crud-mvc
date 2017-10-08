using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Projeto.Armazenamento.Contratos;
using Projeto.Util.Contratos;
using Projeto.Web.Models;
using Projeto.Entidades;
using System.Web.Security;
using Projeto.Entidades.Tipos;
using Projeto.Web.Excecoes;

namespace Projeto.Web.Controllers
{
    public class HomeController : Controller
    {


        private readonly IUsuarioRepository usuarioRepository;
        private readonly ICriptografiaUtil criptografia;



        public HomeController(IUsuarioRepository usuarioRepository, ICriptografiaUtil criptografia)
        {
            this.usuarioRepository = usuarioRepository;
            this.criptografia = criptografia;
        }


        [HttpPost]
        public ActionResult Login(HomeViewModelLogin model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    Usuario u = usuarioRepository.ObterPorEmailSenha(model.EmailAcesso.ToUpper(), criptografia.EncriptarSenha(model.SenhaAcesso));

                    if (u != null && u.Ativo.Equals(SimNao.Sim))
                    {

                        //ticket de acesso do usuario 
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(u.IdUsuario.ToString(), false, 5);

                        //criando um cookie para gravar o tiket do usuario
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));

                        Response.Cookies.Add(cookie);

                        Session.Add("usuario", u);

                        return RedirectToAction("Index", "Usuario", new { area = "AreaRestrita" });

                    } else
                    {
                        ViewBag.MsgErro = "Acesso Negado. Tente novamente.";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.MsgErro = e.Message;

                } finally
                {
                    usuarioRepository.Dispose();
                }
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(HomeViewModelCadastro model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    usuarioRepository.BeginTransaction();

                    Usuario u = new Usuario()
                    {
                        Nome = model.Nome,
                        Email = model.Email.ToUpper(),
                        Senha = criptografia.EncriptarSenha(model.Senha),
                        DataCadastro = DateTime.Now,
                        Ativo = SimNao.Sim
                    };


                    if (usuarioRepository.EmailExistente(u.Email))
                    {
                        throw new CustomException("Já existe um cadastro com este email.");
                    }

                    usuarioRepository.Inserir(u);

                    usuarioRepository.Commit();

                    ViewBag.MsgSucesso = "Cadastro realizado com sucesso.";

                    ModelState.Clear();

                }
                catch (Exception e)
                {
                    usuarioRepository.Rollback();
                    ViewBag.MsgErro = e.Message;

                } finally
                {
                    usuarioRepository.Dispose();
                }
            }

            return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session.Remove("usuario");

            return RedirectToAction("Login", "Home", new { area = "" });

        }
    }
}