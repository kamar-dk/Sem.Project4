using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsApi.Data;
using ModelsApi.Models.DTOs;
using ModelsApi.Models.DTOs.ModelsApi.Models.DTOs;
using ModelsApi.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModelsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public JobsController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// For manegers all jobs with models but without expenses are returned.
        /// For models only their own jobs are returned.
        /// </summary>
        /// <returns>A list of jobs</returns>
        // GET: api/Jobs
        [ProducesResponseType(200, Type = typeof(List<EfJob>))]
        [ProducesResponseType(401)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            var role = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
            
            if (role == "Manager")
            {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                var jobs = await _context.Jobs
                //    .Select(job => new Job
                //    {
                //        JobId = job.EfJobId,
                //        Comments = job.Comments,
                //        StartDate = job.StartDate,
                //        Days = job.Days,
                //        Location = job.Location,
                //        Models = job.JobModels .Models.Select(model => new
                //        {
                //            FirstName = model.FirstName,
                //            LastName = model.LastName
                //        }).ToList()
                //    })
                //.ToListAsync();
                    .Include(j => j.JobModels)
                    .ThenInclude(jm => jm.Model)
                    .ToListAsync().ConfigureAwait(false);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                List<Job> jobDtoList = ConvertJobs(jobs);
                return jobDtoList;
            }
            else // Role == Model
            {
                var modelStr = User.Claims.First(a => a.Type == "ModelId").Value;
                long modelId;
                if (!long.TryParse(modelStr, out modelId))
                    return Unauthorized("ModelId missing");
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                var jobs = await _context.JobModels
                    .Where(jm => jm.EfModelId == modelId)
                    .Include(r => r.Job)
                    .Select(s => s.Job)
                    .ToListAsync().ConfigureAwait(false);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
                var jobDtoList = new List<Job>();
                foreach (var job in jobs)
                {
                    var jobDto = _mapper.Map<Job>(job);
                    jobDto.JobId = job.EfJobId;
                    if (job.JobModels != null)
                        foreach (var jobModel in job.JobModels)
                        {
                            var modelDto = _mapper.Map<Model>(jobModel.Model);
                            jobDto.Models.Add(modelDto);
                        }
                    jobDtoList.Add(jobDto);
                }
                return jobDtoList;
            }

            List<Job> ConvertJobs(List<EfJob> jobs)
            {
                var jobDtoList = new List<Job>();
                foreach (var job in jobs)
                {
                    var jobDto = _mapper.Map<Job>(job);
                    jobDto.JobId = job.EfJobId;
                    if (job.JobModels != null)
                        foreach (var jobModel in job.JobModels)
                        {
                            var modelDto = _mapper.Map<Model>(jobModel.Model);
                            jobDto.Models.Add(modelDto);
                        }
                    jobDtoList.Add(jobDto);
                }

                return jobDtoList;
            }
        }

        /// <summary>
        /// To get a job with assigned models
        /// </summary>
        /// <param name="id">JobId</param>
        /// <returns>The job</returns>
        // GET: api/Jobs/5
        [ProducesResponseType(204, Type = typeof(Job))]
        [ProducesResponseType(401)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(long id)
        {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var job = await _context.Jobs.Where(j => j.EfJobId == id)
                .Include(j => j.JobModels)
                .ThenInclude(jm => jm.Model)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            if (job == null)
            {
                return NotFound();
            }

            var jobDto = _mapper.Map<Job>(job);
            jobDto.JobId = job.EfJobId;

            if (job.JobModels != null)
                foreach (var jobModel in job.JobModels)
                {
                    var modelDto = _mapper.Map<Model>(jobModel.Model);
                    jobDto.Models.Add(modelDto);
                }

            return jobDto;
        }

        /// <summary>
        /// To update a jobs properties.
        /// </summary>
        /// <param name="jobId">The job to update</param>
        /// <param name="newJob">The updated job</param>
        /// <returns>204 if succesfull</returns>
        // PUT: api/Jobs/5
        [HttpPut("{jobId}")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> PutJob(long jobId, NewJob newJob)
        {
            try
            {
                var job = await _context.Jobs.FindAsync(jobId).ConfigureAwait(false);
                if (job == null)
                {
                    ModelState.AddModelError("jobId", "jobId not found");
                    return BadRequest(ModelState);
                }
                job.Comments = newJob.Comments;
                job.Customer = newJob.Customer;
                job.Days = newJob.Days;
                job.Location = newJob.Location;
                job.StartDate = newJob.StartDate;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(jobId))
                {
                    ModelState.AddModelError("jobId", "jobId not found");
                    return BadRequest(ModelState);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Jobs
        /// <summary>
        /// Create a new job
        /// </summary>
        /// <param name="newJob"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Job>> PostJob(NewJob newJob)
        {
            var job = _mapper.Map<EfJob>(newJob);
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            var jobDto = _mapper.Map<Job>(job);
            return CreatedAtAction("GetJob", new { id = job.EfJobId }, jobDto);
        }

        // POST: api/Jobs
        /// <summary>
        /// Add model to job.
        /// </summary>
        /// <param name="jobId">jobId</param>
        /// <param name="modelId">modelId</param>
        /// <returns></returns>
        [HttpPost("{jobId}/model/{modelId}")]
        //[Authorize(Roles = "Manager")]
        public async Task<ActionResult<EfJob>> AddModelToJob(long jobId, long modelId)
        {
            var job = await _context.Jobs.FindAsync(jobId).ConfigureAwait(false);
            if (job == null)
            {
                ModelState.AddModelError("jobId", "jobId not found");
                return BadRequest(ModelState);
            }
            var model = await _context.Models.FindAsync(modelId).ConfigureAwait(false);
            if (model == null)
            {
                ModelState.AddModelError("modelId", "modelId not found");
                return BadRequest(ModelState);
            }

#pragma warning disable CS8603 // Possible null reference return.
            _context.Entry(job)
                .Collection(j => j.JobModels)
                .Load();
#pragma warning restore CS8603 // Possible null reference return.
            if (job.JobModels != null)
                job.JobModels.Add(new EfJobModel()
                {
                    Job = job,
                    Model = model
                });

            await _context.SaveChangesAsync();

            var jobDto = _mapper.Map<Job>(job);
            if (job.JobModels != null)
                foreach (var jobModel in job.JobModels)
                {
                    var modelDto = _mapper.Map<Model>(jobModel.Model);
                    jobDto.Models.Add(modelDto);
                }


            return Created(job.EfJobId.ToString(), jobDto);
        }

        // DELETE: api/Jobs/1/model/3.
        /// <summary>
        /// Removes the model from the job.
        /// </summary>
        /// <param name="jobId">jobId</param>
        /// <param name="modelId">ModelId</param>
        /// <returns></returns>
        [HttpDelete("{jobId}/model/{modelId}")]
        //[Authorize(Roles = "Manager")]
        public async Task<ActionResult<EfJob>> RemoveModelFromJob(long jobId, long modelId)
        {
            var jobModel = await _context.JobModels.Where(
                jm => jm.EfJobId == jobId && jm.EfModelId == modelId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (jobModel == null)
            {
                ModelState.AddModelError("jobId", "jobId or modelId not found");
                return BadRequest(ModelState);
            }

            _context.JobModels.Remove(jobModel);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> DeleteJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                ModelState.AddModelError("jobId", "jobId or modelId not found");
                return BadRequest(ModelState);
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool JobExists(long id)
        {
            return _context.Jobs.Any(e => e.EfJobId == id);
        }
    }
}
