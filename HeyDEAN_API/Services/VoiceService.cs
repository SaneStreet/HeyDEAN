using System.Globalization;
using HeyDEAN_API.DTOs;
using HeyDEAN_API.Models;
using HeyDEAN_API.Repositories.Interfaces;

namespace HeyDEAN_API.Services
{
    public interface IVoiceService
    {
        Task<VoiceExecuteResponse> ExecuteAsync(VoiceExecuteRequest request);
    }

    public class VoiceService : IVoiceService
    {
        private readonly IIntentService _intentService;
        private readonly IGenericRepository<TaskItem> _taskRepo;
        private readonly IGenericRepository<VoiceLog> _voiceRepo;

        public VoiceService(IIntentService intentService,
            IGenericRepository<TaskItem> taskRepo,
            IGenericRepository<VoiceLog> voiceRepo)
        {
            _intentService = intentService;
            _taskRepo = taskRepo;
            _voiceRepo = voiceRepo;
        }

        public async Task<VoiceExecuteResponse> ExecuteAsync(VoiceExecuteRequest request)
        {
            var (intent, parameters) = _intentService.ParseIntent(request.Transcript);
            var response = new VoiceExecuteResponse { Success = false, ActionPerformed = "none" };

            try
            {
                if (intent == "create_task")
                {
                    var title = parameters != null && parameters.ContainsKey("title")
                        ? parameters["title"]
                        : request.Transcript;

                    var t = new TaskItem
                    {
                        Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title),
                        UserId = request.UserId ?? Guid.Empty
                    };

                    var created = await _taskRepo.AddAsync(t);
                    response.Success = true;
                    response.ActionPerformed = "task_created";
                    response.Data = new { taskId = created.TaskId, title = created.Title };
                    response.Message = "Task oprettet";
                }
                else if (intent == "weather")
                {
                    response.Success = true;
                    response.ActionPerformed = "weather_report";
                    response.Message = "Vejr-rapport (dummy) : 7°C, overskyet";
                }
                else if (intent == "unknown")
                {
                    response.Success = false;
                    response.ActionPerformed = "unknown";
                    response.Message = "Jeg forstod ikke kommandoen";
                }

                // Save voice log
                var log = new VoiceLog
                {
                    UserId = request.UserId,
                    Transcript = request.Transcript,
                    Intent = intent,
                    Response = response.Message ?? string.Empty
                };
                await _voiceRepo.AddAsync(log);

                return response;
            }
            catch (Exception ex)
            {
                return new VoiceExecuteResponse
                {
                    Success = false,
                    ActionPerformed = "error",
                    Message = ex.Message
                };
            }
        }
    }
}
