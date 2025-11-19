namespace HeyDEAN_API.DTOs
{
    public class VoiceExecuteRequest
    {
        public string Transcript {get; set;} = string.Empty;
        public string? IntentOverride {get; set;}
        public Guid? UserId {get; set;}
    }

    public class VoiceExecuteResponse
    {
        public bool Success {get; set;}
        public string ActionPerformed {get; set;} = string.Empty;
        public object? Data {get; set;}
        public string Message {get; set;} = string.Empty;
    }
}