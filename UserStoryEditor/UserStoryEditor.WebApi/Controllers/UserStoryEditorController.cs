namespace UserStoryEditor.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UserStoryEditor.Core;
    using UserStoryEditor.Core.Operation;

    [Route("api/userstoryeditor")]
    [ApiController]
    public class UserStoryEditorController : ControllerBase
    {
        private readonly Backlog backlog;

        public UserStoryEditorController(
            Backlog backlog)
        {
            this.backlog = backlog;
        }

        [HttpGet("getestimation")]
        public ActionResult<int> GetEstimation()
        {
            return this.backlog
                .GetEstimation();
        }

        [HttpPost("adduserstory")]
        public object AddUserStory([FromBody] UserStory userStory)
        {
            
            return this.Ok();
        }
    }
}