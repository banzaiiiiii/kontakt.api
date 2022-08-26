using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Kontakt.API.Data;
using Kontakt.API.Dtos;
using Kontakt.API.Models;
using System.Collections.Generic;

namespace Kontakt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepo _contactRepo;
        private readonly IMapper _mapper;
       

        public ContactsController(IContactRepo contactRepo, IMapper mapper)
        {
            _contactRepo = contactRepo;
            _mapper = mapper;
        }

       
        [HttpGet]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<IEnumerable<ContactReadDto>>> GetContactsAsync()
        {
            var contacts = await _contactRepo.GetAllContactsAsync();

            return Ok(_mapper.Map<IEnumerable<ContactReadDto>>(contacts));
        }

       
        [HttpGet("{id}", Name = "GetContactByIdAsync")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<ContactReadDto>> GetContactByIdAsync(int id)
        {
            var contact = await _contactRepo.GetContactByIdAsync(id);
            if (contact != null) {
                return Ok(_mapper.Map<ContactReadDto>(contact));
            }
            return NotFound();
        }

       
        [HttpPost]
        public async Task<ActionResult<ContactReadDto>> CreateContactAsync(ContactCreateDto contactCreateDto)
        {
            var contactModel = _mapper.Map<Contact>(contactCreateDto);

            await _contactRepo.CreateContactAsync(contactModel);

            var contactReadDto = _mapper.Map<ContactReadDto>(contactModel);
           
            return CreatedAtRoute(nameof(GetContactByIdAsync), new { Id = contactReadDto.Id }, contactReadDto);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateContactAsync(int id, ContactUpdateDto contactUpdateDto)
        {
            //check existence
            var contactModelFromRepo = await _contactRepo.GetContactByIdAsync(id);
            if (contactModelFromRepo == null) {
                return NotFound();
            }
            _mapper.Map(contactUpdateDto, contactModelFromRepo);
            //await _contactRepo.UpdateContactAsync(contactModelFromRepo);
            await _contactRepo.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialContactUpdateAsync(int id, JsonPatchDocument<ContactUpdateDto> patchDocument)
        {
            var contactModelFromRepo = await _contactRepo.GetContactByIdAsync(id);
            if (contactModelFromRepo == null)
            {
                return NotFound();
            }
            var contactToPatch = _mapper.Map<ContactUpdateDto>(contactModelFromRepo);
            patchDocument.ApplyTo(contactToPatch, ModelState);
            if (!TryValidateModel(contactToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(contactToPatch, contactModelFromRepo);
            //await _contactRepo.UpdateContactAsync(contactModelFromRepo);
            await _contactRepo.SaveChangesAsync();
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContactAsync(int id)
        {
            var contactModelFromRepo = await _contactRepo.GetContactByIdAsync(id);
            if (contactModelFromRepo == null)
            {
                return NotFound();
            }
            await _contactRepo.DeleteContactAsync(contactModelFromRepo);
            return NoContent(); 
        }
    }
}
