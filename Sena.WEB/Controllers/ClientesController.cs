﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sena.Business.Abstract;
using Sena.Business.Business;
using Sena.DAL;
using Sena.Models.Entities;
using Sena.WEB.Helpers;
using System;
using System.Threading.Tasks;

namespace Sena.WEB.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteBusiness _clienteBusiness;
        private readonly ITipoDocumentoBusiness _tipoDocumentoBusiness;
        public ClientesController(IClienteBusiness clienteBusiness, ITipoDocumentoBusiness tipoDocumentoBusiness)
        {
            _clienteBusiness = clienteBusiness;
            _tipoDocumentoBusiness = tipoDocumentoBusiness;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Titulo = "Gestión de clientes";
            return View(await _clienteBusiness.ObtenerClientes());
        }


        [NoDirectAccessAttribute]
        [HttpGet]
        public async Task<IActionResult> Crear() {
            ViewBag.Titulo = "Crear cliente";
            ViewBag.TiposDocumento = new SelectList(await _tipoDocumentoBusiness.ObtenerTiposDocumento(), "TipoDocumentoId", "Nombre");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            if (ModelState.IsValid) {
                try
                {
                    _clienteBusiness.Crear(cliente);
                    var guardar = await _clienteBusiness.GuardarCambios();
                    if (guardar) {
                        //TempData["Accion"] = "Guardar";
                        //TempData["Mensaje"] = $"Se creó con exito el cliente {cliente.Nombres}";
                        //return RedirectToAction("Index");
                        return Json(new { isValid = true, operacion = "crear" });

                    }
                }
                catch (Exception)
                {

                    return Json(new { isValid = false, tipoError = "error", error = "Error al crear el registro" });
                }
            }
            //TempData["Accion"] = "Validación";
            //TempData["Mensaje"] = "Debe llenar los campos requeridos";
            ViewBag.TiposDocumento = new SelectList(await _tipoDocumentoBusiness.ObtenerTiposDocumento(), "TipoDocumentoId", "Nombre");
            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Crear", cliente) });

        }


        [NoDirectAccessAttribute]
        [HttpGet]
        public async Task<IActionResult> Detalle(int ? id )
        {
            if (id != null) {
                try
                {
                    var cliente = await _clienteBusiness.ObtenerClientePorId(id);
                    if (cliente != null) { 
                    
                        return View(cliente);   
                    }
                    return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
                }
                catch (Exception)
                {

                    return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
                }
            
            }
            return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
        }


        [NoDirectAccessAttribute]
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id != null)
            {
                try
                {
                    var cliente = await _clienteBusiness.ObtenerClientePorId(id.Value);
                    if (cliente != null)
                    {
                        ViewBag.Titulo = "Editar cliente";
                        ViewBag.TiposDocumento = new SelectList(await _tipoDocumentoBusiness.ObtenerTiposDocumento(), "TipoDocumentoId", "Nombre");
                        return View(cliente);
                    }
                    else
                        return Json(new { isValid = false, tipoError = "error", mensaje = "No existe el cliente" });
                }
                catch (Exception)
                {
                    return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
                }

            }
            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> Editar(int? id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
                return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
            if (ModelState.IsValid)
            {
                try
                {
                    _clienteBusiness.Editar(cliente);
                    var editar = await _clienteBusiness.GuardarCambios();
                    if (editar)
                        return Json(new { isValid = true, operacion = "editar" });
                    else
                        return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
                }
                catch (Exception)
                {

                    return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
                }
            }
            // si el modelo tiene errores en las validaciones
            ViewBag.Titulo = "Editar cliente";
            ViewBag.TiposDocumento = new SelectList(await _tipoDocumentoBusiness.ObtenerTiposDocumento(), "TipoDocumentoId", "Nombre");
            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Editar", cliente) });
        }



        [NoDirectAccessAttribute]
        public async Task<IActionResult> CambiarEstado(int? id)
        {
            if (id != null)
            {
                try
                {
                    var cliente = await _clienteBusiness.ObtenerClientePorId(id);
                    if (cliente != null)
                    {
                        //cliente.Estado = !cliente.Estado;
                        if (cliente.Estado)
                            cliente.Estado = false;
                        else
                            cliente.Estado = true;
                        _clienteBusiness.Editar(cliente);

                        var guardar = await _clienteBusiness.GuardarCambios();
                        if (guardar)
                            return Json(new { isValid = true });

                    }
                    return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });

                }
                catch (Exception)
                {

                    return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
                }

            }
            return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
        }




    }
}
