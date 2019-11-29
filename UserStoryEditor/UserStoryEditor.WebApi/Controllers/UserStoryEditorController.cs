namespace UserStoryEditor.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UserStoryEditor.Core;

    [Route("api/userstoryeditor")]
    [ApiController]
    public class UserStoryEditorController : ControllerBase
    {
        private readonly ProductBacklog userStoryEditor;

        public UserStoryEditorController(
            ProductBacklog userStoryEditor)
        {
            this.userStoryEditor = userStoryEditor;
        }

        [HttpGet("getestimation")]
        public ActionResult<int> GetEstimation()
        {
            return this.userStoryEditor
                .GetEstimation()
                .Value;
        }

        [HttpPost("adduserstory")]
        public object AddUserStory([FromBody] UserStory userStory)
        {
            this.userStoryEditor
                .AddUserStory(
                    Guid.NewGuid(),
                    new Estimate(17));

            return this.Ok();
        }
    }
}