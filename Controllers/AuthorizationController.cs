using Microsoft.AspNetCore.Mvc;
using Payments.Entities.DTO;
using Payments.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Payments.Controllers
{
    /// <summary>
    /// Controller in charge of handling payment authorisation requests.
    /// </summary>
    [ApiController]
    [Route("payment")]
    public class AuthorizationController : ControllerBase
    {
        private AuthorizationService authorizationService;

        /// <summary>
        /// Controller constructor injecting the dependency of the authorisation service.
        /// </summary>
        public AuthorizationController(AuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        /// <summary>
        /// Endpoint to request a payment authorisation.
        /// </summary>
        [SwaggerOperation(
            Summary = "Requests an authorization of a payment",
            Description = "This method returns a AuthorizationRequest object"
        )]
        [HttpPost("request-authorization")]
        public IActionResult RequestAuthorization([FromBody] AuthorizationRequestDTO requestDTO)
        {
            return Ok(authorizationService.AuthorizePayment(requestDTO).Result);
        }

        /// <summary>
        /// Endpoint to confirm an authorisation request.
        /// </summary>
        [SwaggerOperation(
            Summary = "Confirms the payment of a double factor authorization request",
            Description = "This method returns a AuthorizationRequest object"
        )]
        [HttpPost("confirm-request")]
        public IActionResult ConfirmRequest([FromBody] ConfirmationDTO confirmationDTO)
        {
            return Ok(authorizationService.ConfirmAuthorization(confirmationDTO).Result);
        }
    }
}
