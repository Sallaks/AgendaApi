﻿using System.Security.Claims;
using AgendaApi.Data.Repository.Interfaces;
using AgendaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        
        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_contactRepository.GetById(id));
            }
            catch
            {
                return NotFound("Contacto no existente");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("all")]
        public IActionResult GetAllByUser()
        {
            var currentUserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var contacts = _contactRepository.FindAllByUser(currentUserId);
            return Ok(contacts);
        }


        [HttpPost]
        public IActionResult CreateContact(CreateAndUpdateContact createContactDto)
        {
            try
            {
                _contactRepository.Create(createContactDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("Created", createContactDto);
        }

        [HttpPut]
        public IActionResult UpdateContact(CreateAndUpdateContact dto)
        {
            try
            {
                _contactRepository.Update(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteContactById (int id)
        {
            try
            {
                _contactRepository.Delete(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }




    }
}
