using Model.Entities;
using serilizer = Newtonsoft.Json.JsonConvert;

namespace Domain.Services;

public class FileService{
    public void WriteSessionInfoToFile(string path, SessionInfo sessionInfo){
        var json = serilizer.SerializeObject(sessionInfo);
        File.WriteAllText(path + "/sessionInfo.json", json);
    }

    public SessionInfo? ReadSessionInfoFromFile(string name){
        var path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, $"Databases\\{name}\\sessionInfo.json");
        var json = File.ReadAllText(path);
        SessionInfo? sessionInfo = serilizer.DeserializeObject<SessionInfo>(json);
        return new SessionInfo();
    }
}