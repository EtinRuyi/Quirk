namespace Quirk.Models.ViewModels
{
    public class EditTagRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
