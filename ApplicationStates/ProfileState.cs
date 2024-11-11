using SharedLibrary.DTOs;

namespace Client.ApplicationStates
{
    public class ProfileState
    {
        public Action? Action { get; set; }
        public Profile Profile { get; set; } = new();
        public void ProfileUpdated() => Action?.Invoke();

    }
}