﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using DURAND.Models;
using DURAND.Services;
using Newtonsoft.Json;
using System.IO;

namespace DURAND.Controllers
{
    public class HomeController : Controller
    {

        //ACCIONES
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(Medico unMedico)
        {
            Medico MedicoTest;
            Especialidades currentEspecialidad;
            MedicoTest = MedicosService.ObtenerPorMailyContrasenia(unMedico.Mail, unMedico.Contrasenia);
            if (MedicoTest != null)
            {
                currentEspecialidad = EspecialidadesService.ObtenerPorId(MedicoTest.Especialidad);

                System.Web.HttpContext.Current.Session["sessionMedico"]         = MedicoTest;
                System.Web.HttpContext.Current.Session["SessionEspecialidad"]   = currentEspecialidad;
                return View("PantallaPrincipal");
            }
            else
            {
                return View();
            }
        }

        public ActionResult MedicoNoExiste(bool existe)
        {
            ViewBag.blnMedicoExiste = (existe) ? "true" : "false";
            return View("Login");
        }

        public ActionResult PantallaPrincipal()
        {
            return View();
        }

        //ESTADISTICAS
        public ActionResult Estadisticas()
        {
            IEnumerable<SelectListItem> estadisticas = EstadisticasService.ObtenerTodosDropDown().ToList();
            ViewBag.estadisticaList = estadisticas;

            return View();
        }

        [HttpPost]
        public string Estadisticas(string filtro = "3")
        {
            int intIdEstadistica = 0;
            if (filtro != null)
            {
                intIdEstadistica = Convert.ToInt32(filtro);
            }


            IEnumerable<SelectListItem> estadisticas = EstadisticasService.ObtenerTodosDropDown().ToList();
            ViewBag.estadisticaList = estadisticas;
            Object[] objetos;
            switch (intIdEstadistica)
            {
                case 1:
                    objetos =  EstadisticasService.VisitasXMes().ToArray();
                    break;
                case 3:
                     objetos =  EstadisticasService.PacientesXPatologias().ToArray();
                    break;
                case 6:
                     objetos =  EstadisticasService.PacientesXObraSocial().ToArray();
                    break;

                default:
                    objetos = null;
                    break;
            }

            string json = JsonConvert.SerializeObject(objetos);
            return json;
        }


