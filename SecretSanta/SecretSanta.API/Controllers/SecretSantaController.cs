using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.API.Data;
using SecretSanta.API.Helper;
using SecretSanta.API.Models;
using SecretSanta.API.Services;

namespace SecretSanta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretSantaController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly SecretSantaContext _secretSantaContext;

        public SecretSantaController(IEmailService emailService, SecretSantaContext secretSantaContext)
        {
            this._emailService = emailService;
            this._secretSantaContext = secretSantaContext;
        }

        [HttpPost("sendSecretSantaEmail")]
        public async Task<IActionResult> SendSecretSantaEmail()
        {
            try
            {
                var assignedBuddies = new List<int>();

                var secretSantaEvent = _secretSantaContext.Events.FirstOrDefault();

                var participants = secretSantaEvent.EventParticipants.ToList();

                var assignedBuddyList = new List<AssignedBuddy>();

                foreach (var participant in participants)
                {
                    var emailMessage = new EmailMessage();

                    emailMessage.EmailBody = await _emailService.GetEmailTemplateContent();

                    var availableBuddies = participants
                        .Where(x => x.Id != participant.Id && !assignedBuddies.Contains(x.Id))
                        .ToList();

                    var random = new Random();

                    var selectedBuddyIndex = random.Next(availableBuddies.Count);

                    var selectedBuddy = availableBuddies[selectedBuddyIndex];

                    assignedBuddies.Add(selectedBuddy.Id);

                    assignedBuddyList
                        .Add(new AssignedBuddy()
                        { ParticipantId = participant.Id, AssignedParticipantId = selectedBuddy.Id });

                    emailMessage.ToEmails.Add(participant.Email);

                    emailMessage.EmailBody = emailMessage.EmailBody.Replace("@SantaName", participant.NickName);
                    emailMessage.EmailBody = emailMessage.EmailBody.Replace("@Secretsant", selectedBuddy.Name);

                    emailMessage.Subject = secretSantaEvent.EmailSubject;
                    emailMessage.IsHtmlEnable = true;
                    await _emailService.SendEmail(emailMessage);

                }



            

                _secretSantaContext.AssignedBuddies.AddRange(assignedBuddyList);

                await _secretSantaContext.SaveChangesAsync();




                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
