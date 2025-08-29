using Microsoft.AspNetCore.Mvc;
using TESTE.Data;
using TESTE.Data.Entities;
using TESTE.Services;

namespace TESTE.Controller;
[ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityService _activityService;

        public ActivityController(ActivityService activityService)
        {
            _activityService = activityService;
        }

        // GET: api/activity
        [HttpGet]
        public IActionResult GetAll()
        {
            var activities = _activityService.GetAllActivities();
            return Ok(activities);
        }

        // GET: api/activity/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var activity = _activityService.GetById(id);
            if (activity == null)
                return NotFound(new { mensagem = "Atividade não encontrada" });

            return Ok(activity);
        }

        // GET: api/activity/user/{userid}
        [HttpGet("user/{userid}")]
        public IActionResult GetByUserId(string UserId)
        {
            var activities = _activityService.GetByUserid(UserId);

            if (activities == null || activities.Count == 0)
                return NotFound(new { mensagem = "Nenhuma atividade encontrada para este cliente" });

            return Ok(activities);
        }

        // POST: api/activity
        [HttpPost]
        public IActionResult Create([FromBody] Activity activity)
        {
            if (activity == null || string.IsNullOrEmpty(activity.UserId))
                return BadRequest("Activity ou Id do Usuario inválido.");

            _activityService.AddActivity(activity);
            return CreatedAtAction(nameof(GetById), new { id = activity.Id }, activity);
        }

        // PUT: api/activity/{id}
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Activity activity)
        {
            var existingActivity = _activityService.GetById(id);
            if (existingActivity == null)
                return NotFound(new { mensagem = "Atividade não encontrada" });

            _activityService.UpdateActivity(id, activity);
            return NoContent();
        }
    }