namespace UserStoryEditor.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UserStoryEditor.Core;
    using UserStoryEditor.Core.Blocks.Estimations;
    using UserStoryEditor.Core.Operation;

    [Route("api/userstoryeditor")]
    [ApiController]
    public class UserStoryEditorController : ControllerBase
    {
        private readonly IRootFactory factory;

        public UserStoryEditorController(
            IRootFactory factory)
        {
            this.factory = factory;
        }

        [HttpGet("getestimation")]
        public ActionResult<int> GetEstimation()
        {
            return this.factory
                .CreateBacklogOperations()
                .GetEstimation(Strategy.Sum);
        }

        [HttpPost("adduserstory")]
        public object AddUserStory([FromBody] UserStory userStory)
        {
            this.factory
                .CreateBacklogOperations()
                .AddUserStory(
                    Guid.NewGuid(),
                    userStory.Title,
                    7);

            return this.Ok();
        }
    }
}