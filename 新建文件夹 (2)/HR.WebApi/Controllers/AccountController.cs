using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using HR.WebApi.Data.Entities;
using HR.WebApi.Repositorys;

namespace HR.WebApi.Controllers
{
    [RoutePrefix("api/Account")]
    [Authorize(Users = "lir")]
    [WebApiTracker]
    public class AccountController : ApiController
    {

        private readonly AuthRepository _authRepository = null;

        public AccountController()
        {
            _authRepository = new AuthRepository();
        }

        

        // POST api/Account/Register
        [NonAction]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _authRepository.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }
            return Ok();
        }

        [NonAction]
        public  async Task<bool> LocalRegister(UserModel userModel)
        {
            IdentityResult result = await _authRepository.RegisterUser(userModel);
            IHttpActionResult errorResult = GetErrorResult(result);
            if (errorResult != null)
            {
                return false;
            }
            return true;
        }

        [NonAction]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _authRepository.Dispose();
            }

            base.Dispose(disposing);
        }
        [NonAction]
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}