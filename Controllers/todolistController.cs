using Microsoft.AspNetCore.Mvc;
using todolist.Models;
using todolist.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

namespace todolist.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class todolistController : ControllerBase
    {
        private readonly todolistDBContext _context;

        public todolistController(todolistDBContext context){_context = context;}

        #region methods

        protected async Task SaveAsync() => await _context.SaveChangesAsync();
        protected async Task<Boolean> AddActivityAsync(Activity activity) //add an activity
        {
            if (activity is null)
                return false;
            await _context.Activities.AddAsync(activity);
            await SaveAsync();
            return true;
        }
        protected async Task<Boolean> SetActivityState(Int16 id, JsonPatchDocument<Activity> patch)
        {   
            Activity activity = await GetActivityAsync(id);

            patch.ApplyTo(activity);

            //await SaveAsync();

            activity.StatoString = activity.Stato.ToString();

            if (activity.Stato == ActivityState.Backlog || activity.Stato == ActivityState.ToDo)
            {
                activity.DataInizio = null;
                activity.DataFine = null;
            }
            else if (activity.Stato == ActivityState.InCorso)
            {
                activity.DataInizio = DateTime.Now;
                activity.DataFine = null;
            }    
            else if(activity.Stato == ActivityState.Fatto)
                activity.DataFine = DateTime.Now;


            _context.Activities.Update(activity);
            await SaveAsync();
            return true;
        }
        
        protected async Task<List<Activity>> GetActivitiesAsync() => await _context.Activities.ToListAsync(); //get all the activities
        protected async Task<List<Activity>> GetActivitiesOrderedByStatoAsync() => await _context.Activities.OrderBy(a => a.Stato).ToListAsync(); //get all the activities ordered by their states
        protected async Task<Activity> GetActivityAsync(Int16 Id) => await _context.Activities.FindAsync(Id); //get an activity by id
        
        protected async Task<List<Activity>> GetActivitiesByAssegnatarioAsync(string assegnatario)
        {
            List<Activity> toReturn = await GetActivitiesAsync();

            return toReturn.FindAll(a => a.Assegnatario.ToLower().Equals(assegnatario.ToLower()));
        }
        protected async Task<List<Activity>> GetActivitiesByStatoAsync(ActivityState stato) //get an activity by state(defined in the enum ActivityStates)
        {
            List<Activity> toReturn = await GetActivitiesAsync();
            
            return toReturn.FindAll(a => a.Stato == stato);
        }
        

        #endregion

        [HttpPost]
        [Route("activity")]
        public async Task<ActionResult> PostActivityAsync(Activity activity)
        {
            activity.Stato = ActivityState.Backlog;
            activity.StatoString = activity.Stato.ToString();
            activity.DataInizio = null;
            activity.DataFine = null;
            if(await AddActivityAsync(activity))
                return Ok();

            return BadRequest();
        }
        [HttpGet]
        [Route("activity")]
        public async Task<ActionResult<List<Activity>>> GetAllAsync()
        {
            return Ok(await GetActivitiesOrderedByStatoAsync());
        }
        [HttpGet]
        [Route("activity/{id}")]
        public async Task<ActionResult> GetIdAsync(Int16 id)
        {
            return Ok(await GetActivityAsync(id));
        }
        [HttpGet]
        [Route("activity/assegnatario/{assegnatario}")]
        public async Task<ActionResult> GetAssegnatarioAsync(string assegnatario)
        {
            return Ok(await GetActivitiesByAssegnatarioAsync(assegnatario));
        }
        [HttpPatch]
        [Route("activity/{id}")]
        public async Task<ActionResult> SetStato(Int16 id, JsonPatchDocument<Activity> patch)
        {
            if(await SetActivityState(id, patch))
                return Ok(); 
            
            return BadRequest();
            
        }

        [HttpGet]
        [Route("activity/backlog")]
        public async Task<ActionResult> GetBacklog()
        {
            return Ok(await GetActivitiesByStatoAsync(ActivityState.Backlog));
        }
        [HttpGet]
        [Route("activity/todo")]
        public async Task<ActionResult> GetToDo()
        {
            return Ok(await GetActivitiesByStatoAsync(ActivityState.ToDo));
        }
        [HttpGet]
        [Route("activity/incorso")]
        public async Task<ActionResult> GetInCorso()
        {
            return Ok(await GetActivitiesByStatoAsync(ActivityState.InCorso));
        }
        [HttpGet]
        [Route("activity/fatto")]
        public async Task<ActionResult> GetFatto()
        {
            return Ok(await GetActivitiesByStatoAsync(ActivityState.Fatto));
        }















        


    }
}