using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AlimentosMvc.Models;
using AlimentosMvc.Controllers;

namespace AlimentosMvc.Controllers
{
    public class UsuariosController : Controller
    {
        public string UriBase = "torresssgio.somee.com/Usuarios/";


        [HttpGet]
        public ActionResult Index()
            {
                return View("CadastrarUsuario");
            }
   [HttpPost]
        public async Task<ActionResult> RegistrarAsync(UsuarioViewModel u)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient()) 
                {
                    string uriComplementar = "Registrar";

                    var content = new StringContent(JsonConvert.SerializeObject(u));
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(UriBase + uriComplementar, content);

                    string serialized = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["Mensagem"] = "Usuario registrado com sucesso!";
                        return View("AutenticarUsuario");
                    }
                    else
                    {
                        throw new Exception(serialized);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
            
    }
}