using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.Services.concrets;

public class UserOnlineStatusService
{
    public Dictionary<int, List<string>> _userConnections { get; set; }

    public UserOnlineStatusService()
    {
        _userConnections = new Dictionary<int, List<string>>();
    }

    public void AddConnectionId(User user, string connectionId)
    {
        if (_userConnections.Any(x => x.Key == user.Id))
        {
            List<string> connectionIds = _userConnections[user.Id];
            connectionIds.Add(connectionId);
        }
        else
        {
            _userConnections.Add(user.Id, new List<string> { connectionId });
        }
    }

    public void RemoveConnectionId(User user, string connectionId)
    {
        List<string> connectionIds = _userConnections[user.Id];
        connectionIds.Remove(connectionId);

        if (!connectionIds.Any())
        {
            _userConnections.Remove(user.Id);
        }
    }

    public List<string> GetAllConnectionIds(User user)
    {
        if (_userConnections.ContainsKey(user.Id))
        {
            return _userConnections[user.Id];
        }

        return new List<string>();
    }




}
