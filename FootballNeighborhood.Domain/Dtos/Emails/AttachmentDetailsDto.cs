namespace FootballNeighborhood.Domain.Dtos.Emails;

public class AttachmentDetailsDto
{
    public string Name { get; }
    public Stream Stream { get; }

    public AttachmentDetailsDto(string name, Stream stream)
    {
        Name = name;
        Stream = stream;
    }
}