        //MEDICO
        public ActionResult RegistrarMedico()
        {
            IEnumerable<SelectListItem> especialidades = EspecialidadesService.ObtenerTodosDropDown().ToList();
            ViewBag.especialidadList = especialidades;
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarMedicoOK(Medico unMedico)
        {
            string strEspecialidad  = Request["nombreEspecialidad"];
            unMedico.Especialidad   = Convert.ToInt32(strEspecialidad);
            int RegsAfec            = MedicosService.AgregarMedico(unMedico);

            if (RegsAfec > 0)
            {
                if (Request.Files[0].FileName != "")
                {
                    string strNombreArchivo = System.IO.Path.Combine(Server.MapPath("~/images"), RegsAfec.ToString() + ".jpg");
                    string strRutaRelativa = "/images/" + RegsAfec.ToString() + ".jpg";
                    Request.Files[0].SaveAs(strNombreArchivo);

                    Medico currentMedico;
                    currentMedico = MedicosService.ObtenerPorId(RegsAfec);
                    if (currentMedico != null)
                    {
                        currentMedico.Foto = strRutaRelativa;
                        int intRegsAffected = MedicosService.ModificarMedico(currentMedico);

                    }
                }
                // Se púdo registrar entonces verifico si vino una imagen.
                // La obtengo "Request.Files.Count", la almaceno en el disco con "/imagesn/id.jpg" y acutlaizo el registro del Medico (campo foto)
            }
            return View("Login");
        }

        public ActionResult ModificarMedico()
        {
            IEnumerable<SelectListItem> especialidades = EspecialidadesService.ObtenerTodosDropDown().ToList();
            ViewBag.especialidadList = especialidades;
            Medico unMedico = (Medico) System.Web.HttpContext.Current.Session["sessionMedico"];

            return View("ModificarMedico", unMedico);
        }

        [HttpPost]
        public ActionResult ModificarMedicoOk(Medico unMedico)
        {
            string strIDEspecialidad = Request["nombreEspecialidad"];
            unMedico.Especialidad = Convert.ToInt32(strIDEspecialidad);
            int RegsAfec = MedicosService.ModificarMedico(unMedico);

            return View("PantallaPrincipal");
        }

        //PACIENTE
        public ActionResult RegistrarPaciente()
        {
            IEnumerable<SelectListItem> sexos = SexosService.ObtenerTodosDropDown().ToList();
            IEnumerable<SelectListItem> obrasSociales = ObrasSocialesService.ObtenerTodosDropDown().ToList();
            IEnumerable<SelectListItem> provincias = ProvinciasService.ObtenerTodosDropDown().ToList();
            IEnumerable<SelectListItem> localidades = LocalidadesService.ObtenerTodosDropDown().ToList();
            IEnumerable<SelectListItem> patologias = PatologiasService.ObtenerTodosDropDown().ToList();
            
            ViewBag.sexoList = sexos;
            ViewBag.obraSocialList = obrasSociales;
            ViewBag.provinciaList = provincias;
            ViewBag.localidadList = localidades;
            ViewBag.patologiaList = patologias;

            return View();
        }

        [HttpPost]
        public ActionResult RegistrarPacienteOk(Paciente unPaciente)
        {           
            string strIDSexos       = Request["nombreSexo"];
            string strIDObraSocial  = Request["nombreObraSocial"];
            string strIDLocalidad   = Request["nombreLocalidad"];
            string strIDProvincia   = Request["nombreProvincia"];
            string strIDPatologia   = Request["nombrePatologia"];

            unPaciente.IDSexo       = Convert.ToInt32(strIDSexos);
            unPaciente.IDObraSocial = Convert.ToInt32(strIDObraSocial);
            unPaciente.IDLocalidad  = Convert.ToInt32(strIDLocalidad);
            unPaciente.IDProvincia  = Convert.ToInt32(strIDProvincia);
            unPaciente.IDPatologia  = Convert.ToInt32(strIDPatologia);
            unPaciente.IDArchivo    = 5;//ver

            int RegsAfec = PacientesService.AgregarPaciente(unPaciente);

            return View("PantallaPrincipal");
        }

        public ActionResult BuscarPaciente()
        {
            ViewBag.error = false;
            return View();
        }
        
        public ActionResult PacienteNoExiste(bool existe)
        {
            ViewBag.blnPacienteExiste = (existe) ? "true" : "false";
            return View("BuscarPaciente");
        }
        
        //HISTORIA CLINICAS
        [HttpPost]
        public ActionResult HistoriaClinica(Paciente modelo)
        {
            Paciente unPaciente;
            List<Visita> visitasList;
            Patologia unaPatologia;
            ObraSocial unaObraSocial;

            unPaciente = PacientesService.ObtenerPorDNI(modelo.DNI);
            if (unPaciente != null)
            {
                visitasList     = VisitasService.ObtenerPorIdPaciente(unPaciente.Id);
                unaObraSocial   = ObrasSocialesService.ObtenerPorId(unPaciente.IDObraSocial);
                unaPatologia    = PatologiasService.ObtenerPorId(unPaciente.IDPatologia);

                ViewBag.NombreObraSocial    = unaObraSocial.Nombre;
                ViewBag.NombrePatologia     = unaPatologia.Nombre;
                ViewBag.Paciente            = unPaciente;
                ViewBag.VisitasList         = visitasList;

                return View();
            }
            else
            {
                return RedirectToAction("PacienteNoExiste", new { existe = false });
            }

        }

        //VISITAS
        [HttpGet]
        public ActionResult AgregarVisita(int id)
        {
            Visita          modelo = new Visita();
            List<Medico>    medicoList;
            Paciente        unPaciente;
            Patologia unaPatologia;
            ObraSocial unaObraSocial;

            unPaciente = PacientesService.ObtenerPorId(id);
            medicoList = MedicosService.ObtenerTodos();
            unaObraSocial = ObrasSocialesService.ObtenerPorId(unPaciente.IDObraSocial);
            unaPatologia = PatologiasService.ObtenerPorId(unPaciente.IDPatologia);

            IEnumerable<SelectListItem> items = medicoList.Select(c => new SelectListItem
                    {
                        Value = c.IdMedico.ToString(),
                        Text = c.Nombre
                    });

            ViewBag.NombreObraSocial = unaObraSocial.Nombre;
            ViewBag.NombrePatologia = unaPatologia.Nombre;
            ViewBag.Paciente = unPaciente;
            ViewBag.medicoList = items;
            
            return View(modelo);
        }

        [HttpPost]
        public ActionResult AgregarVisitaOk(Visita unaVisita)
        {
            int RegAfectados;
            Paciente elPaciente = new Paciente();
            List<Visita> visitasList = new List<Visita>();

            string strIdPaciente = Request["IDPacientex"];
            string strIDMedico = Request["nombreMedico"];
            unaVisita.IDPaciente = Convert.ToInt32(strIdPaciente);
            unaVisita.IDMedico = Convert.ToInt32(strIDMedico);

            if (unaVisita != null)
            {
                elPaciente          = PacientesService.ObtenerPorId(unaVisita.IDPaciente);
                RegAfectados        = VisitasService.AgregarVisita(unaVisita);
                visitasList         = VisitasService.ObtenerPorIdPaciente(elPaciente.Id);
            }

            ViewBag.Paciente = elPaciente;
            
            ViewBag.VisitasList = visitasList;

            return View("HistoriaClinica", elPaciente);
        }


        //DROGAS
        [HttpGet]
        public ActionResult AgregarMedicamento(int id)
        {
            Paciente unPaciente;

            unPaciente = PacientesService.ObtenerPorId(id);

            //Para el dropdown 
            IEnumerable<SelectListItem> droga = DrogasService.ObtenerTodosDropDown().ToList();
            ViewBag.drogaList = droga;
            ViewBag.Paciente = unPaciente;
            
            //Para el foreach 
            List<DrogaXPaciente>lista = DrogaXPacienteService.ObtenerPorId(id);
            ViewBag.drogaXPacienteList = lista;
             
            return View();
        }

        [HttpPost]
        public ActionResult AgregarMedicamentoOk(DrogaXPaciente unaDroga)
        {
            Paciente unPaciente;
            int RegAfectados;

            string strIdPAciente    = Request["IDPacientex"];
            string Altura           = Request["Altura"];
            string Peso             = Request["Peso"];
            string Nombre           = Request["Nombre"];


            unaDroga.IdPaciente     = Convert.ToInt32(strIdPAciente);
            unaDroga.Altura         = float.Parse(Altura);
            unaDroga.Peso           = float.Parse(Peso);
            unaDroga.IdDroga        = Convert.ToInt32(Nombre);

            if (unaDroga != null)
            {
                RegAfectados = DrogaXPacienteService.AgregarDroga(unaDroga);
            }

            unPaciente = PacientesService.ObtenerPorId(unaDroga.IdPaciente);
            
            //Dropdown
            IEnumerable<SelectListItem> droga = DrogasService.ObtenerTodosDropDown().ToList();
            ViewBag.drogaList = droga;
            ViewBag.Paciente = unPaciente;
            
            //Para el foreach
            List<DrogaXPaciente> lista = DrogaXPacienteService.ObtenerPorId(unaDroga.IdPaciente);
            ViewBag.drogaXPacienteList = lista;

            return View("AgregarMedicamento", unaDroga);
        }

    }
}