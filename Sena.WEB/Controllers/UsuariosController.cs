﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sena.Business.Abstract;
using Sena.Business.DTOs.usuarios;
using Sena.Models.Entities;
using Sena.WEB.Helpers;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Sena.WEB.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioBusiness _usuarioBusiness;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuariosController(IUsuarioBusiness usuarioBusiness, SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _usuarioBusiness = usuarioBusiness;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Titulo = "Gestión de Usuarios";

            return View(await _usuarioBusiness.ObtenerListaUsuarios());
        }
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(RegistrarUsuarioDto registrarUsuario)
        {

            if (ModelState.IsValid)
            {
                //comprobar su existe el usuario con ese correo
                var email = await _usuarioBusiness.ObtenerUsuarioDtoPorEmail(registrarUsuario.Email);
                if (email == null)
                {
                    try
                    {
                        var usuarioId = await _usuarioBusiness.Crear(registrarUsuario);

                        if (usuarioId == null)
                            return Json(new { isValid = false, tipoError = "danger", error = "Error al crear el usuario" });

                        if (usuarioId.Equals("ErrorPassword"))
                            return Json(new { isValid = false, tipoError = "danger", error = "Error de password" });

                        return Json(new { isValid = true, operacion = "crear" });
                    }
                    catch (Exception)
                    {

                        return Json(new { isValid = false, tipoError = "danger", error = "Error al crear el usuario" });
                    }
                }
            }
            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Crear", registrarUsuario) });



        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RecordarMe, false);
                if (resultado.Succeeded)
                    return RedirectToAction("Dashboard", "Admin");


            }


            return View();
        }
        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Usuarios");
        }

        public IActionResult OlvidePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OlvidePassword(RecuperarPasswordDto recuperarPasswordDto)
        {
            if (ModelState.IsValid)
            {
                //buscamos si el email existe
                var usuario = await _userManager.FindByEmailAsync(recuperarPasswordDto.Email);
                if (usuario != null)
                {
                    //generamos un token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    //creamos un link para resetear el password
                    var passwordresetLink = Url.Action("ResetearPassword", "Usuarios",
                        new { email = recuperarPasswordDto.Email, token = token }, Request.Scheme);

                    //Metodo tradicional de enviar correos por smtp
                    MailMessage mensaje = new();
                    mensaje.To.Add(recuperarPasswordDto.Email); //destinatario
                    mensaje.Subject = "SENA recuperar password";
                    mensaje.Body = passwordresetLink;
                    mensaje.IsBodyHtml = false;
                    //mensaje.From = new MailAddress("pruebas@xofsystems.com","Notificaciones");
                    mensaje.From = new MailAddress("pruebas@xofsystems.com", "Notificaciones");
                    SmtpClient smtpClient = new("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential("pruebas@xofsystems.com", "Tempo123!");
                    smtpClient.Send(mensaje);

                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult ResetearPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Error token");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetearPassword(ResetearPassword resetearPassword)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(resetearPassword.Email);

                if (usuario != null)
                {
                    //resetear el password
                    var resultado = await _userManager.ResetPasswordAsync(usuario, resetearPassword.Token, resetearPassword.Password);
                    if (resultado.Succeeded)
                    {
                        return RedirectToAction("Login", "Usuarios");
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> Editar(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
                return Json(new { isValid = false, error = "No se encuentra el registro" });

            ViewBag.ListaRoles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            var roles = await _userManager.GetRolesAsync(usuario);
            UsuarioAsignarRol usuarioAsignarRol = new()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Estado = usuario.Estado,
                Rol = roles.FirstOrDefault()
            };

            return View(usuarioAsignarRol);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(string id, UsuarioAsignarRol usuarioAsignarRol)
        {
            if (id != usuarioAsignarRol.Id)
                return Json(new { isValid = false, tipoError = "danger", error = "Error al actualizar el registro" });
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = await _userManager.FindByIdAsync(usuarioAsignarRol.Id);
                    var resultado = await _userManager.AddToRoleAsync(usuario, usuarioAsignarRol.Rol);
                    if (resultado.Succeeded)
                        return Json(new { isValid = true, operacion = "editar" });
                    else
                        return Json(new { isValid = false, tipoError = "danger", error = "Error al actualizar el registro" });


                }
                catch (Exception)
                {

                    return Json(new { isValid = false, tipoError = "danger", error = "Error al actualizar el registro" });
                }
            }
            ViewBag.ListaRoles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            return Json(new { isValid = false, tipoError = "danger", error = "Error al actualizar el registro" });
        }

    }
}
