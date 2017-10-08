using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Projeto.Armazenamento.Contratos;
using Projeto.Web.Areas.AreaRestrita.Models;
using Projeto.Entidades;
using Projeto.Entidades.Tipos;
using Projeto.Web.Excecoes;
using Projeto.Util.Contratos;

namespace Projeto.Web.Areas.AreaRestrita.Controllers
{
    public class UsuarioController : Controller
    {


        private readonly IUsuarioRepository usuarioRepository;
        private readonly ICriptografiaUtil criptografia;


        public UsuarioController(IUsuarioRepository usuarioRepository, ICriptografiaUtil criptografia)
        {
            this.usuarioRepository = usuarioRepository;
            this.criptografia = criptografia;
        }


        // GET: AreaRestrita/Usuario
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost, Authorize]
        public ActionResult Cadastro(UsuarioViewModelCadastro model)
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


                    if(usuarioRepository.EmailExistente(u.Email))
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
                }
                finally
                {
                    usuarioRepository.Dispose();
                }
            }

            return View();
        }




        public UsuarioViewModelEdicao MontarUsuario(int id)
        {
            UsuarioViewModelEdicao model = new UsuarioViewModelEdicao();

            try
            {
                Usuario u = usuarioRepository.ObterPorId(id);

                model.IdUsuario = u.IdUsuario;
                model.Nome = u.Nome;
                model.Email = u.Email;
                model.DataCadastro = u.DataCadastro;
                model.Ativo = u.Ativo;

            } catch (Exception e)
            {

            }
            finally
            {
                usuarioRepository.Dispose();
            }
            
            return model;
        }




        [Authorize]
        public ActionResult Edicao(int id)
        {
            return View(MontarUsuario(id));
        }


        [HttpPost, Authorize]
        public ActionResult Edicao(UsuarioViewModelEdicao model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    usuarioRepository.BeginTransaction();

                    Usuario u = usuarioRepository.ObterPorId(model.IdUsuario);

                    u.Nome = model.Nome;
                    u.Email = model.Email.ToUpper();
                    u.Senha = criptografia.EncriptarSenha(model.Senha);
                    u.Ativo = model.Ativo;


                    if(usuarioRepository.EmailExistente(u.Email, u.IdUsuario))
                    {
                        throw new CustomException("Já existe um cadastro com este email.");
                    }

                    usuarioRepository.Atualizar(u);

                    usuarioRepository.Commit();

                    ModelState.Clear();

                    ViewBag.MsgSucesso = "Cadastro atualizado com sucesso.";

                }
                catch (Exception e)
                {
                    usuarioRepository.Rollback();
                    ViewBag.MsgErro = e.Message;
                }
            }

            return View(MontarUsuario(model.IdUsuario));
        }


        [Authorize]
        public ActionResult Exclusao(int id)
        {
            Usuario u = usuarioRepository.ObterPorId(id);

            UsuarioViewModelExclusao model = new UsuarioViewModelExclusao()
            {
                IdUsuario = u.IdUsuario,
                Nome = u.Nome,
                Email = u.Email,
                DataCadastro = u.DataCadastro,
                Ativo = u.Ativo
            };

            return View(model);
        }


        [HttpPost, Authorize]
        public ActionResult Exclusao(UsuarioViewModelExclusao model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    usuarioRepository.BeginTransaction();

                    Usuario u = usuarioRepository.ObterPorId(model.IdUsuario);

                    usuarioRepository.Excluir(u);

                    usuarioRepository.Commit();

                    TempData["MsgSucesso"] = "Cadastro excluido com sucesso.";

                }
                catch (Exception e)
                {
                    usuarioRepository.Rollback();
                    TempData["MsgErro"] = e.Message;
                }
                finally
                {
                    usuarioRepository.Dispose();
                }
            }

            return RedirectToAction("Consulta");
        }

        [Authorize]
        public ActionResult Consulta()
        {
            List<UsuarioViewModelConsulta> lista = new List<UsuarioViewModelConsulta>();

            foreach (Usuario u in usuarioRepository.ListarTodos())
            {
                lista.Add(new UsuarioViewModelConsulta()
                {
                    IdUsuario = u.IdUsuario,
                    Nome = u.Nome,
                    Ativo = u.Ativo,
                    DataCadastro = u.DataCadastro,
                    Email = u.Email,
                    Senha = u.Senha
                });
            }

            return View(lista);
        }
    }
}
