namespace FootballNeighborhood.Services.Repositories;

public interface IUserConfirmationRepository
{
    Task<bool> IsConfirmationActiveForUserId(int userId);
}

