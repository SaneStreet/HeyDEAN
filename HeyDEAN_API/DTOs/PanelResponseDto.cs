namespace HeyDEAN_API.DTOs
{
    public class PanelResponseDto<T>
    {
        public string Type { get; set; } = "";
        public IEnumerable<T> Items { get; set; } = [];
    }
}
