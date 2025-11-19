namespace HeyDEAN_API.Services
{
    public interface IIntentService
    {
        public (string intent, Dictionary<string,string>? parameters) ParseIntent(string text);
    }

    public class IntentService : IIntentService
    {
        public (string intent, Dictionary<string, string>? parameters) ParseIntent(string text)
        {
            var t = text.Trim().ToLowerInvariant();

            // simple examples
            if (t.Contains("vejret") || t.Contains("weather"))
                return ("weather", null);

            if (t.StartsWith("tilføj") || t.StartsWith("add") || t.Contains("create task") || t.Contains("create a task"))
            {
                // Extract simple title
                // naive parsing: take substring after keywords
                var title = t;
                string[] keywords = new[] { "tilføj", "add", "create task", "create a task", "add task" };
                foreach (var k in keywords)
                {
                    if (title.Contains(k))
                    {
                        var idx = title.IndexOf(k) + k.Length;
                        var maybe = title.Substring(idx).Trim(new char[] { ' ', ':' , '-' , '"' });
                        if (!string.IsNullOrWhiteSpace(maybe))
                        {
                            return ("create_task", new Dictionary<string,string>{{ "title", maybe }});
                        }
                    }
                }

                return ("create_task", new Dictionary<string,string>{{ "title", "New task" }});
            }

            if (t.StartsWith("hvad") && t.Contains("på kalender") || t.Contains("calendar"))
                return ("calendar_query", null);

            return ("unknown", null);
        }
    }
}
