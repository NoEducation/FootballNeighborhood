namespace FootballNeighborhood.Domain.Options
{
    public class ConfirmationOptions
    {
        public static string Key => "ConfirmationOptions";

        public int ConfirmationValidTimeMinutes { get; set; }
    }
}
