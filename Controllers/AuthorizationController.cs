using Microsoft.AspNetCore.Mvc;
using Payments.Entities.DTO;
using Payments.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Payments.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar las solicitudes de autorización de pago.
    /// </summary>
    [ApiController]
    [Route("payment")]
    public class AuthorizationController : ControllerBase
    {
        private AuthorizationService authorizationService;

        /// <summary>
        /// Constructor del controlador que inyecta la dependencia del servicio de autorización.
        /// </summary>
        public AuthorizationController(AuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        /// <summary>
        /// Endpoint para solicitar una autorización de pago.
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
        /// Endpoint para confirmar una solicitud de autorización.
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
