namespace RandomNameGenerator.Application.DTO
{
    public class NamesDTO
    {
        public string Name { get; }
        public bool GenderId { get; }

        public NamesDTO(string name, bool genderId)
        {
            Name = name;
            GenderId = genderId;
        }
    }
}
