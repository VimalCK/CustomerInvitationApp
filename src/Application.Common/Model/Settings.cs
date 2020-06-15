namespace Application.Common.Model{
    public sealed class Settings{
        public string Title { get; set; }
        public int Distance { get; set; }
        public Location OfficeLocation { get; set; }
        public string OutputFile { get; set; }
    }
}