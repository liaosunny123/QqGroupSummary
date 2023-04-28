using Sorux.Bot.Core.Interface.PluginsSDK.Ability;
using Sorux.Bot.Core.Interface.PluginsSDK.Register;

namespace EpicMo.Plugins.QqGroupSummary;

public class Register : IBasicInformationRegister, ICommandPrefix, ICommandPermission
{
    public string GetAuthor()
    {
        return "EpicMo";
    }

    public string GetDescription()
    {
        return "QQ群信息整理，可以提供消息概括，话题检测后私发等省流功能";
    }

    public string GetName()
    {
        return "QqGroupSummary";
    }

    public string GetVersion()
    {
        return "1.0.0-release";
    }

    public string GetDLL()
    {
        return "EpicMo.Plugins.QqGroupSummary.dll";
    }

    public string GetPermissionDeniedMessage()
    {
        return "";
    }

    public bool IsPermissionDeniedAutoAt()
    {
        return false;
    }

    public bool IsPermissionDeniedLeakOut()
    {
        return false;
    }

    public bool IsPermissionDeniedAutoReply()
    {
        return false;
    }

    public string GetCommandPrefix()
    {
        return "#";
    }
}