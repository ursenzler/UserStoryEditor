namespace UserStoryEditor.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UserStoryEditor.Core;

    [Route("api/[controller]")]
    [ApiController]
    public class UserStoryEditorController : ControllerBase
    {
        private readonly UserStoryEditor userStoryEditor;

        public UserStoryEditorController(
            UserStoryEditor userStoryEditor)
        {
            this.userStoryEditor = userStoryEditor;
        }

        [HttpGet("getestimation")]
        public ActionResult<int> GetEstimation()
        {
            return this.userStoryEditor
                .GetEstimation();
        }

        [HttpPost("adduserstory")]
        public object AddUserStory([FromBody] UserStory userStory)
        {
            this.userStoryEditor
                .AddUserStory(
                    userStory.Title);

            return this.Ok();
        }
    }
}